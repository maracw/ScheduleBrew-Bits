using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleBrewClasses.Models;

namespace schedbrew_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly ScheduleBrewContext _context;

        public StyleController(ScheduleBrewContext context)
        {
            _context = context;
        }

        // GET: api/Style
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Style>>> GetStyles()
        {
            if (_context.Styles == null)
            {
                return NotFound();
            }
            return await _context.Styles.ToListAsync();
        }
        
        // GET: api/Style/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Style>> GetStyle(int id)
        {
            if (_context.Styles == null)
            {
                return NotFound();
            }
            var style = await _context.Styles.FindAsync(id);

            if (style == null)
            {
                return NotFound();
            }

            return style;
        }
        //get by style name
        [HttpGet("name/search")]
        public async Task<ActionResult<IEnumerable<Style>>> GetStyleByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return await _context.Styles.ToListAsync();
            }
            else
            {
                var styles = await _context.Styles.Where(s => s.Name.Contains(name)).ToListAsync();
                return styles;
            }

        }

        //get by style category
        [HttpGet("category/search")]
        public async Task<ActionResult<IEnumerable<Style>>> GetStyleByCat(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                return await _context.Styles.ToListAsync();
            }
            else
            {
                var styles = await _context.Styles.Where(s => s.CategoryName.Contains(category)).ToListAsync();
                return styles;
            }

        }

      
        /*Users will not make new styles, delete or update styles - styles are used for searching batches only*/
   
       
        private bool StyleExists(int id)
        {
            return (_context.Styles?.Any(e => e.StyleId == id)).GetValueOrDefault();
        }
    }
}
