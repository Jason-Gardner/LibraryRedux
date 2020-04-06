using System;
using System.Collections.Generic;

namespace LibraryRedux.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Booktitle { get; set; }
        public DateTime Duedate { get; set; }
        public string Renew { get; set; }
    }
}
