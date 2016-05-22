
using System;

namespace Server.AdditionalClasses
{
	public class HDDParam
	{
		public HDDParam(string label, string size, string busy)
		{
			Label = label;
			Size = size;
			Busy = busy;
		}
				
		public string Label { get; set; }
		public string Size { get; set; }
		public string Busy { get; set; }
	}
}
