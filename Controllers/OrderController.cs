using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



//NUEVO
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class OrderController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public OrderController(DataContext context){
        _context = context;
    }


    [HttpGet]
    public ActionResult<List<OrderItem>> Get() {

        return Ok(_context.Carts);
    }


    [HttpGet]
    [Route("{Id}")]
    public ActionResult<OrderItem> Get(int Id) {
        
        var OrderItem = _context.Orders.Find(Id);
        return OrderItem == null ? NotFound() : Ok(OrderItem);
    }

    [HttpPost]
    public ActionResult Post(OrderItem orderItem){
        var existingOrderItem = _context.Orders.Find(orderItem.id);
        if (existingOrderItem != null) {
            return Conflict("Ya existe una orden con ese id");
        } else {
            _context.Orders.Add(orderItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            var resourceUrl = Request.Path.ToString() + "/" + orderItem.id;
            return Created(resourceUrl, orderItem);
        }
    }

    [HttpPut]
    public ActionResult Put(OrderItem orderItem){
        var existingOrderItem = _context.Orders.Find(orderItem.id);

        if (existingOrderItem == null) {
            return Conflict("No existe el carrito con ese id");
        } else {
            existingOrderItem.quantity = orderItem.quantity;
            existingOrderItem.productID = orderItem.productID;
            existingOrderItem.productID = orderItem.price;
            
            _context.SaveChanges(); //instruccion para guardar cambios

            return Ok();
        }
    }

    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<OrderItem> Delete(int Id) {
    
        var existingOrderItem = _context.Orders.Find(Id);
        if (existingOrderItem == null){
            return NotFound("Elemento orden no encontrado");
        } else{
            _context.Orders.Remove(existingOrderItem);
            _context.SaveChanges(); //instruccion para guardar cambios
            return NoContent();
        }
    }

 }
}