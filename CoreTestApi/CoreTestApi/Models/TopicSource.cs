using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApi.Models
{
    public class TopicSource
    {
        private static List<Topic> _topics = null;

        public static List<Topic> All
        {
            get
            {
                if (_topics == null)
                {
                    _topics = new List<Topic>();
                    _topics.Add(new Topic() { Id = 1, Title = "topic1", Enabled = false });
                    _topics.Add(new Topic() { Id = 2, Title = "topic2", Enabled = true });
                    _topics.Add(new Topic() { Id = 3, Title = "topic3", Enabled = false });
                    _topics.Add(new Topic() { Id = 4, Title = "topic4", Enabled = true });
                    _topics.Add(new Topic() { Id = 5, Title = "topic5", Enabled = false });
                }
                return _topics;
            }
        }
    }
}
