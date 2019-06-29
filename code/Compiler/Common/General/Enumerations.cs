using System;

namespace VikingOne.Common
{
    public enum WatchState
    {
        START,
        STOP
    }

    public enum StateType
    {
        START,
        OPEN,
        TERMINAL,
        ERROR
    }

    public enum PascalToken
    {
        DEFAULT,

        AND, ARRAY, BEGIN, CASE, CONST, DIV, DO, DOWNTO, ELSE, END,
        FILE, FOR, FUNCTION, GOTO, IF, IN, LABEL, MOD, NIL, NOT,
        OF, OR, PACKED, PROCEDURE, PROGRAM, RECORD, REPEAT, SET,
        THEN, TO, TYPE, UNTIL, VAR, WHILE, WITH,

        PLUS, MINUS, STAR, FORESLASH, COLON_EQUALS,
        DOT, COMMA, SEMICOLON, COLON, BACKSLASH,
        EQUALS, NOT_EQUALS, LESS_THAN, LESS_EQUALS,
        GREATER_EQUALS, GREATER_THAN, LEFT_PAREN, RIGHT_PAREN,
        LEFT_BRACKET, RIGHT_BRACKET, UP_ARROW, DOT_DOT,

        IDENTIFIER, INTEGER, REAL, STRING,
        ERROR, WHITESPACE, COMMENT
    }

    public enum AstNodeType
    {
        PROGRAM, PROCEDURE, FUNCTION,

        COMPOUND, ASSIGN, LOOP, TEST, CALL, PARAMETERS,
        IF, SELECT, SELECT_BRANCH, SELECT_CONSTANTS, NO_OP,

        EQ, NE, LT, LE, GT, GE, NOT,

        ADD, SUBTRACT, OR, NEGATE,

        MULTIPLY, INTEGER_DIVIDE, FLOAT_DIVIDE, MOD, AND,

        VARIABLE, SUBSCRIPTS, FIELD,
        INTEGER_CONSTANT, REAL_CONSTANT,
        STRING_CONSTANT, BOOLEAN_CONSTANT,

        WRITE_PARM
    }

    public enum AstNodeSyntax
    {
        COMPOUND_STATEMENT,
        ASSIGNMENT_STATEMENT,
        EXPRESSION,
        SIMPLE_EXPRESSION,
        TERM,
        FACTOR
    }

    public enum AstNodeKey
    {
        LINE,
        ID,
        VALUE
    }

    public enum MessageType
    {
        INFO,
        WARN,
        ERROR
    }
}
