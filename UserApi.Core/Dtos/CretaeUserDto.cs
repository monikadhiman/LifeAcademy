using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UserApi.Core.Models;

namespace UserApi.Core.Dtos
{
   public class CretaeUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //public DateTime CreatedOn { get; set; }
        public int StateId { get; set; }
        public string Password { get; set; }
        public string Image1 { get; set; }
        public IFormFile file { get; set; }
        public bool isVerified { get; set; }

    }
}
