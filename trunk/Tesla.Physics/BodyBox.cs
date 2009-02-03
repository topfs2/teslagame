// Body.cs created with MonoDevelop
// User: topfs at 8:36 AM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.Ode;

using Tesla.Common;

namespace Tesla.Physics
{
	public class BodyBox
	{
		World hostWorld;
		Space space;
		IntPtr bodyID, geomID;
		
		//Ode.dMass mass;
		Vector3f position;
		Color4f rotationOGL;
		
		
		public BodyBox(World hostWorld, Space space, Vector3f position, Vector3f size, Vector3f force)
		{
			this.hostWorld = hostWorld;
			this.space = space;
			
            bodyID = Ode.dBodyCreate(hostWorld.getID());

            // create a mass object, in this case a box of size 50 x 0.2 x 50
            Ode.dMass mass = new Ode.dMass();
            //Ode.dMassSetBox(ref mass, 200.0f, radius, radius, radius);
			Ode.dMassSetBoxTotal(ref mass, 200.0f, size.x, size.y, size.z);
            // set it's mass to 1000.0f. If this value is too low, 
            // you'll get some wierd collisions
            //mass.mass = 1000.0f;

            // set the mass object on the body
            Ode.dBodySetMass(bodyID, ref mass);

            // Set the body's position 
            Ode.dBodySetPosition(bodyID, position.x, position.y, position.z);

            // Set an initial force on the body. This will be 
            // wiped to zero after the first frame.
            Ode.dBodyAddForce( bodyID, force.x, force.y, force.z );

            // create a collion geometry to go with our rigid body. 
            // without this, the rigid body will not collide with anything.
            geomID = Ode.dCreateBox(space.getSpaceID(), size.x, size.y, size.z);

            // assign a rigid body to the collision geometry. If we didn't do this,
            // the object would be a static object much like our ground plane.
            Ode.dGeomSetBody(geomID, bodyID);
	
            this.position = position.copy();
            this.rotationOGL = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
		}
		
		public void update()
		{
            Ode.dVector3    position = Ode.dGeomGetPosition(geomID);
            Ode.dQuaternion rotation = new Ode.dQuaternion();
            Ode.dGeomGetQuaternion(geomID,ref rotation);
			
			this.position.set(position.X, position.Y, position.Z);
			
			// get the position & rotation of the sphere


            // convert the quaternion to an axis angle so we can put the 
            // rotation into glRotatef()

            float cos_a = rotation.W;
            float angle = (float)(Math.Acos(cos_a) * 2.0f);
            float sin_a = (float)(Math.Sqrt(1.0f - cos_a * cos_a));
            if (Math.Abs(sin_a) < 0.0005f)
                sin_a = 1.0f;
            sin_a = 1.0f / sin_a;

            angle = RAD_TO_DEG(angle);
            float x_axis = rotation.X * sin_a;
            float y_axis = rotation.Y * sin_a;
            float z_axis = rotation.Z * sin_a;

            this.rotationOGL.set(angle, x_axis, y_axis, z_axis);
		}

        private static float RAD_TO_DEG(float a)
        {
            return 57.295779513082320876798154814105f * a;
        }

		public Vector3f getPosition()
		{
			return this.position;
		}

		public Color4f getOGLRotation()
		{
			return rotationOGL;
		}
		
		public void addForce(Vector3f force)
		{
			Ode.dBodyAddForce(bodyID, force.x, force.y, force.z);
		}
	}
}
