using System;
using System.Collections.Generic;

namespace LibraryRedux.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public int Booktitle { get; set; }
        public DateTime Duedate { get; set; }
        public string Renew { get; set; }

        public virtual Book BooktitleNavigation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
