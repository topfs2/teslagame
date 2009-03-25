// Weapon.cs created with MonoDevelop
// User: topfs at 9:49 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Audio;
using Tesla.Common;

namespace Tesla
{
	public class Weapon
	{
		int reloadTime, damage, lastFired;
		Sound sound;
		Effect effect;
		
		public Weapon(int reloadTime, int damage, Sound sound, Effect effect)
		{
			this.reloadTime = reloadTime;
			this.damage = damage;
			this.sound = sound;
			this.effect = effect;
		}
		
		public bool canFire()
		{
			if (lastFired + reloadTime > System.Environment.TickCount)
				return false;
			else
				return true;
		}
		
		public string ammoString()
		{
			return "12 / 23";
		}
		
		public void Fire(Vector3f position, Vector3f direction)
		{
			if (!canFire())
				return;
			
			sound.play(position);
			effect.play(position, direction);
			lastFired = System.Environment.TickCount;
		}
	}
}
