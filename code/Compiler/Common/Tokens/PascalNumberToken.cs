using System;
using System.Linq;

namespace VikingOne.Common
{
    class PascalNumberToken : Token
    {
        public PascalNumberToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL, "Integer"));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.OPEN, "Point"));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL, "Real"));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.OPEN, "E"));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.OPEN, "Sign"));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL, "Exponent"));
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[0] });
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => ct.Character == Utility.Point && (p_Source.View(1) != null && p_Source.View(1).Character != Utility.Point), TransitionState = p_States[1] });
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Utility.Exponent.Any(c => c == ct.Character), TransitionState = p_States[3] });
            p_States[1].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[2] });
            p_States[2].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[2] });
            p_States[2].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Utility.Exponent.Any(c => c == ct.Character), TransitionState = p_States[3] });
            p_States[3].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Utility.Signs.Any(c => c == ct.Character), TransitionState = p_States[4] });
            p_States[3].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[5] });
            p_States[4].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[5] });
            p_States[5].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsDigit(ct.Character), TransitionState = p_States[5] });
        }

        public override void Finish()
        {
            if (p_Automaton.FinalState == StateType.OPEN)
                Type = PascalToken.ERROR;

            if (p_Automaton.FinalState == StateType.TERMINAL && p_Automaton.StateName == "Integer")
            {
                Int64 value;
                Type = PascalToken.INTEGER;
                if (!Int64.TryParse(Text, out value))
                    Type = PascalToken.ERROR;
                Value = value;
            }

            if (p_Automaton.FinalState == StateType.TERMINAL && p_Automaton.StateName != "Integer")
            {
                Double value;
                Type = PascalToken.REAL;
                if (!Double.TryParse(Text, out value))
                    Type = PascalToken.ERROR;
                Value = value;
            }

        }
    }
}
