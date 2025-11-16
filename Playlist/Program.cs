using Playlist;
using System;

namespace PlaylistCircular
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var playlist = new CircularPlaylist();

            var songs = new[]
            {
                new Song(Guid.NewGuid(), "Imagine", "John Lennon", 183),
                new Song(Guid.NewGuid(), "Billie Jean", "Michael Jackson", 294),
                new Song(Guid.NewGuid(), "Smells Like Teen Spirit", "Nirvana", 301),
                new Song(Guid.NewGuid(), "One", "U2", 276),
            };

            foreach (var s in songs)
                playlist.AddLast(s);

            Console.WriteLine("Playlist inicial:");
            Console.WriteLine(playlist.DebugPrint());

            Console.WriteLine("Next(): " + playlist.Next());
            Console.WriteLine("Cursor: " + playlist.Current);

            Console.WriteLine("\nShuffle(42):");
            playlist.Shuffle(42);
            Console.WriteLine(playlist.DebugPrint());

            Console.WriteLine("Export JSON:");
            Console.WriteLine(playlist.ExportTitlesJson());

            Console.WriteLine("\nEliminando: " + songs[1].Title);
            playlist.RemoveById(songs[1].Id);
            Console.WriteLine(playlist.DebugPrint());

            Console.WriteLine("Fin del demo.");
        }
    }
}