
using System;

namespace Server.AdditionalClasses
{

	public class Route
	{
		
		public string Destination { get; set; }
		public string Mask { get; set; }
		public string Gateway { get; set; }
		public string Interface { get; set; }
		public string Metric { get; set; }
		
		public Route()
		{
			
		}
	}
}
