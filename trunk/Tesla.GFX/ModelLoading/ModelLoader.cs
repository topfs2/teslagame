using System;
using System.Collections.Generic;
using System.Text;
using Tesla.GFX;
using Tesla.Common;

namespace Tesla.GFX.ModelLoading
{
    public interface ModelLoader
    {
        LoadableModel LoadModel(String fileName);
    }
}
