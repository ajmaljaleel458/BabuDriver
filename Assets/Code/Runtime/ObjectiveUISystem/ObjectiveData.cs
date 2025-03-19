using System;

namespace BabuDriver.ObjectiveUISystem
{
    public class ObjectiveData
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        public ObjectiveData(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}