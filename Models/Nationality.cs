namespace FeladatAPI.Models;

public class Nationality
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
