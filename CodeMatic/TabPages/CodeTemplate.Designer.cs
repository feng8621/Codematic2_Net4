﻿using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic
{
    partial class CodeTemplate
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeTemplate));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTemplate = new LTP.TextEditor.TextEditorControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHidelist = new System.Windows.Forms.Button();
            this.btn_Run = new System.Windows.Forms.Button();
            this.list_KeyField = new System.Windows.Forms.ListBox();
            this.btn_SetKey = new System.Windows.Forms.Button();
            this.btn_SelI = new System.Windows.Forms.Button();
            this.btn_SelAll = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            //this.txtCode = new LTP.TextEditor.TextEditorControl();
            this.imglistView = new System.Windows.Forms.ImageList(this.components);

            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();


            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            menu_Copy,
            menu_Save,
            menu_SaveAs
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);

            // 
            // menu1ToolStripMenuItem
            // 
            this.menu_Copy.Name = "menu_Copy";
            this.menu_Copy.Size = new System.Drawing.Size(152, 22);
            this.menu_Copy.Text = "复制(&C)";
            this.menu_Copy.Click += new System.EventHandler(this.menu_Copy_Click);

            // 
            // menu1ToolStripMenuItem
            // 
            this.menu_Save.Name = "menu_Save";
            this.menu_Save.Size = new System.Drawing.Size(152, 22);
            this.menu_Save.Text = "保存(&S)";
            this.menu_Save.Click += new System.EventHandler(this.menu_Save_Click);


            // 
            // menu1ToolStripMenuItem
            // 
            this.menu_SaveAs.Name = "menu_Save";
            this.menu_SaveAs.Size = new System.Drawing.Size(152, 22);
            this.menu_SaveAs.Text = "另存为";
            this.menu_SaveAs.Click += new System.EventHandler(this.menu_SaveAs_Click);



            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 554);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.ImageList = this.imglistView;
            this.tabControl1.ItemSize = new System.Drawing.Size(58, 19);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(860, 554);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.ImageIndex = 1;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage1.Size = new System.Drawing.Size(852, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "模版";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 188);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 335);
            this.panel1.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTemplate);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(831, 283);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模版";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplate.Font = new System.Drawing.Font("新宋体", 9F);
            this.txtTemplate.Location = new System.Drawing.Point(4, 22);
            this.txtTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(823, 25);
            this.txtTemplate.TabIndex = 8;
            this.txtTemplate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtTemplate_MouseUp);
            this.txtTemplate.ContextMenuStrip = this.contextMenuStrip1;



            this.txtTemplate.IsIconBarVisible = false;
            this.txtTemplate.ShowInvalidLines = false;
            this.txtTemplate.ShowSpaces = false;
            this.txtTemplate.ShowTabs = false;
            this.txtTemplate.ShowEOLMarkers = false;
            this.txtTemplate.ShowVRuler = false;
            this.txtTemplate.Language = TextEditorControlBase.Languages.HTML;
            this.txtTemplate.Encoding = System.Text.Encoding.Default;



            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnHidelist);
            this.groupBox2.Controls.Add(this.btn_Run);
            this.groupBox2.Controls.Add(this.list_KeyField);
            this.groupBox2.Controls.Add(this.btn_SetKey);
            this.groupBox2.Controls.Add(this.btn_SelI);
            this.groupBox2.Controls.Add(this.btn_SelAll);
            this.groupBox2.Location = new System.Drawing.Point(4, -6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(831, 55);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnHidelist
            // 
            this.btnHidelist.Location = new System.Drawing.Point(224, 19);
            this.btnHidelist.Name = "btnHidelist";
            this.btnHidelist.Size = new System.Drawing.Size(80, 29);
            this.btnHidelist.TabIndex = 14;
            this.btnHidelist.Text = "隐藏表格";
            this.btnHidelist.UseVisualStyleBackColor = true;
            this.btnHidelist.Click += new System.EventHandler(this.btnHidelist_Click);
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(710, 19);
            this.btn_Run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(100, 29);
            this.btn_Run.TabIndex = 13;
            this.btn_Run.Text = "生成代码";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // list_KeyField
            // 
            this.list_KeyField.FormattingEnabled = true;
            this.list_KeyField.ItemHeight = 15;
            this.list_KeyField.Location = new System.Drawing.Point(515, 16);
            this.list_KeyField.Margin = new System.Windows.Forms.Padding(4);
            this.list_KeyField.Name = "list_KeyField";
            this.list_KeyField.Size = new System.Drawing.Size(171, 34);
            this.list_KeyField.TabIndex = 1;
            // 
            // btn_SetKey
            // 
            this.btn_SetKey.Location = new System.Drawing.Point(363, 19);
            this.btn_SetKey.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SetKey.Name = "btn_SetKey";
            this.btn_SetKey.Size = new System.Drawing.Size(144, 29);
            this.btn_SetKey.TabIndex = 0;
            this.btn_SetKey.Text = "主键(条件)字段";
            this.btn_SetKey.UseVisualStyleBackColor = true;
            this.btn_SetKey.Click += new System.EventHandler(this.btn_SetKey_Click);
            // 
            // btn_SelI
            // 
            this.btn_SelI.Location = new System.Drawing.Point(97, 19);
            this.btn_SelI.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SelI.Name = "btn_SelI";
            this.btn_SelI.Size = new System.Drawing.Size(60, 29);
            this.btn_SelI.TabIndex = 0;
            this.btn_SelI.Text = "反选";
            this.btn_SelI.UseVisualStyleBackColor = true;
            this.btn_SelI.Click += new System.EventHandler(this.btn_SelI_Click);
            // 
            // btn_SelAll
            // 
            this.btn_SelAll.Location = new System.Drawing.Point(21, 19);
            this.btn_SelAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SelAll.Name = "btn_SelAll";
            this.btn_SelAll.Size = new System.Drawing.Size(60, 29);
            this.btn_SelAll.TabIndex = 0;
            this.btn_SelAll.Text = "全选";
            this.btn_SelAll.UseVisualStyleBackColor = true;
            this.btn_SelAll.Click += new System.EventHandler(this.btn_SelAll_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(4, 184);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(844, 4);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Location = new System.Drawing.Point(4, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(844, 180);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            //this.tabPage2.Controls.Add(this.txtCode);
            this.tabPage2.ImageIndex = 2;
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(852, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代码";
            this.tabPage2.UseVisualStyleBackColor = true;
            //// 
            //// txtCode
            //// 
            //this.txtCode.ContextMenuStrip = this.contextMenuStrip1;
            //this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.txtCode.Font = new System.Drawing.Font("新宋体", 9F);
            //this.txtCode.Location = new System.Drawing.Point(4, 4);
            //this.txtCode.Margin = new System.Windows.Forms.Padding(4);
            //this.txtCode.Name = "txtCode";
            //this.txtCode.Size = new System.Drawing.Size(844, 25);
            //this.txtCode.TabIndex = 0;



            //this.txtCode.IsIconBarVisible = false;
            //this.txtCode.ShowInvalidLines = false;
            //this.txtCode.ShowSpaces = false;
            //this.txtCode.ShowTabs = false;
            //this.txtCode.ShowEOLMarkers = false;
            //this.txtCode.ShowVRuler = false;
            //this.txtCode.Language = TextEditorControlBase.Languages.CSHARP;
            //this.txtCode.Encoding = System.Text.Encoding.Default;
            // 
            // imglistView
            // 
            this.imglistView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistView.ImageStream")));
            this.imglistView.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistView.Images.SetKeyName(0, "fild2.gif");
            this.imglistView.Images.SetKeyName(1, "xml.ico");
            this.imglistView.Images.SetKeyName(2, "file.ico");
                    
        
            
            
            
            // 
            // CodeTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 576);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CodeTemplate";
            this.Text = "模版代码生成";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);            
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;        
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox list_KeyField;
        private System.Windows.Forms.Button btn_SetKey;
        private System.Windows.Forms.Button btn_SelI;
        private System.Windows.Forms.Button btn_SelAll;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.ImageList imglistView;
        private LTP.TextEditor.TextEditorControl txtTemplate;
        //private LTP.TextEditor.TextEditorControl txtCode;//LTP.TextEditor.TextEditorControl
        private System.Windows.Forms.Button btnHidelist;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Save;
        private System.Windows.Forms.ToolStripMenuItem menu_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem menu_Copy;
  
    }
}