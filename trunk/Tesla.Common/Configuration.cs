using System;
using System.IO;

namespace Tesla.Common
{
    public class Configuration
    {
        public int resWidth, resHeight, bpp;
        public bool fullscreen;
        public string defaultPath, title;

        public Configuration()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            resWidth = 640;
            resHeight = 480;
            bpp = 32;
            fullscreen = false;
            defaultPath = "/";
            title = "Tesla";
        }

        public Configuration(string fileName)
        {
            LoadFile(fileName);
        }

        public void LoadFile(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            try
            {
                StreamReader reader = new StreamReader(fileStream);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();
                    string[] splitted = line.Split(new char[] { '=' });
                    string var = splitted[0];
                    string val = splitted[1];
                    if (var.Equals("resWidth"))
                    {
                        resWidth = Convert.ToInt16(val);
                    }
                    else if (var.Equals("resHeight"))
                    {
                        resHeight = Convert.ToInt16(val);
                    }
                    else if (var.Equals("fullscreen"))
                    {
                        if(val.Equals("true"))
                            fullscreen = true;
                        else
                            fullscreen = false;
                    }
                    else if (var.Equals("bpp"))
                    {
                        bpp = Convert.ToInt16(val);
                    }
                    else if (var.Equals("title"))
                    {
                        title = val;
                    }
                    else if (var.Equals("defaultPath"))
                    {
                        defaultPath = val;
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}
