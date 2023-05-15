using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathwayNIE.GPT;
using PathwayNIE.Models;
using PathwayNIE.Models.Entities;
using System.Diagnostics;
using System.Security.Claims;

namespace PathwayNIE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		public PathwayContext context;
		public string GeneratedTaskName = string.Empty;

		public HomeController(PathwayContext db, ILogger<HomeController> logger)
		{
			context = db;
			_logger = logger;
		}

		public IActionResult AddRoles()
		{
			context.Roles.Add(new Role { Name = "admin" });
			context.Roles.Add(new Role { Name = "user" });
			context.Roles.Add(new Role { Name = "employer" });
			context.Roles.Add(new Role { Name = "mentor" });
			context.SaveChanges();
			return View("Index");
		}

		[AllowAnonymous]
		public IActionResult Index()
		{
			ViewBag.User = HttpContext.User.Claims.FirstOrDefault();
			return View();
		}

		public ActionResult MakeNewUser()
		{
			context.UserLogins.Add(new UserLogin
			{
				UserName = "Buba",
				Password = "123"
			});
			context.SaveChanges();
			return View("Index");
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLogin model)
		{
			UserLogin user = await context.UserLogins.Include(x => x.Role).FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);
			if (user != null)
			{
				await Authenticate(user); // аутентификация

				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			return View(model);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Register()
		{
			ViewBag.Roles = context.Roles.Select(x => x.Name).ToList();
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserLogin model, string role)
		{
			UserLogin user = await context.UserLogins.FirstOrDefaultAsync(u => u.UserName == model.UserName);
			ViewBag.Roles = context.Roles.Select(x => x.Name).ToList();
			if (user == null)
			{
				// добавляем пользователя в бд
				var role1 = context.Roles.Where(x => x.Name == role).FirstOrDefault();

				context.UserLogins.Add(new UserLogin { UserName = model.UserName, Password = model.Password, Role = role1 });
				await context.SaveChangesAsync();
				model.Role = role1;
				await Authenticate(model); // аутентификация

				return RedirectToAction("Index", "Home");
			}
			else
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			return View(model);
		}

		private async Task Authenticate(UserLogin user)
		{
			// создаем один claim

			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		[AllowAnonymous]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Home");
		}

		[AllowAnonymous]
		public ActionResult AddAchievement()
		{
			context.Achievements.Add(new Achievement { Name = "Кто я", Content = "Заполнить личный кабинет", IsAchived = true });
			context.Achievements.Add(new Achievement { Name = "К оружию", Content = "Начать выполнять задания", IsAchived = true });
			context.Achievements.Add(new Achievement { Name = "Первые высоты", Content = "Выполнить первое задание", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Звёздный путь", Content = "Получить оценку выполненых заданий", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Хорошая работа", Content = "Получить приглашение на собеседование", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Расширяем кругозор", Content = "Начать изучение курсов", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Преисполняемся в познании", Content = "Закончить курс", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Встать на путь критика", Content = "Оценить выполненное задание другого пользователя", IsAchived = false });
			context.Achievements.Add(new Achievement { Name = "Мы сами пишем свою историю", Content = "Опубликовать задание для других пользователей", IsAchived = true });
			context.SaveChanges();
			return View("Index");
		}

		[AllowAnonymous]
		public ActionResult AddCategory()
		{
			context.CategoryTasks.Add(new CategoryTask { Name = "экология" });
			context.CategoryTasks.Add(new CategoryTask { Name = "астрономия" });
			context.CategoryTasks.Add(new CategoryTask { Name = "маркетинг" });
			context.CategoryTasks.Add(new CategoryTask { Name = "C# разработка" });
			context.CategoryTasks.Add(new CategoryTask { Name = "data science" });
			context.CategoryTasks.Add(new CategoryTask { Name = "разработка нейросети" });
			context.SaveChanges();
			return View("Index");
		}

		[AllowAnonymous]
		public ActionResult AddComplexity()
		{
			context.ComplexityTasks.Add(new ComplexityTask { Name = "легкий" });
			context.ComplexityTasks.Add(new ComplexityTask { Name = "средний" });
			context.ComplexityTasks.Add(new ComplexityTask { Name = "сложный" });
			context.SaveChanges();
			return View("Index");
		}

		[Authorize]
		public ActionResult GenerateTask()
		{
			var aboba = new List<string>
			{
				"веб-разработка",
				"дизайн",
				"экология",
				"маркетинг"
			};

			var abobus = new List<string>
			{
				"легкий",
				"средний",
				"сложный",
			};
			ViewBag.Category = context.CategoryTasks.Select(x => x.Name).ToList();
			ViewBag.DifficultyLevels = context.ComplexityTasks.Select(x => x.Name).ToList();
			return View();
		}

		[Authorize(Roles = "admin, user, mentor, employer")]
		[HttpPost]
		public ActionResult GenerateTask(string message, string select1, string select2)
		{

			var aboba = new List<string>
			{
				"веб-разработка",
				"дизайн",
				"экология",
				"маркетинг"
			};
			var abobus = new List<string>
			{
				"легкий",
				"средний",
				"сложный",
			};

			ViewBag.Category = context.CategoryTasks.Select(x => x.Name).ToList();
			ViewBag.DifficultyLevels = context.ComplexityTasks.Select(x => x.Name).ToList();

			ViewBag.GeneratedTaskName = $"Сгенерированная задача по теме: {select2}";
			string result = $"Сгенерируй задачу уровень: {select1}, по теме {select2}. {message}";
			ViewBag.GeneratedTask = GptResponse.GetResponse(result);

			return View();
		}

		public ActionResult SaveTaskToDb(string description, string GeneratedTaskName)
		{
			var userName = HttpContext.User.Claims.FirstOrDefault().Value;
			context.MakedTasks.Add(
					new MakedTask
					{
						UserLogin = context.UserLogins.Where(x => x.UserName == userName).FirstOrDefault(),
						Name = GeneratedTaskName,
						Description = description
					});
			context.SaveChanges();
			return RedirectToAction("TaskList");		
		}

		public ActionResult TaskList()
		{
			ViewBag.Tasks = context.MakedTasks.Include(x => x.UserLogin).ToList();
			return View("TaskList");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Account()
		{
			var User = context.UserLogins.Include(x => x.Role).Where(x => x.UserName == HttpContext.User.Claims.FirstOrDefault().Value).FirstOrDefault();
			ViewBag.Achievement = context.Achievements.ToList();
			return View(User);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}