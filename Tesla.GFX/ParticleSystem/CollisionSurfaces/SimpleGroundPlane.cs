// SimpleGroundPlane.cs created with MonoDevelop
// User: topfs at 11:51 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class SimpleGroundPlane : CollisionSurface
	{
		bool active;		
		float y, friction;
		public SimpleGroundPlane(float y, float friction)
		{
			active = true;
			this.y = y;
			this.friction = friction;
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
		{
			return pointB.y < y;
		}

		public Point3f computeTrajectory (Point3f vector)
		{
			Point3f newTrajectory = new Point3f(vector.x, 0 - vector.y, vector.z);
			newTrajectory.stretch(friction);

			return newTrajectory;			
		}
	}
}
