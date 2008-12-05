using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX.ModelLoading
{
    public class ObjLoader : ModelLoader
    {
        private static NumberFormatInfo numformat;
        private Dictionary<string, Material> materials;

        public ObjLoader(Dictionary<string, Material> materials)
        {
            this.materials = materials;
            SetNumformat();
        }

        public ObjLoader(string fileName)
        {
            MtlLoader ml = new MtlLoader();
            materials = ml.LoadFile(fileName);
            SetNumformat();
        }

        public ObjLoader()
        {
            this.materials = null;
            SetNumformat();
        }

        /// <summary>
        /// A Material File (.mtl) must be set <i>before</i> the model is loaded.
        /// </summary>
        /// <param name="fileName">Path to the material file</param>
        public void SetMaterialFile(string fileName)
        {
            MtlLoader ml = new MtlLoader();
            materials = ml.LoadFile(fileName);
        }

        /// <summary>
        /// A Material Dictionary must be set <i>before</i> the model is loaded.
        /// </summary>
        /// <param name="materials"></param>
        public void SetMaterialDictionary(Dictionary<string, Material> materials)
        {
            this.materials = materials;
        }

        private void SetNumformat()
        {
            numformat = new NumberFormatInfo();
            numformat.NumberDecimalSeparator = ".";
        }

        public LoadableModel LoadModel(String fileName, String materialFile)
        {
            SetMaterialFile(materialFile);
            return LoadModel(fileName);
        }

        /// <summary>
        /// Parses a Drawable model from the specified file path.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public LoadableModel LoadModel(String fileName)
        {
            int polygonCount = 0;
            int groupCount = 0;
            if (materials == null)
            {
                Log.Write("ObjLoader: Material Dictionary is missing", LogType.Warning);
            }
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            List<Group> groups = new List<Group>();
            try
            {
                groups.Add(new Group(null));
                StreamReader reader = new StreamReader(fileStream);
                List<Vector3f> vertices = new List<Vector3f>();
                List<Vector2f> textureVertices = new List<Vector2f>();
                List<Vector3f> normalVertices = new List<Vector3f>();
                List<Material> material = new List<Material>();
                Material currentMaterial = null;
                Regex regex = new Regex(@"[\s]+");

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (line.StartsWith("g "))
                    {
                        groupCount++;
                        string[] splittedGroupLine = line.Split(new char[] {' '}, 2);
                        Group newGroup = new Group(splittedGroupLine[1]);
                        if(currentMaterial != null)
                            newGroup.SetMaterial(currentMaterial);
                        groups.Add(newGroup);
                        
                    }

                    else if (line.StartsWith("usemtl "))
                    {
                        if (materials != null)
                        {
                            string[] splittedGroupLine = line.Split(new char[] { ' ' }, 2);
                            try
                            {
                                currentMaterial = materials[splittedGroupLine[1]];
                                groups[groups.Count-1].SetMaterial(currentMaterial);
                            }
                            catch (KeyNotFoundException)
                            {
                                Log.Write("Material \"" + splittedGroupLine[1] + "\" not found in dictionary", LogType.Warning);
                            }
                        }
                    }
                    else if (line.StartsWith("v "))
                    {
                        string[] split = regex.Split(line);
                        vertices.Add(new Vector3f(ToFloat(split[1]), ToFloat(split[2]), ToFloat(split[3])));
                    }
                    else if (line.StartsWith("vt "))
                    {
                        string[] split = regex.Split(line);
                        textureVertices.Add(new Vector2f(ToFloat(split[1]), ToFloat(split[2])));
                    }
                    else if (line.StartsWith("vn "))
                    {
                        string[] split = regex.Split(line);
                        normalVertices.Add(new Vector3f(ToFloat(split[1]), ToFloat(split[2]), ToFloat(split[3])));
                    }
                    else if (line.StartsWith("f "))
                    {
                        polygonCount++;
                        List<Vector3f> v = new List<Vector3f>();
                        List<Vector2f> vt = new List<Vector2f>();
                        List<Vector3f> vn = new List<Vector3f>();
                        string[] splittedFaceLine = regex.Split(line);
                        Regex slash = new Regex(@"[/]");
                        Match match = slash.Match(line);
                        int max = 0;
                        for (int i = 1; i < splittedFaceLine.Length; i++)
                        {
                            string[] splittedTriplet = slash.Split(splittedFaceLine[i]);

                            int length = splittedTriplet.Length;
                            if (!splittedTriplet[0].Equals(""))
                                v.Add(vertices[ToInt(splittedTriplet[0])-1]);
                            if (length > 1 && !splittedTriplet[1].Equals(""))
                                vt.Add(textureVertices[ToInt(splittedTriplet[1])-1]);
                            if (length > 2 && !splittedTriplet[2].Equals(""))
                                vn.Add(normalVertices[ToInt(splittedTriplet[2])-1]);
                            if (i > max)
                                max = i;
                        }
                        groups[groups.Count-1].AddFace(new Face(v.ToArray(), vt.ToArray(), vn.ToArray(), max));
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }
            List<Group> cleanGroups = new List<Group>();
            foreach (Group g in groups)
            {
                if (g.Count() > 0)
                    cleanGroups.Add(g);
            }
            Log.Write("Loaded \"" + fileName + "\" with " + polygonCount + " polygons in " + groupCount + " groups.", LogType.Info);
            return new LoadableModel(cleanGroups.ToArray());
        }

        private float ToFloat(string str)
        {
            return Convert.ToSingle(str, numformat);
        }

        private int ToInt(string str)
        {
            return Convert.ToInt32(str, numformat);
        }
    }
}
