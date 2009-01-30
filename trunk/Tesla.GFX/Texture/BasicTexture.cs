// BasicTexture.cs created with MonoDevelop
// User: topfs at 8:42 PM 1/27/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tao.OpenGl;

namespace Tesla.GFX
{
	public enum TextureFilter
	{
		Nearest,
		Linear,
		MipMap
	}
	
	public class BasicTexture : Texture
	{
		private int textureID;
		private int width, height;
		private TextureFilter textureFilter;
		
		public BasicTexture(String TexturePath) : this(TexturePath, TextureFilter.MipMap)
		{
		}
		
		public BasicTexture(String TexturePath, TextureFilter textureFilter) : this(new Pixmap(TexturePath), textureFilter)
		{
		}
		
		public BasicTexture(Pixmap pixmap) : this(pixmap, TextureFilter.MipMap)
		{
		}
		
		public BasicTexture(Pixmap pixmap, TextureFilter textureFilter)
		{
            int[] TempGL = new int[1];
			Gl.glGenTextures(1, TempGL);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TempGL[0]);

			Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, pixmap.Bpp, pixmap.Width, pixmap.Height, pixmap.getFormat(), Gl.GL_UNSIGNED_BYTE, pixmap.Data);

			textureID = TempGL[0];
			this.textureFilter = textureFilter;
			width = pixmap.Width;
			height = pixmap.Height;
		}
		
		public void RegenerateTexture(Pixmap pixmap)
		{
			if (pixmap.Width > width || pixmap.Height > height)
				throw new NotSupportedException("Cannot regenerate texture if the new texture is bigger than the old");
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
			
			Gl.glTexSubImage2D(Gl.GL_TEXTURE_2D, 0, 0, 0, pixmap.Width, pixmap.Height, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixmap.Data);
		}
		
		public void Bind()
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
			
   			if (GlExtensionLoader.IsExtensionSupported("GL_EXT_texture_filter_anisotropic"))
			{
				float anistropicFiltering = 0.0f;
				Gl.glGetFloatv(Gl.GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT, out anistropicFiltering);
				Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAX_ANISOTROPY_EXT, anistropicFiltering);
			}
			
			if (textureFilter == TextureFilter.Nearest)
            {
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
                   
            }
            else if (textureFilter == TextureFilter.Linear)
            {
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            }
            else
            {
            	Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);
            }
		}
		public void UnBind()
		{
		}
	}
}
