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
using Tesla.Audio;
using Tao.Sdl;
using Tao.OpenGl;

namespace Tesla
{
	class MainClass
	{
		static SDLWindow w;
        static Configuration c = new Configuration("config.dat");
		
		static AudioContext audioContext;
		
		static void Initialize()
		{
			c  = new Configuration("config.dat");
			audioContext = new AudioContext(c);
			
			w = new SDLWindow(c);
			w.Add(buttonAction);
		}
		
		static void Deinitialize()
		{
			audioContext.Dispose();
		}
		
		static void LoadObjects()
		{
			w.setSkyBox(new SkyBox(new CubeMapTexture(c.defaultPath + "CubeMap/sky0", CubeMapType.None)));
			
			Font font = SDLFontv2.Create(c.defaultPath + "Fonts/zektonbi.ttf", 32);
			w.Add(new SimpleFontHandler(font, "welcome to TESLA!", new Vector2f(10.0f, 10.0f), new Color4f(1.0f, 1.0f, 1.0f, 1.0f)));
			Landscape2D l = new Landscape2D(new BasicTexture(c.defaultPath + "Texture/Tile/rock512.bmp"), new BasicTexture(c.defaultPath + "Texture/Tile/grass.jpg"));
			w.Add(l);
			
			
		}
		
		
		public static void Main(string[] args)
		{
			Initialize();
			
			LoadObjects();
			
			w.getActiveCamera().getPosition().set(10.0f, 2.0f, 10.0f);
			w.getActiveCamera().rotateX(-3.0f);
			
			FPSCounter frameCounter = new FPSCounter();
			bool quitFlag = false;
			
			while(!quitFlag)
			{
				float frameTime = frameCounter.Update();
				quitFlag = w.Update(frameTime);
			}

			Deinitialize();
		}

		static void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{
		}
	}
}