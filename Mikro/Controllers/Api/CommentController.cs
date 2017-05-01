﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Mikro.Core;
using Mikro.Core.Dtos;
using Mikro.Core.Models;
using Mikro.Persistance;
using Mikro.Persistance.Extension;

namespace Mikro.Controllers.Api
{
    public class CommentController : ApiController
    {
        private readonly IUnitOfWork uow;
        public CommentController(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public CommentController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        [HttpPost]
        public IHttpActionResult AddComent(CommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = uow.Posts.GetPost(dto.PostId);

            IEnumerable<string> tags = Regex.Split(dto.Content, @"\s+").Where(i => i.StartsWith("#"));
            var output = TagFunction.TagToUrl(dto.Content, tags);
            var comment = new Comment
            {
                UserId = User.Identity.GetUserId(),
                PostId = dto.PostId,
                UserName = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                PostedContent = output,
                Content = dto.Content,
                Post = post
            };

            uow.Comments.Add(comment);
            uow.SaveChanges();
            return Ok(comment);
        }
    }
}
