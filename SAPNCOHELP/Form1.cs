using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAP.Middleware.Connector;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
namespace SAPNCOHELP
{
    public partial class Form1 : Form
    {
        DataTable dtIM = new DataTable();  //传入参数表
        DataTable dtEX = new DataTable();  //传出参数表
        DataSet dsRFC = new DataSet();  //函数DataSet
        RfcDestination SapRfcDestination;
        RfcRepository SapRfcRepository;//RFC方法库变量
        IRfcFunction myfun;//RFC函数变量
        Stopwatch Stopwatch = new Stopwatch();//记录执行时间
        public RfcConfigParameters parms = new RfcConfigParameters();
        private TextBoxRemind remind = null;//文本框记录

        public Form1()
        {
            InitializeComponent();

            remind = new TextBoxRemind();
            InitTextBoxRemind();

            //string path = Assembly.GetEntryAssembly().Location;
            //FileInfo finfo = new FileInfo(path);
            ////return finfo.Name;
            //XmlDocument xmlDoc = new XmlDocument();
            //string file = finfo.Name + ".config";
            //xmlDoc.Load(file);
            //XmlNode xNode = xmlDoc.SelectSingleNode("//appSettings/add");
            remind.RemLines = 1000;// int.Parse(xNode.Attributes["value"].Value.ToString());
            //   xmlDoc.Save(file);
        }

        void InitTextBoxRemind()
        {
            remind.InitAutoCompleteCustomSource(ttbFunName);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ttbFunName.Focus();
            登陆ToolStripMenuItem_Click(null, null);
        }

        /// <summary>添加TAB页展示传出结果
        /// </summary>
        /// <param name="tc1">页签控件</param>
        /// <param name="tpName">页签名称</param>
        /// <param name="tpText">页签显示文本</param>
        /// <param name="tgName">列表控件名称</param>
        /// <param name="dt">数据表</param>
        private void AddTP(TabControl tc1, string tpName, string tpText, string tgName, object dt)
        {
            System.Windows.Forms.TabPage tabPage1;
            CDataGridView dataGridView1;
            tabPage1 = new System.Windows.Forms.TabPage();
            dataGridView1 = new CDataGridView();
            tc1.Controls.Add(tabPage1);
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
            dataGridView1.ButtonSelectClick += new CDataGridView.ButtonClick(dgIM_ButtonSelectClick); ;
        }

        /// <summary>读取日志填写屏幕
        /// </summary>
        public void FillForm()
        {
            if (dsRFC.DataSetName != "")
            {
                //函数名称赋值
                ttbFunName.Text = dsRFC.DataSetName.Trim();
                ReSet();
                foreach (DataTable item in dsRFC.Tables)
                {
                    if (item.Namespace == "IM")
                    {
                        dgIM.DataSource = item;
                    }
                    if (item.Namespace == "EX")
                    {
                        dgEX.DataSource = item;
                    }
                    if (item.Namespace == "IMS")
                    {
                        AddTP(tcIMJieGou, "tp" + item.TableName, item.TableName + "-" + item.Prefix, "dg" + item.TableName, item);
                    }
                    if (item.Namespace == "EXS")
                    {
                        AddTP(tcEXJieGou, "tp" + item.TableName, item.TableName + "-" + item.Prefix, "dg" + item.TableName, item);
                    }
                    if (item.Namespace == "Tables")
                    {
                        AddTP(tcTable, "tp" + item.TableName, item.TableName + "-" + item.Prefix, "dg" + item.TableName, item);
                    }
                }
            }
        }
        /// <summary>清空
        /// </summary>
        public void ReSet()
        {
            dsRFC = new DataSet();
            //定义传入值表
            dtIM = new DataTable();
            dtIM.Namespace = "IM";
            dtIM.TableName = "IM";
            dtIM.Prefix = "传入参数";
            dtIM.Columns.Add("Code");
            dtIM.Columns.Add("Name");
            dtIM.Columns.Add("Value");
            dtIM.Columns.Add("remarks");
            //定义传出值表
            dtEX = new DataTable();
            dtEX.Namespace = "EX";
            dtEX.TableName = "EX";
            dtEX.Prefix = "传出参数";
            dtEX.Columns.Add("Code");
            dtEX.Columns.Add("Name");
            dtEX.Columns.Add("Value");
            dtEX.Columns.Add("remarks");
            tcIMJieGou.Controls.Clear();
            tcEXJieGou.Controls.Clear();
            tcTable.Controls.Clear();

            dgIM.DataSource = null;
            dgEX.DataSource = null;
            toolStripMenuItem1.Text = "SAP端执行时间：";

        }
        /// <summary>获取函数结构
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 读取结构ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ttbFunName.Text))
                {
                    MessageBox.Show("必须输入函数名");
                    return;
                }
                dsRFC = new DataSet();
                //定义函数名称
                dsRFC.DataSetName = ttbFunName.Text.Trim();
                myfun = SapRfcRepository.CreateFunction(ttbFunName.Text.Trim());
                ReSet();
                //循环获取函数结构
                for (int i = 0; i < myfun.Metadata.ParameterCount; i++)
                {
                    //myfun.Metadata[i].Direction  获取参数类型   
                    if (myfun.Metadata[i].Direction == RfcDirection.IMPORT) // IMPORT  传入RFC参数
                    {
                        //根据myfun.Metadata[i].DataType 判断参数是否为结构
                        if (myfun.Metadata[i].DataType == RfcDataType.STRUCTURE)
                        {
                            DataTable dt = new DataTable();
                            dt = createStructureByParameter(myfun.Metadata[i], "IMS", myfun.Metadata[i].Name);
                            AddTP(tcIMJieGou, "tp" + myfun.Metadata[i].Name, myfun.Metadata[i].Name +
                                  "-" + myfun.Metadata[i].Documentation, "dg" + myfun.Metadata[i].Name, dt);
                        }
                        else if (myfun.Metadata[i].DataType == RfcDataType.TABLE)
                        {
                            DataRow dr = dtIM.NewRow();
                            dr["Code"] = myfun.Metadata[i].Name;
                            dr["Name"] = myfun.Metadata[i].Documentation;
                            dr["Value"] = "点击赋值";
                            dr["remarks"] = "";
                            DataTable dt = new DataTable();
                            dt = createStructureByParameter(myfun.Metadata[i], "IMT", myfun.Metadata[i].Name);
                            dtIM.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dtIM.NewRow();
                            dr["Code"] = myfun.Metadata[i].Name;
                            dr["Name"] = myfun.Metadata[i].Documentation;
                            dr["Value"] = myfun.Metadata[i].DefaultValue;
                            dr["remarks"] = "";
                            dtIM.Rows.Add(dr);
                        }
                    }
                    else if (myfun.Metadata[i].Direction == RfcDirection.EXPORT) //          EXPORT   RFC返回参数
                    {
                        if (myfun.Metadata[i].DataType == RfcDataType.STRUCTURE)
                        {
                            DataTable dt = new DataTable();
                            dt = createStructureByParameter(myfun.Metadata[i], "EXS", myfun.Metadata[i].Name);
                            AddTP(tcEXJieGou, "tp" + myfun.Metadata[i].Name, myfun.Metadata[i].Name + "-" + myfun.Metadata[i].Documentation, "dg" + myfun.Metadata[i].Name, dt);
                        }
                        else if (myfun.Metadata[i].DataType == RfcDataType.TABLE)
                        {
                            DataRow dr = dtEX.NewRow();
                            dr["Code"] = myfun.Metadata[i].Name;
                            dr["Name"] = myfun.Metadata[i].Documentation;
                            dr["Value"] = "点击赋值";
                            dr["remarks"] = "";
                            DataTable dt = new DataTable();
                            dt = createStructureByParameter(myfun.Metadata[i], "EXT", myfun.Metadata[i].Name);
                            dtEX.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dtEX.NewRow();
                            dr["Code"] = myfun.Metadata[i].Name;
                            dr["Name"] = myfun.Metadata[i].Documentation;
                            dr["Value"] = "";
                            dr["remarks"] = "";
                            dtEX.Rows.Add(dr);
                        }
                    }
                    else if (myfun.Metadata[i].Direction == RfcDirection.TABLES) //          TABLES   表参数  
                    {
                        DataTable dt = new DataTable();
                        dt.Namespace = "Tables";
                        dt.TableName = myfun.Metadata[i].Name;
                        //dt.Prefix = myfun.Metadata[i].Documentation;
                        for (int j = 0; j < myfun.Metadata[i].ValueMetadataAsTableMetadata.LineType.FieldCount; j++)
                        {
                            DataColumn dc = new DataColumn();
                            dc.Namespace = "STRING";
                            dc.ColumnName = myfun.Metadata[i].ValueMetadataAsTableMetadata.LineType[j].Name;
                            dc.Caption = myfun.Metadata[i].ValueMetadataAsTableMetadata.LineType[j].Documentation;
                            dt.Columns.Add(dc);
                        }
                        dsRFC.Tables.Add(dt);
                        AddTP(tcTable, "tp" + myfun.Metadata[i].Name, myfun.Metadata[i].Name + "-" + myfun.Metadata[i].Documentation, "dg" + myfun.Metadata[i].Name, dt);
                    }
                }
                dsRFC.Tables.Add(dtIM);
                dsRFC.Tables.Add(dtEX);
                dgIM.DataSource = dtIM;
                foreach (DataGridViewColumn item in dgIM.Columns)
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgEX.DataSource = dtEX;

                foreach (DataGridViewColumn item in dgEX.Columns)
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>执行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 函数执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ttbFunName.Focus();
                //指定函数名称
                dsRFC.DataSetName = ttbFunName.Text.Trim();
                myfun = SapRfcRepository.CreateFunction(ttbFunName.Text.Trim());//根据输入的函数名获取函数对象
                //传入值赋值
                foreach (DataTable item in dsRFC.Tables)
                {
                    //传入值赋值
                    if (item.Namespace == "IM")
                    {
                        int imrowindex = 0;
                        foreach (DataRow item1 in item.Rows)
                        {
                            if (myfun.Metadata[item1["Code"].ToString()].DataType != RfcDataType.TABLE && myfun.Metadata[item1["Code"].ToString()].DataType != RfcDataType.STRUCTURE)
                            {
                                if (!string.IsNullOrEmpty(item1["Value"].ToString()))
                                {
                                    myfun.SetValue(item1["Code"].ToString(), item1["Value"].ToString());
                                }
                            }
                            if (myfun.Metadata[item1["Code"].ToString()].DataType == RfcDataType.TABLE)
                            {
                                if (dsRFC.Tables.Contains("IMT." + item1["Code"].ToString() + "[R" + (imrowindex + 1).ToString() + ",C3]"))
                                {
                                    //setStructureByFun(myfun, myfun.Metadata[item1["Code"].ToString()].DataType.ToString(), "IMT." + item1["Code"].ToString(), "IMT." + item1["Code"].ToString() + "[R" + (imrowindex + 1).ToString() + ",C3]");
                                    setStructureByFun(myfun,
                                                      myfun.Metadata[item1["Code"].ToString()].DataType.ToString(),
                                                      "IMT." + item1["Code"].ToString() + "[R" + (imrowindex + 1).ToString() + ",C3]",
                                                      "IMT." + item1["Code"].ToString());
                                }
                            }
                            imrowindex++;
                        }
                    }
                    //传入结构赋值
                    if (item.Namespace == "IMS")
                    {
                        if (item.Rows.Count > 0)
                        {
                            setStructureByFun(myfun, "STRUCTURE", item.Namespace, item.TableName);
                        }
                    }
                    //表赋值
                    if (item.Namespace == "Tables")
                    {
                        if (item.Rows.Count > 0)
                        {
                            IRfcStructure import = null;
                            IRfcTable table = myfun.GetTable(item.TableName);
                            foreach (DataRow item1 in item.Rows)
                            {
                                import = SapRfcRepository.GetStructureMetadata(myfun.Metadata[item.TableName].ValueMetadataAsTableMetadata.LineType.Name).CreateStructure();
                                foreach (DataColumn item2 in item.Columns)
                                {
                                    item2.Namespace = "String";
                                    if (!string.IsNullOrEmpty(item1[item2.ColumnName].ToString()))
                                    {
                                        import.SetValue(item2.ColumnName, item1[item2.ColumnName]);
                                    }
                                }
                                table.Insert(import);
                            }
                        }
                    }
                }
                Stopwatch = new System.Diagnostics.Stopwatch();
                CWaitForm b = new CWaitForm();
                b._wtVoid = 执行;
                b.ShowDialog();
                toolStripMenuItem1.Text = "SAP端执行时间：" + Stopwatch.Elapsed.ToString();
                int exrowindex = -1;
                //获取返回值
                foreach (DataRow item in dtEX.Rows)
                {
                    exrowindex++;
                    string flag = "";
                    if (item["Value"].ToString().Length >= 4)
                    {
                        flag = item["Value"].ToString().Substring(0, 4);
                    }
                    if (flag == "点击赋值")
                    {
                        if (dsRFC.Tables.Contains("EXT." + item["Code"].ToString()))
                        {
                            DataTable dt = new DataTable();
                            if (!dsRFC.Tables.Contains("EXT." + item["Code"].ToString() + "[R" + (exrowindex + 1).ToString() + ",C3]"))
                            {
                                dt = dsRFC.Tables["EXT." + item["Code"].ToString()].Clone();
                                dt.Namespace = "EXT." + item["Code"].ToString();
                                dt.TableName = "EXT." + item["Code"].ToString() + "[R" + (exrowindex + 1).ToString() + ",C3]";
                                dsRFC.Tables.Add(dt);
                            }
                            else
                            {
                                dt = dsRFC.Tables["EXT." + item["Code"].ToString() + "[R" + (exrowindex + 1).ToString() + ",C3]"];
                                dt.Clear();
                            }

                            IRfcTable irtb;
                            irtb = (IRfcTable)myfun.GetValue(item["Code"].ToString());
                            if (irtb.RowCount > 0)
                            {
                                for (int i = 0; i < irtb.RowCount; i++)
                                {
                                    DataRow dr = dt.NewRow();
                                    foreach (DataColumn item1 in dt.Columns)
                                    {
                                        item1.Namespace = "String";
                                        irtb.CurrentIndex = i;
                                        dr[item1.ColumnName] = irtb.CurrentRow[item1.ColumnName].GetValue();
                                    }
                                    dt.Rows.Add(dr);
                                }
                            }
                        }



                        //if (!dsRFC.Tables.Contains(linetablename))
                        //{
                        //    //不存在则创建
                        //    DataTable dt = new DataTable();
                        //    dt = dsRFC.Tables[linetabletypename].Clone();
                        //    dt.Namespace = linetabletypename;
                        //    dt.TableName = linetablename;
                        //    dsRFC.Tables.Add(dt);
                        //}
                    }
                    else
                    {
                        item["Value"] = myfun.GetValue(item["Code"].ToString());
                    }
                }
                //获取表结果，结构结果
                foreach (DataTable item in dsRFC.Tables)
                {
                    if (item.Namespace == "Tables")
                    {
                        IRfcTable irtb;
                        irtb = (IRfcTable)myfun.GetValue(item.TableName);
                        if (irtb.RowCount > 0)
                        {
                            item.Clear();
                            for (int i = 0; i < irtb.RowCount; i++)
                            {
                                DataRow dr = item.NewRow();
                                foreach (DataColumn item1 in item.Columns)
                                {
                                    item1.Namespace = "String";
                                    irtb.CurrentIndex = i;
                                    dr[item1.ColumnName] = irtb.CurrentRow[item1.ColumnName].GetValue();
                                }
                                item.Rows.Add(dr);
                            }
                        }
                    }
                    else if (item.Namespace == "EXS")
                    {
                        IRfcStructure irs;
                        String[] ssname = item.TableName.Split('.');
                        irs = (IRfcStructure)myfun.GetValue(ssname[1]);
                        if (irs.Count > 0)
                        {
                            item.Clear();
                            DataRow dr = item.NewRow();
                            foreach (DataColumn item1 in item.Columns)
                            {
                                if (item1.Namespace == "STRUCTURE")
                                {
                                    dr[item1.ColumnName.ToString()] = "点击赋值";
                                }
                                else if (item1.Namespace == "TABLE")
                                {
                                    dr[item1.ColumnName.ToString()] = "点击赋值";
                                }
                                else
                                {
                                    dr[item1.ColumnName.ToString()] = irs.GetValue(item1.ColumnName.ToString());
                                }
                            }
                            item.Rows.Add(dr);
                        }
                    }
                }
                string filepath = "";
                filepath = System.Environment.CurrentDirectory +
                        "\\RFCFunConfig\\" +
                        dsRFC.DataSetName.ToString() +
                        "\\" + DateTime.Now.Year.ToString() +
                        DateTime.Now.Month.ToString() +
                        DateTime.Now.Day.ToString() +
                        DateTime.Now.Hour.ToString() +
                        DateTime.Now.Minute.ToString() +
                        DateTime.Now.Second.ToString() +
                        DateTime.Now.Millisecond.ToString() +
                        "\\";
                if (!File.Exists(filepath))
                {
                    FileInfo oInfo = new FileInfo(filepath);
                    if (!Directory.Exists(oInfo.DirectoryName))
                    {
                        Directory.CreateDirectory(oInfo.DirectoryName);
                    }
                }
                dsRFC.WriteXmlSchema(filepath + "\\JieGou.XML");
                dsRFC.WriteXml(filepath + "\\ShuZhi.XML");
                MessageBox.Show("执行成功,结果保存在" + filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>读取执行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 读取执行记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件夹";
                dialog.SelectedPath = System.Environment.CurrentDirectory + "\\RFCFunConfig\\";
                string foldPath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foldPath = dialog.SelectedPath;
                }
                else
                {
                    return;
                }
                if (!File.Exists(foldPath + "\\JieGou.XML"))
                {
                    MessageBox.Show("文件夹中未找到JieGou.XML文件");
                    return;
                }

                if (!File.Exists(foldPath + "\\ShuZhi.XML"))
                {
                    MessageBox.Show("文件夹中未找到ShuZhi.XML文件");
                    return;
                }
                DataSet ds2 = new DataSet();
                ds2.ReadXmlSchema(foldPath + "\\JieGou.XML");
                ds2.ReadXml(foldPath + "\\ShuZhi.XML");
                dsRFC = new DataSet();
                dsRFC = ds2;
                FillForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 批量执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void 执行()
        {
            //执行函数
            Stopwatch.Start();
            myfun.Invoke(SapRfcDestination);
            Stopwatch.Stop();
        }

        private void 更改配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginConfig a = new LoginConfig();
            if (a.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("更改配置成功，请重新登陆系统");
                Application.Exit();
            }
        }

        public void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //RfcConfigParameters parms = new RfcConfigParameters();
                //使用para必须把app.config排除在项目外

                SapRfcDestination = RfcDestinationManager.GetDestination(parms);//SAP链接变量  
                //SapRfcDestination = RfcDestinationManager.GetDestination("RL700");//SAP链接变量
                SapRfcRepository = SapRfcDestination.Repository;

                this.Text = "RFC测试工具";

                this.Text += "  IP: " + parms["USER"].ToString();
                this.Text += "  客户端号: " + parms["CLIENT"].ToString();
                this.Text += "  登陆语言: " + parms["LANG"].ToString();
                this.Text += "  登陆用户: " + parms["USER"].ToString();
                this.Text += "  系统编号: " + parms["SYSNR"].ToString();

                //f.parms.Add("USER", tbDengLuZhangHao.Text.ToString());
                //f.parms.Add("CLIENT", tbKeHuDuanHao.Text.ToString());
                //f.parms.Add("LANG", tbDengLuYuYan.Text.ToString());
                //f.parms.Add("ASHOST", tbIP.Text.ToString());
                //f.parms.Add("SYSNR", tbXiTongHao.Text.ToString());
                //f.parms.Add("MAX_POOL_SIZE", "10");
                //f.parms.Add("IDLE_TIMEOUT", "10");
                //f.parms.Add("NAME", "AAA");
                //f.parms.Add("SAPROUTER", tbLuYou.Text.ToString());

                //XmlDocument xmlDoc = new XmlDocument();
                //string file = (new FileInfo(Assembly.GetEntryAssembly().Location)).Name + ".config";
                //DataSet ds = new DataSet();
                //ds.ReadXml(file);
                //this.Text = "RFC测试工具";
                //foreach (DataRow item in ds.Tables["add"].Rows)
                //{
                //    this.Text += "  IP: " + item["ASHOST"].ToString();
                //    this.Text += "  客户端号: " + item["CLIENT"].ToString();
                //    this.Text += "  登陆语言: " + item["LANG"].ToString();
                //    this.Text += "  登陆用户: " + item["USER"].ToString();
                //    this.Text += "  系统编号: " + item["SYSNR"].ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据参数创建内表
        /// </summary>
        /// <param name="rfcpm"></param>
        /// <param name="ceng"></param>
        /// <param name="tname"></param>
        /// <returns></returns>
        public DataTable createStructureByParameter(RfcParameterMetadata rfcpm, string ceng, string tname)
        {
            DataTable dt = new DataTable();
            dt.Namespace = ceng;
            dt.TableName = ceng + "." + tname;
            if (rfcpm.DataType == RfcDataType.STRUCTURE)
            {
                for (int j = 0; j < rfcpm.ValueMetadataAsStructureMetadata.FieldCount; j++)
                {
                    DataColumn dc = new DataColumn();
                    if (rfcpm.ValueMetadataAsStructureMetadata[j].DataType == RfcDataType.TABLE)
                    {
                        dc.Namespace = "TABLE";
                        dc.ColumnName = rfcpm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcpm.ValueMetadataAsStructureMetadata[j], dt.TableName);
                    }
                    else if (rfcpm.ValueMetadataAsStructureMetadata[j].DataType == RfcDataType.STRUCTURE)
                    {
                        dc.Namespace = "STRUCTURE";
                        dc.ColumnName = rfcpm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcpm.ValueMetadataAsStructureMetadata[j], dt.TableName);
                    }
                    else
                    {
                        dc.Namespace = "STRING";
                        dc.ColumnName = rfcpm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                    }
                }
            }
            else if (rfcpm.DataType == RfcDataType.TABLE)
            {
                for (int j = 0; j < rfcpm.ValueMetadataAsTableMetadata.LineType.FieldCount; j++)
                {
                    DataColumn dc = new DataColumn();
                    if (rfcpm.ValueMetadataAsTableMetadata[j].DataType == RfcDataType.TABLE)
                    {
                        dc.Namespace = "TABLE";
                        dc.ColumnName = rfcpm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcpm.ValueMetadataAsTableMetadata[j], dt.TableName);
                    }
                    else if (rfcpm.ValueMetadataAsTableMetadata[j].DataType == RfcDataType.STRUCTURE)
                    {
                        dc.Namespace = "STRUCTURE";
                        dc.ColumnName = rfcpm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcpm.ValueMetadataAsTableMetadata[j], dt.TableName);
                    }
                    else
                    {
                        dc.Namespace = "STRING";
                        dc.ColumnName = rfcpm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcpm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                    }
                }
            }
            else
            {

            }
            dsRFC.Tables.Add(dt);
            return dt;
        }

        public DataTable createStructureByField(RfcFieldMetadata rfcspm, string ceng)
        {

            DataTable dt = new DataTable();
            dt.Namespace = ceng;
            dt.TableName = ceng + "." + rfcspm.Name;


            if (rfcspm.DataType == RfcDataType.STRUCTURE)
            {
                for (int j = 0; j < rfcspm.ValueMetadataAsStructureMetadata.FieldCount; j++)
                {
                    DataColumn dc = new DataColumn();
                    if (rfcspm.ValueMetadataAsStructureMetadata[j].DataType == RfcDataType.TABLE)
                    {
                        dc.Namespace = "TABLE";
                        dc.ColumnName = rfcspm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcspm.ValueMetadataAsStructureMetadata[j], dt.TableName);
                    }
                    else if (rfcspm.ValueMetadataAsStructureMetadata[j].DataType == RfcDataType.STRUCTURE)
                    {
                        dc.Namespace = "STRUCTURE";
                        dc.ColumnName = rfcspm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcspm.ValueMetadataAsStructureMetadata[j], dt.TableName);
                    }
                    else
                    {
                        dc.Namespace = "STRING";
                        dc.ColumnName = rfcspm.ValueMetadataAsStructureMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsStructureMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                    }
                }
            }
            else if (rfcspm.DataType == RfcDataType.TABLE)
            {
                for (int j = 0; j < rfcspm.ValueMetadataAsTableMetadata.LineType.FieldCount; j++)
                {
                    DataColumn dc = new DataColumn();
                    if (rfcspm.ValueMetadataAsTableMetadata[j].DataType == RfcDataType.TABLE)
                    {
                        dc.Namespace = "TABLE";
                        dc.ColumnName = rfcspm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcspm.ValueMetadataAsTableMetadata[j], dt.TableName);
                    }
                    else if (rfcspm.ValueMetadataAsTableMetadata[j].DataType == RfcDataType.STRUCTURE)
                    {
                        dc.Namespace = "STRUCTURE";
                        dc.ColumnName = rfcspm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                        createStructureByField(rfcspm.ValueMetadataAsStructureMetadata[j], dt.TableName);
                    }
                    else
                    {
                        dc.Namespace = "STRING";
                        dc.ColumnName = rfcspm.ValueMetadataAsTableMetadata[j].Name;
                        dc.Caption = rfcspm.ValueMetadataAsTableMetadata[j].Documentation;
                        dt.Columns.Add(dc);
                    }
                }
            }
            dsRFC.Tables.Add(dt);
            return dt;
        }

        /// <summary>
        /// 给函数深层表结构赋值
        /// </summary>
        /// <param name="IRFCF"></param>
        /// <param name="stype"></param>
        /// <param name="ceng"></param>
        /// <param name="tname"></param>
        /// <returns></returns>
        public DataTable setStructureByFun(IRfcFunction IRFCF, string stype, string ceng, string tname)
        {
            string linetabletypename = "";//表结构名称 
            string linetablename = "";//表名称
            int rowindex = 0;
            int columnindex = 0;
            int r = 0;
            int c = 0;
            string sname = "";
            DataTable dt = new DataTable();
            String[] tnames = tname.Split('.');

            String[] snames = tnames[tnames.Count() - 1].Split('[', ']', ',');
            sname = snames[0].ToString();
            //r = int.Parse(snames[1].Substring(1, snames[1].Length - 1).ToString());
            //c = int.Parse(snames[2].Substring(1, snames[2].Length - 1).ToString());
            if (stype == "TABLE")
            {
                IRfcTable table = IRFCF.GetTable(sname);
                dt = dsRFC.Tables[ceng];
                foreach (DataRow item1 in dt.Rows)
                {
                    IRfcStructure structure = null;
                    structure = SapRfcRepository.GetStructureMetadata(table.Metadata.LineType.Name).CreateStructure();
                    columnindex = 0;
                    foreach (DataColumn item2 in dt.Columns)
                    {
                        if (item2.Namespace == "TABLE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else if (item2.Namespace == "STRUCTURE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else
                        {
                            structure.SetValue(item2.ColumnName, item1[item2.ColumnName]);
                        }
                        columnindex++;
                    }
                    table.Insert(structure);
                    rowindex++;
                }
            }
            else if (stype == "STRUCTURE")
            {
                IRfcStructure structure = IRFCF.GetStructure(tnames[tnames.Count() - 1]);
                dt = dsRFC.Tables[tname];
                foreach (DataRow item1 in dt.Rows)
                {
                    columnindex = 0;
                    foreach (DataColumn item2 in dt.Columns)
                    {
                        if (item2.Namespace == "TABLE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else if (item2.Namespace == "STRUCTURE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else
                        {
                            structure.SetValue(item2.ColumnName, item1[item2.ColumnName]);
                        }
                        columnindex++;
                    }
                    rowindex++;
                }
            }
            return dt;
        }

        /// <summary>
        /// 给函数的深层结构赋值（行赋值）
        /// </summary>
        /// <param name="IRFCS"></param>
        /// <param name="stype"></param>
        /// <param name="ceng"></param>
        /// <param name="tname"></param>
        /// <returns></returns>
        public DataTable setStructureByStru(IRfcStructure IRFCS, string stype, string ceng, string tname)
        {
            string linetabletypename = "";//表结构名称 
            string linetablename = "";//表名称
            int rowindex = 0;
            int columnindex = 0;
            int r = 0;
            int c = 0;
            string sname = "";
            String[] tnames = tname.Split('.');
            DataTable dt = new DataTable();
            if (stype == "TABLE")
            {
                dt = dsRFC.Tables[tname];
                String[] snames = tnames[tnames.Count() - 1].Split('[', ']', ',');
                sname = snames[0].ToString();
                r = int.Parse(snames[1].Substring(1, snames[1].Length - 1).ToString());
                c = int.Parse(snames[2].Substring(1, snames[2].Length - 1).ToString());
                IRfcStructure import = null;
                IRfcTable table = IRFCS.GetTable(sname);
                foreach (DataRow item1 in dt.Rows)
                {
                    import = SapRfcRepository.GetStructureMetadata(table.Metadata.LineType.Name).CreateStructure();
                    columnindex = 0;
                    foreach (DataColumn item2 in dt.Columns)
                    {
                        if (item2.Namespace == "TABLE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(import, item2.Namespace, linetabletypename, linetablename);
                        }
                        else if (item2.Namespace == "STRUCTURE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(import, item2.Namespace, linetabletypename, linetablename);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item1[item2.ColumnName].ToString()))
                            {
                                import.SetValue(item2.ColumnName, item1[item2.ColumnName]);
                            }
                        }
                        columnindex++;
                    }
                    table.Insert(import);
                    rowindex++;
                }
            }
            else if (stype == "STRUCTURE")
            {
                dt = dsRFC.Tables[tname];
                String[] snames = tnames[tnames.Count() - 1].Split('[', ']', ',');
                sname = snames[0].ToString();
                r = int.Parse(snames[1].Substring(1, snames[1].Length - 1).ToString());
                c = int.Parse(snames[2].Substring(1, snames[2].Length - 1).ToString());

                IRfcStructure structure = IRFCS.GetStructure(sname);
                foreach (DataRow item1 in dt.Rows)
                {
                    columnindex = 0;
                    foreach (DataColumn item2 in dt.Columns)
                    {
                        if (item2.Namespace == "TABLE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else if (item2.Namespace == "STRUCTURE")
                        {
                            linetabletypename = tname + "." + item2.ColumnName.ToString();
                            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
                            setStructureByStru(structure, item2.Namespace, linetabletypename, linetablename);
                        }
                        else
                        {
                            structure.SetValue(item2.ColumnName, item1[item2.ColumnName]);
                        }
                        columnindex++;
                    }
                    rowindex++;
                }
            }
            else
            {
            }
            return dt;
        }



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dsRFC.Tables.Count != 0)
            {
                DialogResult dr;
                dr = MessageBox.Show("选择是，则填写内容将被清空", "是否重置函数结构", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ttbFunName.Text = ttbFunName.Text.ToUpper();
                    remind.Remind(ttbFunName.Text.ToString());
                    登陆ToolStripMenuItem_Click(null, null);
                    读取结构ToolStripMenuItem_Click(null, null);
                    InitTextBoxRemind();
                }
            }
            else
            {
                ttbFunName.Text = ttbFunName.Text.ToUpper();
                remind.Remind(ttbFunName.Text.ToString());
                登陆ToolStripMenuItem_Click(null, null);
                读取结构ToolStripMenuItem_Click(null, null);
                InitTextBoxRemind();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            函数执行ToolStripMenuItem_Click(null, null);
        }

        private void dgIM_ButtonSelectClick(CDataGridView cdgv, int rowindex, int columnindex)
        {
            string linetabletypename = "";//表结构名称 
            string linetablename = "";//表名称
            if (((DataTable)cdgv.DataSource).TableName.ToString() == "IM")
            {
                linetabletypename = ((DataTable)cdgv.DataSource).TableName.ToString() + "T" + "." + ((DataTable)cdgv.DataSource).Rows[rowindex]["Code"].ToString();
            }
            else
            {
                linetabletypename = ((DataTable)cdgv.DataSource).TableName.ToString() + "." + ((DataTable)cdgv.DataSource).Columns[columnindex].ColumnName.ToString();
            }
            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
            if (!dsRFC.Tables.Contains(linetablename))
            {
                //不存在则创建
                DataTable dt = new DataTable();
                dt = dsRFC.Tables[linetabletypename].Clone();
                dt.Namespace = linetabletypename;
                dt.TableName = linetablename;
                dsRFC.Tables.Add(dt);
            }
            FullTable a = new FullTable();
            a.Text = linetablename;
            a.ds = dsRFC;
            a.dt = dsRFC.Tables[linetablename];
            a.ShowDialog();
            cdgv.Rows[rowindex].Cells[columnindex].Value = "点击赋值:" + a.lines.ToString() + "行";
        }


        private void dgEX_ButtonSelectClick(CDataGridView cdgv, int rowindex, int columnindex)
        {
            string linetabletypename = "";//表结构名称 
            string linetablename = "";//表名称
            if (((DataTable)cdgv.DataSource).TableName.ToString() == "EX")
            {
                linetabletypename = ((DataTable)cdgv.DataSource).TableName.ToString() + "T" + "." + ((DataTable)cdgv.DataSource).Rows[rowindex]["Code"].ToString();
            }
            else
            {
                linetabletypename = ((DataTable)cdgv.DataSource).TableName.ToString() + "." + ((DataTable)cdgv.DataSource).Columns[columnindex].ColumnName.ToString();
            }
            linetablename = linetabletypename + "[R" + (rowindex + 1).ToString() + ",C" + (columnindex + 1).ToString() + "]";
            if (!dsRFC.Tables.Contains(linetablename))
            {
                //不存在则创建
                DataTable dt = new DataTable();
                dt = dsRFC.Tables[linetabletypename].Clone();
                dt.Namespace = linetabletypename;
                dt.TableName = linetablename;
                dsRFC.Tables.Add(dt);
            }
            FullTable a = new FullTable();
            a.Text = linetablename;
            a.ds = dsRFC;
            a.dt = dsRFC.Tables[linetablename];
            a.ShowDialog();
            cdgv.Rows[rowindex].Cells[columnindex].Value = "点击赋值:" + a.lines.ToString() + "行";
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReSet();
        }
    }
}
