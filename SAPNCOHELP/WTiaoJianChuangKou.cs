using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
  
    public partial class WTiaoJianChuangKou : Form
    {
        public List<ShaiXuan> ShaiXuans = new List<ShaiXuan>();
        public List<BianLiang> BianLiangs = new List<BianLiang>();
        public WTiaoJianChuangKou(DataGridView dg)
        {
            InitializeComponent();

            //标题赋值
            //
            //this.ClientSize = new System.Drawing.Size(25 + 12 * 10 + 300, 80 + (dg.Columns.Count - 1) * 24);
            //设置窗体大小
            //获取最多描述字体个数
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                SuspendLayout();
                AddLabel(dg.Columns[i].HeaderText, dg.Columns[i].HeaderText.Length, i + 1);
                AddTextBox(dg.Columns[i].HeaderText, dg.Columns[i].HeaderText.Length, i + 1);
                this.ResumeLayout(false);
            }
          
        }
        public String ZhiXingYuju = "";
        /// <summary>添加Lable
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ZiShu"></param>
        /// <param name="XuHao"></param>
        private void AddLabel(String Name, int ZiShu, int XuHao)
        {
            System.Windows.Forms.Label label1;
            label1 = new System.Windows.Forms.Label();
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, XuHao * 24);
            label1.Name = "lb" + Name;
            label1.Size = new System.Drawing.Size(12 * ZiShu, 12);
            label1.TabIndex = 0;
            label1.Text = Name;
            this.Controls.Add(label1);
        }
        /// <summary>添加TextBox
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ZiShu"></param>
        /// <param name="XuHao"></param>
        private void AddTextBox(String Name, int ZiShu, int XuHao)
        {
            System.Windows.Forms.TextBox textBox1;
            textBox1 = new System.Windows.Forms.TextBox();
            textBox1.Location = new System.Drawing.Point( 12 * ZiShu, 21 + (XuHao - 1) * 24);
            textBox1.Name = "tb" + Name;
            textBox1.Size = new System.Drawing.Size(200, 21);
            textBox1.TabIndex = XuHao;
            this.Controls.Add(textBox1);
            BianLiangs.Add(new BianLiang(textBox1, "TextBox"));
        }
        /// <summary>取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (BianLiang item in BianLiangs)
                {
                    if (item.LeiXing == "TextBox")
                    {
                        ShaiXuans.Add(new ShaiXuan((item.DuiXiang as TextBox).Name.Substring(2, (item.DuiXiang as TextBox).Name.Length - 2), (item.DuiXiang as TextBox).Text));
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    /// <summary>筛选
    /// </summary>
    public class ShaiXuan
    {
        private String _ZiDuan = "";
        /// <summary> 字段
        /// </summary>
        public String ZiDuan
        {
            get { return _ZiDuan; }
            set { _ZiDuan = value; }
        }

        private string _Zhi = "";
        /// <summary> 值
        /// </summary>
        public string Zhi
        {
            get { return _Zhi; }
            set { _Zhi = value; }
        }

        public ShaiXuan(string sZiDuan, string sZhi)
        {
            ZiDuan = sZiDuan;
            Zhi = sZhi;
        }
    }

    /// <summary>记录创建的变量
    /// </summary>
    public class BianLiang
    {
        private object _DuiXiang = new object();
        /// <summary> 存储变量对象
        /// </summary>
        public object DuiXiang
        {
            get { return _DuiXiang; }
            set { _DuiXiang = value; }
        }

        private string _LeiXing = "";
        /// <summary>类型
        /// </summary>
        public string LeiXing
        {
            get { return _LeiXing; }
            set { _LeiXing = value; }
        }
        public BianLiang(object sDuiXiang, string sLeiXing)
        {
            _DuiXiang = sDuiXiang;
            _LeiXing = sLeiXing;
        }

    }
 