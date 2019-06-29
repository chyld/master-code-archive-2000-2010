using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class Utility
    {
        public static List<PascalToken> ReservedWords;
        public static Dictionary<String, PascalToken> ReservedSymbols;
        public static Char WhiteSpace = ' ';
        public static Char BeginComment = '{';
        public static Char EndComment = '}';
        public static Char SingleQuote = '\'';
        public static Char NullChar = '\0';
        public static Char Point = '.';
        public static String Exponent = "eE";
        public static String Signs = "+-";
        public static String NewLine = "\r\n";

        public Utility()
        {
            ReservedWords = new List<PascalToken>();
            ReservedSymbols = new Dictionary<String, PascalToken>();

            for (Int32 i = (Int32)PascalToken.AND; i <= (Int32)PascalToken.WITH; i++)
            {
                ReservedWords.Add((PascalToken)i);
            }

            ReservedSymbols.Add(@"+", PascalToken.PLUS);
            ReservedSymbols.Add(@"-", PascalToken.MINUS);
            ReservedSymbols.Add(@"*", PascalToken.STAR);
            ReservedSymbols.Add(@"^", PascalToken.UP_ARROW);
            ReservedSymbols.Add(@"/", PascalToken.FORESLASH);
            ReservedSymbols.Add(@"\", PascalToken.BACKSLASH);

            ReservedSymbols.Add(@".", PascalToken.DOT);
            ReservedSymbols.Add(@"..", PascalToken.DOT_DOT);
            ReservedSymbols.Add(@",", PascalToken.COMMA);
            ReservedSymbols.Add(@";", PascalToken.SEMICOLON);
            ReservedSymbols.Add(@":", PascalToken.COLON);
            ReservedSymbols.Add(@":=", PascalToken.COLON_EQUALS);

            ReservedSymbols.Add(@"=", PascalToken.EQUALS);
            ReservedSymbols.Add(@"<", PascalToken.LESS_THAN);
            ReservedSymbols.Add(@"<=", PascalToken.LESS_EQUALS);
            ReservedSymbols.Add(@"<>", PascalToken.NOT_EQUALS);
            ReservedSymbols.Add(@">=", PascalToken.GREATER_EQUALS);
            ReservedSymbols.Add(@">", PascalToken.GREATER_THAN);

            ReservedSymbols.Add(@"(", PascalToken.LEFT_PAREN);
            ReservedSymbols.Add(@")", PascalToken.RIGHT_PAREN);
            ReservedSymbols.Add(@"[", PascalToken.LEFT_BRACKET);
            ReservedSymbols.Add(@"]", PascalToken.RIGHT_BRACKET);
        }
    }
}
