using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1
{
    class DFA
    {
        private List<State> states;
        private List<Transition> transitions;
        private RichTextBox editor;
        List<Token> tokens;
        public List<string> comments;
        public List<Symbol> symbols;
        public List<Error> errors;

        public DFA(RichTextBox editor)
        { 
            this.editor = editor;
            states = new List<State>();
            transitions = new List<Transition>();
            tokens = new List<Token>();
            comments = new List<string>();
            symbols = new List<Symbol>();
            errors = new List<Error>();
        }

        public List<Token> GenericDFA() 
        {
            
            
            for (int line = 0; line < editor.Lines.Length; ++line )
            {
                if (editor.Lines[line].Length < 1)
                    continue;

                int charIndex = 0;
                Token tmpToken;

                State state = new State();
                state.isFinal = false;
                state.isStart = true;
                state.val = 0;

                State newState = new State();
                newState.isFinal = false;
                newState.val = 1;


                char ch = editor.Lines[line][charIndex];

                char first = ch;
                string tmpStr = "";
                while (charIndex < editor.Lines[line].Length)
                {
                    
                    while (!(state.isFinal || state.isError))
                    {
                        //Console.Write(charIndex);
                        if ((ch == ' ' || ch=='\n') && first!='{') 
                        {
                            state.isFinal = true;
                            ++charIndex;
                            ch = charIndex < editor.Lines[line].Length ? editor.Lines[line][charIndex] : ch;
                            first = ch;
                            continue;
                        }
                        if (isLetter(first))
                        {
                            if (isLetter(ch) || isDigit(ch))
                            {
                                Transition transition = new Transition();
                                transition.val = ch;
                                transition.currState += transition.currState;
                                transition.nextState += transition.nextState;

                                transitions.Add(transition);

                                newState.isFinal = false;
                            }
                            else
                            {
                                newState.isFinal = true;
                                first = ch;
                            }
                        }
                        else if (isSymbol(first))
                        {
                            if (isSymbol(ch))
                            {
                                Transition transition = new Transition();
                                transition.val = ch;
                                transition.currState += transition.currState;
                                transition.nextState += transition.nextState;

                                transitions.Add(transition);

                                newState.isFinal = false;
                            }
                            else
                            {
                                newState.isFinal = true;
                                first = ch;
                            }
                        }
                        else if (isDigit(first))
                        {
                            if (isDigit(ch))
                            {
                                Transition transition = new Transition();
                                transition.val = ch;
                                transition.currState += transition.currState;
                                transition.nextState += transition.nextState;

                                transitions.Add(transition);

                                newState.isFinal = false;
                            }
                            else
                            {
                                newState.isFinal = true;
                                first = ch;
                            }
                        }
                        else if (first == '{')
                        {

                            Transition transition = new Transition();
                            transition.val = ch;
                            transition.currState += transition.currState;
                            transition.nextState += transition.nextState;

                            transitions.Add(transition);
                            newState.isFinal = false;


                        }
                        else
                        {
                            state.isError = true;
                            Console.WriteLine("ERROR");
                            Error err = new Error();
                            err.desc = "Invalid exeprision";
                            err.lineNumber = line + 1;
                            errors.Add(err);
                        }

                        if (!newState.isFinal)
                        {
                            if (charIndex + 1 < editor.Lines[line].Length)
                                ch = editor.Lines[line][++charIndex];
                            else
                            {
                                newState.isFinal = true;
                                ++charIndex;
                            }
                        }

                        state = newState;

                    }

                    if (state.isFinal)
                    {
                        string token = "";

                        for (int i = 0; i < transitions.Count; ++i) 
                        {
                            token += transitions[i].val;
                        }
                            

                        transitions = new List<Transition>();
                        state.isFinal = false;
                        state.isStart = true;
                        newState.isFinal = false;

                        tmpToken = new Token();

                        if (token.Length > 0)
                        {
                            Error err = new Error();
                            if (isLetter(token[0]))
                            {
                                tmpToken.name = (isReservedWord(token) == TokenType.ID) ? token : "_";
                                tmpToken.tokenType = isReservedWord(token);
                                if (isReservedWord(token) == TokenType.ID) 
                                {
                                    
                                    int j;
                                    for (j = 0; j < symbols.Count; ++j )
                                    {
                                        if (symbols[j].symbol == token)
                                            break;
                                    }
                                    if (j == symbols.Count) 
                                    {
                                        
                                        Symbol symbol = new Symbol();
                                        symbol.symbol = token;
                                        symbol.lineNumber = line + 1;
                                        symbols.Add(symbol);
                                    }
                                }
                            }
                            else if (isDigit(token[0]))
                            {
                                bool flag = false;
                                for (int j = 0; j < token.Length; ++j )
                                {
                                    //Console.WriteLine()
                                }
                                tmpToken.name = token;
                                tmpToken.tokenType = TokenType.Num;
                            }
                            else if (token[0]=='{')
                            {

                                if (!(token[0] == '{' && token[token.Length - 1] == '}'))
                                {
                                    err.desc = "Invalid exeprision";
                                    err.lineNumber = line + 1;
                                    errors.Add(err);
                                }
                                token = token.Replace("{","").Replace("}","");
                                comments.Add(token);
                                
                            }
                            else
                            {
                                tmpToken.tokenType = (token == ":=") ? TokenType.Assign : getSymbol(token);
                            }
                            tokens.Add(tmpToken);
                        }
                    }
                }
            } 
            

            return tokens;
            
        }

        private bool isFinalState() 
        { 
            return false;
        }

        private bool isLetter(char ch)
        {
            return (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90);
        }

        private bool isDigit(char ch)
        {
            return (ch >= 48 && ch <= 57);
        }

        private TokenType isReservedWord(string token) 
        {
            if (token.ToLower() == "if")
                return TokenType.If;

            if (token.ToLower() == "then")
                return TokenType.Then;

            if (token.ToLower() == "else")
                return TokenType.Else;

            if (token.ToLower() == "end")
                return TokenType.End;

            if (token.ToLower() == "repeat")
                return TokenType.Repeat;

            if (token.ToLower() == "until")
                return TokenType.Until;

            if (token.ToLower() == "read")
                return TokenType.Read;

            if (token.ToLower() == "write")
                return TokenType.Write;

            return TokenType.ID;
        }

        private TokenType getSymbol(string token) 
        {
            if (token.ToLower() == "=")
                return TokenType.Equal;

            if (token.ToLower() == "<")
                return TokenType.LessThan;

            if (token.ToLower() == "+")
                return TokenType.Plus;

            if (token.ToLower() == "-")
                return TokenType.Minus;

            if (token.ToLower() == "*")
                return TokenType.Times;

            if (token.ToLower() == "(")
                return TokenType.LeftParentheses;

            if (token.ToLower() == ")")
                return TokenType.RightParentheses;

            return TokenType.SemiColon;
        }

        private bool isSymbol(char ch)
        {
            return (ch == '+' || ch == '-' || ch == '*' || ch == '=' || ch == '<' || ch == '(' || ch == ')' || ch == ';' || ch == ':');
        }
    }
}
