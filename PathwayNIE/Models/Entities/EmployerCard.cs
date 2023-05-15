namespace PathwayNIE.Models.Entities
{
	public class EmployerCard
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string Info { get; set; }
		public List<Vacancy> VacanciesList { get; set; }
    }
}
