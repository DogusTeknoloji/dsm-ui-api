﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Models.User
{
    public class DomainUserHolder
    {
        public User User { get; set; }
        public DomainUserInfo DomainUser { get; set; }
    }
}
