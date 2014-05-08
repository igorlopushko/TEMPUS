using System;
using System.Web.Security;
using TEMPUS.WebSite.Interfaces;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.WebSite.Services
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        private readonly ICommandSender _commandSender;

        public AccountMembershipService(MembershipProvider provider, ICommandSender commandSender)
        {
            _provider = provider ?? Membership.Provider;
            _commandSender = commandSender;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", "password");
            }

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, string lastName, Guid roleId, DateTime dateOfBirth)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", "password");
            }
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", "email");
            }

            MembershipCreateStatus status;
            var user = _provider.CreateUser(userName, password, email, null, null, true, null, out status);

            ICommand command;
            if (user != null && user.ProviderUserKey != null)
            {
                var userId = new Guid();
                Guid.TryParse(user.ProviderUserKey.ToString(), out userId);

                command = new ChangeUserInformation(new UserId(userId), null, null, userName, lastName, dateOfBirth);
                _commandSender.Send(command);

                command = new AddRoleToUser(new UserId(userId), roleId);
                _commandSender.Send(command);
            }

            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(oldPassword))
            {
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            }
            if (String.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");
            }

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }
}