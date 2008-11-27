// GravityBall.cs created with MonoDevelop
// User: topfs at 6:09 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public class GravityBall : TemplateManipulator
	{
		Point3f position;
		float   size2, strength, threshold2;
		
		public GravityBall(Point3f position, float size, float strength, float threshold)
		{
			this.position = position;
			this.size2 = size * size; // Faster to query against length2
			this.strength = strength;
			this.threshold2 = threshold * threshold;
		}
		
		public override void manipulate (Particle particle, Point3f deltaVelocity, Color4f deltaColor, ref float deltaLife)
		{
			Point3f tmp = this.position.diff(particle.position);
			float len2 = tmp.length2();
			if (len2 > size2)
				return;
			else if (len2 < threshold2)
			{
				deltaVelocity.set(particle.velocity);
				deltaVelocity.stretch(strength);
				deltaVelocity.invert();
				return;
			}
			
			
			len2 = size2 / len2;
			
			deltaVelocity.set(tmp);
			deltaVelocity.stretch(strength);
			deltaVelocity.stretch(len2);		
		}
	}
}
