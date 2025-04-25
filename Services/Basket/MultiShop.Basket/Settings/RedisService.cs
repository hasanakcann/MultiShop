using StackExchange.Redis;

namespace MultiShop.Basket.Settings;

public class RedisService
{
    private readonly string _host;
    private readonly int _port;
    private ConnectionMultiplexer _connection;

    public RedisService(string host, int port, ConnectionMultiplexer connection)
    {
        _host = host;
        _port = port;
        _connection = connection;
    }

    public void Connect()
    {
        if (_connection == null || !_connection.IsConnected)
        {
            _connection = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        }
    }

    public IDatabase GetDatabase(int db = 1)
    {
        if (_connection == null || !_connection.IsConnected)
            throw new InvalidOperationException("Redis connection has not been established. Call Connect() first.");

        return _connection.GetDatabase(db);
    }
}
