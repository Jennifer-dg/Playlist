using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Playlist
{
    public class CircularPlaylist
    {
        private Node? head;
        private Node? tail;
        private Node? cursor;

        public int Count { get; private set; }

        public CircularPlaylist()
        {
            Count = 0;
        }

        public void AddLast(Song s)
        {
            var n = new Node(s);

            if (head == null)
            {
                head = tail = cursor = n;
                Count = 1;
                return;
            }

            n.Prev = tail!;
            n.Next = head;

            tail!.Next = n;
            head.Prev = n;

            tail = n;

            Count++;
        }

        public bool RemoveById(Guid id)
        {
            if (head == null) return false;

            var cur = head;

            for (int i = 0; i < Count; i++)
            {
                if (cur.Value.Id == id)
                {
                    if (Count == 1)
                    {
                        head = tail = cursor = null;
                        Count = 0;
                        return true;
                    }

                    cur.Prev.Next = cur.Next;
                    cur.Next.Prev = cur.Prev;

                    if (cur == head) head = cur.Next;
                    if (cur == tail) tail = cur.Prev;

                    if (cursor == cur) cursor = cur.Next;

                    Count--;
                    return true;
                }

                cur = cur.Next;
            }

            return false;
        }

        public Song? Next()
        {
            if (cursor == null) return null;
            cursor = cursor.Next;
            return cursor.Value;
        }

        public Song? Prev()
        {
            if (cursor == null) return null;
            cursor = cursor.Prev;
            return cursor.Value;
        }

        public Song? Current => cursor?.Value;

        public void Shuffle(int seed)
        {
            if (Count <= 1) return;

            var nodes = new List<Node>();

            var cur = head!;
            for (int i = 0; i < Count; i++)
            {
                nodes.Add(cur);
                cur = cur.Next;
            }

            var currentId = cursor?.Value.Id;

            var rng = new Random(seed);

            for (int i = nodes.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (nodes[i], nodes[j]) = (nodes[j], nodes[i]);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                Node n = nodes[i];
                Node next = nodes[(i + 1) % nodes.Count];
                Node prev = nodes[(i - 1 + nodes.Count) % nodes.Count];

                n.Next = next;
                n.Prev = prev;
            }

            head = nodes[0];
            tail = nodes[^1];

            cursor = nodes.FirstOrDefault(n => n.Value.Id == currentId) ?? head;
        }

        public string ExportTitlesJson()
        {
            var titles = new List<string>();

            if (head == null)
                return JsonSerializer.Serialize(titles);

            var cur = head;
            for (int i = 0; i < Count; i++)
            {
                titles.Add(cur.Value.Title);
                cur = cur.Next;
            }

            return JsonSerializer.Serialize(titles, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public string DebugPrint()
        {
            if (head == null) return "(empty)";

            var cur = head;
            var result = "";

            for (int i = 0; i < Count; i++)
            {
                string mark = (cur == cursor) ? " <- cursor" : "";
                result += $"{i + 1}. {cur.Value}{mark}\n";
                cur = cur.Next;
            }

            return result;
        }
    }
}
