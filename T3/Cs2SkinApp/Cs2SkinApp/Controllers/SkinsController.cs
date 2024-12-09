using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cs2SkinApp.Controllers
{
    public class SkinsController : Controller
    {
        public async Task<IActionResult> Skins()
        {
            var httpClient = new HttpClient();
            string url = "https://steamcommunity.com/market/search/render/?appid=730&norender=1&start=0&count=100"; // Steam Market API URL
            string baseImageUrl = "https://steamcommunity-a.akamaihd.net/economy/image/"; // Base URL for images from Steam Market API
            var response = await httpClient.GetStringAsync(url);

            // Deserialize JSON response from Steam Market API
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(response);
            var skins = searchResult.Results.Select(r => new Skin
            {
                Name = r.Name,
                Sell_Listings = r.Sell_Listings,
                Sell_Price = r.Sell_Price,
                Asset_Description = new AssetDescription
                {
                    Icon_Url = baseImageUrl + r.Asset_Description.Icon_Url,
                    Instance_id = r.Asset_Description.Instance_id
                }
            }).ToList();

            // ViewBags for all the data we want to display on the skins page
            ViewBag.MostExpensiveSoldToday = skins.OrderByDescending(s => s.Sell_Price).FirstOrDefault()?.Name;
            ViewBag.MostExpensiveSoldTodayImage = skins.OrderByDescending(s => s.Sell_Price).FirstOrDefault()?.Asset_Description.Icon_Url;

            ViewBag.MostExpensiveSkinMarket = skins.OrderByDescending(s => s.Sell_Price).FirstOrDefault()?.Name;
            ViewBag.MostExpensiveSkinMarketImage = skins.OrderByDescending(s => s.Sell_Price).FirstOrDefault()?.Asset_Description.Icon_Url;

            ViewBag.MostSoldSkinToday = skins.OrderByDescending(s => s.Sell_Listings).FirstOrDefault()?.Name;
            ViewBag.MostSoldSkinTodayImage = skins.OrderByDescending(s => s.Sell_Listings).FirstOrDefault()?.Asset_Description.Icon_Url;

            ViewBag.MostListedSkinMarket = skins.OrderByDescending(s => s.Sell_Listings).FirstOrDefault()?.Name;
            ViewBag.MostListedSkinMarketImage = skins.OrderByDescending(s => s.Sell_Listings).FirstOrDefault()?.Asset_Description.Icon_Url;

            ViewBag.NewestSkinAdded = skins.OrderByDescending(s => s.Asset_Description.Instance_id).FirstOrDefault()?.Name;
            ViewBag.NewestSkinAddedImage = skins.OrderByDescending(s => s.Asset_Description.Instance_id).FirstOrDefault()?.Asset_Description.Icon_Url;

            ViewBag.OldestSkinMarket = skins.OrderBy(s => s.Asset_Description.Instance_id).FirstOrDefault()?.Name;
            ViewBag.OldestSkinMarketImage = skins.OrderBy(s => s.Asset_Description.Instance_id).FirstOrDefault()?.Asset_Description.Icon_Url;

            return View(skins);
        }

        public async Task<IActionResult> Search(string query, string sortBy)
        {
            var httpClient = new HttpClient();
            string url = $"https://steamcommunity.com/market/search/render/?appid=730&query={query}&norender=1&start=0&count=100"; // Steam Market API URL
            string baseImageUrl = "https://steamcommunity-a.akamaihd.net/economy/image/"; // Base URL for images from Steam Market API
            var response = await httpClient.GetStringAsync(url);

            // Deserialize JSON response from Steam Market API
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(response);
            var skins = searchResult.Results.Select(r => new Skin
            {
                Name = r.Name,
                Hash_Name = r.Hash_Name,
                Sell_Listings = r.Sell_Listings,
                Sell_Price = r.Sell_Price,
                Sell_Price_Text = r.Sell_Price_Text,
                Sale_Price_Text = r.Sale_Price_Text,
                Asset_Description = new AssetDescription
                {
                    Icon_Url = baseImageUrl + r.Asset_Description.Icon_Url,
                    Type = r.Asset_Description.Type
                }
            }).ToList();

            // Sort skins based on the sortBy parameter (price_asc, price_desc)
            skins = sortBy switch
            {
                "price_asc" => skins.OrderBy(s => s.Sell_Price).ToList(),
                "price_desc" => skins.OrderByDescending(s => s.Sell_Price).ToList(),
                _ => skins
            };

            return View(skins);
        }

    }
}
