// FPSCounterTest.cs created with MonoDevelop
// User: topfs at 1:53 PM 2/4/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using NUnit.Framework;

namespace Tesla
{
	
	
	[TestFixture()]
	public class FPSCounterTest
	{
		
		[Test()]
		public void TestNewlyCreated()
		{
			FPSCounter frameCounter = new FPSCounter();
			Assert.AreEqual(0, frameCounter.CurrentFPS);
		}
		
		[Test()]
		public void TestUpdateLong()
		{
			FPSCounter frameCounter = new FPSCounter();
			System.Threading.Thread.Sleep(2500);
			Assert.AreEqual(2.5f, Math.Round(frameCounter.Update(), 1));
		}
		
		[Test()]
		public void TestUpdateSeveralSmall()
		{
			FPSCounter frameCounter = new FPSCounter();
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep(100);
				Assert.AreEqual(0.1f, Math.Round(frameCounter.Update(), 1));
			}
		}
		
		[Test()]
		public void TestFPSOnSmall()
		{
			FPSCounter frameCounter = new FPSCounter();
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep(200);
				frameCounter.Update();
			}
			
			Assert.AreEqual(5, frameCounter.CurrentFPS);
		}
	}
}
