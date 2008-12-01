// CircularPlaneEmitter.cs created with MonoDevelop
// User: topfs at 8:20 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class BoxEmitter : TemplateParticleEmitter
	{
		Vector3f side;
		Random random;
		public BoxEmitter(Vector3f position, Vector3f side) : base(position)
		{
			this.side = side;
			random = new Random();
		}

		public override Particle emit (ParticleFactory particleFactory)
		{
			Vector3f tmp = new Vector3f(0.5f - random.NextDouble(), 0.5f - random.NextDouble(), 0.5f - random.NextDouble());
			tmp.stretch(new Vector3f((float)random.NextDouble() * side.x, (float)random.NextDouble() * side.y, (float)random.NextDouble() * side.z));
			tmp.add(position);
			
			return particleFactory.createParticle(tmp, false, null);
		}
	}
}
