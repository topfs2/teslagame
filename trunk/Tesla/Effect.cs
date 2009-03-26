// Effect.cs created with MonoDevelop
// User: topfs at 10:53 PM 3/25/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;
using Tesla.GFX;

namespace Tesla
{
	
	
	public class Effect : Drawable 
	{
		ParticleFactory pf;
		ParticleSystem ps;
		ParticleEmitter pe;

		BillboardedQuad quad;
		
		Vector3f position;
		Vector3f direction;
		
		public Effect(Camera camera, Configuration c)
		{
			Texture t = new BasicTexture(c.defaultPath + "Texture/Particle/p.png");
			this.position = new Vector3f();
			this.direction = new Vector3f();
			pe = new PointEmitter(position);
			
			Vector3f maxV = new Vector3f(0.3f, 0.3f, 0.3f);
			Vector3f minV = maxV * -1.0f;
			Vector3f g = new Vector3f(0.0f, -0.001f, 0.0f);
			
			Color4f minC = new Color4f(1.0f, 0.7f, 0.7f, 0.7f);
			Color4f maxC = new Color4f(1.0f, 1.0f, 1.0f, 1.0f);
			
			pf = new BillboardedParticleFactory(t, minV, maxV, g, 0.0f, 1.0f, minC, maxC, 0.2f);
			ps = new ParticleSystem(pe, pf, camera, true, 0.0f, 1000);
			pe.setActive(false);
			ps.reset();
			quad = new BillboardedQuad(new BasicTexture(c.defaultPath + "Texture/Particle/Flare.png"), camera, position, new Vector2f(1.0f, 1.0f));
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			position.set( position + frameTime * direction * 10.0f);
			ps.Draw(frameTime, frustum);

			quad.Draw(frameTime, frustum);
		}
		
		public void play(Vector3f position, Vector3f direction)
		{
			this.position.set(position);
			this.direction.set(direction);
			ps.reset();
			pe.setActive(true);
		}
	}
}
