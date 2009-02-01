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
		CubeMapTexture texture;
		
		public SkyBox(CubeMapTexture texture)
		{
			this.texture = texture;
		}

		public void Draw (Vector3f pos)
		{
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDepthMask(0);
			Gl.glTranslatef(pos.x, pos.y, pos.z);
			texture.Bind();
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			float fExtent = 1.0f;
			Gl.glBegin(Gl.GL_QUADS);
				// Negative X
				Gl.glTexCoord3f(-1.0f, -1.0f, 1.0f);
		        Gl.glVertex3f(-fExtent, -fExtent, fExtent);
				Gl.glTexCoord3f(-1.0f, -1.0f, -1.0f);
		        Gl.glVertex3f(-fExtent, -fExtent, -fExtent);
				Gl.glTexCoord3f(-1.0f,  1.0f, -1.0f);
		        Gl.glVertex3f(-fExtent, fExtent, -fExtent);
				Gl.glTexCoord3f(-1.0f, 1.0f, 1.0f);
		        Gl.glVertex3f(-fExtent, fExtent, fExtent);
		        
		        // Positive Z
				Gl.glTexCoord3f( 1.0f,    -1.0f,    1.0f);
		        Gl.glVertex3f(   fExtent, -fExtent, fExtent);
				Gl.glTexCoord3f(-1.0f,    -1.0f,    1.0f);
		        Gl.glVertex3f(  -fExtent, -fExtent, fExtent);
				Gl.glTexCoord3f(-1.0f,    1.0f,     1.0f);
		        Gl.glVertex3f(  -fExtent, fExtent,  fExtent);
				Gl.glTexCoord3f( 1.0f,    1.0f,     1.0f);
		        Gl.glVertex3f(   fExtent, fExtent,  fExtent);
		        
		        // Positive X
				Gl.glTexCoord3f( 1.0f, 1.0f,-1.0f);
		        Gl.glVertex3f(fExtent, fExtent,-fExtent);
				Gl.glTexCoord3f( 1.0f, 1.0f, 1.0f);
		        Gl.glVertex3f( fExtent, fExtent, fExtent);
				Gl.glTexCoord3f( 1.0f,-1.0f, 1.0f);
		        Gl.glVertex3f( fExtent,-fExtent, fExtent);
				Gl.glTexCoord3f( 1.0f, -1.0f, -1.0f);
		        Gl.glVertex3f( fExtent, -fExtent, -fExtent);
		        
		        // Negative Z
				Gl.glTexCoord3f(-1.0f,     1.0f,    -1.0f);
		        Gl.glVertex3f(  -fExtent,  fExtent, -fExtent);
				Gl.glTexCoord3f( 1.0f,     1.0f,    -1.0f);
		        Gl.glVertex3f(   fExtent,  fExtent, -fExtent);
				Gl.glTexCoord3f( 1.0f,    -1.0f,    -1.0f);
		        Gl.glVertex3f(   fExtent, -fExtent, -fExtent);
				Gl.glTexCoord3f(-1.0f,    -1.0f,    -1.0f);
		        Gl.glVertex3f(  -fExtent, -fExtent, -fExtent);
		        
		        // Positive Y
				Gl.glTexCoord3f( 1.0f,     1.0f,     1.0f);
		        Gl.glVertex3f(   fExtent,  fExtent,  fExtent);
				Gl.glTexCoord3f(-1.0f,     1.0f,     1.0f);
		        Gl.glVertex3f(  -fExtent,  fExtent,  fExtent);
				Gl.glTexCoord3f(-1.0f,     1.0f,    -1.0f);
		        Gl.glVertex3f(  -fExtent,  fExtent, -fExtent);
				Gl.glTexCoord3f( 1.0f,     1.0f,    -1.0f);
		        Gl.glVertex3f(   fExtent,  fExtent, -fExtent);
		        
		        // Negative Y
				Gl.glTexCoord3f( 1.0f,    -1.0f,     1.0f);
		        Gl.glVertex3f(   fExtent, -fExtent,  fExtent);
				Gl.glTexCoord3f(-1.0f,    -1.0f,     1.0f);
		        Gl.glVertex3f(  -fExtent, -fExtent,  fExtent);
				Gl.glTexCoord3f(-1.0f,    -1.0f,    -1.0f);
		        Gl.glVertex3f(  -fExtent, -fExtent, -fExtent);
				Gl.glTexCoord3f( 1.0f,    -1.0f,    -1.0f);
		        Gl.glVertex3f(   fExtent, -fExtent, -fExtent);
			Gl.glEnd();
			texture.UnBind();
			Gl.glDepthMask(1);
			Gl.glPopMatrix();
		}
	}
}