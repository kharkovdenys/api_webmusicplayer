public class MusicDb : DbContext
{
    public MusicDb(DbContextOptions<MusicDb> options) : base(options) { }
    public DbSet<Music> Musics => Set<Music>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>().ToTable("Music").HasKey("Id");
    }
}