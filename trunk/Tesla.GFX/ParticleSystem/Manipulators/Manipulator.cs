// Manipulator.cs created with MonoDevelop
// User: topfs at 12:04 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public interface Manipulator : Controller
	{
		void manipulate(Particle particle, Vector3f deltaVelocity, Color4f deltaColor, ref float deltaLife); // Should alter deltaFOO instead of returning something
	}
}
