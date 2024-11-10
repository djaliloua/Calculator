using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RegistrationApplication.CustomProperties
{
    public class Yam
    {
        public string Name { get; set; }
        public Yam(string name)
        {
            Name = name;
        }
        public Yam()
        {
            
        }
    }

    public static class DataSource
    {
        public static EventsCollection Events = new EventsCollection();
        public static Yam Yam = new Yam("Hello world");
        
        static DataSource()
        {
            Events.Add(new Event("Loaded"));
            Events.Add(new Event("ggg"));
        }
    }

    public static class EventToCommand
    {
        public static void SetAppName(DependencyObject d, Yam value)
        {
            d.SetValue(AppNameProperty, value);
        }
        public static Yam GetAppName(DependencyObject d) => (Yam)d.GetValue(AppNameProperty);
        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppNameProperty =
            DependencyProperty.RegisterAttached("AppName",
                typeof(Yam),
                typeof(EventToCommand),
                new PropertyMetadata(new Yam("Maman mia")));

        public static void SetWIndowsEvents(DependencyObject element, EventsCollection value)
        {
            element.SetValue(WIndowsEventsProperty, value);
        }
        public static EventsCollection GetWIndowsEvents(DependencyObject element) => (EventsCollection)element.GetValue(WIndowsEventsProperty);


        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WIndowsEventsProperty =
            DependencyProperty.RegisterAttached("WIndowsEvents", 
                typeof(EventsCollection), 
                typeof(EventToCommand), 
                new PropertyMetadata(new EventsCollection()));


    }
    public class Event
    {
        public string EventName { get; set; }
        public ICommand Command { get; set; }
        public Event(string name)
        {
            EventName = name;
        }
        public Event()
        {
            
        }
    }
    public class EventsCollection:ObservableCollection<Event>
    {
        public EventsCollection()
        {
            
        }
    }
}
