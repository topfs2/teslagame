using System;
using System.Text;
using System.Collections.Generic;
using Tao.OpenGl;
using Tesla.Utils;
namespace Tesla.GFX.ModelLoading
{
    public class Group
    {
        private string name;
        private List<Face> faces;
        private int count;
        private Material material;

        public Group(string name)
        {
            this.name = name;
            faces = new List<Face>();
            count = 0;
            material = null;
        }

        public void SetMaterial(Material material)
        {
            this.material = material;
        }

        public void AddFace(Face face)
        {
            faces.Add(face);
            count++;
        }

        public int Count()
        {
            return count;
        }

        public void Init()
        {
            LoadableModel.Init(faces[0].PolygonType());
        }

        public void Draw()
        {
            if (material != null)
            {
                material.SetMaterial();
            }
            Init();
            foreach (Face f in faces)
            {
                f.Draw();
            }
            Gl.glEnd();
        }

        public override string ToString()
        {
            return "Group \"" + name + "\" contains " + count + " faces";
        }
    }
}
