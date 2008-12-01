// ARBParticleFactory.cs created with MonoDevelop
// User: topfs at 7:26 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class ARBParticleFactory : ParticleFactory
	{
		Texture texture;
		Point3f minimalInitialVelocity, maximumInitialVelocity, gravity;
		float minimalParticleLife, maximumParticleLife;
		Color4f minimalColor, maximumColor;
		
		Random random;
	
		public ARBParticleFactory(Texture texture, Point3f minimalInitialVelocity, Point3f maximumInitialVelocity,
								  Point3f gravity, float minimalParticleLife, float maximumParticleLife,
								  Color4f minimalColor, Color4f maximumColor)
		{
			this.texture = texture;
			this.minimalInitialVelocity = minimalInitialVelocity;
			this.maximumInitialVelocity = maximumInitialVelocity;
			this.gravity = gravity;
			this.minimalParticleLife = minimalParticleLife;
			this.maximumParticleLife = maximumParticleLife;
			this.minimalColor = minimalColor;
			this.maximumColor = maximumColor;
			
			random = new Random();
		}

		public void preDraw ()
		{
			texture.Bind();
			Gl.glEnable(Gl.GL_BLEND );
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDepthMask( Gl.GL_FALSE );
			//Gl.glDisable(Gl.GL_DEPTH_TEST);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE);
			
			float[] quadratic =  { 1.0f, 0.0f, 0.01f };
			Gl.glPointParameterfvARB(Gl.GL_POINT_DISTANCE_ATTENUATION_ARB, quadratic);

		    // Query for the max point size supported by the hardware
		    float maxSize = 0.0f;
		    Gl.glGetFloatv(Gl.GL_POINT_SIZE_MAX_ARB, out maxSize);

		    // Clamp size to 100.0f or the sprites could get a little too big on some  
		    // of the newer graphic cards. My ATI card at home supports a max point 
		    // size of 1024.0f!
		    if( maxSize > 100.0f )
		        maxSize = 100.0f;

		    Gl.glPointSize( maxSize );

		    // The alpha of a point is calculated to allow the fading of points 
		    // instead of shrinking them past a defined threshold size. The threshold 
		    // is defined by GL_POINT_FADE_THRESHOLD_SIZE_ARB and is not clamped to 
		    // the minimum and maximum point sizes.
		    Gl.glPointParameterfARB( Gl.GL_POINT_FADE_THRESHOLD_SIZE_ARB, 60.0f );

		    Gl.glPointParameterfARB( Gl.GL_POINT_SIZE_MIN_ARB, 1.0f );
		    Gl.glPointParameterfARB( Gl.GL_POINT_SIZE_MAX_ARB, maxSize );

		    // Specify point sprite texture coordinate replacement mode for each 
		    // texture unit
		    Gl.glTexEnvf( Gl.GL_POINT_SPRITE_ARB, Gl.GL_COORD_REPLACE_ARB, Gl.GL_TRUE );

		    //
			// Render point sprites...
			//

		    Gl.glEnable( Gl.GL_POINT_SPRITE_ARB );

			Gl.glBegin( Gl.GL_POINTS );
		}

		public void postDraw ()
		{
			Gl.glEnd();

			Gl.glDisable( Gl.GL_POINT_SPRITE_ARB );
			
			Gl.glDepthMask( Gl.GL_TRUE );
			Gl.glEnable(Gl.GL_LIGHTING);
		}

		public Particle createParticle (Point3f emitterPosition, bool emitterUseDirection, Point3f emitterDirection)
		{
			Color4f color = new Color4f((maximumColor.a - minimalColor.a) * random.NextDouble() + minimalColor.a,
			                            (maximumColor.r - minimalColor.r) * random.NextDouble() + minimalColor.r,
			                            (maximumColor.g - minimalColor.g) * random.NextDouble() + minimalColor.g,
			                            (maximumColor.b - minimalColor.b) * random.NextDouble() + minimalColor.b);
			Point3f velocity = new Point3f((maximumInitialVelocity.x - minimalInitialVelocity.x) * random.NextDouble(),
			                               (maximumInitialVelocity.y - minimalInitialVelocity.y) * random.NextDouble(),
			                               (maximumInitialVelocity.z - minimalInitialVelocity.z) * random.NextDouble());
			velocity.add(minimalInitialVelocity);
			float particleLife = minimalParticleLife;
			float t = (maximumParticleLife - minimalParticleLife);
			particleLife += t * (float)random.NextDouble();
			
			return new ARBParticle(emitterPosition.copy(), velocity, gravity, color, particleLife);
		}
	}
}
