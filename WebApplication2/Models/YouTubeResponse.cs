using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class YouTubeResponse
    {
        public string kind { get; set; } = String.Empty;
        public string etag { get; set; } = String.Empty;

        public YouTubeResponsePageInfo pageInfo { get; set; } = new YouTubeResponsePageInfo();

        public List<Object> items { get; set; } = new List<Object>();

    }

    public class YouTubeResponsePageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; } 
    }
}