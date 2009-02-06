// AudioContext.cs created with MonoDevelop
// User: topfs at 7:58 PM 1/31/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;
using Tao.OpenAl;

namespace Tesla.Audio
{
	public class AudioContext : IDisposable
	{
		private static bool SubSystemsInitialized = false;
		
		public AudioContext(Configuration c)
		{
	    	Al.alGetError();
			if (!SubSystemsInitialized)
			{
				Alut.alutInit();
				SubSystemsInitialized = true;
			}
			
			Check();
		}
		
		public void Dispose()
		{
			AudioSource.unload();
			AudioBuffer.unload();
			Alut.alutExit();
		}
		
		private bool Check()
		{
			int error = Al.alGetError();
			if (error != Al.AL_NO_ERROR)
				throw new Exception(Alut.alutGetErrorString(error));
			else
				return true;
		}
		
		
	}
}
