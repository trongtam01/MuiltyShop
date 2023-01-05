using Microsoft.AspNetCore.Http;
using MuiltyShop.Areas.Product.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MuiltyShop.Areas.Product.Data
{
    public class HearstService
    {
        // Key lưu chuỗi json của Hearst
        public const string CARTKEY = "hearst";
        private readonly IHttpContextAccessor _context;
        private readonly HttpContext HttpContext;

        public HearstService(IHttpContextAccessor context)
        {
            _context = context;
            HttpContext = context.HttpContext;
        }

        // Lấy cart từ Session (danh sách CartItem)
        public List<HearstItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<HearstItem>>(jsoncart);
            }
            return new List<HearstItem>();
        }
        // Xóa cart khỏi session
        public void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(List<HearstItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
