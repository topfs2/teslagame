// Weapon.cs created with MonoDevelop
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
	public class MissileWeapon : Weapon
	{
		int reloadTime, damage, lastFired;
		static Sound sound;
		ParticleFactory pf;
		ParticleSystem ps;
		ParticleEmitter pe;

		CenteredQuad quad;
		
		Vector3f position;
		Vector3f direction;
		
		public MissileWeapon(string defaultPath, Camera camera)
		{
			if (sound == null)
				sound = new Sound(defaultPath + "Audio/laserfire3.wav");
			
			this.reloadTime = 1000;
			this.damage = 10;
			
			Texture t = new BasicTexture(defaultPath + "Texture/Particle/p.png");
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
			quad = new CenteredQuad(new BasicTexture(defaultPath + "Texture/Particle/Flare.png"), position, 1.0f, 1.0f, 1.0f, 1.0f);
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
			return "Missile 2k3";
		}
		
		public string ammoString()
		{
			return "12 / 23";
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			position.set( position + frameTime * direction * 10.0f);
			ps.Draw(frameTime, frustum);

			quad.Draw(frameTime, frustum);
		}
		
		public void Fire(Vector3f playerPosition, Vector3f crosshairPosition)
		{
			if (!canFire())
				return;
			
			sound.play(position);
			position.set(playerPosition);
			direction = (crosshairPosition - playerPosition).Normalize();
			ps.reset();
			pe.setActive(true);
			lastFired = System.Environment.TickCount;
		}
	}
}
