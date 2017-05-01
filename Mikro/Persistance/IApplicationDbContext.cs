using System.Data.Entity;
using Mikro.Core.Models;

namespace Mikro.Persistance
{
    public interface IApplicationDbContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<PostPlus> PostPluses { get; set; }
        DbSet<CommentPlus> CommentPluses { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<PostTag> PostTag { get; set; }
        DbSet<Following> Followings { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}