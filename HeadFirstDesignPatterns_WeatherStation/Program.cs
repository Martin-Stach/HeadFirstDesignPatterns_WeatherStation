using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class Program
    {
        static void Main(string[] args)
        {
            string location = "Hamburg,DE";
            WeatherData HamburgWeather = new WeatherData(location);
            HamburgWeather.CheckWeather();
            System.Console.WriteLine($"Current Weather in {location}\n" + 
                                     $"Temperature {HamburgWeather.Temperature} °C\n" +
                                     $"Humidity    {HamburgWeather.Humidity} %\n" + 
                                     $"Pressure    {HamburgWeather.Pressure} hPa\n" + 
                                     $"Windspeed   {HamburgWeather.Windspeed} m/s\n" + 
                                     $"WindChill   {HamburgWeather.WindChillIndex} °C\n" + 
                                     $"HeatIndex   {HamburgWeather.HeatIndex} °C");  // TODO: here is something wrong - not right value

            Console.WriteLine("-----------------------");

            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
            HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);

            weatherData.SetMeasurements(18, 65, 1012);
            Console.WriteLine("-----------------------");
            weatherData.SetMeasurements(20, 70, 995);
            Console.WriteLine("-----------------------");
            weatherData.SetMeasurements(16, 90, 995);
            Console.WriteLine("--------END------------");

            Console.ReadKey();
        }
    }
}
