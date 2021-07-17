#region Using

using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using KKN_UI.Models;
using WebMatrix.WebData;
using System.Web;

#endregion

namespace KKN_UI.Controllers
{
    [Authorize]
    [KKN_UI.Filters.InitializeSimpleMembership]
    public class AccountController : Controller
    {
        // TODO: This should be moved to the constructor of the controller in combination with a DependencyResolver setup
        // NOTE: You can use NuGet to find a strategy for the various IoC packages out there (i.e. StructureMap.MVC5)
        private readonly UserManager _manager = UserManager.Create();
        public ActionResult Index()
        {
            using (KKN_Demo_Service.KKN_Demo_Service conn = new KKN_Demo_Service.KKN_Demo_Service())
            {
                var vr = conn.GetDemoByDetailId(1);
            }

            return View();
        }
        // GET: /account/forgotpassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session.Clear();
            var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };
            AccountLoginModel LoginModel = new AccountLoginModel();
            if (returnUrl != null && returnUrl != "/")
            {
                //returnUrl = Base64Decode(returnUrl);

                HttpCookie ver = new HttpCookie("Version");
                //ver["VersionAPI"] = GetAPIVersionAPI();
                ver["VersionUI"] = GetVersionUI();
                ver.Expires.Add(new System.TimeSpan(8, 0, 0));
                Response.Cookies.Add(ver);
                if (returnUrl.IndexOf(":") != -1)
                {
                    string[] arrayLogin = returnUrl.Split(':');
                    LoginModel.Username = arrayLogin[0];
                    LoginModel.Password = arrayLogin[1];
                    LoginModel.RememberMe = Convert.ToBoolean(arrayLogin[2]);
                    if (arrayLogin.Length == 5)
                    {
                        LoginModel.Controller = arrayLogin[3];
                        LoginModel.View = arrayLogin[4];
                    }

                    if (WebSecurity.Login(LoginModel.Username, LoginModel.Password, persistCookie: LoginModel.RememberMe))
                    {
                        string encode = Base64Encode(LoginModel.Password);
                        if (viewModel.ReturnUrl != null)
                        {
                            return RedirectToAction(LoginModel.View, LoginModel.Controller);
                        }
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "เกิดข้อผิดพลาดในการเข้าสู่ระบบ.");
                        return View(LoginModel);
                    }
                }
            }
            return View();
        }

        //public string GetAPIVersionAPI()
        //{
        //    string version = "";
        //    using (KKN_Finance_Service conn = new KKN_Finance_Service())
        //    {
        //        var result = conn.GetVersion();
        //        version = result.data.ToString();
        //    }

        //    return version;
        //}

        public string GetVersionUI()
        {
            string version = "";
            var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            version = string.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
            return version;
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(AccountLoginModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            await Task.Delay(500);
            if (WebSecurity.Login(viewModel.Username, viewModel.Password, persistCookie: viewModel.RememberMe))
            {
                string encode = Base64Encode(viewModel.Password);

                Session["SessionUsername"] = viewModel.Username;
                Session["SessionPassword"] = encode;

                HttpCookie ver = new HttpCookie("Version");
                //ver["VersionAPI"] = GetAPIVersionAPI();
                ver["VersionUI"] = GetVersionUI();
                ver.Expires.Add(new System.TimeSpan(8, 0, 0));
                Response.Cookies.Add(ver);

                if (viewModel.ReturnUrl != "" && viewModel.ReturnUrl != "/" && viewModel.ReturnUrl != null)
                {
                    return RedirectToLocal(viewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // No existing user was found that matched the given criteria
            ModelState.AddModelError("", "Invalid username or password.");
            
            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        //public string Base64Decode(string base64EncodedData)
        //{
        //    //var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}


        // GET: /account/error
        [AllowAnonymous]
        public ActionResult Error()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/register
        [AllowAnonymous]
        public ActionResult Register()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View(new AccountRegistrationModel());
        }

        // POST: /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
                return View(viewModel);

            // Prepare the identity with the provided information
            var user = new IdentityUser
            {
                UserName = viewModel.Username ?? viewModel.Email,
                Email = viewModel.Email
            };

            // Try to create a user with the given identity
            try
            {
                var result = await _manager.CreateAsync(user, viewModel.Password);

                // If the user could not be created
                if (!result.Succeeded)
                {
                    // Add all errors to the page so they can be used to display what went wrong
                    AddErrors(result);

                    return View(viewModel);
                }

                // If the user was able to be created we can sign it in immediately
                // Note: Consider using the email verification proces
                await SignInAsync(user, false);

                return RedirectToLocal();
            }
            catch (DbEntityValidationException ex)
            {
                // Add all errors to the page so they can be used to display what went wrong
                AddErrors(ex);

                return View(viewModel);
            }
        }
      
        public ActionResult Logout()
        {

            WebSecurity.Logout();
            // First we clean the authentication ticket like always
            FormsAuthentication.SignOut();

            // Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request

            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            // If the return url starts with a slash "/" we assume it belongs to our site
            // so we will redirect to this "action"
            if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // If we cannot verify if the url is local to our host we redirect to a default location
            return RedirectToAction("index", "home");
        }

        private void AddErrors(DbEntityValidationException exc)
        {
            foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            // Add all errors that were returned to the page error collection
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Create a claims based identity for the current user
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(identity.Name, isPersistent);
        }

        // GET: /account/lock
        [AllowAnonymous]
        public ActionResult Lock()
        {
            return View();
        }
    }
}