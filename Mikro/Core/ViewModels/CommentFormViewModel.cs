﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mikro.Core.Models;

namespace Mikro.Core.ViewModels
{
    public class CommentFormViewModel
    {
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string UserName { get; set; }
        public PostPlus PostPlus { get; set; }
        public ICollection<CommentPlus> CommentPlus { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Post Post { get; set; }
    }
}