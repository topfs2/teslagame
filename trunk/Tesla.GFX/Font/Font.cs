// Font.cs created with MonoDevelop
// User: topfs at 7:29 AM 11/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX.Font
{
	public interface Font
	{
		void Draw(string text, Vector2f position, Color4f color);
	}
}
