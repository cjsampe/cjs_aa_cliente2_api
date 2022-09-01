using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



//NUEVO
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class productsController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public productsController(DataContext context){
        _context = context;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <param name="item"></param>
    /// <returns>All products</returns>
    /// <response code="200">ok, Get all products</response>    
    [HttpGet]
    public ActionResult<IEnumerable<ProductsItem>> Get() {
        IEnumerable<ProductsItem> products = _context.Products
            .Include(products => products.images)
            .ToList();
        return Ok(products);
    }    


    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a product by id</returns>
    /// <response code="200">OK get a product by id</response>  
    [HttpGet]
    [Route("{id}")]
    public ActionResult<ProductsItem> Get(int id) {
        
        var productItem = _context.Products.Find(id);
        IEnumerable<ProductsItem> products = _context.Products
            .Include(products => products.images)
            .ToList();
        return productItem == null ? NotFound() : Ok(productItem);
    }


    /// <summary>
    /// Post a product 
    /// </summary>
    /// <returns>Post an product by Id</returns>
    /// <response code="200">Item POST ok</response>
    [HttpPost]
    public ActionResult Post(ProductsItem productItem){
        var existingProductItem = _context.Products.Find(productItem.id);

            _context.Products.Add(productItem);
            _context.SaveChanges(); //instruccion para guardar cambios

            var resourceUrl = Request.Path.ToString() + "/" + productItem.id;
            return Created(resourceUrl, productItem);
        }


    /// <summary>
    /// PUT a product by Id
    /// </summary>
    /// <returns>Put a product by Id</returns>
    /// <response code="200">Item PUT ok</response>
    [HttpPut]
    public ActionResult Put(ProductsItem productItem){
    
        var existingProductItem = _context.Products.Find(productItem.id);

        if (existingProductItem == null) {
            return Conflict("No existe el producto con ese id");
        } else {
            existingProductItem.name = productItem.name;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }
}}
