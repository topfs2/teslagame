// ParticleEmittor.cs created with MonoDevelop
// User: topfs at 7:11 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	public interface ParticleEmitter : Controller
	{
		Particle emit(ParticleFactory particleFactory);
		
		Vector3f getPosition();
	}
}
