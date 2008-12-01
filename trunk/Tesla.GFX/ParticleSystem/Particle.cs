// Particle.cs created with MonoDevelop
// User: topfs at 8:52 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public abstract class Particle
	{
		public Point3f position, velocity, gravity;
		protected Color4f color;
		protected float startLife, remainingLife;
		protected float size;
		public Particle(Point3f position, Point3f velocity, Point3f gravity, Color4f color, float life, float size)
		{
			this.position = position;
			this.velocity = velocity;
			this.gravity  = gravity;
			this.color    = color;
			startLife = remainingLife = life;
			this.size = size;
		}

		public void update(float frameTime, List<CollisionSurface> listCollisionSurfaces, List<Manipulator> listManipulators)
		{
			Point3f positionTemp = position.copy();
			velocity.add(gravity);
			positionTemp.add(velocity * frameTime);
			remainingLife -= frameTime;
			
			foreach (CollisionSurface cs in listCollisionSurfaces)
			{
				if (cs.getActive() && cs.collisionDetect(position, positionTemp))
				{
					velocity = cs.computeTrajectory(velocity);
					positionTemp = position.copy();
					positionTemp.add(velocity * frameTime);					
				}
			}
			
			foreach (Manipulator m in listManipulators)
			{
				if (m.getActive())
				{
					Point3f deltaVelocity = new Point3f(0.0f, 0.0f, 0.0f);
					Color4f deltaColor = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
					float deltaLife = 0.0f;
					m.manipulate(this, deltaVelocity, deltaColor, ref deltaLife);
					velocity.add(deltaVelocity.stretch(frameTime));
					color.add(deltaColor.multiply(frameTime));
					remainingLife += deltaLife * frameTime;
				}
			}
			
			position.set(positionTemp);
		}
		
		public bool dead()
		{
			return remainingLife < 0.0f;
		}
		
		public abstract void Draw(Camera activeCamera, float frameTime);
	}
}
