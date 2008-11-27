// PlainGeometry.cs created with MonoDevelop
// User: topfs at 6:59 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class PlainGeometry : Geometry
	{
		
		public PlainGeometry()
		{
		}

		public float getHeight (float x, float z)
		{
			return 0.0f;
		}

		public Point3f getNormal (float x, float z)
		{
			return new Point3f(0.0f, 1.0f, 0.0f);
		}

		public int maximumX ()
		{
			return 128;
		}

		public int maximumZ ()
		{
			return 128;
		}
	}
}
