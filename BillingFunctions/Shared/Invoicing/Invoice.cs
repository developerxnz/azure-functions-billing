﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingFunctions
{
    public class Invoice
    {
        public string Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public List<InvoiceLine> Lines { get; set; }
        public decimal TotalCost { get; set; }

        public static Invoice Create(string customerCode,int year,int month)
        {
            return new Invoice
            {
                Customer = customerCode,
                InvoiceNumber = $"{customerCode}/{month}/{year}",
                Description = $"Invoice for insurance policies for {month}/{year}",
                Lines = new List<InvoiceLine>(),
                TotalCost = 0M
            };
        }

        public void BillItems(List<BillingItem> itemsToBill)
        {
            var groups = itemsToBill
                    .GroupBy(bi => bi.ProductCode);

            foreach (var itemGroup in groups)
            {
                Lines.Add(new InvoiceLine
                {
                    ItemName = $"Policy {itemGroup.Key}",
                    Cost = itemGroup.Sum(i=>Convert.ToDecimal(i .Amount))
                });
            }

            TotalCost = Lines.Sum(l => l.Cost);
        }
    }

    public class InvoiceLine
    {
        public string ItemName { get; set; }
        public decimal Cost { get; set; }
    }
}
