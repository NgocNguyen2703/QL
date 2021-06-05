using OnThi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnThi.Controllers
{
    public class AccountController : Controller
    {
        Encrytion encry = new Encrytion();
        QLDBConText db = new QLDBConText();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Nhận DL từ client gửi lên
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                string encrytionpass = encry.PasswordEncrytion(acc.Password);
                var model = db.Accounts.Where(m => m.Username == acc.Username && m.Password == encrytionpass).ToList().Count();
                //Thong tin dang nhap chinh xac
                if (model == 1)
                {
                    //Luu cookie dang nhap thanh cong
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thong tin dang nhap khong chinh xac");
                }
            }
            return View(acc);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //Ma hoa mat khau truoc khi luu vao database
                acc.Password = encry.PasswordEncrytion(acc.Password);
                if (CheckUsername(acc.Username))
                {
                    ModelState.AddModelError("", "Tai khoan da ton tai");
                }
                else
                {
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(acc);
        }
        //Kiem tra username da ton tai trong he thong chua
        public bool CheckUsername(string username)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["QLDBConText"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Accounts where Username = @Username", con))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Username";
                    param.Value = username;
                    cmd.Parameters.Add(param);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                    else
                        return false;
                }
            }
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}