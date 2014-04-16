using System;

namespace TEMPUS.DB.Models
{
    /// <summary>
    /// The class represents base class for all of the entities.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }
    }
}