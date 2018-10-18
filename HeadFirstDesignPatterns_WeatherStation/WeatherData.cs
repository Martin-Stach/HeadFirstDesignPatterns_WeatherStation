using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Xml;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class WeatherData : ISubject
    {
        private List<IObserver> _observers;
        private float _temperature;
        private float _humidity;
        private float _pressure;
        private float _windspeed;
        private float _windChillIndex;
        private float _heatIndex;
        private string _city;

        public float Temperature { get => _temperature; set => _temperature = value; }
        public float Humidity { get => _humidity; set => _humidity = value; }
        public float Pressure { get => _pressure; set => _pressure = value; }
        public float Windspeed { get => _windspeed; set => _windspeed = value; }
        public float WindChillIndex { get => _windChillIndex; set => _windChillIndex = value; }
        public float HeatIndex { get => _heatIndex; set => _heatIndex = value; }
        public string City { get => _city; set => _city = value; }

        // Constructor with parameter
        public WeatherData(string city)
        {
            _city = city;
            _observers = new List<IObserver>();
        }

        public WeatherData()
        {
            _observers = new List<IObserver>();
        }

        // Get WeatherData
        public void CheckWeather()
        {
            WeatherAPI DataAPI = new WeatherAPI(_city);
            _temperature = DataAPI.GetTemp();
            _humidity = DataAPI.GetHumidiy();
            _pressure = DataAPI.GetPressure();
            _windspeed = DataAPI.GetWindspeed();
            _windChillIndex = DataAPI.GetWindChillIndex(_temperature, _windspeed);
            _heatIndex = DataAPI.GetHeatIndex(_temperature, _humidity, _windspeed);
        }

        public void RegisterObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            int i = _observers.IndexOf(o);
            if(i >= 0)
            {
                _observers.RemoveAt(i); // In C# possible with Remove(element)?
            }
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
            }
        }

        public void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            this._temperature = temperature;
            this._humidity = humidity;
            this._pressure = pressure;
            MeasurementsChanged();
        }
    }
}
