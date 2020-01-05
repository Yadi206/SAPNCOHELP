using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class WReturnShow : Form
{
    /// <summary>执行结果
    /// </summary>
    public RFC_Return sRFCReturn = new RFC_Return();

    /// <summary>RFC执行变量
    /// </summary>
    public RFC_Function sRFC = new RFC_Function();

    public WReturnShow()
    {
        InitializeComponent();
    }
    private void WReturnShow_Load(object sender, EventArgs e)
    {
        CWaitForm b = new CWaitForm();
        b._wtVoid = 执行;
        b.ShowDialog();
        this.Text = sRFC.FunName;
        if (sRFCReturn.IsConnect)
        {
            tbDiaoYongJieGuo.Text = "";
            tbDiaoYongJieGuo.Text += "调用函数名称:" + sRFCReturn.FunName + Environment.NewLine;
            tbDiaoYongJieGuo.Text += "函数执行时间(秒S):" + sRFCReturn.RunTime.TotalSeconds + " 秒" + Environment.NewLine + Environment.NewLine;
            if (sRFCReturn.OutPutZhiCanShus.Count > 0)//值参数
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("字段名");
                dt.Columns.Add("值");
                tbDiaoYongJieGuo.Text += "★★返回值类型【" + sRFCReturn.OutPutZhiCanShus.Count.ToString() + "个】★★" + Environment.NewLine;
                foreach (RFC_ZhiCanShu item in sRFCReturn.OutPutZhiCanShus)
                {
                    DataRow dr = dt.NewRow();
                    dr["字段名"] = item.Name;
                    dr["值"] = item.Value;
                    dt.Rows.Add(dr);
                    tbDiaoYongJieGuo.Text += item.Name + ": " + item.Value + "   ；" + Environment.NewLine;
                }
                AddTP("tpChuanChuZhi", "传出值", "tgChuanChuZhi", dt);
                tbDiaoYongJieGuo.Text += Environment.NewLine;
            }
            if (sRFCReturn.OutPutTables.Count > 0)//表参数
            {
                tbDiaoYongJieGuo.Text += "★★返回表类型【" + sRFCReturn.OutPutTables.Count.ToString() + "个】★★" + Environment.NewLine;
                foreach (RFC_Table item in sRFCReturn.OutPutTables)
                {
                    AddTP("tp" + item.TableName, "表:" + item.TableName, "tg" + item.TableName, item.Table);
                    tbDiaoYongJieGuo.Text += item.TableName + ": 总行数" + item.RFC_Structures.Count.ToString() + "；" + Environment.NewLine;
                }
                tbDiaoYongJieGuo.Text += Environment.NewLine;
            }

            if (sRFCReturn.OutPutStructures.Count > 0)//结构
            {
                tbDiaoYongJieGuo.Text += "★★返回结构类型【" + sRFCReturn.OutPutStructures.Count.ToString() + "个】★★" + Environment.NewLine;
                foreach (RFC_Structure item in sRFCReturn.OutPutStructures)
                {
                    RFC_Table dt = new RFC_Table();
                    tbDiaoYongJieGuo.Text += item.StructureName + "【" + item.RFC_ZhiCanShus.Count.ToString() + "个】" + Environment.NewLine;
                    dt.RFC_Structures.Add(item);
                    foreach (RFC_ZhiCanShu item1 in item.RFC_ZhiCanShus)
                    {
                        tbDiaoYongJieGuo.Text += item1.Name + ": " + item1.Value + "   ；" + Environment.NewLine;
                    }
                    AddTP("tp" + item.StructureName, "结构" + item.StructureName, "tg" + item.StructureName, dt.Table);
                }
                tbDiaoYongJieGuo.Text += Environment.NewLine;
            }
        }
        else
        {
            tbDiaoYongJieGuo.Text = "";
            tbDiaoYongJieGuo.Text += "SAP 连接失败";
        }

    }
    /// <summary>添加TAB页展示传出结果.
    /// </summary>
    /// <param name="tpName"></param>
    /// <param name="tpText"></param>
    /// <param name="tgName"></param>
    private void AddTP(string tpName, string tpText, string tgName, object dt)
    {
        System.Windows.Forms.TabPage tabPage1;
        CDataGridView dataGridView1;
        tabPage1 = new System.Windows.Forms.TabPage();
        dataGridView1 = new CDataGridView();
        tabControl1.Controls.Add(tabPage1);
        tabPage1.Controls.Add(dataGridView1);
        tabPage1.Location = new System.Drawing.Point(4, 22);
        tabPage1.Name = tpName;
        tabPage1.Size = new System.Drawing.Size(761, 429);
        tabPage1.TabIndex = 1;
        tabPage1.Text = tpText;
        tabPage1.UseVisualStyleBackColor = true;
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToOrderColumns = true;
        dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        dataGridView1.Location = new System.Drawing.Point(0, 0);
        dataGridView1.Name = tgName;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.Size = new System.Drawing.Size(761, 429);
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        dataGridView1.TabIndex = 2;
        dataGridView1.DataSource = dt;
    }

    private void 执行()
    {
        SAPHelper a = new SAPHelper();
        sRFCReturn = a.RunRFC(sRFC);
    }
}

