﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Core.Models
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpirtyTime { get; set; }
    }
}
