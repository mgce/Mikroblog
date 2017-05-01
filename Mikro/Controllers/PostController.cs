using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Mikro.Core;
using Mikro.Core.Models;
using Mikro.Core.ViewModels;
using Mikro.Persistance.Extension;

namespace Mikro.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork uow;

        public PostController(IUnitOfWork _uow)
        {
            uow = _uow;
        }       

        public ActionResult Index()
        {
            ViewBag.actualUserId = User.Identity.GetUserId();

            var viewModel = new PostFormViewModel
            {
                Posts = uow.Posts.GetAllPosts()
            };

            return View(viewModel);
        }

        [Route("/Edit/{id:int}")]
        public ActionResult EditPost(int id)
        {
            var post = uow.Posts.GetPost(id);

            if (post == null | post.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var editPost = uow.Posts.GetPost(post.Id);
                        
            IEnumerable<string> tags = Regex.Split(post.Content, @"\s+").Where(i => i.StartsWith("#"));
            var output = TagFunction.TagToUrl(post.Content, tags);

            editPost.Content = post.Content;
            editPost.PostedContent = output;
            uow.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Route("/{id:int}")]
        public ActionResult Post(int id)
        {
            ViewBag.actualUserId = User.Identity.GetUserId();
            var userId = User.Identity.GetUserId();
            var viewModel = new CommentFormViewModel
            {
                Post = uow.Posts.GetPost(id),
                Comments = uow.Comments.GetCommentsByPostId(id),
                CommentPlus = uow.CommentPluses.GetCommentPlusesByUser(userId),
                PostPlus = uow.PostPluses.GetPostPlusByPostIdAndUserId(id, userId)
            };

            if (viewModel.Post == null)
                return HttpNotFound();

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {        
            var post = uow.Posts.GetPost(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            uow.Posts.Delete(post);
            uow.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}