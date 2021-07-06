using Microsoft.VisualStudio.TestTools.UnitTesting;
using api_rest.Services;
using api_rest.Util;
using api_rest.Domain.Models;
using Moq;
using api_rest.Domain.Repositories;
using api_rest.Domain;
using System.Threading.Tasks;
using api_rest.Persistence.Repositories;
using api_rest.Persistence.Context;
using System;

namespace api_rest.Test
{
    [TestClass]
    public class UnitTest1
    {
        private UserService userService;

        [TestMethod]
        public async Task TestAddUserAsync()
        {
            userService = new UserService(new Mock<IUserRepository>().Object, new Mock<IUnitOfWork>().Object);
            
            
            User user = new User();
            user.Password = "teste";
            user.Username = "teste";
            user.Name = "teste";
            user.TypeUser = 1;


            var addUser = await userService.SaveAsync(user);

            Assert.IsTrue(addUser.Success);
            
        }

    }
}
