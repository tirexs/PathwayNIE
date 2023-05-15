namespace PathwayNIE.Models.Entities
{
	public class Characteristic
	{
		public int Id { get; set; }
		public double MotivationValue { get; set; }
		public double IntelligenceValue { get; set; }
		public double PsychologyProfile { get; set; }
		public double Archetype { get; set; }
		public double Engagement { get; set; }
		public List<SolvedTask> SolvedTasks { get; set; }
		public List<Score> Scores { get; set; }
	}
}
