using System;

namespace GiveCRM.DummyDataGenerator
{
    public class EventArgs<T> : EventArgs
    {
        private readonly T eventData;

        public EventArgs(T data)
        {
            eventData = data;
        }

        public T Data
        {
            get { return eventData; }
        }
    }
}
