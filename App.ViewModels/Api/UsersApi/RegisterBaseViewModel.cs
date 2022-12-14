using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.ViewModels.Api.UsersApi
{
        public class RegisterViewModel : RegisterBaseViewModel
        {
            //[GoogleRecaptchaValidation]
            //[BindProperty(Name = "g-recaptcha-response")]
            //public string GoogleRecaptchaResponse { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار کلمه عبور")]
            [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
            public string ConfirmPassword { get; set; }

        }

        public class RegisterBaseViewModel
        {
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            [EmailAddress(ErrorMessage = "ایمیل شما نامعتبر است.")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            public string Password { get; set; }

            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            [Display(Name = "تاریخ تولد")]
            public string BirthDate { get; set; }

            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string UserName { get; set; }

            [Display(Name = "شماره موبایل")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string PhoneNumber { get; set; }

            public bool EmailConfirmed { get; set; }

            public bool PhoneNumberConfirmed { get; set; }
        }

      
    }


