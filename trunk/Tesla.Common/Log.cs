// Log.cs created with MonoDevelop
// User: topfs at 11:15 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	public enum LogType
	{
		Error,
		CriticalError,
		Warning,
		Notice,
		Debug,
		Info
	}
	
	public class Log
	{
		
		public Log()
		{
		}
		
		public static void Write(string text)
		{
			Write(text, LogType.Debug);
		}
		
		public static void Write(string text, LogType type)
		{
			string Type = "";
			switch(type)
			{
			case LogType.Error:
				Type = "Error: ";
				break;
			case LogType.CriticalError:
				Type = "Critical: ";
				break;
			case LogType.Notice:
				Type = "Notice: ";
				break;
			case LogType.Info:
				Type = "Info: ";
				break;
			case LogType.Warning:
				Type = "Warning: ";
				break;
			case LogType.Debug:
				Type = "Debug: ";
				break;
			default:
				Type = "Default: ";
				break;
			}
			
			System.Console.WriteLine(Type + text);
		}
	}
}
