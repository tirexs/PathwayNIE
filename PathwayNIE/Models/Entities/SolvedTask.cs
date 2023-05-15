namespace PathwayNIE.Models.Entities
{
	public class SolvedTask
	{
		public int Id { get; set; }
		public UserLogin UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Solution { get; set; }
		public List<TaskAttachment> TaskAttachments { get; set; }
	}
}
