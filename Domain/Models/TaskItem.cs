using Domain.Enums;

namespace Domain.Models;

public class TaskItem {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public bool IsRecurring { get; set; }
    public bool IsComplete { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}