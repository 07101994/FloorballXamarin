using System;
using System.Collections.Generic;
using Floorball;

namespace FloorballAdminiOS.UI.Entity
{
    public class SegmentControlModel
    {
        public List<string> Model { get; set; }
        public List<object> SelectedValues { get; set; } = new List<object>();

        public int Selected { get; set; }

        public SegmentControlModel(List<string> segmentControlModel, List<int> selectedValues)
        {
            Model = segmentControlModel;

            foreach (var v in selectedValues)
            {
                SelectedValues.Add(v);
            }

        }

		public SegmentControlModel(List<string> segmentControlModel, List<GenderEnum> selectedValues)
		{
			Model = segmentControlModel;

			foreach (var v in selectedValues)
			{
				SelectedValues.Add(v);
			}

		}

    }
}
