[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Получить все категории
    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _categoryService.GetAllCategories();
        return Ok(categories);
    }

    // Получить категорию по ID
    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    // Создать новую категорию
    [HttpPost]
    public IActionResult CreateCategory([FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _categoryService.AddCategory(category);
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
    }

    // Удалить категорию
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var deleted = _categoryService.DeleteCategory(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}