// Source.cs created with MonoDevelop
// User: topfs at 7:45 PM 1/31/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenAl;
using Tesla.Common;

namespace Tesla.Audio
{
	internal class Source : IDisposable
	{
		protected int sourceID;
		private static System.Collections.Generic.List<int> sources = new System.Collections.Generic.List<int>();
	
		public Source(Buffer buffer, float rolloff)
		{
			Al.alGenSources(1, out sourceID);
			
	    	Al.alSourcef(sourceID,  Al.AL_PITCH, 1.0f);
	    	Al.alSourcef(sourceID,  Al.AL_GAIN, 1.0f);
	    	Al.alSourcei(sourceID,  Al.AL_BUFFER, buffer.getBuffer());
			Al.alSourcef(sourceID, Al.AL_ROLLOFF_FACTOR, rolloff);

			sources.Add(sourceID);
			
			int error = Alut.alutGetError();
			if (error != Alut.ALUT_ERROR_NO_ERROR)
				throw new Exception(Alut.alutGetErrorString(error));
		}
		
		public static void unload()
		{
			for (int i = 0; i < sources.Count; i++)
			{
				int s = sources[i];
				Al.alSourceStop(s);
				Al.alDeleteSources(1, ref s);
				sources[i] = s;
			}
		}
		
		public void setPosition(Vector3f position)
		{
			Al.alSourcefv(sourceID, Al.AL_POSITION, position.vector);
		}
		
		public void setVelocity(Vector3f velocity)
		{
			Al.alSourcefv(sourceID, Al.AL_VELOCITY, velocity.vector);
		}
		
		public void setRelative(bool relative)
		{
			Al.alSourcei(sourceID, Al.AL_SOURCE_RELATIVE, relative ? Al.AL_TRUE : Al.AL_FALSE);
		}
		                        
		
		public void play()
		{
			Al.alSourcePlay(sourceID);
		}
		
		public void pause()
		{
			Al.alSourcePause(sourceID);
		}
		
		public void stop()
		{
			Al.alSourceStop(sourceID);
		}
		
		public bool isPlaying()
		{
			int state;
			Al.alGetSourcei(sourceID, Al.AL_SOURCE_STATE, out state);
	    
			return (state == Al.AL_PLAYING);
		}

		public void Dispose ()
		{
			Al.alDeleteSources(1, ref sourceID);
		}
	}
}
