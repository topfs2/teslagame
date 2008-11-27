// Wind.cs created with MonoDevelop
// User: topfs at 5:57 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public class Friction : TemplateManipulator
	{
		float strength;
		Point3f direction;
		
		public Friction(float strength)
		{
			this.strength  = strength;
		}

		public override void manipulate (Particle particle, Point3f deltaVelocity, Color4f deltaColor, ref float deltaLife)
		{
			deltaVelocity.set(particle.velocity);
			deltaVelocity.stretch(strength);
			deltaVelocity.invert();
		}
	}
}
