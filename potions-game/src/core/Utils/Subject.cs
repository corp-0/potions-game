using System;
using System.Collections.Generic;

namespace PotionsGame.Core.Utils
{
    /// <summary>
    /// Sort of implementation of Typescript's Subject T class.
    /// </summary>
    /// <typeparam name="T">Type we're observing</typeparam>
    public class Subject<T>: IDisposable
    {
        private T value;
        private readonly List<Action<T>> observers = new();
        
        public Subject(T defaultValue)
        {
            value = defaultValue;
        }
        
        public T Value
        {
            get => value;
            set
            {

                this.value = value;
                observers.ForEach(observer => observer(value));
            }
        }
        
        public void Subscribe(Action<T> observer)
        {
            observers.Add(observer);
        }
        
        public void Dispose()
        {
            observers.Clear();
        }
    }
}