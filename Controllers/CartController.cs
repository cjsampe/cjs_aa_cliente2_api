using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class cartController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public cartController(DataContext context){
        _context = context;
    }

    /// <summary>
    /// Get all carts
    /// </summary>
    /// <param name="item"></param>
    /// <returns>All carts</returns>
    /// <response code="200">OK get all carts</response>
    [HttpGet]
    public ActionResult<List<CartItem>> Get() {

        return Ok(_context.Cart);
    }

    /// <summary>
    /// Get a cart by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a cart by id</returns>
    /// <response code="200">OK get a cart by id</response>  
    [HttpGet]
    [Route("{Id}")]
    public ActionResult<CartItem> Get(int Id) {
        
        var CartItem = _context.Cart.Find(Id);
        return CartItem == null ? NotFound() : Ok(CartItem);
    }


    /// <summary>
    /// Post an cart 
    /// </summary>
    /// <returns>Post a cart by Id</returns>
    /// <response code="200">Item POST ok</response>
    [HttpPost]
    public ActionResult Post(CartItem cartItem){
        CartItem existingCartItem = _context.Cart.Find(cartItem.id);
        if (existingCartItem != null) {
            return Conflict("Ya existe el carrito con ese id");
        } else {
            _context.Cart.Add(cartItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            var resourceUrl = Request.Path.ToString() + "/" + cartItem.id;
            return Created(resourceUrl, cartItem);
        }
    }


    /// <summary>
    /// PUT a cart by Id
    /// </summary>
    /// <returns>Put a cart by Id</returns>
    /// <response code="200">Item PUT ok</response>
    [HttpPut]
    public ActionResult Put(CartItem cartItem){
        var existingCartItem = _context.Cart.Find(cartItem.id);

        if (existingCartItem == null) {
            return Conflict("No existe el carrito con ese id");
        } else {
            existingCartItem.quantity = cartItem.quantity;
            existingCartItem.productId = cartItem.productId;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }

    /// <summary>
    /// Delete a cart by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OK delete a cart by Id</returns>
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<CartItem> Delete(int Id) {
    
        var existingCartItem = _context.Cart.Find(Id);
        if (existingCartItem == null){
            return NotFound("Elemento carrito no encontrado");
        } else{
            _context.Cart.Remove(existingCartItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            return NoContent();
        }
    }

 }
}