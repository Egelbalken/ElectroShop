using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
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


        Document invoice = new Document(PageSize.A4);

        //use a variable to let my code fit across the page...

        public string CreatePdf(int orderId)
        {
            //use a variable to let my code fit across the page...
            MemoryStream stream = new MemoryStream();

            PdfWriter.GetInstance(invoice, stream);
            var order = _applicationDbContext.Orders.Find(orderId);
            var orderDetails = _applicationDbContext.OrderDetailModel.Include(or => or.Order).Include(op => op.Product).Where(id => id.Order.OrderId == order.OrderId);

            invoice.Open();
            invoice.Add(new Paragraph("-------------------------------------------------------------------------------------------------------"));
            invoice.Add(new Paragraph("------------------------------------  Invoice from ElectroShop AB  ------------------------------------"));
            invoice.Add(new Paragraph("-------------------------------------------------------------------------------------------------------"));
            invoice.Add(new Paragraph("                      Electro-cutied-order                       "));
            invoice.Add(new Paragraph(""));
            invoice.Add(new Paragraph("/////////////////////////   Details  ////////////////////////////"));
            invoice.Add(new Paragraph(""));
            invoice.Add(new ListItem("Order Id: " + order.OrderId));
            invoice.Add(new Paragraph("-----------------------------------------------------------------"));
            foreach (var detail in orderDetails)
            {
                invoice.Add(new Paragraph(detail.Product.ProductId));
                invoice.Add(new Paragraph(detail.Product.Name));
                invoice.Add(new Paragraph(detail.Product.Price.ToString()));
                invoice.Add(new Paragraph(""));
                invoice.Add(new Paragraph("--------------------------------------------------------------"));
            }
            // start



            // End

            invoice.Close();

            var Pdf =
            stream.ToArray().Select(s => (char)s);

            stream.Dispose();

            return string.Join("", Pdf);
        }
    }
}
