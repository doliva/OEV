using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Utils
{
    class PDFUtil
    {
        void GenerarVoucherPDF(Voucher voucher)
        {
            String filePath = Environment.SpecialFolder.MyDocuments + "\\Test.pdf";
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Resources/LogoOEV.png");
            //logo.ScalePercent(25f);
            //logo.SetAbsolutePosition(doc.PageSize.Width - 150f, doc.PageSize.Height - 100f);

            //doc.Add(logo);

            //Datos compañía
            Paragraph datosCompaniaParagraph = new Paragraph("Dirección falsa 123 Piso 1 Oficina 666 \n Código Postal 1234 \n Teléfono 1533881234 \n Buenos Aires - Argentina");

            //Paragraph paragraph = new Paragraph("This is my first lines using paragraph. \n Te amo MUCHOOOOOOO");
            doc.Add(datosCompaniaParagraph);
            doc.Close();
        }
    }
}
