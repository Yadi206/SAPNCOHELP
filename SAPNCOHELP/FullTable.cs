using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class FullTable : Form
{
  public DataTable dt;
  public DataSet ds;
  public int lines = 0;
  public FullTable()
  {
    InitializeComponent();
  }

  private void FullTable_Load(object sender, EventArgs e)
  {
    cDataGridView1.DataSource = dt;
  }

  private void cDataGridView1_ButtonSelectClick(CDataGridView cdgv, int rowindex, int columnindex)
  {
    string linetabletypename = "";//表结构名称 
    string linetablename = "";//表名称
    if (((DataTable)cdgv.DataSource).TableName.ToString() == "IM")
    {
      linetabletypename = ((DataTable)cdgv.DataSource).Namespace.ToString() + "T" + "." + ((DataTable)cdgv.DataSource).Rows[rowindex]["Code"].ToString();
    }
    else
    {
      linetabletypename = ((DataTable)cdgv.DataSource).Namespace.ToString() + "." + ((DataTable)cdgv.DataSource).Columns[columnindex].ColumnName.ToString();
    }
    linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
    if (!ds.Tables.Contains(linetablename))
    {
      //不存在则创建
      DataTable dt = new DataTable();
      dt = ds.Tables[linetabletypename].Clone();
      dt.Namespace = linetabletypename;
      dt.TableName = linetablename;
      ds.Tables.Add(dt);
    }
    FullTable a = new FullTable();
    a.Text = linetablename;
    a.ds = ds;
    a.dt = ds.Tables[linetablename];
    a.ShowDialog();
    cdgv.Rows[rowindex].Cells[columnindex].Value = "点击赋值:" + a.lines.ToString() + "行";
  }

  private void FullTable_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.lines = ((DataTable)cDataGridView1.DataSource).Rows.Count;
  }
}
