using YMTDotNetTrainingBatch2.DA;
using YMTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YMTDotNetTrainingBatch2.BL
{
    public class SongsService
    {
        private readonly SongsDataAccess _songsDataAccess;

        public SongsService(SongsDataAccess songsDataAccess)
        {
            _songsDataAccess = songsDataAccess;
        }

        public async Task<List<TblSong>> GetSongsAsync(int pageNo, int size)
        {
            if(pageNo == 0 || size == 0)
            {
                throw new Exception("Either page size or page number cannot be zero!");
            }
            return await _songsDataAccess.GetSongsAsync(pageNo, size);
        }

        public async Task<TblSong> GetSongByIdAsync(int id)
        {
            if(id == 0)
            {
                throw new Exception("Invalid id");
            }
            return await _songsDataAccess.GetSongByIdAsync(id);
        }
        public async Task<int> CreateSongAsync(TblSong song)
        {
            int result = await _songsDataAccess.CreateSongAsync(song);
            return result;
        }

        public async Task<int> UpdateSongAsync(TblSong song, int id)
        {
            if(id == 0)
            {
                throw new Exception("Invalid id");
            }
            int result = await _songsDataAccess.UpdateSongAsync(song, id);
            return result;  
        }

        public async Task<int> DeleteSongAsync(int id)
        {
            if(id == 0)
            {
                throw new Exception("Invalid id");
            }
            int result = await _songsDataAccess.DeleteSongAsync(id);
            return result;
        }
    }
}
