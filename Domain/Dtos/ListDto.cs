namespace Domain.Dtos;

public class ListDto
{
    public long? Id { get; set; }
    public string Title { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; }
}