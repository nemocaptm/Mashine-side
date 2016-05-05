
using System;

namespace Server.Classes
{
	public static class ProgramState
	{
		static ProgramState()
		{
			// download from file. if not - default
			// ServerHaveInternet = false;
			// IP = "255.255.255.255";
			
			ServerHaveInternet = true;
			
		}
		
		// сетевые настройки
		public static bool ServerHaveInternet { get; set; }
		public static string IP { get; set; }
		
		// уникальный номер учетной записи (мобильника)
		public static string mobileID { get; set; }
	}
}
