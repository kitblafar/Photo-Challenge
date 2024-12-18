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
using XmasApi.NewFolder;
using XmasApi.ViewModels;

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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<XmasItem>> GetXmasItem(long id)
        {
            var xmasItem = await _context.XmasItem.FindAsync(id);

            if (xmasItem == null)
            {
                return NotFound();
            }

            return xmasItem;
        }


        // GET: api/XmasItems/"hello"
        [HttpGet("{name}")]
        public async Task<List<XmasItem>> GetXmasItem(string name)
        {
            var allXmasItems = _context.XmasItem.Where(xmasItem => xmasItem.Name == name).ToList();

            return allXmasItems;

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
        public async Task<ActionResult<XmasItemVM>> PostXmasItem([FromForm] XmasItemVM xmasItemVM)
        {
            if (xmasItemVM.File.Length > 0 && FormFileExtensions.IsImage(xmasItemVM.File))
            {
                XmasItem xmasItem = new XmasItem();

                xmasItem.Id = xmasItemVM.Id;
                xmasItem.Name = xmasItemVM.Name;
                xmasItem.Challenge = xmasItemVM.Challenge;
                xmasItem.FilePath = Path.Combine(xmasItem.Name, xmasItem.Challenge.ToString()) + Path.GetExtension(xmasItemVM.File.FileName);

                _context.XmasItem.Add(xmasItem);

                await _context.SaveChangesAsync();

                bool exists = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", xmasItem.Name));

                if (!exists)
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Photos", xmasItem.Name));


                Console.WriteLine(xmasItem.FilePath);


                using (var stream = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "Photos", xmasItem.FilePath)))
                {
                    await xmasItemVM.File.CopyToAsync(stream);
                }

                Response.Headers.Add("Access-Control-Allow-Origin", "*");

                return CreatedAtAction("GetXmasItem", new { id = xmasItemVM.Id }, xmasItemVM);
            }
            else
            {
                return BadRequest("Please send an image.");
            }

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
