﻿using System;
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
    public class BatchController : ControllerBase
    {
        private readonly ScheduleBrewContext _context;

        public BatchController(ScheduleBrewContext context)
        {
            _context = context;
        }

        // GET: api/Batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatches()
        {
            if (_context.Batches == null)
            {
                return NotFound();
            }
            return await _context.Batches.ToListAsync();
        }
        
        // GET: api/Batch/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Batch>> GetBatch(int id)
        {
            if (_context.Batches == null)
            {
                return NotFound();
            }
            var batch = await _context.Batches.FindAsync(id);

            if (batch == null)
            {
                return NotFound();
            }

            return batch;
        }
        #region Joins

        [HttpGet ("showname/search")]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatchesWithNames(string styleName, string recipeName)
        {
            //not tested
            var query =
            from batch in _context.Batches
            join recipe in _context.Recipes on batch.RecipeId equals recipe.RecipeId
            join style in _context.Styles on recipe.StyleId equals style.StyleId
            where recipe.Name.Contains(recipeName)
            orderby recipe.Name
            select new
            {
                BatchID = batch.BatchId,
                Sched = batch.ScheduledStartDate,
                Rname = recipe.Name,
                SName = style.Name,
                Cat = style.CategoryName

            };

            if (_context.Batches == null)
            {
                return NotFound();
            }
            return await _context.Batches.ToListAsync();
        }


        
        #endregion
        // PUT: api/Batch/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutBatch(int id, Batch batch)
        {
            if (id != batch.BatchId)
            {
                return BadRequest();
            }

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #region search by date
        //get by batch scheduled date range
        [HttpGet("startdate/search")]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatchesByStartDate(DateTime start)
        {
            //start only
            //not filtering correctly
          
            var batches = await _context.Batches.Where(b => b.ScheduledStartDate>=start).ToListAsync();
            if (batches.Count == 0)
            {
                return NotFound();
            }

            return batches;
        }

        [HttpGet("enddate/search")]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatchesByEndDate(DateTime end)
        {
            //end only
            //not filtering correctly

            var batches = await _context.Batches.Where(b => b.ScheduledStartDate <= end).ToListAsync();
            if (batches.Count==0)
            {
                return NotFound();
            }

            return batches;
        }

        [HttpGet("daterange/search")]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatchesByDateRange(DateTime start, DateTime end)
        {
            //end only
            //not filtering correctly

            var batches = await _context.Batches.Where(b => b.ScheduledStartDate <=end && b.ScheduledStartDate >= start).ToListAsync();
            if (batches.Count == 0)
            {
                return NotFound();
            }

            return batches;
        }
        #endregion

        #region Post/Delete

        // POST: api/Batch
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Batch>> PostBatch(Batch batch)
        {
            if (_context.Batches == null)
            {
                return Problem("Entity set 'ScheduleBrewContext.Batches'  is null.");
            }
            _context.Batches.Add(batch);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BatchExists(batch.BatchId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBatch", new { id = batch.BatchId }, batch);
        }

        // DELETE: api/Batch/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            if (_context.Batches == null)
            {
                return NotFound();
            }
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool BatchExists(int id)
        {
            return (_context.Batches?.Any(e => e.BatchId == id)).GetValueOrDefault();
        }
    }
}
