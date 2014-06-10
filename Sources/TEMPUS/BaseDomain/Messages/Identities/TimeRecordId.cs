using System;

namespace TEMPUS.BaseDomain.Messages.Identities
{
    [Serializable]
    public class TimeRecordId : GuidIdentity
    {
        public const string Tag = "timeRecordId";

        //need this for serialization only
        private TimeRecordId()
        { }

        public TimeRecordId(Guid id)
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

            var target = obj as TimeRecordId;
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

        public static bool operator ==(TimeRecordId a, TimeRecordId b)
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

        public static bool operator !=(TimeRecordId a, TimeRecordId b)
        {
            return !(a == b);
        }
    }
}
