using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playlist
{
    internal class Node
    {
        public Song Value { get; }
        public Node Next { get; set; }
        public Node Prev { get; set; }


        public Node(Song song)
        {
            Value = song;
            Next = this;
            Prev = this;
        }
    }
}
