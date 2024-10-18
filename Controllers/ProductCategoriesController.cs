using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/ProductCategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
    {
        return await _context.ProductCategories.ToListAsync();
    }

    // GET: api/ProductCategories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
    {
        var category = await _context.ProductCategories.FindAsync(id);

        if (category == null)
        {
            return NotFound(new { message = $"Категория с ID = {id} не найдена." });
        }

        return category;
    }

    // POST: api/ProductCategories
    [HttpPost]
    public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.ProductCategories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductCategory), new { id = category.Id }, category);
    }

    // PUT: api/ProductCategories/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductCategory(int id, ProductCategory category)
    {
        if (id != category.Id)
        {
            return BadRequest(new { message = "ID в пути и теле запроса не совпадают." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProductCategories.Any(e => e.Id == id))
            {
                return NotFound(new { message = $"Категория с ID = {id} не найдена." });
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/ProductCategories/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCategory(int id)
    {
        var category = await _context.ProductCategories.FindAsync(id);
        if (category == null)
        {
            return NotFound(new { message = $"Категория с ID = {id} не найдена." });
        }

        _context.ProductCategories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
