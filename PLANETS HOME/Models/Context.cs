
using Microsoft.EntityFrameworkCore;
    namespace PLANETS_HOME.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique indexes on Users

            modelBuilder.Entity<User>()
                .HasIndex(u => u.username).IsUnique();

            // ── Seed: admin account ──────────────────────────────
            modelBuilder.Entity<User>().HasData(new User
            {
                id = 1,
                username = "admin",
                email = "admin@planetshome.com",
                password = "admin123",
                role = "admin",
                created = new DateTime(2025, 1, 1)
            });

            // ── Seed: sample plants ──────────────────────────────
            modelBuilder.Entity<Plant>().HasData(
                new Plant { id = 1, name = "Monstera", category = "Tropical", light = "Bright indirect", water = "Once a week", humidity = "High (60%+)", description = "A tropical plant loved for its large, glossy, split leaves. A statement piece for any room.", caretips = "Wipe leaves monthly. Avoid direct sunlight which burns leaves." },
                new Plant { id = 2, name = "Snake Plant", category = "Succulent", light = "Any light", water = "Every 2-3 weeks", humidity = "Low to medium", description = "One of the most tolerant houseplants. Perfect for beginners. Purifies indoor air.", caretips = "Let soil dry completely between waterings. Overwatering is the main risk." },
                new Plant { id = 3, name = "Peace Lily", category = "Flowering", light = "Low to medium", water = "Once a week", humidity = "High", description = "A graceful flowering plant that thrives in low light. White blooms appear in spring.", caretips = "Drooping leaves signal it needs water. It recovers quickly after watering." },
                new Plant { id = 4, name = "Cactus", category = "Succulent", light = "Full sun", water = "Every 2-4 weeks", humidity = "Low", description = "Drought-tolerant succulents that store water in their stems. Great for sunny windowsills.", caretips = "Use cactus soil. Never let water pool in the saucer." },
                new Plant { id = 5, name = "Pothos", category = "Tropical", light = "Low to medium", water = "Every 1-2 weeks", humidity = "Medium", description = "A fast-growing trailing vine with heart-shaped leaves. Almost impossible to kill.", caretips = "Can grow in water alone. Trim long vines to encourage bushiness." },
                new Plant { id = 6, name = "Orchid", category = "Flowering", light = "Bright indirect", water = "Every 7-10 days", humidity = "Medium to high", description = "Elegant flowering plants with blooms that last for months. A popular gift plant.", caretips = "Soak roots 15 minutes then drain. Never leave in standing water." },
                new Plant { id = 7, name = "Rubber Plant", category = "Tropical", light = "Bright indirect", water = "Every 1-2 weeks", humidity = "Medium", description = "A bold plant with large, glossy, dark-green leaves. Grows tall indoors.", caretips = "Wipe leaves with a damp cloth. Repot every 2 years." },
                new Plant { id = 8, name = "Aloe Vera", category = "Succulent", light = "Full sun", water = "Every 3 weeks", humidity = "Low", description = "A practical succulent. The gel inside its leaves soothes burns and skin irritation.", caretips = "Use a terracotta pot with drainage holes. Direct sun is ideal." }
            );



        }
    }


}
