﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myHell.WebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}