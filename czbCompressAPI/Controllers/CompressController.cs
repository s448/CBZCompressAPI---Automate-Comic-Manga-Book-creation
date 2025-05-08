using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace CBZFileGeneratorAPI.Controllers
{
    [Route("api/comicbook")]
    [ApiController]
    public class ComicBookController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateComicBook([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            // Create a temporary directory to store the images
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                // Save the uploaded images to the temp directory
                foreach (var file in files)
                {
                    var filePath = Path.Combine(tempDir, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                // Create a .cbz (zip) file from the images
                var cbzFileName = $"ComicBook_{Guid.NewGuid()}.cbz";
                var cbzFilePath = Path.Combine(Path.GetTempPath(), cbzFileName);

                ZipFile.CreateFromDirectory(tempDir, cbzFilePath);

                // Return the file as a download response
                var fileBytes = await System.IO.File.ReadAllBytesAsync(cbzFilePath);
                return File(fileBytes, "application/vnd.comicbook+zip", cbzFileName);
            }
            finally
            {
                // Cleanup temporary files
                Directory.Delete(tempDir, true);
            }
        }
    }
}
