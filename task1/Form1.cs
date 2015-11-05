using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1
{
    public partial class Form1 : Form
    {

        //private Scanner scanner;
        private DFA dfa;

        public Form1()
        {
            InitializeComponent();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //scanner = new Scanner(editor);
            dfa = new DFA(editor);
            List<Token> token = dfa.GenericDFA();

            //clear the output tables :D

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();

            for (int i = 0; i < token.Count; ++i)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = token[i].tokenType;
                dataGridView1.Rows[i].Cells[1].Value = token[i].name;
            }

            for (int i = 0; i < dfa.errors.Count; ++i)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = dfa.errors[i].lineNumber;
                dataGridView2.Rows[i].Cells[1].Value = dfa.errors[i].desc;

                //editor.SelectionStart = 0;
                //editor.SelectionLength = editor.Lines[scanner.error[i].lineNumber].Length;
                //editor.SelectionColor = Color.Red;
            }

            for (int i = 0; i < dfa.comments.Count; ++i)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = dfa.comments[i];
            }

            for (int i = 0; i < dfa.symbols.Count; ++i)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = "_";
                dataGridView4.Rows[i].Cells[1].Value = dfa.symbols[i].symbol;

            }

        }

        private void editor_TextChanged(object sender, EventArgs e)
       {
            
        }
    }
}
