using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MobileNotificationModel
    {
        public string Title { get; set; } = String.Empty;
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = String.Empty;
    }
}