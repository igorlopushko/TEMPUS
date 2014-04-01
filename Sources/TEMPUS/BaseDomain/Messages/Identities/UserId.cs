using System;

namespace TEMPUS.BaseDomain.Messages.Identities
{
    [Serializable]
    public class UserId : GuidIdentity
    {
        public const string Tag = "user";

        //need this for serialization only
        private UserId()
        { }

        public UserId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id", "id required, was empty guid");
            }

            Id = id;
        }

        public override Guid Id
        {
            get;
            protected set;
        }

        public override string GetTag()
        {
            return Tag;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var target = obj as UserId;
            if (target == null)
            {
                return false;
            }

            return base.Equals(target);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(UserId a, UserId b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(UserId a, UserId b)
        {
            return !(a == b);
        }
    }
}