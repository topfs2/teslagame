// AnimatedQuad.cs created with MonoDevelop
// User: topfs at 8:12 AM 3/26/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class AnimatedQuad : Drawable
	{
		
		public AnimatedQuad()
		{
		}
		float width, height;
		float f;
		Vector3f pos;
		Texture[] textures;
		
		public AnimatedQuad(Texture[] textures, Vector3f pos, float width, float height)
		{
			this.pos = pos;
			this.width = width;
			this.height = height;
			this.textures = textures;
			f = 0;
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			f += (frameTime * 10);
			int i = (int)f;
			if (i >= textures.Length)
			{
				f = 0.0f;
				i = 0;
			}
			textures[i].Bind();
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0, 0, -1);
			Gl.glTexCoord2f(0, 0); Gl.glVertex3f(pos.x, pos.y, pos.z);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(pos.x + width, pos.y, pos.z);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(pos.x + width, pos.y - height, pos.z);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(pos.x, pos.y - height, pos.z);

			Gl.glEnd();
			textures[i].UnBind();
		}

	}
}
