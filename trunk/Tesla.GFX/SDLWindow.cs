// SDLWindow.cs created with MonoDevelop
// User: topfs at 11:10 PM 9/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using System.Collections;
using Tao.Sdl;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX
{
	public delegate void ButtonAction(byte[] keyState, int numberKeys, float frameTime);
	public delegate void DrawMethod(float frameTime);
	
	public class SDLWindow
	{
		private ArrayList drawables = new ArrayList();
		private ArrayList drawables2D = new ArrayList();
		
		private Sdl.SDL_Event evt;
		private ButtonAction buttonActionDelegate;
		private DrawMethod drawActionDelegate;
		private Frustum frustum;
		private bool quitFlag, Fullscreen, alwaysUpdateMouse;
		private int width, height, bpp, flags;
		int lastTickCount;
		private string Name;

		private int centerX, centerY;
		private int exceptions;
		
		private SkyBox skyBox;
		
		private Camera camera;
		public  CollisionSurface ground;

		public SDLWindow(Configuration configuration)
		{
			InitWindow(configuration);
			InitGL(configuration);
			Tao.DevIl.Il.ilInit();
			Tao.DevIl.Ilut.ilutInit();
		}
		
		private void InitWindow(Configuration configuration)
		{
			width = configuration.resWidth;
			height = configuration.resHeight;
			centerX = width / 2;
			centerY = height / 2;
			bpp = configuration.bpp;
			Name = configuration.title;
			Fullscreen = configuration.fullscreen;
			buttonActionDelegate = new ButtonAction(buttonAction);
			alwaysUpdateMouse = true;
			exceptions = 0;
			
			
            if (Fullscreen)
            {
                flags = (/*Sdl.SDL_HWACCEL | Sdl.SDL_HWSURFACE |*/ Sdl.SDL_OPENGL | Sdl.SDL_FULLSCREEN);

                bpp = 32/*System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel*/;

                /*Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;*/           
            }
            else
                flags = (/*Sdl.SDL_HWACCEL | Sdl.SDL_HWSURFACE |*/ Sdl.SDL_OPENGL);
			
			quitFlag = false;
			try
			{
				Log.Write("InitSDL: " + Width.ToString() + "x" + Height.ToString() + "@" + bpp.ToString());

				Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
				Sdl.SDL_WM_SetCaption(Name, "");

				IntPtr surfacePtr = Sdl.SDL_SetVideoMode(Width, Height, 32, flags);

				Sdl.SDL_Rect rect2 = new Sdl.SDL_Rect(0, 0, (short)Width, (short)Height);
				Sdl.SDL_SetClipRect(surfacePtr, ref rect2);
				Sdl.SDL_WarpMouse((short)centerX, (short)centerY);
				Log.Write("[" + (short)centerX + ", " + (short)centerY + "] <- [" + centerX + ", " + centerY + "]");
				Sdl.SDL_ShowCursor(0);
			}
			catch
			{
				Sdl.SDL_Quit(); // Vi kallar inte Quit() här för detta är Ändå första funktionen så om det inte går här så blir det så mycket null fel I Quit();
				quitFlag = true;
				Log.Write("Create OpenGL Window Failure", LogType.CriticalError);
				throw;                
			}
		}
		
		private void buttonAction(byte[] keyState, int numberKeys, float frameTime)
		{
			float motion = frameTime * 20.0f;
			float rotate = motion * 2;

			if (keyState[Sdl.SDLK_ESCAPE] > 0 || keyState[Sdl.SDLK_q] > 0)
                quitFlag = true;

/*			if (keyState[Sdl.SDLK_LSHIFT] > 0)
				motion *= 3.0f;

			if (keyState[Sdl.SDLK_UP] > 0)
			    camera.rotateX(-rotate);
			if (keyState[Sdl.SDLK_DOWN] > 0)
				camera.rotateX( rotate);
			if (keyState[Sdl.SDLK_LEFT] > 0)
				camera.rotateY(-rotate);
			if (keyState[Sdl.SDLK_RIGHT] > 0)
				camera.rotateY( rotate);
			if (keyState[Sdl.SDLK_a] > 0)
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
		}
		
		public bool Update(float frameTime)
        {			
            Sdl.SDL_PollEvent(out evt);

            if (evt.type == Sdl.SDL_QUIT)
            {
                quitFlag = true;
            }
            int x, y;
            float dx, dy;
			Sdl.SDL_GetMouseState(out x, out y);
			dx = x - centerX;
			dy = y - centerY;
			
			if (frameTime > 0.0f && (alwaysUpdateMouse || (dx > 0 || dx < 0 || dy > 0 || dy < 0)))
			{
				float sensativity = 30.0f;
				bool invertMouse = false;
/*				camera.rotateX((invertMouse ? 1.0f : -1.0f ) * dy * frameTime * sensativity);
				camera.rotateY(dx * frameTime * sensativity);*/
				Sdl.SDL_WarpMouse((short)centerX, (short)centerY);
			}
			
			int numkeys = 0;
			buttonActionDelegate(Sdl.SDL_GetKeyState(out numkeys), numkeys, frameTime);
			
            try
            {
				Draw(frameTime);
				exceptions = 0;
            }
            catch (DivideByZeroException e)
            {
            	quitFlag = true;
            	Log.Write("Quiting as a safety when division by zero comes as it can lock the system", LogType.CriticalError);
            }
            catch (Exception e)
            {
            	Log.Write(e.Message.ToString());
            	if (exceptions < 9)
            		exceptions++;
            	else
            	{
            		quitFlag = true;
            		Log.Write("More that 10 consecutive exceptions in a row, force quit");
            	}
		    }

            return quitFlag;
        }
		
		public Camera getActiveCamera()
		{
			return camera;
		}
		
        private void InitGL(Configuration configuration)
        {
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            //Gl.glDepthFunc(Gl.GL_ALWAYS);
			Gl.glDepthFunc(Gl.GL_LEQUAL);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glEnable(Gl.GL_BLEND);

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

			/* Setup LIGHTING  */
			float[] LightAmbient = { 0.1f, 0.09f, 0.06f, 1.0f };
			float[] LightDiffuse = { 1.0f, 0.819607843f, 0.450980392f, 1.0f };
			float[] LightPosition = { -0.33f, 0.33f, -0.33f, 0.0f };
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, LightAmbient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, LightDiffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, LightPosition);
            
            
            
            float[] LightSpot = {1.0f, 1.0f, 1.0f, 1.0f};


            //Gl.glBlendFunc(Gl.GL_ONE, Gl.GL_SRC_ALPHA);
            //Gl.glBlendFunc(Gl.GL_SRC_ALPHA_SATURATE, Gl.GL_ONE); 
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);  //perfekt för alla mina TexturedQuad
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);

            /* Required setup stuff */
            Gl.glViewport(0, 0, Width, Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45.0f, Width / (float)Height, 0.1f, 2000.0f);
            camera = new Camera(new Vector3f(0.0f, 0.0f, 0.0f), 45.0f, (float)Width / (float)Height, 0.1f, 2000.0f);
            frustum = new Frustum(camera);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            
            //Gl.glEnable(Gl.GL_CULL_FACE);
            //Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL);
            //Gl.glFrontFace(Gl.GL_CW);
            
			int error = Gl.glGetError();
            if (error != Gl.GL_NO_ERROR)
            	Log.Write("OpenGL: " + Gl.glGetString(error), LogType.CriticalError);
	    }
	    
	    public void Add(DrawMethod drawMethod)
	    {
	    	if (drawActionDelegate == null)
	    		drawActionDelegate = drawMethod;
	    	else
	    		drawActionDelegate += drawMethod;
	    }
		
		public void Add(Drawable2D d)
		{
			drawables2D.Add(d);
		}
		
		public void Add(Drawable d)
		{
			drawables.Add(d);
		}
		
		public void Add(ButtonAction buttonAction)
		{
			this.buttonActionDelegate += buttonAction;
		}
		
		public void Add(CollisionSurface collisionSurface)
		{
			ground = collisionSurface;
    	}
    	
    	public void setSkyBox(SkyBox skyBox)
    	{
    		this.skyBox = skyBox;
    	}
		
        public void Draw(float frameTime)
		{
     		// Initialize OpenGL Draw
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glLoadIdentity();
			camera.setCamera();
			frustum.calculateFrustum();
			
			if (skyBox != null)
				skyBox.Draw(camera.getPosition());

			
			foreach (Drawable d in drawables)
				d.Draw(frameTime, frustum);
			
			if (drawables2D.Count > 0)
			{
				/* Setup Ortho for 2D objects*/
	        	Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glDisable(Gl.GL_DEPTH_TEST);								// Disables Depth Testing
				Gl.glMatrixMode(Gl.GL_PROJECTION);							// Select The Projection Matrix
				Gl.glPushMatrix();											// Store The Projection Matrix
				Gl.glLoadIdentity();										// Reset The Projection Matrix
				Gl.glOrtho(0, Width, 0, Height, -1, 1);						// Set Up An Ortho Screen
				Gl.glMatrixMode(Gl.GL_MODELVIEW);							// Select The Modelview Matrix
				Gl.glPushMatrix();											// Store The Modelview Matrix
				Gl.glLoadIdentity();										// Reset The Modelview Matrix
				foreach (Drawable2D d in drawables2D)
					d.Draw(frameTime);

				/* Take back 3D modelview */
				Gl.glMatrixMode(Gl.GL_PROJECTION);							// Select The Projection Matrix
				Gl.glPopMatrix();											// Restore The Old Projection Matrix
				Gl.glMatrixMode(Gl.GL_MODELVIEW);							// Select The Modelview Matrix
				Gl.glPopMatrix();											// Restore The Old Projection Matrix
				Gl.glEnable(Gl.GL_DEPTH_TEST);
				Gl.glEnable(Gl.GL_LIGHTING);
			}

			float[] fc = new float[]{94.0f / 256.0f, 94.0f / 256.0f, 110.0f / 256.0f};
			Gl.glFogfv(Gl.GL_FOG_COLOR, fc);
            Gl.glFogi(Gl.GL_FOG_MODE, Gl.GL_LINEAR);		                    // Fog Mode
            Gl.glFogf(Gl.GL_FOG_DENSITY, 0.05f);                                // How Dense Will The Fog Be
            Gl.glHint(Gl.GL_FOG_HINT, Gl.GL_NICEST);                         // Fog Hint Value
            Gl.glFogf(Gl.GL_FOG_START, 400);                                      // Fog Start Depth
            Gl.glFogf(Gl.GL_FOG_END, 700);                                        // Fog End Depth
            Gl.glEnable(Gl.GL_FOG);  
			
			if (drawActionDelegate != null)
				drawActionDelegate(frameTime);
			
			// End OpenGL Draw
            Sdl.SDL_GL_SwapBuffers();
		}
		
		public int Width
		{ get { return this.width; } }
		
		public int Height
		{ get { return this.height; } }
	}
}
