using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WfDesignerWpf
{
    public class Document
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal Value { get; set; }
    }
}
