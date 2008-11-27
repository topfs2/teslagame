// Quaternion.cs created with MonoDevelop
// User: topfs at 3:40 PM 11/7/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	
	
	public class Quaternion
	{
		float w, x, y, z;
		public Quaternion()
		{
			w = x = y = z = 0.0f;
		}
		
		public Quaternion(float w, float x, float y, float z)
		{
			this.w = w;
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public Quaternion(bool radians, float angle, float x, float y, float z)
		{
			if (!radians)
				angle = (angle / 180.0f) * (float)Math.PI;
				
			float result = (float)Math.Sin(angle / 2.0f);
			
			w = (float)Math.Cos(angle / 2.0f);
			this.x = x * result;
			this.x = y * result;
			this.x = z * result;
		}
		
		public void createMatrix(float[] matrix)
		{
			// Make sure the matrix has allocated memory to store the rotation data
			if(matrix.Length != 16)
				throw new Exception("Can´t create anything besides 4x4 matrix from a quaternion");
			
			// First row
			matrix[ 0] = 1.0f - 2.0f * ( y * y + z * z );
			matrix[ 1] = 2.0f * (x * y + z * w);
			matrix[ 2] = 2.0f * (x * z - y * w);
			matrix[ 3] = 0.0f;
			
			// Second row
			matrix[ 4] = 2.0f * ( x * y - z * w );
			matrix[ 5] = 1.0f - 2.0f * ( x * x + z * z );
			matrix[ 6] = 2.0f * (z * y + x * w );
			matrix[ 7] = 0.0f;

			// Third row
			matrix[ 8] = 2.0f * ( x * z + y * w );
			matrix[ 9] = 2.0f * ( y * z - x * w );
			matrix[10] = 1.0f - 2.0f * ( x * x + y * y );
			matrix[11] = 0.0f;

			// Fourth row
			matrix[12] = 0;
			matrix[13] = 0;
			matrix[14] = 0;
			matrix[15] = 1.0f;

			// Now pMatrix[] is a 4x4 homogeneous matrix that can be applied to an OpenGL Matrix
		}
		public static Quaternion operator *(Quaternion q1, Quaternion q2)

		//public static Quaternion operator* (Quaternion q)
		{
			Quaternion r = new Quaternion();
			
			r.w = q1.w*q2.w - q1.x*q2.x - q1.y*q2.y - q1.z*q2.z;
			r.x = q1.w*q2.x + q1.x*q2.w + q1.y*q2.z - q1.z*q2.y;
			r.y = q1.w*q2.y + q1.y*q2.w + q1.z*q2.x - q1.x*q2.z;
			r.z = q1.w*q2.z + q1.z*q2.w + q1.x*q2.y - q1.y*q2.x;

			Console.Out.WriteLine(q1.ToString() + " * " + q2.ToString() + " = " + r.ToString());

			return r;
		}
		
		public string ToString()
		{
			return "[" + w + ", " + x + ", " + y + ", " + z + "]"; 
		}
	}
}
