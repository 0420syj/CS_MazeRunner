namespace UI_Design
{
    partial class PlaySelect
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.levelBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "SINGLE PLAY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(607, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "MULTI PLAY";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(134, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 86);
            this.button1.TabIndex = 2;
            this.button1.Text = "PLAY";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(616, 431);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 86);
            this.button2.TabIndex = 3;
            this.button2.Text = "PLAY";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(267, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 45);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nickname :";
            // 
            // txtNickname
            // 
            this.txtNickname.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNickname.Location = new System.Drawing.Point(455, 48);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(226, 35);
            this.txtNickname.TabIndex = 5;
            this.txtNickname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNickname_KeyPress);
            // 
            // levelBox
            // 
            this.levelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelBox.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.levelBox.FormattingEnabled = true;
            this.levelBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.levelBox.Items.AddRange(new object[] {
            "초급",
            "중급",
            "고급"});
            this.levelBox.Location = new System.Drawing.Point(211, 316);
            this.levelBox.Name = "levelBox";
            this.levelBox.Size = new System.Drawing.Size(136, 38);
            this.levelBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(88, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 45);
            this.label4.TabIndex = 7;
            this.label4.Text = "LEVEL : ";
            // 
            // txtIp
            // 
            this.txtIp.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtIp.Location = new System.Drawing.Point(659, 316);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(199, 35);
            this.txtIp.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(577, 307);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 45);
            this.label5.TabIndex = 9;
            this.label5.Text = "IP : ";
            // 
            // PlaySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.levelBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PlaySelect";
            this.Size = new System.Drawing.Size(961, 517);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.ComboBox levelBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label5;
    }
}
