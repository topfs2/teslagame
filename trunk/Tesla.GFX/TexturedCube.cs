// TexturedCube.cs created with MonoDevelop
// User: topfs at 8:58 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class TexturedCube : Drawable
	{
		protected float width, height, depth;
		public Vector3f pos;
		protected Texture texture;
		Color4f rotation;
		public TexturedCube(Texture texture, Vector3f pos, float width, float height, float depth)
		{
			this.pos = pos;
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.texture = texture;
			
			rotation = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
		}
		
		public void setRotation(Color4f rotation)
		{
			this.rotation = rotation;
		}
		
		public void setPosition(Vector3f position)
		{
			this.pos.set(position);
		}
		
		public void setSide(Vector3f side)
		{
			width  = side.x;
			height = side.y;
			depth  = side.z;
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glPushMatrix();
			if (texture != null)
				texture.Bind();
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			Gl.glTranslatef(pos.x, pos.y, pos.z);
			Gl.glRotatef(rotation.a, rotation.r, rotation.g, rotation.b);
			Gl.glScalef(width, height, depth);
			
			
			Gl.glBegin(Gl.GL_QUADS);
				// Front Face
				Gl.glNormal3f( 0.0f, 0.0f, 1.0f);					// Normal Pointing Towards Viewer
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 1 (Front)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 2 (Front)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Front)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 4 (Front)
				// Back Face
				Gl.glNormal3f( 0.0f, 0.0f,-1.0f);					// Normal Pointing Away From Viewer
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Back)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 2 (Back)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 3 (Back)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 4 (Back)
				// Top Face
				Gl.glNormal3f( 0.0f, 1.0f, 0.0f);					// Normal Pointing Up
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 1 (Top)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 2 (Top)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Top)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 4 (Top)
				// Bottom Face
				Gl.glNormal3f( 0.0f,-1.0f, 0.0f);					// Normal Pointing Down
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Bottom)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 2 (Bottom)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 3 (Bottom)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 4 (Bottom)
				// Right face
				Gl.glNormal3f( 1.0f, 0.0f, 0.0f);					// Normal Pointing Right
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 1 (Right)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 2 (Right)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Right)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 4 (Right)
				// Left Face
				Gl.glNormal3f(-1.0f, 0.0f, 0.0f);					// Normal Pointing Left
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Left)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 2 (Left)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 3 (Left)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 4 (Left)
			Gl.glEnd();
			
			Gl.glPopMatrix();
		}
	}
}
