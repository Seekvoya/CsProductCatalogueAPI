using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound(new { message = $"Товар с ID = {id} не найден." });
        }

        return product;
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoryExists = await _context.ProductCategories.AnyAsync(c => c.Id == product.CategoryId);
        if (!categoryExists)
        {
            return NotFound(new { message = "Указанная категория не найдена." });
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Products/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest(new { message = "ID в пути и теле запроса не совпадают." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoryExists = await _context.ProductCategories.AnyAsync(c => c.Id == product.CategoryId);
        if (!categoryExists)
        {
            return NotFound(new { message = "Указанная категория не найдена." });
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == id))
            {
                return NotFound(new { message = $"Товар с ID = {id} не найден." });
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Products/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Товар с ID = {id} не найден." });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}