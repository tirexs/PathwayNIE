namespace PathwayNIE.Models.Entities
{
	public class Role
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<UserLogin> User { get; set; }
    }
}
