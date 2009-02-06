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
		float width, height, u, v;
		Vector3f pos;
		Texture texture;
		
		public Quad(Texture texture, Vector3f pos, float maximumWidth, float tileX, float tileY) : this(texture, pos, maximumWidth, maximumWidth * texture.Height() / texture.Width(), tileX, tileY) 
		{
		}
		
		public Quad(Texture texture, Vector3f pos, float width, float height, float tileX, float tileY)
		{
			this.pos = pos;
			this.width = width * tileX;
			this.height = height * tileY;
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
			Gl.glTexCoord2f(0, 0); Gl.glVertex3f(pos.x, pos.y, pos.z);
            Gl.glTexCoord2f(u, 0); Gl.glVertex3f(pos.x + width, pos.y, pos.z);
            Gl.glTexCoord2f(u, v); Gl.glVertex3f(pos.x + width, pos.y - height, pos.z);
            Gl.glTexCoord2f(0, v); Gl.glVertex3f(pos.x, pos.y - height, pos.z);

			Gl.glEnd();
			texture.UnBind();
		}
	}
}
