
using System;
using System.IO;

namespace Server.Classes
{
	public static class ProgramState
	{
		private const string fileName = @"unicalID.txt";
		
		static ProgramState()
		{
			// download from file. if not - default
			// ServerHaveInternet = false;
			// IP = "255.255.255.255";
			
			ServerHaveInternet = true;
			
			if(File.Exists(fileName))
			{
				UnicalID = File.ReadAllText(fileName);
			}	
			
			
			// серийный номер hdd как уникальный id PC
			SystemInfo sinfo = new SystemInfo();
			PCID = sinfo.GetPCID();
			
		}
		
		public static void SaveState()
		{
			if(File.Exists(fileName))
			{
				File.Delete(fileName);	
			}
			File.WriteAllText(fileName, UnicalID);
			
		}
		
		// сетевые настройки
		public static bool ServerHaveInternet { get; set; }
		public static string IP { get; set; }
		
		// уникальный номер учетной записи
		public static string UnicalID { get; set; }
		// уникальный номер учетной записи
		public static string PCID { get; set; }
	}
}
