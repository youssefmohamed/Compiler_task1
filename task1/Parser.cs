using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Parser
    {
        List<Token> tokens;
        TokenType currToken = new TokenType();
        int index = 0;

        public Parser(List<Token> tokens) 
        {
            this.tokens = tokens;
            currToken = tokens[index].tokenType;
        }

        public void error() { Console.WriteLine("error"); }

        public bool match(TokenType expected) 
        {
            if(expected == currToken)
            {
                currToken = tokens[index++].tokenType;
                return true;
            }
            
            error();
            return false;
        }

        /* factor -> (exp) | number | identifier */
        public int factor() 
        {
            int tmp=0;
            
            if(match(TokenType.LeftParentheses)) {
                tmp = exp();
                match(TokenType.RightParentheses);
            }
            else if (match(TokenType.Num)) {
                
            }
            else if (match(TokenType.ID))
            {

            }
            else {
                error();
            }

            return tmp;
        }

        /* exp -> simple-exp comparison-op simple-exp |  simple-exp  */
        public int exp()
        {
            int tmp = simpleExp();
            if(currToken == TokenType.LessThan) {
                match(TokenType.LessThan);
                tmp = simpleExp();
            }
            else if (currToken == TokenType.Equal)
            {
                match(TokenType.Equal);
                tmp = simpleExp();
            }

            return tmp;
        }

        /* term ->factor{ mulop factor } */
        public int term() 
        {
            int tmp = factor();

            while (currToken == TokenType.Times) {
                if (match(TokenType.Times)) {
                    tmp *= factor();
                }
                else {
                    error();
                    break;
                }
            }

            return tmp;
        }

        /* simple-exp -> term{ addop term  } */
        public int simpleExp()
        {
            int tmp = term();

            while(currToken == TokenType.Plus || currToken == TokenType.Minus) {
                if(match(TokenType.Plus)) {
                    tmp += term();
                }
                else if (match(TokenType.Minus)) {
                    tmp -= term();
                }
                else {
                    error();
                    break;
                }
            }
            return tmp;
        }

        /* write-stmt -> write exp */
        public int writeStmt() 
        {
            int tmp = 0;
            if (match(TokenType.Write)) {
                tmp = exp();
            }
            else {
                error();
            }
            return tmp;
        }

        /* read-stmt -> read exp */
        public int readStmt()
        {
            int tmp = 0;
            if (match(TokenType.Read)) {
                tmp = exp();
            }
            else {
                error();
            }
            return tmp;
        }

        /* assign-stmt -> identifier := exp */
        public int assignStmt() 
        {
            int tmp = 0;
            if (match(TokenType.ID)) {
                match(TokenType.Assign);
                tmp = exp();
            }
            else {
                error();
            }
            return tmp;
        }

        /* repeat-stmt -> repeat stmt-sequence until exp */
        public int repeatStmt()
        {
            int tmp = 0;
            if (match(TokenType.Repeat)) {
                tmp = stmtSequence();
                match(TokenType.Until);
                tmp = exp();
            }
            else {
                error();
            }
            return tmp;
        }

        /* stmt-sequence -> statement {; statement } */
        public int stmtSequence() 
        {
            int tmp = statement();
            while(currToken == TokenType.SemiColon) {
                if(match(TokenType.SemiColon)) {
                    tmp = statement();
                }
                else {
                    error();
                }
            }
            return tmp;
        }

        /*statement -> if-stmt | repeat-stmt | assign-stmt | read-stmt | write-stmt*/
        public int statement() 
        {
            int tmp = 0;
            if (match(TokenType.If)) {
            
            }
            else if (match(TokenType.Repeat)) {
            
            }
            else if (match(TokenType.Assign)) {
            
            }
            else if (match(TokenType.Read)) {
            
            }
            else if (match(TokenType.Write)) {
            
            }
            else {
                error();
            }
            return tmp;
        }

        /* if-stmt -> if exp then stmt-sequence end | if exp then stmt-sequence else stmt-  sequence end */
        public int ifStmt() 
        {
            int tmp = 0;
            if (match(TokenType.If)) {
                tmp = exp();
                match(TokenType.Then);
                tmp = stmtSequence();
                match(TokenType.End);
            }
            else {
                match(TokenType.If);
                tmp = exp();
                match(TokenType.Then);
                tmp = stmtSequence();
                match(TokenType.Else);
                tmp = stmtSequence();
                match(TokenType.End);
            }
            return tmp;
        }
    }
}
