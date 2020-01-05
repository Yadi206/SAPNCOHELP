 
    partial class FullTable
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
          this.cDataGridView1 = new CDataGridView();
          ((System.ComponentModel.ISupportInitialize)(this.cDataGridView1)).BeginInit();
          this.SuspendLayout();
          // 
          // cDataGridView1
          // 
          this.cDataGridView1.AllowUserToAddRows = false;
          this.cDataGridView1.AllowUserToDeleteRows = false;
          this.cDataGridView1.AllowUserToOrderColumns = true;
          this.cDataGridView1.BackgroundColor = System.Drawing.Color.White;
          this.cDataGridView1.ColumnHeaderColor1 = System.Drawing.Color.White;
          this.cDataGridView1.ColumnHeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
          this.cDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.cDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cDataGridView1.Location = new System.Drawing.Point(0, 0);
          this.cDataGridView1.Name = "cDataGridView1";
          this.cDataGridView1.PrimaryRowcolor1 = System.Drawing.Color.White;
          this.cDataGridView1.PrimaryRowcolor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(232)))));
          this.cDataGridView1.RowTemplate.Height = 23;
          this.cDataGridView1.SecondaryLength = 2;
          this.cDataGridView1.SecondaryRowColor1 = System.Drawing.Color.White;
          this.cDataGridView1.SecondaryRowColor2 = System.Drawing.Color.White;
          this.cDataGridView1.SelectedRowColor1 = System.Drawing.Color.White;
          this.cDataGridView1.SelectedRowColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(217)))), ((int)(((byte)(254)))));
          this.cDataGridView1.Size = new System.Drawing.Size(818, 357);
          this.cDataGridView1.TabIndex = 0;
          this.cDataGridView1.ButtonSelectClick += new CDataGridView.ButtonClick(this.cDataGridView1_ButtonSelectClick);
          // 
          // FullTable
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(818, 357);
          this.Controls.Add(this.cDataGridView1);
          this.Name = "FullTable";
          this.Text = "FullTable";
          this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FullTable_FormClosed);
          this.Load += new System.EventHandler(this.FullTable_Load);
          ((System.ComponentModel.ISupportInitialize)(this.cDataGridView1)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private CDataGridView cDataGridView1;

    } 