// SimpleFontHandler.cs created with MonoDevelop
// User: topfs at 7:39 AM 11/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;

using Tesla.Common;

namespace Tesla.GFX.Font
{
	
	
	public class SimpleFontHandler : Drawable2D
	{
		Font font;
		public string text;
		public Point2f position;
		public Color4f color;
		
		public SimpleFontHandler(Font font, string text, Point2f position, Color4f color)
		{
			this.font = font;
			this.position = position;
			this.color = color;
			this.text = text;
		}
		
		public void Draw(float frameTime)
		{
			font.Draw(text, position, color); 
		}
	}
}
