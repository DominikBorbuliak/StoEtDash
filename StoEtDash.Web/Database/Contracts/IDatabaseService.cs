﻿using StoEtDash.Web.Database.Models;
using StoEtDash.Web.Models;

namespace StoEtDash.Web.Database.Contracts
{
    public interface IDatabaseService
    {
        User GetUserByUsername(string username);
        void CreateUser(LoginViewModel loginViewModel);
    }
}