// point2f.cs created with MonoDevelop
// User: topfs at 10:08 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{	
	public class Vector2f
	{
		public float x, y;
		public Vector2f(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		public Vector2f subtract(Vector2f dest)
		{
			return new Vector2f(x - dest.x, y - dest.y);
		}
		
		public Vector2f add(Vector2f dest)
		{
			return new Vector2f(x + dest.x, y + dest.y);
		}
		
		public Vector2f divides(Vector2f lower)
		{
			return new Vector2f(x / lower.x, y / lower.y);
		}
		
		public Vector2f divides(float lower)
		{
			return new Vector2f(x / lower, y / lower);
		}
		
		public Vector2f multiply(float multiply)
		{
			return new Vector2f(x * multiply, y * multiply);
		}
		
		public float length2()
		{
			return x * x + y * y;
		}
		
		public float length()
		{
			return (float)System.Math.Sqrt(length2());
		}
		
		public void set(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		public void set(Vector2f src)
		{
			set(src.x, src.y);
		}		
		
		public Vector2f Normalize()
		{
			return divides(length());
		}
	}
}
