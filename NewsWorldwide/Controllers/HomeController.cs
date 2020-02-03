using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsWorldwide.Models;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Text;

namespace NewsWorldwide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = TopNews();

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public FileResult CreatePDF()
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
            doc.Add(AddContentToPDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);
        }

        private PdfPTable AddContentToPDF(PdfPTable tableLayout)
        {
            var list = TopNews();
            float[] headers = {
        40,
        60
    };
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

        private List<ArticleViewModel> TopNews()
        {
            var url = "https://newsapi.org/v2/top-headlines?" +
          "country=us&" +
          "apiKey=10c6e0f03663414faeeb4ec6bc798bf4";

            var json = new WebClient().DownloadData(url);
            List<ArticleViewModel> list = new List<ArticleViewModel>();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var newJson = JsonSerializer.Deserialize<NewsViewModel>(json, options);

            foreach (var item in newJson.Articles)
            {
                list.Add(item);
            }

            return list;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
