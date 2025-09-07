// See https://aka.ms/new-console-template for more information

using YMTDotNetTrainingBatch2.SongConsoleApp;

HttpClientService httpClientService = new HttpClientService("https://localhost:7176/api/Songs/");

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\n Songs ");
    Console.WriteLine("1. View all songs");
    Console.WriteLine("2. View song by Id");
    Console.WriteLine("3. Add new song");
    Console.WriteLine("4. Update existing song");
    Console.WriteLine("5. Delete song");
    Console.WriteLine("6. Exit");
    Console.Write("Pick your option: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": await httpClientService.ReadAsync(); break;
        case "2": await httpClientService.ReadSongById(); break;
        case "3": await httpClientService.CreateSongAsync(); break;
        case "4": await httpClientService.UpdateSongAsync(); break;
        case "5": await httpClientService.DeleteSongAsync(); break;
        case "6": isRunning = false; break;
        default: Console.WriteLine("Invalid choice."); break;
    }
}