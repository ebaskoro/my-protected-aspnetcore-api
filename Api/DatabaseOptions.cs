namespace Api
{

    /// <summary>
    /// Database options.
    /// </summary>
    public class DatabaseOptions
    {

        /// <summary>
        /// Creates database options with default values.
        /// </summary>
        public DatabaseOptions()
        {
            Provider = "InMemory";
            ConnectionString = "Heroes";
        }


        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider
        {
            get;

            set;
        }


        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get;

            set;
        }

    }
    
}
