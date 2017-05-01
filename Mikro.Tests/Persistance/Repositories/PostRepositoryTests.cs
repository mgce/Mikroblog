using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mikro.Core.Models;
using Mikro.Persistance;
using Mikro.Persistance.Repository;
using Mikro.Tests.Extensions;
using Moq;

namespace Mikro.Tests.Persistance.Repositories
{
    [TestClass]
    public class PostRepositoryTests
    {
        private PostRepository _repository;
        private Mock<DbSet<Post>> _mockPosts;
        private Post _post;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPosts = new Mock<DbSet<Post>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(x => x.Posts).Returns(_mockPosts.Object);

            _repository = new PostRepository(mockContext.Object);

            _post = new Post() { Id = 1, Username = "Adam" };
            _mockPosts.SetSource(new[] { _post });
        }

        [TestMethod]
        public void GetAllPostsByUsername_NoUsernameIsGiven_ShouldNotBeReturned()
        {
            var posts = _repository.GetAllPostsByUsername(null);
            posts.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAllPostsByUsername_UsernameIsGiven_ShouldReturn()
        {
            string username = "Adam";
            var posts = _repository.GetAllPostsByUsername(username);
            posts.Should().Contain(_post);
        }

        [TestMethod]
        public void GetPostsById_FalseIdIsGiven_ShouldReturn()
        {
            var posts = _repository.GetPostsById(22);
            posts.Should().NotContain(_post);
        }

        [TestMethod]
        public void GetPostsById_IdIsGiven_ShouldReturn()
        {
            var posts = _repository.GetPostsById(1);
            posts.Should().Contain(_post);
        }
    }
}
