// Space.cs created with MonoDevelop
// User: topfs at 12:56 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.Ode;

namespace Tesla.Physics
{
	public class Space
	{
		IntPtr spaceID;
		IntPtr contactGroup;
		World  hostWorld;
		
		public Space(World world, IntPtr contactGroup)
		{
			this.hostWorld = world;
			this.contactGroup = contactGroup;
			spaceID = Ode.dSimpleSpaceCreate(spaceID);
		}
		
		public IntPtr getSpaceID()
		{
			return spaceID;
		}
		
        public void NearCallback(IntPtr data, IntPtr Geom1, IntPtr Geom2)
        {
        	int MAX_COLLISIONS = 20;
            IntPtr body1 = Ode.dGeomGetBody(Geom1);
            IntPtr body2 = Ode.dGeomGetBody(Geom2);

            if (body1 != IntPtr.Zero
                && body2 != IntPtr.Zero
                && Ode.dAreConnectedExcluding(body1, body2, (int)Ode.dJointTypes.dJointTypeContact) == 1)
                return;

            Ode.dContactGeom[] contactGeoms = new Ode.dContactGeom[MAX_COLLISIONS];

            int numContacts = Ode.dCollide(Geom1, Geom2, MAX_COLLISIONS, contactGeoms,
                System.Runtime.InteropServices.Marshal.SizeOf(contactGeoms[0]));

            Ode.dContact[] contacts = new Ode.dContact[numContacts];
            for(int i = 0; i < numContacts; i++)
            {
                contacts[i].surface.mode = (int)(Ode.dContactFlags.dContactApprox1);
                contacts[i].surface.mu = Ode.dInfinity;
                contacts[i].surface.mu2 = 0;
                contacts[i].surface.bounce = 0.01f;
                contacts[i].surface.bounce_vel = 0.01f;
                //contacts[i].surface.soft_erp = 0.5f;
                contacts[i].surface.soft_cfm = 0.001f;
                contacts[i].geom = contactGeoms[i];

                IntPtr joint = Ode.dJointCreateContact(hostWorld.getID(), contactGroup, ref contacts[i]);
                Ode.dJointAttach(joint, body1, body2);
            }
        }
	}
}
