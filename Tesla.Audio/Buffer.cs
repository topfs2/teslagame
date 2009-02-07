// Buffer.cs created with MonoDevelop
// User: topfs at 6:50 PM 1/31/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using Tao.OpenAl;
using Tesla.Common;

namespace Tesla.Audio
{
	internal class Buffer : IDisposable
	{
		private static Dictionary<string, int> buffers = new Dictionary<string,int>();
		
		private int bufferID;
		
		public int getBuffer()
		{
			return bufferID;
		}
		
		public Buffer(string fileName)
		{
			if (buffers.ContainsKey(fileName))
				bufferID = buffers[fileName];
			else
				bufferID = CreateBuffer(fileName);
		}
		
		private static int CreateBuffer(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				throw new System.IO.FileNotFoundException("Couln´t find " + fileName);

			int buffer = Alut.alutCreateBufferFromFile(fileName);

			if (Check("CreateBuffer"))
			{
				buffers.Add(fileName, buffer);
				return buffer;
			}
			else
				throw new Exception("Error creating buffer from " + fileName);
		}
		
		private static bool Check(string function)
		{
			int error = Alut.alutGetError();
			if (error != Alut.ALUT_ERROR_NO_ERROR)
			{
				Log.Write("Buffer - Alut error in " + function + ": " + error);
				return false;
			}
			return true;
		}
		
		public void Dispose ()
		{
			Al.alDeleteBuffers(1, ref bufferID);
			Check("Dispose");
		}
		
		public static void unload()
		{
			System.Collections.Generic.List<int> bufferList = new List<int>(buffers.Values);
			for (int i = 0; i < bufferList.Count; i++)
			{
				int b = bufferList[i];
				Al.alDeleteBuffers(1, ref b);
				bufferList[i] = b;
				Check("unload[" + i + "]");
			}
		}
	}
}
