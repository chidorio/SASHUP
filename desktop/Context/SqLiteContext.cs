using System;
using Microsoft.EntityFrameworkCore;

namespace desktop;

public class SqLiteContext:DbContext
{
    public DbSet<RefreshToken> RefreshTokens {get;set;}
    public SqLiteContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("FileName=storage.db");
    }
}
