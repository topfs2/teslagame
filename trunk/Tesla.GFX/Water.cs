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
	
	
	public class Water : Drawable 
	{
		public float y, uv;
		Texture texture;
		public Water(float y, Texture texture)
		{
			this.texture = texture;
			this.y = y;
		}
		
		public void Draw(float frameTime, Frustum frustum)
		{
			uv += frameTime*0.05f;
			texture.Bind();
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 0.5f);
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0, 1, 0);
			Gl.glTexCoord2f(0 + uv, 1000 + uv); Gl.glVertex3f(-10000, y, -10000);
            Gl.glTexCoord2f(1000 + uv, 1000 + uv); Gl.glVertex3f( 10000, y, -10000);
            Gl.glTexCoord2f(1000 + uv, 0 + uv); Gl.glVertex3f( 10000, y,  10000);
            Gl.glTexCoord2f(0 + uv, 0 + uv); Gl.glVertex3f(-10000, y,  10000);

			Gl.glEnd();
			Gl.glEnable(Gl.GL_LIGHTING);
		}
	}
}
