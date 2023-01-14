using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VocabularyAPI.Data;

namespace VocabularyAPI.Extenstions
{
    public static class DatabaseMigrationHelper
    {
        public static async Task<IHost> SetupDatabaseAsync(
            this IHost webHost, string SeedFilePath)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IWebHostEnvironment>();
                using (var context = services
                    .GetRequiredService<VocabularyDBContext>())
                {
                    try
                    {
                        await context.Database.MigrateAsync();
                        await context.SeedDatabaseAsync(env.ContentRootPath, SeedFilePath);
                    }
                    catch (Exception ex)
                    {
                        var logger = services
                            .GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex,
                            "An error occured while migrating the database");

                        throw;
                    }
                }
            }
            return webHost;
        }

        /// <summary>
        /// Adds initial data in DictionaryDB
        /// </summary>
        public static async Task SeedDatabaseAsync(
            this VocabularyDBContext context,
            string directoryPath,
            string FilePath)
        {
            if (context.Vocabulary.Any()) return;

            var wordsList = DictionaryJSONLoader.LoadDictionary(
                Path.Combine(directoryPath, FilePath));

            context.Vocabulary.AddRange(wordsList);

            await context.SaveChangesAsync();
        }
    }
}
