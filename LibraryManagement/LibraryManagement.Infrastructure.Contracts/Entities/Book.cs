﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Contracts.Entities
{
    public partial class Book
    {
        public Book()
        {
            Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Genre { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Books")]
        public virtual Author Author { get; set; }
        [InverseProperty("Book")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}