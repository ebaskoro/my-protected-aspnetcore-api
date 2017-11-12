using Api;
using Xunit;


namespace Api.Tests
{

    public class DatabaseOptionsTest
    {

        [Fact]
        public void Constructor_Initialises_Correctly()
        {
            var target = new DatabaseOptions();

            Assert.Equal("InMemory", target.Provider);
            Assert.Equal("Heroes", target.ConnectionString);
        }

    }
    
}
