namespace Api
{

    /// <summary>
    /// Identity server options.
    /// </summary>
    public class IdentityServerOptions
    {

        /// <summary>
        /// Gets or sets the authority URL.
        /// </summary>
        public string Authority
        {
            get;

            set;
        }


        /// <summary>
        /// Gets or sets whether HTTPS metadata is required or not.
        /// </summary>
        public bool RequireHttpsMetadata
        {
            get;

            set;
        }


        /// <summary>
        /// Gets or sets the API name.
        /// </summary>
        public string ApiName
        {
            get;

            set;
        }

    }

}
