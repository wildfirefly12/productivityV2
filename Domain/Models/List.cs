using System.Text.Json.Serialization;

namespace Domain.Models;

public class List
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long CategoryId { get; set; }
    public ListCategory ListCategory { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedDate { get; set; }
    [JsonIgnore] public ICollection<ListItem> Items { get; set; }
    [JsonIgnore] public ICollection<Tag> Tags { get; set; }
}