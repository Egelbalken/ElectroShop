﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Data;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ElectroShop.Pdf
{
    public class PdfCreator
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PdfCreator(ApplicationDbContext applicationDb)
        {
            _applicationDbContext = applicationDb;
        }

        // Instanse of the document pdf creater.
        Document invoice = new Document(PageSize.A4);


        /// <summary>
        /// The Package ITextSharp is added to create a pdf invocise recet of the order.
        /// Sorry that the documentaion of this is bad. OBS! Can redone in javaScript.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>A byte array containing the created PDF.</returns> 
        public byte[] CreatePdf(int orderId)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter.GetInstance(invoice, stream);
                var order = _applicationDbContext.Orders
                    .Include(order => order.Receipt)
                    .SingleOrDefault(order => order.OrderId == orderId);

                var orderDetails = _applicationDbContext.OrderDetailModel
                    .Include(or => or.Order)
                    .Include(op => op.Product)
                    .Include(rp => rp.Order.Receipt)
                    .Where(id => id.Order.OrderId == order.OrderId);

                invoice.AddAuthor("ElectroShop AB");
                invoice.AddCreator("Invoice");
                invoice.AddKeywords("Order Details");
                invoice.AddSubject("Invoice order details");
                invoice.AddTitle("Invoice order details");

                invoice.Open();
                invoice.Add(new Paragraph("                                                           ORDER INVOICE"));
                invoice.Add(new Paragraph("______________________________________________________________________________"));
                invoice.Add(new Paragraph(""));
                invoice.Add(new Paragraph(""));

                foreach (var detail in orderDetails)
                {
                    invoice.Add(new Paragraph("      Product number: " + detail.Product.ProductId));
                    invoice.Add(new Paragraph("      Product Name: " + detail.Product.Name));
                    invoice.Add(new Paragraph("      Price: " + detail.Product.CalculatedPriceOff.ToString()));
                }

                invoice.Add(new Paragraph("______________________________________________________________________________"));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      Customer receipt:"));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      First name: " + order.Receipt.ReceiptFirstName));
                invoice.Add(new Paragraph("      Last name: " + order.Receipt.ReceiptLastName));
                invoice.Add(new Paragraph("      Email: " + order.Receipt.ReceiptEmailAddress));
                invoice.Add(new Paragraph("      Phone nummer: " + order.Receipt.ReceiptPhoneNumber));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      Address: " + order.Receipt.ReceiptStreetAddress));
                invoice.Add(new Paragraph("      ZipCode: " + order.Receipt.ReceiptZipCode));
                invoice.Add(new Paragraph("      State: " + order.Receipt.ReceiptState));
                invoice.Add(new Paragraph("      Country: " + order.Receipt.ReceiptCountry));

                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("______________________________________________________________________________"));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("    * * * * * * * * * * * * * * * * * * * * * * * * * * *"));
                invoice.Add(new Paragraph("    *  ElectroShop AB                                   "));
                invoice.Add(new Paragraph("    *  Lexicon Street 10004                             "));
                invoice.Add(new Paragraph("    *  30257 Lexicon City                               "));
                invoice.Add(new Paragraph("    *  Phone: 555 112 112 122                           "));
                invoice.Add(new Paragraph("    *  Email: electrosupport@electroshop.com            "));
                invoice.Add(new Paragraph("    * * * * * * * * * * * * * * * * * * * * * * * * * * *"));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("      "));
                invoice.Add(new Paragraph("______________________________________________________________________________"));


                invoice.Close();

                return stream.ToArray();
            }
        }
    }
}
