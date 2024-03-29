// Frustum.cs created with MonoDevelop
// User: topfs at 9:01 PM 11/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class Frustum : Drawable
	{
		Plane left, top, right, bottom, far, near;
		Camera camera;
		float nearWidth, nearHeight, farWidth, farHeight;
	
		/* DEBUG */
		Vector3f ftl, ftr, fbl, fbr, ntl, ntr, nbl, nbr, fc, nc;
	
	
		private static float ANG2RAD = 3.14159265358979323846f/180.0f;
		
		public Frustum(Camera camera)
		{
			this.camera = camera;
			//this.camera = camera;
			// compute width and height of the near and far plane sections
			float tang = (float)Math.Tan(ANG2RAD * camera.POV * 0.5) ;
			nearHeight = camera.Near * tang;
			nearWidth  = nearHeight  * camera.Ratio; 
			farHeight  = camera.Far  * tang;
			farWidth   = farHeight   * camera.Ratio;
			calculateFrustum();
		}
		
		public void calculateFrustum()
		{
			Vector3f p = camera.getPosition().copy();
			//Point3f ftl, ftr, fbl, fbr, ntl, ntr, nbl, nbr;
			Vector3f Z = camera.getFrontVector().copy();
			Vector3f X = camera.getRightVector().copy();
			Vector3f Y = camera.getUpVector().copy();

			// compute the centers of the near and far planes
			nc = p + Z * camera.Near;
			fc = p + Z * camera.Far;

			// compute the 4 corners of the frustum on the near plane
			ntl = nc + Y * nearHeight - X * nearWidth;
			ntr = nc + Y * nearHeight + X * nearWidth;
			nbl = nc - Y * nearHeight - X * nearWidth;
			nbr = nc - Y * nearHeight + X * nearWidth;

			// compute the 4 corners of the frustum on the far plane
			ftl = fc + Y * farHeight - X * farWidth;
			ftr = fc + Y * farHeight + X * farWidth;
			fbl = fc - Y * farHeight - X * farWidth;
			fbr = fc - Y * farHeight + X * farWidth;

			this.right  = new Plane(p, ftr, fbr);
			this.left   = new Plane(p, fbl, ftl);
			
			this.top    = new Plane(p, ftl, ftr);
			this.bottom = new Plane(p, fbr, fbl);
			
			this.far    = new Plane(ftr, ftl, fbr);
			this.near   = new Plane(ntl, ntr, nbl);
			

			// compute the six planes
			// the function set3Points assumes that the points
			// are given in counter clockwise order
			/*pl[TOP].set3Points(ntr,ntl,ftl);
			pl[BOTTOM].set3Points(nbl,nbr,fbr);
			pl[LEFT].set3Points(ntl,nbl,fbl);
			pl[RIGHT].set3Points(nbr,ntr,fbr);
			pl[NEARP].set3Points(ntl,ntr,nbr);
			pl[FARP].set3Points(ftr,ftl,fbl);*/
		}
		
		public bool inFrustum(Sphere sphere)
		{
			if (left.distanceTo(sphere.position) < -sphere.radius)
				return false;
			if (right.distanceTo(sphere.position) < -sphere.radius)
				return false;
			if (top.distanceTo(sphere.position) < -sphere.radius)
				return false;
			if (bottom.distanceTo(sphere.position) < -sphere.radius)
				return false;
			if (far.distanceTo(sphere.position) < -sphere.radius)
				return false;
			if (near.distanceTo(sphere.position) < -sphere.radius)
				return false;
				
			return true;
		}
		
		public bool inFrustum(Vector3f position)
		{
			if (left.distanceTo(position) < 0)
				return false;
			if (top.distanceTo(position) < 0)
				return false;
			if (right.distanceTo(position) < 0)
				return false;
			if (bottom.distanceTo(position) < 0)
				return false;
			if (far.distanceTo(position) < 0)
				return false;
			if (near.distanceTo(position) < 0)
				return false;
				
			return true;
		}
		
		
		
		public void Draw (float frameTime, Frustum frustum)
		{		
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			Gl.glBegin(Gl.GL_TRIANGLES);
			Gl.glVertex3fv(camera.getPosition().vector);
			Gl.glVertex3fv(fbl.vector);
			Gl.glVertex3fv(fbr.vector);
			
			Gl.glVertex3fv(camera.getPosition().vector);
			Gl.glVertex3fv(ftr.vector);
			Gl.glVertex3fv(ftl.vector);
			Gl.glEnd();
			
			Gl.glColor3f(1.0f, 1.0f, 0.0f);
			Gl.glBegin(Gl.GL_LINES);
			Gl.glVertex3fv(ftr.vector);
			Gl.glVertex3f(right.a + ftr.x, right.b + ftr.y, right.c + ftr.z);
			Gl.glColor3f(0.0f, 1.0f, 0.0f);
			Gl.glVertex3fv(ftl.vector);
			Gl.glVertex3f(left.a + ftl.x, left.b + ftl.y, left.c + ftl.z);
			Gl.glColor3f(0.0f, 1.0f, 1.0f);
			Gl.glVertex3fv(fc.vector);
			Gl.glVertex3f(far.a + fc.x, far.b + fc.y, far.c + fc.z);
			Gl.glVertex3fv(nc.vector);
			Gl.glVertex3f(near.a + nc.x, near.b + nc.y, near.c + nc.z);
			
			Gl.glColor3f(1.0f, 0.0f, 0.0f);
			Gl.glVertex3fv(fc.vector);
			Gl.glVertex3f(top.a + fc.x, top.b + fc.y, top.c + fc.z);
			Gl.glColor3f(0.0f, 0.0f, 1.0f);
			Gl.glVertex3fv(fc.vector);
			Gl.glVertex3f(bottom.a + fc.x, bottom.b + fc.y, bottom.c + fc.z);
			
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			Gl.glVertex3fv(camera.getPosition().vector);
			Gl.glVertex3fv((camera.getPosition() + camera.getFrontVector()*camera.Far).vector);
			
			Gl.glEnd();
			
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor4f(0.0f, 0.0f, 1.0f, 1.0f);
			Gl.glVertex3fv(ftl.vector);
			Gl.glVertex3fv(ftr.vector);
			Gl.glVertex3fv(fbr.vector);
			Gl.glVertex3fv(fbl.vector);
			
			Gl.glVertex3fv(ntl.vector);
			Gl.glVertex3fv(ntr.vector);
			Gl.glVertex3fv(nbr.vector);
			Gl.glVertex3fv(nbl.vector);
			Gl.glEnd();
		}
		
		public static void test()
		{
			Camera c = new Camera(new Vector3f(0.0f, 0.0f, 0.0f), 90.0f, 1.0f, 1.0f, 10.0f);
			Frustum f = new Frustum(c);
			Check.AssertEquals(f.fc, new Vector3f(0.0f, 0.0f, -10.0f));
			Check.AssertEquals(f.fbl, new Vector3f(-10.0f,-10.0f, -10.0f));
			Check.AssertEquals(f.fbr, new Vector3f( 10.0f,-10.0f, -10.0f));
			Check.AssertEquals(f.ftl, new Vector3f(-10.0f, 10.0f, -10.0f));
			Check.AssertEquals(f.ftr, new Vector3f( 10.0f, 10.0f, -10.0f));

			Check.AssertEquals("Testing p", f.inFrustum(new Vector3f(0.0f, 0.0f, -5.0f)), true);
			Check.AssertEquals(f.inFrustum(new Vector3f(10.0f, 10.0f, -10.0f)), true);
			Check.AssertEquals(f.inFrustum(new Vector3f(1.0f, 1.0f, -1.0f)), true);
			
			Check.AssertEquals(f.inFrustum(new Vector3f(0.0f, 0.0f, 0.0f)), false);
			Check.AssertEquals(f.inFrustum(new Vector3f(0.0f, 0.0f, 20.0f)), false);
			Check.AssertEquals(f.inFrustum(new Vector3f(-20.0f, -20.0f, 20.0f)), false);
		}
	}
}
