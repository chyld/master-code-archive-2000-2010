using System;

namespace VikingOne.Common
{
    public class PascalErrorToken : Token
    {
        public PascalErrorToken(Source source) : base(source) { }
        public override void DefineAutomaton()
        {
            p_States.Add(new State<CharacterTelemetry>(p_Source, StateType.TERMINAL));
        }

        public override void Finish()
        {
            Type = PascalToken.ERROR;
        }
    }
}
