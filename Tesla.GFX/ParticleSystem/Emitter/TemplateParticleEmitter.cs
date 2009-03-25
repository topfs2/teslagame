// TemplateParticleEmitter.cs created with MonoDevelop
// User: topfs at 8:21 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

namespace Tesla.GFX
{
	public abstract class TemplateParticleEmitter : ParticleEmitter
	{
		protected Vector3f position;
		protected bool active;
	
		public TemplateParticleEmitter(Vector3f position)
		{
			this.position = position;
			this.active = true;
		}

		public abstract Particle emit (ParticleFactory particleFactory);

		public bool setActive (bool active)
		{
			this.active = active;
			return active;
		}

		public bool getActive ()
		{
			return active;
		}
		
		public Vector3f getPosition()
		{
			return position.copy();
		}

		public void setPosition (Vector3f position)
		{
			this.position = position;
		}
	}
}
