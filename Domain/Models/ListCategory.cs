namespace Domain.Models;

public class ListCategory {
    public long Id { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<ItemList> Lists { get; set; }
}