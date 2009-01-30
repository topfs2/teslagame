// Matrix44.cs created with MonoDevelop
// User: topfs at 8:10 PM 1/30/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	public class Matrix44
	{
		float [,] src;
		
		public Matrix44()
		{
			src = new float[4,4];
		}
		
		private Matrix44(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.GetLength(0) == 4 && matrix.GetLength(1) == 4)
				src = matrix;
			else
				throw new NotSupportedException("input matrix must be 4x4");
		}
		
		public Matrix44(float[] vector)
		{
			if (vector.Length != 16)
				throw new NotSupportedException("input vector must be 16 of length");
			
			src = new float[4, 4];
			
			for (int i = 0; i < 16; i++)
				src[i / 4, i % 4] = vector[i];
		}
		
		public void setValue(int i, int j, float val)
		{
			if (i > 4 || j > 4)
				throw new NotSupportedException("Can´t set [" + i + "," + j + "] in a 4x4 matrix");
			
			src[i,j] = val;
		}
		
		public float getValue(int i, int j)
		{
			if (i > 4 || j > 4)
				throw new NotSupportedException("Can´t get [" + i + "," + j + "] in a 4x4 matrix");
			
			return src[i,j];
		}
		
		private void swapRows(ref float []a, ref float[]b)
		{
			float []tmp = a;
			a = b;
			b = tmp;
		}
		

		
		public Matrix44 invert()
		{
			float[,] dst = new float[4,4];

			//float [,] wtmp = new float[4,8];
			float m0, m1, m2, m3, s;
			float [] r0, r1, r2, r3;
			
			//r0 = wtmp[0]; r1 = wtmp[1]; r2 = wtmp[2]; r3 = wtmp[3];
			r0 = new float[8];
			r1 = new float[8];
			r2 = new float[8];
			r3 = new float[8];

			r0[0] = src[0,0]; r0[1] = src[0,1];
			r0[2] = src[0,2]; r0[3] = src[0,3];
			r0[4] = 1.0f; r0[5] = r0[6] = r0[7] = 0.0f;

			r1[0] = src[1,0]; r1[1] = src[1,1];
			r1[2] = src[1,2]; r1[3] = src[1,3];
			r1[5] = 1.0f; r1[4] = r1[6] = r1[7] = 0.0f;

			r2[0] = src[2,0]; r2[1] = src[2,1];
			r2[2] = src[2,2]; r2[3] = src[2,3];
			r2[6] = 1.0f; r2[4] = r2[5] = r2[7] = 0.0f;

			r3[0] = src[3,0]; r3[1] = src[3,1];
			r3[2] = src[3,2]; r3[3] = src[3,3];
			r3[7] = 1.0f; r3[4] = r3[5] = r3[6] = 0.0f;

			/* choose pivot - or die */
			if ((float)Math.Abs(r3[0])>(float)Math.Abs(r2[0])) swapRows(ref r3, ref r2);
			if ((float)Math.Abs(r2[0])>(float)Math.Abs(r1[0])) swapRows(ref r2, ref r1);
			if ((float)Math.Abs(r1[0])>(float)Math.Abs(r0[0])) swapRows(ref r1, ref r0);
			if (0.0 == r0[0])  throw new Exception();

			/* eliminate first variable     */
			m1 = r1[0]/r0[0]; m2 = r2[0]/r0[0]; m3 = r3[0]/r0[0];
			s = r0[1]; r1[1] -= m1 * s; r2[1] -= m2 * s; r3[1] -= m3 * s;
			s = r0[2]; r1[2] -= m1 * s; r2[2] -= m2 * s; r3[2] -= m3 * s;
			s = r0[3]; r1[3] -= m1 * s; r2[3] -= m2 * s; r3[3] -= m3 * s;
			s = r0[4];
			if (s != 0.0) { r1[4] -= m1 * s; r2[4] -= m2 * s; r3[4] -= m3 * s; }
			s = r0[5];
			if (s != 0.0) { r1[5] -= m1 * s; r2[5] -= m2 * s; r3[5] -= m3 * s; }
			s = r0[6];
			if (s != 0.0) { r1[6] -= m1 * s; r2[6] -= m2 * s; r3[6] -= m3 * s; }
			s = r0[7];
			if (s != 0.0) { r1[7] -= m1 * s; r2[7] -= m2 * s; r3[7] -= m3 * s; }

			/* choose pivot - or die */
			if ((float)Math.Abs(r3[1])>(float)Math.Abs(r2[1])) swapRows(ref r3, ref r2);
			if ((float)Math.Abs(r2[1])>(float)Math.Abs(r1[1])) swapRows(ref r2, ref r1);
			if (0.0 == r1[1])  throw new Exception();

			/* eliminate second variable */
			m2 = r2[1]/r1[1]; m3 = r3[1]/r1[1];
			r2[2] -= m2 * r1[2]; r3[2] -= m3 * r1[2];
			r2[3] -= m2 * r1[3]; r3[3] -= m3 * r1[3];
			s = r1[4]; if (0.0 != s) { r2[4] -= m2 * s; r3[4] -= m3 * s; }
			s = r1[5]; if (0.0 != s) { r2[5] -= m2 * s; r3[5] -= m3 * s; }
			s = r1[6]; if (0.0 != s) { r2[6] -= m2 * s; r3[6] -= m3 * s; }
			s = r1[7]; if (0.0 != s) { r2[7] -= m2 * s; r3[7] -= m3 * s; }

			/* choose pivot - or die */
			if ((float)Math.Abs(r3[2])>(float)Math.Abs(r2[2])) swapRows(ref r3, ref r2);
			if (0.0 == r2[2])  throw new Exception();

			/* eliminate third variable */
			m3 = r3[2]/r2[2];
			r3[3] -= m3 * r2[3]; r3[4] -= m3 * r2[4];
			r3[5] -= m3 * r2[5]; r3[6] -= m3 * r2[6];
			r3[7] -= m3 * r2[7];

			/* last check */
			if (0.0 == r3[3]) throw new Exception();

			s = 1.0f/r3[3];              /* now back substitute row 3 */
			r3[4] *= s; r3[5] *= s; r3[6] *= s; r3[7] *= s;

			m2 = r2[3];                 /* now back substitute row 2 */
			s  = 1.0f/r2[2];
			r2[4] = s * (r2[4] - r3[4] * m2); r2[5] = s * (r2[5] - r3[5] * m2);
			r2[6] = s * (r2[6] - r3[6] * m2); r2[7] = s * (r2[7] - r3[7] * m2);
			m1 = r1[3];
			r1[4] -= r3[4] * m1; r1[5] -= r3[5] * m1;
			r1[6] -= r3[6] * m1; r1[7] -= r3[7] * m1;
			m0 = r0[3];
			r0[4] -= r3[4] * m0; r0[5] -= r3[5] * m0;
			r0[6] -= r3[6] * m0; r0[7] -= r3[7] * m0;

			m1 = r1[2];                 /* now back substitute row 1 */
			s  = 1.0f/r1[1];
			r1[4] = s * (r1[4] - r2[4] * m1); r1[5] = s * (r1[5] - r2[5] * m1);
			r1[6] = s * (r1[6] - r2[6] * m1); r1[7] = s * (r1[7] - r2[7] * m1);
			m0 = r0[2];
			r0[4] -= r2[4] * m0; r0[5] -= r2[5] * m0;
			r0[6] -= r2[6] * m0; r0[7] -= r2[7] * m0;

			m0 = r0[1];                 /* now back substitute row 0 */
			s  = 1.0f/r0[0];
			r0[4] = s * (r0[4] - r1[4] * m0); r0[5] = s * (r0[5] - r1[5] * m0);
			r0[6] = s * (r0[6] - r1[6] * m0); r0[7] = s * (r0[7] - r1[7] * m0);

			dst[0,0] = r0[4]; dst[0,1] = r0[5];
			dst[0,2] = r0[6]; dst[0,3] = r0[7];
			dst[1,0] = r1[4]; dst[1,1] = r1[5];
			dst[1,2] = r1[6]; dst[1,3] = r1[7];
			dst[2,0] = r2[4]; dst[2,1] = r2[5];
			dst[2,2] = r2[6]; dst[2,3] = r2[7];
			dst[3,0] = r3[4]; dst[3,1] = r3[5];
			dst[3,2] = r3[6]; dst[3,3] = r3[7]; 

			return new Matrix44(dst);		
		}
		
		public float[] getVector()
		{
			float[] vector = new float[16];
			
			for (int i = 0; i < 16; i++)
				vector[i] = src[i / 4, i % 4];
				
			return vector;
		}
		
		public static void test()
		{
			Matrix44 m = new Matrix44();
			
			m.setValue(0, 0, 1.0f); m.setValue(1, 1, 1.0f); m.setValue(2, 2, 1.0f); m.setValue(3, 3, 1.0f);
			
			float[] o1 = {	1.0f, 0.0f, 0.0f, 0.0f,
							0.0f, 1.0f, 0.0f, 0.0f,
							0.0f, 0.0f, 1.0f, 0.0f,
							0.0f, 0.0f, 0.0f, 1.0f	};
			
			float[] o2 = m.getVector();
			
			for (int i = 0; i < 16; i++)
				Check.AssertEquals(o1[i], o2[i]);
		}
	}
}
