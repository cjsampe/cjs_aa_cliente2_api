using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



//NUEVO
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class ProductController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public ProductController(DataContext context){
        _context = context;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <param name="item"></param>
    /// <returns>All products</returns>
    /// <response code="200">ok, Get all products</response>    
    [HttpGet]
    public ActionResult<IEnumerable<ProductItem>> Get() {
        IEnumerable<ProductItem> products = _context.Products
            .Include(product => product.images)
            .ToList();
        return Ok(_context.Products);
    }

    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a product by id</returns>
    /// <response code="200">OK get a product by id</response>  
    [HttpGet]
    [Route("{id}")]
    public ActionResult<ProductItem> Get(int id) {
        
        var productItem = _context.Products.Find(id);
        IEnumerable<ProductItem> products = _context.Products
            .Include(product => product.images)
            .ToList();
        return productItem == null ? NotFound() : Ok(productItem);
    }


    /// <summary>
    /// Post a product 
    /// </summary>
    /// <returns>Post an product by Id</returns>
    /// <response code="200">Item POST ok</response>
    [HttpPost]
    public ActionResult Post(ProductItem productItem){
        var existingProductItem = _context.Products.Find(productItem.id);

        //aquí no se pone un ternario xq se va a poner más código
        if (existingProductItem != null) {
            //si existe mesaje 
            return Conflict("Ya existe el producto con ese id");
        } else {
            //si no existe se añade a nuestro array estática, sino seria a nuestra base de datos
            _context.Products.Add(productItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            //buena practica devolver la url del que de ha creado
            var resourceUrl = Request.Path.ToString() + "/" + productItem.id;
            return Created(resourceUrl, productItem);
        }
    }

    /// <summary>
    /// PUT a product by Id
    /// </summary>
    /// <returns>Put a product by Id</returns>
    /// <response code="200">Item PUT ok</response>
    [HttpPut]
    public ActionResult Put(ProductItem productItem){
    
        var existingProductItem = _context.Products.Find(productItem.id);

        if (existingProductItem == null) {
            return Conflict("No existe el producto con ese id");
        } else {
            existingProductItem.name = productItem.name;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }


    /// <summary>
    /// Delete a product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OK delete a product by Id</returns>
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<ProductItem> Delete(int id) {
    
        var existingProductItem = _context.Products.Find(id);
        if (existingProductItem == null){
            return NotFound("Heroe no encontrado");
        } else{
            _context.Products.Remove(existingProductItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            return NoContent();
        }
    }

 }
}