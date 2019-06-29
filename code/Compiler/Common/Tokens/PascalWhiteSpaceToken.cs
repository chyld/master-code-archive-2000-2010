using System;

namespace VikingOne.Common
{
    public class PascalWhiteSpaceToken : Token
    {
        public PascalWhiteSpaceToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsWhiteSpace(ct.Character), TransitionState = p_States[0] });
        }

        public override void Finish()
        {
            Type = PascalToken.WHITESPACE;
        }
    }
}
