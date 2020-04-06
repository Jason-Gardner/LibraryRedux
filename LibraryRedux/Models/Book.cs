using System;
using System.Collections.Generic;

namespace LibraryRedux.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Available { get; set; }
        public int Checkedout { get; set; }
        public string Type { get; set; }
    }
}
