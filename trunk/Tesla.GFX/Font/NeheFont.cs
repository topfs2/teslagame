// NeheFont.cs created with MonoDevelop
// User: topfs at 9:25 PM 11/4/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX.Font
{
	public enum RenderType
    {
        Blended = 0,
        Solid = 1,
    }

    public class RenderEffect
    {
        public bool RenderShadow;

        public int Shadow_DisplacementX;
        public int Shadow_DisplacementY;

        public Color4f Shadow_Color;

        public bool RenderGradiant;

        public float Gradiant_Rotation;
        public Color4f Gradiant_DestColor;

        public RenderType Render_Mode;

        public RenderEffect()
        {
            RenderShadow = false;
            Shadow_DisplacementX = Shadow_DisplacementY = 5;
            Shadow_Color = new Color4f(43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f);

            Render_Mode = RenderType.Blended;
        }
        public RenderEffect(bool UseShadow, bool UseGradiant)
        {
            RenderShadow = UseShadow;
            Shadow_DisplacementX = Shadow_DisplacementY = 4;
            Shadow_Color = new Color4f(43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f);

            Render_Mode = RenderType.Blended;
        }

        public RenderEffect(bool UseShadow, bool UseGradiant, RenderType RenderMode)
        {
            RenderShadow = UseShadow;
            Shadow_DisplacementX = Shadow_DisplacementY = 4;
            Shadow_Color = new Color4f(43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f, 43.0f/256.0f);

            Render_Mode = RenderMode;
        }

    }
	
	public class NeheFont : Font
	{
		Texture texture;
		
        private int fontbase;

        public int CharSet; // Can only be 1 or 0, nothing else

        private int size;

        private RenderEffect Effects;

        public NeheFont(Texture texture, int Size)
        {
            this.size = Size;

            this.texture = texture;
            Effects = new RenderEffect();
            BuildFont();
        }
		
		private void BuildFont()
        {
            int loop;

            float cx;                                                           // Holds Our X Character Coord
            float cy;                                                           // Holds Our Y Character Coord
            fontbase = Gl.glGenLists(256);                                      // Creating 256 Display Lists
            texture.Bind();
            for (loop = 0; loop < 256; loop++)
            {                                 // Loop Through All 256 Lists
                cx = ((float)(loop % 16)) / 16.0f;                             // X Position Of Current Character
                cy = ((float)(loop / 16)) / 16.0f;                             // Y Position Of Current Character
                Gl.glNewList(fontbase + loop, Gl.GL_COMPILE);                   // Start Building A List
                Gl.glBegin(Gl.GL_QUADS);                                    // Use A Quad For Each Character
                Gl.glTexCoord2f(cx, 1 - cy - 0.0625f);                  // Texture Coord (Bottom Left)
                Gl.glVertex2i(0, 0);                                    // Vertex Coord (Bottom Left)
                Gl.glTexCoord2f(cx + 0.0625f, 1 - cy - 0.0625f);        // Texture Coord (Bottom Right)
                Gl.glVertex2i(size, 0);                                   // Vertex Coord (Bottom Right)
                Gl.glTexCoord2f(cx + 0.0625f, 1 - cy);                  // Texture Coord (Top Right)
                Gl.glVertex2i(size, size);                                  // Vertex Coord (Top Right)
                Gl.glTexCoord2f(cx, 1 - cy);                            // Texture Coord (Top Left)
                Gl.glVertex2i(0, size);                                   // Vertex Coord (Top Left)
                Gl.glEnd();                                                 // Done Building Our Quad (Character)
                Gl.glTranslated((float)this.size * 0.6, 0, 0);                                  // Move To The Right Of The Character
                Gl.glEndList();                                                 // Done Building The Display List
            }                                                                   // Loop Until All 256 Are Built
        }
		
		public void Draw( string text, Point2f position, Color4f color)
        {
            if (Effects.RenderShadow && color != Effects.Shadow_Color)
                Draw(text, new Point2f(position.x + Effects.Shadow_DisplacementX, position.y - Effects.Shadow_DisplacementY), Effects.Shadow_Color);


            if (CharSet != 1 && CharSet != 0)
                CharSet = 1;

            Gl.glColor4f(color.r, color.g, color.b, color.a);
            texture.Bind();										                     // Select Our Font Texture

            if (Effects.Render_Mode == RenderType.Blended)
                Gl.glBlendFunc(Gl.GL_ONE, Gl.GL_ONE);
            else if (Effects.Render_Mode == RenderType.Solid)
                Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);  //perfekt för alla mina TexturedQuad
            Gl.glPushMatrix();
            Gl.glTranslatef(position.x, position.y, 0);                                   // Position The Text (0,0 - Bottom Left)
            Gl.glListBase(fontbase - 32 + (128 * CharSet));             // Choose The Font Set (0 or 1)
            // .NET: We can't draw text directly, it's a string!
            byte[] textbytes = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
                textbytes[i] = (byte)text[i];
            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_BYTE, textbytes);// Write The Text To The Screen
            
            Gl.glPopMatrix();
        }
	}
}
