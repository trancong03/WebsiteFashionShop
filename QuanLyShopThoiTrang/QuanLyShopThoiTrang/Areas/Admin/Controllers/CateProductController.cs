using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class CateProductController : Controller
    {
        // GET: Admin/CateProduct
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=MSI\CONG03;Initial Catalog=QuanLyShop;Integrated Security=True");

        public ActionResult CateProduct()
        {
            var listCate = db.Categories.ToList();
            return View(listCate);

        }
        public ActionResult AddCate()
        {
          
            return View();

        }
        [HttpPost]
        public ActionResult AddCate([Bind(Include = "category_name")] Category p)
        {
            if (ModelState.IsValid)
            {
                db.Categories.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("CateProduct");
        }
        public ActionResult UpdateCate(int id)
        {
            Product product = db.Products.Where(row => row.product_id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult SuaSP(Product pr)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Where(row => row.product_id == pr.product_id).FirstOrDefault();
                //Update
                product.product_id = pr.product_id;
                product.Title = pr.Title;
                product.price = pr.price;
                product.ImageSP = pr.ImageSP;
                product.product_name = pr.product_name;
                product.Dateadd = pr.Dateadd;
                db.SubmitChanges();
            }
            return RedirectToAction("ProductPatial");
        }

        public ActionResult XoaSP(int id)
        {
            Product product = db.Products.Where(row => row.product_id == id).FirstOrDefault();
            return View(product);

        }
        [HttpPost]
        public ActionResult XoaSP(Product p)
        {
            Product product = db.Products.Where(row => row.product_id == p.product_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("ProductPatial");
            }
            else
            {
                try
                {
                    db.Products.DeleteOnSubmit(product);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("ProductPatial");

                }
                return RedirectToAction("ProductPatial");
            }
        }
        public ActionResult ThemMauSP()
        {
            List<Product> product = db.Products.ToList();
            ViewBag.product = product;
            List<Size> sizes = db.Sizes.ToList();
            ViewBag.sizes = sizes;
            List<Color> colors = db.Colors.ToList();
            ViewBag.colors = colors;
            return View();
        }
    }
}