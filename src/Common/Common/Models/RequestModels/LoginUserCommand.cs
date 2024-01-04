﻿using Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.RequestModels
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public LoginUserCommand()
        {
            
        }
        public LoginUserCommand(string emailAddress, string password)
        {
            EmailAddress=emailAddress;
            Password=password;
        }
    }
    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
