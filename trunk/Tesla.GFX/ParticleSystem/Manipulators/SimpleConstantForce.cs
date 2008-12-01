// Wind.cs created with MonoDevelop
// User: topfs at 5:57 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public class SimpleConstantForce : TemplateManipulator
	{
		float strength;
		Vector3f direction;
		
		public SimpleConstantForce(Vector3f direction, float strength)
		{
			this.direction = direction;
			this.strength  = strength;
		}

		public override void manipulate (Particle particle, Vector3f deltaVelocity, Color4f deltaColor, ref float deltaLife)
		{
			deltaVelocity.add(direction);
			deltaVelocity.stretch(strength);
		}
	}
}
