﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infrastructure.Contracts.Entities
{
    public partial class PopulationCount
    {
        [Key]
        public int Id { get; set; }
        public int Counter { get; set; }
        [Column(TypeName = "date")]
        public DateTime Year { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("PopulationCount")]
        public virtual Country IdNavigation { get; set; }
    }
}