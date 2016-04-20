namespace WfDesignerWpf.Models
{
    public class PropertyInfo
    {
        public int Id { get; set; }
        public string PropName { get; set; }
        public string Type { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFilled { get; set; }
    }
}