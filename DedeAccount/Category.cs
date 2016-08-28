using System;
using System.Collections.Generic;
using System.Text;

namespace DedeAccount
{
    [Serializable]
    /// <summary>
    /// 栏目
    /// </summary>
    public class Category
    {
        public Guid ID { get; set; }
        public Category()
        {
            ID = Guid.NewGuid();
            Childs = new List<Category>();
            Sites = new List<Site>();
        }

        public bool IsAdd { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public List<Category> Childs { get; set; }
        public List<Site> Sites { get; set; }
    }
}
