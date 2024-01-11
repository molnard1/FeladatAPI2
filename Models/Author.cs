namespace FeladatAPI.Models;

public class Author
{
    public Guid AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Gender { get; set; }

    public Guid NationalityId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Nationality Nationality { get; set; } = null!;
}
