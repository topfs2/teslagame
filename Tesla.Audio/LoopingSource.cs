// LoopingSource.cs created with MonoDevelop
// User: topfs at 8:52 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenAl;

namespace Tesla.Audio
{
	
	
	internal class LoopingSource : Source
	{
		public LoopingSource(Buffer buffer, float rolloff) : base(buffer, rolloff)
		{
			Al.alSourcef(sourceID, Al.AL_LOOPING, Al.AL_TRUE);
		}
	}
}
