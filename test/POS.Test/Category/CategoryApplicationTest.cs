using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Category.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext _testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValueOrEntity_ValidationError()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //Arrange preparamos un solicitud
            var name = "";
            var description = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_VALIDATE;

            //Act actuar
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            }) ;
            var current = result.Message;

            //Assert se valida la respuesta final como aprodo o no aprobado.
            Assert.AreEqual(expected, current);

        }
    
        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccessfully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //Arrange preparamos un solicitud
            var name = "TEST PROJECT";
            var description = "TEST PROJECT";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_SAVE;

            //Act actuar
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });
            var current = result.Message;

            //Assert se valida la respuesta final como aprodo o no aprobado.
            Assert.AreEqual(expected, current);
        }

    }
}
