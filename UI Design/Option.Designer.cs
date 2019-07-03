namespace UI_Design
{
    partial class Option
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnThemeBlack = new System.Windows.Forms.Button();
            this.btnThemeCap = new System.Windows.Forms.Button();
            this.btnThemeBlue = new System.Windows.Forms.Button();
            this.btnThemeBasy = new System.Windows.Forms.Button();
            this.option1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_themeColor = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.option1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(207, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "테마 색상";
            // 
            // btnThemeBlack
            // 
            this.btnThemeBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnThemeBlack.Location = new System.Drawing.Point(443, 119);
            this.btnThemeBlack.Name = "btnThemeBlack";
            this.btnThemeBlack.Size = new System.Drawing.Size(40, 40);
            this.btnThemeBlack.TabIndex = 1;
            this.btnThemeBlack.UseVisualStyleBackColor = false;
            this.btnThemeBlack.Click += new System.EventHandler(this.btnThemeBlack_Click);
            // 
            // btnThemeCap
            // 
            this.btnThemeCap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnThemeCap.Location = new System.Drawing.Point(508, 119);
            this.btnThemeCap.Name = "btnThemeCap";
            this.btnThemeCap.Size = new System.Drawing.Size(40, 40);
            this.btnThemeCap.TabIndex = 2;
            this.btnThemeCap.UseVisualStyleBackColor = false;
            // 
            // btnThemeBlue
            // 
            this.btnThemeBlue.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThemeBlue.Location = new System.Drawing.Point(574, 119);
            this.btnThemeBlue.Name = "btnThemeBlue";
            this.btnThemeBlue.Size = new System.Drawing.Size(40, 40);
            this.btnThemeBlue.TabIndex = 3;
            this.btnThemeBlue.UseVisualStyleBackColor = false;
            // 
            // btnThemeBasy
            // 
            this.btnThemeBasy.BackColor = System.Drawing.SystemColors.Info;
            this.btnThemeBasy.Location = new System.Drawing.Point(641, 119);
            this.btnThemeBasy.Name = "btnThemeBasy";
            this.btnThemeBasy.Size = new System.Drawing.Size(40, 40);
            this.btnThemeBasy.TabIndex = 4;
            this.btnThemeBasy.UseVisualStyleBackColor = false;
            // 
            // option1
            // 
            this.option1.Controls.Add(this.button1);
            this.option1.Controls.Add(this.lbl_themeColor);
            this.option1.Controls.Add(this.button2);
            this.option1.Controls.Add(this.button3);
            this.option1.Controls.Add(this.button4);
            this.option1.Location = new System.Drawing.Point(33, -16);
            this.option1.Margin = new System.Windows.Forms.Padding(0);
            this.option1.Name = "option1";
            this.option1.Padding = new System.Windows.Forms.Padding(0);
            this.option1.Size = new System.Drawing.Size(901, 653);
            this.option1.TabIndex = 5;
            this.option1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(657, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lbl_themeColor
            // 
            this.lbl_themeColor.AutoSize = true;
            this.lbl_themeColor.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_themeColor.ForeColor = System.Drawing.Color.White;
            this.lbl_themeColor.Location = new System.Drawing.Point(220, 69);
            this.lbl_themeColor.Name = "lbl_themeColor";
            this.lbl_themeColor.Size = new System.Drawing.Size(129, 27);
            this.lbl_themeColor.TabIndex = 5;
            this.lbl_themeColor.Text = "테마 색상";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Blue;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(590, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(459, 63);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 40);
            this.button3.TabIndex = 6;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(524, 63);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 7;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.option1);
            this.Controls.Add(this.btnThemeBasy);
            this.Controls.Add(this.btnThemeBlue);
            this.Controls.Add(this.btnThemeCap);
            this.Controls.Add(this.btnThemeBlack);
            this.Controls.Add(this.label1);
            this.Name = "Option";
            this.Size = new System.Drawing.Size(967, 620);
            this.option1.ResumeLayout(false);
            this.option1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemeBlack;
        private System.Windows.Forms.Button btnThemeCap;
        private System.Windows.Forms.Button btnThemeBlue;
        private System.Windows.Forms.Button btnThemeBasy;
        private System.Windows.Forms.GroupBox option1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_themeColor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
