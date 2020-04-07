
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWorldwide.Models
{
    public class EmailFormViewModel
    {
        [Required(ErrorMessage = "An e-mail is required!")]
        [EmailAddress]
        public string FromEmail { get; set; }
        [Required(ErrorMessage = "A subject is required!")]
        [MinLength(3, ErrorMessage = "Subject should be atleast 3 symbols!")]
        [MaxLength(20, ErrorMessage = "Subject should be maximum 20 symbols!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be atleast 3 symbols!")]
        [MaxLength(15, ErrorMessage = "Name should be maximum 15 symbols!")]
        public string Name { get; set; }
        [MinLength(5, ErrorMessage = "Message shoulde be atleast 3 symbols!")]
        public string Message { get; set; }
    }
}
