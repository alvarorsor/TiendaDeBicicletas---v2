using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaDeBicicletas.Controllers
{
   // [Authorize]
    public class ProfileController : Controller
    {

        private bicitucdbEntities db = new bicitucdbEntities();

        // GET: Profile
        public ActionResult Index()

        
        {



            return View();
        }
    }
}