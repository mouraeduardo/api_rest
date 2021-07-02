﻿using api_rest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User { get; private set; }
        public UserResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }
        public UserResponse(User user) : this (true, string.Empty, user) { }
        public UserResponse(string message) : this(false, message, null){ }
    }
}
