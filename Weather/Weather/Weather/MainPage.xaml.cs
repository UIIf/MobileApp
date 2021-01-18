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
        public string icon { get; set; }
    }
    public partial class MainPage : ContentPage
    { 

        int day = 0;
        string selected_city = "";
        bool flag = true;

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
            DrawWeather();
        }

        private void DrawWeather()
        {
            Inner.Children.Clear();
            if (flag)
            {
                if (WeatherAll.Count > 0 && day > -1 && day < WeatherAll.Count)
                {
                    //date_label.Text = (DateTime.Now.AddDays(day)).ToString("M");
                    //weather_label.Text = WeatherAll[day].weather[0].description;
                    //temperature_label.Text = "Temperature: = " + WeatherAll[day].temp.ToString() + " C°";
                    //feels_temperature_label.Text = "But it feels like: " + WeatherAll[day].feels_like.ToString() + " C°";
                    //wind_speed_label.Text = "Wind speed: " + WeatherAll[day].wind_speed.ToString() + " m/s";
                    //preasure_label.Text = "Air pressure: " + WeatherAll[day].pressure.ToString();
                    Label lbl = new Label();
                    lbl.HorizontalTextAlignment = TextAlignment.Center;
                    lbl.Padding = 5;
                    lbl.FontSize = 30;
                    lbl.Text = (DateTime.Now.AddDays(day)).ToString("M");
                    Inner.Children.Add(lbl);
                    Image img = new Image();
                    img.BackgroundColor = Color.FromHex("#ccc");
                    img.HorizontalOptions = LayoutOptions.Center;
                    img.Source = ImageSource.FromResource("Weather.Icons." + WeatherAll[day].weather[0].icon + ".png");
                    img.WidthRequest = 60;
                    img.HeightRequest = 60;
                    Inner.Children.Add(img);
                    Label weather = new Label();
                    weather.HorizontalTextAlignment = TextAlignment.Center;
                    weather.Padding = 5;
                    weather.FontSize = 40;
                    weather.Text = WeatherAll[day].weather[0].description;
                    Inner.Children.Add(weather);
                    StackLayout TemperatureStck = new StackLayout();
                    TemperatureStck.Padding = 20;
                    Label temper = new Label();
                    temper.HorizontalTextAlignment = TextAlignment.Center;
                    temper.FontSize = 25;
                    temper.Text = "Temperature: = " + WeatherAll[day].temp.ToString() + " C°";
                    TemperatureStck.Children.Add(temper);
                    Label feel_like = new Label();
                    feel_like.HorizontalTextAlignment = TextAlignment.Center;
                    feel_like.FontSize = 25;
                    feel_like.Text = "But it feels like: " + WeatherAll[day].feels_like.ToString() + " C°";
                    TemperatureStck.Children.Add(feel_like);
                    Inner.Children.Add(TemperatureStck);
                    StackLayout OtherStck = new StackLayout();
                    OtherStck.VerticalOptions = LayoutOptions.End;
                    Label windSpeed = new Label();
                    windSpeed.HorizontalTextAlignment = TextAlignment.Center;
                    windSpeed.FontSize = 15;
                    windSpeed.Text = "Wind speed: " + WeatherAll[day].wind_speed.ToString() + " m/s";
                    OtherStck.Children.Add(windSpeed);
                    Label presur = new Label();
                    presur.HorizontalTextAlignment = TextAlignment.Center;
                    presur.FontSize = 15;
                    presur.Text = "Air pressure: " + WeatherAll[day].pressure.ToString();
                    OtherStck.Children.Add(presur);
                    Inner.Children.Add(OtherStck);
                    //Stepper stp = new Stepper();
                    //stp.ValueChanged += stepper_ValueChanged;
                    //stp.Value = day;
                    //stp.VerticalOptions = LayoutOptions.End;
                    //stp.HorizontalOptions = LayoutOptions.Center;
                    //Inner.Children.Add(stp);
                }
                else
                {

                    Label lbl = new Label();
                    lbl.HorizontalTextAlignment = TextAlignment.Center;
                    lbl.Padding = 10;
                    lbl.FontSize = 60;
                    lbl.Text = "City not choosen";
                    Inner.Children.Add(lbl);
                }
            }
            else
            {
                if (WeatherAll.Count > 0 && day > -1 && day < WeatherAll.Count)
                {
                    StackLayout Holedr = new StackLayout();
                    Holedr.Padding = 5;
                    Holedr.VerticalOptions = LayoutOptions.CenterAndExpand;
                    Holedr.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    for(int i = 0; i < WeatherAll.Count; i++)
                    {
                        StackLayout stk = new StackLayout();
                        stk.Orientation = StackOrientation.Horizontal;
                        //stk.HorizontalOptions = LayoutOptions.Center;
                        Image img = new Image();
                        img.BackgroundColor = Color.FromHex("#ccc");
                        img.Source = ImageSource.FromResource("Weather.Icons." + WeatherAll[i].weather[0].icon + ".png");
                        //img.HorizontalOptions = LayoutOptions.Center;
                        stk.Children.Add(img);
                        Label lbl = new Label();
                        //lbl.HorizontalTextAlignment = TextAlignment.Center;
                        lbl.FontSize = 17;
                        lbl.Text = (DateTime.Now.AddDays(i)).ToString("M");
                        lbl.Text += " tmp:" + ((int)WeatherAll[i].temp).ToString() + "C°";
                        lbl.Text += " wind speed:" + ((int)WeatherAll[i].wind_speed).ToString() + "m/s";
                        stk.Children.Add(lbl);
                        Holedr.Children.Add(stk);
                        //Inner.Children.Add(stk);
                    }
                    Inner.Children.Add(Holedr);
                }
                else
                {

                    Label lbl = new Label();
                    lbl.HorizontalTextAlignment = TextAlignment.Center;
                    lbl.Padding = 10;
                    lbl.FontSize = 60;
                    lbl.Text = "City not choosen";
                    Inner.Children.Add(lbl);
                }
            }
        }

        List<WeatherDay> WeatherAll = new List<WeatherDay>() { };
        public MainPage()
        {
            InitializeComponent();
            DrawWeather();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (TextEditor.Text != "" || selected_city != TextEditor.Text)
            {
                selected_city = TextEditor.Text;
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
                DrawWeather();
            }
        }

        private void ChangeDraw(object sender, EventArgs e)
        {
            if (flag)
            {
                Btn.Text = "Day";
                StepperAround.IsVisible = false;
            }
            else
            {
                Btn.Text = "Week";
                StepperAround.IsVisible = true;
            }
            flag = !flag;
            DrawWeather();
        }
    }
}
