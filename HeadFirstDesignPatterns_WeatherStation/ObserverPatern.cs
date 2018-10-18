using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadFirstDesignPatterns_WeatherStation
{
    class ObserverPatern
    {
    }

    public interface ISubject
    {
        // The next two methods take both an Observer as an arguement,; that is, the Observer to be registered or to removed
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        // This method is called to notify all observers when the Subject's state has changed
        void NotifyObservers();
    }

    // The Observer interface is implemented by all observers, so they all have to implement the update() method.
    // Here we´re following Mary and Sue´s lead and passing the measurements to the observers
    public interface IObserver
    {
        void Update(float temp, float humidity, float preassure);
    }

    // The DisplayElement interface just incluedes one method, display(), that we will call wenn the display element needs to be displayed
    public interface IDisplayElement
    {
        void Display();
    }
}
