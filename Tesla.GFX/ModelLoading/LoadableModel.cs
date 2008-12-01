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
        private float scaleX, scaleY, scaleZ, rotX, rotY, rotZ, rotDeg;
        public static int currentPolygon;
        
        public LoadableModel(Group[] groups)
        {
            this.groups = groups;
            position = new Point3f(0, 0, 0);
            scaleX = 1.0f;
            scaleY = 1.0f;
            scaleZ = 1.0f;
            BuildLists();
        }

        public void SetPosition(Point3f position)
        {
            this.position = position;
        }

        public void SetRotation(float deg, float x, float y, float z)
        {
            rotDeg = deg;
            rotX = x;
            rotY = y;
            rotZ = z;
        }

        public void SetScaling(float x, float y, float z)
        {
            scaleX = x;
            scaleY = y;
            scaleZ = z;
        }

        public void SetSymScaling(float scale)
        {
            SetScaling(scale, scale, scale);
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
            Gl.glScalef(scaleX, scaleY, scaleZ);
            Gl.glRotatef(rotDeg, rotX, rotY, rotZ);
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
