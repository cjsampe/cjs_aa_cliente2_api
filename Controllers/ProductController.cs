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


    [HttpGet]
    public ActionResult<List<ProductItem>> Get() {

        return Ok(_context.Products);
    }

    [HttpGet]
    [Route("{Id}")]
    public ActionResult<ProductItem> Get(int Id) {
        
        var heroItem = _context.Products.Find(Id);
        return heroItem == null ? NotFound() : Ok(heroItem);
    }

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

    [HttpPut]
    //en este caso meto yo el ID pero no es lo normal
    public ActionResult Put(ProductItem productItem){
        // ANTERIOR     var existingHeroItem = Heroes.Find(x => x.Id == heroItem.Id);


        var existingProductItem = _context.Products.Find(productItem.id);

        if (existingProductItem == null) {
            return Conflict("No existe el producto con ese id");
        } else {
            existingProductItem.name = productItem.name;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }

    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<ProductItem> Delete(int Id) {
    
        var existingProductItem = _context.Products.Find(Id);
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