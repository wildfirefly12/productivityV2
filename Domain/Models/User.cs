using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser {
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastLoggedDate { get; set; }
    //public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    [JsonIgnore] public ICollection<TaskItem> Tasks { get; set; }
    [JsonIgnore] public ICollection<Tag> Tags { get; set; }
    [JsonIgnore] public ICollection<Note> Notes { get; set; }
    [JsonIgnore] public ICollection<NoteCategory> Categories { get; set; }
    [JsonIgnore] public ICollection<List> Lists { get; set; }
}