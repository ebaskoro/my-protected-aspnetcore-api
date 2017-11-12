using System.ComponentModel.DataAnnotations;


namespace Api.Data
{

    /// <summary>
    /// Hero entity.
    /// </summary>
    public class Hero
    {

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public long Id
        {
            get;

            set;
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name
        {
            get;

            set;
        }

    }

}
