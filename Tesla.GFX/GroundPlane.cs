// Plane.cs created with MonoDevelop
// User: topfs at 1:32 AM 11/1/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

using Tesla.Common;

using Tao.OpenGl;

namespace Tesla.GFX
{
	
	
	public class GroundPlane : Drawable
	{
		Vector3f pointA, pointB, pointC, pointD;
		Vector3f normal;
		Texture texture;
		int tileV, tileU;
		
		public GroundPlane(Texture texture, int tileU, int tileV, Vector3f pointA, float distance) : this(texture, tileU, tileV, pointA + new Vector3f(distance / 2.0f, 0.0f, distance / 2.0f), pointA + new Vector3f(distance / 2.0f, 0.0f, -distance / 2.0f), pointA + new Vector3f(-distance / 2.0f, 0.0f, -distance / 2.0f), pointA + new Vector3f(-distance / 2.0f, 0.0f, distance / 2.0f))
		{
		
		}
		
		public GroundPlane(Texture texture, int tileU, int tileV, Vector3f pointA, float width, float depth) : this(texture, tileU, tileV, pointA + new Vector3f(width / 2.0f, 0.0f, depth / 2.0f), pointA + new Vector3f(width / 2.0f, 0.0f, -depth / 2.0f), pointA + new Vector3f(-width / 2.0f, 0.0f, -depth / 2.0f), pointA + new Vector3f(-width / 2.0f, 0.0f, depth / 2.0f))
		{
		
		}
		
		public GroundPlane(Texture texture, int tileU, int tileV, Vector3f pointA, Vector3f pointB, Vector3f pointC, Vector3f pointD)
		{
			this.texture = texture;
		
			this.tileU = tileU;
			this.tileV = tileV;
		
			this.pointA = pointA;
			this.pointB = pointB;
			this.pointC = pointC;
			this.pointD = pointD;
			
			Vector3f vectorA = pointA.diff(pointB);
			Vector3f vectorB = pointA.diff(pointD);
			
			normal = vectorA.Cross(vectorB);
			normal.Normalize();
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			//Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			if (texture != null)
				texture.Bind();
			Gl.glBegin(Gl.GL_QUADS);	
			Gl.glNormal3fv(normal.vector);
			Gl.glTexCoord2f(0	 , 0);		Gl.glVertex3fv(pointA.vector);
			Gl.glTexCoord2f(0	 , tileV);	Gl.glVertex3fv(pointB.vector);
			Gl.glTexCoord2f(tileU, tileV); 	Gl.glVertex3fv(pointC.vector);
			Gl.glTexCoord2f(tileU, 0); 		Gl.glVertex3fv(pointD.vector);
			texture.UnBind();
			Gl.glEnd();
		}
	}
}
