using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePieceEngine
{
    using Model;
    public class FrmMain : Form
    {
        public FrmMain()
        {
            Natural n = 16;
            Natural m = 1024ul * 1024ul * 1024ul * 1024ul * 1024ul * 1024ul;
            n = n * m;
            Text = n.ToString();
            WindowState = FormWindowState.Maximized;
        }

    }
}