// Landscapev2.cs created with MonoDevelop
// User: topfs at 9:40 PM 11/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
using System.Collections.Generic;
using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	public class SuperVertex
	{
		public Vector3f position, normal;
		public Color4f color;
		public Vector2f texCoord;
		
		public void draw()
		{
			if (normal != null)
				Gl.glNormal3fv(normal.vector);
			if (texCoord != null)
				Gl.glTexCoord2f(texCoord.x, texCoord.y);
			if (color != null)
				Gl.glColor4f(color.r, color.g, color.b, color.a);
			
			if (position != null)
				Gl.glVertex3fv(position.vector);
		}
	}
	
	public class Landscapev2 : Drawable
	{

		
		SuperVertex[] vertrices;
		//int indicesType;
		int[] drawIndices;
		int width, depth;
		int[] vboReference;

        float[] v;
		
		Texture baseTexture, secondTexture, alphaTexture, splattTexture;
		
		
		public Landscapev2(string loadPath, Texture baseTexture, Texture secondTexture, Texture splattTexture, Texture alphaTexture)
		{
			Log.Write("*loadWorld");
			loadWorld(loadPath);
			Log.Write("*calculateNormals");
			calculateNormals();
			Log.Write("*generateTexCoords");
			generateTexCoords();
			Log.Write("*generateDrawIndices");
			generateDrawIndices();
			Log.Write("*generateTextureSplatting");
			generateTextureSplatting();
			Log.Write("*buildVBOs");
			buildVBOs();
			Log.Write("*buildClusters");
			buildClusters();
			
			this.baseTexture 	= baseTexture;
			this.secondTexture 	= secondTexture;
			this.alphaTexture   = alphaTexture;
			this.splattTexture  = splattTexture;
		}
		
		private void loadWorld(string loadPath)
		{
			Bitmap data = new Bitmap(loadPath);
			int step = 1;
			width = data.Width / step;
			depth = data.Height / step;
			vertrices = new SuperVertex[width * depth];
			
			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					vertrices[x + z * width] = new SuperVertex();
					vertrices[x + z * width].position = new Vector3f(x, data.GetPixel(x, z).R / 8.0f, z);
				}
			}
			
			data.Dispose();
		}
		
		private void calculateNormals()
		{
			Vector3f vecA, vecB, vecC, vecD, normA, normB, normC, normD;
			for (int z = 1; z < (depth - 1); z++)
			{
				for (int x = 1; x < (width - 1); x++)
				{
					Vector3f p = vertrices[x + (z * width)].position;
					vecA = vertrices[x-1 + (z     * width)].position.diff(p);
					vecB = vertrices[x   + ((z-1) * width)].position.diff(p);
					vecC = vertrices[x+1 + (z     * width)].position.diff(p);
					vecD = vertrices[x   + ((z+1) * width)].position.diff(p);
					
					normA = vecA.Cross(vecD);
					normB = vecD.Cross(vecC);
					normC = vecC.Cross(vecB);
					normD = vecB.Cross(vecA);
					
					vertrices[x + z * width].normal = normA.add(normB).add(normC).add(normD);
					vertrices[x + z * width].normal.Normalize();
				}
			}
		}
		
		private void generateTexCoords()
		{
			float texSize = 10.0f;
			for (int z = 0; z < depth; z++)
			{
				for (int x = 0; x < width; x++)
				{
					float tx = x / ((float)width);
					tx *= texSize * (float)width / (float)depth;
					float tz = z / ((float)depth);
					tz *= texSize;
					vertrices[x + z * width].texCoord = new Vector2f(tx, tz);
				}
			}
		}
		
		private void generateDrawIndices()
		{
			//indicesType = Gl.GL_QUADS;
			drawIndices = new int[width * depth * 4];
			for (int z = 0; z < (depth - 1); z++)
			{
				for (int x = 0; x < (width - 1); x++)
				{
					drawIndices[0 + (4 * x) + z * ((width-1)*4)] = x   + (z       * width);
					drawIndices[1 + (4 * x) + z * ((width-1)*4)] = x+1 + (z       * width);
					drawIndices[2 + (4 * x) + z * ((width-1)*4)] = x+1 + ((z + 1) * width);
					drawIndices[3 + (4 * x) + z * ((width-1)*4)] = x   + ((z + 1) * width);
				}
			}
		}
		
		private void generateTextureSplatting()
		{
			foreach ( SuperVertex sv in vertrices)
				sv.color = new Color4f(0.33f, 0.33f, 0.33f, 0.33f);
		}
		
		private void generateLight(Vector3f lightPos, Color4f dL)
		{
			for (int x = 0; x < width; x++)
			{
				for (int z = 0; z < depth; z++)
				{
					Vector3f L = lightPos.diff(vertrices[x + z*width].position);
					L.Normalize();
					float ip = L * vertrices[x + z*width].position;//L.multiply(listNormals[i, j].posA);
					if (ip < 0)
						ip = 0;
					
					Color4f vc = new Color4f(0.5f, 1.0f, 1.0f, 1.0f);
					if (vertrices[x + z*width].color == null)
						vertrices[x + z*width].color = new Color4f(0.5f, 1.0f, 1.0f, 1.0f);
					
					Color4f dv = vertrices[x + z*width].color;
					
					vc.r = ip * dL.r * dv.r;
					vc.g = ip * dL.g * dv.g;
					vc.b = ip * dL.b * dv.b;
					
					vertrices[x + z*width].color = vc;
				}
			}			
		}
		
		private void buildVBOs()
		{
			float[] vertexData 			= new float[3 * drawIndices.Length];
			float[] baseTextureData		= new float[2 * drawIndices.Length];
			float[] secondTextureData 	= new float[2 * drawIndices.Length];
			float[] normalData  		= new float[3 * drawIndices.Length];
			float[] colorData			= new float[4 * drawIndices.Length];
			
			for(int i = 0; i < drawIndices.Length; i++)
			{
				int iiii = i*4;
				int iii  = i*3;
				int ii	 = i*2;
				vertexData[iii+0] = vertrices[drawIndices[i]].position.x;
				vertexData[iii+1] = vertrices[drawIndices[i]].position.y;
				vertexData[iii+2] = vertrices[drawIndices[i]].position.z;
				
				if (vertrices[drawIndices[i]].normal == null)
				{
					normalData[iii+0] = 0.0f;
					normalData[iii+1] = 1.0f;
					normalData[iii+2] = 0.0f;				
				}
				else
				{
					normalData[iii+0] = vertrices[drawIndices[i]].normal.x;
					normalData[iii+1] = vertrices[drawIndices[i]].normal.y;
					normalData[iii+2] = vertrices[drawIndices[i]].normal.z;
				}
				baseTextureData[ii+0] = vertrices[drawIndices[i]].texCoord.x;
				baseTextureData[ii+1] = vertrices[drawIndices[i]].texCoord.y;

				secondTextureData[ii+0] = vertrices[drawIndices[i]].texCoord.x * 100.0f;
				secondTextureData[ii+1] = vertrices[drawIndices[i]].texCoord.y * 100.0f;
				
				colorData[iiii + 0] = vertrices[drawIndices[i]].color.r;
				colorData[iiii + 1] = vertrices[drawIndices[i]].color.g;
				colorData[iiii + 2] = vertrices[drawIndices[i]].color.b;
				colorData[iiii + 3] = vertrices[drawIndices[i]].color.a;
			}

            v = vertexData;
           

			vboReference = new int[5];
			Gl.glGenBuffersARB( 5, vboReference );
			Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[0] );
			Gl.glBufferDataARB( Gl.GL_ARRAY_BUFFER_ARB, (IntPtr)(vertexData.Length * sizeof(float)), vertexData, Gl.GL_STATIC_DRAW_ARB );

			Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[1] );
			Gl.glBufferDataARB( Gl.GL_ARRAY_BUFFER_ARB, (IntPtr)(normalData.Length * sizeof(float)), normalData, Gl.GL_STATIC_DRAW_ARB );
			
			Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[2] );
			Gl.glBufferDataARB( Gl.GL_ARRAY_BUFFER_ARB, (IntPtr)(baseTextureData.Length * sizeof(float)), baseTextureData, Gl.GL_STATIC_DRAW_ARB );
			
			Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[3] );
			Gl.glBufferDataARB( Gl.GL_ARRAY_BUFFER_ARB, (IntPtr)(secondTextureData.Length * sizeof(float)), secondTextureData, Gl.GL_STATIC_DRAW_ARB );
			
			Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[4] );
			Gl.glBufferDataARB( Gl.GL_ARRAY_BUFFER_ARB, (IntPtr)(colorData.Length * sizeof(float)), colorData, Gl.GL_STATIC_DRAW_ARB );
		}

		private void buildClusters()
		{
		
		}
		
		public static TexturedCube tc;

public float[] mcolor = { 1.0f, 1.0f, 1.0f, 1.0f };
public float[] mcolor3 = { 1.0f, 1.0f, 1.0f, 1.0f };
public float[] mcolor2 = { -0.2f, -0.2f, -0.2f, 1.0f };

		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glPushMatrix();
			//Gl.glDisable(Gl.GL_LIGHTING);

			//Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE, mcolor);

			//Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_EMISSION, mcolor2);
			/*Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, mcolor3);
			Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, mcolor3);*/
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            //Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);

            this.baseTexture.Bind();

            bool useVBO = true;
			if(useVBO) // supports VBO
			{
				Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[0] );
				Gl.glVertexPointer( 3, Gl.GL_FLOAT, 0, null );		// Set The Vertex Pointer To The Vertex Buffer
				Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[1] );
				Gl.glNormalPointer( Gl.GL_FLOAT, 0, null );
				Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[2] );
				Gl.glClientActiveTextureARB( Gl.GL_TEXTURE0_ARB);
				Gl.glTexCoordPointer( 2, Gl.GL_FLOAT, 0, null );
				Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[2] );
				Gl.glClientActiveTextureARB( Gl.GL_TEXTURE1_ARB);
				Gl.glTexCoordPointer( 2, Gl.GL_FLOAT, 0, null );				
				Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[2] );
				Gl.glClientActiveTextureARB( Gl.GL_TEXTURE2_ARB);
				Gl.glTexCoordPointer( 2, Gl.GL_FLOAT, 0, null );
				
				/*Gl.glBindBufferARB( Gl.GL_ARRAY_BUFFER_ARB, vboReference[4] );
				Gl.glColorPointer( 4, Gl.GL_FLOAT, 0, null );*/
			} 
			else
			{
				Gl.glVertexPointer( 3, Gl.GL_FLOAT, 0, v );	// Set The Vertex Pointer To Our Vertex Data
				//Gl.glTexCoordPointer( 2, Gl.GL_FLOAT, 0, g_pMesh->m_pTexCoords );	// Set The Vertex Pointer To Our TexCoord Data
			}

            Gl.glDrawArrays(Gl.GL_QUADS, 0, (4 * drawIndices.Length));

            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);

            Gl.glPopMatrix();
		}
		
		public float getY(float x, float z)
		{
			return vertrices[(int)x + (int)z*width].position.y;
		}
	}
}
