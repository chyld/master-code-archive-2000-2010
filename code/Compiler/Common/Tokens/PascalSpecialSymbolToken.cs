using System;
using System.Linq;

namespace VikingOne.Common
{
    class PascalSpecialSymbolToken : Token
    {
        public PascalSpecialSymbolToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => p_Automaton.Output[0].Character == '.' && p_Source.Current.Character == '.', TransitionState = p_States[1] });
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => ":<>".Any(c => c == p_Automaton.Output[0].Character) && p_Source.Current.Character == '=', TransitionState = p_States[1] });
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => p_Automaton.Output[0].Character == '<' && p_Source.Current.Character == '>', TransitionState = p_States[1] });
        }

        public override void Finish()
        {
            Type = Utility.ReservedSymbols[Text];
        }
    }
}
