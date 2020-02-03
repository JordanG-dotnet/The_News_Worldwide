using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsWorldwide.Models;

namespace NewsWorldwide.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult SearchResult(string criteria)
        {
            if (criteria != null)
            {
                var searchViewModel = new SearchViewModel();
                searchViewModel.Criteria = criteria;
                var articlesResponse = GetSearchResults(criteria);

                searchViewModel.Articles = articlesResponse.Articles;
                return View(searchViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public FileResult CreatePDF(string criteria)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("TopNews_Pdf_" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 2 columns  
            PdfPTable tableLayout = new PdfPTable(2);
            doc.SetMargins(0f, 0f, 0f, 0f);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(AddContentToPDF(tableLayout, criteria));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);
        }

        private PdfPTable AddContentToPDF(PdfPTable tableLayout, string criteria)
        {
            var list = GetSearchResults(criteria);
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
            foreach (var article in list.Articles)
            {
                AddCellToBody(tableLayout, article.Title);
                AddCellToBody(tableLayout, article.Url);
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
        private ArticlesResult GetSearchResults(string criteria)
        {
            var apiKey = "10c6e0f03663414faeeb4ec6bc798bf4";
            var newsApiClient = new NewsApiClient(apiKey);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = criteria,
                Language = Languages.EN
            });

            return articlesResponse;
        }
    }
}