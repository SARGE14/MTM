using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace MTM
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly WebClient webClient;
        public string url;
        public string coin;
        public string exchange;
        public string imageName;
        public string currencyPair;
        public string priceBidAsk;
        public string errorExh;
        public string alertPair;
        public bool firstUpDown;
        public double oldPrice;
        public double newPrice;
        public double percentUpDown = 1.05f;
        public int timerload = 15;
        public float priceAlertBuyLive;
        public float priceAlertSellHit;
        public float priceAlertSellLive;
        public float priceAlertBuyHit;
        RootObject json;
        public class RootObject
        {
            /*  public double last { get; set; }
              public double high { get; set; }
              public double low { get; set; }
              public double volume { get; set; }
              public double vwap { get; set; }
              public double max_bid { get; set; }
              public double min_ask { get; set; }*/
            public double best_bid { get; set; }
            public double best_ask { get; set; }
            //public string ask { get; set; }
            //public string bid { get; set; }
            public double ask { get; set; }
            public double bid { get; set; }
            public Ticker ticker { get; set; }
        }
        public class Ticker
        {
            /*   public double high { get; set; }
               public double low { get; set; }
               public double avg { get; set; }
               public double vol { get; set; }
               public double vol_cur { get; set; }
               public double last { get; set; }*/
            public double Buy { get; set; }
            public double Sell { get; set; }
            // public int updated { get; set; }
            //  public int server_time { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
            webClient = new WebClient();
            errorText.Text = "";
            PriceGet();
        }
        private void WebTest()
        {
            using (WebClient wc = webClient)
            {
                string webPage = null;

                try
                {
                    webPage = wc.DownloadString(@url);

                    errorExh = "";
                    errorText.Text = "";
                    json = null;
                    try
                    {
                        json = JsonConvert.DeserializeObject<RootObject>(webPage);
                    }
                    catch
                    {
                        errorText.Text = "API " + exchange + " недоступно";
                        url = null;
                        json = null;
                    }
                }
                catch (WebException)
                {
                    errorExh = exchange;
                    errorText.Text = "API " + exchange + " недоступно";
                    url = null;
                }
            }
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            PriceGet();
        }

        private void PriceGet()
        {
            string urlExchange;
            //  messageText.Text = "";
            /*Livecoin*/
            exchange = "Livecoin";
            urlExchange = "https://api.livecoin.net/exchange/ticker?currencyPair=";
            currencyPair = "BTC/USD";
            url = urlExchange + currencyPair;
            WebTest();
            if (errorExh != "Livecoin" && json != null)
            {
                currencyPair = "BTC/USD";
                url = urlExchange + currencyPair;
                WebTest();

                oldPrice = double.Parse(priceBtc.Text);
                newPrice = json.best_ask;
                PriceUpDown();
                priceBtc.Text = newPrice.ToString("F");


                currencyPair = "ETH/USD";
                url = urlExchange + currencyPair;
                WebTest();

                oldPrice = double.Parse(priceEth.Text);
                newPrice = json.best_ask;
                PriceUpDown();
                priceEth.Text = newPrice.ToString("F");

                currencyPair = "PLBT/BTC";
                url = urlExchange + currencyPair;
                WebTest();

                priceBidAsk = "продажи";
                alertPair = currencyPair;
                oldPrice = double.Parse(priceBtcSell_livecoin.Text);
                newPrice = json.best_ask;
                PriceUpDown();
                priceBtcSell_livecoin.Text = newPrice.ToString("F8");

                priceBidAsk = "покупки";
                oldPrice = double.Parse(priceBtcBuy_livecoin.Text);
                newPrice = json.best_bid;
                PriceUpDown();
                priceBtcBuy_livecoin.Text = newPrice.ToString("F8");
            }


            /*HitBTC*/
            exchange = "HitBTC";
            urlExchange = "https://api.hitbtc.com/api/2/public/ticker/";
            currencyPair = "PLBTBTC";
            url = urlExchange + currencyPair;
            WebTest();
            if (errorExh != "HitBTC" && json != null)
            {
                alertPair = "PLBT/BTC";
                priceBidAsk = "продажи";
                oldPrice = double.Parse(priceBtcSell_hitbtc.Text);
                newPrice = json.ask;
                PriceUpDown();
                priceBtcSell_hitbtc.Text = newPrice.ToString("F8");

                priceBidAsk = "покупки";
                oldPrice = double.Parse(priceBtcBuy_hitbtc.Text);
                newPrice = json.bid;
                PriceUpDown();
                priceBtcBuy_hitbtc.Text = newPrice.ToString("F8");
            }
            /*YoBit*/
            exchange = "YoBit";
            urlExchange = "https://yobit.net/api/2/";
            currencyPair = "plbt_btc/ticker";
            url = urlExchange + currencyPair;
            WebTest();
            if (errorExh != "YoBit" && json != null)
            {
                oldPrice = double.Parse(priceBtcSell_yobit.Text);
                newPrice = json.ticker.Sell;

                alertPair = "PLBT/BTC";
                priceBidAsk = "продажи";
                PriceUpDown();
                priceBtcSell_yobit.Text = newPrice.ToString("F8");
                oldPrice = double.Parse(priceBtcBuy_yobit.Text);
                newPrice = json.ticker.Buy;

                priceBidAsk = "покупки";
                PriceUpDown();
                priceBtcBuy_yobit.Text = newPrice.ToString("F8");

            }
        }

        private void PriceUpDown()
        {

        }
    }
}
