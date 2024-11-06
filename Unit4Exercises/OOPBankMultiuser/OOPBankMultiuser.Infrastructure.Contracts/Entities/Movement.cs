﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OOPBankMultiuser.Infrastructure.Contracts.Entities
{
    public partial class Movement
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Timestamp { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Movements")]
        public virtual Account Account { get; set; }
    }
}