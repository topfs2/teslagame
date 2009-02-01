// Vector3fTest.cs created with MonoDevelop
// User: topfs at 9:25 AM 2/1/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using NUnit.Framework;

namespace Tesla.Common
{
	
	
	[TestFixture()]
	public class Vector3fTest
	{
		private Vector3f v0, v1;
				
		[Test()]
		public void overrideEquals()
		{
			Assert.AreEqual(new Vector3f(1.0f, 0.0f, 0.0f), new Vector3f(1.0f, 0.0f, 0.0f));
		}
		
		[Test()]
		public void normalize()
		{
			Assert.AreEqual(new Vector3f(2.0f, 0.0f, 0.0f).Normalize(), new Vector3f(1.0f, 0.0f, 0.0f));
		}
		
		[Test()]
		public void addition()
		{
			Assert.AreEqual(new Vector3f(1.0f, 0.0f, 2.0f) + new Vector3f(-1.0f, 2.0f, 2.0f) , new Vector3f(0.0f, 2.0f, 4.0f));
		}
		
		[Test()]
		public void crossProduct()
		{
			v0 = new Vector3f(3.0f, 0.0f, 0.0f);
			v1 = new Vector3f(0.0f, 2.0f, 0.0f);
			Assert.AreEqual(v0.Cross(v1), new Vector3f(0.0f, 0.0f, 6.0f), v0.ToString() + "x" + v1.ToString());
		}
		
		[Test()]
		public void crossProductWithZeros()
		{
			v0 = new Vector3f(1.0f, 0.0f, 0.0f);
			v1 = new Vector3f(0.0f, 0.0f, -1.0f);
			Assert.AreEqual(v0.Cross(v1), new Vector3f(0.0f, 1.0f, 0.0f), v0.ToString() + "x" + v1.ToString());
		}
		
		[Test()]
		public void multiplication()
		{
			Assert.AreEqual(new Vector3f(1.0f, 1.0f, 1.0f) * 2.0f, new Vector3f(2.0f, 2.0f, 2.0f));
		}
		
		[Test()]
		public void scalarProduct()
		{
			Assert.AreEqual(new Vector3f(1.0f, 1.0f, 1.0f) * new Vector3f(0.0f, 0.0f, 0.0f), 0.0f);
		}
	}
}
