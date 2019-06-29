using System;

namespace VikingOne.Common
{
    class PascalCommentToken : Token
    {
        public PascalCommentToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.OPEN));
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => ct.Character != Utility.EndComment, TransitionState = p_States[0] });
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => ct.Character == Utility.EndComment, TransitionState = p_States[1] });
        }

        public override void Finish()
        {
            Type = p_Automaton.FinalState == StateType.TERMINAL ? PascalToken.COMMENT : PascalToken.ERROR;
        }
    }
}
