using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using XmasApi.Data;
using XmasApi.Models;
using XmasApi.NewFolder;
using XmasApi.ViewModels;
using static System.Net.Mime.MediaTypeNames;

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

        [Route("/error")]
        protected IActionResult HandleError() =>
            Problem();


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
                XmasItemByte XmasItemByte = new XmasItemByte();
                XmasItemByte.Id = item.Id;
                XmasItemByte.Name = item.Name;
                XmasItemByte.Challenge = item.Challenge;

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                XmasItemByte.Image = System.IO.File.ReadAllBytes(fullPath);

                XmasItemBytes.Add(XmasItemByte);
            }

            return XmasItemBytes;

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

                Response.Headers.Append("Access-Control-Allow-Origin", "*");

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
