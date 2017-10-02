using System;
using System.Collections.Generic;

namespace FloorballAdminiOS.UI.Entity
{
    public class SegmentControlModel
    {
        public List<Tuple<string,string>> Segments { get; set; }

        public int Selected { get; set; }
    }
}
