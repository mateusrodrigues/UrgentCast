using System;
using System.ComponentModel.DataAnnotations;

namespace UrgentCast.Models.FeedsViewModels
{
    public class AddFeedViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        public string Link { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool Explicit { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        [Required]
        public string Category { get; set; }
    }
}