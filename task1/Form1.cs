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

        private Scanner scanner;

        public Form1()
        {
            InitializeComponent();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scanner = new Scanner(editor);
            List<Token> token = scanner.tokens();

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

            for (int i = 0; i < scanner.error.Count; ++i)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = scanner.error[i].lineNumber;
                dataGridView2.Rows[i].Cells[1].Value = scanner.error[i].desc;
            }

            for (int i = 0; i < scanner.comment.Count; ++i)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = scanner.comment[i].lineNumber;
                dataGridView3.Rows[i].Cells[1].Value = scanner.comment[i].comment;
            }

            for (int i = 0; i < scanner.symbol.Count; ++i)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = scanner.symbol[i].lineNumber;
                dataGridView4.Rows[i].Cells[1].Value = scanner.symbol[i].symbol;
            }

        }

        private void editor_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
