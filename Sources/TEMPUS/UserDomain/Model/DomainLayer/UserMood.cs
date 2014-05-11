using System;

namespace TEMPUS.UserDomain.Model.DomainLayer
{
    /// <summary>
    /// The class represents the user mood.
    /// </summary>
    public class UserMood
    {
        /// <summary>
        /// Gets or sets the date when user set his mood.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the rate of the last user mood.
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMood"/> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="rate">The rate.</param>
        public UserMood(DateTime date, int rate)
        {
            Date = date;
            Rate = rate;
        }
    }
}