﻿namespace ProductInventoryAPI.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;  
        public decimal Price { get; set; } 
        public int Stock { get; set; }  
        public bool IsAvailable { get; set; } = true;  

        // FK
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
