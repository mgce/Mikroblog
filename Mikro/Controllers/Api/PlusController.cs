using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Mikro.Core;
using Mikro.Core.Models;

namespace Mikro.Controllers.Api
{
    public class PlusController : ApiController
    {
        private readonly IUnitOfWork uow;

        public PlusController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public int GetPostCounter(int id)
        {
            var post = uow.Posts.GetPost(id);
            if(post != null)
                return post.PlusCounter;
            return 0;
        }

        public int GetCommentCounter(int id)
        {
            var comment = uow.Comments.GetCommentById(id);
            if (comment != null)
                return comment.PlusCounter;
            return 0;
        }

        public IHttpActionResult Post(int id)
        {
            var userId = User.Identity.GetUserId();
            var post = uow.Posts.GetPost(id);

            var postPlus = uow.PostPluses.GetPostPlusByPostIdAndUserId(id, userId);

            if (postPlus != null)
            {
                post.PlusCounter -= 1;
                uow.PostPluses.Delete(postPlus);
                uow.SaveChanges();
                return Ok();
            }
            var plus = new PostPlus
            {
                PostId = id,
                UserId = userId
            };
            post.PlusCounter += 1;
            uow.PostPluses.Add(plus);
            uow.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Comment(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = uow.Comments.GetCommentById(id);

            var commentPlus = uow.CommentPluses.GetCommentPlus(userId, id);

            if (commentPlus != null)
            {
                comment.PlusCounter -= 1;
                uow.CommentPluses.Delete(commentPlus);
                uow.SaveChanges();
                return Ok();
            }
            var plus = new CommentPlus()
            {
                CommentId = id,
                UserId = userId
            };
            comment.PlusCounter += 1;
            uow.CommentPluses.Add(plus);
            uow.SaveChanges();
            return Ok();
        }

    }
}
