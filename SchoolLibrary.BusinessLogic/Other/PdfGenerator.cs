namespace SchoolLibrary.BusinessLogic.Other
{
    using System.IO;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using SchoolLibrary.BusinessModels.Models;
    

    public interface IPdfGenerator
    {
        void PdfInit(ItemBusinessModel item, ConsignmentBusinessModel consignment);

        void BarCodeGenerate(string number);

        MemoryStream PdfFinish();
    }
    /// <summary>
    /// The pdf generator for consignment and inventory barCodes
    /// </summary>
    public class PdfGenerator:IPdfGenerator
    {
       
        private PdfWriter writer;

        private Document document;

        private MemoryStream ms;

        const float width = 3f;
        const float height = 1f;
        const float DPI = 72f;
        const float hMargin = 10f;
        const float vMargin = 10f;
        const float FontSize = 11;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfGenerator"/> class.
        /// </summary>
        public PdfGenerator()
        {
            
            Rectangle rect = new Rectangle(width * DPI, height * DPI);
            this.document = new Document(rect, hMargin, hMargin, vMargin, vMargin);
            this.ms = new MemoryStream();
            this.writer = PdfWriter.GetInstance(this.document, this.ms);
            this.document.Open();
        }

        /// <summary>
        /// Inits the Pdf document and first page.
        /// </summary>
        /// <param name="item">
        /// The item params of Name and Year for first page.
        /// </param>
        /// <param name="consignment">
        /// The consignment ArivalDate for first page.
        /// </param>
        public void PdfInit(ItemBusinessModel item,ConsignmentBusinessModel consignment)
        {
            
            BaseFont baseTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            Font defaultFont = new Font(baseTimes, FontSize, Font.NORMAL, BaseColor.BLACK);
            Paragraph title1 = new Paragraph(item.Name + ".- " + item.Year, defaultFont);
            this.document.Add(title1);
            if (consignment.ArrivalDate != null)
            {
                Paragraph title2 = new Paragraph(consignment.ArrivalDate.Value.Date.ToShortDateString(), defaultFont);
                this.document.Add(title2);
            }
        }

        /// <summary>
        /// Generates the new page with barcode128.
        /// </summary>
        /// <param name="number">
        /// The string with code.
        /// </param>
        public void BarCodeGenerate(string number)
        {
            this.document.NewPage();
            
            PdfContentByte cb = this.writer.DirectContent;
            var bc = new Barcode128
            {
                Code = number,
                TextAlignment = Element.ALIGN_CENTER,
                StartStopText = true,
                CodeType = Barcode.CODE128,
                Extended = false
            };

            Rectangle rect = this.document.PageSize;
            Image img = bc.CreateImageWithBarcode(cb, BaseColor.BLACK, BaseColor.BLACK);
            var barCodeRect = new Rectangle(bc.BarcodeSize);
            var widthScale = rect.Width / barCodeRect.Width;
            var heightScale = rect.Height / barCodeRect.Height;

            Rectangle tempRect;
            if (heightScale <= widthScale)
            {
                tempRect = new Rectangle(barCodeRect.Width * heightScale - 2 * hMargin, rect.Height - 2 * vMargin);
                img.ScaleAbsolute(tempRect);
            }
            else
            {
                tempRect = new Rectangle(rect.Width - 2 * hMargin, barCodeRect.Height * widthScale - 2 * vMargin);
                img.ScaleAbsolute(tempRect);
            }

            img.SetAbsolutePosition((rect.Width - tempRect.Width) / 2, (rect.Height - tempRect.Height) / 2);
            cb.AddImage(img);
        }

        /// <summary>
        /// Close all opened streams and returns pdf
        /// </summary>
        /// <returns>
        /// The <see cref="MemoryStream"/> output of file.
        /// </returns>
        public MemoryStream PdfFinish()
        {
            try
            {
                this.document.Close();
            }
            catch (IOException exeption)
            {
                return null;
            }
            
            this.writer.Close();

            byte[] file = this.ms.ToArray();
            MemoryStream output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;
            return output;
        }


    }
}