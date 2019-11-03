using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];//接收Create成功後傳來的訊息 

            List<Models.Product> result = new List<Models.Product>();
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                //使用Linq
                result = (from product in db.Products
                          select product).ToList();
                //result = db.Products.ToList();
            }
            return View(result);
        }


        public ActionResult Create()
        {
            return View();
        }

        //Create頁面資料傳回處理
        [HttpPost] // 只有用POST方法才可進入
        public ActionResult Create(Models.Product postback)
        {
            if(this.ModelState.IsValid) //資料驗證正確才新增進db
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    db.Products.Add(postback);
                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品[{0}]建立成功", postback.Name);//加入訊息 Index會接收這個訊息
                    return RedirectToAction("Index"); //自動跳轉回Product頁面

                }
            }
            ViewBag.ResultMessage = "資料有誤"; //加入訊息
            return View(); //停留在Create頁面 //要加postback嗎?
        }

    }
}