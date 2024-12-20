// Ignore Spelling: Api

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using XmasAPI.Data;
using XmasAPI.Models;
using XmasAPI.Helpers;
using XmasAPI.ViewModels;

namespace XmasAPI.Controllers
{
    [Route("api/xmasapi")]
    [ApiController]
    public class XmasItemsController : ControllerBase
    {
        private readonly XmasAPIContext _context;

        public XmasItemsController(XmasAPIContext context)
        {
            _context = context;
        }

        // GET: api/XmasItems
        [HttpGet("All")]
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
        // returns all the database entries that do not have an approved state
        [HttpGet("Unapproved")]
        public async Task<List<XmasItemByte>> GetUnapprovedXmasItem()
        {

            var allXmasItems = _context.XmasItem.Where(xmasItem => xmasItem.Approved == null).ToList();

            List<XmasItemByte> XmasItemBytes = new List<XmasItemByte>();

            foreach (var item in allXmasItems)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                var image = System.IO.File.ReadAllBytes(fullPath);

                XmasItemByte XmasItemByte = new XmasItemByte(item.Id, item.Name, item.Challenge, image);

                XmasItemBytes.Add(XmasItemByte);
            }

            return XmasItemBytes;

        }


        // GET: api/XmasItems/"hello"
        // returns all databse entries for that name
        [HttpGet("{name}")]
        public async Task<List<XmasItemByte>> GetXmasItem(string name)
        {

            var allXmasItems = _context.XmasItem.Where(xmasItem => xmasItem.Name == name).ToList();

            List<XmasItemByte> XmasItemBytes = new List<XmasItemByte>();

            // if the entry exists in the database but not in the photos directory
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", name)))
            {
                throw new Exception("Name not found");
            }

            foreach (var item in allXmasItems)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                var image = System.IO.File.ReadAllBytes(fullPath);

                XmasItemByte XmasItemByte = new XmasItemByte(item.Id, item.Name, item.Challenge, image);
                XmasItemBytes.Add(XmasItemByte);
            }

            return XmasItemBytes;

        }


        [HttpPost]
        public async Task<bool> Authenticate(string authenticate)
        {

            // just a single password hash for now
            if(authenticate == "8e5b57b7b620900e13dd84aae2390dea0eea199aa7f8d34f47b72824276e93f7")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: api/XmasItems
        [HttpPost("ImageSubmit")]
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

                Response.Headers.Append("Access-Control-Allow-Origin", "*");

                return CreatedAtAction("GetXmasItem", new { id = xmasItemVM.Id }, xmasItemVM);
            }
            else
            {
                return BadRequest("Please send an image.");
            }

        }



        // PUT: api/XmasItems/5
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


        [Route("/error")]
        protected IActionResult HandleError() =>
            Problem();

        private bool XmasItemExists(long id)
        {
            return _context.XmasItem.Any(e => e.Id == id);
        }
    }
}
