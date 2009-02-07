// Ambient.cs created with MonoDevelop
// User: topfs at 8:56 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.Audio
{
	public class Ambient : IDisposable
	{
		Buffer audioBuffer;
		LoopingSource source;

		public Ambient(string fileName)
		{
			audioBuffer = new Buffer(fileName);
			source = new LoopingSource(audioBuffer, 1.0f);
			source.play(new Vector3f(), true);

		}

		public void Dispose ()
		{
			source.stop();
			source.Dispose();
			audioBuffer.Dispose();
		}
	}
}
