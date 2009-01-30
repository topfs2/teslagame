// CubeMapTexture.cs created with MonoDevelop
// User: topfs at 5:40 PM 1/30/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using Tesla.Common;

using Tao.OpenGl;

namespace Tesla.GFX
{
	public enum CubeMapType
	{
		Reflective,
		None
	}

	public class CubeMapTexture : Texture
	{
		private int[] cube = {	Gl.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
								Gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
								Gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
								Gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
								Gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
								Gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z	};
		private string[] path = {	"positive_x",
									"negative_x",
									"positive_y",
									"negative_y",
									"positive_z",
									"negative_z"	};
		private int[] textures;
		private CubeMapType type;
				
		public CubeMapTexture(String texturePath)
		{
			if (!texturePath.EndsWith("/"))
				texturePath += "/";

			textures = new int[1];
			Gl.glGenTextures(1, textures);
			Gl.glBindTexture(Gl.GL_TEXTURE_CUBE_MAP, textures[0]);

			Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
			Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);
			Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_TEXTURE_WRAP_R, Gl.GL_CLAMP_TO_EDGE);

			Pixmap pixmap;

			for(int i = 0; i < 6; i++)
			{
			    // Load this texture map
			    Gl.glTexParameteri(Gl.GL_TEXTURE_CUBE_MAP, Gl.GL_GENERATE_MIPMAP, Gl.GL_TRUE);
			    pixmap = new Pixmap(texturePath + path[i] + ".bmp");
			    Gl.glTexImage2D(cube[i], 0, pixmap.Bpp, pixmap.Width, pixmap.Height, 0, pixmap.getFormat(), Gl.GL_UNSIGNED_BYTE, pixmap.Data);
			}
		}
		
		public void setCubeMapType(CubeMapType type)
		{
			this.type = type;
		}
		
		public void Bind()
		{
			Gl.glEnable(Gl.GL_TEXTURE_CUBE_MAP);
			Gl.glBindTexture(Gl.GL_TEXTURE_CUBE_MAP, textures[0]);
			
			if (type == CubeMapType.Reflective)
			{
				Matrix44 m, invert;
				
				float[] modelview = new float[16];
				Gl.glGetFloatv(Gl.GL_MODELVIEW_MATRIX , modelview);
				
				m = new Matrix44(modelview);
				
				invert = m.invert();
				Gl.glMatrixMode(Gl.GL_TEXTURE);
	    		Gl.glPushMatrix();
				Gl.glMultMatrixf(invert.getVector());

				Gl.glTexGeni(Gl.GL_S, Gl.GL_TEXTURE_GEN_MODE, Gl.GL_REFLECTION_MAP);
				Gl.glTexGeni(Gl.GL_T, Gl.GL_TEXTURE_GEN_MODE, Gl.GL_REFLECTION_MAP);
				Gl.glTexGeni(Gl.GL_R, Gl.GL_TEXTURE_GEN_MODE, Gl.GL_REFLECTION_MAP);

				Gl.glEnable(Gl.GL_TEXTURE_GEN_S);
				Gl.glEnable(Gl.GL_TEXTURE_GEN_T);
				Gl.glEnable(Gl.GL_TEXTURE_GEN_R);
			}
		}
		
		public void UnBind()
		{
			if (type == CubeMapType.Reflective)
			{
				
				Gl.glDisable(Gl.GL_TEXTURE_GEN_S);
				Gl.glDisable(Gl.GL_TEXTURE_GEN_T);
				Gl.glDisable(Gl.GL_TEXTURE_GEN_R);
			}
			Gl.glDisable(Gl.GL_TEXTURE_CUBE_MAP);

			Gl.glPopMatrix();
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
		}
	}
}
