using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XmasApi.Data;
using XmasApi.Models;

namespace XmasApi.Controllers
{
    [Route("api/xmasapi")]
    [ApiController]
    public class XmasItemsController : ControllerBase
    {
        private readonly XmasApiContext _context;

        public XmasItemsController(XmasApiContext context)
        {
            _context = context;
        }

        // GET: api/XmasItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<XmasItem>>> GetXmasItem()
        {
            return await _context.XmasItem.ToListAsync();
        }

        // GET: api/XmasItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<XmasItem>> GetXmasItem(long id)
        {
            var xmasItem = await _context.XmasItem.FindAsync(id);

            if (xmasItem == null)
            {
                return NotFound();
            }

            return xmasItem;
        }


        // PUT: api/XmasItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutXmasItem(long id, XmasItem xmasItem)
        {
            if (id != xmasItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(xmasItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XmasItemExists(id))
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
   
        // POST: api/XmasItems
        [HttpPost]
        public async Task<ActionResult<XmasItem>> PostXmasItem([FromForm] XmasItem xmasItem)
        {

            _context.XmasItem.Add(xmasItem);

            await _context.SaveChangesAsync();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", xmasItem.Name);

            bool exists = Directory.Exists(filePath);

            Console.WriteLine("FILE");

            Console.WriteLine(filePath);

            if (!exists)
                Directory.CreateDirectory(filePath);
            
            var itemFilePath = Path.Combine(filePath,xmasItem.Challenge.ToString())+Path.GetExtension(xmasItem.File.FileName);

            Console.WriteLine(itemFilePath);

            if (xmasItem.File.Length > 0)
            {
                using (var stream = System.IO.File.Create(itemFilePath))
                {
                    await xmasItem.File.CopyToAsync(stream);
                }
            }

            return CreatedAtAction("GetXmasItem", new { id = xmasItem.Id }, xmasItem);
        }

        // DELETE: api/XmasItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteXmasItem(long id)
        {
            var xmasItem = await _context.XmasItem.FindAsync(id);
            if (xmasItem == null)
            {
                return NotFound();
            }

            _context.XmasItem.Remove(xmasItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool XmasItemExists(long id)
        {
            return _context.XmasItem.Any(e => e.Id == id);
        }
    }
}
