using System;

namespace VikingOne.Common
{
    public class RouteDefinition<T>
    {
        public String Name { get; set; }
        public Func<T, Boolean> Predicate { get; set; }
        public State<T> TransitionState { get; set; }
    }
}
