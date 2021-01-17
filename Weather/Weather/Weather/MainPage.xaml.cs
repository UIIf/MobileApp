using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Weather
{

    class RespondWeather
    {
        public WeatherDay current { get; set; }

        public WeatherDaily[] daily { get; set; }
    }   
    class ForCity
    {
        public cord coord { get; set; }
    }
    class cord { 
        public double lon { get; set; }

        public double lat { get; set; }
    }


    class WeatherDay
    {
        public float temp { get; set; }

        public float feels_like { get; set; }

        public float pressure { get; set; }

        public float wind_speed { get; set; }

        public Weather[] weather { get; set; }


        public WeatherDay(){}
        public WeatherDay(WeatherDaily other)
        {
            temp = other.temp.day;
            feels_like = other.feels_like.day;
            pressure = other.pressure;
            wind_speed = other.wind_speed;
            weather = other.weather;
        }
    }

    class WeatherDaily
    {

        public Temperature temp { get; set; }
        public Temperature feels_like { get; set; }
        public float pressure { get; set; }
        public float wind_speed { get; set; }
        public Weather[] weather { get; set; }
    }

    class Temperature
    {
        public float day { get; set; }
    }

    class Weather
    {
        public string description { get; set; }
    }
    public partial class MainPage : ContentPage
    {

        //public class MyItem
        //{
        //    public string Rus;
        //    public string Eng;
        //    public string LatLon;
        //}

        int day = 0;
        string selected_city = "";

        private void stepper_ValueChanged(object _sender, ValueChangedEventArgs _e)
        {
            if(day == 0 && (int)(_sender as Stepper)?.Value > 1)
            {
                (_sender as Stepper).Value = 1;
            }
            day = (int)(_sender as Stepper)?.Value;
            if(day > 7)
            {
                day = 7;
                (_sender as Stepper).Value = 7;
            }
            weather_label.Text = day.ToString();
            DrawWeather();
        }

        private void DrawWeather()
        {
            if(WeatherAll.Count > 0 && day > -1 && day < WeatherAll.Count)
            {
                date_label.Text = (DateTime.Now.AddDays(day)).ToString("M");
                weather_label.Text = WeatherAll[day].weather[0].description;
                temperature_label.Text = "Temperature: = " + WeatherAll[day].temp.ToString() + " C°";
                feels_temperature_label.Text = "But it feels like: " + WeatherAll[day].feels_like.ToString() + " C°";
                wind_speed_label.Text = "Wind speed: " + WeatherAll[day].wind_speed.ToString() + " m/s";
                preasure_label.Text = "Air pressure: " + WeatherAll[day].pressure.ToString();
            }
            else
            {
                date_label.Text = (DateTime.Now.AddDays(day)).ToString("M");
                weather_label.Text = "City not choosen";
                temperature_label.Text = "Temperature: = X C°";
                feels_temperature_label.Text = "But it feels like: X C°";
                wind_speed_label.Text = "Wind speed: X m/s";
                preasure_label.Text = "Air pressure: X";
            }
        }

        //List<MyItem> CitiesList = new List<MyItem>()
        //    {
        //        new MyItem()
        //        {
        //            Rus = "Владивосток",
        //            Eng = "Vladivostok",
        //            LatLon = "lat=43.1056&lon=131.8735"
        //        },
        //        new MyItem()
        //        {
        //            Rus = "Москва",
        //            Eng = "Moscow",
        //            LatLon = "lat=55.7522&lon=37.6156"
        //        },
        //        new MyItem()
        //        {
        //            Rus = "Депутатский",
        //            Eng = "Deputatsky",
        //            LatLon = "lat=69.3&lon=139.9"
        //        }
        //    };

        List<WeatherDay> WeatherAll = new List<WeatherDay>() { };
        public MainPage()
        {
            InitializeComponent();
            City.Text = "";
            //sub.ItemsSource = CitiesList.Select(a => { return a.Eng; }).ToList();
            //sub.SelectedIndex = 0;
            DrawWeather();
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (tapsender, tape) =>
            {
                Page1 pg = new Page1();
                pg.Disappearing += (a, b) =>
                {
                    if (pg.city != "" || selected_city != pg.city)
                    {
                        selected_city = pg.city;
                        WeatherAll.Clear();
                        bool flag = true;
                        string url = "http://api.openweathermap.org/data/2.5/weather?appid=a1ad275c70c301809867deeed0b1e520&q=" + selected_city;
                        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse Res = null;
                        try
                        {
                            Res = (HttpWebResponse)Req.GetResponse();
                        }
                        catch
                        {
                            flag = false;
                        }

                        if (flag)
                        {
                            string res_str;

                            using (StreamReader stream = new StreamReader(Res.GetResponseStream()))
                            {
                                res_str = stream.ReadToEnd();
                            }
                            ForCity Fc = JsonConvert.DeserializeObject<ForCity>(res_str);
                            url = "https://api.openweathermap.org/data/2.5/onecall?exclude=minutely,hourly&units=metric&appid=a1ad275c70c301809867deeed0b1e520&lat=" + Fc.coord.lat + "&lon=" + Fc.coord.lon;
                            Req = (HttpWebRequest)WebRequest.Create(url);
                            Res = (HttpWebResponse)Req.GetResponse();
                            using (StreamReader stream = new StreamReader(Res.GetResponseStream()))
                            {
                                res_str = stream.ReadToEnd();
                            }
                            RespondWeather FinResp = JsonConvert.DeserializeObject<RespondWeather>(res_str);
                            WeatherAll.Clear();
                            WeatherAll.Add(FinResp.current);
                            for (int i = 0; i < FinResp.daily.Length; i++)
                            {
                                WeatherAll.Add(new WeatherDay(FinResp.daily[i]));
                            }
                            day = 0;
                        }
                        City.Text = selected_city;
                        DrawWeather();
                    }
                };
                
                Navigation.PushAsync(pg);
            };
            City.GestureRecognizers.Add(tapGestureRecognizer);
            City.Text = "Find";
        }


        //private void sub_SelectedIndexChanged(object _sender, EventArgs _e)
        //{
        //    string url = "http://api.openweathermap.org/data/2.5/forecast/daily?cnt=16&units=metric&appid=a1ad275c70c301809867deeed0b1e520&q=London"; //+ CitiesList[sub.SelectedIndex].Eng; lat=33.441792&lon=-94.037689
        //    string url = "https://api.openweathermap.org/data/2.5/onecall?exclude=minutely,hourly&units=metric&appid=a1ad275c70c301809867deeed0b1e520&" + CitiesList[sub.SelectedIndex].LatLon;
        //    HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(url);

        //    HttpWebResponse Res = (HttpWebResponse)Req.GetResponse();

        //    string res_str;

        //    using (StreamReader stream = new StreamReader(Res.GetResponseStream()))
        //    {
        //        res_str = stream.ReadToEnd();
        //    }
        //    RespondWeather FinResp = JsonConvert.DeserializeObject<RespondWeather>(res_str);
        //    WeatherAll.Clear();
        //    WeatherAll.Add(FinResp.current);
        //    for (int i = 0; i < FinResp.daily.Length; i++)
        //    {
        //        WeatherAll.Add(new WeatherDay(FinResp.daily[i]));
        //    }
        //    day = 0;
        //    DrawWeather();
        //}

    }
    //Label
   //StackLayout
}
