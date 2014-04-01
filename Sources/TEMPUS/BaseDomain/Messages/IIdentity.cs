namespace TEMPUS.BaseDomain.Messages
{
    public interface IIdentity
    {
        /// <summary>
        /// Gets the id, converted to a string. Only alphanumerics and '-' are allowed.
        /// </summary>
        /// <returns></returns>
        string GetId();

        /// <summary>
        /// Unique tag (should be unique within the assembly) to distinguish
        /// between different identities, while deserializing.
        /// </summary>
        string GetTag();
    }

    public interface ITransportableIdentity : IIdentity
    {
        string ToTransportableString();

        void FromTransportableString(string transportableIdentityString);
    }
}