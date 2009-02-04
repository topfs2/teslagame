// Texture.cs created with MonoDevelop
// User: topfs at 6:42 PM 1/28/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace Tesla.GFX
{
	
	
	public interface Texture
	{
		void Bind();
		void TexCoord(float s, float t);
		void UnBind();
	}
}