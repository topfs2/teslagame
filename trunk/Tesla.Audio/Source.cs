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
			
	    	Al.alSourcef(sourceID, Al.AL_PITCH, 1.0f);
	    	Al.alSourcef(sourceID, Al.AL_GAIN, 1.0f);
	    	Al.alSourcei(sourceID, Al.AL_BUFFER, buffer.getBuffer());
			Al.alSourcef(sourceID, Al.AL_ROLLOFF_FACTOR, rolloff);
			sources.Add(sourceID);
			
			Check("Constructor");
		}
		
		public static void unload()
		{
			for (int i = 0; i < sources.Count; i++)
			{
				int s = sources[i];
				Al.alSourceStop(s);
				Al.alDeleteSources(1, ref s);
				Check("unload[" + i + "]");
				sources[i] = s;
			}
		}
		
		private static bool Check(string function)
		{
			int error = Alut.alutGetError();
			if (error != Alut.ALUT_ERROR_NO_ERROR)
			{
				Log.Write("Source - Alut error in " + function + ": " + error);
				return false;
			}
			return true;
		}

		public void play(Vector3f position, bool relative)
		{
			Al.alSourcefv(sourceID, Al.AL_POSITION, position.vector);
			Al.alSourcei(sourceID, Al.AL_SOURCE_RELATIVE, relative ? Al.AL_TRUE : Al.AL_FALSE);
			Al.alSourcePlay(sourceID);
			Check("play");
		}
		
		public void pause()
		{
			Al.alSourcePause(sourceID);
			Check("pause");
		}
		
		public void stop()
		{
			Al.alSourceStop(sourceID);
			Check("stop");
		}
		
		public bool isPlaying()
		{
			int state;
			Al.alGetSourcei(sourceID, Al.AL_SOURCE_STATE, out state);
			Check("isPlaying");
			return (state == Al.AL_PLAYING);
		}

		public void Dispose ()
		{
			Al.alDeleteSources(1, ref sourceID);
			Check("Dispose");
		}
	}
}
