// point2f.cs created with MonoDevelop
// User: topfs at 10:08 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{	
	public class Point2f
	{
		public float x, y;
		public Point2f(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		public Point2f subtract(Point2f dest)
		{
			return new Point2f(x - dest.x, y - dest.y);
		}
		
		public Point2f add(Point2f dest)
		{
			return new Point2f(x + dest.x, y + dest.y);
		}
		
		public Point2f divides(Point2f lower)
		{
			return new Point2f(x / lower.x, y / lower.y);
		}
		
		public Point2f divides(float lower)
		{
			return new Point2f(x / lower, y / lower);
		}
		
		public Point2f multiply(float multiply)
		{
			return new Point2f(x * multiply, y * multiply);
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
		
		public void set(Point2f src)
		{
			set(src.x, src.y);
		}		
		
		public Point2f Normalize()
		{
			return divides(length());
		}
	}
}
