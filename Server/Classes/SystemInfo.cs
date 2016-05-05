using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

using Server.AdditionalClasses;

namespace Server
{

	public class SystemInfo
	{
		public SystemInfo()
		{
			
		}
		
		public List<Param> GetHDDInfo()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_DiskDrive");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				//Hashtable result = new Hashtable();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					results.Add(new Param("Model", process["Model"].ToString()));
					results.Add(new Param("SerialNumber", process["SerialNumber"].ToString()));
					results.Add(new Param("MediaType", process["MediaType"].ToString()));
					results.Add(new Param("Description", process["Description"].ToString()));
					results.Add(new Param("Caption", process["Caption"].ToString()));
					results.Add(new Param("InterfaceType", process["InterfaceType"].ToString()));
					results.Add(new Param("Partitions", process["Partitions"].ToString()));
					results.Add(new Param("Size", ((UInt64)(process["Size"]) / 1000000000).ToString() + " GB"));
					
					//result["Model"] = process["Model"].ToString();
					//result["SerialNumber"] = process["SerialNumber"].ToString();
					//result["Size"] = ((UInt64)(process["Size"]) / 1000000000).ToString() + " GB";
					//result["InterfaceType"] = process["InterfaceType"].ToString();
					//result["Partitions"] = process["Partitions"].ToString();
					//result["MediaType"] = process["MediaType"].ToString();
					//result["Caption"] = process["Caption"].ToString();
					//result["Description"] = process["Description"].ToString();
					
				}
				
				return results;
			 }
			
		}
		
		public void GetSystemInfo() {
			
		}
	}
}
