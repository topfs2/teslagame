// Check.cs created with MonoDevelop
// User: topfs at 9:45 PM 12/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	
	
	public class Check
	{
		static int succeded = 0;
		static int failed = 0;
		public Check()
		{
		}
		
		public static void AssertEquals(object o1, object o2)
		{
			AssertEquals("", o1, o2);
		}
	
		public static void AssertEquals(String s, object o1, object o2)
		{
			if (o1.Equals(o2))
			{
				s = "Sucess" + (s.Length > 0 ? ":\t" : "\t") + s + " => " + o1.ToString();
				succeded++;
			}
			else
			{
				s = "Failed" + (s.Length > 0 ? ":\t" : "\t") + s + " => " + o1.ToString() + " != " + o2.ToString();
				failed++;
			}
				
			System.Console.Out.WriteLine(s);
		}
		
		public static int getSucceded()
		{
			return succeded;
		}
		
		public static int getFailed()
		{
			return failed;
		}
		
		public static void reset()
		{
			succeded = failed = 0;
		}
	}
}
