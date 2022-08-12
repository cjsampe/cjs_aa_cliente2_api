using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



//NUEVO
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class CartController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public CartController(DataContext context){
        _context = context;
    }


    [HttpGet]
    public ActionResult<List<CartItem>> Get() {

        return Ok(_context.Carts);
    }

    [HttpGet]
    [Route("{Id}")]
    public ActionResult<CartItem> Get(int Id) {
        
        var CartItem = _context.Carts.Find(Id);
        return CartItem == null ? NotFound() : Ok(CartItem);
    }

    [HttpPost]
    public ActionResult Post(CartItem cartItem){
        var existingCartItem = _context.Carts.Find(cartItem.id);

        //aquí no se pone un ternario xq se va a poner más código
        if (existingCartItem != null) {
            //si existe mesaje 
            return Conflict("Ya existe el producto con ese id");
        } else {
            //si no existe se añade a nuestro array estática, sino seria a nuestra base de datos
            _context.Carts.Add(cartItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            //buena practica devolver la url del que de ha creado
            var resourceUrl = Request.Path.ToString() + "/" + cartItem.id;
            return Created(resourceUrl, cartItem);
        }
    }

    [HttpPut]
    //en este caso meto yo el ID pero no es lo normal
    public ActionResult Put(CartItem cartItem){
        // ANTERIOR     var existingHeroItem = Heroes.Find(x => x.Id == heroItem.Id);


        var existingCartItem = _context.Carts.Find(cartItem.id);

        if (existingCartItem == null) {
            return Conflict("No existe el producto con ese id");
        } else {
            existingCartItem.quantity = cartItem.quantity;
            existingCartItem.productID = cartItem.productID;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }

    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<ProductItem> Delete(int Id) {
    
        var existingCartItem = _context.Carts.Find(Id);
        if (existingCartItem == null){
            return NotFound("Elemento carrito no encontrado");
        } else{
            _context.Carts.Remove(existingCartItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            return NoContent();
        }
    }

 }
}