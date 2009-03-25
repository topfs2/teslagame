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
using Tao.FtGl;

namespace Tesla.GFX
{
	public class HUD : Drawable2D 
	{
		List<Drawable2D> objects;
		Tao.FtGl.FtGl.FTFont f;
		Tao.FtGl.FtGl.FTFont f2;
		
		public HUD(string defaultPath)
		{
			objects = new List<Drawable2D>();
			objects.Add(new Quad2D(new BasicTexture(defaultPath + "GPLTextures/Overlay.png"), 0.0f, 0.0f));
			//f = new FontTTF(defaultPath + "Fonts/FreeSans.ttf", 32);
			f = new Tao.FtGl.FtGl.FTGLTextureFont(defaultPath + "Fonts/FreeSans.ttf");
			f.FaceSize(144);
			f2 = new Tao.FtGl.FtGl.FTGLTextureFont(defaultPath + "Fonts/FreeSans.ttf");
			f2.FaceSize(160);
			objects.Add(new Quad2D(new BasicTexture(defaultPath + "GPLTextures/tango_bullet_icon/32x32/apps/bullet.png"), 100.0f, 100.0f));
		}

		public void Draw (float frameTime)
		{
			/* (Drawable2D d in objects)
				d.Draw(frameTime);*/
			String s = "Test";
			
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			f.Render(s + " " + frameTime);

			
			//Gl.glBlendFunc(Gl.GL_ONE, Gl.GL_ONE);
			//f.Draw("These many left, me friend", new Vector3f(30.0f, 50.0f, 0.0f), new Color4f(0.5f, 0.3f, 1.0f, 1.0f));
		}
	}
}
