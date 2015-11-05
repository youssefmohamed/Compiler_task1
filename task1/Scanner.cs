//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace task1
//{
//    class Scanner
//    {
//        private RichTextBox editor;
//        public List<Error> error;
//        public List<Comment> comment;
//        public List<Symbol> symbol; 
//        public List<State> state;
//        public List<Transition> transition;

//        public Scanner(RichTextBox editor) {
//            this.editor = editor;
//            error = new List<Error>();
//            comment = new List<Comment>();
//            symbol = new List<Symbol>();
//            state = new List<State>();
//            transition = new List<Transition>();
//        }

//        public List<Token> tokens() 
//        {
//            List<Token> token = new List<Token>();
//            string chars = "";
//            int t = 0;
//            //number of lines in the file
//            int lines = this.editor.Lines.Length;
            
//            for (int i = 0; i < lines; ++i )
//            {
//                t = 0;
                
//                Error err = new Error();
//                Comment comm = new Comment();
//                Symbol symb = new Symbol();
//                 bool isAssign = false;

//                while (editor.Lines[i].Length > t) 
//                {
//                    chars = "";
//                    // scan ids
//                    if (this.isLetter(editor.Lines[i][t]))
//                    {
//                        chars += editor.Lines[i][t];
//                        t++;
//                        if (editor.Lines[i].Length > t)
//                        {
//                            while (this.isLetter(editor.Lines[i][t]) || this.isDigit(editor.Lines[i][t]))
//                            {
//                                chars += editor.Lines[i][t];
//                                t++;
//                                if (editor.Lines[i].Length <= t)
//                                    break;
//                            }
//                        }

//                        Token tok = new Token();
//                        if (this.isReservedWord(chars) == TokenType.ID)
//                        {
//                            symb.lineNumber = i + 1;
//                            symb.symbol = chars;
//                            symbol.Add(symb);

//                            tok.name = chars;
//                            tok.tokenType = this.isReservedWord(chars);
//                        }
//                        else 
//                        {
//                            tok.name = "_";
//                            tok.tokenType = this.isReservedWord(chars);
//                        }
//                        token.Add(tok);
//                    }
//                    else if (this.isDigit(editor.Lines[i][t]))  // scan nums
//                    {
//                        chars += editor.Lines[i][t];
//                        t++;
//                        if (editor.Lines[i].Length > t)
//                        {
//                            while (this.isDigit(editor.Lines[i][t]))
//                            {
//                                chars += editor.Lines[i][t];
//                                t++;
//                                if (editor.Lines[i].Length <= t)
//                                    break;
//                            }
//                        }

//                        // error num
//                        //if (t > 0 && !this.isDigit(editor.Lines[i][t]))
//                        //{
//                        //    Console.WriteLine("NuM");
//                        //    err.desc = "invalid expression " + chars + " with  " + editor.Lines[i][t];
//                        //    err.lineNumber = i + 1;
//                        //    error.Add(err);
//                        //}

//                        Token tok = new Token();
//                        tok.name = chars;
//                        tok.tokenType = TokenType.Num;
//                        token.Add(tok);
//                    }
//                    else if (editor.Lines[i][t] == '{')  // scan comment
//                    {
//                        t++;
//                        chars += editor.Lines[i][t];
//                        if (editor.Lines[i].Length > t)
//                        {
//                            while (editor.Lines[i][t] != '}')
//                            {
//                                chars += editor.Lines[i][t];
//                                t++; 
//                                if (editor.Lines[i].Length <= t)
//                                    break;
//                            }
//                        }
//                        if (editor.Lines[i][editor.Lines[i].Length-1] != '}') 
//                        {
//                            err.desc = "invalid expression " + chars + " with  " + editor.Lines[i][t-1];
//                            err.lineNumber = i + 1;
//                            error.Add(err);
//                            ++t;

//                            continue;
//                        }
//                        t++;
//                        comm.lineNumber = i + 1;
//                        comm.comment = chars;
//                        comment.Add(comm);
                        
//                    }
//                    else if (editor.Lines[i][t] == '=' && isAssign)
//                    {
//                        chars += ":=";
//                        t++;
//                        Token tok = new Token();
//                        tok.name = chars;
//                        tok.tokenType = TokenType.Assign;
//                        token.Add(tok);
//                        isAssign = false;
//                    }
//                    else if (this.isSymbol(editor.Lines[i][t]))  // scan special symbols
//                    {

//                        if (editor.Lines[i][t] == ':')
//                        {
//                            isAssign = true;
//                            chars += editor.Lines[i][t];
//                            t++;
//                        }
//                        else 
//                        {
//                            chars += editor.Lines[i][t];
//                            t++;
//                            Token tok = new Token();
//                            tok.name = "_";
//                            tok.tokenType = this.getSymbol(chars) ;
//                            token.Add(tok);
//                        }
//                    }
//                    else if (editor.Lines[i][t] == ' ') // scape white space
//                    {
//                        ++t;
//                    }
//                    else
//                    {
//                        Console.WriteLine(i + "  " + t + " " + editor.Lines[i][t]);
//                        // error char
//                        err.desc = "invalid expression " + chars + " with  " + editor.Lines[i][t];
//                        err.lineNumber = i + 1;
//                        error.Add(err);
                        
//                        ++t;
//                    }

//                }
//            }

//            return token;
//        }

//        private bool isLetter(char ch) {
//            return (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90); 
//        }

//        private bool isDigit(char ch) {
//            return (ch >= 48 && ch <= 57);
//        }

//        private bool isSpace(char ch)
//        {
//            return (ch == ' ');
//        }

//        private bool isSymbol(char ch) 
//        {
//            return (ch == '+' || ch == '-' || ch == '*' || ch == '=' || ch == '<' || ch == '(' || ch == ')' || ch == ';' || ch == ':');
//        }

//        private TokenType isReservedWord(string token) {
//            if (token.ToLower() == "if")
//                return TokenType.If;

//            if (token.ToLower() == "then")
//                return TokenType.Then;

//            if (token.ToLower() == "else")
//                return TokenType.Else;

//            if (token.ToLower() == "end")
//                return TokenType.End;

//            if (token.ToLower() == "repeat")
//                return TokenType.Repeat;

//            if (token.ToLower() == "until")
//                return TokenType.Until;

//            if (token.ToLower() == "read")
//                return TokenType.Read;

//            if (token.ToLower() == "write")
//                return TokenType.Write;

//            return TokenType.ID;
//        }


//        private TokenType getSymbol(string token) 
//        {
//            if (token.ToLower() == "=")
//                return TokenType.Equal;

//            if (token.ToLower() == "<")
//                return TokenType.LessThan;

//            if (token.ToLower() == "+")
//                return TokenType.Plus;

//            if (token.ToLower() == "-")
//                return TokenType.Minus;

//            if (token.ToLower() == "*")
//                return TokenType.Times;

//            if (token.ToLower() == "(")
//                return TokenType.LeftParentheses;

//            if (token.ToLower() == ")")
//                return TokenType.RightParentheses;

//            return TokenType.SemiColon;
//        }
//    }
//}
