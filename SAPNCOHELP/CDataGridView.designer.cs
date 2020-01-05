using System.Windows.Forms;
using System.Drawing;

partial class CDataGridView
{
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem CToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem CToolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem CToolStripMenuItem3;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.CToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.CToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        this.CToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
        this.button1 = new System.Windows.Forms.Button();
        this.contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        this.SuspendLayout();
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CToolStripMenuItem1,
            this.CToolStripMenuItem2,
            this.CToolStripMenuItem3,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
        this.contextMenuStrip1.Name = "contextMenuStrip3";
        this.contextMenuStrip1.Size = new System.Drawing.Size(167, 158);
        // 
        // CToolStripMenuItem1
        // 
        this.CToolStripMenuItem1.Name = "CToolStripMenuItem1";
        this.CToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
        this.CToolStripMenuItem1.Text = "复制【&D】 ";
        this.CToolStripMenuItem1.Click += new System.EventHandler(this.CToolStripMenuItem1_Click);
        // 
        // CToolStripMenuItem2
        // 
        this.CToolStripMenuItem2.Name = "CToolStripMenuItem2";
        this.CToolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
        this.CToolStripMenuItem2.Text = "带标题复制【&E】 ";
        this.CToolStripMenuItem2.Click += new System.EventHandler(this.CToolStripMenuItem2_Click);
        // 
        // CToolStripMenuItem3
        // 
        this.CToolStripMenuItem3.Name = "CToolStripMenuItem3";
        this.CToolStripMenuItem3.Size = new System.Drawing.Size(166, 22);
        this.CToolStripMenuItem3.Text = "筛选【&F】 ";
        this.CToolStripMenuItem3.Click += new System.EventHandler(this.CToolStripMenuItem3_Click);
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
        this.toolStripMenuItem1.Text = "添加行【&A】 ";
        this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
        // 
        // toolStripMenuItem2
        // 
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
        this.toolStripMenuItem2.Text = "删除行【&R】 ";
        this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
        // 
        // toolStripMenuItem3
        // 
        this.toolStripMenuItem3.Name = "toolStripMenuItem3";
        this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 22);
        this.toolStripMenuItem3.Text = "从excel粘贴";
        this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
        // 
        // toolStripMenuItem4
        // 
        this.toolStripMenuItem4.Name = "toolStripMenuItem4";
        this.toolStripMenuItem4.Size = new System.Drawing.Size(166, 22);
        this.toolStripMenuItem4.Text = "全屏编辑";
        this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(0, 0);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "表赋值";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Visible = false;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // CDataGridView
        // 
        this.RowTemplate.Height = 23;
        this.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewButton_CellEnter);
        this.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._CellMouseClick);
        this.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.GridView_CellPainting);
        this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridEx_RowPostPaint);
        this.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridView_RowPrePaint);
        this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DataGridViewButton_Scroll);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this._KeyDown);
        this.contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        this.ResumeLayout(false);

    }
    #endregion

    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    public Button button1;
}

