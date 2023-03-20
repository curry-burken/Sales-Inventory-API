using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDetails.Data;
using SalesDetails.Models;

namespace SalesDetails.Controllers
{
    [ApiController]
    [Route("api/Sales")]
    public class SalesController : Controller
    {
        private readonly DbAttribute tempdbattribute;
        public SalesController(DbAttribute tempdbattribute)
        {
            this.tempdbattribute = tempdbattribute;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDetails()
        {
            return Ok(await tempdbattribute.Item.ToListAsync());
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id)
        {
            var tempitem = await tempdbattribute.Item.FindAsync(id);
            if(tempitem != null) 
            {
                return Ok(tempitem);
            }
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewDetail(AddDetails adddetails)
        {
            var tempdetails = new Details()
            {
                Id = Guid.NewGuid(),
                Prodname = adddetails.Prodname,
                Price = adddetails.Price,
                Quantity = adddetails.Quantity,
                Buyer = adddetails.Buyer
            };
            await tempdbattribute.Item.AddAsync(tempdetails);
            await tempdbattribute.SaveChangesAsync();
            return Ok("Item created");
        }
        [HttpPut]
        [Route ("{id:Guid}")]
        public async Task<IActionResult> ChangeDetail([FromRoute] Guid id, UpdateDetails updatedetails)
        {
            var tempdetails = await tempdbattribute.Item.FindAsync(id);
            if(tempdetails != null) 
            {
                tempdetails.Prodname = updatedetails.Prodname;
                tempdetails.Price = updatedetails.Price;
                tempdetails.Quantity = updatedetails.Quantity;
                tempdetails.Buyer = updatedetails.Buyer;
                await tempdbattribute.SaveChangesAsync();
                return Ok("Item updated");
            }
            return NotFound();
        }
        [HttpDelete]
        [Route ("{id:Guid}")]
        public async Task<IActionResult> DeleteDetail([FromRoute] Guid id)
        {
            var tempdetails = await tempdbattribute.Item.FindAsync(id);
            if(tempdetails != null) 
            {
                tempdbattribute.Remove(tempdetails);
                await tempdbattribute.SaveChangesAsync();
                return Ok("Item deleted");
            }
            return NotFound();
        }
    }
}
