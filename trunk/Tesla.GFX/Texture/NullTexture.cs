// NullTexture.cs created with MonoDevelop
// User: topfs at 9:18 PM 1/27/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
namespace Tesla.GFX
{
	public class NullTexture : Texture
	{
		public void Bind()
		{
		}
		public void UnBind()
		{
		}

		public float Width ()
		{
			return 0;
		}

		public float Height ()
		{
			return 0;
		}
	}
}