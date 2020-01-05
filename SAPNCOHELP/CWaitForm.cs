using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
    public partial class CWaitForm : Form
    {
        public CWaitForm()
        {
            InitializeComponent();
        }

        /// <summary>不带参数委托变量
        /// </summary>
        /// <remarks></remarks>
        public WTVoid _wtVoid;
        /// <summary>不带参数委托
        /// </summary>
        /// <remarks></remarks>
        public delegate void WTVoid();


        System.Threading.Thread t;
        private void
        form_Load(System.Object sender, System.EventArgs e)
        {
            t = new System.Threading.Thread(GOGOGO);
            t.Start();
        }
        /// <summary>执行委托
        /// </summary>
        /// <remarks></remarks>
        public void GOGOGO()
        {
            try
            {
                if (_wtVoid != null)
                {
                    _wtVoid();
                    _wtVoid = null;
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
 