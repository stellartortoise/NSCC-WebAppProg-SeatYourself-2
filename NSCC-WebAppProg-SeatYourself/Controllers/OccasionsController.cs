using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSCC_WebAppProg_SeatYourself.Data;
using NSCC_WebAppProg_SeatYourself.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NSCC_WebAppProg_SeatYourself.Controllers
{
    [Authorize]
    public class OccasionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly NSCC_WebAppProg_SeatYourselfContext _context;
        private readonly BlobContainerClient _containerClient;

        public OccasionsController(IConfiguration configuration, NSCC_WebAppProg_SeatYourselfContext context)
        {
            _context = context;
            _configuration = configuration;

            var connectionString = _configuration["AzureStorage"];
            var containerName = "seat-yourself-uploads";
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        // GET: Occasions
        public async Task<IActionResult> Index()
        {
            var nSCC_WebAppProg_SeatYourselfContext = _context.Occasion.Include(o => o.Category).Include(o => o.Venue);
            return View(await nSCC_WebAppProg_SeatYourselfContext.ToListAsync());
        }

        // GET: Occasions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion
                .Include(o => o.Category)
                .Include(o => o.Venue)
                .FirstOrDefaultAsync(m => m.OccasionId == id);
            if (occasion == null)
            {
                return NotFound();
            }

            return View(occasion);
        }

        // GET: Occasions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name");
            return View();
        }

        // POST: Occasions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OccasionId,Title,Description,Date,Time,Owner,VenueId,CategoryId,ImageFile")] Occasion occasion)
        {
            occasion.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                //
                //Step 1: Image upload code would go here
                //
                if (occasion.ImageFile != null && occasion.ImageFile.Length > 0)
                {

                    // Create a unique filename using a GUID and the original file extension
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(occasion.ImageFile.FileName); //ex. guid: 234234-23423-4234234-23423.jpg    

                    // Initialize the filename in the record
                    occasion.Filename = filename;

                    // Get the file path to save the file (C:\WebAppProg\NSCC-WebAppProg-SeatYourself\NSCC-WebAppProg-SeatYourself\wwwroot\Images\Uploads)
                    string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Uploads", filename);

                    //Save file
                    using(FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                    {
                        await occasion.ImageFile.CopyToAsync(fileStream);
                    }

                    //Upload to Azure Blob Storage
                    IFormFile fileUpload = occasion.ImageFile;
                    string blobName = Guid.NewGuid().ToString() + "_" + fileUpload.FileName;
                    var blobClient = _containerClient.GetBlobClient(filename);

                    using (var stream = fileUpload.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobHttpHeaders
                        {
                            ContentType = fileUpload.ContentType
                        });
                    }

                    string blobUrl = blobClient.Uri.ToString();

                    occasion.Filename = blobUrl;

                    //delete the old file

                }

                //
                //Step 2: Save to database
                //

                _context.Add(occasion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Need to move below?
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name");
            return View(occasion);
        }

        // GET: Occasions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion.FindAsync(id);
            if (occasion == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", occasion.CategoryId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name", occasion.VenueId);
            return View(occasion);
        }

        // POST: Occasions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OccasionId,Title,Description,Date,Time,Owner,VenueId,CategoryId,ImageFile")] Occasion occasion)
        {
            //var existingOccasion = await _context.Occasion.FindAsync(id);

            if (id != occasion.OccasionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //
                    //Step 1: Image upload code would go here
                    //
                    if (occasion.ImageFile != null && occasion.ImageFile.Length > 0)
                    {
                        //string oldFilename = occasion.ImageFile.FileName;

                        // Delete old file if it exists
                        //if (!string.IsNullOrEmpty(existingOccasion?.Filename))
                        //{
                        //    string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Uploads", existingOccasion.Filename);
                        //    if (System.IO.File.Exists(oldFilePath))
                        //    {
                        //        System.IO.File.Delete(oldFilePath);
                        //    }
                        //}

                        //Find old file if it exists
                        //var existingOccasion = await _context.Occasion.FindAsync(id);

                        // Create a unique filename using a GUID and the original file extension
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(occasion.ImageFile.FileName); //ex. guid: 234234-23423-4234234-23423.jpg    

                        // Initialize the filename in the record
                        occasion.Filename = filename;

                        // Get the file path to save the file (C:\WebAppProg\NSCC-WebAppProg-SeatYourself\NSCC-WebAppProg-SeatYourself\wwwroot\Images\Uploads)
                        string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Uploads", filename);

                        //Save file
                        using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                        {
                            await occasion.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    //
                    //Step 2: Save to database
                    //
                    
                    occasion.CreatedAt = DateTime.Now; // This will put the edited date/time, to keep the original created date/time it must be put into a variable? leaving this for now
                    _context.Update(occasion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccasionExists(occasion.OccasionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", occasion.CategoryId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name", occasion.VenueId);
            return View(occasion);
        }

        // GET: Occasions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion
                .Include(o => o.Category)
                .Include(o => o.Venue)
                .FirstOrDefaultAsync(m => m.OccasionId == id);
            if (occasion == null)
            {
                return NotFound();
            }

            return View(occasion);
        }

        // POST: Occasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var occasion = await _context.Occasion.FindAsync(id);
            if (occasion != null)
            {
                _context.Occasion.Remove(occasion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccasionExists(int id)
        {
            return _context.Occasion.Any(e => e.OccasionId == id);
        }
    }
}
