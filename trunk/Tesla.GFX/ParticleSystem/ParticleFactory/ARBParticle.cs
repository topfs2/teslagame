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
	
	
	public class ARBParticle : Particle
	{
		
		public ARBParticle(Point3f position, Point3f velocity, Point3f gravity, Color4f color, float life) : base(position, velocity, gravity, color, life)
		{
		}
		
		public override void Draw(Camera activeCamera, float frameTime)
		{
			Gl.glColor4f(color.r, color.g, color.b, (remainingLife / startLife) * color.a); 
			Gl.glVertex3f(position.x, position.y, position.z);
		}
	}
}
