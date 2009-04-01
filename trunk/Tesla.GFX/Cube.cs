// Cube.cs created with MonoDevelop
// User: topfs at 11:24 AM 9/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class Cube : Drawable
	{
		protected float width, height, depth, rot;
		protected Vector3f pos;
		protected Color4f  color;

		public Cube(Color4f color, Vector3f pos, float width, float height, float depth)
		{
			this.pos = pos;
			this.color = color;
			this.width = width;
			this.height = height;
			this.depth = depth;
			rot = 0.0f;
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glColor4f(color.r, color.g, color.b, color.a);
			Gl.glPushMatrix();
			Gl.glTranslatef(pos.x,pos.y,pos.z);				// Move Right And Into The Screen
			Gl.glScalef(width, height, depth);
			Gl.glBegin(Gl.GL_QUADS);					// Start Drawing The Cube
				Gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Top)
				Gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Top)
				Gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Bottom Left Of The Quad (Top)
				Gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Bottom Right Of The Quad (Top)

				Gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Top Right Of The Quad (Bottom)
				Gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Top Left Of The Quad (Bottom)
				Gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Bottom)
				Gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Bottom)

				Gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Front)
				Gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Front)
				Gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Bottom Left Of The Quad (Front)
				Gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Bottom Right Of The Quad (Front)

				Gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Back)
				Gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Back)
				Gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Back)
				Gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Back)

				Gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Left)
				Gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Left)
				Gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Left)
				Gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Bottom Right Of The Quad (Left)

				Gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Right)
				Gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Right)
				Gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Bottom Left Of The Quad (Right)
				Gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Right)
			Gl.glEnd();
			
			rot -= 0.10f;
			
			Gl.glPopMatrix();
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
		}
	}
}
