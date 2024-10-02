namespace Domain.Models;

public class NoteCategory {
    public long Id { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<Note> Notes { get; set; }
}