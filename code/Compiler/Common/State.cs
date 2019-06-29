using System;
using System.Collections.Generic;
using System.Linq;

namespace VikingOne.Common
{
    public class State<T>
    {
        private IEnumerator<T> m_Stream;
        private String m_Name;
        private StateType m_StateType;

        public String Name { get { return m_Name; } }
        public StateType StateType { get { return m_StateType; } }
        public List<RouteDefinition<T>> Routes { get; set; }

        public State(IEnumerator<T> stream, StateType stateType, String name = "")
        {
            m_Stream = stream;
            m_Name = name;
            m_StateType = stateType;
            Routes = new List<RouteDefinition<T>>();
        }

        public AutomatonResult<T> Go(AutomatonResult<T> automaton)
        {
            automaton.StateName = Name;
            automaton.Output.Add(m_Stream.Current);
            automaton.FinalState = StateType;

            if (MoveNextAndCheckRoute())
                automaton = NextRoute().TransitionState.Go(automaton);

            return automaton;
        }

        private Boolean MoveNextAndCheckRoute()
        {
            return (m_Stream.MoveNext()) && (NextRoute() != null);
        }

        private RouteDefinition<T> NextRoute()
        {
            return Routes.Where(r => r.Predicate(m_Stream.Current)).SingleOrDefault();
        }
    }
}
