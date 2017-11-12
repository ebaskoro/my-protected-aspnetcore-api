using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Api.Data;


namespace Api.Tests.Data
{

    public class HeroRepositoryTest : IDisposable
    {

        private readonly SqliteConnection _connection;


        public HeroRepositoryTest()
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

            Target = new HeroRepository(Context);
        }


        HeroContext Context
        {
            get;
        }


        HeroRepository Target
        {
            get;
        }


        [Fact]
        public void GetAll_Returns_NotNull()
        {
            var actual = Target.GetAll();

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetAllAsync_Returns_NotNull()
        {
            var actual = await Target.GetAllAsync();

            Assert.NotNull(actual);
        }


        [Fact]
        public void GetById_When_NotFound_Returns_Null()
        {
            var actual = Target.GetById(999);

            Assert.Null(actual);
        }


        [Fact]
        public void GetById_When_Found_Returns_NotNull()
        {
            var id = 1;
            Context.Heroes.Add(new Hero
            {
                Id = id,
                Name = "My Hero"
            });
            Context.SaveChanges();

            var actual = Target.GetById(id);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetByIdAsync_When_NotFound_Returns_Null()
        {
            var actual = await Target.GetByIdAsync(999);

            Assert.Null(actual);
        }


        [Fact]
        public async void GetByIdAsync_When_Found_Returns_NotNull()
        {
            var id = 1;
            Context.Heroes.Add(new Hero
            {
                Id = id,
                Name = "My Hero"
            });
            Context.SaveChanges();

            var actual = await Target.GetByIdAsync(id);

            Assert.NotNull(actual);
        }


        [Fact]
        public void SearchByName_When_NullNameToMatch_Throws_Exception()
        {
            Assert.Throws<NullReferenceException>(() => Target.SearchByName(null));
        }


        [Fact]
        public void SearchByName_When_NoMatches_Returns_Empty()
        {
            var actual = Target.SearchByName("no match");

            Assert.Empty(actual);
        }


        [Fact]
        public void SearchByName_When_LowercasedNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            Context.SaveChanges();

            var actual = Target.SearchByName("lu");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public void SearchByName_When_UppercasedNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            Context.SaveChanges();

            var actual = Target.SearchByName("LU");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public void SearchByName_When_MixedcaseNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            Context.SaveChanges();

            var actual = Target.SearchByName("lU");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public async void SearchByNameAsync_When_NullNameToMatch_Throws_Exception()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () => await Target.SearchByNameAsync(null));
        }


        [Fact]
        public async void SearchByNameAsync_When_NoMatches_Returns_Empty()
        {
            var actual = await Target.SearchByNameAsync("no match");

            Assert.Empty(actual);
        }


        [Fact]
        public async void SearchByNameAsync_When_LowercasedNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            await Context.SaveChangesAsync();

            var actual = await Target.SearchByNameAsync("lu");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public async void SearchByNameAsync_When_UppercasedNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            await Context.SaveChangesAsync();

            var actual = await Target.SearchByNameAsync("LU");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public async void SearchByNameAsync_When_MixedcaseNameToMatch_And_HasMatches_Returns_NonEmpty()
        {
            Context.Heroes.Add(new Hero
            {
                Name = "Luke Cage"
            });
            await Context.SaveChangesAsync();

            var actual = await Target.SearchByNameAsync("lU");

            Assert.NotEmpty(actual);
        }


        [Fact]
        public void Add_Throws_NoException()
        {
            var hero = new Hero
            {
                Name = "My Hero"
            };

            Target.Add(hero);
        }


        [Fact]
        public async void AddAsync_Throws_NoException()
        {
            var hero = new Hero
            {
                Name = "My Hero"
            };

            await Target.AddAsync(hero);
        }


        [Fact]
        public void Remove_Throws_NoException()
        {
            var hero = new Hero
            {
                Id = 1,
                Name = "My Hero"
            };

            Target.Remove(hero);
        }


        [Fact]
        public void RemoveById_Throws_NoException()
        {
            Target.RemoveById(999);
        }


        [Fact]
        public void RemoveById_When_ExistentId_Updates_Database_Correctly()
        {
            var id = 1;
            Context.Heroes.Add(new Hero
            {
                Id = id,
                Name = "My Hero"
            });
            Context.SaveChanges();

            Target.RemoveById(id);
            Context.SaveChanges();

            Assert.Empty(Context.Heroes);
        }


        public void Dispose()
        {
            _connection.Close();
            Context.Dispose();
        }

    }

}
