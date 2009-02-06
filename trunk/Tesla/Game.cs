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
		static Listener listener; 
		
		static Sound gunShot;
		static Ambient ambient;
		
		static void Initialize()
		{
			c  = new Configuration("config.dat");
			audioContext = new AudioContext(c);
			listener = new Listener();
			
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
			Landscape2D l = new Landscape2D(new BasicTexture(c.defaultPath + "Texture/Tile/rock512.bmp"), new BasicTexture(c.defaultPath + "Texture/Tile/grass.jpg"), 0.0f);
			w.Add(l);
			
			Landscape2D l2 = new Landscape2D(new BasicTexture(c.defaultPath + "Texture/Tile/rock512.bmp"), new BasicTexture(c.defaultPath + "Texture/Tile/grass.jpg"), -10.0f);
			w.Add(l2);	
			
			w.Add(new Quad(new BasicTexture(c.defaultPath + "Texture/Foilage/Vine with alpha.png"), new Vector3f(5,-4,0.001f), 8.0f, 1.0f, 1.0f));
		}
		
		static void LoadAudio()
		{
			gunShot = new Sound(c.defaultPath + "Audio/gunshot2.wav");
			ambient = new Ambient(c.defaultPath + "Ambient/BugsAndBirds.wav");
		}
		
		
		public static void Main(string[] args)
		{
			Initialize();
			
			LoadObjects();
			LoadAudio();
			
			w.getActiveCamera().getPosition().set(10.0f, 2.0f, 10.0f);
			w.getActiveCamera().rotateX(-3.0f);
			
			FPSCounter frameCounter = new FPSCounter();
			bool quitFlag = false;
			
			while(!quitFlag)
			{
				float frameTime = frameCounter.Update();
				quitFlag = w.Update(frameTime);
				updateAudio();
			}

			Deinitialize();
		}
		
		public static void updateAudio()
		{
			Camera c = w.getActiveCamera();
			listener.setPosition(c.getPosition());
			listener.setOrientation(c.getFrontVector(), c.getUpVector());
		}
		
		static int lastPressed;

		static void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{
			if (lastPressed + 100 > System.Environment.TickCount)
				return;
			lastPressed = System.Environment.TickCount;
			
			
			if (keyState[Sdl.SDLK_e] > 0)
				gunShot.play(new Vector3f(), new Vector3f());
		}
	}
}