// Landscape.cs created with MonoDevelop
// User: topfs at 7:51 PM 10/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;

using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	public class quad
	{
		public Point3f posA, posB, posC, posD, normalA, normalB, normalC, normalD;
		public Color4f colA, colB, colC, colD;
		public quad(Point3f posA, Point3f posB, Point3f posC, Point3f posD)
		{
			this.posA = posA;
			this.posB = posB;
			this.posC = posC;
			this.posD = posD;
			
			Point3f vectorA1 = posB.diff(posA);
			Point3f vectorB1 = posB.diff(posC);
			
			Point3f vectorA2 = posD.diff(posA);
			Point3f vectorB2 = posD.diff(posC);
			
			normalB = vectorB1.Cross(vectorA1);
			normalD = vectorA2.Cross(vectorB2);
			
			normalA = normalB.copy().add(normalD);
			if (normalA.length() != 0)
				normalA.Normalize();
			else
				normalA.set(0.0f, 1.0f, 0.0f);
				
			if (normalD.length() != 0)
				normalD.Normalize();
			else
				normalD.set(0.0f, 1.0f, 0.0f);				
			
			if (normalB.length() != 0)			
				normalB.Normalize();
			else
				normalB.set(0.0f, 1.0f, 0.0f);
				
			normalC = new Point3f(normalA.x, normalA.y, normalA.z);
			
			colA = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
			colB = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
			colC = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
			colD = new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
		}
	
	}
	
	public class Landscape : Drawable
	{
		private Geometry geometry;
		private Texture texture;
		private int step;
		private float texsize;
		
		float heightscale;
		int sizeX, sizeZ;
		
		private quad[,] listNormals;
		
		public bool renderNormals;
		
		public Landscape(Geometry geometry, Texture texture, float heightscale, int step, int texsize)
		{
			this.geometry = geometry;
			this.texture = texture;
			this.heightscale = heightscale;
			this.step = step;
			this.texsize = texsize;
			
			generateNormals();
			generateLight(new Point3f(20.0f, 100.0f, 20.0f));
		}
		
		private void generateNormals()
		{
			sizeX = geometry.maximumX() - 1;
			sizeZ = geometry.maximumZ() - 1;
			
			sizeZ /= step;
			sizeX /= step;
			Console.Out.WriteLine("step, sizeZ, sizeX " + step +","+ sizeX +","+ sizeZ);
			
			listNormals = new quad[sizeX, sizeZ];
			
			int s = step;
			float l = heightscale;
			
			for (int i = 0; i < sizeX; i++)
			{
				for (int j = 0; j < sizeZ; j++)
				{
					listNormals[i,j] = new quad(new Point3f(i  , l*geometry.getHeight(i*s    , j*s    ), j  ),
												new Point3f(i  , l*geometry.getHeight(i*s    , (j+1)*s), j+1),
												new Point3f(i+1, l*geometry.getHeight((i+1)*s, (j+1)*s), j+1),
												new Point3f(i+1, l*geometry.getHeight((i+1)*s, j*s    ), j  ));
				}
			}

			
			for (int i = 1; i < sizeX; i++)
			{
				for (int j = 1; j < sizeZ; j++)
				{
					if (j == 0 && i != 0)
						listNormals[i+1, 0].normalA = normalize( listNormals[i, 0].normalA, listNormals[i+1, 0].normalA);
					else if (i == 0 && j != 0)
						listNormals[0, j+1].normalA = normalize( listNormals[0, j].normalA, listNormals[0, j+1].normalA);
					else
					{
						listNormals[i  , j-1].normalB = listNormals[i-1, j-1].normalC =	listNormals[i-1, j  ].normalD = 
						normalize(  listNormals[i  , j  ].normalA,
									listNormals[i  , j-1].normalB,
									listNormals[i-1, j-1].normalC,
									listNormals[i-1, j  ].normalD  );
					}
				}
			}
		}
		
		private void generateLight(Point3f lightPos)
		{
			for (int i = 0; i < sizeX; i++)
			{
				for (int j = 0; j < sizeZ; j++)
				{
					Point3f L = lightPos.diff(listNormals[i, j].posA);
					L.Normalize();
					float ip = L * listNormals[i, j].posA;//L.multiply(listNormals[i, j].posA);
					if (ip < 0)
						ip = 0;
						
/*		vcR = ip * dLR * dvR
		vcG = ip * dLG * dvG
		vcB = ip * dLB * dvB*/
					
					listNormals[i, j].colA.set(1.0f, ip, ip, ip);
				}
			}
		}
		
		private Point3f normalize(Point3f normalA, Point3f normalB, Point3f normalC, Point3f normalD)
		{
			normalA.add(normalB).add(normalC).add(normalD);
			return normalA.Normalize();
		}
		
		private Point3f normalize(Point3f normalA, Point3f normalB)
		{
			return normalA.add(normalB).Normalize();
		}

		void Drawable.Draw (float frameTime, Frustum frustum)
		{
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			texture.Bind();

			Gl.glPushMatrix();
			Gl.glScalef(step, 1.0f, step);
			if (true)
				Gl.glDisable(Gl.GL_LIGHTING);
			if (true)
			{

			Gl.glBegin(Gl.GL_QUADS);
			foreach (quad q in listNormals)
			{
			//	Gl.glColor3f(q.colA.r, q.colA.g, q.colA.b); 
				Gl.glNormal3fv(q.normalA.vector);
				TexCoordFromPosition(q.posA.x, q.posA.z);
				Gl.glVertex3fv(q.posA.vector);
				Gl.glNormal3fv(q.normalB.vector);
				TexCoordFromPosition(q.posB.x, q.posB.z);
				Gl.glVertex3fv(q.posB.vector);
				Gl.glNormal3fv(q.normalC.vector);
				TexCoordFromPosition(q.posC.x, q.posC.z);
				Gl.glVertex3fv(q.posC.vector);
				Gl.glNormal3fv(q.normalD.vector);
				TexCoordFromPosition(q.posD.x, q.posD.z);
				Gl.glVertex3fv(q.posD.vector);
			}
			Gl.glEnd();
			}
			if (renderNormals)
			{
			Point3f tmp = new Point3f(0.0f, 0.0f, 0.0f);
				
			Gl.glBegin(Gl.GL_LINES);
			foreach (quad q in listNormals)
			{
				Gl.glVertex3fv(q.posA.vector);
				tmp.set(q.posA);
				tmp.add(q.normalA);
				Gl.glVertex3fv(tmp.vector);
//				Gl.glNormal3fv(q.normalB.vector);

				Gl.glVertex3fv(q.posB.vector);
				tmp.set(q.posB);
				tmp.add(q.normalB);
				Gl.glVertex3fv(tmp.vector);
				//Gl.glNormal3fv(q.normalC.vector);
				
				Gl.glVertex3fv(q.posC.vector);
				tmp.set(q.posC);
				tmp.add(q.normalC);
				Gl.glVertex3fv(tmp.vector);
				//Gl.glNormal3fv(q.normalD.vector);
				
				Gl.glVertex3fv(q.posD.vector);
				tmp.set(q.posD);
				tmp.add(q.normalD);
				Gl.glVertex3fv(tmp.vector);
			}
			Gl.glEnd();
			}
			Gl.glPopMatrix();
		}

		public Geometry getGeometry()
		{
			return geometry;
		}
		
		private void TexCoordFromPosition(float x, float z)
		{
			float tx = x / ((float)sizeX);
			tx *= texsize;
			float tz = z / ((float)sizeZ);
			tz *= texsize;
			Gl.glTexCoord2f(tx, tz);
		}
	}
}
