using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Countries.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var countries = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        if (countries == null)
        {
            return NotFound();
        }

        _context.Remove(countries);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Country countries)
    {
        if (id != countries.Id)
        {
            return BadRequest("El ID de la URL no coincide con el del objeto");
        }

        var existing = await _context.Countries.FindAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Name = countries.Name;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }
}