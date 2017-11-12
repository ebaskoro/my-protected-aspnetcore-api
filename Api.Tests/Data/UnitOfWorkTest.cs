using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Api.Data;


namespace Api.Tests.Data
{

    public class UnitOfWorkTest : IDisposable
    {

        private readonly SqliteConnection _connection;


        public UnitOfWorkTest()
        {
            var builder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connectionString = builder.ToString();
            _connection = new SqliteConnection(connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<HeroContext>()
                .UseSqlite(_connection)
                .Options;
            Context = new HeroContext(options);
            Context.Database.EnsureCreated();

            Target = new UnitOfWork(Context);
        }


        HeroContext Context
        {
            get;
        }


        UnitOfWork Target
        {
            get;
        }


        [Fact]
        public void Constructor_Initialises_Correctly()
        {
            var actual = Target.HeroRepository;

            Assert.NotNull(actual);
        }


        [Fact]
        public void Save_Throws_NoException()
        {
            Target.Save();
        }


        [Fact]
        public async void SaveAsync_Throws_NoException()
        {
            await Target.SaveAsync();
        }


        public void Dispose()
        {
            _connection.Close();
            Context.Dispose();
        }
        
    }
    
}
