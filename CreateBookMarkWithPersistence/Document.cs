using System;

namespace CreateBookMarkWithPersistence
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