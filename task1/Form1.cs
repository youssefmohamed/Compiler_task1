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

            for (int i = 0; i < token.Count; ++i) {
                Console.WriteLine(token[i].name + " " + token[i].tokenType);
            } 

        }
    }
}
