// World.cs created with MonoDevelop
// User: topfs at 8:27 AM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.Ode;

using Tesla.Common;

namespace Tesla.Physics
{
	public class World
	{
		List<Space> listSpaces;
		List<BodyBox> listBodiesBoxes;
		List<BodySphere> listBodiesSpheres;
		//IntPtr heightField;
		IntPtr worldID, contactGroup;
		
		public World(float gravityX, float gravityY, float gravityZ)
		{
			listBodiesBoxes 	= new List<BodyBox>();
			listBodiesSpheres 	= new List<BodySphere>();
			listSpaces = new List<Space>();
            worldID = Ode.dWorldCreate();
            // set the error reduction parameter for the sim
           // Ode.dWorldSetERP(worldID,0.2f);

            // set the constraint force mixing value
            //Ode.dWorldSetCFM(worldID,1e-6f);
            
            // Add gravity to the world (pull down on the Y axis 9.81 meters/second
            Ode.dWorldSetGravity(worldID, gravityX, gravityY, gravityZ);	
            contactGroup = Ode.dJointGroupCreate(0);
		}
		
		public Space addSpace()
		{
			Space space = new Space( this, this.contactGroup );
			listSpaces.Add(space);
			return space;
		}
		
		public IntPtr getID()
		{
			return worldID;
		}
		
		public void createPlane(Space space, float y)
		{
			IntPtr ground = Ode.dCreatePlane(space.getSpaceID(), 0.01f,1.0f,0.0f, 10.0f);
			//Ode.dGeomSetPosition(ground, 0.0f, y, 0.0f);
		}
		
		public void addHeightField(Space space, double[] field, float width, float depth, int widthsamples, int depthsamples)
		{
			IntPtr dataID = Ode.dGeomHeightfieldDataCreate();
			Ode.dGeomHeightfieldDataBuildDouble(dataID, field, 1, width, depth, widthsamples, depthsamples, 1.0f, 0.0f, 1.0f, 0); 
			IntPtr heightfield = Ode.dCreateHeightfield(space.getSpaceID(), dataID, 1);
			Ode.dGeomSetPosition(heightfield, width/2.0f, 0.0f, depth/2.0f);
		}
		
		public BodyBox createBox(Space space, Vector3f position, Vector3f size, Vector3f force)
		{
			BodyBox b = new BodyBox(this, space, position, size, force);
			listBodiesBoxes.Add(b);
			return b;
		}
		
		public BodySphere createShphere(Space space, Vector3f position, float radius, Vector3f force)
		{
			BodySphere b = new BodySphere(this, space, position, radius, force);
			listBodiesSpheres.Add(b);
			return b;
		}
		
		public void update(float frameTime)
		{
            // process all collisions within the scene. We need to provide this 
            // with a callback to actually process any collisions that occur between bodies, 
            // In this case, NearCallback.
            foreach (Space s in listSpaces)
            	Ode.dSpaceCollide(s.getSpaceID(), new IntPtr(), new Ode.dNearCallback(s.NearCallback));

            // step the world. by the specified amount
            Ode.dWorldStep(worldID, frameTime);

            // empty any collision contacts that occur after we have finished processing.
            Ode.dJointGroupEmpty(contactGroup);
			foreach(BodyBox b in listBodiesBoxes)
				b.update();
			foreach(BodySphere b in listBodiesSpheres)
				b.update();
		}
	}
}
