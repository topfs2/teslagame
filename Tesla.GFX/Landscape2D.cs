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
		private SuperVertex[,] upperVertrices;
		private SuperVertex[,] lowerVertrices;
		Texture textureWall, textureGround;
		
			int width = 80;
			int depth = 20;		
		
		public Landscape2D(Texture textureWall, Texture textureGround, float translateY)
		{
			this.textureWall = textureWall;
			this.textureGround = textureGround;
			Log.Write("Creating heightData");
			
			upperVertrices = new SuperVertex[width, depth];
			generateVertrices(upperVertrices,  1.0f + translateY, 0.35f, 0.1f);
			generateNormals(upperVertrices, true);
			lowerVertrices = new SuperVertex[width, depth];
			generateVertrices(lowerVertrices, -5.0f + translateY, 0.55f, 0.1f);
			generateNormals(lowerVertrices, false);
		}
		
		private void generateVertrices(SuperVertex[,] vertrices, float translateY, float maxHeightDifference, float maxScramble)
		{
			Random rand = new Random();
			float last = 0.0f;
			float ratio = (float)width / (float)depth;
			int mul = 2;
			for (int i = 0; i < width; i++)
			{
				float h = ((0.5f - (float)rand.NextDouble()) * maxHeightDifference) + last + translateY;
				
				float x = (float)i / 2.0f;
				
				for (int j = 0; j < depth; j++)
				{
					float y = (float)j / 2.0f;
					vertrices[i, j] = new SuperVertex();
					vertrices[i, j].position = new Vector3f(x, h + ((0.5f - (float)rand.NextDouble()) * maxScramble), -y);
					vertrices[i, j].texCoord = new Vector2f((float)(i * mul) / (float)depth, ((float)(j * mul) / (float)depth));
				}
				last = h - translateY;
			}
			
			for (int i = 0; i < width; i++)
			{
				vertrices[i, 0].position.y -= maxHeightDifference; 
				vertrices[i, 1].position.y -= (maxHeightDifference) / 2.0f;
			}
		}
		
		
		private void generateNormals(SuperVertex[,] vertrices, bool invertNormal)
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
					if (invertNormal)
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
				Gl.glVertex3f(upperVertrices[i, 0].position.x, upperVertrices[i, 0].position.y, 0);
				Gl.glTexCoord2f(fio, 1);
				Gl.glVertex3f(upperVertrices[i+1, 0].position.x, upperVertrices[i+1, 0].position.y, 0);
				
				Gl.glTexCoord2f(fio, 0);
				Gl.glVertex3f(lowerVertrices[i+1, 0].position.x, lowerVertrices[i+1, 0].position.y, 0);
				Gl.glTexCoord2f(fiz, 0);
				Gl.glVertex3f(lowerVertrices[i, 0].position.x  , lowerVertrices[i, 0].position.y, 0);
			}
			Gl.glEnd();
			textureWall.UnBind();
			
			textureGround.Bind();
			Gl.glBegin(Gl.GL_QUADS);
			for (int i = 0; i < width - 1; i++)
			{
				for (int j = 0; j < depth - 1; j++)
				{
					upperVertrices[i  ,j  ].draw();
					upperVertrices[i+1,j  ].draw();
					upperVertrices[i+1,j+1].draw();
					upperVertrices[i  ,j+1].draw();
					
					lowerVertrices[i  ,j  ].draw();
					lowerVertrices[i+1,j  ].draw();
					lowerVertrices[i+1,j+1].draw();
					lowerVertrices[i  ,j+1].draw();		
				}
			}
			Gl.glEnd();
			textureGround.UnBind();
		}
	}
}
