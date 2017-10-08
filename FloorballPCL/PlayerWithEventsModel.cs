using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Models
{
    public class PlayerWithEventsModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public int Goals
        {
            get
            {
                return Events.Count(e => e.Type == EventType.G);
            }
        }

        public int Assists
        {
            get
            {
                return Events.Count(e => e.Type == EventType.A);
            }
        }

        public int P2
        {
            get
            {
                return Events.Count(e => e.Type == EventType.P2);
            }
        }

        public int P5
        {
            get
            {
                return Events.Count(e => e.Type == EventType.P5);
            }
        }

        public int P10
        {
            get
            {
                return Events.Count(e => e.Type == EventType.P10);
            }
        }

        public int PV
        {
            get
            {
                return Events.Count(e => e.Type == EventType.PV);
            }
        }

    }
}