﻿using LocalizationManager.DAL.Entities;
using LocalizationManager.DAL.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalizationManager.DAL.Context;

public class LocalizationDbContext(DbContextOptions<LocalizationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Language> Languages { get; set; }
    public DbSet<LocalizationValue> LocalizationValues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        LanguageSeed.SeedLanguages(builder);
    }
}
