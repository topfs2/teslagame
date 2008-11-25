// ModelLoading.cs created with MonoDevelop
// User: topfs at 9:50 PM 10/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;
using Tesla.GFX;
using Tesla.GFX.ModelLoading;
using System.Collections.Generic;

namespace TeslaMain
{
	
	
	public class ModelLoading
	{
		
		public ModelLoading()
		{
		}
		
		static SDLWindow w;
		public static void Main(string[] args)
		{
			w = new SDLWindow("Tesla", 1000, 650, 32, false);
			w.Add(buttonAction);
            
            //SOCCERBALL
            Material black = new Material(
                new float[] { 0.2f, 0.2f, 0.2f, 1.0f },
                new float[] { 0.05f, 0.05f, 0.05f, 1.0f },
                new float[] { 1.0f, 0.0f, 1.0f, 1.0f },
                1.0f, 0.0f,
                Material.IllumType.SPECULAR, null);

            Material white = new Material(
                new float[] { 0.2f, 0.2f, 0.2f, 1.0f },
                new float[] { 0.8f, 0.8f, 0.8f, 1.0f },
                new float[] { 0.5f, 0.5f, 0.5f, 1.0f },
                1.0f, 0.0f,
                Material.IllumType.SPECULAR, null);
            
            //OLD TREE
            /*
            Texture texture = Texture.CreateFromFile("F:/Programmering/Tesla/res/rings.jpg");
            Material mat = new Material(
                new float[]{ 0.2f, 0.2f, 0.2f, 1.0f },
                new float[] { 0.8f, 0.8f, 0.8f, 1.0f },
                new float[] { 0.5f, 0.5f, 0.5f, 1.0f },
                1.0f, 0.0f,
                Material.IllumType.SPECULAR,
                null);
            
            Material mat1 = new Material(
                new float[] { 0.2f, 0.2f, 0.2f, 1.0f },
                new float[] { 0.8f, 0.8f, 0.8f, 1.0f },
                new float[] { 0.5f, 0.5f, 0.5f, 1.0f },
                1.0f, 0.0f,
                Material.IllumType.SPECULAR, Texture.CreateFromFile("F:/Programmering/Tesla/res/dead_tree_bark.jpg"));
            */
            Dictionary<string, Material> soccerMaterialMap = new Dictionary<string, Material>();
            soccerMaterialMap.Add("black", black);
            soccerMaterialMap.Add("white", white);
            /*
            Dictionary<string, Material> treeMaterialMap = new Dictionary<string, Material>();
            treeMaterialMap.Add("Mat", mat);
            treeMaterialMap.Add("Mat_1", mat1);
            */
            Dictionary<string, Material> lashaMap = new Dictionary<string, Material>();
            
            //ModelLoader treeLoader = new ObjLoader(treeMaterialMap);
            //w.Add(treeLoader.LoadModel("F:/Programmering/Tesla/res/dead_tree-OBJ.obj", new Point3f(0.0f, 0.0f, 200.0f)));

            //ModelLoader loader = new ObjLoader();
            //w.Add(loader.LoadModel("F:/Programmering/Tesla/res/earthelemental.obj", new Point3f(0, 0, 0)));

            //ModelLoader loader = new ObjLoader("F:/Programmering/Tesla/res/Lasha.mtl");
            //w.Add(loader.LoadModel("F:/Programmering/Tesla/res/Lasha.obj", new Point3f(0, 0, 0)));

            ModelLoader loader2 = new ObjLoader(soccerMaterialMap);
            w.Add(loader2.LoadModel("F:/Programmering/Tesla/res/soccerball.obj", new Point3f(0, 0, 0)));
            
			int lastFrame = System.Environment.TickCount;
			int lastTickCount = System.Environment.TickCount;
			int ticker = 0;
			bool quitFlag = false;
			while(!quitFlag)
			{
				float frameTime = (System.Environment.TickCount - lastTickCount) / 1000.0f;
				lastTickCount = System.Environment.TickCount;
				
				quitFlag = w.Update(frameTime);

				/*Console.Out.WriteLine(bodyTest.getPosition().ToString());
				Console.Out.WriteLine(bodyTest2.getPosition().ToString());*/

				if (ticker == 60)
				{
					Console.WriteLine("FPS " + (30000d / (double)(System.Environment.TickCount - lastFrame)));
					lastFrame = System.Environment.TickCount;
					ticker = 0;
				}
				else
					ticker++;
			}
		}
		
		static void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{

		}
	}
}
