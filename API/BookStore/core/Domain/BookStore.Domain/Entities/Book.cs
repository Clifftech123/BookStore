﻿namespace BookStore.Domain.Entities;

public class Book
{
    public int  Id { get; set; }
    public string  Name  { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    
}