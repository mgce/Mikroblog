using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface ICommentPlusRepository
    {
        ICollection<CommentPlus> GetCommentPlusesByUser(string userId);
        CommentPlus GetCommentPlus(string userId, int commentId);
        void Add(CommentPlus commentPlus);
        void Delete(CommentPlus commentPlus);
    }
}