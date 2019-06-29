using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class SymbolStack
    {
        private Int32 m_CurrentNestingLevel;
        private List<SymbolTable> m_Tables;

        public Int32 CurrentNestingLevel { get { return m_CurrentNestingLevel; } }

        public SymbolStack()
        {
            m_CurrentNestingLevel = 0;
            m_Tables = new List<SymbolTable> { new SymbolTable(m_CurrentNestingLevel) };
        }

        public SymbolTable GetLocalTable()
        {
            return m_Tables[m_CurrentNestingLevel];
        }

        public Symbol LookupLocalSymbol(String name)
        {
            return m_Tables[m_CurrentNestingLevel].Lookup(name);
        }

        public Symbol LookupGlobalSymbol(String name)
        {
            return m_Tables[m_CurrentNestingLevel].Lookup(name);
        }

        public Symbol InsertLocalSymbol(String name)
        {
            return m_Tables[m_CurrentNestingLevel].Insert(name);
        }
    }
}
