using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Core.Models;

namespace UserApi.Core.Dtos
{
   public class StateDto
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        //public int CountryId { get; set; }
       
        public Country Country { get; set; }
    }
}
