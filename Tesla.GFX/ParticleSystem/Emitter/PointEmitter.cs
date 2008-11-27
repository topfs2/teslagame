// PointEmitter.cs created with MonoDevelop
// User: topfs at 7:23 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class PointEmitter : TemplateParticleEmitter
	{
		public PointEmitter(Point3f position) : base(position)
		{ }

		public override Particle emit (ParticleFactory particleFactory)
		{
			return particleFactory.createParticle(position, false, null);
		}
	}
}
