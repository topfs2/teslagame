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
		const double radianFactor = 2 * 3.1415926535;		
		
		Point3f position;
		Point3f lookAt;
		
		bool calculateLookAtPosition;
		public bool Full3D;
		
		Point3f rightVector, frontVector, upVector;
		
		float pov, ratio;
		float near, far;
		
		float rotatedX, rotatedY, rotatedZ;
		
		public Camera(Point3f position, float pov, float ratio, float near, float far)
		{
			initialize(position, pov, ratio, near, far);
		}
		
		/*public Camera()
		{
			initialize(new Point3f(0.0f, 0.0f, 0.0f));
		}*/
		
		private void initialize(Point3f position, float pov, float ratio, float near, float far)
		{
			this.position = position;
			lookAt = new Point3f(0.0f, 0.0f, 0.0f);
			
			frontVector = new Point3f(0.0f, 0.0f, 1.0f);
			rightVector = new Point3f(1.0f, 0.0f, 0.0f);
			upVector    = new Point3f(0.0f, 1.0f, 0.0f);
			calculateLookAtPosition = true;
			
			rotatedX = rotatedY = rotatedZ = 0.0f;
			
			this.pov   = pov;
			this.ratio = ratio;
			this.near  = near;
			this.far   = far;
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
			if (Full3D)
				upVector = rightVector.Cross(frontVector);

			
		}

		public void rotateY (float angle)
		{
			rotatedY += angle;
			
			//Rotate viewdir around the up vector:
			frontVector = frontVector * (float)Math.Cos(angle*PIdiv180) + rightVector*(float)Math.Sin(angle*PIdiv180);
			frontVector.Normalize();

			//now compute the new RightVector (by cross product)
			rightVector = upVector.Cross(frontVector) * -1;
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
		{
			position.add(rightVector * step);
		}
		
		public void stepUp(float step)
		{
			position.add(upVector * step);
		}
		
		public Point3f getPosition()
		{
			return position;
		}
		
		public Point3f getLookAtPosition()
		{
			return lookAt;
		}
		
		public Point3f getFrontVector()
		{
			return frontVector;
		}
		
		public Point3f getUpVector()
		{
			return upVector;
		}
		
		public Point3f getRightVector()
		{
			return rightVector;
		}
		
		public void linkPosition(Point3f position)
		{
			this.position = position;
		}
		
		public void setCamera()
		{
			Point3f viewPos;
			if (this.calculateLookAtPosition)
				viewPos = position + frontVector;
			else
				viewPos = this.lookAt.copy();
			
			Glu.gluLookAt(position.x, position.y, position.z, viewPos.x, viewPos.y, viewPos.z, upVector.x, upVector.y, upVector.z);
		}
		
		public void linkLookAtPosition(Point3f position)
		{
			calculateLookAtPosition = false;
			Log.Write("locking LookAt was " + lookAt.ToString());
			this.lookAt = position;
		}
		
		public void unlinkLookAtPosition()
		{
			Log.Write("unlocking LookAt was " + lookAt.ToString());
			calculateLookAtPosition = true;
			Point3f tmp = new Point3f(lookAt.x, lookAt.y, lookAt.z);
			lookAt = tmp;
		}
	}
}
