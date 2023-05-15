namespace PathwayNIE.Models.Entities
{
	public class UserLogin
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public int? RoleId { get; set; }
		public Role Role { get; set; }

		public EmployerCard? EmployerCard { get; set; }
		public CV? Cv { get; set; }
		public Characteristic? CharacteristicSet { get; set; }
		public UserLogin() { }
	}
}
