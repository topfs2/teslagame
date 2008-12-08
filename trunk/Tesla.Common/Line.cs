// Line.cs created with MonoDevelop
// User: topfs at 9:40 PM 12/8/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	public class Line
	{
		private Plane p0, p1;
		public Line(Vector3f pointAOnLine, Vector3f pointBOnLine, Vector3f pointNotOnLine)
		{
			p0 = new Plane(pointAOnLine, pointBOnLine, pointNotOnLine);
			Vector3f temp = pointAOnLine - pointBOnLine;
			p1 = new Plane(temp.Cross(p1.getNormal()), pointAOnLine);
		}
		
		public float distanceTo2(Vector3f p)
		{
			return p0.distanceTo2(p) + p1.distanceTo2(p);
		}
		
		public float distanceTo(Vector3f p)
		{
			return (float)Math.Sqrt(distanceTo2(p));
		}
	}
}
