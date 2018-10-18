using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class ForecastDisplay : IObserver, IDisplayElement
    {
        private float _currentPressure = 1000;
        private float _lastPressure;
        private WeatherData _weatherData;

        public ForecastDisplay(WeatherData weatherData)
        {
            this._weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            _lastPressure = _currentPressure;
            _currentPressure = pressure;

            Display();
        }

        public void Display()
        {
            Console.Write("Forecast: ");
            if(_currentPressure > _lastPressure)
            {
                Console.WriteLine("Improving weather on the way!");
            }
            else if(_currentPressure == _lastPressure)
            {
                Console.WriteLine("more of the same");
            }
            else
            {
                Console.WriteLine("Watch out for cooler, rainy weather");
            }
        }

    }
}
