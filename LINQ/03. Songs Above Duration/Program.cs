
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MusicHub.Data;
using MusicHub.Data.Models;
using  MusicHub.Initializer;

namespace MusicHub{

    public class StartUp{


        public static void Main(string[] args){
            MusicHubDbContext context = new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);
            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }


        public static  string ExportAlbumsInfo(MusicHubDbContext context, int ProducerId){

            var Albums = context.Albums.Where(x => x.ProducerId  == ProducerId).Select(x => new {
                x.Name,
                ReleaseDate = x.ReleaseDate.ToString("MM/dd/yyyy"),
                ProducerName = x.Producer.Name,
                Songs = x.Songs.OrderByDescending(x => x.Name).ThenBy(x => x.Writer.Name).ToList(),
                TotalPrice = x.Songs.Sum(x => x.Price)
            })
            .OrderByDescending(x => x.TotalPrice).ToList();


            List<string> final = new List<string>();


            foreach (var album in Albums){

                final.Add($"-AlbumName: {album.Name}");
                final.Add($"-ReleaseDate: {album.ReleaseDate}");
                final.Add($"-ProducerName: {album.ProducerName}");
                final.Add("-Songs:");
                int  count = 1;

                foreach (var song in album.Songs){

                    final.Add($"---#{count}");
                    count++;
                    final.Add($"---SongName: {song.Name}");
                    final.Add($"---Price: {song.Price:F2}");
                    final.Add($"---Writer: {song.Writer.Name}");

                }

                final.Add($"-AlbumPrice: {album.TotalPrice:F2}");


            }

            return string.Join("\n", final);
        }


        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration){

            var songs = context.Songs
            .AsEnumerable() 
            .Where(x => x.Duration.TotalSeconds > duration)
            .Select(x => new {
                x.Name,
                Performer_Name = x.SongPerformers.Select(y => new {FullName = $"{y.Performer.FirstName} {y.Performer.LastName}"}).OrderBy(z => z.FullName),
                WriterName = x.Writer.Name,
                AlbumProducer = x.Album.Producer.Name,
                Duration = $"{x.Duration:c}"
            })
            .OrderBy(x => x.Name)
            .ThenBy(x => x.WriterName);

            List<string> final = new List<string>();

            int count = 1;
            foreach(var song in songs){
                final.Add($"-Song #{count}");
                final.Add($"---SongName: {song.Name}");
                final.Add($"---Writer: {song.WriterName}");
                if (song.Performer_Name != null){
                    foreach (var name in song.Performer_Name){
                        final.Add($"---Performer: {name.FullName}");
                    }
                }
                final.Add($"---AlbumProducer: {song.AlbumProducer}");
                final.Add($"---Duration: {song.Duration}");

                count++;
            }


            return string.Join("\n", final);
        }

    }


    


}
