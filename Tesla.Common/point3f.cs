// point3f.cs created with MonoDevelop
// User: topfs at 12:26 PM 9/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{	
	public class Point3f
	{
		public float x, y, z;
		
		public Point3f(double x, double y, double z)
		{
			set((float)x, (float)y, (float)z);
		}
		
		public Point3f(float x, float y, float z)
		{
			set(x, y, z);
		}

		public float[] vector
		{
			get { return new float[]{x, y, z}; }
		}
		
		public Point3f subtract(Point3f dest)
		{
			x -= dest.x;
			y -= dest.y;
			z -= dest.z;
			return this;
		}
		
		public Point3f add(Point3f dest)
		{
			x += dest.x;
			y += dest.y;
			z += dest.z;
			return this;
		}
		
		public Point3f divides(Point3f lower)
		{
			x /= lower.x;
			y /= lower.y;
			z /= lower.z;
			return this;
		}
		
		public Point3f diff(Point3f dst)
		{
			return new Point3f(x - dst.x, y - dst.y, z - dst.z);
		}
		
		public Point3f divides(float lower)
		{
			if (lower == 0.0f)
				throw new Exception("Can´t divide with zero");
			x /= lower;
			y /= lower;
			z /= lower;
			return this;
		}
		
		public Point3f stretch(float multiply)
		{
			x *= multiply;
			y *= multiply;
			z *= multiply;
			return this;
		}
		
		public Point3f transform(float[] matrix)
		{
			if (matrix.Length != 16)
				throw new Exception("Can´t transform without a 4x4 matrix");
			
			/*Point3f r = new Point3f(x * matrix[0] + y * matrix[1] + z * matrix[2],
									x * matrix[4] + y * matrix[5] + z * matrix[6],
									x * matrix[8] + y * matrix[9] + z * matrix[10]);*/

			Point3f r = new Point3f(x * matrix[0] + y * matrix[4] + z * matrix[8] + matrix[12],
									x * matrix[1] + y * matrix[5] + z * matrix[9] + matrix[13],
									x * matrix[2] + y * matrix[6] + z * matrix[10]+ matrix[14]);							
			return r;
		}
		
		public static Point3f operator +(Point3f v1, Point3f v2)
		{
			return v1.copy().add(v2);
		}
		
		public static Point3f operator -(Point3f v1, Point3f v2)
		{
			return new Point3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}
		
		public static Point3f operator *(Point3f v1, float f)
		{
			return new Point3f(v1.x * f, v1.y * f, v1.z * f);
		}
		
		public static float operator *(Point3f v1, Point3f v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}
		
		public Point3f stretch(Point3f multiply)
		{
			x *= multiply.x;
			y *= multiply.y;
			z *= multiply.z;
			return this;
		}
		
		public float length(Point3f dst)
		{
			Point3f delta = new Point3f(x - dst.x, y - dst.y, z - dst.z);
			
			return delta.length();
		}
		
		public float length2(Point3f dst)
		{
			Point3f delta = new Point3f(x - dst.x, y - dst.y, z - dst.z);
			
			return delta.length2();
		}
		
		public float length2()
		{
			return x * x + y * y + z * z;
		}
		
		public float length()
		{
			return (float)System.Math.Sqrt(length2());
		}
		
		public Point3f Normalize()
		{			
			return divides(length());
		}
		
		public void set(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public void set(Point3f src)
		{
			set(src.x, src.y, src.z);
		}
		
		public void invert()
		{
			set(-x, -y, -z);
		}
		
		public Point3f copy()
		{
			return new Point3f(x, y, z);
		}
		public Point3f Cross(Point3f b)
		{
//			Consider two vectors, a = (1,2,3) and b = (4,5,6). The cross product a × b is
//          a × b = (1,2,3) × (4,5,6) = ((2×6 - 3×5), (3×4 - 1×6), (1×5 - 2×4)) = (-3,6,-3). 
			float u1, u2, u3;
			u1 = y * b.z - z * b.y;
			u2 = x * b.z - z * b.x;
			u3 = x * b.y - y * b.x;

			return new Point3f(u1, 0-u2, u3);
		}
		
		public String ToString()
		{
			return "[" + x + ", " + y + ", " + z +"]";
		}
	}
}
