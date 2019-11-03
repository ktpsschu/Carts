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
            if (this.ModelState.IsValid) //資料驗證正確才新增進db
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

        public ActionResult Edit(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from product in db.Products
                              where product.Id == id
                              select product).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    return View(result);
                }
                else
                {
                    TempData["ResultMessage"] = "此商品不存在";
                    return RedirectToAction("Index");
                }
            }
        }

        //Edit頁面資料傳回處理
        [HttpPost]
        public ActionResult Edit(Models.Product postback)
        {
            if (this.ModelState.IsValid) //資料驗證正確才新增進db 
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    var result = (from product in db.Products
                                  where product.Id == postback.Id
                                  select product).FirstOrDefault();
                    result.Name = postback.Name;
                    result.Price = postback.Price;
                    result.PublishDate = postback.PublishDate;
                    result.Status = postback.Status;
                    result.Quantity = postback.Quantity;
                    result.DefaultImageId = postback.DefaultImageId;
                    result.Description = postback.Description;
                    result.CategoryId = postback.CategoryId;

                    db.SaveChanges();
                    
                    TempData["ResultMessage"] = String.Format("商品[{0}]編輯成功", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //ViewBag.ResultMessage = "資料有誤"; //加入訊息
                return View(postback); //要加postback嗎?
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from product in db.Products
                              where product.Id == id
                              select product).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    db.Products.Remove(result);
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("商品[{0}]刪除成功", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultMessage"] = "此商品不存在";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}