using PagedList;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shop/Details/5
        public ActionResult Details(string id)
        {
            var db = ProductModel.Instance.ChiTiet(id);

            //UpdateProductViews(id);

            return View(db);
        }

        public void UpdateProductViews(string id)
        {
            ProductModel.Instance.UpdateProductViews(id);
        }

        /*
         * POST:Product/UpdateLike
         */
        [HttpPost]
        public void UpdateProductLike(string id) => ProductModel.Instance.UpdateProductLike(id);

        /*
         * Display all phone and has a page size,each page will display 4 products
         */
        public ActionResult AllPhone(int page = 1, int pageSize = 4)
        {
            var db = ProductModel.Instance.AllPhone().ToPagedList(page, pageSize);
            return View(db);
        }

        /*
         * Display all Laptop and has a page size,each page will display 4 products
         */
        public ActionResult AllLaptop(int page = 1, int pageSize = 4) => View(ProductModel.Instance.AllLaptop().ToPagedList(page, pageSize));

        /*
         * Display products of the same type with a price difference of 2M VND
         */
        public ActionResult SimilarProducts(string producerCode, int price) => View(ProductModel.Instance.SimilarProducts(producerCode, price));

        /*
         * Get all Products from Database
         */
        private static IEnumerable<SanPham> GetAllProducts() => ProductModel.Instance.DanhSach();

        private static int s_count;
        public static int CountAllProductsFound { get => s_count; set => s_count = value; }

        //Create new List<SanPham> listProduct to add items using GetAllProducts() method and find characters request from client
        //then response the first 4 products
        [HttpGet]
        public ActionResult SearchProductByName(string queryProductName)
        {
            List<SanPham> listProduct = new List<SanPham>();
            listProduct.AddRange(GetAllProducts());

            var resultSearchProduct = listProduct.FindAll(product => product.TenSanPham.ToLower().Contains(queryProductName));

            //Initialize session from found product
            Session[queryProductName] = resultSearchProduct;
            Session["loadMoreProduct"] = 4;

            CountProductsFoundBySessionName(queryProductName);

            return View(resultSearchProduct.Take(4));
        }

        //Count the number products found when client search
        public void CountProductsFoundBySessionName(string sessionName)
        {
            List<SanPham> listProducts = (List<SanPham>)Session[sessionName];

            CountAllProductsFound = listProducts.Count;
        }

        //If the product finds more than 4 results,if the client request to see more products,it will continue to display 4 products
        //find first character '=' to split into new string
        [HttpPost]
        public ActionResult LoadMoreProductSearch(string urlSearchResults)
        {
            string[] sessionName = urlSearchResults.Split(new[] { '=' }, 2);

            List<SanPham> loadMoreListProduct = (List<SanPham>)Session[sessionName[1]];

            //The number of products taken out each time increases by 4 products
            Session["loadMoreProduct"] = Convert.ToInt32(Session["loadMoreProduct"]) + 4;
            int numbers = Convert.ToInt32(Session["loadMoreProduct"]);

            CountProductsFoundBySessionName(sessionName[1]);

            //Delete session when the number of lists if less than or equal to the number of products retrieved
            if (loadMoreListProduct.Count <= numbers)
            {
                Session.Remove(sessionName[1]);
            }

            return PartialView("_LoadMoreProductSearch", loadMoreListProduct.Take(numbers));
        }
    }
}
