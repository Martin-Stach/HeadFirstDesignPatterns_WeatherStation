using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class HeatIndexDisplay : IObserver, IDisplayElement
    {
        private float _heatIndex = 0;
        private WeatherData _weatherData;

        public HeatIndexDisplay(WeatherData weatherData)
        {
            this._weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temp, float humidity, float pressure)
        {
            _heatIndex = ComputeHeatIndex(temp, humidity);

            Display();
        }

        private float ComputeHeatIndex(float t, float rh)
        {
            //         float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh) 
            //+ (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) 
            //+ (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
            //(0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 * 
            //(rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) + 
            //(0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +
            //0.000000000843296 * (t * t * rh * rh * rh)) -
            //(0.0000000000481975 * (t * t * t * rh * rh * rh)));

            double c1 = -8.784695;
            double c2 = 1.61139411;
            double c3 = 2.338549;
            double c4 = -0.14611605;
            double c5 = -1.2308094 * Math.Pow(10, -2);
            double c6 = -1.6424828 * Math.Pow(10, -2);
            double c7 = 2.211732 * Math.Pow(10, -3);
            double c8 = 7.2546 * Math.Pow(10, -4);
            double c9 = -3.582 * Math.Pow(10, -6);

            // Fahrenheit???
            //double c1 = -42.379;
            //double c2 = 2.04901523;
            //double c3 = 10.14333127;
            //double c4 = -0.22475541;
            //double c5 = -6.83783 * Math.Pow(10, -3);
            //double c6 = -5.481717 * Math.Pow(10, -2);
            //double c7 = 1.22874 * Math.Pow(10, -3);
            //double c8 = 8.5282 * Math.Pow(10, -4);
            //double c9 = -1.99 * Math.Pow(10, -6);


            double index =
                c1 +
                c2 * t +
                c3 * rh +
                c4 * t * rh +
                c5 * Math.Pow(t, 2) +
                c6 * Math.Pow(rh, 2) +
                c7 * Math.Pow(t, 2) * rh +
                c8 * t * Math.Pow(rh, 2) +
                c9 * Math.Pow(t, 2) * Math.Pow(rh, 2);

		return (float)index;
        }

        public void Display()
        {
            Console.WriteLine($"Heat index is {_heatIndex}");
        }

    }
}
