
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
	public sealed class CreateUser : ICommand<UserId>
	{
		public UserId Id { get; set; }
		public int Version { get; set; }
		private CreateUser () {}
		public CreateUser (UserId userId)
		{
			Id = userId;
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
			return Id.Equals(target.Id);
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
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

}