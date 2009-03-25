// ARBParticleSystem.cs created with MonoDevelop
// User: topfs at 8:55 PM 10/29/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class ParticleSystem : Drawable
	{
		bool  endlessLife;
		float defaultEmitterLife;
		float emitterLife;
		List<Particle> listParticles;
		//int lastTickCount;
		int maxParticles;
		List<CollisionSurface> listCollisionSurfaces;
		List<Manipulator> listManipulators;
		Camera activeCamera;
		// NEW
		ParticleEmitter particleEmitter;
		ParticleFactory particleFactory;
		
		Sphere boundingSphere;
		
		private int CompareParticlesAgainstCamera(Particle x, Particle y)
		{
			Vector3f tmp1 = activeCamera.getPosition().copy();
			Vector3f tmp2 = activeCamera.getPosition().copy();
			
			tmp1.subtract(x.position);
			tmp2.subtract(y.position);
			
			return (int)((tmp2.length2() - tmp1.length2()) * 1000000.0f);
		}
		
		public ParticleSystem(ParticleEmitter particleEmitter, ParticleFactory particleFactory, Camera activeCamera, bool endlessLife, float emitterLife, int maxParticles)
		{
			this.particleEmitter = particleEmitter;
			this.particleFactory = particleFactory;
		
			this.endlessLife = endlessLife;
			this.emitterLife = defaultEmitterLife = emitterLife;
			this.maxParticles = maxParticles;
			listParticles = new List<Particle>(maxParticles);
			listCollisionSurfaces = new List<CollisionSurface>();
			listManipulators = new List<Manipulator>();
			this.activeCamera = activeCamera;
			
			boundingSphere = new Sphere(particleEmitter.getPosition(), particleFactory.getRange());
		}
		
		public void Draw(float frameTime, Frustum frustum)
		{
			updateParticles(frameTime);
			drawParticles(frameTime, frustum);
			
			if (!endlessLife)
				emitterLife -= frameTime;
			if (particleEmitter.getActive() && (endlessLife || emitterLife > 0.0f) && listParticles.Count < maxParticles)
			{                                                
				listParticles.Add(particleEmitter.emit(particleFactory));
			}
		}
		
		public bool isAlive()
		{
			return endlessLife || emitterLife > 0 || listParticles.Count > 0;
		}
		
		public bool isEmitting()
		{
			return listParticles.Count < maxParticles;
		}
		
		public void reset()
		{
			listParticles.Clear();
			emitterLife = defaultEmitterLife;
		}
		
		/*public Point3f linkedPosition()
		{
			return ;
		}*/
		
		public CollisionSurface addCollisionSurface(CollisionSurface collisionSurface)
		{
			listCollisionSurfaces.Add(collisionSurface);
			return collisionSurface;
		}
		
		public Manipulator addManipulator(Manipulator manipulator)
		{
			listManipulators.Add(manipulator);
			return manipulator;
		}
		
		public void updateParticles(float frameTime)
		{
			for (int i = 0; i < listParticles.Count; i++)
			{
				listParticles[i].update(frameTime, listCollisionSurfaces, listManipulators);
				if (listParticles[i].dead())
					listParticles.RemoveAt(i);
			}
			
			//listParticles.Sort(CompareParticlesAgainstCamera);
		}
		
		public void drawParticles(float frameTime, Frustum frustum)
		{
			particleFactory.preDraw();
            //int i = 0;
            //int j = 0;
			//if (frustum.inFrustum(boundingSphere)) //TODO Not working properly
			{
		        foreach (Particle p in listParticles)
		        {
					if (frustum.inFrustum(p.position))
						p.Draw(activeCamera, frameTime);
				}
	        }
		    particleFactory.postDraw();
		}
	}
}
