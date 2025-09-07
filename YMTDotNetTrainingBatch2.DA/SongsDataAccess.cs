using Microsoft.EntityFrameworkCore;
using YMTDotNetTrainingBatch2.Database.AppDbContextModels;
namespace YMTDotNetTrainingBatch2.DA
  
{
    public class SongsDataAccess
    {
        private readonly AppDbContext _db;

        public SongsDataAccess(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TblSong>> GetSongsAsync(int pageNo, int size)
        {
            return await _db.TblSongs
                .Where(x => !x.DeleteFlag)
                .Skip((pageNo - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<TblSong> GetSongByIdAsync(int id)
        {
            var item = await _db.TblSongs.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if(item == null)
            {
                throw new Exception("Song not found");

            }
            return item;
        }

        public async Task<int> CreateSongAsync(TblSong song)
        {
            await _db.AddAsync(song);
            int res = await _db.SaveChangesAsync();
            return res;
        }

        public async Task<int> UpdateSongAsync(TblSong song, int id)
        {
            TblSong? item = await _db.TblSongs.FirstOrDefaultAsync(x => !x.DeleteFlag && x.Id == id);
            if(item == null)
            {
                throw new Exception("Song not found");
            }
            item.Title = song.Title;
            item.Artist = song.Artist;
            item.Genres = song.Genres;
            item.ReleasedDate = song.ReleasedDate;

            int res = await _db.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteSongAsync(int id)
        {
            TblSong? item = await _db.TblSongs.FirstOrDefaultAsync(x => !x.DeleteFlag && x.Id == id);
            if (item == null)
            {
                throw new Exception("Song not found");
            }
            item.DeleteFlag = true;
            int res = await _db.SaveChangesAsync();
            return res;
        }
        
    }
}
