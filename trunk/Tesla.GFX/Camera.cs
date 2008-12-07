// Camera.cs created with MonoDevelop
// User: topfs at 10:27 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class Camera
	{
		const double radianFactor = 2 * (float)Math.PI;		
		
		Vector3f position;
		Vector3f lookAt;
		
		bool calculateLookAtPosition;
		public bool Full3D;
		
		Vector3f rightVector, frontVector, upVector;
		
		float pov, ratio;
		float near, far;
		
		float rotatedX, rotatedY, rotatedZ;
		
		public Camera(Vector3f position, float pov, float ratio, float near, float far)
		{
			initialize(position, pov, ratio, near, far);
		}
		
		/*public Camera()
		{
			initialize(new Point3f(0.0f, 0.0f, 0.0f));
		}*/
		
		private void initialize(Vector3f position, float pov, float ratio, float near, float far)
		{
			this.position = position;
			lookAt = new Vector3f(0.0f, 0.0f, 0.0f);
			
			frontVector = new Vector3f(0.0f, 0.0f, 1.0f);
			rightVector = new Vector3f(1.0f, 0.0f, 0.0f);
			upVector    = new Vector3f(0.0f, 1.0f, 0.0f);
			calculateLookAtPosition = true;
			
			rotatedX = rotatedY = rotatedZ = 0.0f;
			
			this.pov   = pov;
			this.ratio = ratio;
			this.near  = near;
			this.far   = far;
			
			this.Full3D = false;
		}
		
		public float POV
		{ get { return pov; } }
		
		public float Near
		{ get { return near; } }
		
		public float Far
		{ get { return far; } }
		
		public float Ratio
		{ get { return ratio; } }
		
		
		private static float PIdiv180 = (float)Math.PI / 180.0f;
		
		public void rotateX (float angle)
		{
			rotatedX += angle;
			
			//Rotate viewdir around the right vector:
			frontVector = frontVector * (float)Math.Cos(angle*PIdiv180)	+ upVector*(float)Math.Sin(angle*PIdiv180);
			frontVector.Normalize();

			//now compute the new UpVector (by cross product)
			upVector = rightVector.Cross(frontVector);
		}

		public void rotateY (float angle)
		{
			rotatedY += angle;
			
			//Rotate viewdir around the up vector:
			frontVector = frontVector * (float)Math.Cos(angle*PIdiv180) + rightVector*(float)Math.Sin(angle*PIdiv180);
			frontVector.Normalize();

			Vector3f up;
			if (Full3D)
				up = upVector;
			else
				up = new Vector3f(0.0f, 1.0f, 0.0f);

			//now compute the new RightVector (by cross product)
			rightVector = up.Cross(frontVector);
		}

		public void rotateZ (float angle)
		{
			rotatedZ += angle;
			
			//Rotate viewdir around the right vector:
			rightVector = rightVector*(float)Math.Cos(angle*PIdiv180) + upVector*(float)Math.Sin(angle*PIdiv180);

			//now compute the new UpVector (by cross product)
			upVector = rightVector.Cross(frontVector);
		}
		
		public void stepForward(float step)
		{
			position.add(frontVector * step);			
		}
		
		public void stepSideway(float step)
		{System.Console.Out.WriteLine(rightVector.ToString() + "*" + step + "=" + (rightVector * step).ToString());
			position.add(rightVector * step);
		}
		
		public void stepUp(float step)
		{
			position.add(upVector * step);
		}
		
		public Vector3f getPosition()
		{
			return position;
		}
		
		public Vector3f getLookAtPosition()
		{
			return lookAt;
		}
		
		public Vector3f getFrontVector()
		{
			return frontVector;
		}
		
		public Vector3f getUpVector()
		{
			return upVector;
		}
		
		public Vector3f getRightVector()
		{
			return rightVector;
		}
		
		public void linkPosition(Vector3f position)
		{
			this.position = position;
		}
		
		public void setCamera()
		{
			Vector3f viewPos, up;
			if (this.calculateLookAtPosition)
				viewPos = position + frontVector;
			else
				viewPos = this.lookAt.copy();
				
			if (Full3D)
				up = upVector;
			else
				up = new Vector3f(0.0f, 1.0f, 0.0f);
			Glu.gluLookAt(position.x, position.y, position.z, viewPos.x, viewPos.y, viewPos.z, up.x, up.y, up.z);
		}
		
		public void linkLookAtPosition(Vector3f position)
		{
			calculateLookAtPosition = false;
			Log.Write("locking LookAt was " + lookAt.ToString());
			this.lookAt = position;
		}
		
		public void unlinkLookAtPosition()
		{
			Log.Write("unlocking LookAt was " + lookAt.ToString());
			calculateLookAtPosition = true;
			Vector3f tmp = new Vector3f(lookAt.x, lookAt.y, lookAt.z);
			lookAt = tmp;
		}
		
		public static void test()
		{
			Camera c = new Camera(new Vector3f(0.0f, 0.0f, 0.0f), 45.0f, 1.3333f, 0.1f, 100.0f);
			Check.AssertEquals(c.getFrontVector(), 	new Vector3f(0.0f, 0.0f, 1.0f));
			Check.AssertEquals(c.getUpVector(),		new Vector3f(0.0f, 1.0f, 0.0f));
			Check.AssertEquals(c.getRightVector(),  new Vector3f(1.0f, 0.0f, 0.0f));
			Check.AssertEquals(c.getPosition(), new Vector3f(0.0f, 0.0f, 0.0f));
			
			c.stepForward(1.0f);
			Check.AssertEquals(c.getPosition(), new Vector3f(0.0f, 0.0f, 1.0f));
			c.stepSideway(1.0f);
			Check.AssertEquals(c.getPosition(), new Vector3f(1.0f, 0.0f, 1.0f));
			c.stepUp(1.0f);
			Check.AssertEquals(c.getPosition(), new Vector3f(1.0f, 1.0f, 1.0f));
			
			int r = 7;
			c.rotateY(180);
			Vector3f v0 = c.getFrontVector();
			v0 = new Vector3f((float)Math.Round(v0.x,r), (float)Math.Round(v0.y,r), (float)Math.Round(v0.z,r));
			Check.AssertEquals(v0, new Vector3f(0.0f, 0.0f, -1.0f));
			v0 = c.getRightVector();
			v0 = new Vector3f((float)Math.Round(v0.x,r), (float)Math.Round(v0.y,r), (float)Math.Round(v0.z,r));
			Check.AssertEquals(v0,  new Vector3f(-1.0f, 0.0f, 0.0f));
			
			                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      c.stepSideway(1.0f);
			Check.AssertEquals(c.getPosition(), new Vector3f(0.0f, 1.0f, 1.0f));
			
			c.rotateY(180);
			v0 = c.getFrontVector();
			v0 = new Vector3f((float)Math.Round(v0.x,r), (float)Math.Round(v0.y,r), (float)Math.Round(v0.z,r));
			Check.AssertEquals(v0, 	new Vector3f(0.0f, 0.0f, 1.0f));
			v0 = c.getRightVector();
			v0 = new Vector3f((float)Math.Round(v0.x,r), (float)Math.Round(v0.y,r), (float)Math.Round(v0.z,r));
			Check.AssertEquals(v0,  new Vector3f(1.0f, 0.0f, 0.0f));
			

		}
	}
}
