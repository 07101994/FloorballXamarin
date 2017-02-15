using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.Updater
{
    public class UpdateData
    {
        public UpdateEnum Type { get; set; }

        public object Entity { get; set; }

        public bool IsAdding { get; set; }

    }
}
