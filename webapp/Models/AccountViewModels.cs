using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfiles> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfiles
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }



    public class AccountLoginModel
    {
        [Required(ErrorMessage = "*กรุณาระบุรหัสผู้ใช้งาน")]
        [Display(Name = "รหัสผู้ใช้งาน")]
        //public string Email { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "*กรุณาระบุรหัสผ่าน")]
        [Display(Name = "รหัสผ่าน")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }     
        public string Controller { get; set; }
        public string View { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AccountResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountRegistrationModel
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "*กรุณาระบุรหัสผู้ใช้งาน")]
        [Display(Name = "รหัสผู้ใช้งาน")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*กรุณาระบุรหัสผ่าน")]
        [DataType(DataType.Password)]
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; }
    }

    public class ControllerAndActionModel
    {
        public string Controller { get; set; }
        public string ViewPage { get; set; }
    }
}