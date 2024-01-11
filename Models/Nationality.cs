using System.Text.Json.Serialization;

namespace FeladatAPI.Models;

public class Nationality
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
