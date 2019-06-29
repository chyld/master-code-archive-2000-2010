using System;
using System.Linq;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class Ast
    {
        private IEnumerator<Token> m_Tokens;
        private SymbolStack m_Stack;
        private Dictionary<AstNodeSyntax, Func<AstNode, AstNode>> m_Rules;

        public AstNode Root { get; set; }

        public Ast(IEnumerator<Token> tokens, SymbolStack stack)
        {
            Root = new AstNode(AstNodeType.PROGRAM);
            m_Tokens = tokens;
            m_Stack = stack;
            m_Rules = new Dictionary<AstNodeSyntax, Func<AstNode, AstNode>>();
            InitializeNodes();
        }

        public void Create()
        {
            if (m_Tokens.MoveNext() && m_Tokens.Current.Type == PascalToken.BEGIN)
                m_Rules[AstNodeSyntax.COMPOUND_STATEMENT](Root);
        }

        private void InitializeNodes()
        {
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            m_Rules[AstNodeSyntax.COMPOUND_STATEMENT] = parent =>
            {
                while (m_Tokens.Current.Type == PascalToken.BEGIN)
                {
                    var compound = parent.AddChild(new AstNode(AstNodeType.COMPOUND));
                    m_Tokens.MoveNext();

                    if (m_Tokens.Current.Type == PascalToken.BEGIN)
                        m_Rules[AstNodeSyntax.COMPOUND_STATEMENT](compound);

                    while (m_Tokens.Current.Type == PascalToken.IDENTIFIER)
                        m_Rules[AstNodeSyntax.ASSIGNMENT_STATEMENT](compound);

                    MoveNextIf(PascalToken.END);
                }

                return null;
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            m_Rules[AstNodeSyntax.ASSIGNMENT_STATEMENT] = parent =>
            {
                var assign = parent.AddChild(new AstNode(AstNodeType.ASSIGN));

                var variable = new AstNode(AstNodeType.VARIABLE);
                variable.Attributes.Add(AstNodeKey.ID, CreateSymbol());
                assign.AddChild(variable);
                m_Tokens.MoveNext();

                MoveNextIf(PascalToken.COLON_EQUALS);

                m_Rules[AstNodeSyntax.EXPRESSION](assign);

                return null;
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //            
            m_Rules[AstNodeSyntax.EXPRESSION] = parent =>
            {
                m_Rules[AstNodeSyntax.SIMPLE_EXPRESSION](parent);

                return null;
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //            
            m_Rules[AstNodeSyntax.SIMPLE_EXPRESSION] = parent =>
            {
                MoveNextIf(PascalToken.PLUS);
                var term = m_Rules[AstNodeSyntax.TERM](parent);

                if ((new List<PascalToken> { PascalToken.PLUS, PascalToken.MINUS, PascalToken.OR }).Any(t => t == m_Tokens.Current.Type))
                {
                    var binary = (m_Tokens.Current.Type == PascalToken.OR) ? new AstNode(AstNodeType.OR) : new AstNode(AstNodeType.ADD);
                    m_Tokens.MoveNext();

                    if (parent.NodeType == AstNodeType.ADD || parent.NodeType == AstNodeType.OR)
                    {
                        binary.AddChild(parent);
                        parent.Parent.AddChild(binary);
                        parent.Parent.Children.Remove(parent);
                        parent.Parent = null;
                    }
                    else
                    {
                        binary.AddChild(term);
                        parent.AddChild(binary);
                        parent.Children.Remove(term);
                        term.Parent = null;
                    }

                    m_Rules[AstNodeSyntax.SIMPLE_EXPRESSION](binary);
                }

                return null;
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //            
            m_Rules[AstNodeSyntax.TERM] = parent =>
            {
                return m_Rules[AstNodeSyntax.FACTOR](parent);
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            m_Rules[AstNodeSyntax.FACTOR] = parent =>
            {
                switch (m_Tokens.Current.Type)
                {
                    case PascalToken.IDENTIFIER:
                        var variable = new AstNode(AstNodeType.VARIABLE);
                        variable.Attributes.Add(AstNodeKey.ID, CreateSymbol());
                        parent.AddChild(variable);
                        m_Tokens.MoveNext();
                        return variable;
                    case PascalToken.INTEGER:
                        var integer = new AstNode(AstNodeType.INTEGER_CONSTANT);
                        integer.Attributes.Add(AstNodeKey.VALUE, m_Tokens.Current.Value);
                        parent.AddChild(integer);
                        m_Tokens.MoveNext();
                        return integer;
                    case PascalToken.REAL:
                        var real = new AstNode(AstNodeType.REAL_CONSTANT);
                        real.Attributes.Add(AstNodeKey.VALUE, m_Tokens.Current.Value);
                        parent.AddChild(real);
                        m_Tokens.MoveNext();
                        return real;
                    case PascalToken.STRING:
                        var str = new AstNode(AstNodeType.STRING_CONSTANT);
                        str.Attributes.Add(AstNodeKey.VALUE, m_Tokens.Current.Text);
                        parent.AddChild(str);
                        m_Tokens.MoveNext();
                        return str;
                    case PascalToken.MINUS:
                        var negate = new AstNode(AstNodeType.NEGATE);
                        parent.AddChild(negate);
                        m_Tokens.MoveNext();
                        m_Rules[AstNodeSyntax.FACTOR](negate);
                        return negate;
                    case PascalToken.NOT:
                        var not = new AstNode(AstNodeType.NOT);
                        parent.AddChild(not);
                        m_Tokens.MoveNext();
                        m_Rules[AstNodeSyntax.FACTOR](not);
                        return not;
                    case PascalToken.LEFT_PAREN:
                        return null;
                    default:
                        return null;
                }
            };
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
            // ----------------------------------------------------------------------------------------------------------------------------------- //
        }

        private Symbol CreateSymbol()
        {
            var name = m_Tokens.Current.Text.ToLower();
            var symbol = m_Stack.LookupGlobalSymbol(name) ?? m_Stack.InsertLocalSymbol(name);
            symbol.AddArea(m_Tokens.Current.BeginPosition, m_Tokens.Current.EndPosition);
            return symbol;
        }

        private void MoveNextIf(PascalToken tokenType)
        {
            if (m_Tokens.Current.Type == tokenType)
                m_Tokens.MoveNext();
        }
    }
}
