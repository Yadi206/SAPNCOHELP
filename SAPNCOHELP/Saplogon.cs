using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SAP.Middleware.Connector;
using System.Xml;
using System.Reflection;

namespace SAPNCOHELP
{
    public partial class Saplogon : Form
    {
        DataTable dtLJPZ = new DataTable();
        DataTable dtKJFS = new DataTable();
        public Saplogon()
        {
            InitializeComponent();
        }

        private void Saplogon_Load(object sender, EventArgs e)
        {
            dtLJPZ.Columns.Add("EntryKey");
            dtLJPZ.Columns.Add("Description");
            dtLJPZ.Columns.Add("Server");
            dtLJPZ.Columns.Add("Database");
            dtLJPZ.Columns.Add("MSSysName");
            dtLJPZ.Columns.Add("Router");
            dtLJPZ.Columns.Add("Address");
            dtLJPZ.Columns.Add("Router2");
            dtLJPZ.Columns.Add("RouterChoice");
            dtLJPZ.Columns.Add("System");
            dtLJPZ.Columns.Add("MSSrvName");
            dtLJPZ.Columns.Add("MSSrvPort");
            dtLJPZ.Columns.Add("SessManKey");
            dtLJPZ.Columns.Add("SncName");
            dtLJPZ.Columns.Add("SncChoice");
            dtLJPZ.Columns.Add("Codepage");
            dtLJPZ.Columns.Add("CodepageIndex");
            dtLJPZ.Columns.Add("Origin");
            dtLJPZ.Columns.Add("LowSpeedConnection");
            dtLJPZ.Columns.Add("Utf8Off");
            dtLJPZ.Columns.Add("EncodingID");
            dtLJPZ.Columns.Add("ShortcutType");
            dtLJPZ.Columns.Add("ShortcutString");
            dtLJPZ.Columns.Add("ShortcutTo");
            dtLJPZ.Columns.Add("ShortcutBy");
            dtLJPZ.Columns.Add("SncNoSSO");
            dtLJPZ.Columns.Add("MSLast");
            dgLJPZ.DataSource = dtLJPZ;
            dgLJPZ.Columns["EntryKey"].HeaderText = "ID";
            dgLJPZ.Columns["Description"].HeaderText = "描述";
            dgLJPZ.Columns["Server"].HeaderText = "组/服务器";
            dgLJPZ.Columns["Database"].HeaderText = "实例编号";
            dgLJPZ.Columns["MSSysName"].HeaderText = "系统标识";
            dgLJPZ.Columns["Router"].HeaderText = "路由";
            dgLJPZ.Columns["Address"].HeaderText = "消息服务器";
 
            dtKJFS.Columns.Add("Label");
            dtKJFS.Columns.Add("desc");
            dtKJFS.Columns.Add("sid");
            dtKJFS.Columns.Add("clt");
            dtKJFS.Columns.Add("u");
            dtKJFS.Columns.Add("l");
            dtKJFS.Columns.Add("t");
            dtKJFS.Columns.Add("tit");
            dtKJFS.Columns.Add("wsz");
            dtKJFS.Columns.Add("wd");
            dtKJFS.Columns.Add("pwenc");
            dtKJFS.Columns.Add("cmd");
            dtKJFS.Columns.Add("ok");
            dtKJFS.Columns.Add("reuse");
            dtKJFS.Columns.Add("(null)");
            dgKJFS.DataSource = dtKJFS;

            dgKJFS.Columns["Label"].HeaderText = "ID";
            dgKJFS.Columns["tit"].HeaderText = "名称";
            dgKJFS.Columns["desc"].HeaderText = "链接名称";
            dgKJFS.Columns["sid"].HeaderText = "系统标识";
            dgKJFS.Columns["clt"].HeaderText = "客户端";
            dgKJFS.Columns["u"].HeaderText = "用户名";
            dgKJFS.Columns["l"].HeaderText = "语言";
            dgKJFS.Columns["t"].HeaderText = "模式";
            dgKJFS.Columns["wsz"].HeaderText = "窗口状态";
            dgKJFS.Columns["wd"].HeaderText = "临时文件位置";
            dgKJFS.Columns["pwenc"].HeaderText = "加密密码";
            dgKJFS.Columns["cmd"].HeaderText = "cmd";
            dgKJFS.Columns["ok"].HeaderText = "启动事务代码";
            dgKJFS.Columns["reuse"].HeaderText = "reuse";
            dgKJFS.Columns["(null)"].HeaderText = "reuse";
   

            string Current = Directory.GetCurrentDirectory();//获取当前根目录
            filllogon(Current + @"\saplogon.ini");
            fillsapshort(Current + @"\sapshortcut.ini");
        }

        public void filllogon(String file)
        {
            Ini ini = new Ini(file);

            for (int i = 1; i < 1000; i++)
            {
                string stemp = ini.ReadValue("Server", "Item" + i.ToString());
                if (string.IsNullOrEmpty(stemp))
                {
                    break;
                }
                DataRow dr = dtLJPZ.NewRow();
                dr["Server"] = stemp;
                stemp = ini.ReadValue("EntryKey", "Item" + i.ToString());
                dr["EntryKey"] = stemp;
                stemp = ini.ReadValue("Description", "Item" + i.ToString());
                dr["Description"] = stemp;
                stemp = ini.ReadValue("Database", "Item" + i.ToString());
                dr["Database"] = stemp;
                stemp = ini.ReadValue("MSSysName", "Item" + i.ToString());
                dr["MSSysName"] = stemp;
                stemp = ini.ReadValue("Router", "Item" + i.ToString());
                dr["Router"] = stemp;
                stemp = ini.ReadValue("Address", "Item" + i.ToString());
                dr["Address"] = stemp;
                stemp = ini.ReadValue("Router2", "Item" + i.ToString());
                dr["Router2"] = stemp;
                stemp = ini.ReadValue("RouterChoice", "Item" + i.ToString());
                dr["RouterChoice"] = stemp;
                stemp = ini.ReadValue("System", "Item" + i.ToString());
                dr["System"] = stemp;
                stemp = ini.ReadValue("MSSrvName", "Item" + i.ToString());
                dr["MSSrvName"] = stemp;
                stemp = ini.ReadValue("MSSrvPort", "Item" + i.ToString());
                dr["MSSrvPort"] = stemp;
                stemp = ini.ReadValue("SessManKey", "Item" + i.ToString());
                dr["SessManKey"] = stemp;
                stemp = ini.ReadValue("SncName", "Item" + i.ToString());
                dr["SncName"] = stemp;
                stemp = ini.ReadValue("SncChoice", "Item" + i.ToString());
                dr["SncChoice"] = stemp;
                stemp = ini.ReadValue("Codepage", "Item" + i.ToString());
                dr["Codepage"] = stemp;
                stemp = ini.ReadValue("CodepageIndex", "Item" + i.ToString());
                dr["CodepageIndex"] = stemp;
                stemp = ini.ReadValue("Origin", "Item" + i.ToString());
                dr["Origin"] = stemp;
                stemp = ini.ReadValue("LowSpeedConnection", "Item" + i.ToString());
                dr["LowSpeedConnection"] = stemp;
                stemp = ini.ReadValue("Utf8Off", "Item" + i.ToString());
                dr["Utf8Off"] = stemp;
                stemp = ini.ReadValue("EncodingID", "Item" + i.ToString());
                dr["EncodingID"] = stemp;
                stemp = ini.ReadValue("ShortcutType", "Item" + i.ToString());
                dr["ShortcutType"] = stemp;
                stemp = ini.ReadValue("ShortcutString", "Item" + i.ToString());
                dr["ShortcutString"] = stemp;
                stemp = ini.ReadValue("ShortcutTo", "Item" + i.ToString());
                dr["ShortcutTo"] = stemp;
                stemp = ini.ReadValue("ShortcutBy", "Item" + i.ToString());
                dr["ShortcutBy"] = stemp;
                stemp = ini.ReadValue("SncNoSSO", "Item" + i.ToString());
                dr["SncNoSSO"] = stemp;
                stemp = ini.ReadValue("MSLast", "Item" + i.ToString());
                dr["MSLast"] = stemp;
                dtLJPZ.Rows.Add(dr);
            }
            dgLJPZ.DataSource = dtLJPZ;
        }

        private void bnIMLogOn_Click(object sender, EventArgs e)
        {
            string file = "";
            string filecopy = "";
            string Current = Directory.GetCurrentDirectory();//获取当前根目录

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择SAP配置文件所在文件夹";
            System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();

            dialog.SelectedPath = @"C:\Documents and Settings\" + Environment.UserName.ToString() + @"\Application Data\SAP\Common";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    file = dialog.SelectedPath.ToString() + @"\saplogon.ini";
                    filllogon(file);
                    filecopy = Current + @"\saplogon.ini";

                    FileInfo fcopy = new FileInfo(file);
                    if (fcopy.Exists)
                    {
                        fcopy.CopyTo(filecopy,true);
                    }
                    file = dialog.SelectedPath.ToString() + @"\sapshortcut.ini";
                    fillsapshort(file);
                    filecopy = Current + @"\sapshortcut.ini";

                     fcopy = new FileInfo(file);
                    if (fcopy.Exists)
                    {
                        fcopy.CopyTo(filecopy, true);
                    }

                }
            }

            //string sourceFile = @"c:\temp\New Text Document.txt";
            //string destinationFile = @"c:\temp\test.txt";
            //FileInfo file = new FileInfo(sourceFile);
            //if (file.Exists)
            //{
            //    // true is overwrite 
            //    file.CopyTo(destinationFile, true);
            //}
















        }
        public void fillsapshort(string file)
        {
            Ini ini = new Ini(file);
                        for (int i = 1; i < 1000; i++)
            {
                string stemp = ini.ReadValue("Label", "Key" + i.ToString());
                if (!string.IsNullOrEmpty(stemp))
                {
                    DataRow dr = dtKJFS.NewRow();
                    dr["Label"] = stemp;
                    stemp = ini.ReadValue("Command", "Key" + i.ToString());
                    string[] rs = stemp.Split('"');
                    for (int j = 0; j < rs.Count() - 1; j++)
                    {
                        if (rs[j].Substring(0, 1) == " ")
                        {
                            rs[j] = rs[j].Substring(1, rs[j].Count() - 1);
                        }
                        if (rs[j].Substring(0, 1) == "-")
                        {
                            dr[rs[j].Substring(1, rs[j].Count() - 2)] = rs[j + 1];
                        }
                    }
                    dtKJFS.Rows.Add(dr);
                }
            }
            dgKJFS.DataSource = dtKJFS;
        }
        private void bnIMSapshortcut_Click(object sender, EventArgs e)
        {
            string file = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
            }
            fillsapshort(file);

            //string sourceFile = @"c:\temp\New Text Document.txt";
            //string destinationFile = @"c:\temp\test.txt";
            //FileInfo file = new FileInfo(sourceFile);
            //if (file.Exists)
            //{
            //    // true is overwrite 
            //    file.CopyTo(destinationFile, true);
            //}
        }

        private void dgLJPZ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                tbIP.Text = dgLJPZ.Rows[e.RowIndex].Cells["Server"].Value.ToString();
                tbDengLuYuYan.Text = "ZH";
                tbXiTongHao.Text = dgLJPZ.Rows[e.RowIndex].Cells["Database"].Value.ToString();
                tbLuYou.Text = dgLJPZ.Rows[e.RowIndex].Cells["Router"].Value.ToString();
                tbIP.Text = dgLJPZ.Rows[e.RowIndex].Cells["Server"].Value.ToString();
            }
        }

        private void dgKJFS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                foreach (DataGridViewRow item in dgLJPZ.Rows)
                {
                    if (item.Cells["Description"].Value.ToString() == dgKJFS.Rows[e.RowIndex].Cells["desc"].Value.ToString())
                    {
                        tbIP.Text = item.Cells["Server"].Value.ToString();
                        tbXiTongHao.Text = item.Cells["Database"].Value.ToString();
                        tbLuYou.Text = item.Cells["Router"].Value.ToString();
                        tbIP.Text = item.Cells["Server"].Value.ToString();
                    }
                }
                tbDengLuYuYan.Text = dgKJFS.Rows[e.RowIndex].Cells["l"].Value.ToString();
                tbKeHuDuanHao.Text = dgKJFS.Rows[e.RowIndex].Cells["clt"].Value.ToString();
                tbDengLuZhangHao.Text = dgKJFS.Rows[e.RowIndex].Cells["u"].Value.ToString();
                tbMiMa.Focus();
            }
        }

        RfcDestination SapRfcDestination;
        RfcRepository SapRfcRepository;//RFC方法库变量
        IRfcFunction myfun;//RFC函数变量
        public RfcConfigParameters parms = new RfcConfigParameters();

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMiMa.Text.ToString()))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            Form1 f = new Form1();
            RfcConfigParameters RFCPara = new RfcConfigParameters();
            f.parms.Add(RfcConfigParameters.AppServerHost, tbIP.Text.ToString());   //SAP主机IP 
            // paras.Add(RfcConfigParameters.SAPRouter,  tbLuYou.Text.ToString());   //SAP主机 路由器
            f.parms.Add(RfcConfigParameters.SystemNumber, tbXiTongHao.Text.ToString());  //SAP实例
            f.parms.Add(RfcConfigParameters.User, tbDengLuZhangHao.Text.ToString());  //用户名
            f.parms.Add(RfcConfigParameters.Password, tbMiMa.Text.ToString());  //密码
            f.parms.Add(RfcConfigParameters.Client, tbKeHuDuanHao.Text.ToString());  // Client
            f.parms.Add(RfcConfigParameters.Language, tbDengLuYuYan.Text.ToString());  //登陆语言
            f.parms.Add(RfcConfigParameters.Name, "HYDRFC");  //登陆语言
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void tbIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(null, null);

            }
        }
    }
}
