namespace PathwayNIE.Models.Entities
{
	public class CV
	{
        public int Id { get; set; }
        public string FCs { get; set; }
        public string CoreSkills { get; set; }
		public string AboutMe { get; set; }
		public string Contacts { get; set; }
		public string EducationInfo { get; set; }
		public string LanguageSkills { get; set; }
		public List<SolvedTask> SolvedTasks { get; set; }
		public List<Achievement>? AchievementsList { get; set; }
	}
}
