using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class StatisticController : Controller
{
    private readonly ICatalogStatisticService _catalogStatisticService;
    private readonly IUserStatisticService _userStatisticService;
    private readonly ICommentStatisticService _commentStatisticService;
    private readonly IDiscountStatisticService _discountStatisticService;
    private readonly IMessageStatisticService _messageStatisticService;
    public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, ICommentStatisticService commentStatisticService, IDiscountStatisticService discountStatisticService, IMessageStatisticService messageStatisticService)
    {
        _catalogStatisticService = catalogStatisticService;
        _userStatisticService = userStatisticService;
        _commentStatisticService = commentStatisticService;
        _discountStatisticService = discountStatisticService;
        _messageStatisticService = messageStatisticService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.v0 = "İstatistiksel Veriler";
        ViewBag.v1 = "Admin";
        ViewBag.v2 = "İstatistiksel Veri";
        ViewBag.v3 = "İstatistiksel İşlemler";

        #region Catalog
        var getBrandCount = await _catalogStatisticService.GetBrandCountAsync();
        var getProductCount = await _catalogStatisticService.GetProductCountAsync();
        var getCategoryCount = await _catalogStatisticService.GetCategoryCountAsync();
        var getMaxPriceProductName = await _catalogStatisticService.GetMaxPriceProductNameAsync();
        var getMinPriceProductName = await _catalogStatisticService.GetMinPriceProductNameAsync();
        var getProductAveragePrice = await _catalogStatisticService.GetProductAveragePriceAsync();

        ViewBag.getBrandCount = getBrandCount;
        ViewBag.getProductCount = getProductCount;
        ViewBag.getCategoryCount = getCategoryCount;
        ViewBag.getMaxPriceProductName = getMaxPriceProductName;
        ViewBag.getMinPriceProductName = getMinPriceProductName;
        ViewBag.getProductAveragePrice = getProductAveragePrice;
        #endregion

        #region User
        var getUserCount = await _userStatisticService.GetUserCountAsync();
        ViewBag.getUserCount = getUserCount;
        #endregion

        #region Comment
        var getTotalCommentCount = await _commentStatisticService.GetTotalCommentCountAsync();
        var getActiveCommentCount = await _commentStatisticService.GetActiveCommentCountAsync();
        var getPassiveCommentCount = await _commentStatisticService.GetPassiveCommentCountAsync();
        ViewBag.getTotalCommentCount = getTotalCommentCount;
        ViewBag.getActiveCommentCount = getActiveCommentCount;
        ViewBag.getPassiveCommentCount = getPassiveCommentCount;
        #endregion

        #region Discount
        var getDiscountCouponCount = await _discountStatisticService.GetDiscountCouponCountAsync();
        ViewBag.getDiscountCouponCount = getDiscountCouponCount;
        #endregion

        #region Message
        var getTotalMessageCount = await _messageStatisticService.GetTotalMessageCountAsync();
        ViewBag.getTotalMessageCount = getTotalMessageCount;
        #endregion

        return View();
    }
}
