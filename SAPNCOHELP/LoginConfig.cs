using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;


public partial class LoginConfig : Form
{
    public LoginConfig()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string path = Assembly.GetEntryAssembly().Location;
        FileInfo finfo = new FileInfo(path);
        //return finfo.Name;
        XmlDocument xmlDoc = new XmlDocument();
        string file = finfo.Name + ".config";
        xmlDoc.Load(file);
        XmlNode xNode = xmlDoc.SelectSingleNode("//configuration/SAP.Middleware.Connector/ClientSettings/DestinationConfiguration/destinations/add");
        xNode.Attributes["ASHOST"].Value = tbIP.Text;
        xNode.Attributes["CLIENT"].Value = tbKeHuDuanHao.Text;
        xNode.Attributes["LANG"].Value = tbDengLuYuYan.Text;
        xNode.Attributes["USER"].Value = tbDengLuZhangHao.Text;
        xNode.Attributes["PASSWD"].Value = tbMiMa.Text;
        xNode.Attributes["SYSNR"].Value = tbXiTongHao.Text;
        xNode.Attributes["SAPROUTER"].Value = tbLuYou.Text;
        xmlDoc.Save(file);
        this.DialogResult = DialogResult.OK;
    }

    private void LoginConfig_Load(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string file = (new FileInfo(Assembly.GetEntryAssembly().Location)).Name + ".config";
        DataSet ds = new DataSet();
        ds.ReadXml(file);
        foreach (DataRow item in ds.Tables["add"].Rows)
        {
            tbIP.Text = item["ASHOST"].ToString();
            tbKeHuDuanHao.Text = item["CLIENT"].ToString();
            tbDengLuYuYan.Text = item["LANG"].ToString();
            tbDengLuZhangHao.Text = item["USER"].ToString();
            tbMiMa.Text = item["PASSWD"].ToString();
            tbXiTongHao.Text = item["SYSNR"].ToString();
            tbLuYou.Text = item["SAPROUTER"].ToString();
        }
    }

    private void button2_Click_1(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
    }
}
