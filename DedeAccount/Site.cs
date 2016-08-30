using System;
using System.Collections.Generic;
using System.Text;

namespace DedeAccount
{
    [Serializable]
    public class Site
    {
        public Site()
        {
            ID = Guid.NewGuid();
        }
        public bool IsAdd { get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Passowrd { get; set; }
        public string Remark { get; set; }
        public string  AdminUrl { get; set; }
        public string  Url { get; set; }
        public string AdminFolder { get; set; }
    }
}
