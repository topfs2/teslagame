// ParticleARB.cs created with MonoDevelop
// User: topfs at 7:26 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

using Tao.OpenGl;

namespace Tesla.GFX
{
	
	
	public class BillboardedParticle : Particle
	{
		
		public BillboardedParticle(Point3f position, Point3f velocity, Point3f gravity, Color4f color, float life, float size) : base(position, velocity, gravity, color, life, size)
		{
		}
		
		public override void Draw(Camera activeCamera, float frameTime)
		{
			Gl.glPushMatrix();
			Gl.glTranslatef(position.x, position.y, position.z);
			Gl.glScalef(size, size, size);
			
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor4f(color.r, color.g, color.b, (remainingLife / startLife) * color.a); 
			
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3fv(((activeCamera.getUpVector() * -1.0f) - activeCamera.getRightVector()).vector);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3fv(((activeCamera.getUpVector()        ) - activeCamera.getRightVector()).vector);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3fv(((activeCamera.getUpVector()        ) + activeCamera.getRightVector()).vector);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3fv(((activeCamera.getUpVector() * -1.0f) + activeCamera.getRightVector()).vector);
			
			Gl.glEnd();
			
			Gl.glPopMatrix();
		}
	}
}
