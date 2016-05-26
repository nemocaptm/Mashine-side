
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using Server.AdditionalClasses;

namespace Server.Classes
{
	public class POST
	{		
		public string URLDevices { get; set; }
		public string URLApps { get; set; }
		public string URLRams { get; set; }
		
		public POST()
		{
			URLDevices = @"http://center.hol.es/endpoints/devices.php";
			URLApps = @"http://center.hol.es/endpoints/apps.php";
			URLRams = @"http://center.hol.es/endpoints/rams.php";
		}
		
		private StringBuilder JSONObject { get; set; }
		
		//public string Caption { get; set; }
		//public string deviceID { get; set; }
		//public string caption { get; set; }
		//public string serialNumber { get; set; }
		//public string bankLabel { get; set; }
		//public string formFactor { get; set; }
		//public string memoryType { get; set; }
		//public string capacity { get; set; }
		//public string speed { get; set; }
		
		public void SendDevices()
		{
			SystemInfo systemInfo = new SystemInfo();
			
			//List<Param> hdd = systemInfo.GetHDDInfo();
			//List<Param> net = systemInfo.GetNetworkInfo(true);
			//List<Route> staticRoutes = systemInfo.GetStaticRoutes();
			//List<Param> network = systemInfo.GetRAMInfo();
									
			JSONObject = new StringBuilder();
			JSONObject.Clear();
			JSONObject.AppendLine("{ ");
			
			List<Param> cpu = systemInfo.GetCPUInfo();
			List<Param> os = systemInfo.GetOperatingSystem();
			 
			AddToJSON(cpu, "CPU");
			AddToJSON(os, "OS");
						
			List<Param> AdvancedData = new List<Param>();
			AdvancedData.Add(new Param("UnicalID", ProgramState.UnicalID));
			AdvancedData.Add(new Param("PCID", ProgramState.PCID));
			
			AddToJSON(AdvancedData, string.Empty);
			
			StringBuilder newJSONObject = JSONObject.Remove(JSONObject.Length - 3, 1);
			newJSONObject.AppendLine(" }");
			
			string data = JSONObject.ToString();
			SendToWeb(data, URLDevices);
			
		}
			
		private void AddToJSON(List<Param> collection, string prefix)
		{
			foreach (var element in collection) {
				if(element.ParamName == string.Empty) continue;
				JSONObject.AppendLine("\"" + prefix + element.ParamName + "\": \"" + element.ParamValue + "\",");
			}
		}

		public void SendApps()
		{
			SystemInfo systemInfo = new SystemInfo();
			
			JSONObject = new StringBuilder();
			JSONObject.Clear();
			JSONObject.AppendLine("{ ");
			
			List<string> apps = systemInfo.GetApplicationsInfo();

			AddToJSON(apps, "Name");
						
			List<Param> AdvancedData = new List<Param>();
			AdvancedData.Add(new Param("PCID", ProgramState.PCID));
			
			AddToJSON(AdvancedData, string.Empty);
			
			StringBuilder newJSONObject = JSONObject.Remove(JSONObject.Length - 3, 1);
			newJSONObject.AppendLine(" }");
			
			string data = JSONObject.ToString();
			SendToWeb(data, URLApps);
			
		}
		
		private void AddToJSON(List<string> collection, string name)
		{
			JSONObject.AppendLine("\"" + name + "\":");
			JSONObject.AppendLine("[ ");
			foreach (var element in collection) {
				JSONObject.AppendLine("\"" + element + "\",");
			}
			
			StringBuilder newJSONObject = JSONObject.Remove(JSONObject.Length - 3, 1);
			newJSONObject.AppendLine(" ],");
		}
		
		public void SendRAMs()
		{
			SystemInfo systemInfo = new SystemInfo();
			
			JSONObject = new StringBuilder();
			JSONObject.Clear();
			JSONObject.AppendLine("{ ");
			
			List<Param> AdvancedData = new List<Param>();
			AdvancedData.Add(new Param("PCID", ProgramState.PCID));
			AddToJSON(AdvancedData, string.Empty);
			
			List<Param> rams = systemInfo.GetRAMInfo();

			AddToJSON(rams, "RAMs", true);
						

			
			//StringBuilder newJSONObject = JSONObject.Remove(JSONObject.Length - 3, 1);
			JSONObject.AppendLine(" }");
			
			string data = JSONObject.ToString();
			SendToWeb(data, URLRams);
			
		}
		
		private void AddToJSON(List<Param> collection, string name, bool severalElements)
		{
			JSONObject.AppendLine("\"" + name + "\":");
			JSONObject.AppendLine("[{");
			foreach (var element in collection) {
				
				if(element.ParamName == string.Empty)
				{
					JSONObject = JSONObject.Remove(JSONObject.Length - 3, 1);
					JSONObject.AppendLine(" }, {");
					continue;
				}
				else {
					JSONObject.AppendLine("\"" + element.ParamName + "\": \"" + element.ParamValue + "\",");
				}
				
			}			
			JSONObject = JSONObject.Remove(JSONObject.Length - 5, 3);
			JSONObject.AppendLine(" ]");
		}
		
		private void SendToWeb(string data, string url)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			
    		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
    			streamWriter.Write(data);
    			streamWriter.Flush();
    			streamWriter.Close();
			}
    		
    		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
    			var result = streamReader.ReadToEnd();
			}
		}
	}
}
