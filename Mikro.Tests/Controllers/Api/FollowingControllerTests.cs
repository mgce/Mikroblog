using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mikro.Core;
using Mikro.Core.Repositories;
using Mikro.Tests.Extensions;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Mikro.Controllers.Api;
using Mikro.Core.Dtos;
using Mikro.Core.Models;
using Moq;

namespace Mikro.Tests.Controllers.Api
{
    [TestClass]
    public class FollowingControllerTests
    {
        private FollowingController _controller;
        private Mock<IFollowingRepository> _mockRepository;
        private Mock<DbSet<Tag>> _mockTag;
        private Mock<ITagRepository> _mockTagRepository;
        private Tag _tags;

        public FollowingControllerTests()
        {
            _mockRepository = new Mock<IFollowingRepository>();
            _mockTagRepository = new Mock<ITagRepository>();

            
            var mockUoW = new Mock<IUnitOfWork>();

            _tags = new Tag() {Id = 1, Name = "Tag"};
            _mockTag = new Mock<DbSet<Tag>>();
            _mockTag.SetSource(new[] {_tags});

            mockUoW.SetupGet(x => x.Followings).Returns(_mockRepository.Object);
            mockUoW.SetupGet(x=>x.Tags).Returns(_mockTagRepository.Object);

            _controller = new FollowingController(mockUoW.Object);
            _controller.MockCurrentUser("1", "user@domain.com");

            
        }

        [TestMethod]
        public void Follow_NoTagNameInDto_NotFound()
        {
            var dto = new FollowingDto
            {
                TagName = null
            };
            var result = _controller.Follow(dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Follow_DtoIsGiven_Ok()
        {
            var dto = new FollowingDto
            {
                TagName = "tag"
            };
            var result = _controller.Follow(dto);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
