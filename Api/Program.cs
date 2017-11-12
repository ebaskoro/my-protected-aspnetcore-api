using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Api
{

    /// <summary>
    /// Main entry point.
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Executes the application.
        /// </summary>
        public static void Main(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
                
    }

}
