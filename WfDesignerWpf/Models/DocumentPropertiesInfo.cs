using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace WfDesignerWpf.Models
{
    public class DocumentPropertiesInfo
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string StageId { get; set; }
        public string StageName { get; set; }
        public Type DocumentType { get; set; }
        public List<PropertyInfo> PropertiesInfo { get; set; }

    }
}