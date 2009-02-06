// Listener.cs created with MonoDevelop
// User: topfs at 8:03 PM 1/31/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenAl;
using Tesla.Common;

namespace Tesla.Audio
{
	public class Listener
	{		
		public Listener() : this(new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(0.0f, 0.0f, -1.0f), new Vector3f(0.0f, 1.0f, 0.0f))
		{
		}
		
		public Listener(Vector3f position, Vector3f velocity, Vector3f frontVector, Vector3f upVector)
		{
			setPosition(position);
			setVelocity(velocity);
			setOrientation(frontVector, upVector);
		}
		
		public void setPosition(Vector3f position)
		{
			Al.alListenerfv(Al.AL_POSITION, position.vector);
		}
		
		public void setVelocity(Vector3f velocity)
		{
	    	Al.alListenerfv(Al.AL_VELOCITY, velocity.vector);
		}
		
		public void setOrientation(Vector3f frontVector, Vector3f upVector)
		{
			float[] orientation = new float[6];
			orientation[0] = frontVector.x;
			orientation[1] = frontVector.y;
			orientation[2] = frontVector.z;
			orientation[3] = upVector.x;
			orientation[4] = upVector.y;
			orientation[5] = upVector.z;
	    	Al.alListenerfv(Al.AL_ORIENTATION, orientation);
		}
	}
}
