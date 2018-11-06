namespace JoyStickTest
{
    partial class ucJoyStick
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.grpJoyStick = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJoyStickPos = new System.Windows.Forms.TextBox();
            this.picCircle = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJoyStickButton = new System.Windows.Forms.TextBox();
            this.lblClickCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpJoyStick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // grpJoyStick
            // 
            this.grpJoyStick.Controls.Add(this.label1);
            this.grpJoyStick.Controls.Add(this.txtJoyStickPos);
            this.grpJoyStick.Controls.Add(this.picCircle);
            this.grpJoyStick.Controls.Add(this.label4);
            this.grpJoyStick.Controls.Add(this.txtJoyStickButton);
            this.grpJoyStick.Controls.Add(this.lblClickCount);
            this.grpJoyStick.Controls.Add(this.label5);
            this.grpJoyStick.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpJoyStick.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpJoyStick.Location = new System.Drawing.Point(0, 0);
            this.grpJoyStick.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpJoyStick.Name = "grpJoyStick";
            this.grpJoyStick.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpJoyStick.Size = new System.Drawing.Size(228, 176);
            this.grpJoyStick.TabIndex = 36;
            this.grpJoyStick.TabStop = false;
            this.grpJoyStick.Text = "JoyStick";
            this.grpJoyStick.Paint += new System.Windows.Forms.PaintEventHandler(this.grpJoyStick_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "油门:";
            // 
            // txtJoyStickPos
            // 
            this.txtJoyStickPos.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJoyStickPos.Location = new System.Drawing.Point(54, 79);
            this.txtJoyStickPos.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtJoyStickPos.Multiline = true;
            this.txtJoyStickPos.Name = "txtJoyStickPos";
            this.txtJoyStickPos.ReadOnly = true;
            this.txtJoyStickPos.Size = new System.Drawing.Size(154, 83);
            this.txtJoyStickPos.TabIndex = 28;
            // 
            // picCircle
            // 
            this.picCircle.Location = new System.Drawing.Point(140, 30);
            this.picCircle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picCircle.Name = "picCircle";
            this.picCircle.Size = new System.Drawing.Size(29, 28);
            this.picCircle.TabIndex = 34;
            this.picCircle.TabStop = false;
            this.picCircle.Paint += new System.Windows.Forms.PaintEventHandler(this.picCircle_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "位置:";
            // 
            // txtJoyStickButton
            // 
            this.txtJoyStickButton.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJoyStickButton.Location = new System.Drawing.Point(54, 31);
            this.txtJoyStickButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtJoyStickButton.Name = "txtJoyStickButton";
            this.txtJoyStickButton.ReadOnly = true;
            this.txtJoyStickButton.Size = new System.Drawing.Size(77, 29);
            this.txtJoyStickButton.TabIndex = 31;
            // 
            // lblClickCount
            // 
            this.lblClickCount.AutoSize = true;
            this.lblClickCount.Location = new System.Drawing.Point(166, 62);
            this.lblClickCount.Name = "lblClickCount";
            this.lblClickCount.Size = new System.Drawing.Size(0, 16);
            this.lblClickCount.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 249);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 32;
            this.label5.Text = "按钮:";
            // 
            // ucJoyStick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpJoyStick);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucJoyStick";
            this.Size = new System.Drawing.Size(228, 176);
            this.Load += new System.EventHandler(this.ucJoyStick_Load);
            this.grpJoyStick.ResumeLayout(false);
            this.grpJoyStick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCircle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpJoyStick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJoyStickPos;
        private System.Windows.Forms.PictureBox picCircle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJoyStickButton;
        private System.Windows.Forms.Label lblClickCount;
        private System.Windows.Forms.Label label5;
    }
}
