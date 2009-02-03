// MS3DLoader.cs created with MonoDevelop
// User: topfs at 1:10 PM 12/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.IO;

using Tesla.Common;

namespace Tesla.GFX.ModelLoading
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
			//public sbyte boneInder;
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
		
		private class Joint
		{
			public static int NoParent = -1;
			public byte flags;
			public string name;
			public int parentIndex;
			public Vector3f rotation;
			public Vector3f position;
			public KeyFrame[] rotationKeyFrames;
			public KeyFrame[] translationKeyFrames;
		}
		
		private class KeyFrame
		{
			public KeyFrame(float time, Vector3f parameter)
			{
				this.time = time;
				this.parameter = parameter;
			}
			public float time;
			public Vector3f parameter;		
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
		
		private static Joint parseJoint(BinaryReader reader, out string parent)
		{
			Joint joint = new Joint();
			joint.flags = reader.ReadByte();
			joint.name 	= new string(reader.ReadChars(32));
			parent 		= new string(reader.ReadChars(32));
			joint.parentIndex = -1;
			joint.rotation = parseVector3f(reader);
			joint.position = parseVector3f(reader);
			
			ushort NumRotationKeyFrames = reader.ReadUInt16();
			ushort NumTranslationKeyFrames = reader.ReadUInt16();
			
			joint.rotationKeyFrames = new KeyFrame[NumRotationKeyFrames];
			for(int i = 0; i < NumRotationKeyFrames; i++)
				joint.rotationKeyFrames[i] = parseKeyFrame(reader);

			joint.translationKeyFrames = new KeyFrame[NumTranslationKeyFrames];
			for(int i = 0; i < NumTranslationKeyFrames; i++)
				joint.translationKeyFrames[i] = parseKeyFrame(reader);
				
			return joint;
		}
		
		private static KeyFrame parseKeyFrame(BinaryReader reader)
		{
			return new KeyFrame(reader.ReadSingle(), parseVector3f(reader));
		}
		
		private static Vector3f parseVector3f(BinaryReader reader)
		{
			return new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		
		private static Color4f parseColor4f(BinaryReader reader)
		{
			return new Color4f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		
		
	}
}
