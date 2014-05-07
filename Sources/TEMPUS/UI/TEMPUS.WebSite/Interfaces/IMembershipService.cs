﻿using System;
using System.Web.Security;

namespace TEMPUS.WebSite.Interfaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email, string lastName, string role, DateTime dateOfBirth);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}