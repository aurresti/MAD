
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

        public override bool Equals(object obj)
        {
            bool result = false;
            int i = 0;
            ImageBlock target = (ImageBlock)obj;
            if (Images.Count == target.Images.Count) 
            {
                for (i = 0; i < Images.Count; i++) 
                {
                    if (Images[i] != target.Images[i])
                        i = Images.Count + 2;
                }
                if (i != Images.Count + 2)
                    result = true;
            }
            
            return result;
        }
        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            int result = 0;
            foreach (ImageInfo i in Images)
                result += i.GetHashCode();
            return result;
        }
    }
}
