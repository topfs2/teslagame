// HUD.cs created with MonoDevelop
// User: topfs at 5:00 PM 2/8/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tesla.GFX.Font;
using Tesla.Common;
using Tao.OpenGl;

namespace Tesla.GFX
{
	public class HUD : Drawable2D 
	{
		List<Drawable2D> objects;
		GLFTFont f;
		
		public HUD(string defaultPath)
		{
			objects = new List<Drawable2D>();
			f = new GLFTFont(defaultPath + "Fonts/FreeSans.ttf", 64);
			objects.Add(new Quad2D(new BasicTexture(defaultPath + "GPLTextures/tango_bullet_icon/32x32/apps/bullet.png"), 10.0f, 10.0f));
		}

		public void Draw (float frameTime)
		{
			foreach (Drawable2D d in objects)
				d.Draw(frameTime);
			String s = "Welcome to Tesla!!!!";

			f.Draw(s, new Vector2f(10.0f, 650.0f), new Color4f(1.0f, 0.0f, 0.0f, 0.0f));
		}
	}
}
