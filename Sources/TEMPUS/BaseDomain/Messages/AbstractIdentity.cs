using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TEMPUS.BaseDomain.Messages
{
    public static class AbstractIdentityTransport
    {
        public static T FromTransportableString<T>(string transportableString)
            where T : ITransportableIdentity
        {
            var id = (T)Activator.CreateInstance(typeof(T), true);
            id.FromTransportableString(transportableString);
            return id;
        }
    }

    /// <summary>
    /// Base implementation of <see cref="IIdentity"/>, which implements
    /// equality and ToString once and for all.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    [Serializable]
    public abstract class AbstractIdentity<TKey> : ITransportableIdentity, IEquatable<AbstractIdentity<TKey>>,
                                                   IXmlSerializable
    {
        public abstract TKey Id { get; protected set; }

        public string GetId()
        {
            return Id.ToString();
        }

        public abstract string GetTag();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            var identity = obj as AbstractIdentity<TKey>;

            if (identity != null)
            {
                return identity.Id.Equals(Id) && string.Equals(identity.GetTag(), GetTag());
            }

            return false;
        }

        public override string ToString()
        {
            return ToTransportableString();
        }

        public virtual string ToTransportableString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", GetTag(), ToTransportableId());
        }

        protected virtual string ToTransportableId()
        {
            return GetId();
        }

        public virtual void FromTransportableString(string transportableString)
        {
            if (String.IsNullOrEmpty(transportableString))
            {
                throw new ArgumentNullException("transportableString");
            }

            var args = transportableString.Split(new[] { '-' }, 2);
            var tag = args[0];
            var id = args[1];

            if (!GetTag().Equals(tag, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                                                  "Invalid id tag. Expected '{0}' but was '{1}'", GetTag(), tag));
            }

            FromTransportableId(id);
        }

        protected abstract void FromTransportableId(string id);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (GetTag().GetHashCode());
            }
        }

        public bool Equals(AbstractIdentity<TKey> other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            var id = reader.GetAttribute("id");
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new SerializationException("Failed to deserialize identity. Id attribute is missing");
            }

            FromTransportableString(id);
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("id", ToTransportableString());
        }
    }

    [Serializable]
    public abstract class StringIdentity : AbstractIdentity<string>
    {
        protected override void FromTransportableId(string id)
        {
            Id = id;
        }
    }

    [Serializable]
    public abstract class GuidIdentity : AbstractIdentity<Guid>
    {
        protected override void FromTransportableId(string id)
        {
            Id = new Guid(id);
        }
    }
}