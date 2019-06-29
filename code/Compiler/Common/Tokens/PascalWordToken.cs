using System;
using System.Linq;

namespace VikingOne.Common
{
    public class PascalWordToken : Token
    {
        public PascalWordToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
            p_States[0].Routes.Add(new RouteDefinition<CharacterTelemetry> { Predicate = ct => Char.IsLetterOrDigit(ct.Character), TransitionState = p_States[0] });
        }

        public override void Finish()
        {
            var reserved_word = Utility.ReservedWords.Where(t => t.ToString().ToUpper() == Text.ToUpper()).SingleOrDefault();
            Type = (reserved_word == PascalToken.DEFAULT) ? PascalToken.IDENTIFIER : reserved_word;
        }
    }
}
