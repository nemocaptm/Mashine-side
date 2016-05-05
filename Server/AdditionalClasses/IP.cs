
using System;

namespace Server.AdditionalClasses
{
	public class IP
	{
		
		private int FirstNumber { get; set; }
		private int SecondNumber { get; set; }
		private int ThirdNumber { get; set; }
		private int FourthNumber { get; set; }
		
		public IP()
		{
			FirstNumber = 0;
			SecondNumber = 0;
			ThirdNumber = 0;
			FourthNumber = 0;
		}
		
		public IP(string first, string second, string third, string fourth) : base()
		{
			int a, b, c, d;
			
			int.TryParse(first, out a);
			int.TryParse(second, out b);
			int.TryParse(third, out c);
			int.TryParse(fourth, out d);
			
			if ( (0 <= a && a < 256) &&
				 (0 <= b && b < 256) &&
				 (0 <= c && c < 256) &&
				 (0 <= d && d < 256) )
			{
				FirstNumber = a;
				SecondNumber = b;
				ThirdNumber = c;
				FourthNumber = d;
			}

		}

		public string[] GetIP()
		{
			return new string[] { 
				FirstNumber.ToString(),
				SecondNumber.ToString(), 
				ThirdNumber.ToString(),
				FourthNumber.ToString()
			};
		}
		
		public static string IpToString(string first, string second, string third, string fourth)
		{
			return string.Format("{0}.{1}.{2}.{3}", first, second, third, fourth);
		}
		
		public static string[] StringToIp(string ip)
		{
			return ip.Split('.');
		}
		
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3}", FirstNumber, SecondNumber, ThirdNumber, FourthNumber);
		}
		
	}
}
