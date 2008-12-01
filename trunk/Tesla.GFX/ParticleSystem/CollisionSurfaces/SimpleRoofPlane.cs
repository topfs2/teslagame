// SimpleGroundPlane.cs created with MonoDevelop
// User: topfs at 11:51 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class SimpleRoofPlane : CollisionSurface
	{
		bool active;		
		float y, friction;
		public SimpleRoofPlane(float y, float friction)
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
		
		public bool collisionDetect (Vector3f pointA, Vector3f pointB)
		{
			return (pointA.y < y && pointB.y > y);
		}

		public Vector3f computeTrajectory (Vector3f vector)
		{
			Vector3f newTrajectory = new Vector3f(vector.x, 0 - vector.y, vector.z);
			newTrajectory.stretch(friction);

			return newTrajectory;
		}
	}
}
