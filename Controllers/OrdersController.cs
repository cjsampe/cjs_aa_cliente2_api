using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



//NUEVO
using  cjs_aa_cliente2_api.Data;

namespace cjs_aa_cliente2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
 public class ordersController : ControllerBase{




    //NUEVO
    private readonly DataContext _context;



    //NUEVO
    public ordersController(DataContext context){
        _context = context;
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    /// <param name="item"></param>
    /// <returns>All orders</returns>
    /// <response code="200">OK get all orders</response>
    [HttpGet]
    public ActionResult<List<OrdersItem>> Get() {

        return Ok(_context.Orders);
    }


    /// <summary>
    /// Get an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get an order by id</returns>
    /// <response code="200">OK get an order by id</response>
    [HttpGet]
    [Route("{Id}")]
    public ActionResult<OrdersItem> Get(int Id) {
        
        var OrderItem = _context.Orders.Find(Id);
        return OrderItem == null ? NotFound() : Ok(OrderItem);
    }

    /// <summary>
    /// Post an order 
    /// </summary>
    /// <returns>Post an order by Id</returns>
    /// <response code="200">Item POST ok</response>
    [HttpPost]
    public ActionResult Post(OrdersItem orderItem){
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


    /// <summary>
    /// Delete an order by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OK delete a order by Id</returns>
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<OrdersItem> Delete(int Id) {
    
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