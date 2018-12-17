using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreTestApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        //[Required]
        public string Title { get; set; }
        [DefaultValue(false)]
        public bool Enabled { get; set; }

        public Topic DublData(Topic topic)
        {
            topic.Id *= 2;
            topic.Title = topic.Title + "___" + topic.Title;
            return topic;
        }
    }
}
