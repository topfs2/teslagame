// Weapon.cs created with MonoDevelop
// User: topfs at 10:14 PM 3/26/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;
using Tesla.GFX;

namespace Tesla
{
	
	
	public interface Weapon : Drawable
	{
		bool canFire();
		
		string nameString();
		
		void Fire(Vector3f playerPosition, Vector3f crosshairPosition);
	}
}
