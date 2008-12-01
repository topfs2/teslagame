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
	
	
	public class MultiTexturedQuad : Drawable
	{
		protected float width, height;
		public Vector2f pos;
		Texture t0, t1, t2;
		public Color4f c;
		
		public MultiTexturedQuad(Texture t0, Texture t1, Texture t2, Vector2f pos)
		{
			this.pos = pos;
			this.width = 1.0f;
			this.height = 1.0f;

			this.t0 = t0;
			this.t1 = t1;
			this.t2 = t2;
			c = new Color4f(1.0f, 1.0f, 1.0f, 1.0f);
		}

		
		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glPushMatrix();
			
			Gl.glDisable(Gl.GL_LIGHTING);
			//Gl.glColor3f(1.0f, 1.0f, 1.0f);
		    /*float rgbValue   = c.r / (1.0f - c.b);
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
			
			Gl.glColor4f( rgbValue, rgbValue, rgbValue, alphaValue );*/
			
			preDraw();
			
			Gl.glTranslatef(pos.x, pos.y, 0);

			Gl.glScalef(width, height, 1.0f);
			
			Gl.glBegin(Gl.GL_QUADS);
				// Front Face
				Gl.glNormal3f( 0.0f, 0.0f, 1.0f);					// Normal Pointing Towards Viewer
				setTex2f(0.0f, 0.0f);Gl.glVertex3f(-1.0f, -1.0f,  0.0f);	// Point 1 (Front)
				setTex2f(1.0f, 0.0f); Gl.glVertex3f( 1.0f, -1.0f,  0.0f);	// Point 2 (Front)
				setTex2f(1.0f, 1.0f); Gl.glVertex3f( 1.0f,  1.0f,  0.0f);	// Point 3 (Front)
				setTex2f(0.0f, 1.0f); Gl.glVertex3f(-1.0f,  1.0f,  0.0f);	// Point 4 (Front)
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
			Gl.glActiveTextureARB(Gl.GL_TEXTURE0);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			t1.Bind();
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_ALPHA_ARB, Gl.GL_TEXTURE0);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_ALPHA_ARB, Gl.GL_SRC_ALPHA);

		
		
			Gl.glActiveTextureARB(Gl.GL_TEXTURE1);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			t0.Bind();
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_COMBINE_RGB, Gl.GL_MODULATE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_COMBINE_ALPHA, Gl.GL_REPLACE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_RGB_ARB, Gl.GL_TEXTURE1);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_RGB_ARB, Gl.GL_SRC_COLOR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_RGB_ARB, Gl.GL_TEXTURE1);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_RGB_ARB, Gl.GL_SRC_COLOR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_ALPHA_ARB, Gl.GL_TEXTURE0);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_ALPHA_ARB, Gl.GL_ONE_MINUS_SRC_ALPHA);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_ALPHA_ARB, Gl.GL_TEXTURE0);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_ALPHA_ARB, Gl.GL_ONE_MINUS_SRC_ALPHA);			
			
			Gl.glActiveTextureARB(Gl.GL_TEXTURE2);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			t2.Bind();
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_COMBINE_RGB, Gl.GL_MODULATE);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_RGB_ARB, Gl.GL_TEXTURE2);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_RGB_ARB, Gl.GL_SRC_COLOR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_RGB_ARB, Gl.GL_PRIMARY_COLOR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_RGB_ARB, Gl.GL_SRC_COLOR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE0_ALPHA_ARB, Gl.GL_TEXTURE0);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND0_ALPHA_ARB, Gl.GL_SRC_ALPHA);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_SOURCE1_ALPHA_ARB, Gl.GL_TEXTURE0);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_OPERAND1_ALPHA_ARB, Gl.GL_SRC_ALPHA);
			
			
			
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        	Gl.glEnable(Gl.GL_BLEND);
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
