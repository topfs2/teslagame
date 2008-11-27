// Landscape2D.cs created with MonoDevelop
// User: topfs at 11:55 PM 11/7/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	public class Landscape2D : Drawable
	{
		static float d = -10, l = -10;				   
		public SuperVertex[] vertrices;
		Texture texture;
		public Landscape2D(Texture texture)
		{
			this.texture = texture;
			Log.Write("Creating heightData");
			Random rand = new Random();
			int max = 40;
			float maxHeightDifference = 1;
			float[] height = new float[max];
			string s = "height: ";
			float last = 0.0f;
			for (int i = 0; i < max; i++)
			{
				height[i] = ((0.5f - (float)rand.NextDouble()) * maxHeightDifference) + last;
				last = height[i];
				s += height[i] + " ";
			}
			Log.Write(s);
			generateTerrain(height);
			generateNormal(height.Length);
			generateTexCoords(height);
		}
		
		private void generateTexCoords(float []height)
		{
			float maxH = 0.0f;
			foreach (SuperVertex v in vertrices)
			{
				if (v.position.y > maxH)
					maxH = v.position.y;
			}
			float scaleX = 2;
			float scaleZ = 1;
			float l = scaleX / (float)height.Length;
			maxH += 10.0f;

			for (int i = 0; i < (height.Length-1); i++)
			{
				vertrices[0 + i*8].texCoord = new Point2f(i*l	 , 0);
				vertrices[1 + i*8].texCoord = new Point2f(i*l	 , 1);
				vertrices[2 + i*8].texCoord = new Point2f((i+1)*l, 1);
				vertrices[3 + i*8].texCoord = new Point2f((i+1)*l, 0);
				
				vertrices[4 + i*8].texCoord = new Point2f(i*l	 , 0);
				vertrices[5 + i*8].texCoord = new Point2f(i*l	 , scaleZ);
				vertrices[6 + i*8].texCoord = new Point2f((i+1)*l, scaleZ);
				vertrices[7 + i*8].texCoord = new Point2f((i+1)*l, 0);
			}
		}
		
		private void generateNormal(int length)
		{
			Point3f norm = new Point3f(0.0f, 0.0f, 1.0f);
			
			for (int i = 0; i < (length-1); i++)
			{
				vertrices[4 + i*8].normal = new Point3f(0, 1, 0);
				vertrices[5 + i*8].normal = new Point3f(0, 1, 0);
				vertrices[6 + i*8].normal = new Point3f(0, 1, 0);
				vertrices[7 + i*8].normal = new Point3f(0, 1, 0);
				
				vertrices[0 + i*8].normal = norm;
				vertrices[1 + i*8].normal = norm;
				vertrices[2 + i*8].normal = norm;
				vertrices[3 + i*8].normal = norm;
			}
		}
		
		private void generateTerrain(float []height)
		{
			Log.Write("generateTerrain");
			Random rand = new Random();
			vertrices = new SuperVertex[((height.Length-1) * 8)];
			for (int i = 0; i < ((height.Length-1) * 8); i++)
				vertrices[i] = new SuperVertex();
			
			for (int i = 0; i < (height.Length-1); i++)
			{
				vertrices[0 + i*8].position = new Point3f(i  , -10.0f   , 0.0f);
				vertrices[1 + i*8].position = new Point3f(i  , height[i], 0.0f);
				vertrices[2 + i*8].position = new Point3f(i+1, height[i+1], 0.0f);
				vertrices[3 + i*8].position = new Point3f(i+1, -10.0f, 0.0f);
				
				vertrices[4 + i*8].position = new Point3f(i, height[i], 0);
				vertrices[5 + i*8].position = new Point3f(i, height[i], 13.0f);
				vertrices[6 + i*8].position = new Point3f(i+1, height[i+1], 13.0f);
				vertrices[7 + i*8].position = new Point3f(i+1, height[i+1], 0);
			}
			foreach ( SuperVertex v in vertrices)
			{
				v.color = new Color4f(1.0f, (float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
			}
		}
		
		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL);
			Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
			//Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			//Gl.glDisable(Gl.GL_TEXTURE_2D);
			texture.Bind();
			Gl.glBegin(Gl.GL_QUADS);
			foreach (SuperVertex v in vertrices)
			{
				Gl.glNormal3fv(v.normal.vector);
				Gl.glTexCoord2f(v.texCoord.x, v.texCoord.y);
				Gl.glColor4f(v.color.r, v.color.g, v.color.b, v.color.a); 
				Gl.glVertex3fv(v.position.vector);
			}
			Gl.glEnd();
		}
	}
}
