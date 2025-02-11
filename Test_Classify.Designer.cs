namespace TestWinform
{
    partial class Test_Classify
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiButton_Path = new Sunny.UI.UIButton();
            this.TextBox_Type = new Sunny.UI.UITextBox();
            this.uiButton_Classify = new Sunny.UI.UIButton();
            this.label_Classify = new Sunny.UI.UILabel();
            this.hWindow_img = new HalconDotNet.HWindowControl();
            this.SuspendLayout();
            // 
            // uiButton_Path
            // 
            this.uiButton_Path.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_Path.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_Path.Location = new System.Drawing.Point(1030, 81);
            this.uiButton_Path.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Path.Name = "uiButton_Path";
            this.uiButton_Path.Size = new System.Drawing.Size(169, 85);
            this.uiButton_Path.TabIndex = 0;
            this.uiButton_Path.Text = "载入图片路径";
            this.uiButton_Path.Click += new System.EventHandler(this.uiButton_Path_Click);
            // 
            // TextBox_Type
            // 
            this.TextBox_Type.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBox_Type.FillColor = System.Drawing.Color.White;
            this.TextBox_Type.FocusedSelectAll = true;
            this.TextBox_Type.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.TextBox_Type.Location = new System.Drawing.Point(998, 467);
            this.TextBox_Type.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBox_Type.Maximum = 2147483647D;
            this.TextBox_Type.Minimum = -2147483648D;
            this.TextBox_Type.MinimumSize = new System.Drawing.Size(1, 1);
            this.TextBox_Type.Name = "TextBox_Type";
            this.TextBox_Type.Padding = new System.Windows.Forms.Padding(5);
            this.TextBox_Type.ReadOnly = true;
            this.TextBox_Type.Size = new System.Drawing.Size(309, 39);
            this.TextBox_Type.TabIndex = 2;
            // 
            // uiButton_Classify
            // 
            this.uiButton_Classify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_Classify.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton_Classify.Location = new System.Drawing.Point(1030, 246);
            this.uiButton_Classify.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Classify.Name = "uiButton_Classify";
            this.uiButton_Classify.Size = new System.Drawing.Size(169, 78);
            this.uiButton_Classify.TabIndex = 3;
            this.uiButton_Classify.Text = "识别下一张";
            this.uiButton_Classify.Click += new System.EventHandler(this.uiButton_Classify_Click);
            // 
            // label_Classify
            // 
            this.label_Classify.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_Classify.Location = new System.Drawing.Point(834, 464);
            this.label_Classify.Name = "label_Classify";
            this.label_Classify.Size = new System.Drawing.Size(211, 42);
            this.label_Classify.TabIndex = 4;
            this.label_Classify.Text = "图片分类类型：";
            this.label_Classify.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hWindow_img
            // 
            this.hWindow_img.BackColor = System.Drawing.Color.Black;
            this.hWindow_img.BorderColor = System.Drawing.Color.Black;
            this.hWindow_img.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindow_img.Location = new System.Drawing.Point(12, 12);
            this.hWindow_img.Name = "hWindow_img";
            this.hWindow_img.Size = new System.Drawing.Size(816, 874);
            this.hWindow_img.TabIndex = 5;
            this.hWindow_img.WindowSize = new System.Drawing.Size(816, 874);
            // 
            // Test_Classify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 944);
            this.Controls.Add(this.hWindow_img);
            this.Controls.Add(this.uiButton_Classify);
            this.Controls.Add(this.TextBox_Type);
            this.Controls.Add(this.uiButton_Path);
            this.Controls.Add(this.label_Classify);
            this.Name = "Test_Classify";
            this.Text = "图片分类处理器";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton uiButton_Path;
        public Sunny.UI.UITextBox TextBox_Type;
        private Sunny.UI.UIButton uiButton_Classify;
        private Sunny.UI.UILabel label_Classify;
        public HalconDotNet.HWindowControl hWindow_img;
    }
}

