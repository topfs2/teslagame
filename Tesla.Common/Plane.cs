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
		
		public Plane(Vector3f p0, Vector3f p0A, Vector3f p0B)
		{
			Vector3f vecA = p0A - p0;
			Vector3f vecB = p0B - p0;
			Vector3f normal = vecA.Cross(vecB);
			
			a = normal.x;
			b = normal.y;
			c = normal.z;
			d = -1.0f * (normal * p0);
		}
		
		public Plane(Vector3f normal, Vector3f p)
		{
			a = normal.x;
			b = normal.y;
			c = normal.z;
			d = -1.0f * (normal * p);
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
			return d;
		}
		
		public float distanceTo2(Vector3f p)
		{
			float d = distanceTo(p);
			return d*d;
		}
		
		public float distanceToABS(Vector3f p)
		{
			return (float)Math.Abs(distanceTo(p));
		}
		
		public float getY(float x, float z)
		{
			return ((-1.0f * a * x) + d + (-1.0f * c * z)) / b; 
		}
		
		public Vector3f getNormal()
		{
			return new Vector3f(a, b, c).Normalize();
		}
		
		public override String ToString()
		{
			return a.ToString() + "x + " + b.ToString() + "y + " + c.ToString() + "z + " + d.ToString() + " = 0";
		}
		
		public override bool Equals (object o)
		{
			Plane p = (Plane)o;
			return p.a == a && p.b == b && p.c == c && p.d == d; 
		}

        public override int GetHashCode()
        {
            float[] points = new float[] { a, b, c, d };
            return points.GetHashCode();
        }
		
		public static void test()
		{
			stdTest("Create Plane with a, b, c, d", new Plane(0, 1, 0, 0));
			stdTest("Create Plane with Normal and point", new Plane(new Vector3f(0, 1, 0), new Vector3f(0, 0, 0)));
			stdTest("Create Plane with Normal and another point", new Plane(new Vector3f(0, 1, 0), new Vector3f(30, 0, 20)));
			stdTest("Create Plane with 3 vectors", new Plane(new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(1.0f, 0.0f, 0.0f), new Vector3f(0.0f, 0.0f, -1.0f)));
			
			Check.AssertEquals(new Plane(0, 0, 0, 0), new Plane(0, 0, 0, 0));
			
			Vector3f v = new Vector3f(1.0f, 1.0f, 1.0f).Normalize();
			Plane p = new Plane(v.x, v.y, v.z, 0);
			Check.AssertEquals(p.ToString(), p.distanceTo(new Vector3f(0.0f, 0.0f, 0.0f)), 0.0f);
			p = new Plane(v.x, v.y, v.z, 2);
			Check.AssertEquals(p.ToString(), p.distanceTo(new Vector3f(0.0f, 0.0f, 0.0f)).ToString(), 2.0f.ToString());
			p = new Plane(v.x, v.y, v.z, -2);
			Check.AssertEquals(p.ToString(), p.distanceTo(new Vector3f(0.0f, 0.0f, 0.0f)).ToString(), (-2.0f).ToString());
			
			p = new Plane(new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f( 10.0f, -10.0f, 10.0f), new Vector3f(5.0f, 10.0f, 10.0f));

			Check.AssertEquals(p.distanceTo(new Vector3f(0.0f, 0.0f, 0.0f)), 0.0f);
			Check.AssertEquals(p.distanceTo(new Vector3f(5.0f, 10.0f, 10.0f)), 0.0f);
			Check.AssertEquals(p.distanceTo(new Vector3f(10.0f, -10.0f, 10.0f)), 0.0f);
			Check.AssertEquals(p.distanceTo(new Vector3f(0.0f, 0.0f, 5.0f)) >= 0 , true);
		}
		
		private static void stdTest(string s, Plane p)
		{
			Check.AssertEquals(s, p.distanceTo(new Vector3f(0.0f, 2.0f, 0.0f)), 2.0f);
			Check.AssertEquals(s, p.distanceTo(new Vector3f(0.0f, 0.0f, 0.0f)), 0.0f);
			Check.AssertEquals(s, p.distanceTo(new Vector3f(0.0f, -2.0f, 15.0f)), -2.0f);	
		}
	}
}
