// Controller.cs created with MonoDevelop
// User: topfs at 6:35 PM 10/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.GFX
{
	public interface Controller
	{
		bool setActive(bool active); // return what it became, doenst need to be active just because you say so
		bool getActive();
	}
}
