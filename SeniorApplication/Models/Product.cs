using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SeniorApplication.Shared;

namespace SeniorApplication.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Order]
        public Guid NegociationId { get; set; }

        [Order]
        public string Company { get; set; }

        [Order]
        public string Name { get; set; }

        [Order]
        public DateTime CreateDate { get; set; }

        [Order]
        public int Quantity { get; set; }

        [Order]
        public double Price { get; set; }

    }

    public enum ProductDefine
    {
        Id,
        Name,
        Company,
        Quantity,
        Price,
        CreateDate
    }
}