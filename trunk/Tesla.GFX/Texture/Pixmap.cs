// Pixmap.cs created with MonoDevelop
// User: topfs at 6:43 PM 1/28/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Runtime.InteropServices;
using Tao.OpenGl;
using Tao.DevIl;

namespace Tesla.GFX
{
	public class Pixmap
	{
		private int width, height, bpp;
		public byte[] data;
		private int format;

		public Pixmap(byte[] data, int width, int height, int bpp, int format)
		{
			this.data = data;
			this.width = width;
			this.height = height;
			this.bpp = bpp;
			this.format = format;
		}
		
		public Pixmap(String Path)
		{
            if (!System.IO.File.Exists(Path))
                throw new System.IO.IOException("File doesn't exist: " + Path);

			int ilImage;
            Il.ilGenImages(1, out ilImage);
            Il.ilBindImage(ilImage);

            if (Il.ilLoad(Il.IL_TYPE_UNKNOWN, Path))
            {
                Il.ilConvertImage(Il.IL_RGBA, Il.IL_UNSIGNED_BYTE);
                
                width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                bpp = Il.ilGetInteger(Il.IL_IMAGE_BPP);
                data = new byte[width * height * 4];
                
                format = Gl.GL_RGBA;
				Marshal.Copy(Il.ilGetData(), data, 0, width * height * 4);
				
				Il.ilDeleteImages(1, ref ilImage);
            }
            else
                throw new System.IO.IOException("Error While Loading: " + Path);
		}
		
		public int Width
		{
			get
			{
				return width;
			}
		}
		
		public int Height
		{
			get
			{
				return height;
			}
		}
		
		public int Bpp
		{
			get
			{
				return bpp;
			}
		}
		
		public byte[] Data
		{
			get
			{
				return data;
			}
		}
		
		public int getFormat()
		{
			return format;
		}
	}
}
