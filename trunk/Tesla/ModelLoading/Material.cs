using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tesla.Utils;
using Tao.OpenGl;

namespace Tesla.GFX.ModelLoading
{
    public class Material
    {
        public enum IllumType
        {
            FLAT,
            SPECULAR
        }

        private float[] ambient, diffuse, specular;
        private float alpha;
        private float shininess;
        private Texture texture;
        private IllumType illumType;

        public Material(float[] ambient, float[] diffuse, float[] specular, float alpha, float shininess, IllumType illumType, Texture texture)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.alpha = alpha;
            this.shininess = shininess;
            this.illumType = illumType;
            this.texture = texture;
        }

        public void SetMaterial()
        {
            if(illumType != IllumType.FLAT && illumType == IllumType.SPECULAR)
                Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, specular);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT, ambient);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, diffuse);
            Gl.glMateriali(Gl.GL_FRONT, Gl.GL_SHININESS, (int)shininess);

            if (texture != null)
            {
                texture.Bind();
                Gl.glEnable(Gl.GL_TEXTURE_2D);
            }
        }
    }
}
