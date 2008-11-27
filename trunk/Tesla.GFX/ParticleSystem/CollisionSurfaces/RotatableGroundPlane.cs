// RotatableCollisionGround.cs created with MonoDevelop
// User: topfs at 10:42 AM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class RotatableGroundPlane : CollisionSurface
	{
		bool active;
		Point3f position, normal;
		float deltaY_X, deltaY_Z, friction;
		
		public RotatableGroundPlane(Point3f position, float deltaY_X, float deltaY_Z, float friction)
		{
			active = true;
			this.friction = friction;
			this.position = position;
			this.deltaY_X = deltaY_X;
			this.deltaY_Z = deltaY_Z;
			Point3f vectorA = new Point3f(1.0f, -deltaY_X, 0.0f);
			Point3f vectorB = new Point3f(0.0f, -deltaY_Z, 1.0f);
			normal = vectorA.Cross(vectorB);
			normal.Normalize();
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
			return  getHeight(pointB.x, pointB.z) > pointB.y;
		}
		
		private float getHeight(float x, float z)
		{
			return ((x - position.x) * deltaY_X + (z - position.z) * deltaY_Z) - position.y;
		}
		
		public Point3f computeTrajectory (Point3f vector)
		{
			float len = vector.length();
			Point3f newTrajectory = vector.copy();

			float projection = (vector * normal) / (normal * normal);

			Point3f u = normal * projection;
			
			newTrajectory.subtract(u).subtract(u);
			
			newTrajectory.stretch(friction);
			
			//Console.Out.WriteLine("Got: " + vector.ToString() + " computed: " + newTrajectory.ToString());
			
			return newTrajectory;
		}
	}
}
