using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DLUProject.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage="Email bắt buộc nhập")]
        [Display(Name = "Email / Tên đăng nhập")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage="Mật khẩu bắt buộc nhập")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ tôi?")]
        public bool RememberMe { get; set; }
    }
}
