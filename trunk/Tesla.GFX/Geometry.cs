// Geometry.cs created with MonoDevelop
// User: topfs at 8:16 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public interface Geometry
	{
		float getHeight(float x, float z);
		Point3f getNormal(float x, float z);
		int   maximumX();
		int   maximumZ();
	}
}
