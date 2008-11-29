using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using Tesla.Common;

namespace Tesla.GFX.ModelLoading
{
    public class LoadableModel : Drawable
    {
        private Group[] groups;
        private Point3f position;
        private int list;
        public static int currentPolygon;
        
        public LoadableModel(Group[] groups, Point3f position)
        {
            this.groups = groups;
            this.position = position;
            BuildLists();
        }

        public static void Init(int polType)
        {
            LoadableModel.currentPolygon = polType;
            if (polType == 3)
                Gl.glBegin(Gl.GL_TRIANGLES);
            else if (polType == 4)
                Gl.glBegin(Gl.GL_QUADS);
            else if (polType >= 5)
                Gl.glBegin(Gl.GL_POLYGON);
        }

        public void Draw(float frameTime, Frustum frustum)
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(position.x, position.y, position.z);
            Gl.glCallList(list);
            Gl.glPopMatrix();
        }

        public void BuildLists()
        {
            list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glFrontFace(Gl.GL_CCW);
            foreach (Group g in groups)
            {
                g.Draw();
            }
            Gl.glEndList();
        }
    }
}
