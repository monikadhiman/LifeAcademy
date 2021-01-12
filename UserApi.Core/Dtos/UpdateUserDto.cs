using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Core.Dtos
{
   public class UpdateUserDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int StateId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Password { get; set; }
        public byte[] Image { get; set; }
        public StateDto StateDto;
        public IFormFile file { get; set; }
        public string Image1 { get; set; }
        public bool isVerified { get; set; }


    }
}
