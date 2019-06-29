using System;
using System.Collections.Generic;
using System.Linq;

namespace VikingOne.Common
{
    public class Scanner
    {
        private Source m_Source;
        private Token m_Token;

        public Token CurrentToken { get { return m_Token; } }

        public IEnumerable<Token> Tokens
        {
            get
            {
                while (m_Source.Current != null)
                {
                    if (Char.IsWhiteSpace(m_Source.Current.Character))
                    {
                        new PascalWhiteSpaceToken(m_Source);
                        continue;
                    }

                    if (Char.IsLetter(m_Source.Current.Character))
                        m_Token = new PascalWordToken(m_Source);
                    else if (Char.IsDigit(m_Source.Current.Character))
                        m_Token = new PascalNumberToken(m_Source);
                    else if (m_Source.Current.Character == Utility.BeginComment)
                        m_Token = new PascalCommentToken(m_Source);
                    else if (m_Source.Current.Character == Utility.SingleQuote)
                        m_Token = new PascalStringToken(m_Source);
                    else if (Utility.ReservedSymbols.Any(d => d.Key == m_Source.Current.Character.ToString()))
                        m_Token = new PascalSpecialSymbolToken(m_Source);
                    else
                        m_Token = new PascalErrorToken(m_Source);

                    yield return m_Token;
                }
            }
        }

        public Scanner(Source source)
        {
            m_Source = source;
        }
    }
}
