// GLFTFont.cs created with MonoDevelop
// User: topfs at 6:08 PM 3/25/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;
using Tao.OpenGl;
using Tao.FtGl;

namespace Tesla.GFX.Font
{
	public class GLFTFont : Font
	{
		FtGl.FTFont font;
		int fontSize;
		public GLFTFont(string font, int size)
		{
			this.font = new FtGl.FTGLTextureFont(font);
			this.font.FaceSize(size);
			fontSize = size;
		}
		
		public void Draw(string text, Vector2f position, Color4f color)
		{
			string[] rows = text.Split('\n');
			
			Gl.glPushMatrix();
			Gl.glColor4f(color.r, color.g, color.b, color.a);
			Gl.glTranslatef(position.x, position.y, 0.0f);
			for (int i = 0; i < rows.Length; i++)
			{
				font.Render(rows[i]);
				Gl.glTranslatef(0.0f, -fontSize, 0.0f);
			}
			Gl.glPopMatrix();
		}
	}
}
