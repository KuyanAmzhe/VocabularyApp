using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyAPI.Data.Models;

namespace VocabularyAPI.Data
{
    public class VocabularyDBContext : DbContext
    {
        public VocabularyDBContext(
            DbContextOptions<VocabularyDBContext> options)
            : base(options) { }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            //Sets filter for all queries so that doesn't include soft deleted
            //records from WordDictionary table
            modelBuilder.Entity<WordDictionary>()
                .HasQueryFilter(e => !e.SoftDeleted);
        }

        public DbSet<WordDictionary> Vocabulary { get; set; }
        public DbSet<TestResult> TestsResults { get; set; }
    }
}
