namespace Domain.Dtos;

public class NoteDto
{
    public long? Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Color { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; }
    public List<TagDto>? Tags { get; set; }
}