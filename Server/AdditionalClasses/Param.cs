
using System;

namespace Server.AdditionalClasses
{
	// класс, как тип название параметра и значение (пример: 
	// Model: Asus)
	public class Param
	{
		public Param(string param, string val)
		{
			ParamName = param;
			ParamValue = val;
		}
		
		public string ParamName { get; set; }
		public string ParamValue { get; set; }
	}
}
