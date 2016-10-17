using System;
using System.ComponentModel.DataAnnotations;

namespace UrgentCast.Models.EpisodesViewModels
{
    public class AddEpisodeViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public bool Explicit { get; set; }
    }
}