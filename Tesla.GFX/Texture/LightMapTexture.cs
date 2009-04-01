// LightMapTexture.cs created with MonoDevelop
// User: topfs at 5:52 PM 4/1/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	public class LightMapTexture : Texture
	{
		private int textureID;
		private int size;
		
		public LightMapTexture(List<Light> lights, Vector3f center, Vector3f frontVector, Vector3f rightVector, int textureSize, float realSize)
		{
			size = textureSize;
			int halfSize  = size  / 2;
			float halfRealSize = realSize / 2.0f;

			System.Collections.Generic.List<float> data = new System.Collections.Generic.List<float>();
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					float x = halfRealSize * ((float)(halfSize - i) / (float)halfSize);
					float y = halfRealSize * ((float)(halfSize - j) / (float)halfSize);
					Vector3f v = frontVector * y + rightVector * x;
					v.add(center);

					float r, g, b;
					r = g = b = 0.0f;
					for (int l = 0; l < lights.Count; l++)
					{
						Color4f color = lights[l].calculateColor(v);
						r += color.r;
						g += color.g;
						b += color.b;
					}
					r = r > 1.0f ? 1.0f : r;
					g = g > 1.0f ? 1.0f : g;
					b = b > 1.0f ? 1.0f : b;
					
					data.Add(r);
					data.Add(g);
					data.Add(b);
				}
			}
			
            int[] TempGL = new int[1];

			Gl.glGenTextures(1, TempGL);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TempGL[0]);

			Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, 3, size, size, Gl.GL_RGB, Gl.GL_FLOAT, data.ToArray());

			textureID = TempGL[0];
		}

		public void Bind()
		{
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
		}

		public void UnBind()
		{
		}

		public float Width ()
		{
			return size;
		}

		public float Height ()
		{
			return size;
		}
	}
}
