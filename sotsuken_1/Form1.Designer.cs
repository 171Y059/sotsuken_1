namespace sotsuken_1
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonPCTime = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSuiminTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPCTime
            // 
            this.buttonPCTime.Location = new System.Drawing.Point(91, 68);
            this.buttonPCTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonPCTime.Name = "buttonPCTime";
            this.buttonPCTime.Size = new System.Drawing.Size(120, 29);
            this.buttonPCTime.TabIndex = 1;
            this.buttonPCTime.Text = "buttonPCTime";
            this.buttonPCTime.UseVisualStyleBackColor = true;
            this.buttonPCTime.Click += new System.EventHandler(this.buttonPCTime_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(106, 187);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(90, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSuiminTime
            // 
            this.buttonSuiminTime.Location = new System.Drawing.Point(91, 114);
            this.buttonSuiminTime.Name = "buttonSuiminTime";
            this.buttonSuiminTime.Size = new System.Drawing.Size(120, 29);
            this.buttonSuiminTime.TabIndex = 2;
            this.buttonSuiminTime.Text = "睡眠時間を計算";
            this.buttonSuiminTime.UseVisualStyleBackColor = true;
            this.buttonSuiminTime.Click += new System.EventHandler(this.buttonSuiminTime_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 240);
            this.Controls.Add(this.buttonSuiminTime);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonPCTime);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPCTime;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSuiminTime;
    }
}

