using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YMTDotNetTrainingBatch2.Database.AppDbContextModels;

namespace YMTDotNetTrainingBatch2.SongConsoleApp
{
    internal class HttpClientService
    {

        private readonly HttpClient _httpClient;
        public HttpClientService(string baseUrl)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task ReadAsync()
        {
        EnterPageNo:
            Console.Write("\nEnter page number : ");
            bool isInt = int.TryParse(Console.ReadLine(), out int pageNo);
            if (!isInt)
            {
                Console.WriteLine("Invalid page number ");
                goto EnterPageNo;
            }
        EnterPageSize:
            Console.Write("Enter page size : ");
            isInt = int.TryParse(Console.ReadLine(), out int size);
            if (!isInt)
            {
                Console.WriteLine("Invalid page number ");
                goto EnterPageSize;
            }

            
            var response = await _httpClient.GetFromJsonAsync<List<TblSong>>($"List/{pageNo}/{size}");
            if(response != null && response.Any())
            {
                Console.WriteLine();
                PrintTableData(response);
                Console.WriteLine();

            }

        }

        public async Task ReadSongById()
        {
        EnterId:
            Console.Write("\nEnter Song Id : ");
            bool isInt = int.TryParse(Console.ReadLine(), out int id);
            if (!isInt)
            {
                Console.WriteLine("Invalid id");
                goto EnterId;
            }
            var response = await _httpClient.GetFromJsonAsync<TblSong>($"{id}");
            if(response != null)
            {
                Console.WriteLine();
                PrintTableData(response);
                Console.WriteLine();

            }
        }

        public async Task CreateSongAsync()
        {
            TblSong song = new TblSong();

            Console.Write("\nSong name : ");
            string songTitle = Console.ReadLine()!;
            Console.Write("Artist  : ");
            string songArtist = Console.ReadLine()!;
            Console.Write("Genre : ");
            string songGenre = Console.ReadLine()!;
            Console.Write("Release Date : ");

        EnterDate:
            bool isDate = DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate);
            if (!isDate)
            {
                Console.WriteLine("Invalid date format!");
                goto EnterDate;
            }
            bool deleteFlag = false;

            song.Title = songTitle;
            song.Artist = songArtist;
            song.Genres = songGenre;
            song.ReleasedDate = releaseDate;
            song.DeleteFlag = deleteFlag;

            var response = await  _httpClient.PostAsJsonAsync<TblSong>("song", song);
            Console.WriteLine(response.IsSuccessStatusCode ? "Song Added Successfully" : "Failed To Add Song");
        }

        public async Task UpdateSongAsync()
        {
            TblSong song = new TblSong();
        EnterId: 
            Console.Write("\nEnter Id To Update : ");
            bool isInt = int.TryParse(Console.ReadLine(), out int id);
            if (!isInt)
            {
                Console.WriteLine("Invalid Id");
                goto EnterId;
            }
            TblSong? existing = await _httpClient.GetFromJsonAsync<TblSong>($"{id}");
            if(existing == null)
            {
                Console.WriteLine("Invalid Id");
                goto EnterId;
            }
            PrintTableData(existing);

            Console.WriteLine("Updaing song data");
            Console.WriteLine("-------------------\n\n");
            Console.Write("Song name : ");
            string songTitle = Console.ReadLine()!;
            Console.Write("Artist  : ");
            string songArtist = Console.ReadLine()!;
            Console.Write("Genre : ");
            string songGenre = Console.ReadLine()!;
            Console.Write("Release Date : ");
        EnterDate:
            bool isDate = DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate);
            if (!isDate)
            {
                Console.WriteLine("Invalid date format!");
                goto EnterDate;
            }

            existing.Title = songTitle;
            existing.Artist = songArtist;
            existing.Genres = songGenre;
            existing.ReleasedDate = releaseDate;

            var res = await _httpClient.PatchAsJsonAsync($"{id}", existing);
            Console.WriteLine(res.IsSuccessStatusCode ? "Song updated successfully" : "Failed to update song");
        }

        public async Task DeleteSongAsync()
        {
        EnterId:
            Console.Write("\nEnter Id To Delete : ");
            bool isInt = int.TryParse(Console.ReadLine(), out int id);
            if (!isInt)
            {
                Console.WriteLine("Invalid Id");
                goto EnterId;
            }
            var res = await _httpClient.DeleteAsync($"{id}");
            Console.WriteLine(res.IsSuccessStatusCode ? "Song deleted successfully" : "Failed to delete song");
            
        }

        #region Print Table Data With Table Format
        //Print table data for 1 row
        private void PrintTableData(TblSong song)
        {

            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-30} {4,-30}",
               "Song Id", "Song Name", "Artist", "Genres", "Released Date");
            Console.WriteLine("{0, -30} {1, -30} {2, -30} {3,-30} {4,-30}",
                "-------------------------", "-------------------------", "-------------------------", "-------------------------", "-------------------------");


            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-30} {4,-30}", song.Id, song.Title, song.Artist, song.Genres, song.ReleasedDate);
            Console.WriteLine();
        }
        //Print table data for a list of rows
        private void PrintTableData(List<TblSong> songList)
        {
            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-30} {4,-30}",
               "Song Id", "Song Name", "Artist", "Genres", "Released Date");
            Console.WriteLine("{0, -30} {1, -30} {2, -30} {3,-30} {4,-30}",
                "-------------------------", "-------------------------", "-------------------------", "-------------------------", "-------------------------");

            songList.ForEach(row =>
                Console.WriteLine("{0,-30} {1,-30} {2,-30} {3, -30} {4, -30}", row.Id, row.Title, row.Artist, row.Genres, row.ReleasedDate)
            );
            Console.WriteLine(new string('-', 150));
            Console.WriteLine();
        }
        #endregion
    }
}
