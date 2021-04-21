
using System.Collections.Generic;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    public class ImageBlock
    {
        public List<ImageInfo> Images { get; private set; }
        public bool existMoreImages { get; private set; }

        public ImageBlock(List<ImageInfo> images, bool existMoreImages)
        {
            this.Images = images;
            this.existMoreImages = existMoreImages;
        }
    }
}
