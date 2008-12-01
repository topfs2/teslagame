// Quad.cs created with MonoDevelop
// User: topfs at 11:48 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class Quad : Drawable
	{
		float width, height;
		Vector3f pos;
		
		public Quad(Vector3f pos, float width, float height)
		{
			this.pos = pos;
			this.width = width;
			this.height = height;
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glColor3f(1.0f, 1.0f, 1.0f);			
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0, 0, -1);

			Gl.glTexCoord2f(0, 1); Gl.glVertex3f(pos.x, pos.y, pos.z);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(pos.x + width, pos.y, pos.z);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(pos.x + width, pos.y - height, pos.z);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(pos.x, pos.y - height, pos.z);

			Gl.glEnd();
		}
	}
}
