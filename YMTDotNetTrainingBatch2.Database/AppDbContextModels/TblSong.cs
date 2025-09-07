using System;
using System.Collections.Generic;

namespace YMTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class TblSong
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public string Genres { get; set; } = null!;

    public DateTime ReleasedDate { get; set; }

    public bool DeleteFlag { get; set; }
}
