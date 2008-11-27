// SimpleCollisionPlane.cs created with MonoDevelop
// User: topfs at 11:01 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public class SimpleCollisionPlane : CollisionSurface
	{
		float y, friction, thickness;
		bool active;
		public SimpleCollisionPlane(float y, float friction, float thickness)
		{
			active = true;
			this.y = y;
			this.friction = friction;
			this.thickness = thickness;
			active = true;
		}
		
		public bool getActive()
		{
			return true;
		}
		
		public bool setActive(bool activate)
		{
			return (this.active = active);
		}
		
		public bool collisionDetect (Point3f pointA, Point3f pointB)
		{   // need thickness
			return (pointA.y < (y - thickness) && pointB.y > (y - thickness)) || (pointA.y > (y + thickness) && pointB.y < (y + thickness));
		}

		public Point3f computeTrajectory (Point3f vector)
		{
			Point3f newTrajectory = new Point3f(vector.x, 0 - vector.y, vector.z);
			newTrajectory.stretch(friction);
			
			return newTrajectory;
		}
	}
}
