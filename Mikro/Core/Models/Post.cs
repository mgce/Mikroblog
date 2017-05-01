﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mikro.Core.Models
{
    public class Post
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime PostedOn { get; set; }
        public int PlusCounter { get; set; }
        public DateTime? Modifed { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostedContent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; }


    }
}