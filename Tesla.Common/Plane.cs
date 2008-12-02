// Plane.cs created with MonoDevelop
// User: topfs at 7:34 PM 11/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	
	
	public class Plane
	{
		public float a, b, c, d;	
		
		public Plane(Vector3f p0, Vector3f p1, Vector3f p2)
		{
			Vector3f vecA = p1 - p0;
			Vector3f vecB = p2 - p0;
			Vector3f normal = vecA.Cross(vecB);
			normal.Normalize();
			
			a = normal.x;
			b = normal.y;
			c = normal.z;
			d = -1.0f * (normal * p0);
			
			//Console.Out.WriteLine(a + "x + " + b + "y + " + c + "z + " + d);
		}
		
		public Plane(Vector3f normal, Vector3f p)
		{
			a = normal.x;
			b = normal.y;
			c = normal.z;
			d = -1.0f * (normal * p);
			
			//Console.Out.WriteLine(a + "x + " + b + "y + " + c + "z + " + d);
		}
		
		public Plane(float a, float b, float c, float d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}
		
		public float distanceTo(Vector3f p)
		{
			Vector3f plane = new Vector3f(a, b, c);
			float d = (p * plane + this.d) / plane.length();
			Console.Out.WriteLine(d);
			return d;
		}
		
		public float distanceToABS(Vector3f p)
		{
			return (float)Math.Abs(distanceTo(p));
		}
		
		public float getY(float x, float z)
		{
			return ((-1.0f * a * x) + d + (-1.0f * c * z)) / b; 
		}
	}
}