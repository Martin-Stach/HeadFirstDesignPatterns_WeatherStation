using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class StatisticsDisplay : IObserver, IDisplayElement
    {
        private float _maxTemp = 0;
        private float _minTemp = 100;
        private float _tempSum = 0;
        private int _numReadings;
        private WeatherData _weatherData;

        public StatisticsDisplay(WeatherData weatherData)
        {
            this._weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            _tempSum += temp;
            _numReadings++;

            if(temp > _maxTemp)
            {
                _maxTemp = temp;
            }

            if(temp < _minTemp)
            {
                _minTemp = temp;
            }

            Display();
        }

        public void Display()
        {
            Console.WriteLine($"Avg/Max/Min temperature = {_tempSum/_numReadings} / {_maxTemp} / {_minTemp}");
        }

    }
}
