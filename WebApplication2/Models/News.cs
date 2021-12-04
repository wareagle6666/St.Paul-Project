using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class News : IDisposable
    {
        private readonly IDisposable _news;
        private SqlDataProvider _dataProvider;
        public News()
        {
            _dataProvider = new SqlDataProvider();
        }
        public string NewsTitle { get; set; }
        public string Link { get; set; }
        public bool isSpotlight { get; set; }
        public String EnglishText { get; set; }
        public String ArabicText { set; get; }
        public Guid? ImageID { get; set; }
        public Guid ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }



        public int SaveNews(News news)
        {
            var result = _dataProvider.SaveNews(news);
            return result;
        }

        public List<News> GetAlltNews()
        {
            var result = _dataProvider.GetAllNews();
            return result;
        }
        public List<News> GetSpotNews()
        {
            var result = _dataProvider.GetSpotNews();
            return result;
        }

        public News GetSingleNews(Guid NewsID)
        {
            var result = _dataProvider.GetSingleNews(NewsID);
            return result;
        }

        public int UpdateNews(News news)
        {
            var result = _dataProvider.UpdateNews(news);
            return result;
        }

        public int DeleteNews(Guid NewsId)
        {
            var result = _dataProvider.DeleteNews(NewsId);
            return result;
        }
        public int MakeSpotlight(Guid NewsId)
        {
            var result = _dataProvider.MakeSpotlight(NewsId);
            return result;
        }
        public int RemoveMakeSpotlight(Guid NewsId)
        {
            var result = _dataProvider.RemoveMakeSpotlight(NewsId);
            return result;
        }
        public void Dispose()
        {
            _news?.Dispose();
        }
    }
}