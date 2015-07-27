using System;
using System.Windows.Forms;
using SimpleHashing.Net;

namespace SimpleHashing.UI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            SimpleHash hash = new SimpleHash();

            txtHash.Text = hash.Compute(txtPassword.Text, (int) txtIterations.Value);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            SimpleHash hash = new SimpleHash();
            if (hash.Verify(txtPassword.Text, txtHash.Text))
            {
                Text = "Verification succeeded";
            }
            else
            {
                Text = "Verification failed";
            }
        }
    }
}
