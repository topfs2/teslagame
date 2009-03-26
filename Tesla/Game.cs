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
		
		static Weapon gun;
		static Ambient ambient;
		
		static Effect myEffect;

		static AnimatedQuad player;
		static Vector3f playerPosition;
		static Vector3f playerVelocity;
		
		static Vector3f crosshairPosition;
		
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
			crosshairPosition   = new Vector3f(3.0f, 1.0f, 0.0f);
			
			w.Add(new HUD(c.defaultPath));
			Vector3f va, vb, vc, vd;
			va = new Vector3f(-100.0f, 0.0f, -10.0f);
			vb = new Vector3f( 100.0f, 0.0f, -10.0f);
			vc = new Vector3f( 100.0f, 0.0f,  50.0f);
			vd = new Vector3f(-100.0f, 0.0f,  50.0f);
			w.Add(new GroundPlane(new BasicTexture(c.defaultPath + "Texture/Tile/chess0.jpg"), 16, 4, new Vector3f(0.0f, 0.0f, -20.0f), 200, 50.0f));
			myEffect = new Effect(w.getActiveCamera(), c);
			w.Add(myEffect);
			
			playerPosition = new Vector3f(0.0f, 1.0f, 0.0f);
			playerVelocity = new Vector3f();
			BasicTexture[] textures = new BasicTexture[8];
			for (int i = 1; i <= 8; i++)
				textures[i-1] = new BasicTexture(c.defaultPath + "How/Media/Girl/Move" + i + ".png");
			player = new AnimatedQuad(textures, playerPosition, 1.0f, textures[0].Width() / textures[0].Height());
			w.Add(player);
		
			w.Add(new Quad(new BasicTexture(c.defaultPath + "Texture/Foilage/Vine with alpha.png"), new Vector3f(-100, 0.65f, 5.001f), 10.0f, 20.0f, 1.0f));
			w.Add(new BillboardedQuad(new BasicTexture(c.defaultPath + "Texture/Particle/crosshairs.png"), w.getActiveCamera(),  crosshairPosition, new Vector2f(1.0f, 1.0f)));
		}
		
		static void LoadAudio()
		{
			ambient = new Ambient(c.defaultPath + "Ambient/BugsAndBirds.wav");
		}
		
		
		public static void Main(string[] args)
		{
			Initialize();
			
			LoadObjects();
			/* Load small Sounds before ambient as otherwise we get error creating buffer */
			gun = new Weapon(1000, 10, new Sound(c.defaultPath + "Audio/laserfire3.wav"), myEffect);
			LoadAudio();

			
			
			w.getActiveCamera().getPosition().set(0.0f, 0.0f, 10.0f);
			w.getActiveCamera().rotateX(-3.0f);
			//w.getActiveCamera().linkLookAtPosition(playerPosition);
			
			FPSCounter frameCounter = new FPSCounter();
			bool quitFlag = false;
			
			while(!quitFlag)
			{
				float frameTime = frameCounter.Update();
				
				//playerPosition.set(new Vector3f(1.0f, 0.0f, 0.0f).stretch(w.getActiveCamera().getPosition()) + new Vector3f(0.0f, 1.0f, 0.0f));
				
				playerVelocity.y -= 0.01f * frameTime;
				playerPosition.set(playerPosition + playerVelocity);
				crosshairPosition.set(crosshairPosition + playerVelocity);
				
				float zoom = crosshairPosition.length(playerPosition) * -2.0f;
				Console.Out.WriteLine(zoom);
				zoom = zoom > -10.0f ? -10.0f : zoom;
				w.getActiveCamera().getPosition().set(w.getActiveCamera().getFrontVector().copy().stretch(zoom) + ((playerPosition + crosshairPosition) * 0.5f));
				
				if (playerPosition.y < 1.0f)
				{
					playerPosition.y = 1.0f;
					playerVelocity.y = 0;
				}
				
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
			if (keyState[Sdl.SDLK_e] > 0 && gun.canFire())
			{
				Vector3f direction = (crosshairPosition - playerPosition).Normalize();
				Console.Out.WriteLine(direction.ToString());
				gun.Fire(playerPosition, direction);
			}
			
			if (keyState[Sdl.SDLK_SPACE] > 0)
			{
				playerVelocity.y += 0.052f * frameTime;
			}
			
			/*if (keyState[Sdl.SDLK_a] > 0)
				camera.stepSideway(-motion);
			if (keyState[Sdl.SDLK_d] > 0)
				camera.stepSideway( motion);
			if (keyState[Sdl.SDLK_w] > 0)
				camera.stepForward( motion);
			if (keyState[Sdl.SDLK_s] > 0)
				camera.stepForward(-motion);
			if (keyState[Sdl.SDLK_r] > 0)
				camera.stepUp( motion);
			if (keyState[Sdl.SDLK_f] > 0)
				camera.stepUp(-motion);*/

			playerVelocity.x -= playerVelocity.x * 10.0f * frameTime;
			if (keyState[Sdl.SDLK_a] > 0)
				playerVelocity.x = -0.005f;
			if (keyState[Sdl.SDLK_d] > 0)
				playerVelocity.x =  0.005f;
			
			
			if (keyState[Sdl.SDLK_UP] > 0)
			    crosshairPosition.y += (frameTime * 4.0f);
			if (keyState[Sdl.SDLK_DOWN] > 0)
				crosshairPosition.y -= (frameTime * 4.0f);
			if (keyState[Sdl.SDLK_LEFT] > 0)
				crosshairPosition.x -= (frameTime * 4.0f);
			if (keyState[Sdl.SDLK_RIGHT] > 0)
				crosshairPosition.x += (frameTime * 4.0f);
		}
	}
}