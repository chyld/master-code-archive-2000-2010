using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public abstract class Token
    {
        protected Source p_Source;
        protected List<State<CharacterTelemetry>> p_States;
        protected AutomatonResult<CharacterTelemetry> p_Automaton;

        public PascalToken Type { get; set; }
        public String Text { get; set; }
        public dynamic Value { get; set; }
        public Coordinate BeginPosition { get; set; }
        public Coordinate EndPosition { get; set; }

        public Token(Source source)
        {
            p_Source = source;
            p_States = new List<State<CharacterTelemetry>>();
            p_Automaton = new AutomatonResult<CharacterTelemetry>();

            BeginPosition = new Coordinate { X = p_Source.Current.X, Y = p_Source.Current.Y };
            DefineAutomaton();
            p_Automaton = p_States[0].Go(p_Automaton);
            p_Automaton.Output.ForEach(t => Text += t.Character);
            Finish();
            EndPosition = new Coordinate { X = (p_Source.View(-1) != null) ? p_Source.View(-1).X : p_Source.View(0).X, Y = (p_Source.View(-1) != null) ? p_Source.View(-1).Y : p_Source.View(0).Y };
        }

        public abstract void DefineAutomaton();
        public abstract void Finish();
    }
}
