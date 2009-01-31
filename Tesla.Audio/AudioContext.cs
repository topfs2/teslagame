// AudioContext.cs created with MonoDevelop
// User: topfs at 7:58 PM 1/31/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenAl;

namespace Tesla.Audio
{
	public class AudioContext
	{
		public AudioContext()
		{
		}
		
		public static void initialize()
		{
	    	Al.alGetError();
			Alut.alutInit();
		}
		
		public static void deinitialize()
		{
			AudioSource.unload();
			AudioData.unload();
			Alut.alutExit();
		}
		
		public static bool checkForError()
		{
			int error = Al.alGetError();
			if (error != Al.AL_NO_ERROR)
				throw new Exception(Alut.alutGetErrorString(error));
			else
				return true;
		}
	}
}
