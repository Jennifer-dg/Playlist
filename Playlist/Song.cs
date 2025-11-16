using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playlist
    {
        public class Song
        {
            public Guid Id { get; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public int DurationInSeconds { get; set; }


            public Song(Guid id, string title, string artist, int durationInSeconds)
            {
                Id = id;
                Title = title;
                Artist = artist;
                DurationInSeconds = durationInSeconds;
            }


            public override string ToString() => $"{Title} - {Artist} ({DurationInSeconds}s)";
        }
    }
