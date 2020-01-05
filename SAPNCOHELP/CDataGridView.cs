using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;


public partial class CDataGridView : DataGridView
{
    private Array _ShowButtonColumns;   //有按钮的列名称   
    /// <summary>  
    /// 设置要显示按钮的列  
    /// </summary>  
    /// <param name="ShowButtonColumns"></param>  
    public void SetParam(Array ShowButtonColumns)
    {
        _ShowButtonColumns = ShowButtonColumns;
    }

    public CDataGridView()
    {
        InitializeComponent();
        this.Controls.Add(button1);
    }

    //定义按钮的单击事件  
    public delegate void ButtonClick(CDataGridView cdgv, int rowindex, int columnindex);
    public event ButtonClick ButtonSelectClick;
    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            this.ButtonSelectClick.DynamicInvoke(this, this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    public void _KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
            {
                this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
                Clipboard.SetText(this.GetClipboardContent().GetData(DataFormats.Text).ToString());
            }
            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Control)
            {
                this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                Clipboard.SetText(this.GetClipboardContent().GetData(DataFormats.Text).ToString());
            }
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void CToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            Clipboard.SetText(this.GetClipboardContent().GetData(DataFormats.Text).ToString());

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void CToolStripMenuItem2_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            Clipboard.SetText(this.GetClipboardContent().GetData(DataFormats.Text).ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private void CToolStripMenuItem3_Click(object sender, EventArgs e)
    {
        try
        {
            WTiaoJianChuangKou a = new WTiaoJianChuangKou(this);
            if (a.ShowDialog() == DialogResult.OK)
            {
                DataView dv = new DataView();
                dv = (this.DataSource as DataTable).DefaultView;
                string aa = "1=1 ";
                foreach (ShaiXuan item in a.ShaiXuans)
                {
                    if (!string.IsNullOrEmpty(item.Zhi))
                    {
                        aa += " AND " + item.ZiDuan + " Like '%" + item.Zhi + "%' ";
                    }
                }
                dv.RowFilter = aa;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
        try
        {
            //添加行
            DataTable dt = new DataTable();
            dt = (this.DataSource as DataTable);
            DataRow dr = dt.NewRow();
            foreach (DataColumn item in dt.Columns)
            {
                if (item.Namespace != "STRING" && !string.IsNullOrEmpty(item.Namespace))
                {
                    dr[item.ColumnName] = "点击赋值";
                }
            }
            dt.Rows.Add(dr);
            this.DataSource = dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
        try
        {
            int j = this.SelectedRows.Count;
            if (j == 0)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }
            for (int i = 0; i < j; i++)
            {
                this.Rows.Remove(this.SelectedRows[0]);
                this.EndEdit();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtt = new DataTable();
            dtt = GetCopyString();
            int CurrentRowIndex, CurrentColumnIndex = 0;
            if (this.CurrentCell.RowIndex == null)
            {
                return;
            }
            CurrentRowIndex = this.CurrentCell.RowIndex;
            CurrentColumnIndex = this.CurrentCell.ColumnIndex;
            int r, c;
            r = 0;
            for (int i = CurrentRowIndex; i < this.Rows.Count; i++)
            {
                c = 0;
                for (int j = CurrentColumnIndex; j < this.Columns.Count; j++)
                {
                    this.Rows[i].Cells[j].Value = dtt.Rows[r][c];
                    c++;
                    if (c == dtt.Columns.Count)
                    {
                        break;
                    }
                }
                r++;
                if (r == dtt.Rows.Count)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private void toolStripMenuItem4_Click(object sender, EventArgs e)
    {
        try
        {

            FullTable a = new FullTable();
            a.dt = this.DataSource as DataTable;
            a.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    public void _CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {

            if (e.Button == MouseButtons.Right)
            {
                Point formPoint = this.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
                contextMenuStrip1.Show(this.PointToScreen(formPoint));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    /// <summary>重新画行头
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void DataGridEx_RowPostPaint(object sender, System.Windows.Forms.DataGridViewRowPostPaintEventArgs e)
    {
        try
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
            this.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, ((e.RowIndex) + 1).ToString(),
            this.RowHeadersDefaultCellStyle.Font,
            rectangle, this.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void GridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
        try
        {

            Rectangle rowBounds =
                new Rectangle(0, e.RowBounds.Top, this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) -
                                                  this.HorizontalScrollingOffset + 1,
                              e.RowBounds.Height);
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                if (this.RowTemplate.DefaultCellStyle.SelectionBackColor == Color.Transparent)
                    DrawLinearGradient(rowBounds, e.Graphics, _SelectedRowColor1, _SelectedRowColor2);
            }
            else
            {
                if (this.RowTemplate.DefaultCellStyle.BackColor == Color.Transparent)
                {
                    if (e.RowIndex % _SecondaryLength == 1)
                    {
                        DrawLinearGradient(rowBounds, e.Graphics, _PrimaryRowColor1, _PrimaryRowColor2);
                    }
                    else
                    {
                        DrawLinearGradient(rowBounds, e.Graphics, _SecondaryRowColor1, _SecondaryRowColor2);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private void GridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
        try
        {
            if (e.RowIndex == -1)
            {
                DataGridViewColumn dgvc = new DataGridViewColumn();

                if (!(_ColumnHeaderColor1 == Color.Transparent) && !(_ColumnHeaderColor2 == Color.Transparent) &&
                    !_ColumnHeaderColor1.IsEmpty && !_ColumnHeaderColor2.IsEmpty)
                {
                    DrawLinearGradient(e.CellBounds, e.Graphics, _ColumnHeaderColor1, _ColumnHeaderColor2);
                    e.Paint(e.ClipBounds, (DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background));
                    e.Handled = true;
                }
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private Color _ColumnHeaderColor1 = Color.White;
    /// <summary>表头起始颜色
    /// </summary>
    public Color ColumnHeaderColor1
    {
        get { return _ColumnHeaderColor1; }
        set
        {
            _ColumnHeaderColor1 = value;
            this.Invalidate();
        }
    }
    private Color _ColumnHeaderColor2 = Color.FromArgb(212, 208, 200);
    /// <summary>表头终止颜色
    /// </summary>
    public Color ColumnHeaderColor2
    {
        get { return _ColumnHeaderColor2; }
        set
        {
            _ColumnHeaderColor2 = value;
            this.Invalidate();
        }
    }
    private Color _PrimaryRowColor1 = Color.White;
    /// <summary>奇行起始颜色
    /// </summary>
    public Color PrimaryRowcolor1
    {
        get { return _PrimaryRowColor1; }
        set
        {
            if (value.IsEmpty || value == Color.Transparent)
                _PrimaryRowColor1 = Color.White;
            else
                _PrimaryRowColor1 = value;
        }
    }
    private Color _PrimaryRowColor2 = Color.FromArgb(255, 249, 232);
    /// <summary>奇行终止颜色
    /// </summary>
    public Color PrimaryRowcolor2
    {
        get { return _PrimaryRowColor2; }
        set
        {
            if (value.IsEmpty || value == Color.Transparent)
                _PrimaryRowColor2 = Color.White;
            else
                _PrimaryRowColor2 = value;
        }
    }
    private Color _SelectedRowColor1 = Color.White;
    /// <summary>偶行起始颜色
    /// </summary>
    public Color SecondaryRowColor1
    {
        get { return _SecondaryRowColor1; }
        set
        {
            if (value.IsEmpty || value == Color.Transparent)
                _SecondaryRowColor1 = Color.White;
            else
                _SecondaryRowColor1 = value;
        }
    }
    private Color _SelectedRowColor2 = Color.FromArgb(171, 217, 254);
    /// <summary>偶行起始颜色
    /// </summary>
    public Color SecondaryRowColor2
    {
        get { return _SecondaryRowColor2; }
        set
        {
            if (value.IsEmpty || value == Color.Transparent)
                _SecondaryRowColor2 = Color.White;
            else
                _SecondaryRowColor2 = value;
        }
    }
    private Color _SecondaryRowColor1 = Color.White;
    /// <summary>选中行起始颜色
    /// </summary>
    public Color SelectedRowColor1
    {
        get { return _SelectedRowColor1; }

        set { _SelectedRowColor1 = value; }
    }
    private Color _SecondaryRowColor2 = Color.White;
    /// <summary>选中行终止颜色
    /// </summary>
    public Color SelectedRowColor2
    {
        get { return _SelectedRowColor2; }

        set { _SelectedRowColor2 = value; }
    }
    private int _SecondaryLength = 2;
    /// <summary>这个长度现在是指导隔多少个行出现一个偶行
    /// </summary>
    public int SecondaryLength
    {
        get { return _SecondaryLength; }
        set { _SecondaryLength = value; }
    }
    private static void DrawLinearGradient(Rectangle Rec, Graphics Grp, Color Color1, Color Color2)
    {
        try
        {
            if (Color1 == Color2)
            {
                Brush backbrush = new SolidBrush(Color1);
                Grp.FillRectangle(backbrush, Rec);
            }
            else
            {
                using (Brush backbrush =
                    new LinearGradientBrush(
                        Rec,
                        Color1,
                        Color2,
                        LinearGradientMode.Vertical
                        )
                        )
                {
                    Grp.FillRectangle(backbrush, Rec);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>获取剪切板内容到Datatable中
    /// </summary>
    /// <returns></returns>
    public DataTable GetCopyString()
    {
        DataTable dtt = new DataTable();
        string str = Clipboard.GetText().Replace("\n", "");
        str = str.TrimEnd('\r');
        str = str.TrimEnd('\t');
        string[] strArray3 = str.Split(new char[] { '\r' });
        string[] strArray2 = strArray3[0].Split(new char[] { '\t' });
        for (int i = 0; i <= strArray2.GetUpperBound(0); i++)
        {
            dtt.Columns.Add(i.ToString());
        }
        foreach (string item in strArray3)
        {
            string[] strArray1 = item.Split(new char[] { '\t' });
            DataRow dr = dtt.NewRow();
            for (int j = 0; j <= strArray1.GetUpperBound(0); j++)
            {
                dr[j.ToString()] = strArray1[j];
            }
            dtt.Rows.Add(dr);
        }
        return dtt;
    }

    /// <summary>  
    /// 数组中是否有与指定值相等的元素  
    /// </summary>  
    /// <param name="columnName"></param>  
    /// <param name="ShowButtonColumns"></param>  
    /// <returns></returns>  
    private bool IsShowButtonColumn(string columnName, Array ShowButtonColumns)
    {
        if (string.IsNullOrEmpty(columnName) || ShowButtonColumns == null || ShowButtonColumns.Length < 1) return false;

        foreach (string astr in ShowButtonColumns)
            if (astr == columnName) return true;

        return false;
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);
    }

    //显示按钮
    private void DataGridViewButton_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        if (this.FirstDisplayedCell == null)
        {
            return;
        }
        string flag = "";
        if (this.CurrentCell.Value.ToString().Length >= 4)
        {
            flag = this.CurrentCell.Value.ToString().Substring(0, 4);
        }
        if (IsShowButtonColumn(this.Columns[this.CurrentCell.ColumnIndex].Name, _ShowButtonColumns) || flag == "点击赋值")
        {

            Point p = new Point();

            if (this.button1.Height != this.Rows[this.CurrentCell.RowIndex].Height)
            {
                this.button1.Height = this.Rows[this.CurrentCell.RowIndex].Height;
            }
            #region 获取X轴的位置


            if (this.RowHeadersVisible)
            {
                //判断该类是否包含行标题,如果该列包含行标题，按钮的横坐标位置等于当前位置加上行标题  
                p.X += this.RowHeadersWidth;
            }
            //FirstDisplayedCell表示左上角第一个单元格  
            for (int i = this.FirstDisplayedCell.ColumnIndex; i <= this.CurrentCell.ColumnIndex; i++)
            {
                if (this.Columns[i].Visible)
                {
                    //当前位置=单元格的宽度加上分隔符发宽度  
                    p.X += this.Columns[i].Width + this.Columns[i].DividerWidth;
                }
            }

            p.X -= this.FirstDisplayedScrollingColumnHiddenWidth;
            p.X -= this.button1.Width;
            #endregion

            #region 获取Y轴位置

            if (this.ColumnHeadersVisible)
            {
                //如果列表题可见，按钮的初始纵坐标位置等于当前位置加上列标题  
                p.Y += this.ColumnHeadersHeight;
            }

            //获取或设置某一列的索引，该列是显示在 DataGridView 上的第一列  
            for (int i = this.FirstDisplayedScrollingRowIndex; i < this.CurrentCell.RowIndex; i++)
            {
                if (this.Rows[i].Visible)
                {
                    p.Y += this.Rows[i].Height + this.Rows[i].DividerHeight;
                }
            }

            #endregion

            this.button1.Location = p;
            this.button1.Visible = true;
        }
        else
        {
            this.button1.Visible = false;
        }
    }
    //隐藏按钮
    private void DataGridViewButton_Scroll(object sender, ScrollEventArgs e)
    {
        this.button1.Visible = false;
    }

}
