using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Xml;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class WeatherAPI
    {
        private const string APIKEY = "dbd1cddcef0dcee548cdb905d2cbd264";
        private string _currentURL;
        private XmlDocument _xmlDocument;

        public WeatherAPI(string city)
        {
            SetCurrentURL(city);
            _xmlDocument = GetXml(_currentURL);
        }

        public float GetTemp()
        {
            XmlNode temperatur_node = _xmlDocument.SelectSingleNode("//temperature");
            XmlAttribute temperature_value = temperatur_node.Attributes["value"];
            string temperature_string = temperature_value.Value;

            return float.Parse(temperature_string, System.Globalization.CultureInfo.InvariantCulture);
        }

        public float GetHumidiy()
        {
            XmlNode humidity_node = _xmlDocument.SelectSingleNode("//humidity");
            XmlAttribute humidity_value = humidity_node.Attributes["value"];
            string humidity_string = humidity_value.Value;

            return float.Parse(humidity_string, System.Globalization.CultureInfo.InvariantCulture);
        }

        public float GetPressure()
        {
            XmlNode pressure_node = _xmlDocument.SelectSingleNode("//pressure");
            XmlAttribute pressure_value = pressure_node.Attributes["value"];
            string pressure_string = pressure_value.Value;

            return float.Parse(pressure_string, System.Globalization.CultureInfo.InvariantCulture);
        }

        public float GetWindspeed()
        {
            XmlNode windspeed_node = _xmlDocument.SelectSingleNode("//speed");
            XmlAttribute windspeed_value = windspeed_node.Attributes["value"];
            string windspeed_string = windspeed_value.Value;

            return float.Parse(windspeed_string, System.Globalization.CultureInfo.InvariantCulture);
        }

        public float GetHeatIndex(float t, float rh, float wspd)
        { 
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



            //var windchill = 13.12 + 0.6215 * temperatur - 11.37 * Math.pow(windspeed, 0.16) + 0.3965 * temperatur * Math.pow(windspeed, 0.16);
            //double index = 13.12 + 0.6215 * t - 11.37 * Math.Pow(wspd, 0.16) + 0.3965 * t * Math.Pow(wspd, 0.16);

            return (float)index;
        }

        public float GetWindChillIndex(float temperature, float windspeed)
        {
            return (float)(13.12 + 0.6215 * temperature - 11.37 * Math.Pow(windspeed, 0.16) + 0.3965 * temperature * Math.Pow(windspeed, 0.16));
        }

        private void SetCurrentURL(string location)
        {
            //_currentURL = "http://api.openweathermap.org/data/2.5/forecast?id=524901&APPID={APIKEY}"
            //_currentURL = "http://api.openweathermap.org/data/2.5/weather?q=" +
            //              location + "&mode=xml&units=metric&APPID=" +
            //              APIKEY;
            _currentURL = 
                $"http://api.openweathermap.org/data/2.5/weather?q={location}" +
                $"&mode=xml&units=metric&APPID={APIKEY}";


            //http://api.openweathermap.org/data/2.5/weather?q=Hamburg,DE&mode=xml&units=metric&APPID=dbd1cddcef0dcee548cdb905d2cbd264
        }

        private XmlDocument GetXml(string currentURL)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(currentURL);
                
                XmlDocument _xmlDokument = new XmlDocument();
                _xmlDokument.LoadXml(xmlContent);
                return _xmlDokument;
            }
        }
    }
}
