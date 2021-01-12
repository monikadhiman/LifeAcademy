using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Core.Dtos
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
    }
}
