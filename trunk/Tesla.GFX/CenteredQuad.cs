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
	
	
	public class CenteredQuad : Drawable
	{
		float w2, h2, u, v;
		Vector3f pos;
		Texture texture;

		public CenteredQuad(Texture texture, Vector3f pos, float width, float height, float tileX, float tileY)
		{
			this.pos = pos;
			this.w2 = width / 2.0f;
			this.h2 = height / 2.0f;
			u = tileX;
			v = tileY;
			this.texture = texture;
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			texture.Bind();
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0, 0, -1);
			Gl.glTexCoord2f(0, 0); Gl.glVertex3f(pos.x - w2, pos.y + h2, pos.z);
            Gl.glTexCoord2f(u, 0); Gl.glVertex3f(pos.x + w2, pos.y + h2, pos.z);
            Gl.glTexCoord2f(u, v); Gl.glVertex3f(pos.x + w2, pos.y - h2, pos.z);
            Gl.glTexCoord2f(0, v); Gl.glVertex3f(pos.x - w2, pos.y - h2, pos.z);

			Gl.glEnd();
			texture.UnBind();
		}
	}
}
