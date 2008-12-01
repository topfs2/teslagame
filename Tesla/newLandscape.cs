// Main.cs created with MonoDevelop
// User: topfs at 10:10 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using System.Collections;
using Tesla.GFX;
using Tesla.GFX.Font;
using Tesla.Physics;
using Tesla.Common;
using Tesla.GFX.ModelLoading;
using Tao.Ode;
using Tao.Sdl;
using Tao.OpenGl;

namespace Tesla
{
	class MainClass
	{
		public static SimpleConstantForce force;
		public static World worldTest;
		public static Space space;
		static BodyBox bodyTest1, bodyTest2;
		static string write = "Loading..."; 
		static SDLWindow w;
		static Point3f lpos;
		static Water water;
        static Configuration c = new Configuration("config.dat");
		static Frustum testFrustum;
		public static void Main(string[] args)
		{
		/* TEST UTRYMME */

		/* /TEST UTRYMME */
			worldTest = new World(0.0f, -9.81f, 0.0f);
			space = worldTest.addSpace();
			if (space == null)
				Console.Out.WriteLine("WHAHW");
		
			lpos = new Point3f(300.0f, 100.0f, 0.0f);

			w = new SDLWindow(c.title, c.resWidth, c.resHeight, c.bpp, c.fullscreen);
			w.Add(buttonAction);

            SDLFontv2 f = SDLFontv2.Create(c.defaultPath + "Fonts/FreeSans.ttf", 32);
			SimpleFontHandler sfh = new SimpleFontHandler(f, "FPS: 0", new Point2f(20.0f, 20.0f), new Color4f(1.0f, 1.0f, 1.0f, 1.0f));
			w.Add(sfh);
			
			Texture grass = Texture.CreateFromFile(c.defaultPath + "Texture/Tile/grass.jpg");
            Texture stone = Texture.CreateFromFile(c.defaultPath + "Texture/Tile/texture1.png");
            Texture dirt = Texture.CreateFromFile(c.defaultPath + "Texture/Tile/texture3.png");

            Landscapev2 l = new Landscapev2(c.defaultPath + "Heightmap/h2.jpg", 
											grass, stone, dirt,
                                            Texture.CreateFromFile(c.defaultPath + "Texture/Tile/dirty_stone.jpg"));
			
			w.Add(l);
            ModelLoader ml = new ObjLoader(c.defaultPath + "Object/Lasha.mtl");
            Point3f pos = new Point3f(0.0f, 0.0f, 0.0f);
            //w.getActiveCamera().linkLookAtPosition(pos);
            Drawable lm = ml.LoadModel(c.defaultPath + "Object/Lasha.obj");
            w.Add(lm);
            w.setSkyBox(new SkyBox(c.defaultPath + "SkyBox/skyboxsun5deg2"));

			int lastFrame = System.Environment.TickCount;
			int lastTickCount = System.Environment.TickCount;
			int ticker = 0;
			bool quitFlag = false;
			int updateFPS = 100; //ms
			//w.Add(debug);
			
			while(!quitFlag)
			{
				float frameTime = (System.Environment.TickCount - lastTickCount) / 1000.0f;
				lastTickCount = System.Environment.TickCount;
				if (frameTime != 0)
                    worldTest.update(frameTime);
							    
				//Gl.glDisable(Gl.GL_LIGHT1);
				/*Point3f lpos = w.getActiveCamera().getLookAtPosition();*/
			  	float[] LightAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
	            float[] LightDiffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
	            float[] LightPosition = { lpos.x, lpos.y, lpos.z, 1.0f };
	            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, LightAmbient);
	            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, LightDiffuse);
	            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, LightPosition);

				quitFlag = w.Update(frameTime);
				
				if ((System.Environment.TickCount - lastFrame) >= updateFPS)
				{
					sfh.text = "FPS: " + (int)(ticker * (1000.0d / (double)(System.Environment.TickCount - lastFrame))) + " lookpos: " + w.getActiveCamera().getLookAtPosition().ToString();
					lastFrame = System.Environment.TickCount;
					ticker = 0;
				}
				else
					ticker++;
			}
		}
		static int lastCreateBox = 0;
		static int x = 800;
        static SimpleFontHandler debug = new SimpleFontHandler(SDLFontv2.Create(c.defaultPath + "Fonts/FreeSans.ttf", 32), "", new Point2f(400, 400), new Color4f(1.0f, 1.0f, 1.0f, 1.0f));
		static void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{
		}
	}
}