// FPSCounter.cs created with MonoDevelop
// User: topfs at 1:39 PM 2/4/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla
{
	
	
	public class FPSCounter
	{
		const float updateFPS = 0.1f; //s
		float currentFPS;
		int lastFrame;
		float dTime;
		int dFrames;
		
		public FPSCounter()
		{
			currentFPS = 0;
			lastFrame = System.Environment.TickCount;
			dTime = 0.0f;
			dFrames = 0;
		}
		
		// returns how much time progressed since last update
		public float Update()
		{
			int currentTime = System.Environment.TickCount;
			
			float frameTime = (currentTime - lastFrame) / 1000.0f;
			lastFrame = currentTime;
			
			dFrames++;
			dTime += frameTime;
			if (dFrames > 0 && dTime >= updateFPS)
			{
				currentFPS = (float)dFrames / dTime;
				dFrames = 0;
				dTime = 0;
			}
			
			return frameTime;
		}
		
		public int CurrentFPS
		{
			get { return (int)currentFPS; }
		}
	}
}
