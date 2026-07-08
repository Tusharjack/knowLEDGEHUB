using System.ComponentModel.DataAnnotations;

namespace KnowledgeHub.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Slug { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public Guid? CategoryId { get; set; }

        public string? Tags { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}