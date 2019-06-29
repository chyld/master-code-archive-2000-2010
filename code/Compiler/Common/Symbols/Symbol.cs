using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class Symbol
    {
        private String m_Name;
        private SymbolTable m_Table;

        public String Name { get { return m_Name; } }
        public SymbolTable Table { get { return m_Table; } }
        public List<Area> Areas { get; private set; }

        public Symbol(String name, SymbolTable table)
        {
            m_Name = name;
            m_Table = table;
            Areas = new List<Area>();
        }

        public void AddArea(Coordinate beginPosition, Coordinate endPosition)
        {
            Areas.Add(new Area { BeginPosition = beginPosition, EndPosition = endPosition });
        }
    }
}
