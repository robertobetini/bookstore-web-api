﻿using Core.Entities.Enums;

namespace Core.Entities;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Edition { get; set; }
    public string Language { get; set; }
    public string Publisher { get; set; }
    public int Pages { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int Year { get; set; }
    public BookPreservation Preservation { get; set; }

    public Book() { }

    public Book(
        string title,
        string author,
        int edition,
        string language,
        string publisher,
        int pages,
        int quantity,
        double price,
        int year,
        BookPreservation preservation = BookPreservation.Undefined)
    {
        Title = title;
        Author = author;
        Edition = edition;
        Language = language;
        Publisher = publisher;
        Pages = pages;
        Quantity = quantity;
        Price = price;
        Year = year;
        Preservation = preservation;
    }
}
