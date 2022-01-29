using RestSharp;
using System.Text;
using System.Text.Json;

namespace Shkiya
{
    public partial class Form1 : Form
    {
        static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler((o, e) =>
            {
                base.Capture = false;
                Message message = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref message);
            });
            Refresh();
        }

        void Refresh()
        {
            var today = DateTime.UtcNow.Date.ToString("MM-dd-yyyy");
            var arr = today.Split('-');
            var client = new RestClient($"https://ru.chabad.org/webservices/zmanim/zmanim/Get_Zmanim?locationid=247&locationtype=1&tdate={today}&jewish=Halachic-Times.htm&aid=143790&startdate={arr[1]}%2F{arr[0]}%2F{arr[2]}&enddate={arr[1]}%2F{arr[0]}%2F{arr[2]}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("authority", "ru.chabad.org");
            request.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"96\", \"Google Chrome\";v=\"96\"");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36 co_ajax/2.0");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("sec-fetch-mode", "cors");
            request.AddHeader("sec-fetch-dest", "empty");
            label2.Text = today;
            request.AddHeader("referer", $"https://ru.chabad.org/calendar/zmanim_cdo/locationid/247/locationtype/1/tdate/{today}/jewish/Halachic-Times.htm");
            request.AddHeader("accept-language", "ru-BY,ru;q=0.9,en-US;q=0.8,en;q=0.7,he-IL;q=0.6,he;q=0.5,ru-RU;q=0.4");
            request.AddHeader("cookie", "autoplay=true; __utmc=90962713; __utmc=1; _gac_UA-74858-1=1.1638032697.Cj0KCQiAy4eNBhCaARIsAFDVtI0jLrNlvad9t43aW-hQLPkxBOwRJfUoThqHwBdPY5Epyg1NRsctkqsaAqfVEALw_wcB; _gac_UA-74858-9=1.1638032697.Cj0KCQiAy4eNBhCaARIsAFDVtI0jLrNlvad9t43aW-hQLPkxBOwRJfUoThqHwBdPY5Epyg1NRsctkqsaAqfVEALw_wcB; _gac_UA-74858-12=1.1638032697.Cj0KCQiAy4eNBhCaARIsAFDVtI0jLrNlvad9t43aW-hQLPkxBOwRJfUoThqHwBdPY5Epyg1NRsctkqsaAqfVEALw_wcB; _gac_UA-74858-13=1.1638032697.Cj0KCQiAy4eNBhCaARIsAFDVtI0jLrNlvad9t43aW-hQLPkxBOwRJfUoThqHwBdPY5Epyg1NRsctkqsaAqfVEALw_wcB; _gcl_au=1.1.1735651458.1638032705; _gcl_aw=GCL.1638032708.Cj0KCQiAy4eNBhCaARIsAFDVtI0jLrNlvad9t43aW-hQLPkxBOwRJfUoThqHwBdPY5Epyg1NRsctkqsaAqfVEALw_wcB; spcnt=-1; _gid=GA1.2.2060263076.1638886536; __utmz=90962713.1638886537.8.8.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); __utmz=1.1638886537.11.8.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); __utma=90962713.341516539.1638032693.1638886537.1638907123.9; __utma=1.341516539.1638032693.1638886537.1638907123.12; _ga=GA1.2.341516539.1638032693; _ga_6DZ1MR0QYE=GS1.1.1638907121.9.1.1638907326.0; _gali=content; li=1638963632058; __cf_bm=l3aN.ve.yyclgEz827XQKpafTjwBEG.qF1GvRg8gTa0-1638963633-0-AYv2gaHMJx8YYrmxmRyssJlH2QU5ri6ior7RDar+vY2mbKIhLZ6B+hOmJwGkuqXuHvc3KJioJiEoS53gMFYyqouV7M0W0Kre/f4h7RfG9Ns1; __cf_bm=_VbWyKRQil2rXushDO405mEv.lG0FvrWM9gaVWIm6TE-1638968240-0-Ac9yk7+Wc7F5233vHr2dOQs9Y8aohbDSyGNfmGpOCUyIyWZOuVag6YqSSf83NuLU3RTA3T8Ace84R3wqWdY8t/c/qcZKkrPF9z+eaiR/hUdb");
            IRestResponse response = client.Execute(request);
            var json = JsonSerializer.Deserialize<Rootobject>(response.Content);
            var time = json?.Days[0].TimeGroups[10].Items[0].Zman; // можно за пол часа напоминание
            label1.Text = $"Ўки€ в {time}";
        }

        private void Form1_Activated_1(object sender, EventArgs e)
        {
            //HttpResponseMessage response = client.GetAsync("https://ru.chabad.org/webservices/zmanim/zmanim/Get_Zmanim?locationid=247&locationtype=1&tdate=12-9-2021&jewish=Halachic-Times.htm&aid=143790&startdate=12%2F10%2F2021&enddate=12%2F10%2F2021").Result;
            //response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;
            //this.MinimizeBox = false;
            //this.SendToBack();
        }

        public class Rootobject
        {
            //public Footnotes Footnotes { get; set; }
            //public string[] FootnoteOrder { get; set; }
            public Day[] Days { get; set; }
            //public Groupheading[] GroupHeadings { get; set; }
            //public bool IsNewLocation { get; set; }
            //public bool IsDefaultLocation { get; set; }
            //public string LocationName { get; set; }
            //public string City { get; set; }
            //public Coordinates Coordinates { get; set; }
            //public string LocationDetails { get; set; }
            //public string EndDate { get; set; }
            //public DateTime GmtStartDate { get; set; }
            //public DateTime GmtEndDate { get; set; }
            //public bool IsAdvanced { get; set; }
            //public string PageTitle { get; set; }
            //public string LocationId { get; set; }
        }

        public class Footnotes
        {
            public string LaterMincha { get; set; }
        }

        public class Coordinates
        {
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

        public class Day
        {
            public Timegroup[] TimeGroups { get; set; }
            //public string Parsha { get; set; }
            //public string DisplayDate { get; set; }
            //public int DayOfWeek { get; set; }
            //public bool IsDstActive { get; set; }
            //public bool IsDayOfDstChange { get; set; }
            //public DateTime GmtDate { get; set; }
        }

        public class Timegroup
        {
            //public string Title { get; set; }
            //public string ZmanType { get; set; }
            //public string FootnoteType { get; set; }
            //public string HebrewTitle { get; set; }
            //public int Order { get; set; }
            //public string EssentialZmanType { get; set; }
            //public string EssentialTitle { get; set; }
            public Item[] Items { get; set; }
            //public int InfoMessageIndex { get; set; }
            //public string InfoMessage { get; set; }
        }

        public class Item
        {
            //public string EssentialZmanType { get; set; }
            //public int Order { get; set; }
            //public string Title { get; set; }
            //public string FootnoteType { get; set; }
            //public string EssentialTitle { get; set; }
            //public string Link { get; set; }
            //public string OpinionInformation { get; set; }
            //public string OpinionDescription { get; set; }
            //public string TechnicalInformation { get; set; }
            //public string ZmanType { get; set; }
            public string Zman { get; set; }
            //public DateTime Date { get; set; }
            //public int InfoMessageIndex { get; set; }
            //public string InfoMessage { get; set; }
            //public bool Default { get; set; }
        }

        public class Groupheading
        {
            public string EssentialZmanType { get; set; }
            public int Order { get; set; }
            public string EssentialTitle { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}