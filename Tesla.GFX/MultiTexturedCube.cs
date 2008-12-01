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
	
	
	public class MultiTexturedCube : Drawable
	{
		protected float width, height, depth;
		public Vector3f pos;
		Texture t0, t1, t2;
		public Color4f c;
		
		public MultiTexturedCube(Texture t0, Texture t1, Texture t2, Vector3f pos)
		{
			this.pos = pos;
			this.width = 1.0f;
			this.height = 1.0f;
			this.depth = 1.0f;
			this.t0 = t0;
			this.t1 = t1;
			this.t2 = t2;
			c = new Color4f(1.0f, 1.0f, 1.0f, 1.0f);
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
			
			Gl.glDisable(Gl.GL_LIGHTING);
			//Gl.glColor3f(1.0f, 1.0f, 1.0f);
		    float rgbValue   = c.r / (1.0f - c.b);
		    float alphaValue = 1.0f - c.b;

		    // Do some bounds checking...
		    if( rgbValue < 0.0f )
		        rgbValue = 0.0f;
		    if( rgbValue > 1.0f )
		        rgbValue = 1.0f;

		    if( alphaValue < 0.0f )
		        alphaValue = 0.0f;
		    if( alphaValue > 1.0f )
		        alphaValue = 1.0f;
			
			Gl.glColor4f( rgbValue, rgbValue, rgbValue, alphaValue );
			
			preDraw();
			
			Gl.glTranslatef(pos.x, pos.y, pos.z);

			Gl.glScalef(width, height, depth);
			
			Gl.glBegin(Gl.GL_QUADS);
				// Front Face
				Gl.glNormal3f( 0.0f, 0.0f, 1.0f);					// Normal Pointing Towards Viewer
				setTex2f(0.0f, 0.0f);Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 1 (Front)
				setTex2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 2 (Front)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Front)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 4 (Front)
				// Back Face
				Gl.glNormal3f( 0.0f, 0.0f,-1.0f);					// Normal Pointing Away From Viewer
				setTex2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Back)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 2 (Back)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 3 (Back)
				setTex2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 4 (Back)
				// Top Face
				Gl.glNormal3f( 0.0f, 1.0f, 0.0f);					// Normal Pointing Up
				setTex2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 1 (Top)
				setTex2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 2 (Top)
				setTex2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Top)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 4 (Top)
				// Bottom Face
				Gl.glNormal3f( 0.0f,-1.0f, 0.0f);					// Normal Pointing Down
				setTex2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Bottom)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 2 (Bottom)
				setTex2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 3 (Bottom)
				setTex2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 4 (Bottom)
				// Right face
				Gl.glNormal3f( 1.0f, 0.0f, 0.0f);					// Normal Pointing Right
				setTex2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f, -1.0f);	// Point 1 (Right)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f, -1.0f);	// Point 2 (Right)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  1.0f);	// Point 3 (Right)
				setTex2f(0.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  1.0f);	// Point 4 (Right)
				// Left Face
				Gl.glNormal3f(-1.0f, 0.0f, 0.0f);					// Normal Pointing Left
				setTex2f(0.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f, -1.0f);	// Point 1 (Left)
				setTex2f(1.0f, 0.0f); Gl.glVertex3f(-1.0f, -1.0f,  1.0f);	// Point 2 (Left)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  1.0f);	// Point 3 (Left)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f, -1.0f);	// Point 4 (Left)
			Gl.glEnd();
			
			postDraw();
			
			Gl.glPopMatrix();
		}
		
		private void setTex2f(float u, float v)
		{
			Gl.glMultiTexCoord2fARB( Gl.GL_TEXTURE0_ARB, u, v );
			Gl.glMultiTexCoord2fARB( Gl.GL_TEXTURE1_ARB, u, v );
		    Gl.glMultiTexCoord2fARB( Gl.GL_TEXTURE2_ARB, u, v );
		}
		
		private void preDraw()
		{
		
		    Gl.glActiveTextureARB( Gl.GL_TEXTURE0_ARB );
		    Gl.glEnable( Gl.GL_TEXTURE_2D );
		    t0.Bind();

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE );

		    //
		    // Texture Stage 1
		    //
		    // Perform a linear interpolation between the output of stage 0 
		    // (i.e texture0) and texture1 and use the RGB portion of the vertex's 
		    // color to mix the two. 
		    //

		    Gl.glActiveTextureARB(Gl.GL_TEXTURE1_ARB );
		    Gl.glEnable( Gl.GL_TEXTURE_2D );
		    t1.Bind();

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_COMBINE_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_COMBINE_RGB_ARB, Gl.GL_INTERPOLATE_ARB );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_RGB_ARB, Gl.GL_PREVIOUS_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_RGB_ARB, Gl.GL_SRC_COLOR );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_RGB_ARB, Gl.GL_TEXTURE );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_RGB_ARB, Gl.GL_SRC_COLOR );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE2_RGB_ARB, Gl.GL_PRIMARY_COLOR_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND2_RGB_ARB, Gl.GL_SRC_COLOR );

		    //
		    // Texture Stage 2
		    //
		    // Perform a linear interpolation between the output of stage 1 
		    // (i.e texture0 mixed with texture1) and texture2 and use the ALPHA 
		    // portion of the vertex's color to mix the two. 
		    //

		    Gl.glActiveTextureARB( Gl.GL_TEXTURE2_ARB );
		    Gl.glEnable( Gl.GL_TEXTURE_2D );
		    t2.Bind();

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_COMBINE_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_COMBINE_RGB_ARB, Gl.GL_INTERPOLATE_ARB );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_RGB_ARB, Gl.GL_PREVIOUS_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_RGB_ARB, Gl.GL_SRC_COLOR );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_RGB_ARB, Gl.GL_TEXTURE );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_RGB_ARB, Gl.GL_SRC_COLOR );

		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE2_RGB_ARB, Gl.GL_PRIMARY_COLOR_ARB );
		    Gl.glTexEnvi( Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND2_RGB_ARB, Gl.GL_SRC_ALPHA );
		}
		
		private void postDraw()
		{
		    Gl.glActiveTextureARB(Gl.GL_TEXTURE0_ARB);
		    Gl.glDisable(Gl.GL_TEXTURE_2D);
		    Gl.glActiveTextureARB(Gl.GL_TEXTURE1_ARB);
		    Gl.glDisable(Gl.GL_TEXTURE_2D);
		    Gl.glActiveTextureARB(Gl.GL_TEXTURE2_ARB);
		    Gl.glDisable(Gl.GL_TEXTURE_2D);
		    
		    Gl.glActiveTexture(Gl.GL_TEXTURE0);
		    Gl.glEnable(Gl.GL_TEXTURE_2D);		
		}
	}
}
