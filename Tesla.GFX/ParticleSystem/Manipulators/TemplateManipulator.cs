// TemplateManipulator.cs created with MonoDevelop
// User: topfs at 8:46 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{

	public abstract class TemplateManipulator : Manipulator
	{
		bool active;
		public TemplateManipulator()
		{
			active = true;
		}
				
		public bool getActive()
		{
			return this.active;
		}
		
		public bool setActive(bool activate)
		{
			return (this.active = activate);
		}

		public abstract void manipulate (Particle particle, Vector3f deltaVelocity, Color4f deltaColor, ref float deltaLife);
	}
}
