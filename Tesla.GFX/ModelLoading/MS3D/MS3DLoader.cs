// MS3DLoader.cs created with MonoDevelop
// User: topfs at 1:10 PM 12/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.IO;

using Tesla.Common;

namespace Tesla.GFX
{
	public class MS3DLoader
	{
		public class MS3DModel : Tesla.GFX.Drawable
		{
			public void Draw (float frameTime, Frustum frustum)
			{
				throw new NotImplementedException();
			}
		}
		
		private class MS3DVertex
		{
			public sbyte boneInder;
			public Vector3f position;
			public byte flags;
			public byte referanceCount;
			
			public Vector3f normal;
			public Vector2f textureCoordinate;
		}
		
		private class MS3DTriangle
		{
			public ushort flags;
			public ushort[] vertexIndices;
		}
		
		private class MS3DGroup
		{
			public byte flags;
			public string name;
			public MS3DTriangle[] triangles;
			public MS3DVertex[] vertices;
			public sbyte materialIndex;
		}
		
		private class MS3DMaterial
		{
			public string name;
			public Vector3f ambient;
			public Vector3f diffuse;
			public Vector3f specular;
			public Vector3f emissive;
			public float shininess;
			public float transparency;
			public string textureFileName;
			public string alphaTextureFileName;		
		}
		
		private class MS3DJoint
		{
			public static int NoParent = -1;
			public byte flags;
			public string name;
			public int parentIndex;
			public Vector3f rotation;
			public Vector3f position;
			public MS3DKeyFrame[] rotationKeyFrames;
			public MS3DKeyFrame[] translationKeyFrames;
		}
		
		private class MS3DKeyFrame
		{
			public float Time;
			public Vector3f Parameter;		
		}
		
		public MS3DLoader()
		{
		}
		
		public static MS3DModel Load(string filePath)
		{
			using(FileStream inStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				/*
				 * It is important that we open the file in ASCII encoding, otherwise when reading names
				 * (e.g. group names) with BinaryReader.ReadChars() a char may be read as more than one
				 * byte (because of UTF8 encoding).
				*/
				BinaryReader reader = new BinaryReader(inStream, System.Text.Encoding.ASCII);
				try
				{
					MS3DModel m= new MS3DModel();
					if (!parseHeader(reader, m))
						return null;

					parseVertrices(reader, m);
					parseTriangles(reader, m);
					parseGroups(reader, m);
					parseMaterials(reader, m);
					parseKeyFrameData(reader, m);
					parseJoints(reader, m);
					
					return m;
				}
				catch (Exception e)
				{ }
			}
			
			return null;
		}
		
		private static bool parseHeader(BinaryReader reader, MS3DModel model)
		{
			return true;
		}
		
		private static void parseVertrices(BinaryReader reader, MS3DModel model)
		{
			return;
		}
		
		private static void parseTriangles(BinaryReader reader, MS3DModel model)
		{
			return;
		}
		
		private static void parseGroups(BinaryReader reader, MS3DModel model)
		{
			return;
		}
		
		private static void parseMaterials(BinaryReader reader, MS3DModel model)
		{
			return;
		}
		
		private static void parseKeyFrameData(BinaryReader reader, MS3DModel model)
		{
			return;
		}
		
		private static void parseJoints(BinaryReader reader, MS3DModel model)
		{
			return;
		}
	}
}
