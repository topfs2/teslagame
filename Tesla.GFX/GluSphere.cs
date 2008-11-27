// GlutSphere.cs created with MonoDevelop
// User: topfs at 12:06 PM 11/1/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

using Tao.OpenGl;

namespace Tesla.GFX
{
	
	
	public class GluSphere : Drawable
	{
		Point3f position;
		float radius;
		int slices;
		Glu.GLUquadric quad;
		
		float angle;
        float x_axis;
        float y_axis;
        float z_axis;
		//Texture texture;
		public GluSphere(/*Texture texure, */Point3f position, float radius, int slices)
		{
			//this.texture = texture;
			this.position = position;
			this.radius = radius;
			this.slices = slices;

			quad = Glu.gluNewQuadric();
			
			Glu.gluQuadricNormals(quad, Glu.GLU_SMOOTH);	// Create Smooth Normals ( NEW )
			Glu.gluQuadricTexture(quad, Gl.GL_TRUE);		// Create Texture Coords ( NEW )
			Glu.gluQuadricDrawStyle(quad, Glu.GLU_LINE);
			
			angle  = 0.0f;
	        x_axis = 0.0f;
	        y_axis = 0.0f;
	        z_axis = 0.0f;
		}
		
		public void setPosition(Point3f position)
		{
			this.position = position;
		}
		
		private static float RAD_TO_DEG(float a)
        {
            return 57.295779513082320876798154814105f * a;
        }
		
		public void setRotation(float W, float X, float Y, float Z)
		{
            // convert the quaternion to an axis angle so we can put the 
            // rotation into glRotatef()

            float cos_a = W;
            float angle = (float)(Math.Acos(cos_a) * 2.0f);
            float sin_a = (float)(Math.Sqrt(1.0f - cos_a * cos_a));
            if (Math.Abs(sin_a) < 0.0005f)
                sin_a = 1.0f;
            sin_a = 1.0f / sin_a;

            angle = RAD_TO_DEG(angle);
            x_axis = X * sin_a;
            y_axis = Y * sin_a;
            z_axis = Z * sin_a;

            // get the sphere radius
            //float sphere_radius = Ode.dGeomSphereGetRadius(sphere);

            // transform and draw the sphere
            /*Gl.glPushMatrix();

                Gl.glTranslatef(position.X, position.Y, position.Z);
                Gl.glRotatef(angle, x_axis, y_axis, z_axis);
                Glut.glutWireSphere(sphere_radius, 15, 15);

            Gl.glPopMatrix();*/
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			//texture.Bind();
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDisable(Gl.GL_TEXTURE_2D);
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			//Gl.glLoadIdentity();

			Gl.glTranslatef(position.x, position.y, position.z);
			Gl.glRotatef(angle, x_axis, y_axis, z_axis);
			Glu.gluSphere(quad, radius, slices, slices);
			
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glPopMatrix();
		}
	}
}
