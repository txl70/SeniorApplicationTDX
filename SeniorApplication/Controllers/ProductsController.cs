using System.Linq;
using System.Web.Mvc;
using SeniorApplication.App_Data.Context;

namespace SeniorApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SeniorApplicationContext _context;

        public ProductsController() : this(new SeniorApplicationContext()) { }

        public ProductsController(SeniorApplicationContext context)
        {
            _context = context;
        }

        // GET: CsvFile
        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Show()
        {
            return View(_context.Products.ToList());
        }
    }
}