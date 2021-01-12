using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserApi.Core.Dtos
{
   public class SetPasswordDto
    {
        [Required]
        public Guid guid { get; set; }
        public string Password { get; set; }
    }
}
