using Microsoft.EntityFrameworkCore;

namespace PathwayNIE.Models.Entities
{
	public class PathwayContext : DbContext
	{
		public PathwayContext(DbContextOptions<PathwayContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// использование Fluent API
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<UserLogin> UserLogins { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<CV> CVs { get; set; }
		public DbSet<Achievement> Achievements { get; set; }
		public DbSet<Vacancy> Vacancies { get; set; }
		public DbSet<SolvedTask> SolvedTasks { get; set; }
		public DbSet<EmployerCard> EmployerCards { get; set; }
		public DbSet<MakedTask> MakedTasks { get; set; }
		public DbSet<CategoryTask> CategoryTasks { get; set; }
        public DbSet<ComplexityTask> ComplexityTasks { get; set; }
    }
}
