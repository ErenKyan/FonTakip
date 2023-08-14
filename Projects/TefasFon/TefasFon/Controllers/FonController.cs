using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TefasFon.Models;
using HtmlAgilityPack;
using System.Net.Http;

namespace TefasFon.Controllers
{
    public class FonController : Controller
    {
        [HttpGet("FonView/{encodedFonKodu}")]
        public IActionResult FonView(string encodedFonKodu)
        {
            try
            {
                string fonKodu = Uri.UnescapeDataString(encodedFonKodu);
                string url = string.Format("https://www.tefas.gov.tr/FonAnaliz.aspx?FonKod={0}", fonKodu);

                string htmlContent = GetHtmlContent(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                List<string> dataList = new List<string>();

                string[] xPaths = {
                "//*[@id=\"MainContent_FormViewMainIndicators_LabelFund\"]",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[1]/li[1]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[1]/li[2]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[1]/li[3]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[1]/li[4]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[1]/li[5]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[2]/li[1]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[2]/li[2]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[1]/ul[2]/li[3]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[2]/ul/li[1]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[2]/ul/li[2]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[2]/ul/li[3]/span",
                "//*[@id=\"MainContent_PanelInfo\"]/div[2]/ul/li[4]/span"
                };

                foreach (var xPath in xPaths)
                {
                    var nodes = doc.DocumentNode.SelectNodes(xPath);

                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            dataList.Add(node.InnerText);
                        }
                    }
                    else
                    {
                        dataList.Add("Veri gelmedi.");
                    }
                }


                // Verileri model aracılığıyla view'a taşıma
                var model = new DataL
                {
                    DataList = dataList
                };

                return View("FonView", model);
            }
            catch (Exception ex)
            {
                    // eğer veri çekmede Hata olursa diye hata mesajını görüntüleme
                var model = new DataL
                {
                    DataList = new List<string> { "Hata: try-catch bloğu yakaladı." + ex.Message }
                };

                return View("FonView", model);
            }
        }
        //URL içinden HTML çekmek için.
        private string GetHtmlContent(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }


        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}