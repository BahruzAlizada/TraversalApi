using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraversalApi.DAL;
using TraversalApi.Entities;
using TraversalApi.ToDoItems;

namespace TraversalApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {

        #region VisitorList
        [HttpGet("VisitorList")]      
        public async Task<IActionResult> VisitorList()
        {
            using(var context=new Context())
            {
                List<Visitor> visitors = await context.Visitors.ToListAsync();
                return Ok(visitors);
            }
        }
        #endregion

        #region VisitorGetById
        [HttpGet("{id}")]
        public async Task<IActionResult> VisitorGetById(int? id)
        {
            using (var context = new Context())
            {
                if (id == null)
                    return StatusCode(StatusCodes.Status404NotFound, new Respone { Status = "Error", Message = "Id can not be null" });
                Visitor visitor = await context.Visitors.FirstOrDefaultAsync(x => x.Id == id);
                if (visitor == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new Respone { Status = "Error", Message="Id is not entered coorectly" });

                return Ok(visitor);
            }
        }
        #endregion

        #region VisitorAdd
        [HttpPost]
        [Route("VisitorAdd")]
        public async Task<IActionResult> VisitorAdd(Visitor visitor)
        {
            using (var context=new Context())
            {
                await context.AddAsync(visitor);
                await context.SaveChangesAsync();
                return Ok();
            }
        }
        #endregion

        #region VisitorUpdate
        [HttpPut("{id}")]
        public async Task<IActionResult> VisitorUpdate(int? id,Visitor visitor)
        {
            using (var context=new Context())
            {
                if (id == null)
                    return StatusCode(StatusCodes.Status404NotFound, new Respone { Status = "Error", Message = "Id can not be null" });
                Visitor dbvisitor = await context.Visitors.FirstOrDefaultAsync(x => x.Id == id);
                if (dbvisitor == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new Respone { Status = "Error", Message = "Id is not entered correctly"});

                dbvisitor.Name=visitor.Name;
                dbvisitor.Surname=visitor.Surname;
                dbvisitor.Country=visitor.Country;
                dbvisitor.City = visitor.City;
                dbvisitor.Mail = visitor.Mail;

               
                await context.SaveChangesAsync();
                return Ok(visitor);
            }
        }
        #endregion

        #region VisitorDelete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            using(var context=new Context())
            {
                if (id == null)
                    return StatusCode(StatusCodes.Status404NotFound,new Respone { Status="Error",Message="Id can not be null"});
                Visitor visitor = await context.Visitors.FirstOrDefaultAsync(x => x.Id == id);
                if (visitor == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new Respone { Status = "Error", Message = "Id is not entered correctly" });

                context.Visitors.Remove(visitor);
                await context.SaveChangesAsync();
                return Ok();
            }
        }
        #endregion
    }
}
