using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1
{
    class Scanner
    {
        private RichTextBox editor;
        public List<string> error; 

        
        public Scanner(RichTextBox editor) {
            this.editor = editor;
        }

        public List<Token> tokens() 
        {
            List<Token> token = new List<Token>();
            string chars = "";
            int t = 0;
            
            int lines = this.editor.Lines.Length;            
            for (int i = 0; i < lines; ++i )
            {
                t = 0;
                Error err = new Error();
                bool isError = true;
                while (editor.Lines[i].Length > t) 
                {
                    chars = "";

                    if (this.isLetter(editor.Lines[i][t]))
                    {
                        chars += editor.Lines[i][t++];
                        if (editor.Lines[i].Length > t)
                        {
                            while (this.isLetter(editor.Lines[i][t]) || this.isDigit(editor.Lines[i][t]))
                            {
                                chars += editor.Lines[i][t++];
                                if (editor.Lines[i].Length <= t)
                                    break;
                            }
                        }

                        Token tok = new Token();
                        tok.name = chars;
                        tok.tokenType = TokenType.ID;
                        token.Add(tok);
                    }
                    else if (this.isDigit(editor.Lines[i][t])) 
                    {
                        chars += editor.Lines[i][t++];
                        if (editor.Lines[i].Length > t)
                        {
                            while (this.isDigit(editor.Lines[i][t]))
                            {
                                chars += editor.Lines[i][t++];
                                if (editor.Lines[i].Length <= t)
                                    break;
                            }
                        }

                        Token tok = new Token();
                        tok.name = chars;
                        tok.tokenType = TokenType.Num;
                        token.Add(tok);
                    }
                    else
                    {

                    }

                }
            }

            return token;
        }

        private bool isLetter(char ch) {
            return (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90); 
        }

        private bool isDigit(char ch) {
            return (ch >= 48 && ch <= 57);
        }

        private bool isSpace(char ch)
        {
            return (ch == ' ');
        }

       
    }
}
