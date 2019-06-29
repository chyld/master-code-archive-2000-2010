using System;
using System.Collections.Generic;
using System.Linq;

namespace VikingOne.Common
{
    public class SymbolTable
    {
        private Int32 m_NestingLevel;

        public Int32 NestingLevel { get { return m_NestingLevel; } }
        public Dictionary<String, Symbol> Symbols { get; private set; }

        public SymbolTable(Int32 nestingLevel)
        {
            m_NestingLevel = nestingLevel;
            Symbols = new Dictionary<String, Symbol>();
        }

        public Symbol Insert(String name)
        {
            return Symbols[name] = new Symbol(name, this);
        }

        public Symbol Lookup(String name)
        {
            if (Symbols.Keys.Any(k => k == name.ToLower()))
                return Symbols[name];

            return null;
        }
    }
}
