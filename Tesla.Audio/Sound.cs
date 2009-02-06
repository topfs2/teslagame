// SimpleSound.cs created with MonoDevelop
// User: topfs at 3:19 PM 2/6/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;

using Tesla.Common;

namespace Tesla.Audio
{
	
	
	public class Sound
	{
		Queue<Source> sources;
		Buffer audioBuffer;
		public Sound(string fileName) : this(fileName, 1)
		{
		}
		
		public Sound(string fileName, int numberOfSourcesHint)
		{
			audioBuffer = new Buffer(fileName);
			sources = new Queue<Source>(numberOfSourcesHint);
		}
		
		public void play(Vector3f position, Vector3f velocity)
		{
			Source source;
			if (sources.Count == 0 || sources.Peek().isPlaying())
				source = new Source(audioBuffer, 1.0f);
			else
				source = sources.Dequeue();
			
			source.setPosition(position);
			source.setVelocity(velocity);
			source.play();
			
			sources.Enqueue(source);

			Console.WriteLine("NbrSources: " + sources.Count);
		}
		
	}
}
