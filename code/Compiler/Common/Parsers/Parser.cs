using System;
using System.Linq;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class Parser
    {
        private Scanner m_Scanner;
        private IEnumerator<Token> m_Tokens;
        private SymbolStack m_Stack;
        private Ast m_Tree;

        public Parser(Scanner scanner)
        {
            m_Scanner = scanner;
            m_Tokens = m_Scanner.Tokens.GetEnumerator();
            m_Stack = new SymbolStack();
            m_Tree = new Ast(m_Tokens, m_Stack);
        }

        public void Parse()
        {
            m_Tree.Create();
        }
    }
}
