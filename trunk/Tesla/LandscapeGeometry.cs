// LandscapeGeometry.cs created with MonoDevelop
// User: topfs at 8:17 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
using Tesla;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class LandscapeGeometry : CollisionSurface, Geometry 
	{
		private Bitmap data;
		public LandscapeGeometry(String bitmap)
		{
			data = new Bitmap(bitmap);
		}
		
		public bool getActive()
		{
			return true;
		}
		
		public bool setActive(bool activate)
		{
			return true;
		}

		public float getHeight (float x, float z)
		{
			if (x < 0 || z < 0 || x > maximumX() || z > maximumZ())
				return 0.0f;
			else
				return (data.GetPixel((int)x, (int)z).R / 16.0f);
		}
		
		public Point3f getNormal(float x, float z)
		{
			int x0 = (int)x;
			int x1 = x0 + 1;
			int z0 = (int)z;
			int z1 = z0 + 1;
			
			float y00, y01, y10;
			y00 = getHeight(x0, z0);
			y01 = getHeight(x0, z1);
			y10 = getHeight(x1, z0);
			
			Point3f a = new Point3f(0.0f, y00 - y01, 1.0f);
			Point3f b = new Point3f(1.0f, y00 - y10, 0.0f);
			
			return a.Cross(b);
		}

		public int maximumX ()
		{
			return data.Width;
		}

		public int maximumZ ()
		{
			return data.Height;
		}

		public bool collisionDetect (Point3f pointA, Point3f pointB)
		{
			return (pointB.y < getHeight(pointB.x, pointB.z)); 
		}

		public Point3f computeTrajectory (Point3f vector)
		{
			Point3f newTrajectory = new Point3f(vector.x, 0 - vector.y, vector.z);
			newTrajectory.stretch(0.7f);

			return newTrajectory;
		}
	}
}
