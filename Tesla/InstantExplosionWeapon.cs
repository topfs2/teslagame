// InstantExplosionWeapon.cs created with MonoDevelop
// User: topfs at 9:49 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.GFX;
using Tesla.Audio;
using Tesla.Common;

namespace Tesla
{
	public class InstantExplosionWeapon : Weapon
	{
		int reloadTime, damage, lastFired;
		static Sound sound;
		ParticleFactory pf;
		ParticleSystem ps;
		ParticleEmitter pe;
		
		Vector3f position;
		Vector3f direction;
		
		public InstantExplosionWeapon(string defaultPath, Camera camera)
		{
			if (sound == null)
				sound = new Sound(defaultPath + "Audio/laserfire3.wav");
			
			this.reloadTime = 1000;
			this.damage = 10;
			
			Texture t = new BasicTexture(defaultPath + "Texture/Particle/p.png");
			this.position = new Vector3f();
			this.direction = new Vector3f();
			pe = new PointEmitter(position);
			
			Vector3f maxV = new Vector3f(10.0f, 10.0f, 10.0f);
			Vector3f minV = maxV * -1.0f;
			Vector3f g = new Vector3f(0.0f, -0.001f, 0.0f);
			
			Color4f minC = new Color4f(1.0f, 0.7f, 0.7f, 0.0f);
			Color4f maxC = new Color4f(1.0f, 1.0f, 1.0f, 1.0f);
			
			pf = new BillboardedParticleFactory(t, minV, maxV, g, 0.0f, 1.0f, minC, maxC, 0.2f);
			ps = new ParticleSystem(pe, pf, camera, false, 0.1f, 1000);
			pe.setActive(false);
			ps.reset();
		}
		
		public bool canFire()
		{
			if (lastFired + reloadTime > System.Environment.TickCount)
				return false;
			else
				return true;
		}
		
		public string nameString()
		{
			return "Kaboom";
		}
		
		public string ammoString()
		{
			return "12 / 23";
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			ps.Draw(frameTime, frustum);
		}
		
		public void Fire(Vector3f playerPosition, Vector3f crosshairPosition)
		{
			if (!canFire())
				return;
			
			sound.play(position);
			position.set(crosshairPosition);
			ps.reset();
			pe.setActive(true);
			lastFired = System.Environment.TickCount;
		}
	}
}

