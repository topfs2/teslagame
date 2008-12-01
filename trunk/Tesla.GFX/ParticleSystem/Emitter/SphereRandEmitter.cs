// CircularPlaneEmitter.cs created with MonoDevelop
// User: topfs at 8:20 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class SphereRandEmitter : TemplateParticleEmitter
	{
		float radius;
		Random random;
		public SphereRandEmitter(Vector3f position, float radius) : base(position)
		{
			this.radius = radius;
			random = new Random();
		}

		public override Particle emit (ParticleFactory particleFactory)
		{
			Vector3f tmp = new Vector3f(0.5f - random.NextDouble(), 0.5f - random.NextDouble(), 0.5f - random.NextDouble());
			tmp.Normalize();
			tmp.stretch(radius);
			tmp.add(position);
			
			return particleFactory.createParticle(tmp, false, null);
		}
	}
}
