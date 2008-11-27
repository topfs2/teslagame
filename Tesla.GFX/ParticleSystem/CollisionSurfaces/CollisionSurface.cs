// CollisionSurface.cs created with MonoDevelop
// User: topfs at 11:03 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public interface CollisionSurface : Controller
	{
		bool collisionDetect(Point3f pointA, Point3f pointB);
		Point3f computeTrajectory(Point3f vector);
	}
}
