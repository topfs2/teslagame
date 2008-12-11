// Texture.cs created with MonoDevelop
// User: topfs at 12:37 PM 9/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
using System.Collections.Generic;
using Tao.OpenGl;
using System.Collections;
using Tesla.Common;

namespace Tesla.GFX
{	
	public enum TextureFilter
	{
		Nearest,
		Linear,
		MipMap
	}

	public class Texture
	{
		private static System.Collections.Generic.Dictionary<int, Texture> LoadedTextures = new System.Collections.Generic.Dictionary<int,Texture>();
		private int TextureIndex;
		
		private Texture(Bitmap image, TextureFilter textureFilter, int wrapMode)
		{
			image.RotateFlip(RotateFlipType.Rotate90FlipNone);
			Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
			List<byte> color = new List<byte>();
			for (int i = 0; i < image.Width; i++)
			{
				for (int j = 0; j < image.Height; j++)
				{
					color.Add(image.GetPixel(i, j).R);
					color.Add(image.GetPixel(i, j).G);
					color.Add(image.GetPixel(i, j).B);
					color.Add(image.GetPixel(i, j).A);
				}	
			}
			

			int []texture = new int[1];
			Gl.glGenTextures(1, texture);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, wrapMode);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, wrapMode);
			// Create Linear Filtered Texture

			if (textureFilter == TextureFilter.Nearest)
			{
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
				Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGBA, image.Width, image.Height, 0, (int)Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, color.ToArray());
			}
			else if (textureFilter == TextureFilter.Linear)
			{
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
				Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGBA, image.Width, image.Height, 0, (int)Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, color.ToArray());
			}
			else if (textureFilter == TextureFilter.MipMap)
			{
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
				Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
				Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, (int)Gl.GL_RGBA, image.Width, image.Height, (int)Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, color.ToArray());
			}

			image.Dispose();
			this.TextureIndex = texture[0];
		}
		
		private Texture(string TexturePath)
		{
			throw new NotImplementedException();
		}
		public static Texture CreateFromFile(string Path)
		{
			return CreateFromFile(Path, TextureFilter.MipMap, Gl.GL_REPEAT);
		}
		public static Texture CreateFromFile(string Path, TextureFilter textureFilter, int wrapMode)
		{
			Texture t = null;
			if (!LoadedTextures.TryGetValue(Path.GetHashCode(), out t))
			{
				Log.Write("Texture: Could not find texture in dictionary " + Path + ", Creating and adding a new one");
				t = new Texture(new Bitmap(Path), textureFilter, wrapMode);
				LoadedTextures.Add(Path.GetHashCode(), t); 
			}
			
			return t;
		}
		
		public void Bind()
		{
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureIndex);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
		}
	}
}
