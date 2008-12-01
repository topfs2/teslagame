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
		bool collisionDetect(Vector3f pointA, Vector3f pointB);
		Vector3f computeTrajectory(Vector3f vector);
	}
}
