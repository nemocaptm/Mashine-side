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
					results.Add(new Param(string.Empty, string.Empty));
				
				}
				
				return results;
			 }
			
		}
		
		public List<Param> GetNetworkInfo(bool viewOnlyWithMac)
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					// если у устройства нету MAC и установлен флаг "показывать только с MAC"
					// значит пропускаем устройство
					if( !(process["MACAddress"] is string) && (viewOnlyWithMac) ) continue;
					
					if(process["AdapterType"] is string)
					{
						results.Add(new Param("AdapterType", process["AdapterType"].ToString()));
					}
					results.Add(new Param("Name", process["Name"].ToString()));

					if(process["MACAddress"] is string)
					{
						results.Add(new Param("MACAddress", process["MACAddress"].ToString()));
						
						// ищем дополнительные данные(IP, MASK, GATEWAY)
						List<Param> IPs = GetAddressInfoByMAC(process["MACAddress"].ToString());
						
						foreach (var Address in IPs) {
							results.Add(new Param(Address.ParamName, Address.ParamValue));
						}
					}
					
					// пустая строчка из 2 ячеек - разделитель
					results.Add(new Param(string.Empty, string.Empty));
					
				}
	
				return results;
			 }
		}
		
		public List<Param> GetAddressInfoByMAC(string mac)
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE MACAddress='" + mac + "'");// WHERE IPEnabled = 'TRUE'");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				ManagementObjectCollection queryCollection = searcher.Get();
				foreach (ManagementObject process in queryCollection) 
				{
					string[] IPs = (string[])process["IPAddress"];
					if(IPs is string[])
					{
						results.Add(new Param("IPAddress", IPs[0]));
					}
					
					string[] subnets = (string[])process["IPSubnet"];
					if(subnets is string[])
					{
						results.Add(new Param("Subnetmask", subnets[0]));
					}
					
					string[] gateways = (string[])process["DefaultIPGateway"];
					if(gateways is string[])
					{
						results.Add(new Param("Gateway", gateways[0]));
					}					
				}
						
				return results;
			 }
		}
		
		public List<string> GetApplicationsInfo()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_Product");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<string> results = new List<string>();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					results.Add(process["Name"].ToString());
				
				}
				
				// сортируем список перед отправкой
				results.Sort();
				
				return results;
			 }
		}
		
		public void GetSystemInfo() {
			
		}
	}
}
