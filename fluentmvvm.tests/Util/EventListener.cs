using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace FluentMvvm.Tests.Util
{
    internal sealed class EventListener<T>
    {
        public IList<T> Received { get; } = new List<T>();

        private EventListener()
        {
        }

        public static EventListener<string> Create(INotifyPropertyChanged obj)
        {
            EventListener<string> eventListener = new EventListener<string>();
            obj.PropertyChanged += (sender, args) => eventListener.Received.Add(args.PropertyName);

            return eventListener;
        }

        public static EventListener<EventArgs> Create(ICommand command)
        {
            EventListener<EventArgs> eventListener = new EventListener<EventArgs>();
            command.CanExecuteChanged += (sender, args) => eventListener.Received.Add(args);

            return eventListener;
        }
    }
}