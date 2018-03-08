using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace komunikaty
{
    public class KomunikatySerwera
    {
        public const string OK = "<msg>connect</msg>OK";
        //public const string OK = "###__OK";
        public const string Cancel = "###__Cancel";
        public const string Rozlacz = "###__Rozlacz";
    }

    public class KomunikatyKlienta
    {
        public const string Zadaj = "<msg>connect</msg>";
        public const string Rozlacz = "<msg>disconnect</msg>";
        //public const string Zadaj = "###__Zadaj";
        //public const string Rozlacz = "###__Rozlacz";
    }
}