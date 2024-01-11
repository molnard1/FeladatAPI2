namespace FeladatAPI.Models;

public class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; } = null!;

    public int PageCount { get; set; }

    public int PublicationYear { get; set; }

    public Guid AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;
}
