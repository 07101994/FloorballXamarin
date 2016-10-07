using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball
{
    public class UpdateData
    {
        public UpdateEnum Type { get; set; }

        public object Entity { get; set; }

        public bool IsAdding { get; set; }

    }
}
