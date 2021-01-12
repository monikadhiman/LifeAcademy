using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Core.Models;

namespace UserApi.Core.Dtos
{
  public  class GetAllUserDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public  byte[]  Image { get; set; }
        public string Image1 { get; set; }
        public StateDto StateDto { get; set; }
        public bool isVerified { get; set; }

        //public Country country { get; set; }


    }
}
