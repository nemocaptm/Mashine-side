﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Management;

using Server.AdditionalClasses;

namespace Server
{

	public class SystemInfo
	{
		private Hashtable _formFactor = new Hashtable();		
		private Hashtable _memoryType = new Hashtable();
		private Hashtable _CPUArchitecture = new Hashtable();
		
		private void _setFormFactorHashTable()
		{
			_formFactor["0"] = "Unknown";
			_formFactor["1"] = "Other";
			_formFactor["2"] = "SIP";
			_formFactor["3"] = "DIP";
			_formFactor["4"] = "ZIP";
			_formFactor["5"] = "SOJ";
			_formFactor["6"] = "Proprietary";
			_formFactor["7"] = "SIMM";
			_formFactor["8"] = "DIMM";
			_formFactor["9"] = "TSOP";
			_formFactor["10"] = "PGA";
			_formFactor["11"] = "RIMM";
			_formFactor["12"] = "SODIMM";
			_formFactor["13"] = "SRIMM";
			_formFactor["14"] = "SMD";
			_formFactor["15"] = "SSMP";
			_formFactor["16"] = "QFP";
			_formFactor["17"] = "TQFP";
			_formFactor["18"] = "SOIC";
			_formFactor["19"] = "LCC";
			_formFactor["20"] = "PLCC";
			_formFactor["21"] = "BGA";
			_formFactor["22"] = "FPBGA";
			_formFactor["23"] = "LGA";
		}
		
		private void _setMemoryTypeHashTable()
		{
			_memoryType["0"] = "Unknown";
			_memoryType["1"] = "Other";
			_memoryType["2"] = "DRAM";
			_memoryType["3"] = "Synchronous DRAM";
			_memoryType["4"] = "Cache DRAM";
			_memoryType["5"] = "EDO";
			_memoryType["6"] = "EDRAM";
			_memoryType["7"] = "VRAM";
			_memoryType["8"] = "SRAM";
			_memoryType["9"] = "RAM";
			_memoryType["10"] = "ROM";
			_memoryType["11"] = "Flash";
			_memoryType["12"] = "EEPROM";
			_memoryType["13"] = "FEPROM";
			_memoryType["14"] = "EPROM";
			_memoryType["15"] = "CDRAM";
			_memoryType["16"] = "3DRAM";
			_memoryType["17"] = "SDRAM";
			_memoryType["18"] = "SGRAM";
			_memoryType["19"] = "RDRAM";
			_memoryType["20"] = "DDR";
			_memoryType["21"] = "DDR2";
			_memoryType["22"] = "DDR2 FB-DIMM";
			
			_memoryType["24"] = "DDR3";
			_memoryType["25"] = "FBD2";
		}
		
		private void _setCPUArchitecture()
		{
			_CPUArchitecture["0"] = "x86";
			_CPUArchitecture["1"] = "MIPS";
			_CPUArchitecture["2"] = "Alpha";
			_CPUArchitecture["3"] = "PowerPC";
			_CPUArchitecture["5"] = "ARM";
			_CPUArchitecture["6"] = "ia64";
			_CPUArchitecture["9"] = "x64";
		}
		
		public SystemInfo()
		{
			// заполняем таблицу форм-факторов
			_setFormFactorHashTable();
			// заполняем таблицу типов ОЗУ
			_setMemoryTypeHashTable();
			// заполняем таблицу архитерктур CPU
			_setCPUArchitecture();
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
					results.Add(new Param("Size", ((UInt64)(process["Size"]) / (1024 * 1024 * 1024) ).ToString() + " GB"));
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
		
		public Hashtable GetNetworkInterfaceIndexs()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				Hashtable results = new Hashtable();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					string[] IPs = (string[])process["IPAddress"];
					if(IPs is string[])
					{
						results[IPs[0]] = IPs[0];
					}
					
					//results.Add(process["InterfaceIndex"].ToString());				
				}

					return results;
			 }
				

		}
		
		public List<Route> GetStaticRoutes()
		{
			
			Hashtable interfaceindexs = GetNetworkInterfaceIndexs();
			
			List<Route> results = new List<Route>();
			List<string> testInterface = new List<string>();
			
				SelectQuery query = new SelectQuery("SELECT * FROM Win32_IP4RouteTable");// WHERE InterfaceIndex=" + index + ";");
			
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
				{
					foreach (ManagementObject process in searcher.Get()) 
					{
						process.Get();
						
						Route route = new Route()
						{
							Destination = process["Destination"].ToString(),
							Mask = process["Mask"].ToString(),
							Gateway = process["NextHop"].ToString(),
							Metric = process["Metric1"].ToString()
						};
						
						
						SelectQuery queryTest = new SelectQuery( "SELECT * FROM Win32_NetworkAdapterConfiguration where Interfaceindex =" + process["InterfaceIndex"].ToString() );
						if( process["InterfaceIndex"].ToString() == "1" ) 
						{
							route.Interface = "127.0.0.1";
							results.Add(route);
							continue;
						}
							using (ManagementObjectSearcher searcherTest = new ManagementObjectSearcher(queryTest)) 
							{
								foreach (ManagementObject processTest in searcherTest.Get()) 
								{
									processTest.Get();
									string[] IPs = (string[])processTest["IPAddress"];
									if(IPs is string[])
									{
										route.Interface = IPs[0];
									}
								}
							}
							
						results.Add(route);
						
						}			
				}
			
			return results;
				
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
		
		public List<Param> GetRAMInfo()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_PhysicalMemory");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					results.Add(new Param("Caption", process["Caption"].ToString()));
					results.Add(new Param("SerialNumber", process["SerialNumber"].ToString()));
					results.Add(new Param("BankLabel", process["BankLabel"].ToString()));
					results.Add(new Param("FormFactor", (string)_formFactor[process["FormFactor"].ToString()]));
					results.Add(new Param("MemoryType", (string)_memoryType[process["MemoryType"].ToString()]));
					results.Add(new Param("Capacity", ((UInt64)process["Capacity"] / 1000000000).ToString() + " GB"));
					results.Add(new Param("Speed", process["Speed"].ToString() + " MHz"));
					
					results.Add(new Param(string.Empty, string.Empty));
					
				}
							
				return results;
			 }
		}
		
		public List<Param> GetCPUInfo()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_Processor");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					results.Add(new Param("Caption", process["Caption"].ToString()));
					results.Add(new Param("Manufacturer", process["Manufacturer"].ToString()));
					results.Add(new Param("Architecture", (string)_CPUArchitecture[process["Architecture"].ToString()]));
					results.Add(new Param("Role", process["Role"].ToString()));
					results.Add(new Param("NumberOfCores", process["NumberOfCores"].ToString()));	
					results.Add(new Param("CurrentClockSpeed", process["CurrentClockSpeed"].ToString() + " MHz"));
					results.Add(new Param("MaxClockSpeed", process["MaxClockSpeed"].ToString() + " MHz"));					
					results.Add(new Param("L2CacheSize", process["L2CacheSize"].ToString() + " KB"));
					results.Add(new Param("L3CacheSize", process["L3CacheSize"].ToString() + " KB"));

					results.Add(new Param(string.Empty, string.Empty));
					
				}
							
				return results;
			 }
		}
		
		public string GetCPUTemperature()
		{
			string result = string.Empty;
			
			foreach (var temperature in Temperature.Instances)
            {
				result = string.Format("{0:0.00} °C", temperature.CurrentTemperature);
            }
			
			return result;
		}
		
		public List<byte> GetHDDsTemperature()
		{
			const byte TEMPERATURE_ATTRIBUTE = 194;
			List<byte> result = new List<byte>();
			
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSStorageDriver_ATAPISmartData");
                // проходимся по всем установленым HHD 
        	foreach (ManagementObject queryObj in searcher.Get())
        	{
            	byte[] arrVendorSpecific = (byte[])queryObj.GetPropertyValue("VendorSpecific");
            	
            	// получаем температуру каждого
                int tempIndex = Array.IndexOf(arrVendorSpecific, TEMPERATURE_ATTRIBUTE);
           		result.Add(arrVendorSpecific[tempIndex + 5]);	
        	}
        	
        	return result;
		}
		
		public byte GetHDDCount()
		{
			byte result = 0;
			
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_DiskDrive");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				result = Convert.ToByte(searcher.Get().Count);
			}
			
			return result;
		}
		
		public List<Param> GetOperatingSystem()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				List<Param> results = new List<Param>();
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();
					
					results.Add(new Param("Name", process["Caption"].ToString()));
					results.Add(new Param("Version", process["Version"].ToString()));
					results.Add(new Param("RegisteredUser", process["RegisteredUser"].ToString()));
					results.Add(new Param("Manufacturer", process["Manufacturer"].ToString()));
				}
							
				return results;
			 }
		}
		
		
		public string GetBusyCPU()
		{
			string result = string.Empty;
			
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor");
			foreach (ManagementObject obj in searcher.Get())
			{
				result = obj["PercentProcessorTime"].ToString() + " %";
			}

			return result;
		}
		
		public string GetBusyRAM()
		{
			string result = string.Empty;

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");
			
			foreach (ManagementObject queryObj in searcher.Get())
            {
            	double free = Double.Parse(queryObj["FreePhysicalMemory"].ToString());
                double total = Double.Parse(queryObj["TotalVisibleMemorySize"].ToString());
                result = Math.Round(((total - free)/total * 100), 2).ToString() + " %";
            }
			
			return result;
		}
		
		public List<HDDParam> GetBusyHDDs()
		{
			List<HDDParam> results = new List<HDDParam>();

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_LogicalDisk");
			
			foreach (ManagementObject queryObj in searcher.Get())
            {
				double size = 0;
				double free = 0;
				
				if (!string.IsNullOrEmpty(Convert.ToString(queryObj["Size"]))) 
				{ 
					size = Double.Parse( ((UInt64)queryObj["Size"] / 1024 / 1024 / 1024).ToString() ) ;
				}
				
				if (!string.IsNullOrEmpty(Convert.ToString(queryObj["FreeSpace"]))) 
				{ 
					free = Double.Parse( ((UInt64)queryObj["FreeSpace"] / 1024 / 1024 / 1024).ToString() );
				}
				
				if (string.IsNullOrEmpty(Convert.ToString(queryObj["Name"])) )
				{ 
					if( !string.IsNullOrEmpty(Convert.ToString(queryObj["FreeSpace"])) && !string.IsNullOrEmpty(Convert.ToString(queryObj["FreeSpace"])) )
					{
						results.Add( new HDDParam("Local Disk", size.ToString() + " GB", Math.Round(((size - free)/size * 100), 2).ToString() + " %") );
					}	
				} 
				else 
				{ 
					if( !string.IsNullOrEmpty(Convert.ToString(queryObj["FreeSpace"])) && !string.IsNullOrEmpty(Convert.ToString(queryObj["FreeSpace"])) )
					{
						results.Add( new HDDParam(queryObj["Name"].ToString(), size.ToString() + " GB", Math.Round(((size - free)/size * 100), 2).ToString() + " %") );
					}	
				}
            }
			
			return results;
		}
		
		public string GetPCID()
		{
			string result = string.Empty;
			
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_DiskDrive");
			
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query)) 
			{				
				foreach (ManagementObject process in searcher.Get()) 
				{
					process.Get();					
					result = process["SerialNumber"].ToString();
				}
			 }
			
			return result;
		}
		
		public void GetSystemInfo() {
			
			
		}
	}
}
