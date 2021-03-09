using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ElectroShop.Pdf
{
    public class PdfCreator 
    {
        Document invoice = new Document(PageSize.A4);

        //use a variable to let my code fit across the page...

        public string CreatePdf()
        {
            //use a variable to let my code fit across the page...
            MemoryStream stream = new MemoryStream();

            PdfWriter.GetInstance(invoice, stream);

            invoice.Open();
            invoice.Add(new Paragraph("Hello World!"));
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
