﻿using System.Collections.Generic;
using System.Linq;
using Mikro.Core.Models;
using Mikro.Core.Repositories;

namespace Mikro.Persistance.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IApplicationDbContext _context;
        public PostRepository(IApplicationDbContext context)
        {
            _context = context;

        }

        public IList<Post> GetAllPosts()
        {
            return _context.Posts
                .OrderByDescending(x => x.PostedOn)
                .ToList();
        }

        public IList<Post> GetAllPostsByUsername(string username)
        {
            return _context.Posts
                .Where(x=>x.Username == username)
                .OrderByDescending(x => x.PostedOn)
                .ToList();
        }

        public Post GetPost(int postId)
        {
            return _context.Posts
                .SingleOrDefault(x => x.Id == postId);
        }

        public IList<Post> GetPostsById(int postId)
        {
            return _context.Posts.Where(x => x.Id == postId).ToList();
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
        }

    }
}