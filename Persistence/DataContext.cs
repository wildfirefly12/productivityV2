using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<User> {
        //public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }
        public DbSet<ListCategory> ListCategories { get; set; }
        public DbSet<ItemList> Lists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NoteCategory>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .IsRequired();

            builder.Entity<Note>()
                .HasOne(n => n.NoteCategory)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId)
                .IsRequired();

            builder.Entity<ItemList>()
                .HasOne(l => l.ListCategory)
                .WithMany(c => c.Lists)
                .HasForeignKey(l => l.CategoryId)
                .IsRequired();

            builder.Entity<ItemList>()
                .HasOne(l => l.User)
                .WithMany(u => u.Lists)
                .HasForeignKey(l => l.UserId)
                .IsRequired();

            builder.Entity<ListItem>()
                .HasOne(l => l.ItemList)
                .WithMany(u => u.Items)
                .HasForeignKey(l => l.ListId)
                .IsRequired();

            builder.Entity<TaskItem>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Tasks)
                .UsingEntity(
                    "TaskTag",
                    l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                    r => r.HasOne(typeof(Task)).WithMany().HasForeignKey("TasksId").HasPrincipalKey(nameof(Task.Id)),
                    j => j.HasKey("TasksId", "TagsId"));

            builder.Entity<ItemList>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Lists)
                .UsingEntity(
                    "ListTag",
                    l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                    r => r.HasOne(typeof(ItemList)).WithMany().HasForeignKey("ListsId").HasPrincipalKey(nameof(ItemList.Id)),
                    j => j.HasKey("ListsId", "TagsId"));

            builder.Entity<Note>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Notes)
                .UsingEntity(
                    "NotedTag",
                    l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                    r => r.HasOne(typeof(Note)).WithMany().HasForeignKey("NotesId").HasPrincipalKey(nameof(Note.Id)),
                    j => j.HasKey("NotesId", "TagsId"));

        }
}