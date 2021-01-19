using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReviews.API.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MovieReview { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Required]
        public string ImdbId { get; set; }

        [ForeignKey(nameof(ImdbId))]
        public Movie movie { get; set; }
    }
}
