// TexturedQuad.cs created with MonoDevelop
// User: topfs at 3:02 PM 9/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class TexturedQuad : Drawable2D 
	{
		float width, height;
		Point2f pos;
		Texture tex;
		public TexturedQuad(Texture texture, Point2f pos, float width, float height)
		{
			this.pos = pos;
			this.width = width;
			this.height = height;
			tex = texture;
		}
		
		public void Draw(float frameTime)
		{
			tex.Bind();
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);		
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0, 0, 1);

			Gl.glTexCoord2f(0, 1); Gl.glVertex3f(pos.x, pos.y, 0);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(pos.x + width, pos.y, 0);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(pos.x + width, pos.y - height, 0);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(pos.x, pos.y - height, 0);

			Gl.glEnd();
		}
	}
}
