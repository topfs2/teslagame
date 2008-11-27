// Color3f.cs created with MonoDevelop
// User: topfs at 5:54 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	public class Color4f
	{
		private float[] argb = new float[4];
			
		public Color4f(float a, float r, float g, float b)
		{
			set(a, r, g, b);
		}
		
		public void set(float a, float r, float g, float b)
		{
			this.a = a;
			this.r = r;
			this.g = g;
			this.b = b;
		}
		
		public Color4f copy()
		{
			return new Color4f(a, r, g, b);
		}
		
		public Color4f add(Color4f src)
		{
			a += src.a;
			r += src.r;
			g += src.g;
			b += src.b;
			
			return this;
		}
		
		public Color4f multiply(float multiply)
		{
			a *= multiply;
			r *= multiply;
			g *= multiply;
			b *= multiply;
			
			return this;
		}
		
		public float a
		{
			set { argb[0] = value; }
			get { return argb[0];  }
		}
		
		public float r
		{
			set { argb[1] = value; }
			get { return argb[1];  }
		}
		
		public float g
		{
			set { argb[2] = value; }
			get { return argb[2];  }
		}
		
		public float b
		{
			set { argb[3] = value; }
			get { return argb[3];  }
		}
		
		public float[] argbVector
		{
			//get { return argb; }
			get { return new float[]{a, r, g, b}; }
		}
		
		public float[] rgbaVector
		{
			get { return new float[]{r, g, b, a}; }
		}
	}
}
