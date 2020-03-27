using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Data;
using NewsWorldwide.Models;

namespace NewsWorldwide.Controllers
{
    public class CreatePDFController : Controller
    {
        public async Task<FileResult> CreatePDF(string country = "bg", string language="en", string type = "", string criteria = "")
        {
            MemoryStream workStream = new MemoryStream();
            //StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("News_Pdf_" + dTime.ToString("dd_MM_yyyy") + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 2 columns  
            PdfPTable tableLayout = new PdfPTable(2);
            doc.SetMargins(0f, 0f, 0f, 0f);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(await AddContentToPDF(tableLayout, country, language, type, criteria));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);
        }

        private async Task<PdfPTable> AddContentToPDF(PdfPTable tableLayout, string country, string language, string type, string criteria)
        {
            IEnumerable<ArticleViewModel> list = new List<ArticleViewModel>();
            switch (type)
            {
                case "top":
                    list = await GetNews.TopNews(country);
                    break;
                case "search":
                    list = await GetNews.GetSearchResults(criteria, language);
                    break;
                default:
                    break;
            }
            //var list = GetNews.TopNews(country);
            float[] headers = { 40, 60 };
            //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 80; //Set the PDF File witdh percentage  
                                              //Add Title to the PDF file at the top  
            tableLayout.AddCell(new PdfPCell(new Phrase("Creating PDF file using iTextsharp", new Font(Font.FontFamily.HELVETICA, 13, 1, iTextSharp.text.BaseColor.BLUE)))
            {
                Colspan = 4,
                Border = 0,
                PaddingBottom = 20,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //Add header  
            AddCellToHeader(tableLayout, "Article Title");
            AddCellToHeader(tableLayout, "Link to Article");
            //Add body
            foreach (var article in list)
            {
                AddCellToBody(tableLayout, article.Title);
                AddCellToBody(tableLayout, article.URL);
            }
            return tableLayout;
        }

        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            });
        }
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            });
        }
    }
}