using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YMTDotNetTrainingBatch2.BL;
using YMTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YMTDotNetTrainingBatch2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongsService _songsService;
        private readonly IConfiguration _configuration;

        public SongsController(SongsService songsService, IConfiguration configuration)
        {
            _songsService = songsService;
            _configuration = configuration;
        }

        [HttpGet("List/{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetSongsAsync(int pageNo, int pageSize)
        {
            var songs = await _songsService.GetSongsAsync(pageNo, pageSize);
            return Ok(songs);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongByIdAsync(int id)
        {
            var song = await _songsService.GetSongByIdAsync(id);
            return Ok(song);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSongAsync([FromBody] TblSong song)
        {
            if (song == null)
            {
                return BadRequest("New song is empty");
            }
            var res = await _songsService.CreateSongAsync(song);
            return Ok(res);

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSongAsync(int id, [FromBody] TblSong song)
        {
            if (song == null)
            {
                return BadRequest("Updated song is empty");

            }
            var res = await _songsService.UpdateSongAsync(song, id);
            return Ok(res);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongAsync(int id)
        {
            var res = await _songsService.DeleteSongAsync(id);
            return Ok(res);
        }


    }
}