using System.Text.Json.Serialization;

namespace Domain.Models;

public class Tag {
    public long Id { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    [JsonIgnore] public ICollection<TaskItem> Tasks { get; set; }
    [JsonIgnore] public ICollection<Note> Notes { get; set; }
    [JsonIgnore] public ICollection<ItemList> Lists { get; set; }
}