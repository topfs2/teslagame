// ARBParticleFactory.cs created with MonoDevelop
// User: topfs at 7:26 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class BillboardedParticleFactory : ParticleFactory
	{
		Texture texture;
		Vector3f minimalInitialVelocity, maximumInitialVelocity, gravity;
		float minimalParticleLife, maximumParticleLife;
		Color4f minimalColor, maximumColor;
		float size;
		
		Random random;
	
		public BillboardedParticleFactory(Texture texture, Vector3f minimalInitialVelocity, Vector3f maximumInitialVelocity,
								  Vector3f gravity, float minimalParticleLife, float maximumParticleLife,
								  Color4f minimalColor, Color4f maximumColor, float size)
		{
			this.texture = texture;
			this.minimalInitialVelocity = minimalInitialVelocity;
			this.maximumInitialVelocity = maximumInitialVelocity;
			this.gravity = gravity;
			this.minimalParticleLife = minimalParticleLife;
			this.maximumParticleLife = maximumParticleLife;
			this.minimalColor = minimalColor;
			this.maximumColor = maximumColor;
			this.size = size;
			
			random = new Random();
		}

		public void preDraw ()
		{
			texture.Bind();
			Gl.glEnable(Gl.GL_BLEND );
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDepthMask( Gl.GL_FALSE );
			//Gl.glDisable(Gl.GL_DEPTH_TEST);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE);
			//Gl.glBlendFunc(Gl.GL_ONE, Gl.GL_ONE);
		}

		public void postDraw ()
		{
			Gl.glDepthMask( Gl.GL_TRUE );
			Gl.glEnable(Gl.GL_LIGHTING);
		}

		public Particle createParticle (Vector3f emitterPosition, bool emitterUseDirection, Vector3f emitterDirection)
		{
			Color4f color = new Color4f((maximumColor.a - minimalColor.a) * random.NextDouble() + minimalColor.a,
			                            (maximumColor.r - minimalColor.r) * random.NextDouble() + minimalColor.r,
			                            (maximumColor.g - minimalColor.g) * random.NextDouble() + minimalColor.g,
			                            (maximumColor.b - minimalColor.b) * random.NextDouble() + minimalColor.b);
			Vector3f velocity = new Vector3f((maximumInitialVelocity.x - minimalInitialVelocity.x) * random.NextDouble(),
			                               (maximumInitialVelocity.y - minimalInitialVelocity.y) * random.NextDouble(),
			                               (maximumInitialVelocity.z - minimalInitialVelocity.z) * random.NextDouble());
			velocity.add(minimalInitialVelocity);
			float particleLife = minimalParticleLife;
			float t = (maximumParticleLife - minimalParticleLife);
			particleLife += t * (float)random.NextDouble();
			
			return new BillboardedParticle(emitterPosition.copy(), velocity, gravity, color, particleLife, size);
		}
	}
}
