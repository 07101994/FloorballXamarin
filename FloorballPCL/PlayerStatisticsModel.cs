using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball
{
    public class PlayerStatisticsModel
    {

        public int PlayerId { get; set; }

        public int TeamId { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int Points
        {
            get
            {
                return Goals + Assists;
            }
        }

        public string Penalties { get; set; }



    }
}
