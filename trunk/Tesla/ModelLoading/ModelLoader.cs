using System;
using System.Collections.Generic;
using System.Text;
using Tesla.GFX;
using Tesla.Common;

namespace Tesla.GFX.ModelLoading
{
    public interface ModelLoader
    {
        Drawable LoadModel(String fileName, Point3f position);
    }
}
