namespace PathwayNIE.Models.Entities
{
	public class Score
	{
		public int Id { get; set; }
		public UserLogin UserId { get; set; }
		public SolvedTask TaskId { get; set; }
	}
}
