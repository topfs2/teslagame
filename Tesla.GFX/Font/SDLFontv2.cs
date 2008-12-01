using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//using Program = System.Windows.Forms.Application;

using Tao.OpenGl;
using Tao.Sdl;

using Tesla.Common;

namespace Tesla.GFX.Font
{
    public class SDLFontv2 : Font
    {        
        string FontPath;
        int FontSize;
        FontStyle Style;
        IntPtr m_Font;
        public bool RenderBoundingBox = false;
        public bool MipMap = false; //Currently Not Working

        public TextOrigin TextRenderFrom = TextOrigin.Left;

        private SortedList<char, SDLGlyph> GlyphList = new SortedList<char, SDLGlyph>();

        private SDLFontv2(string FontPath, int Size, FontStyle Style)
        {            
            this.FontPath = GetFontPath(FontPath);

            if (SdlTtf.TTF_WasInit() != 1)
                SdlTtf.TTF_Init();

            if (this.FontPath != null)
            {
                this.m_Font = SdlTtf.TTF_OpenFont(this.FontPath, Size);
                this.FontSize = Size;
                this.Style = Style;
                switch (Style)
                {
                    case FontStyle.Bold:
                        SdlTtf.TTF_SetFontStyle(this.m_Font, SdlTtf.TTF_STYLE_BOLD);
                        break;
                    case FontStyle.Italic:
                        SdlTtf.TTF_SetFontStyle(this.m_Font, SdlTtf.TTF_STYLE_ITALIC);
                        break;
                    case FontStyle.Regular:
                        SdlTtf.TTF_SetFontStyle(this.m_Font, SdlTtf.TTF_STYLE_NORMAL);
                        break;
                    case FontStyle.Underline:
                        SdlTtf.TTF_SetFontStyle(this.m_Font, SdlTtf.TTF_STYLE_UNDERLINE);
                        break;
                }
            }
        }
        private SDLFontv2(string FontPath, int Size)
        {
            this.FontPath = GetFontPath(FontPath);

            if (SdlTtf.TTF_WasInit() == 0)
                SdlTtf.TTF_Init();

            if (this.FontPath != null)
            {
                this.m_Font = SdlTtf.TTF_OpenFont(this.FontPath, Size);
                this.FontSize = Size;
                SdlTtf.TTF_SetFontStyle(this.m_Font, SdlTtf.TTF_STYLE_NORMAL);
            }
        }


        public void Draw(string Text, Vector2f position, Color4f Color)
        {
            char[] TextCharArray = Text.ToCharArray();

            int LastIndex = 0;
            List<char[]> SubTextArrays = new List<char[]>();

            for (int i = 0; i < TextCharArray.Length; i++)
            {
                if (TextCharArray[i] == '\n')
                {
                    SubTextArrays.Add(Text.Substring(LastIndex, i - LastIndex).ToCharArray());
                    LastIndex = i + 1;
                }
            }
            SubTextArrays.Add(Text.Substring(LastIndex, Text.Length - LastIndex).ToCharArray());

            for (int i = 0; i < SubTextArrays.Count; i++)
            {
                DrawCharArray(SubTextArrays[i], position.x, position.y - (this.FontSize * i), Color);
            }
            //DrawCharArray(TextCharArray, Coord.X, Coord.Y, Color);
        }

        private void DrawCharArray(char[] TextCharArray, float X, float Y, Color4f Color)
        {
            List<SDLGlyph> TextGLTexList = new List<SDLGlyph>();
            
            foreach (char C in TextCharArray)
                TextGLTexList.Add(GetSDLGlyph(C));

            float CurrX = X;
            float Translate = 0;

            switch (this.TextRenderFrom)
            {
                case TextOrigin.Left:
                    Translate = 0;
                    break;
                case TextOrigin.Center:
                    foreach (SDLGlyph SG in TextGLTexList)
                    {
                        Translate -= SG.Metrics.Advance;
                    }
                    CurrX -= (Translate / 2);
                    break;
                case TextOrigin.Right:
                    foreach (SDLGlyph SG in TextGLTexList)
                    {
                        Translate -= SG.Metrics.Advance;
                    }
                    CurrX += Translate;
                    break;
            }
            foreach (SDLGlyph SG in TextGLTexList)
            {
                DrawGlyph(SG, CurrX, Y, Color);
                CurrX += SG.Metrics.Advance;
            }
        }

        private void DrawGlyph(SDLGlyph SGlyph, float X, float Y, Color4f Color)
        {
            Gl.glTranslatef(X, Y, 0.0f);
            Gl.glColor4f(Color.r, Color.g, Color.b, Color.a);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, SGlyph.GLTexture);
            //Gl.glBlendFunc(Gl.GL_ONE, Gl.GL_ONE);
            Gl.glCallList(SGlyph.DispList);
            Gl.glTranslatef(-X, -Y, 0.0f);


            if (RenderBoundingBox)                  //Debug
            {
                Gl.glDisable(Gl.GL_TEXTURE_2D);
                Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3f(X, Y, 0.0f);
                Gl.glVertex3f(X + SGlyph.Width, Y, 0.0f);
                Gl.glVertex3f(X + SGlyph.Width, Y - SGlyph.Height, 0.0f);
                Gl.glVertex3f(X, Y - SGlyph.Height, 0.0f);
                Gl.glVertex3f(X, Y, 0.0f);
                Gl.glEnd();
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                //Gl.glColor4f((float)SrcColor.Color.R / 255.0f, (float)SrcColor.Color.G / 255.0f, (float)SrcColor.Color.B / 255.0f, (float)SrcColor.Color.A / 255.0f);
            }
        }

        int round(double x)
        {
            return (int)(x + 0.5);
        }
        int nextpoweroftwo(int x)
        {
            double logbase2 = System.Math.Log(x) / System.Math.Log(2);
            return (int)System.Math.Round(System.Math.Pow(2, System.Math.Ceiling(logbase2)));
        }

        private SDLGlyph GetSDLGlyph(char Char)
        {
            if (!GlyphList.ContainsKey(Char))
            {
                try
                {
                    GlyphList.Add(Char, CreateGlyph(Char));
                }
                catch { }
            }
            
            return GlyphList[Char];
        }

        private SDLGlyph CreateGlyph(char Char)
        {
            Sdl.SDL_Surface initial;
            Sdl.SDL_Surface intermediary;

            //Sdl.SDL_Rect rect;
            int w, h;
            int[] texture = new int[1];
            
            Sdl.SDL_Color color = new Sdl.SDL_Color(255, 255, 255, 255);
            /* Use SDL_TTF to render our text */
            IntPtr Test = SdlTtf.TTF_RenderGlyph_Shaded(m_Font, (short)Char, color, new Sdl.SDL_Color(0, 0, 0, 0));
            //IntPtr Test = SdlTtf.TTF_RenderGlyph_Blended(m_Font, (short)Char, color);
            //initial = test;
            initial = (Sdl.SDL_Surface)System.Runtime.InteropServices.Marshal.PtrToStructure(Test, typeof(Sdl.SDL_Surface));

            /* Convert the rendered text to a known format */
            w = nextpoweroftwo(initial.w);
            h = nextpoweroftwo(initial.h);

            IntPtr Temp = Sdl.SDL_CreateRGBSurface(Sdl.SDL_SRCALPHA, w, h, 32,
                    0, 0, 0, 0);
           
            intermediary = (Sdl.SDL_Surface)System.Runtime.InteropServices.Marshal.PtrToStructure(Temp, typeof(Sdl.SDL_Surface));

            Sdl.SDL_SetAlpha(Temp, Sdl.SDL_SRCALPHA, 255);
            

            //Sdl.SDL_PixelFormat;
            Sdl.SDL_BlitSurface(Test, ref initial.clip_rect, Temp, ref intermediary.clip_rect);

            /* Tell GL about our new texture */
            Gl.glGenTextures(1, texture);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);

            if (!MipMap)
            {
                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, 4, w, h, 0, Gl.GL_BGRA,
                    Gl.GL_UNSIGNED_BYTE, intermediary.pixels);

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_ENV, Gl.GL_MODULATE);
            }
            else
            {
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGB8, w, h, Gl.GL_ABGR_EXT, Gl.GL_UNSIGNED_BYTE, intermediary.pixels);
            }

            /*int[] ExtraTex = new int[1];
            Gl.glGenTextures(1, ExtraTex);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ExtraTex[0]);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, 4, w, h, 0, Gl.GL_ABGR_EXT,
                Gl.GL_UNSIGNED_BYTE, intermediary.pixels);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_ENV, Gl.GL_MODULATE);//


            /* Clean up */
            Sdl.SDL_FreeSurface(Temp);
            Sdl.SDL_FreeSurface(Test);
            //Gl.glDeleteTextures(1, texture);
            SDLMetrics Metrics = new SDLMetrics();


            SdlTtf.TTF_GlyphMetrics(m_Font, (short)Char, out Metrics.MinX, out Metrics.MaxX, out Metrics.MinY, out Metrics.MaxY, out Metrics.Advance);
                       

            SDLGlyph ReturnGlyph = new SDLGlyph(Metrics, texture[0], w, h, Char);
            BuildList(ReturnGlyph);

            //ReturnGlyph.extraGLTex = ExtraTex[0];
            return ReturnGlyph;

        }
        private void BuildList(SDLGlyph SGlyph)
        {
            int NewList = Gl.glGenLists(1);

            Gl.glNewList(NewList, Gl.GL_COMPILE);
            Gl.glTranslatef(SGlyph.Metrics.MinX, SGlyph.Metrics.MaxY, 0.0f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(0.0f, 0.0f, 0);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(0.0f + SGlyph.Width, 0.0f, 0);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(0.0f + SGlyph.Width, 0.0f - SGlyph.Height, 0);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(0.0f, 0.0f - SGlyph.Height, 0);
            Gl.glEnd();
            Gl.glTranslatef(-SGlyph.Metrics.MinX, -SGlyph.Metrics.MaxY, 0.0f);
            Gl.glEndList();

            SGlyph.DispList = NewList;
        }

        private string CheckFontPath(string UnkownFont)
        {
            string FontPath = null;

            if (File.Exists(UnkownFont))
                FontPath = UnkownFont;
            /*else if (File.Exists(Program.StartupPath + UnkownFont))
                FontPath = Program.StartupPath + UnkownFont;
            else if (File.Exists(Program.StartupPath + "/" + UnkownFont))
                FontPath = Program.StartupPath + "/" + UnkownFont;
            else if (File.Exists(Program.StartupPath + "/Fonts/" + UnkownFont))
                FontPath = Program.StartupPath + "/Fonts/" + UnkownFont;
            else if (File.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "/Fonts/" + UnkownFont))
                FontPath = Environment.GetEnvironmentVariable("SystemRoot") + "/Fonts/" + UnkownFont;*/

            return FontPath;
        }
        private string GetFontPath(string UnkownFont)
        {
            if (this.CheckFontPath(UnkownFont) != null)
                return this.CheckFontPath(UnkownFont);
            else if (this.CheckFontPath(UnkownFont + ".ttf") != null)
                return this.CheckFontPath(UnkownFont + ".ttf");
            else if (this.CheckFontPath(UnkownFont.ToLower()) != null)
                return this.CheckFontPath(UnkownFont.ToLower());
            else if (this.CheckFontPath(UnkownFont.ToLower() + ".ttf") != null)
                return this.CheckFontPath(UnkownFont.ToLower() + ".ttf");
            else
                return null;
        }

        private void DeleteUnmanaged()
        {
            
            int[] GLTex = new int[GlyphList.Count];
            int i = 0;

            IList<SDLGlyph> GlyphListTemp = GlyphList.Values;

            foreach (SDLGlyph SGlyph in GlyphListTemp)
            {
                Gl.glDeleteLists(SGlyph.DispList, 1);
                GLTex[i] = SGlyph.GLTexture;
                i++;
            }

            Gl.glDeleteTextures(GlyphList.Count, GLTex);
        }

        public int Size
        {
            get { return this.FontSize; }
        }

        private static List<SDLFontv2> FontList = new List<SDLFontv2>();

        public static SDLFontv2 Create(string FontPath, int Size, FontStyle Style)
        {
            foreach (SDLFontv2 SdlF in FontList)
            {
                if (SdlF.FontPath == FontPath && SdlF.Size == Size && SdlF.Style == Style)

                    //Add to list igentligen
                    return SdlF;
            }
            SDLFontv2 ReturnFont = new SDLFontv2(FontPath, Size, Style);
            FontList.Add(ReturnFont);
            return ReturnFont;                       
        }
        public static SDLFontv2 Create(string FontPath, int Size) { return Create(FontPath, Size, FontStyle.Regular); }

        public static void Delete()
        {
            foreach (SDLFontv2 SdlF in FontList)
                SdlF.DeleteUnmanaged();
        }
    }

    public class SDLMetrics
    {
        public int MinX, MaxX, MinY, MaxY, Advance;
    }
    
    internal class SDLGlyph
    {
        public readonly SDLMetrics Metrics;
        public readonly int GLTexture;
        public readonly int Width, Height;
        public readonly char Char;
        public int DispList;

        public SDLGlyph(char Char)
        {
            this.Char = Char;
        }

        public SDLGlyph(SDLMetrics Metrics, int GLTexture, int Width, int Height, char Char)
        {            
            this.Metrics = Metrics;
            this.GLTexture = GLTexture;
            this.Width = Width;
            this.Height = Height;
            this.Char = Char;
        }
    }

    public enum FontStyle
    {
        Bold,
        Italic,
        Regular,
        Underline
    }

    public enum TextOrigin
    {
        Left,
        Center,
        Right
    }
}
