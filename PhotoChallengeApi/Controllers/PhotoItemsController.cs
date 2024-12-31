// Ignore Spelling: Api

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using PhotoChallengeAPI.Data;
using PhotoChallengeAPI.Models;
using PhotoChallengeAPI.Helpers;
using PhotoChallengeAPI.ViewModels;

namespace PhotoChallengeAPI.Controllers
{
    [Route("api/photochallengeapi")]
    [ApiController]
    public class PhotoItemsController : ControllerBase
    {
        private readonly PhotoChallengeAPIContext _context;

        public PhotoItemsController(PhotoChallengeAPIContext context)
        {
            _context = context;
        }

        #region GET
        // GET: api/PhotoItems
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<PhotoItem>>> GetPhotoItem()
        {
            return await _context.PhotoItem.ToListAsync();
        }

        // GET: api/PhotoItems
        [HttpGet("Special/All")]
        public async Task<ActionResult<IEnumerable<PhotoSpecialItem>>> GetPhotoSpecialItem()
        {
            return await _context.PhotoSpecialItem.ToListAsync();
        }

        // GET: api/PhotoItems/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PhotoItem>> GetPhotoItem(long id)
        {
            var photoItem = await _context.PhotoItem.FindAsync(id);

            if (photoItem == null)
            {
                return NotFound();
            }

            return photoItem;
        }

        // GET: api/PhotoItems/"hello"
        // returns all the database entries that do not have an approved state
        [HttpGet("Unapproved")]
        public async Task<List<ReturnPhotoByteItem>> GetUnapprovedPhotoItem()
        {

            var allPhotoItems = await _context.PhotoItem.Where(photoItem => photoItem.Approved == null).ToListAsync();

            List<ReturnPhotoByteItem> returnPhotoByteItems = new List<ReturnPhotoByteItem>();

            foreach (var item in allPhotoItems)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                var image = System.IO.File.ReadAllBytes(fullPath);

                ReturnPhotoByteItem returnPhotoByteItem = new ReturnPhotoByteItem(item.Id, item.Name, item.Challenge, image, item.Approved, item.Message);

                returnPhotoByteItems.Add(returnPhotoByteItem);
            }

            return returnPhotoByteItems;

        }


        // GET: api/PhotoItems/"hello"
        // returns all database entries for that name
        [HttpGet("{name}")]
        public async Task<List<ReturnPhotoByteItem>> GetPhotoItem(string name)
        {

            var allPhotoItems = await _context.PhotoItem.Where(photoItem => photoItem.Name == name).ToListAsync();

            List<ReturnPhotoByteItem> returnPhotoByteItems = new List<ReturnPhotoByteItem>();

            // if the entry exists in the database but not in the photos directory
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", name)))
            {
                throw new Exception("Name not found");
            }

            foreach (var item in allPhotoItems)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                var image = System.IO.File.ReadAllBytes(fullPath);

                ReturnPhotoByteItem returnPhotoByteItem = new ReturnPhotoByteItem(item.Id, item.Name, item.Challenge, image, item.Approved, item.Message);
                returnPhotoByteItems.Add(returnPhotoByteItem);
            }

            return returnPhotoByteItems;

        }

        // GET: api/PhotoItems/Special/{challenge}
        // returns all the images for a given challenge
        [HttpGet("Special/Challenge/{challenge}")]
        public async Task<List<ReturnPhotoSpecialByteItem>> GetPhotoSpecialItem(int challenge)
        {

            var allPhotoSpecialItems = await _context.PhotoSpecialItem.Where(photoItem => photoItem.Challenge == challenge).ToListAsync();

            List<ReturnPhotoSpecialByteItem> returnPhotoSpecialByteItems = new List<ReturnPhotoSpecialByteItem>();

            // if the entry exists in the database but not in the photos directory
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", challenge.ToString())))
            {
                throw new Exception("Name not found");
            }

            foreach (var item in allPhotoSpecialItems)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", item.FilePath);

                // if the entry exists in the database but not in the photos directory just leave it
                // (perhaps sub in some not found image here)
                if (!System.IO.File.Exists(fullPath))
                {
                    continue;
                }

                var image = System.IO.File.ReadAllBytes(fullPath);

                ReturnPhotoSpecialByteItem returnPhotoSpecialByteItem = new ReturnPhotoSpecialByteItem(item.Id, item.Name, item.Challenge, image, item.Voters, item.Votes);
                returnPhotoSpecialByteItems.Add(returnPhotoSpecialByteItem);
            }

            return returnPhotoSpecialByteItems;

        }


        // GET: api/PhotoItems/Leaderboard
        // returns the leaderboard for the main challenge
        [HttpGet("Leaderboard")]
        public async Task<List<LeaderBoardItem>> GetLeaderBoard()
        {
            List<LeaderBoardItem> leaderboard = new List<LeaderBoardItem>();
            var result = await _context.PhotoItem.GroupBy(photoItem => photoItem.Name)
                  .Select(g => new
                  {
                      Name = g.Key,
                      TrueCount = g.Count(photoItem => photoItem.Approved == true)
                  })
                  .OrderByDescending(photoItem => photoItem.TrueCount)
                  .ToListAsync();

            for (int i = 0; i < result.Count; i++)
            {
                leaderboard.Add(new LeaderBoardItem(i, result[i].Name, result[i].TrueCount));
            }

            return leaderboard;

        }

        // GET: api/PhotoItems/Leaderboard/Special/{challenge}
        // return the leaderboard for a given challenge
        [HttpGet("Leaderboard/Special/{challenge}")]
        public async Task<List<LeaderBoardSpecialItem>> GetLeaderBoardSpecial(int challenge)
        {
            var photoSpecialItems = await _context.PhotoSpecialItem.Where(photoItem => photoItem.Challenge == challenge)
                                              .OrderByDescending(e => e.Votes)
                                              .ToListAsync();

            var leaderBoardSpecialItems = new List<LeaderBoardSpecialItem>();

            for (int i = 0; i < photoSpecialItems.Count; i++)
            {
                leaderBoardSpecialItems.Add(new LeaderBoardSpecialItem(photoSpecialItems[i].Id, i, photoSpecialItems[i].Name, photoSpecialItems[i].Votes, photoSpecialItems[i].Voters));
            }

            return leaderBoardSpecialItems;

        }

        // GET: api/PhotoItems/Leaderboard/Special/{challenge}
        // return the leaderboard for a given challenge
        [HttpGet("Special/Id/{id}")]
        public async Task<ReturnPhotoSpecialByteItem> GetSpecialImage(long id)
        {
            var item = await _context.PhotoSpecialItem.FindAsync(id);

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", item.FilePath);

            // if the entry exists in the database but not in the photos directory just leave it
            // (perhaps sub in some not found image here)
            if (!System.IO.File.Exists(fullPath))
            {
                throw new Exception("File not found");
            }

            var image = System.IO.File.ReadAllBytes(fullPath);

            ReturnPhotoSpecialByteItem returnPhotoByteItem = new ReturnPhotoSpecialByteItem(item.Id, item.Name, item.Challenge, image, item.Voters, item.Votes);

            return returnPhotoByteItem;

        }


        #endregion

        #region POST

        [HttpPost]
        public bool Authenticate(string authenticate)
        {

            // just a single password hash for now
            if (authenticate == "8e5b57b7b620900e13dd84aae2390dea0eea199aa7f8d34f47b72824276e93f7")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: api/PhotoItems
        [HttpPost("ImageSubmit")]
        public async Task<ActionResult<ReceivePhotoFormItem>> PostPhoto([FromForm] ReceivePhotoFormItem receivePhotoItem)
        {
            if (receivePhotoItem.File.Length > 0 && FormFileExtensions.IsImage(receivePhotoItem.File))
            {

                PhotoItem photoItem = new PhotoItem
                {
                    Name = receivePhotoItem.Name,
                    Challenge = receivePhotoItem.Challenge
                };

                // check if this entry already exists
                var preExist = _context.PhotoItem.Where(photoItem => (photoItem.Name == receivePhotoItem.Name) && (photoItem.Challenge == receivePhotoItem.Challenge)).ToList();
                if (preExist.Any())
                {
                    photoItem = preExist[0];
                    photoItem.Approved = null;
                    photoItem.Message = null;
                    photoItem.FilePath = Path.Combine(photoItem.Name, photoItem.Challenge.ToString()) + Path.GetExtension(receivePhotoItem.File.FileName);
                    Console.WriteLine("This entry already exists");
                    photoItem.Id = preExist[0].Id;
                    _context.Entry(photoItem).State = EntityState.Modified;
                }
                else
                {
                    photoItem.FilePath = Path.Combine(photoItem.Name, photoItem.Challenge.ToString()) + Path.GetExtension(receivePhotoItem.File.FileName);
                    photoItem.Id = receivePhotoItem.Id;
                    _context.PhotoItem.Add(photoItem);
                }

                await _context.SaveChangesAsync();

                bool exists = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", photoItem.Name));

                if (!exists)
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Photos", photoItem.Name));
                }

                Console.WriteLine(photoItem.FilePath);

                // Delete any files with the challenge name that might already exist (ony one image per challenge)

                DirectoryInfo d = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Photos", photoItem.Name));

                FileInfo[] files = d.GetFiles(photoItem.Challenge + ".*"); //Getting all items with this challenge name
                Console.WriteLine(files);

                if (files.Any())
                {
                    foreach (FileInfo singleFile in files)
                    {
                        singleFile.Delete();
                    }
                }

                using (var stream = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "Photos", photoItem.FilePath)))
                {
                    await receivePhotoItem.File.CopyToAsync(stream);
                }

                Response.Headers.Append("Access-Control-Allow-Origin", "*");

                return CreatedAtAction("GetPhotoItem", new { id = receivePhotoItem.Id }, receivePhotoItem);
            }
            else
            {
                return BadRequest("Please send an image.");
            }

        }

        // POST: api/PhotoItems
        [HttpPost("ImageSubmit/Special")]
        public async Task<ActionResult<ReceivePhotoFormItem>> PostPhotoSpecial([FromForm] ReceivePhotoFormItem receivePhotoItem)
        {
            Console.WriteLine("Special photo submitted");
            if (receivePhotoItem.File.Length > 0 && FormFileExtensions.IsImage(receivePhotoItem.File))
            {

                PhotoSpecialItem photoItemSpecial = new PhotoSpecialItem
                {
                    Name = receivePhotoItem.Name,
                    Challenge = receivePhotoItem.Challenge,
                    Voters = [],
                    Votes = 0
                };

                // check if this entry already exists
                var preExist = _context.PhotoSpecialItem.Where(photoItem => (photoItem.Name == receivePhotoItem.Name) && (photoItem.Challenge == receivePhotoItem.Challenge)).ToList();

                if (preExist.Count != 0)
                {
                    photoItemSpecial = preExist[0];
                    photoItemSpecial.Voters = [];
                    photoItemSpecial.Votes = 0;
                    photoItemSpecial.FilePath = Path.Combine(photoItemSpecial.Challenge.ToString(), photoItemSpecial.Name + Path.GetExtension(receivePhotoItem.File.FileName));
                    Console.WriteLine("This entry already exists");
                    photoItemSpecial.Id = preExist[0].Id;
                    _context.PhotoSpecialItem.Entry(photoItemSpecial).State = EntityState.Modified;
                }
                else
                {
                    photoItemSpecial.FilePath = Path.Combine(photoItemSpecial.Challenge.ToString(), photoItemSpecial.Name) + Path.GetExtension(receivePhotoItem.File.FileName);
                    photoItemSpecial.Id = receivePhotoItem.Id;
                    _context.PhotoSpecialItem.Add(photoItemSpecial);
                }

                await _context.SaveChangesAsync();

                // Create a directory of challengenumber/name for each challenge
                bool exists = Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", photoItemSpecial.Challenge.ToString()));

                if (!exists)
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", photoItemSpecial.Challenge.ToString()));
                }

                Console.WriteLine(photoItemSpecial.FilePath);

                // Delete any files with the challenge name that might already exist (ony one image per challenge)
                Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", photoItemSpecial.Challenge.ToString()));
                DirectoryInfo d = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", photoItemSpecial.Challenge.ToString()));

                FileInfo[] files = d.GetFiles(photoItemSpecial.Name + ".*"); //Getting all items with this challenge name

                if (files.Count() != 0)
                {
                    foreach (FileInfo singleFile in files)
                    {
                        singleFile.Delete();
                    }
                }

                using (var stream = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "Photos", "Special", photoItemSpecial.FilePath)))
                {
                    await receivePhotoItem.File.CopyToAsync(stream);
                }

                return CreatedAtAction("GetPhotoItem", new { id = receivePhotoItem.Id }, receivePhotoItem);
            }
            else
            {
                return BadRequest("Please send an image.");
            }

        }

        #endregion

        #region PATCH
        // PUT: api/PhotoItems/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPhotoItem(ApprovalItem photoApproval)
        {
            Console.WriteLine("patching");

            var photoItem = _context.PhotoItem.Where(photoItem => photoItem.Id == photoApproval.Id).ToList()[0];

            photoItem.Approved = photoApproval.Approved;
            photoItem.Message = photoApproval.Message;

            _context.Entry(photoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoItemExists(photoApproval.Id))
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

        #endregion

        #region PUT
        // PUT: api/PhotoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhotoItem(long id, PhotoItem photoItem)
        {
            if (id != photoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(photoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoItemExists(id))
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

        #endregion

        #region DELETE
        // DELETE: api/PhotoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoItem(long id)
        {
            var photoItem = await _context.PhotoItem.FindAsync(id);
            if (photoItem == null)
            {
                return NotFound();
            }

            _context.PhotoItem.Remove(photoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        [Route("/error")]
        protected IActionResult HandleError() =>
            Problem();

        private bool PhotoItemExists(long id)
        {
            return _context.PhotoItem.Any(e => e.Id == id);
        }
    }
}
