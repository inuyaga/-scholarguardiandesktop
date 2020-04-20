using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scolarguardian
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegistrar registrar = new FormRegistrar();
            registrar.ShowDialog();
        }

        private void btn_identificar_Click(object sender, EventArgs e)
        {
            FomrIdentificar identificar = new FomrIdentificar();
            identificar.ShowDialog();

        }
    }
}
