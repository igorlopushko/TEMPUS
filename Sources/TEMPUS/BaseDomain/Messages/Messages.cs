﻿
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
	public sealed class AssignUserToProject : ICommand<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		public UserId UserId { get; set; }
		public Guid RoleId { get; set; }
		public int FTE { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		private AssignUserToProject () {}
		public AssignUserToProject (ProjectId projectId, UserId userId, Guid roleId, int fTE, DateTime startDate, DateTime endDate)
		{
			Id = projectId;
			UserId = userId;
			RoleId = roleId;
			FTE = fTE;
			StartDate = startDate;
			EndDate = endDate;
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
			return Id.Equals(target.Id) && UserId.Equals(target.UserId) && RoleId.Equals(target.RoleId) && FTE.Equals(target.FTE) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ UserId.GetHashCode() ^ RoleId.GetHashCode() ^ FTE.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
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
	public sealed class UserAssignedToProject : IEvent<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		public UserId UserId { get; set; }
		public Guid RoleId { get; set; }
		public int FTE { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		private UserAssignedToProject () {}
		public UserAssignedToProject (ProjectId projectId, UserId userId, Guid roleId, int fTE, DateTime startDate, DateTime endDate)
		{
			Id = projectId;
			UserId = userId;
			RoleId = roleId;
			FTE = fTE;
			StartDate = startDate;
			EndDate = endDate;
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
			return Id.Equals(target.Id) && UserId.Equals(target.UserId) && RoleId.Equals(target.RoleId) && FTE.Equals(target.FTE) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ UserId.GetHashCode() ^ RoleId.GetHashCode() ^ FTE.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
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
		public string Phone { get; set; }
		public string Image { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		private ChangeUserInformation () {}
		public ChangeUserInformation (UserId userId, string phone, string image, string firstName, string lastName, DateTime dateOfBirth)
		{
			Id = userId;
			Phone = phone;
			Image = image;
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
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
			return Id.Equals(target.Id) && Phone.Equals(target.Phone) && Image.Equals(target.Image) && FirstName.Equals(target.FirstName) && LastName.Equals(target.LastName) && DateOfBirth.Equals(target.DateOfBirth);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Phone.GetHashCode() ^ Image.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode() ^ DateOfBirth.GetHashCode();
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
		public string Phone { get; set; }
		public string Image { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		private UserInformationChanged () {}
		public UserInformationChanged (UserId userId, string phone, string image, string firstName, string lastName, DateTime dateOfBirth)
		{
			Id = userId;
			Phone = phone;
			Image = image;
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
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
			return Id.Equals(target.Id) && Phone.Equals(target.Phone) && Image.Equals(target.Image) && FirstName.Equals(target.FirstName) && LastName.Equals(target.LastName) && DateOfBirth.Equals(target.DateOfBirth);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Phone.GetHashCode() ^ Image.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode() ^ DateOfBirth.GetHashCode();
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
	public sealed class AddRoleToUser : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public Guid RoleId { get; set; }
		private AddRoleToUser () {}
		public AddRoleToUser (UserId userId, Guid roleId)
		{
			Id = userId;
			RoleId = roleId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AddRoleToUser;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && RoleId.Equals(target.RoleId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ RoleId.GetHashCode();
		}
		public static bool operator ==(AddRoleToUser a, AddRoleToUser b)
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
		public static bool operator !=(AddRoleToUser a, AddRoleToUser b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RoleToUserAdded : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public Guid RoleId { get; set; }
		private RoleToUserAdded () {}
		public RoleToUserAdded (UserId userId, Guid roleId)
		{
			Id = userId;
			RoleId = roleId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RoleToUserAdded;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && RoleId.Equals(target.RoleId);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ RoleId.GetHashCode();
		}
		public static bool operator ==(RoleToUserAdded a, RoleToUserAdded b)
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
		public static bool operator !=(RoleToUserAdded a, RoleToUserAdded b)
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
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class RestoreUser : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private RestoreUser () {}
		public RestoreUser (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as RestoreUser;
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
		public static bool operator ==(RestoreUser a, RestoreUser b)
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
		public static bool operator !=(RestoreUser a, RestoreUser b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserRestored : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private UserRestored () {}
		public UserRestored (UserId userId)
		{
			Id = userId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserRestored;
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
		public static bool operator ==(UserRestored a, UserRestored b)
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
		public static bool operator !=(UserRestored a, UserRestored b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class SetUserMood : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public int Rate { get; set; }
		private SetUserMood () {}
		public SetUserMood (UserId userId, int rate)
		{
			Id = userId;
			Rate = rate;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as SetUserMood;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Rate.Equals(target.Rate);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Rate.GetHashCode();
		}
		public static bool operator ==(SetUserMood a, SetUserMood b)
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
		public static bool operator !=(SetUserMood a, SetUserMood b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class UserMoodSet : IEvent<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		public int Rate { get; set; }
		public DateTime Date { get; set; }
		private UserMoodSet () {}
		public UserMoodSet (UserId userId, int rate, DateTime date)
		{
			Id = userId;
			Rate = rate;
			Date = date;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as UserMoodSet;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Rate.Equals(target.Rate) && Date.Equals(target.Date);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Rate.GetHashCode() ^ Date.GetHashCode();
		}
		public static bool operator ==(UserMoodSet a, UserMoodSet b)
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
		public static bool operator !=(UserMoodSet a, UserMoodSet b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CreateProject : ICommand<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProjectOrderer { get; set; }
		public string RecievingOrganization { get; set; }
		public bool Mandatory { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid PpsClassificationId { get; set; }
		public UserId OwnerId { get; set; }
		public UserId Manager { get; set; }
		private CreateProject () {}
		public CreateProject (ProjectId projectId, string name, string description, string projectOrderer, string recievingOrganization, bool mandatory, DateTime startDate, DateTime endDate, Guid departmentId, Guid ppsClassificationId, UserId ownerId, UserId manager)
		{
			Id = projectId;
			Name = name;
			Description = description;
			ProjectOrderer = projectOrderer;
			RecievingOrganization = recievingOrganization;
			Mandatory = mandatory;
			StartDate = startDate;
			EndDate = endDate;
			DepartmentId = departmentId;
			PpsClassificationId = ppsClassificationId;
			OwnerId = ownerId;
			Manager = manager;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CreateProject;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && Description.Equals(target.Description) && ProjectOrderer.Equals(target.ProjectOrderer) && RecievingOrganization.Equals(target.RecievingOrganization) && Mandatory.Equals(target.Mandatory) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate) && DepartmentId.Equals(target.DepartmentId) && PpsClassificationId.Equals(target.PpsClassificationId) && OwnerId.Equals(target.OwnerId) && Manager.Equals(target.Manager);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ ProjectOrderer.GetHashCode() ^ RecievingOrganization.GetHashCode() ^ Mandatory.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode() ^ DepartmentId.GetHashCode() ^ PpsClassificationId.GetHashCode() ^ OwnerId.GetHashCode() ^ Manager.GetHashCode();
		}
		public static bool operator ==(CreateProject a, CreateProject b)
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
		public static bool operator !=(CreateProject a, CreateProject b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProjectCreated : IEvent<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		private ProjectCreated () {}
		public ProjectCreated (ProjectId projectId)
		{
			Id = projectId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProjectCreated;
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
		public static bool operator ==(ProjectCreated a, ProjectCreated b)
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
		public static bool operator !=(ProjectCreated a, ProjectCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ChangeProjectInformation : ICommand<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProjectOrderer { get; set; }
		public string RecievingOrganization { get; set; }
		public bool Mandatory { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid PpsClassificationId { get; set; }
		public UserId OwnerId { get; set; }
		public UserId Manager { get; set; }
		private ChangeProjectInformation () {}
		public ChangeProjectInformation (ProjectId projectId, string name, string description, string projectOrderer, string recievingOrganization, bool mandatory, DateTime startDate, DateTime endDate, Guid departmentId, Guid ppsClassificationId, UserId ownerId, UserId manager)
		{
			Id = projectId;
			Name = name;
			Description = description;
			ProjectOrderer = projectOrderer;
			RecievingOrganization = recievingOrganization;
			Mandatory = mandatory;
			StartDate = startDate;
			EndDate = endDate;
			DepartmentId = departmentId;
			PpsClassificationId = ppsClassificationId;
			OwnerId = ownerId;
			Manager = manager;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ChangeProjectInformation;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && Description.Equals(target.Description) && ProjectOrderer.Equals(target.ProjectOrderer) && RecievingOrganization.Equals(target.RecievingOrganization) && Mandatory.Equals(target.Mandatory) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate) && DepartmentId.Equals(target.DepartmentId) && PpsClassificationId.Equals(target.PpsClassificationId) && OwnerId.Equals(target.OwnerId) && Manager.Equals(target.Manager);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ ProjectOrderer.GetHashCode() ^ RecievingOrganization.GetHashCode() ^ Mandatory.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode() ^ DepartmentId.GetHashCode() ^ PpsClassificationId.GetHashCode() ^ OwnerId.GetHashCode() ^ Manager.GetHashCode();
		}
		public static bool operator ==(ChangeProjectInformation a, ChangeProjectInformation b)
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
		public static bool operator !=(ChangeProjectInformation a, ChangeProjectInformation b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class ProjectInformationChanged : IEvent<ProjectId>
	{
		public ProjectId Id { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProjectOrderer { get; set; }
		public string RecievingOrganization { get; set; }
		public bool Mandatory { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid PpsClassificationId { get; set; }
		public UserId OwnerId { get; set; }
		public UserId Manager { get; set; }
		private ProjectInformationChanged () {}
		public ProjectInformationChanged (ProjectId projectId, string name, string description, string projectOrderer, string recievingOrganization, bool mandatory, DateTime startDate, DateTime endDate, Guid departmentId, Guid ppsClassificationId, UserId ownerId, UserId manager)
		{
			Id = projectId;
			Name = name;
			Description = description;
			ProjectOrderer = projectOrderer;
			RecievingOrganization = recievingOrganization;
			Mandatory = mandatory;
			StartDate = startDate;
			EndDate = endDate;
			DepartmentId = departmentId;
			PpsClassificationId = ppsClassificationId;
			OwnerId = ownerId;
			Manager = manager;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as ProjectInformationChanged;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && Name.Equals(target.Name) && Description.Equals(target.Description) && ProjectOrderer.Equals(target.ProjectOrderer) && RecievingOrganization.Equals(target.RecievingOrganization) && Mandatory.Equals(target.Mandatory) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate) && DepartmentId.Equals(target.DepartmentId) && PpsClassificationId.Equals(target.PpsClassificationId) && OwnerId.Equals(target.OwnerId) && Manager.Equals(target.Manager);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ ProjectOrderer.GetHashCode() ^ RecievingOrganization.GetHashCode() ^ Mandatory.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode() ^ DepartmentId.GetHashCode() ^ PpsClassificationId.GetHashCode() ^ OwnerId.GetHashCode() ^ Manager.GetHashCode();
		}
		public static bool operator ==(ProjectInformationChanged a, ProjectInformationChanged b)
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
		public static bool operator !=(ProjectInformationChanged a, ProjectInformationChanged b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class CreateTimeRecord : ICommand<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		public UserId UserId { get; set; }
		public ProjectId ProjectId { get; set; }
		public string Description { get; set; }
		public double Effort { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		private CreateTimeRecord () {}
		public CreateTimeRecord (TimeRecordId timeRecordId, UserId userId, ProjectId projectId, string description, double effort, DateTime startDate, DateTime endDate)
		{
			Id = timeRecordId;
			UserId = userId;
			ProjectId = projectId;
			Description = description;
			Effort = effort;
			StartDate = startDate;
			EndDate = endDate;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as CreateTimeRecord;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && UserId.Equals(target.UserId) && ProjectId.Equals(target.ProjectId) && Description.Equals(target.Description) && Effort.Equals(target.Effort) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ UserId.GetHashCode() ^ ProjectId.GetHashCode() ^ Description.GetHashCode() ^ Effort.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
		}
		public static bool operator ==(CreateTimeRecord a, CreateTimeRecord b)
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
		public static bool operator !=(CreateTimeRecord a, CreateTimeRecord b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class TimeRecordCreated : IEvent<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		public UserId UserId { get; set; }
		public ProjectId ProjectId { get; set; }
		public string Description { get; set; }
		public double Effort { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		private TimeRecordCreated () {}
		public TimeRecordCreated (TimeRecordId timeRecordId, UserId userId, ProjectId projectId, string description, double effort, DateTime startDate, DateTime endDate)
		{
			Id = timeRecordId;
			UserId = userId;
			ProjectId = projectId;
			Description = description;
			Effort = effort;
			StartDate = startDate;
			EndDate = endDate;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as TimeRecordCreated;
			if (target == null)
			{
				return false;
			}
			return Id.Equals(target.Id) && UserId.Equals(target.UserId) && ProjectId.Equals(target.ProjectId) && Description.Equals(target.Description) && Effort.Equals(target.Effort) && StartDate.Equals(target.StartDate) && EndDate.Equals(target.EndDate);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ UserId.GetHashCode() ^ ProjectId.GetHashCode() ^ Description.GetHashCode() ^ Effort.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
		}
		public static bool operator ==(TimeRecordCreated a, TimeRecordCreated b)
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
		public static bool operator !=(TimeRecordCreated a, TimeRecordCreated b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class NotifyTimeRecord : ICommand<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private NotifyTimeRecord () {}
		public NotifyTimeRecord (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as NotifyTimeRecord;
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
		public static bool operator ==(NotifyTimeRecord a, NotifyTimeRecord b)
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
		public static bool operator !=(NotifyTimeRecord a, NotifyTimeRecord b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class TimeRecordNotified : IEvent<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private TimeRecordNotified () {}
		public TimeRecordNotified (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as TimeRecordNotified;
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
		public static bool operator ==(TimeRecordNotified a, TimeRecordNotified b)
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
		public static bool operator !=(TimeRecordNotified a, TimeRecordNotified b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class AcceptTimeRecord : ICommand<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private AcceptTimeRecord () {}
		public AcceptTimeRecord (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as AcceptTimeRecord;
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
		public static bool operator ==(AcceptTimeRecord a, AcceptTimeRecord b)
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
		public static bool operator !=(AcceptTimeRecord a, AcceptTimeRecord b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class TimeRecordAccepted : IEvent<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private TimeRecordAccepted () {}
		public TimeRecordAccepted (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as TimeRecordAccepted;
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
		public static bool operator ==(TimeRecordAccepted a, TimeRecordAccepted b)
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
		public static bool operator !=(TimeRecordAccepted a, TimeRecordAccepted b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class DeclineTimeRecord : ICommand<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private DeclineTimeRecord () {}
		public DeclineTimeRecord (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as DeclineTimeRecord;
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
		public static bool operator ==(DeclineTimeRecord a, DeclineTimeRecord b)
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
		public static bool operator !=(DeclineTimeRecord a, DeclineTimeRecord b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class TimeReportDeclined : IEvent<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private TimeReportDeclined () {}
		public TimeReportDeclined (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as TimeReportDeclined;
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
		public static bool operator ==(TimeReportDeclined a, TimeReportDeclined b)
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
		public static bool operator !=(TimeReportDeclined a, TimeReportDeclined b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class DeleteTimeReport : ICommand<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private DeleteTimeReport () {}
		public DeleteTimeReport (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as DeleteTimeReport;
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
		public static bool operator ==(DeleteTimeReport a, DeleteTimeReport b)
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
		public static bool operator !=(DeleteTimeReport a, DeleteTimeReport b)
		{
			return !(a == b);
		}
	}
	
	[Serializable]
	[GeneratedCodeAttribute("MessagesGenerator", "1.0.0.0")]
	public sealed class TimeReportDeleted : IEvent<TimeRecordId>
	{
		public TimeRecordId Id { get; set; }
		public int Version { get; set; }
		private TimeReportDeleted () {}
		public TimeReportDeleted (TimeRecordId timeRecordId)
		{
			Id = timeRecordId;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var target = obj as TimeReportDeleted;
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
		public static bool operator ==(TimeReportDeleted a, TimeReportDeleted b)
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
		public static bool operator !=(TimeReportDeleted a, TimeReportDeleted b)
		{
			return !(a == b);
		}
	}

}