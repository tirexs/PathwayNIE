namespace PathwayNIE.Models.Entities
{
	public class MakedTask 
	{
		public int Id { get; set; }
		public UserLogin UserLogin { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<TaskAttachment>? TaskAttachments { get; set; } 
	}
}
