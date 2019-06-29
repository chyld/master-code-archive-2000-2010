using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class AutomatonResult<T>
    {
        public String StateName { get; set; }
        public StateType FinalState { get; set; }
        public List<T> Output { get; set; }

        public AutomatonResult()
        {
            Output = new List<T>();
        }
    }
}
