using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using Microsoft.Extensions.Logging;
namespace MyWebApi.Controllers 
{
    [Route ("api/[controller]")]
    [ApiController]
    public class NotesController : Controller {

        IEFCoreDatabaseContext access = null;
        ILogger logger;
        public NotesController (IEFCoreDatabaseContext _access,ILogger<NotesController> _logger) 
        {
            this.logger=_logger;
            this.access = _access;
        }

        // GET api/Notes
        [HttpGet]
        public IActionResult Get () 
        {
            try 
            {
                var data = access.GetData ();
                if (data.Count()!=0) 
                {
                    return Ok (data);
                } 
                else 
                {
                    return NoContent ();
                }
            } catch (Exception e) {
                return Conflict (e.Message);
            }
        }

        // GET api/Notes/title
        [HttpGet ("{id}")] 
        public IActionResult Get (string id) 
        {
            try 
            {
                var data = access.GetDataById (id);
                if (data != null) {
                    return Ok (data);
                } 
                else 
                {
                    return NoContent ();
                }
            } catch (Exception e) 
            {
                return Conflict (e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id,[FromQuery] string type)
        {
            try
            {
                var notes=access.GetDataByType(id,type);
                if(notes!=null)
                {
                    return Ok(notes);
                }
                else
                {
                    return NotFound("No content found for this  information");
                }
            }catch(Exception e)
            {
                return Conflict(e.InnerException.Message);
            }     
        }

        // POST api/Notes
        [HttpPost]
        public IActionResult Post ([FromBody] Note note) {
            try {
                var status = access.AddData(note);
                if (status == 1) {
                    return Created ("/api/Notes", note);
                } else {
                    return BadRequest ("your request cannot be proccessed");
                }
            } catch (Exception e) {
                return Conflict (e.Message);
            }
        }

        // PUT api/Notes/5
        [HttpPut ("{id}")]
        public IActionResult Put (string id, [FromBody] Note updated) 
        {
            return BadRequest ();
        }

        // DELETE api/Notess/5
        [HttpDelete ("{title}")]
        public IActionResult Delete (String title) 
        {
            return Ok ();
        }
    }
}