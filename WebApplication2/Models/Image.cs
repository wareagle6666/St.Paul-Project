using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Image
    {
        private SqlDataProvider _dataProvider;
        public Image() {
            _dataProvider = new SqlDataProvider();
        }
        public Guid Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime DateTime { get; set; }
        public int ImageType { get; set; }

        public int SaveImage(Image image)
        {
            var result= _dataProvider.SaveImage(image);
            return result;
        }
        public Guid SaveNewsImage(Image image)
        {
            try {
                var result = _dataProvider.SaveNewsImage(image);
                return result;
            }
            catch(Exception e)
            {
                var error = e;
                throw new Exception();
            }

        }
        public List<Image> GetGalleryImages()
        {
            var result = _dataProvider.GetGalleryImages();
            return result;
        }
        public Image GetHomeImage()
        {
            var result = _dataProvider.GetHomeImage();
            return result;
        }
    }
}