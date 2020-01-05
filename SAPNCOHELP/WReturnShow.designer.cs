
partial class WReturnShow
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tpDiaoYongJieGuo = new System.Windows.Forms.TabPage();
        this.tbDiaoYongJieGuo = new System.Windows.Forms.TextBox();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.tabControl1.SuspendLayout();
        this.tpDiaoYongJieGuo.SuspendLayout();
        this.contextMenuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tpDiaoYongJieGuo);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 0);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(769, 455);
        this.tabControl1.TabIndex = 0;
        // 
        // tpDiaoYongJieGuo
        // 
        this.tpDiaoYongJieGuo.Controls.Add(this.tbDiaoYongJieGuo);
        this.tpDiaoYongJieGuo.Location = new System.Drawing.Point(4, 22);
        this.tpDiaoYongJieGuo.Name = "tpDiaoYongJieGuo";
        this.tpDiaoYongJieGuo.Padding = new System.Windows.Forms.Padding(3);
        this.tpDiaoYongJieGuo.Size = new System.Drawing.Size(761, 429);
        this.tpDiaoYongJieGuo.TabIndex = 0;
        this.tpDiaoYongJieGuo.Text = "调用结果";
        this.tpDiaoYongJieGuo.UseVisualStyleBackColor = true;
        // 
        // tbDiaoYongJieGuo
        // 
        this.tbDiaoYongJieGuo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tbDiaoYongJieGuo.Location = new System.Drawing.Point(3, 3);
        this.tbDiaoYongJieGuo.Multiline = true;
        this.tbDiaoYongJieGuo.Name = "tbDiaoYongJieGuo";
        this.tbDiaoYongJieGuo.Size = new System.Drawing.Size(755, 423);
        this.tbDiaoYongJieGuo.TabIndex = 0;
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制CToolStripMenuItem});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(175, 48);
        // 
        // 复制CToolStripMenuItem
        // 
        this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
        this.复制CToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
        this.复制CToolStripMenuItem.Text = "复制【C】 Ctrl+D";
        // 
        // WReturnShow
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(769, 455);
        this.Controls.Add(this.tabControl1);
        this.KeyPreview = true;
        this.Name = "WReturnShow";
        this.Text = "WReturnShow";
        this.Load += new System.EventHandler(this.WReturnShow_Load);
        this.tabControl1.ResumeLayout(false);
        this.tpDiaoYongJieGuo.ResumeLayout(false);
        this.tpDiaoYongJieGuo.PerformLayout();
        this.contextMenuStrip1.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpDiaoYongJieGuo;
    private System.Windows.Forms.TextBox tbDiaoYongJieGuo;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
}
