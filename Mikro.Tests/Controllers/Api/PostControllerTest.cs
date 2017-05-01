using System;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mikro.Controllers;
using Mikro.Controllers.Api;
using Mikro.Core;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Mikro.Core.Dtos;
using Mikro.Core.Models;
using Mikro.Core.Repositories;
using Mikro.Tests.Extensions;

namespace Mikro.Tests.Controllers.Api
{
    [TestClass]
    public class PostControllerTest
    {
        private PostsController _controller;
        private Mock<IPostRepository> _mockRepository;

        public PostControllerTest()
        {
            _mockRepository = new Mock<IPostRepository>();

            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.SetupGet(x => x.Posts).Returns(_mockRepository.Object);
            _controller = new PostsController(mockUoW.Object);
            _controller.MockCurrentUser("1", "user@domain.com");
        }

        [TestMethod]
        public void Add_NoContenInDto_ShouldReturnNotFound()
        {
            var dto = new AddingPostDto
            {
                Content = null
            };
            var result = _controller.Post(dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Post_DtoExist_ShouldReturnOk()
        {
            var dto = new AddingPostDto
            {
                Content = "test"
            };
            var result = _controller.Post(dto);
            var posRes = result as OkNegotiatedContentResult<Post>;
            Assert.IsNotNull(posRes);
        } 
    }
}
