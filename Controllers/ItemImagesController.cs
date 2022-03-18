#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;
using InventoryAPI.Data;

using System.Data;
using System.Data.SqlClient;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemImagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;  //kev: to get Photos folder path

        public ItemImagesController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/ItemImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemImage>>> GetItemImage()
        {
            return await _context.ItemImage.ToListAsync();
        }

        // GET: api/ItemImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemImage>> GetItemImage(int id)
        {
            var itemImage = await _context.ItemImage.FindAsync(id);

            if (itemImage == null)
            {
                return NotFound();
            }

            return itemImage;
        }

        // PUT: api/ItemImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemImage(int id, ItemImage itemImage)
        {
            if (id != itemImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemImageExists(id))
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

        // POST: api/ItemImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemImage>> PostItemImage(ItemImage itemImage)
        {
            _context.ItemImage.Add(itemImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemImage", new { id = itemImage.Id }, itemImage);
        }

        // DELETE: api/ItemImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemImage(int id)
        {
            var itemImage = await _context.ItemImage.FindAsync(id);
            if (itemImage == null)
            {
                return NotFound();
            }

            _context.ItemImage.Remove(itemImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemImageExists(int id)
        {
            return _context.ItemImage.Any(e => e.Id == id);
        }

        [Route("GetImageByItemId/{itemId}")]
        [HttpGet]
        public async Task<ActionResult<ItemImage>> GetImageByItemId(int itemId)
        {
            string query = "SELECT * FROM dbo.ItemImage WHERE ItemId = " + itemId.ToString();
            var result = _context.ItemImage.FromSqlRaw(query).FirstOrDefaultAsync().Result;

            if (result == null)
                return NotFound();

            return await GetItemImage(result.Id);
        }

        [Route("AddFile")]
        [HttpPost]
        public JsonResult AddFile()
        {
            try
            {
                var postedFile = Request.Form.Files[0]; //kev: get first image only
                string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(postedFile.FileName);
                string physicalPath = getFilePath(fileName);

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch
            {                
                return new JsonResult("default.jpg");   //kev: give default photo
            }
        }

        [Route("RemoveFile/{fileName}")]        
        [HttpDelete]
        public JsonResult RemoveFile(string fileName)
        {
            try
            {
                FileInfo file = new(getFilePath(fileName));

                file.Delete();
                return new JsonResult("File removed successfully");
            }
            catch
            {
                return new JsonResult("default.jpg");   //kev: give default photo
            }
        }

        private string getFilePath(string fileName)
        {
            return _env.ContentRootPath + "Photos\\" + fileName;
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
