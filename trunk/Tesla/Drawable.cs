// Drawable.cs created with MonoDevelop
// User: topfs at 11:44 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.GFX
{
	
	
	public interface Drawable
	{
		void Draw(float frameTime);
	}
	
	public abstract class DrawableAnimation : Drawable
	{
		void Drawable.Draw(float frameTime)
		{
			Draw(true);
		}
		
		public abstract void Draw(bool Animate);
	}
}