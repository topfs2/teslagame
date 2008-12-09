// BillboardedQuad.cs created with MonoDevelop
// User: topfs at 11:03 PM 12/1/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tao.OpenGl;

using Tesla.Common;

namespace Tesla.GFX
{
	
	
	public class BillboardedQuad : Drawable
	{
		private static int displayList = -1;
	
		private Vector2f size;
		private Vector3f position;
		private Texture texture;
		private Camera camera;
		public BillboardedQuad(Texture texture, Camera activeCamera, Vector3f position, Vector2f size)
		{
			this.texture = texture;
			this.position = position;
			this.size = size;
			this.camera = activeCamera;
			
			if (displayList == -1)
				createDisplayList();
		}

		private static void createDisplayList()
		{
			displayList = Gl.glGenLists(1);
            Gl.glNewList(displayList, Gl.GL_COMPILE);		
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-0.5f, -0.5f, 0.0f);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-0.5f,  0.5f, 0.0f);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f( 0.5f,  0.5f, 0.0f);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f( 0.5f, -0.5f, 0.0f);
			Gl.glEnd();
            Gl.glEndList();
		}

		public void Draw (float frameTime, Frustum frustum)
		{
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glColor3f(1.0f, 1.0f, 1.0f);
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glTranslatef(position.x, position.y, position.z);
			Gl.glScalef(size.x, size.y, 1.0f);
			texture.Bind();
			Gl.glCallList(displayList);
		}
	}
}
