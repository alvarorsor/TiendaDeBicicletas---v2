using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TiendaDeBicicletas.Controllers
{
    public class HomeController : Controller
    {

        private bicitucdbEntities db = new bicitucdbEntities();
        // GET: Home
        public ActionResult Index(string message = "")
        {

            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password) {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {

             var user =  db.staffs.FirstOrDefault(e => e.email == email && e.first_name == password);
                //si usuario es distinto de null
                if (user != null)
                {
                    //encontramos un usuario existente y valido
                    FormsAuthentication.SetAuthCookie(user.email, true);
                  
                    return RedirectToAction("Index", "Profile");
                }
                else {

                    return RedirectToAction("Index", new { message= "No encontramos tus datos" });
                }


            }else
            {
                return RedirectToAction("Index", new { message = "Llena los campos para poder iniciar sesion" });
               


            }
            
        }

        [Authorize]
        public ActionResult Logout() {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
    }
}