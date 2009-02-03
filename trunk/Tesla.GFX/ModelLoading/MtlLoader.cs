using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Tesla.GFX.ModelLoading
{
    public class MtlLoader
    {
        private NumberFormatInfo numformat;
        private Regex regex;

        public MtlLoader()
        {
            numformat = new NumberFormatInfo();
            numformat.NumberDecimalSeparator = ".";
            regex = new Regex(@"[\s]+");
        }

        public Dictionary<string, Material> Load(string fileName)
        {
            Dictionary<string, Material> dict = new Dictionary<string, Material>();
            Dictionary<string, Texture> textureList = new Dictionary<string, Texture>();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                StreamReader reader = new StreamReader(fileStream);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();
                    string[] words = regex.Split(line);
                    if (words[0].Trim().Equals("newmtl"))
                    {
                        GetMaterial(reader, words[1], dict, fileName, textureList);
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }
            return dict;
        }

        private void GetMaterial(StreamReader reader, string name, Dictionary<string, Material> dict, string fileName, Dictionary<string, Texture> textureList)
        {
            Material material = new Material();
            float[] ambient = new float[4];
            float[] diffuse = new float[4];
            float[] specular = new float[4];
            float alpha = ambient[3] = diffuse[3] = specular[3] = 1.0f;
            float shininess = 0.2f;
            Material.IllumType illumType = Material.IllumType.SPECULAR;
            Texture texture = null;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Trim();
                string[] words = regex.Split(line);
                string firstWord = words[0].ToLower();
                if(firstWord.Equals("newmtl"))
                {
                    GetMaterial(reader, words[1].Trim(), dict, fileName, textureList);
                }
                else if (firstWord.Equals("ka"))
                {
                    for(int i = 0; i < 3; i++)
                        ambient[i] = (ToFloat(words[i+1]));
                }
                else if (firstWord.Equals("kd"))
                {
                    for (int i = 0; i < 3; i++)
                        diffuse[i] = (ToFloat(words[i + 1]));
                }
                else if (firstWord.Equals("ks"))
                {
                    for (int i = 0; i < 3; i++)
                        specular[i] = (ToFloat(words[i + 1]));
                }
                else if (firstWord.Equals("d") || firstWord.Equals("tr"))
                {
                    alpha = ambient[3] = diffuse[3] = specular[3] = ToFloat(words[1]);
                }
                else if (firstWord.Equals("ns"))
                {
                    shininess = ToFloat(words[1]);
                }
                else if (firstWord.Equals("illum"))
                {
                    if (words[1].Equals("1"))
                        illumType = Material.IllumType.FLAT;
                    else if (words[1].Equals("2"))
                        illumType = Material.IllumType.SPECULAR;

                }
                else if (firstWord.Equals("map_kd") || firstWord.Equals("map_ks"))
                {
                    int lastS = fileName.LastIndexOf('/');
                    string filePath = fileName.Substring(0, lastS+1);
                    if (words.Length > 1)
                    {
                        if (!textureList.ContainsKey(words[1]))
                        {
                            texture = new BasicTexture(new Pixmap(filePath + words[1]));
                            //texture = Texture.CreateFromFile(filePath + words[1]);
                            textureList.Add(words[1], texture);
                        }
                        else
                        {
                            texture = textureList[words[1]];
                        }
                    }
                }
            }
            material.SetAmbient(ambient);
            material.SetDiffuse(diffuse);
            material.SetSpecular(specular);
            material.SetAlpha(alpha);
            material.SetShininess(shininess);
            material.SetIllumType(illumType);
            material.SetTexture(texture);
            dict.Add(name, material);
        }

        private float ToFloat(string number){
            return Convert.ToSingle(number, numformat);
        }
    }
}
