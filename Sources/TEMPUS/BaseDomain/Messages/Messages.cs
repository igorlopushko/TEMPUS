
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;


namespace TEMPUS.BaseDomain.Messages
{
		
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AssignUserToProject : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public ProjectId ProjectId { get; set; }
		private AssignUserToProject () {}
		public AssignUserToProject (UserId userId, ProjectId projectId)
		{
			Id = userId;
			ProjectId = projectId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AssignUserToProject;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && ProjectId.Equals(target.ProjectId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ ProjectId.GetHashCode();
		}
		public static bool operator ==(AssignUserToProject a, AssignUserToProject b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(AssignUserToProject a, AssignUserToProject b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserAssignedToProject : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public ProjectId ProjectId { get; set; }
		private UserAssignedToProject () {}
		public UserAssignedToProject (UserId userId, ProjectId projectId)
		{
			Id = userId;
			ProjectId = projectId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserAssignedToProject;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && ProjectId.Equals(target.ProjectId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ ProjectId.GetHashCode();
		}
		public static bool operator ==(UserAssignedToProject a, UserAssignedToProject b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(UserAssignedToProject a, UserAssignedToProject b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserCreated : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		private UserCreated () {}
		public UserCreated (UserId userId, string login, string password)
		{
			Id = userId;
			Login = login;
			Password = password;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserCreated;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Login.Equals(target.Login) && Password.Equals(target.Password);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Login.GetHashCode() ^ Password.GetHashCode();
		}
		public static bool operator ==(UserCreated a, UserCreated b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(UserCreated a, UserCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CreateUser : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		private CreateUser () {}
		public CreateUser (UserId userId, string login, string password)
		{
			Id = userId;
			Login = login;
			Password = password;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CreateUser;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Login.Equals(target.Login) && Password.Equals(target.Password);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Login.GetHashCode() ^ Password.GetHashCode();
		}
		public static bool operator ==(CreateUser a, CreateUser b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(CreateUser a, CreateUser b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ChangeUserInformation : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public int Age { get; set; }
		public string Phone { get; set; }
		public string Image { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		private ChangeUserInformation () {}
		public ChangeUserInformation (UserId userId, int age, string phone, string image, string password, string firstName, string lastName)
		{
			Id = userId;
			Age = age;
			Phone = phone;
			Image = image;
			Password = password;
			FirstName = firstName;
			LastName = lastName;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ChangeUserInformation;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Age.Equals(target.Age) && Phone.Equals(target.Phone) && Image.Equals(target.Image) && Password.Equals(target.Password) && FirstName.Equals(target.FirstName) && LastName.Equals(target.LastName);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Age.GetHashCode() ^ Phone.GetHashCode() ^ Image.GetHashCode() ^ Password.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode();
		}
		public static bool operator ==(ChangeUserInformation a, ChangeUserInformation b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(ChangeUserInformation a, ChangeUserInformation b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserInformationChanged : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public int Age { get; set; }
		public string Phone { get; set; }
		public string Image { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		private UserInformationChanged () {}
		public UserInformationChanged (UserId userId, int age, string phone, string image, string password, string firstName, string lastName)
		{
			Id = userId;
			Age = age;
			Phone = phone;
			Image = image;
			Password = password;
			FirstName = firstName;
			LastName = lastName;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserInformationChanged;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Age.Equals(target.Age) && Phone.Equals(target.Phone) && Image.Equals(target.Image) && Password.Equals(target.Password) && FirstName.Equals(target.FirstName) && LastName.Equals(target.LastName);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Age.GetHashCode() ^ Phone.GetHashCode() ^ Image.GetHashCode() ^ Password.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode();
		}
		public static bool operator ==(UserInformationChanged a, UserInformationChanged b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(UserInformationChanged a, UserInformationChanged b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class DeleteUser : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private DeleteUser () {}
		public DeleteUser (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as DeleteUser;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(DeleteUser a, DeleteUser b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(DeleteUser a, DeleteUser b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserDeleted : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private UserDeleted () {}
		public UserDeleted (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserDeleted;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public static bool operator ==(UserDeleted a, UserDeleted b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Equals(b);
		}
		public static bool operator !=(UserDeleted a, UserDeleted b)
		{
			return !(a == b);
		}
	}

}