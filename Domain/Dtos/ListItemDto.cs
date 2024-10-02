namespace Domain.Dtos;

public class ListItemDto
{
    public long? Id { get; set; }
    public string Description { get; set; } = String.Empty;
    public bool? IsChecked { get; set; }
    public long ListId { get; set; }
}