// Sphere.cs created with MonoDevelop
// User: topfs at 6:25 PM 12/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.Common
{
	
	
	public class Sphere
	{	
		public Vector3f position;
		public float radius;
		
		public Sphere(Vector3f position, float radius)
		{
			this.position = position;
			this.radius = radius;
		}
	}
}
