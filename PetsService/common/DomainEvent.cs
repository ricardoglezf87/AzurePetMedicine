using System;
using System.Collections.Generic;

namespace PetsService.Common
{
    public class DomainEvent<T> 
    {
        private List<Action<T>> Actions = new List<Action<T>>();

        public void Register(Action<T> callback)
        {
            Actions.Add(callback);
        }

        public void Publish(T arg)
        {
            foreach (var action in Actions)
            {
                action(arg);
            }
        }
    }
}