﻿namespace ProductInventoryAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }  
        public string Name { get; set; } = string.Empty;  
        public decimal Price { get; set; }  
        public int Stock { get; set; }  
        public bool IsAvailable { get; set; } 
        public int CategoryId { get; set; }  
    }
}
