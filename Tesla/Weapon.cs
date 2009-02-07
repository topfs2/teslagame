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
		
		public Weapon(int reloadTime, int damage, Sound sound)
		{
			this.reloadTime = reloadTime;
			this.damage = damage;
			this.sound = sound;
		}
		
		public bool canFire()
		{
			if (lastFired + reloadTime > System.Environment.TickCount)
				return false;
			else
				return true;
		}
		
		public void Fire(Vector3f position)
		{
			if (!canFire())
				return;
			
			sound.play(position);
			lastFired = System.Environment.TickCount;
		}
	}
}
