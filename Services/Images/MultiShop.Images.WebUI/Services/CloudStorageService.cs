using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace MultiShop.Images.WebUI.Services;

public class CloudStorageService : ICloudStorageService
{
    private readonly GCSConfigOptions _options;
    private readonly ILogger<CloudStorageService> _logger;
    private readonly GoogleCredential _googleCredential;

    public CloudStorageService(IOptions<GCSConfigOptions> options, ILogger<CloudStorageService> logger)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (string.IsNullOrWhiteSpace(_options.GCPStorageAuthFile))
        {
            throw new InvalidOperationException("GCPStorageAuthFile must be provided in configuration.");
        }

        try
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _googleCredential = environment == Environments.Production
                ? GoogleCredential.FromJson(_options.GCPStorageAuthFile)
                : GoogleCredential.FromFile(_options.GCPStorageAuthFile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating GoogleCredential.");
            throw;
        }
    }

    public async Task DeleteFileAsync(string fileNameToDelete)
    {
        if (string.IsNullOrWhiteSpace(fileNameToDelete))
        {
            throw new ArgumentException("File name to delete cannot be null or empty.", nameof(fileNameToDelete));
        }

        try
        {
            using var storageClient = StorageClient.Create(_googleCredential);
            await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToDelete);
            _logger.LogInformation("File {FileName} deleted", fileNameToDelete);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting file {FileName}", fileNameToDelete);
            throw;
        }
    }

    public async Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
    {
        if (string.IsNullOrWhiteSpace(fileNameToRead))
        {
            throw new ArgumentException("File name to read cannot be null or empty.", nameof(fileNameToRead));
        }

        try
        {
            if (_googleCredential.UnderlyingCredential is not ServiceAccountCredential sac)
            {
                throw new InvalidOperationException("GoogleCredential must be a ServiceAccountCredential to generate signed URLs.");
            }

            var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
            var signedUrl = await urlSigner.SignAsync(
                _options.GoogleCloudStorageBucketName,
                fileNameToRead,
                TimeSpan.FromMinutes(timeOutInMinutes));

            _logger.LogInformation("Signed URL obtained for file {FileName}", fileNameToRead);
            return signedUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while obtaining signed URL for file {FileName}", fileNameToRead);
            throw;
        }
    }

    public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
    {
        if (fileToUpload == null || fileToUpload.Length == 0)
        {
            throw new ArgumentException("File to upload cannot be null or empty.", nameof(fileToUpload));
        }

        if (string.IsNullOrWhiteSpace(fileNameToSave))
        {
            throw new ArgumentException("File name to save cannot be null or empty.", nameof(fileNameToSave));
        }

        try
        {
            _logger.LogInformation("Uploading file {FileName} to bucket {Bucket}", fileNameToSave, _options.GoogleCloudStorageBucketName);

            using var memoryStream = new MemoryStream();
            await fileToUpload.CopyToAsync(memoryStream);

            using var storageClient = StorageClient.Create(_googleCredential);
            var uploadedFile = await storageClient.UploadObjectAsync(
                _options.GoogleCloudStorageBucketName,
                fileNameToSave,
                fileToUpload.ContentType,
                memoryStream);

            _logger.LogInformation("File {FileName} uploaded to bucket {Bucket}", fileNameToSave, _options.GoogleCloudStorageBucketName);
            return uploadedFile.MediaLink;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while uploading file {FileName}", fileNameToSave);
            throw;
        }
    }
}
