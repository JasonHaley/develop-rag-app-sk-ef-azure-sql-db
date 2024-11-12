using Microsoft.EntityFrameworkCore;

namespace PdfChatApp.Models;
public class DocDbContext(DbContextOptions<DocDbContext> options) : DbContext(options)
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<PageChunk> PageChunks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Document_ID");
            entity.ToTable("Document");

            entity.HasMany(p => p.Pages).WithOne().HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Page_ID");
            entity.ToTable("Page");

            entity.HasOne(p => p.Document).WithMany(p => p.Pages).HasForeignKey(p => p.DocumentId);
            entity.HasMany(p => p.PageChunks).WithOne(p => p.Page).HasForeignKey(p => p.PageId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PageChunk>(entity =>
        {
            entity.Property(p => p.Embedding).HasColumnType("vector(384)"); // NOTE: array dimensions need to match the embedding dimensions

            entity.HasKey(e => e.Id).HasName("PK_PageChunk_ID");
            entity.ToTable("PageChunk");

            entity.HasOne(p => p.Page).WithMany(p => p.PageChunks).HasForeignKey(p => p.PageId);
        });
    }
}
