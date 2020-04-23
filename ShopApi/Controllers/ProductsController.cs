using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApi.Models;

namespace ShopApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ShopDBContext _ShopDBContext;
    public ProductsController(ShopDBContext dbContext)
    {
      _ShopDBContext = dbContext;
    }

    [HttpGet("All")]
    public async Task<List<Product>> GetAll()
    {
      return await _ShopDBContext.Product.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Product> GetProductAsync(int id)
    {
      return await _ShopDBContext.Product.FindAsync(id);
    }

    [HttpPost]
    public async Task CreateAsync(Product p)
    {
      try
      {
        p.CreationDate = DateTime.Now;
        _ShopDBContext.Product.Add(p);
        await _ShopDBContext.SaveChangesAsync();
      }
      catch (Exception)
      {

        throw;
      }
    }

    [HttpPut]
    public async Task UpdateAsync(Product p)
    {
      try
      {
        var product = await _ShopDBContext.Product.FindAsync(p.Id);
        product.Name = p.Name;
        product.Price = p.Price;
        product.Stock = p.Stock;
        product.UpdateDate = DateTime.Now;

        await _ShopDBContext.SaveChangesAsync();
      }
      catch (Exception)
      {

        throw;
      }
    }

  }
}
