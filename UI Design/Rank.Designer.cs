namespace UI_Design
{
    partial class Rank
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
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_Score = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.levelBox = new System.Windows.Forms.ComboBox();
            this.btnRankBefore = new System.Windows.Forms.Button();
            this.btnRankNext = new System.Windows.Forms.Button();
            this.lbl_Rank = new System.Windows.Forms.Label();
            this.lbl_Page = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Font = new System.Drawing.Font("함초롬돋움", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Name.Location = new System.Drawing.Point(400, 149);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(111, 41);
            this.lbl_Name.TabIndex = 2;
            this.lbl_Name.Text = "닉네임";
            // 
            // lbl_Score
            // 
            this.lbl_Score.AutoSize = true;
            this.lbl_Score.Font = new System.Drawing.Font("함초롬돋움", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Score.Location = new System.Drawing.Point(674, 149);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(80, 41);
            this.lbl_Score.TabIndex = 3;
            this.lbl_Score.Text = "점수";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Refresh.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Refresh.Location = new System.Drawing.Point(124, 22);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(188, 55);
            this.btn_Refresh.TabIndex = 4;
            this.btn_Refresh.Text = "새로고침";
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // levelBox
            // 
            this.levelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelBox.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.levelBox.FormattingEnabled = true;
            this.levelBox.Items.AddRange(new object[] {
            "초급",
            "중급",
            "고급",
            "멀티"});
            this.levelBox.Location = new System.Drawing.Point(338, 22);
            this.levelBox.Name = "levelBox";
            this.levelBox.Size = new System.Drawing.Size(414, 53);
            this.levelBox.TabIndex = 7;
            // 
            // btnRankBefore
            // 
            this.btnRankBefore.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRankBefore.Location = new System.Drawing.Point(16, 317);
            this.btnRankBefore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRankBefore.Name = "btnRankBefore";
            this.btnRankBefore.Size = new System.Drawing.Size(65, 67);
            this.btnRankBefore.TabIndex = 8;
            this.btnRankBefore.Text = "◀";
            this.btnRankBefore.UseVisualStyleBackColor = true;
            this.btnRankBefore.Click += new System.EventHandler(this.btnRankBefore_Click);
            // 
            // btnRankNext
            // 
            this.btnRankNext.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRankNext.Location = new System.Drawing.Point(821, 317);
            this.btnRankNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRankNext.Name = "btnRankNext";
            this.btnRankNext.Size = new System.Drawing.Size(65, 67);
            this.btnRankNext.TabIndex = 9;
            this.btnRankNext.Text = "▶";
            this.btnRankNext.UseVisualStyleBackColor = true;
            this.btnRankNext.Click += new System.EventHandler(this.btnRankNext_Click);
            // 
            // lbl_Rank
            // 
            this.lbl_Rank.AutoSize = true;
            this.lbl_Rank.Font = new System.Drawing.Font("함초롬돋움", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Rank.Location = new System.Drawing.Point(119, 149);
            this.lbl_Rank.Name = "lbl_Rank";
            this.lbl_Rank.Size = new System.Drawing.Size(80, 41);
            this.lbl_Rank.TabIndex = 10;
            this.lbl_Rank.Text = "등수";
            // 
            // lbl_Page
            // 
            this.lbl_Page.AutoSize = true;
            this.lbl_Page.Font = new System.Drawing.Font("함초롬돋움", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Page.Location = new System.Drawing.Point(400, 587);
            this.lbl_Page.Name = "lbl_Page";
            this.lbl_Page.Size = new System.Drawing.Size(66, 41);
            this.lbl_Page.TabIndex = 11;
            this.lbl_Page.Text = "1/1";
            // 
            // Rank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Page);
            this.Controls.Add(this.lbl_Rank);
            this.Controls.Add(this.btnRankNext);
            this.Controls.Add(this.btnRankBefore);
            this.Controls.Add(this.levelBox);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.lbl_Score);
            this.Controls.Add(this.lbl_Name);
            this.Name = "Rank";
            this.Size = new System.Drawing.Size(1054, 580);
            this.Load += new System.EventHandler(this.Rank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_Score;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ComboBox levelBox;
        private System.Windows.Forms.Button btnRankBefore;
        private System.Windows.Forms.Button btnRankNext;
        private System.Windows.Forms.Label lbl_Rank;
        private System.Windows.Forms.Label lbl_Page;
    }
}
