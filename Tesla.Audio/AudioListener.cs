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
	public class AudioListener
	{
		Vector3f position, velocity, orientation;
		
		public AudioListener() : this(new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(0.0f, 0.0f, 0.0f))
		{
		}
		
		public AudioListener(Vector3f position, Vector3f velocity, Vector3f orientation)
		{
			setPosition(position);
			setVelocity(velocity);
			setOrientation(orientation);
		}
		
		public void setPosition(Vector3f position)
		{
			this.position = position;
			Al.alListenerfv(Al.AL_POSITION, position.vector);
		}
		
		public void setVelocity(Vector3f velocity)
		{
			this.velocity = velocity;
	    	Al.alListenerfv(Al.AL_VELOCITY, velocity.vector);
		}
		
		public void setOrientation(Vector3f orientation)
		{
			this.velocity = velocity;
	    	Al.alListenerfv(Al.AL_ORIENTATION, orientation.vector);
		}
	}
}
