// point3f.cs created with MonoDevelop
// User: topfs at 12:26 PM 9/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{	
	public class Vector3f
	{
		public float x, y, z;
		
		public Vector3f(double x, double y, double z)
		{
			set((float)x, (float)y, (float)z);
		}
		
		public Vector3f(float x, float y, float z)
		{
			set(x, y, z);
		}

		public float[] vector
		{
			get { return new float[]{x, y, z}; }
		}
		
		public Vector3f subtract(Vector3f dest)
		{
			x -= dest.x;
			y -= dest.y;
			z -= dest.z;
			return this;
		}
		
		public Vector3f add(Vector3f dest)
		{
			x += dest.x;
			y += dest.y;
			z += dest.z;
			return this;
		}
		
		public Vector3f divides(Vector3f lower)
		{
			if (lower.x == 0.0f || lower.y == 0.0f || lower.z == 0.0f)
				throw new DivideByZeroException("Can´t divide with zero");
			x /= lower.x;
			y /= lower.y;
			z /= lower.z;
			return this;
		}
		
		public Vector3f diff(Vector3f dst)
		{
			return new Vector3f(x - dst.x, y - dst.y, z - dst.z);
		}
		
		public Vector3f divides(float lower)
		{
			if (lower == 0.0f)
				throw new DivideByZeroException("Can't divide with zero");
			x /= lower;
			y /= lower;
			z /= lower;
			return this;
		}
		
		public Vector3f stretch(float multiply)
		{
			x *= multiply;
			y *= multiply;
			z *= multiply;
			return this;
		}
		
		public Vector3f transform(float[] matrix)
		{
			if (matrix.Length != 16)
				throw new Exception("Can´t transform without a 4x4 matrix");
			
			/*Point3f r = new Point3f(x * matrix[0] + y * matrix[1] + z * matrix[2],
									x * matrix[4] + y * matrix[5] + z * matrix[6],
									x * matrix[8] + y * matrix[9] + z * matrix[10]);*/

			Vector3f r = new Vector3f(x * matrix[0] + y * matrix[4] + z * matrix[8] + matrix[12],
									x * matrix[1] + y * matrix[5] + z * matrix[9] + matrix[13],
									x * matrix[2] + y * matrix[6] + z * matrix[10]+ matrix[14]);							
			return r;
		}
		
		public static Vector3f operator +(Vector3f v1, Vector3f v2)
		{
			return v1.copy().add(v2);
		}
		
		public static Vector3f operator -(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}
		
		public static Vector3f operator *(Vector3f v1, float f)
		{
			return new Vector3f(v1.x * f, v1.y * f, v1.z * f);
		}
		
		public static Vector3f operator *(float f, Vector3f v1)
		{
			return v1 * f;
		}
		
		public static float operator *(Vector3f v1, Vector3f v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}
		
		public Vector3f stretch(Vector3f multiply)
		{
			x *= multiply.x;
			y *= multiply.y;
			z *= multiply.z;
			return this;
		}
		
		public float length(Vector3f dst)
		{
			Vector3f delta = new Vector3f(x - dst.x, y - dst.y, z - dst.z);
			
			return delta.length();
		}
		
		public float length2(Vector3f dst)
		{
			Vector3f delta = new Vector3f(x - dst.x, y - dst.y, z - dst.z);
			
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
		
		public Vector3f Normalize()
		{
			return divides(length());
		}
		
		public void set(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public void set(Vector3f src)
		{
			set(src.x, src.y, src.z);
		}
		
		public void invert()
		{
			set(-x, -y, -z);
		}
		
		public Vector3f copy()
		{
			return new Vector3f(x, y, z);
		}
		
		public Vector3f Cross(Vector3f b)
		{
//			Consider two vectors, a = (1,2,3) and b = (4,5,6). The cross product a × b is
//          a × b = (1,2,3) × (4,5,6) = ((2×6 - 3×5), (3×4 - 1×6), (1×5 - 2×4)) = (-3,6,-3). 
			float u1, u2, u3;
			u1 = y * b.z - z * b.y;
			u2 = z * b.x - x * b.z;
			u3 = x * b.y - y * b.x;

			return new Vector3f(u1, u2, u3);
		}
		
		public static void Lerp(ref Vector3f value1, ref Vector3f value2, float amount, out Vector3f result)
		{
			result = value1 + (value2 - value1) * amount;
		}
		
		public static Vector3f Lerp(ref Vector3f value1, ref Vector3f value2, float amount)
		{
			return value1 + (value2 - value1) * amount;
		}

/* Overrides */
		public override String ToString()
		{
			return "[" + x + ", " + y + ", " + z +"]";
		}
		
		public override bool Equals(object o)
		{
			Vector3f v = (Vector3f)o;
			
			if (x == v.x && y == v.y && z == v.z)
				return true;
			else
				return false;
		}

        public override int GetHashCode()
        {
            float[] points = new float[] {x, y, z};
            return points.GetHashCode();
        }
	}
}
