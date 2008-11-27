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
	
	
	public class SkyBox
	{
		Texture top, left, right, front, back;
		
		public SkyBox(string skyBoxPath)
		{
			left 	= Texture.CreateFromFile(skyBoxPath + "/skyrender0001.bmp", TextureFilter.Nearest, Gl.GL_CLAMP_TO_EDGE);
			front 	= Texture.CreateFromFile(skyBoxPath + "/skyrender0002.bmp", TextureFilter.Nearest, Gl.GL_CLAMP_TO_EDGE);
			right 	= Texture.CreateFromFile(skyBoxPath + "/skyrender0004.bmp", TextureFilter.Nearest, Gl.GL_CLAMP_TO_EDGE);
			back 	= Texture.CreateFromFile(skyBoxPath + "/skyrender0005.bmp", TextureFilter.Nearest, Gl.GL_CLAMP_TO_EDGE);
			top		= Texture.CreateFromFile(skyBoxPath + "/skyrender0003.bmp", TextureFilter.Nearest, Gl.GL_CLAMP_TO_EDGE);
		}

		public void Draw (Point3f pos)
		{
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDepthMask(0);
			//Gl.glDisable(Gl.GL_CULL_FACE );
			//Gl.glEnable(Gl.GL_TEXTURE_2D);
			//Gl.glDisable(Gl.GL_DEPTH_TEST);
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			Gl.glTranslatef(pos.x, pos.y, pos.z);
			
			left.Bind();
			Gl.glBegin(Gl.GL_QUADS);
				// Left Face
				Gl.glNormal3f( 1.0f, 0.0f, 0.0f);					// Normal Pointing Left
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Left)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 2 (Left)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 3 (Left)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 4 (Left)
			Gl.glEnd();
			
			front.Bind();
			Gl.glBegin(Gl.GL_QUADS);
				// Front Face
				Gl.glNormal3f( 0.0f, 0.0f, -1.0f);					// Normal Pointing Towards Viewer
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 1 (Front)
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 2 (Front)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Front)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 4 (Front)
			Gl.glEnd();

			// Top Face			
			top.Bind();
			Gl.glBegin(Gl.GL_QUADS);
				Gl.glNormal3f( 0.0f, -1.0f, 0.0f);					// Normal Pointing Up
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 1 (Top)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 2 (Top)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Top)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 4 (Top)
			Gl.glEnd();
			
			right.Bind();			
			Gl.glBegin(Gl.GL_QUADS);
				// Right face
				Gl.glNormal3f(-1.0f, 0.0f, 0.0f);					// Normal Pointing Right
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 1 (Right)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 2 (Right)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Right)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 4 (Right)
			Gl.glEnd();

			// Back Face
			back.Bind();
			Gl.glBegin(Gl.GL_QUADS);
				Gl.glNormal3f( 0.0f, 0.0f, 1.0f);					// Normal Pointing Away From Viewer
				Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Back)
				Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 2 (Back)
				Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 3 (Back)
				Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 4 (Back)
			Gl.glEnd();
			
			
			Gl.glEnable(Gl.GL_LIGHTING);
			
			Gl.glDepthMask(1);
			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_MIRRORED_REPEAT_ARB);
			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_MIRRORED_REPEAT_ARB);
			Gl.glPopMatrix();
		}
	}
}