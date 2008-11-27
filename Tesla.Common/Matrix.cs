// Matrix.cs created with MonoDevelop
// User: topfs at 7:56 PM 11/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	
	
	public class Matrix
	{
		float [,] m;
		
		public Matrix(float [,] m)
		{
			this.m = m;
		}
		
		public float determinant()
		{
			if (m.Length != 9)
				throw new NotImplementedException("Can only do the determinant on 3x3 matrix");
			float a = m[0,0] * m[1,1] * m[2,2] + m[1,0] * m[2,1] * m[0,2] + m[2,0] * m[0,1] * m[1,2];
			float b = m[0,2] * m[1,1] * m[2,0] + m[1,2] * m[2,1] * m[0,0] + m[2,2] * m[0,1] * m[1,0];
			
			return a - b;
		}
	}
}
