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

        public Material()
        {
            this.ambient = new float[] {0, 0, 0, 0};
            this.diffuse = new float[] { 0, 0, 0, 0 };
            this.specular = new float[] { 0, 0, 0, 0 };
            this.alpha = 0;
            this.shininess = 0;
            this.illumType = IllumType.FLAT;
            this.texture = null;
        }

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
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                texture.Bind();
                Gl.glEnable(Gl.GL_TEXTURE_2D);
            }
            else
            {
                Gl.glDisable(Gl.GL_TEXTURE_2D);
            }
        }

        public void SetAmbient(float[] ambient){ this.ambient = ambient; }
        public void SetDiffuse(float[] diffuse) { this.diffuse = diffuse; }
        public void SetSpecular(float[] specular) { this.specular = specular; }
        public void SetAlpha(float alpha) { this.alpha = alpha; }
        public void SetShininess(float shininess) { this.shininess = shininess; }
        public void SetTexture(Texture texture) { this.texture = texture; }
        public void SetIllumType(IllumType illumType) { this.illumType = illumType; }

        public float[] GetAmbient() { return ambient; }
        public float[] GetDiffuse() { return diffuse; }
        public float[] GetSpecular() { return specular; }
        public float GetAlpha() { return alpha; }
        public float GetShininess() { return shininess; }
        public Texture GetTexture() { return texture; }
        public IllumType GetIllumType() { return illumType; }
    }
}
