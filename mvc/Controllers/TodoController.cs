using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.DTOs.Todo;
using mvc.Helpers.interfaces;

namespace mvc.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoHelper _todoHelper;

        public TodoController(ITodoHelper todoHelper)
        {
            _todoHelper = todoHelper;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // GET: /Todo
        public async Task<IActionResult> Index()
        {
            var todos = await _todoHelper.GetAllByUser(GetUserId());
            return View(todos);
        }

        // GET: /Todo/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var created = await _todoHelper.Create(dto, GetUserId());

            if (!created)
            {
                ModelState.AddModelError("", "Failed to create todo");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Todo/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _todoHelper.GetById(id, GetUserId());

            if (todo == null)
                return NotFound();

            var dto = new TodoDto
            {
                Title = todo.Title,
                Description = todo.Description
            };

            ViewBag.TodoId = todo.Id;
            return View(dto);
        }

        // POST: /Todo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TodoId = id;
                return View(dto);
            }

            var updated = await _todoHelper.Update(id, dto, GetUserId());

            if (!updated)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Todo/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            await _todoHelper.ToggleComplete(id, GetUserId());
            return RedirectToAction(nameof(Index));
        }

        // POST: /Todo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoHelper.Delete(id, GetUserId());
            return RedirectToAction(nameof(Index));
        }
    }
}
