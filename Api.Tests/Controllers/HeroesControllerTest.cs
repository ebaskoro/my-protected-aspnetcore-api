using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using Xunit;
using Api.Controllers;
using Api.Data;
using Api.Models;


namespace Api.Tests.Controllers
{

    /// <summary>
    /// Unit tests for the Heroes controller.
    /// </summary>
    public class HeroesControllerTest : IDisposable
    {

        /// <summary>
        /// CreateHeroAsyncs the test contexts.
        /// </summary>
        public HeroesControllerTest()
        {
            UnitOfWork = A.Fake<IUnitOfWork>();

            Target = new HeroesController(UnitOfWork);
        }


        /// <summary>
        /// Gets the fake unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork
        {
            get;
        }



        /// <summary>
        /// Gets the target to test.
        /// </summary>
        HeroesController Target
        {
            get;
        }


        [Fact]
        public async void GetAllHeroesAsync_Returns_NotNull()
        {
            var actual = await Target.GetAllHeroesAsync();

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetAllHeroesAsync_Returns_InstanceOf_OkObjectResult()
        {
            var actual = await Target.GetAllHeroesAsync();

            Assert.IsType<OkObjectResult>(actual);
        }


        [Fact]
        public async void GetAllHeroesAsync_Returns_Value_InstanceOf_ListOfHeroModels()
        {
            var actionResult = await Target.GetAllHeroesAsync();
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = okObjectResult.Value;

            Assert.IsType<List<HeroModel>>(actual);
        }


        [Fact]
        public async void GetHeroByIdAsync_Returns_NotNull()
        {
            var actual = await Target.GetHeroByIdAsync(0);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetHeroByIdAsync_When_NonExistentId_Returns_InstanceOf_NotFoundResult()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetByIdAsync(A<long>.Ignored))
                .Returns(Task.FromResult((Hero)null));

            var actual = await Target.GetHeroByIdAsync(999);

            Assert.IsType<NotFoundResult>(actual);
        }


        [Fact]
        public async void GetHeroByIdAsync_When_ExistentId_Returns_InstanceOf_OkObjecResult()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult(new Hero()));

            var actual = await Target.GetHeroByIdAsync(1);

            Assert.IsType<OkObjectResult>(actual);
        }


        [Fact]
        public async void GetHeroByIdAsync_When_ExistentId_Returns_Value_InstanceOf_HeroMode()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult(new Hero()));

            var actionResult = await Target.GetHeroByIdAsync(1);
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = okObjectResult.Value;

            Assert.IsType<HeroModel>(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_Returns_NotNull()
        {
            var actual = await Target.SearchHeroesByNameAsync(null);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_Returns_InstanceOf_OkObjectResult()
        {
            var actual = await Target.SearchHeroesByNameAsync(null);

            Assert.IsType<OkObjectResult>(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_Returns_Value_InstanceOf_ListOfHeroes()
        {
            var actionResult = await Target.SearchHeroesByNameAsync(null);
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = okObjectResult.Value;

            Assert.IsType<List<HeroModel>>(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_NullName_Returns_AllHeroes()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetAllAsync())
                .Returns(Task.FromResult(new List<Hero>
                {
                    new Hero(),
                    new Hero(),
                    new Hero(),
                    new Hero(),
                    new Hero()
                }.AsEnumerable()));

            var actionResult = await Target.SearchHeroesByNameAsync(null);
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Equal(5, actual.Count);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_EmptyName_Returns_AllHeroes()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetAllAsync())
                .Returns(Task.FromResult(new List<Hero>
                {
                    new Hero(),
                    new Hero(),
                    new Hero(),
                    new Hero(),
                    new Hero()
                }.AsEnumerable()));

            var actionResult = await Target.SearchHeroesByNameAsync(string.Empty);
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Equal(5, actual.Count);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_NoMatches_Returns_Empty()
        {
            var actionResult = await Target.SearchHeroesByNameAsync("no match");
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Empty(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_LowercasedName_And_HasMatches_Returns_NonEmpty()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.SearchByNameAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new List<Hero>
                {
                    new Hero()
                }.AsEnumerable()));

            var actionResult = await Target.SearchHeroesByNameAsync("lu");
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Single(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_UppercasedName_And_HasMatches_Returns_NonEmpty()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.SearchByNameAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new List<Hero>
                {
                    new Hero()
                }.AsEnumerable()));

            var actionResult = await Target.SearchHeroesByNameAsync("LU");
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Single(actual);
        }


        [Fact]
        public async void SearchHeroesByNameAsync_When_MixedcasedName_And_HasMatches_Returns_NonEmpty()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.SearchByNameAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new List<Hero>
                {
                    new Hero()
                }.AsEnumerable()));

            var actionResult = await Target.SearchHeroesByNameAsync("Lu");
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = (List<HeroModel>)okObjectResult.Value;

            Assert.Single(actual);
        }


        [Fact]
        public async void CreateHeroAsync_Returns_NotNull()
        {
            var actual = await Target.CreateHeroAsync(null);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void CreateHeroAsync_When_NullHero_Returns_InstanceOf_BadRequestResult()
        {
            var actual = await Target.CreateHeroAsync(null);

            Assert.IsType<BadRequestResult>(actual);
        }


        [Fact]
        public async void CreateHeroAsync_When_NonNullHero_Returns_InstanceOf_CreatedAtRouteResult()
        {
            var actual = await Target.CreateHeroAsync(new HeroModel());
            
            Assert.IsType<CreatedAtRouteResult>(actual);
        }


        [Fact]
        public async void CreateHeroAsync_When_NonNullHero_Returns_Correctly()
        {
            var expectedId = 6L;
            A.CallTo(() => UnitOfWork.HeroRepository.AddAsync(A<Hero>.Ignored))
                .Invokes((Hero hero) => hero.Id = expectedId);

            var actionResult = await Target.CreateHeroAsync(new HeroModel());
            var actual = (CreatedAtRouteResult)actionResult;

            Assert.Equal("GetHeroById", actual.RouteName);
            Assert.Equal(expectedId, actual.RouteValues["id"]);
            Assert.IsType<HeroModel>(actual.Value);
        }


        [Fact]
        public async void CreateHeroAsync_When_NonNullHero_Updates_Database_Correctly()
        {
            await Target.CreateHeroAsync(new HeroModel());

            A.CallTo(() => UnitOfWork.SaveAsync())
                .MustHaveHappened();
        }


        [Fact]
        public async void UpdateHeroAsync_Returns_NotNull()
        {
            var actual = await Target.UpdateHeroAsync(0, null);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void UpdateHeroAsync_When_NullHero_Returns_InstanceOf_BadRequestResult()
        {
            var actual = await Target.UpdateHeroAsync(0, null);

            Assert.IsType<BadRequestResult>(actual);
        }


        [Fact]
        public async void UpdateHeroAsync_When_MismatchedId_Returns_InstanceOf_BadRequestResult()
        {
            var hero = new HeroModel
            {
                Id = 1
            };
            var actual = await Target.UpdateHeroAsync(2, hero);

            Assert.IsType<BadRequestResult>(actual);
        }


        [Fact]
        public async void UpdateHeroAsync_When_NonExistentHero_Returns_InstanceOf_NotFoundResult()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetByIdAsync(A<long>.Ignored))
                .Returns(Task.FromResult((Hero)null));

            var id = 999;
            var hero = new HeroModel
            {
                Id = id
            };
            var actual = await Target.UpdateHeroAsync(id, hero);

            Assert.IsType<NotFoundResult>(actual);
        }


        [Fact]
        public async void UpdateHeroAsync_When_ExistentHero_Returns_InstanceOf_NoContentResult()
        {
            var id = 1;
            var hero = new HeroModel
            {
                Id = id,
                Name = "New Name"
            };
            var actual = await Target.UpdateHeroAsync(id, hero);

            Assert.IsType<NoContentResult>(actual);
        }


        [Fact]
        public async void UpdateHeroAsync_When_ExistentHero_Updates_Database_Correctly()
        {
            var id = 1;
            var expected = "New Name";
            var hero = new HeroModel
            {
                Id = id,
                Name = expected
            };
            await Target.UpdateHeroAsync(id, hero);

            A.CallTo(() => UnitOfWork.SaveAsync())
                .MustHaveHappened();
        }


        [Fact]
        public async void DeleteHeroAsync_Returns_NotNull()
        {
            var actual = await Target.DeleteHeroAsync(0);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void DeleteHeroAsync_When_NonExistentId_Returns_InstanceOf_NotFoundResult()
        {
            A.CallTo(() => UnitOfWork.HeroRepository.GetByIdAsync(A<long>.Ignored))
                .Returns(Task.FromResult((Hero)null));

            var actual = await Target.DeleteHeroAsync(999);

            Assert.IsType<NotFoundResult>(actual);
        }


        [Fact]
        public async void DeleteHeroAsync_When_ExistentId_Returns_InstanceOf_NoContentResult()
        {
            var actual = await Target.DeleteHeroAsync(1);

            Assert.IsType<NoContentResult>(actual);
        }

        
        [Fact]
        public async void DeleteHeroAsync_When_ExistentId_Updates_Database_Correctly()
        {
            await Target.DeleteHeroAsync(1);

            A.CallTo(() => UnitOfWork.SaveAsync())
                .MustHaveHappened();
        }
        

        /// <summary>
        /// Disposes the test context.
        /// </summary>
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

    }

}
