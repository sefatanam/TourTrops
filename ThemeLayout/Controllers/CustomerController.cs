using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThemeLayout.Models;

namespace ThemeLayout.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ProjectDbContext _db = new ProjectDbContext();
        // GET: Customer
        [HttpGet]
        public ActionResult Map()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            Customer model = new Customer();
            return View(model);
        }

        [HttpGet]
        public ActionResult Hotels()
        {
            var data = _db.Hotels.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Restuarant()
        {
            var data = _db.Restuarants.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Transports()
        {
            var data = _db.Transports.ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult AddHotel()
        {
            Hotel model = new Hotel();
            return View(model);
        }
        [HttpGet]
        public ActionResult GoogleMap()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddRestuarant()
        {
            Restuarant model = new Restuarant();
            return View(model);
        }


        // ------------------------------ Post Method -----------------------------------//


        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRestuarant(Restuarant model)
        {
            bool Status = false;
            string messege = "";
            if (ModelState.IsValid)
            {

                if (model.ImageData != null)
                {
                    model.Photo = new byte[model.ImageData.ContentLength];
                    model.ImageData.InputStream.Read(model.Photo, 0, model.ImageData.ContentLength);

                }
                else
                {
                    return View(model);
                }


                #region Save in Database
                _db.Restuarants.Add(model);
                _db.SaveChanges();
                #endregion
                Status = true;
                messege = "Success";

            }
            else
            {
                messege = "Invalid Request";
            }
            ViewBag.Message = messege;
            ViewBag.Status = Status;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddHotel(Hotel model)
        {
            bool Status = false;
            string messege = "";
            if (ModelState.IsValid)
            {

                if (model.ImageData != null)
                {
                    model.Photo = new byte[model.ImageData.ContentLength];
                    model.ImageData.InputStream.Read(model.Photo, 0, model.ImageData.ContentLength);

                }
                else
                {
                    return View(model);
                }


                #region Save in Database
                _db.Hotels.Add(model);
                _db.SaveChanges();
                #endregion
                Status = true;
                messege = "Success";

            }
            else
            {
                messege = "Invalid Request";
            }
            ViewBag.Message = messege;
            ViewBag.Status = Status;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddTransport(Transport model)
        {
            bool Status = false;
            string messege = "";
            if (ModelState.IsValid)
            {

                if (model.ImageData != null)
                {
                    model.Image = new byte[model.ImageData.ContentLength];
                    model.ImageData.InputStream.Read(model.Image, 0, model.ImageData.ContentLength);
                }


                #region Save in Database
                _db.Transports.Add(model);
                _db.SaveChanges();
                #endregion
                Status = true;
                messege = "Success";

            }
            else
            {
                messege = "Invalid Request";
            }
            ViewBag.Message = messege;
            ViewBag.Status = Status;
            return View(model);
        }

        #region Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVarified,ActivitionCode,HotelId,TransportId")] Customer user)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region Email Check

                var isAdded = IsEmailExist(user.EmailId);
                if (isAdded)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exist");
                    return View(user);

                }
                #endregion
                #region activition Key
                user.ActivitionCode = System.Guid.NewGuid();
                #endregion
                #region Password Hash
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion

                user.IsEmailVarified = false;

                #region Save in Database
                _db.Customers.Add(user);
                _db.SaveChanges();
                #endregion

                #region Send Email
                SendVarificationLinkEmail(user.EmailId, user.ActivitionCode.ToString());
                #endregion

                message = "Registration is successfully complete,Account varification link has been sent to your email :" + user.EmailId;
                Status = true;

            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);

        }

        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Customer");
        }
        #endregion

        #region Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            string Message = "";


            using (ProjectDbContext db = new ProjectDbContext())
            {
                var data = db.Customers.Where(a => a.EmailId == model.Email).FirstOrDefault();
                if (data != null)
                {
                    if (string.Compare(Crypto.Hash(model.Password), data.Password) == 0)
                    {
                        int timeout = model.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(model.Email, model.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Customer", null);
                    }
                    else
                    {
                        Message = "Wrong Password";
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Customer", null);
                }
            }

            ViewBag.Message = Message;
            return View();

        }
        #endregion

        #region Verify Account Controller
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (ProjectDbContext db = new ProjectDbContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var check = db.Customers.Where(a => a.ActivitionCode == new System.Guid(id)).FirstOrDefault();
                if (check != null)
                {
                    check.IsEmailVarified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = true;

            return View(Status);
        }
        #endregion

        //----------------------------------------------- Non Action Methods ------------------------------------------//

        #region Send VerificationLink And Email Check
        [NonAction]
        public bool IsEmailExist(string email)
        {
            var data = _db.Customers.Where(u => u.EmailId == email).FirstOrDefault();
            return data != null;

        }

        [NonAction]
        public void SendVarificationLinkEmail(string email, string activitionCode)
        {
            var VerifyAccount = "/Customer/VerifyAccount/" + activitionCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, VerifyAccount);
            var formEmail = new MailAddress("Sefatanam@gmail.com", "Voboghure");
            var toEmail = new MailAddress(email);
            var formPassword = "azaxdevError@";
            var subject = "Your Account is Successfully created in Voboghure";

            string body = "<h4>Welcome traveller</h4><br/>Your journey awaits!Explore worldwide hotel deals and receive inspiring hotel articles for your next destination.<br/>" +
                         " <br/>Find the ideal hotel for your next getaway or business trip with one simple search.<br/><br/>"
                           + "Please Click on the link bellow for verify your account <br/><a herf='" + link + "'>" + link + "</a> <br/><br/>   Stay With Us,Thanks.";

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(formEmail.Address, formPassword)

            };
            using (var m = new MailMessage(formEmail, toEmail)
            {

                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(m);

        }
        #endregion


    }
}

