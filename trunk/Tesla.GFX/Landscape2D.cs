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
		//static float d = -10, l = -10;				   
		public SuperVertex[,] vertrices;
		Texture textureWall, textureGround;
		
			int width = 40;
			int depth = 20;		
		
		public Landscape2D(Texture textureWall, Texture textureGround)
		{
			this.textureWall = textureWall;
			this.textureGround = textureGround;
			Log.Write("Creating heightData");
			Random rand = new Random();

			float maxHeightDifference = 0.75f;
			float maxScramble = 0.3f;
			vertrices = new SuperVertex[width, depth];
			float last = 0.0f;
			float ratio = (float)width / (float)depth;
			int mul = 2;
			for (int i = 0; i < width; i++)
			{
				float h = ((0.5f - (float)rand.NextDouble()) * maxHeightDifference) + last;
				
				for (int j = 0; j < depth; j++)
				{
					vertrices[i, j] = new SuperVertex();
					vertrices[i, j].position = new Vector3f(i, h + ((0.5f - (float)rand.NextDouble()) * maxScramble), -j);
					vertrices[i, j].texCoord = new Vector2f((float)(i * mul) / (float)depth, ((float)(j * mul) / (float)depth));
				}
				last = h;
			}
			
			calculateNormals();
		}
		
		private void calculateNormals()
		{
			Vector3f vecA, vecB, vecC, vecD, normA, normB, normC, normD;
			for (int z = 1; z < (depth - 1); z++)
			{
				for (int x = 1; x < (width - 1); x++)
				{
					Vector3f p = vertrices[x , z].position;
					vecA = vertrices[x-1 , z ].position.diff(p);
					vecB = vertrices[x   ,z-1].position.diff(p);
					vecC = vertrices[x+1 ,z  ].position.diff(p);
					vecD = vertrices[x   ,z+1].position.diff(p);
					
					normA = vecA.Cross(vecD);
					normB = vecD.Cross(vecC);
					normC = vecC.Cross(vecB);
					normD = vecB.Cross(vecA);
					
					vertrices[x , z].normal = normA.add(normB).add(normC).add(normD);
					vertrices[x , z].normal.invert();
					vertrices[x , z].normal.Normalize();
				}
			}
		}
		
		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glEnable(Gl.GL_COLOR_MATERIAL);
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			textureWall.Bind();
			Gl.glBegin(Gl.GL_QUADS);
			for (int i = 0; i < width - 1; i++)
			{
				float fiz = (float)i;
				fiz /= 5.0f;
				float fio = (float)i + 1.0f;
				fio /= 5.0f;
				Gl.glNormal3f(0.0f, 0.0f, 1.0f);
				Gl.glTexCoord2f(fiz, 1);
				Gl.glVertex3f(i, vertrices[i, 0].position.y, 0);
				Gl.glTexCoord2f(fio, 1);
				Gl.glVertex3f(i+1, vertrices[i+1, 0].position.y, 0);
				Gl.glTexCoord2f(fio, 0);
				Gl.glVertex3f(i+1, -5, 0);
				Gl.glTexCoord2f(fiz, 0);
				Gl.glVertex3f(i  , -5, 0);
			}
			Gl.glEnd();
			textureWall.UnBind();
			
			textureGround.Bind();
			Gl.glBegin(Gl.GL_QUADS);
			for (int i = 0; i < width - 1; i++)
			{
				for (int j = 0; j < depth - 1; j++)
				{
					vertrices[i  ,j  ].draw();
					vertrices[i+1,j  ].draw();
					vertrices[i+1,j+1].draw();
					vertrices[i  ,j+1].draw();					
				}
			}
			Gl.glEnd();
			textureGround.UnBind();
		}
	}
}
