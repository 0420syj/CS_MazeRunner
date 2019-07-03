namespace UI_Design
{
    partial class Form1
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCredit = new System.Windows.Forms.Button();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnRank = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.option1 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblWall = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_themeColor = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.rank1 = new UI_Design.Rank();
            this.home1 = new UI_Design.Home();
            this.credit1 = new UI_Design.Credit();
            this.playSelect1 = new UI_Design.PlaySelect();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.CharZ_front = new System.Windows.Forms.PictureBox();
            this.select1 = new System.Windows.Forms.PictureBox();
            this.select2 = new System.Windows.Forms.PictureBox();
            this.select3 = new System.Windows.Forms.PictureBox();
            this.select4 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.option1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CharZ_front)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.panel1.Controls.Add(this.SidePanel);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnCredit);
            this.panel1.Controls.Add(this.btnOption);
            this.panel1.Controls.Add(this.btnRank);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Controls.Add(this.btnHome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 662);
            this.panel1.TabIndex = 0;
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.Color.Lavender;
            this.SidePanel.Location = new System.Drawing.Point(0, 48);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(20, 83);
            this.SidePanel.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(28, 493);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(258, 83);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "        EXIT";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCredit
            // 
            this.btnCredit.FlatAppearance.BorderSize = 0;
            this.btnCredit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredit.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredit.ForeColor = System.Drawing.Color.White;
            this.btnCredit.Image = ((System.Drawing.Image)(resources.GetObject("btnCredit.Image")));
            this.btnCredit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCredit.Location = new System.Drawing.Point(28, 404);
            this.btnCredit.Name = "btnCredit";
            this.btnCredit.Size = new System.Drawing.Size(258, 83);
            this.btnCredit.TabIndex = 1;
            this.btnCredit.Text = "        HELP";
            this.btnCredit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCredit.UseVisualStyleBackColor = true;
            this.btnCredit.Click += new System.EventHandler(this.btnCredit_Click);
            // 
            // btnOption
            // 
            this.btnOption.FlatAppearance.BorderSize = 0;
            this.btnOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOption.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOption.ForeColor = System.Drawing.Color.White;
            this.btnOption.Image = ((System.Drawing.Image)(resources.GetObject("btnOption.Image")));
            this.btnOption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOption.Location = new System.Drawing.Point(28, 315);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(258, 83);
            this.btnOption.TabIndex = 1;
            this.btnOption.Text = "        OPTION";
            this.btnOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // btnRank
            // 
            this.btnRank.FlatAppearance.BorderSize = 0;
            this.btnRank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRank.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRank.ForeColor = System.Drawing.Color.White;
            this.btnRank.Image = ((System.Drawing.Image)(resources.GetObject("btnRank.Image")));
            this.btnRank.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRank.Location = new System.Drawing.Point(28, 226);
            this.btnRank.Name = "btnRank";
            this.btnRank.Size = new System.Drawing.Size(258, 83);
            this.btnRank.TabIndex = 3;
            this.btnRank.Text = "        RANK";
            this.btnRank.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRank.UseVisualStyleBackColor = true;
            this.btnRank.Click += new System.EventHandler(this.btnRank_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlay.Location = new System.Drawing.Point(28, 137);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(258, 83);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "        PLAY";
            this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnHome
            // 
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(28, 48);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(258, 83);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "        Home";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1199, 19);
            this.panel2.TabIndex = 0;
            // 
            // option1
            // 
            this.option1.Controls.Add(this.select4);
            this.option1.Controls.Add(this.select3);
            this.option1.Controls.Add(this.select2);
            this.option1.Controls.Add(this.select1);
            this.option1.Controls.Add(this.pictureBox2);
            this.option1.Controls.Add(this.button10);
            this.option1.Controls.Add(this.pictureBox1);
            this.option1.Controls.Add(this.button8);
            this.option1.Controls.Add(this.CharZ_front);
            this.option1.Controls.Add(this.button9);
            this.option1.Controls.Add(this.label2);
            this.option1.Controls.Add(this.button7);
            this.option1.Controls.Add(this.label1);
            this.option1.Controls.Add(this.button6);
            this.option1.Controls.Add(this.button5);
            this.option1.Controls.Add(this.lblWall);
            this.option1.Controls.Add(this.button1);
            this.option1.Controls.Add(this.lbl_themeColor);
            this.option1.Controls.Add(this.button2);
            this.option1.Controls.Add(this.button3);
            this.option1.Controls.Add(this.button4);
            this.option1.Location = new System.Drawing.Point(288, 18);
            this.option1.Margin = new System.Windows.Forms.Padding(0);
            this.option1.Name = "option1";
            this.option1.Padding = new System.Windows.Forms.Padding(0);
            this.option1.Size = new System.Drawing.Size(909, 661);
            this.option1.TabIndex = 11;
            this.option1.TabStop = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.White;
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Image = ((System.Drawing.Image)(resources.GetObject("button10.Image")));
            this.button10.Location = new System.Drawing.Point(580, 366);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 50);
            this.button10.TabIndex = 23;
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Location = new System.Drawing.Point(647, 241);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 50);
            this.button8.TabIndex = 14;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.Location = new System.Drawing.Point(449, 366);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(50, 50);
            this.button9.TabIndex = 22;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(157, 495);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 37);
            this.label2.TabIndex = 69;
            this.label2.Text = "캐릭터";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(580, 241);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 50);
            this.button7.TabIndex = 13;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(157, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 37);
            this.label1.TabIndex = 21;
            this.label1.Text = "배경음악";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(514, 241);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 50);
            this.button6.TabIndex = 12;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(449, 240);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 50);
            this.button5.TabIndex = 11;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblWall
            // 
            this.lblWall.AutoSize = true;
            this.lblWall.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWall.ForeColor = System.Drawing.Color.White;
            this.lblWall.Location = new System.Drawing.Point(157, 245);
            this.lblWall.Name = "lblWall";
            this.lblWall.Size = new System.Drawing.Size(107, 37);
            this.lblWall.TabIndex = 10;
            this.lblWall.Text = "벽 모양";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(647, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnThemeBasy_Click);
            // 
            // lbl_themeColor
            // 
            this.lbl_themeColor.AutoSize = true;
            this.lbl_themeColor.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_themeColor.ForeColor = System.Drawing.Color.White;
            this.lbl_themeColor.Location = new System.Drawing.Point(157, 120);
            this.lbl_themeColor.Name = "lbl_themeColor";
            this.lbl_themeColor.Size = new System.Drawing.Size(134, 37);
            this.lbl_themeColor.TabIndex = 5;
            this.lbl_themeColor.Text = "테마 색상";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(580, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 50);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnThemeBlue_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(449, 112);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 50);
            this.button3.TabIndex = 6;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnThemeBlack_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(514, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 50);
            this.button4.TabIndex = 7;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnThemeCap_Click);
            // 
            // rank1
            // 
            this.rank1.Location = new System.Drawing.Point(290, 25);
            this.rank1.Name = "rank1";
            this.rank1.Size = new System.Drawing.Size(909, 656);
            this.rank1.TabIndex = 9;
            // 
            // home1
            // 
            this.home1.Location = new System.Drawing.Point(288, 19);
            this.home1.Name = "home1";
            this.home1.Size = new System.Drawing.Size(911, 670);
            this.home1.TabIndex = 8;
            // 
            // credit1
            // 
            this.credit1.Location = new System.Drawing.Point(289, 19);
            this.credit1.Name = "credit1";
            this.credit1.Size = new System.Drawing.Size(910, 662);
            this.credit1.TabIndex = 7;
            // 
            // playSelect1
            // 
            this.playSelect1.Location = new System.Drawing.Point(288, 19);
            this.playSelect1.Name = "playSelect1";
            this.playSelect1.Size = new System.Drawing.Size(911, 670);
            this.playSelect1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UI_Design.Properties.Resources.feet;
            this.pictureBox1.Location = new System.Drawing.Point(647, 490);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 71;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(548, 490);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 72;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // CharZ_front
            // 
            this.CharZ_front.Image = global::UI_Design.Properties.Resources.Characer1_front;
            this.CharZ_front.Location = new System.Drawing.Point(449, 490);
            this.CharZ_front.Name = "CharZ_front";
            this.CharZ_front.Size = new System.Drawing.Size(50, 50);
            this.CharZ_front.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CharZ_front.TabIndex = 70;
            this.CharZ_front.TabStop = false;
            this.CharZ_front.Click += new System.EventHandler(this.CharZ_front_Click);
            // 
            // select1
            // 
            this.select1.Image = ((System.Drawing.Image)(resources.GetObject("select1.Image")));
            this.select1.Location = new System.Drawing.Point(455, 70);
            this.select1.Name = "select1";
            this.select1.Size = new System.Drawing.Size(39, 43);
            this.select1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select1.TabIndex = 73;
            this.select1.TabStop = false;
            // 
            // select2
            // 
            this.select2.Image = ((System.Drawing.Image)(resources.GetObject("select2.Image")));
            this.select2.Location = new System.Drawing.Point(455, 198);
            this.select2.Name = "select2";
            this.select2.Size = new System.Drawing.Size(39, 43);
            this.select2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select2.TabIndex = 74;
            this.select2.TabStop = false;
            // 
            // select3
            // 
            this.select3.Image = ((System.Drawing.Image)(resources.GetObject("select3.Image")));
            this.select3.Location = new System.Drawing.Point(455, 324);
            this.select3.Name = "select3";
            this.select3.Size = new System.Drawing.Size(39, 43);
            this.select3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select3.TabIndex = 75;
            this.select3.TabStop = false;
            // 
            // select4
            // 
            this.select4.Image = ((System.Drawing.Image)(resources.GetObject("select4.Image")));
            this.select4.Location = new System.Drawing.Point(455, 447);
            this.select4.Name = "select4";
            this.select4.Size = new System.Drawing.Size(39, 43);
            this.select4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.select4.TabIndex = 76;
            this.select4.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1199, 681);
            this.Controls.Add(this.option1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rank1);
            this.Controls.Add(this.home1);
            this.Controls.Add(this.credit1);
            this.Controls.Add(this.playSelect1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MazeRunner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.option1.ResumeLayout(false);
            this.option1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CharZ_front)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.Button btnRank;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnCredit;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel SidePanel;
        private PlaySelect playSelect1;
        private Credit credit1;
        private Home home1;
        private Rank rank1;
        private System.Windows.Forms.GroupBox option1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_themeColor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblWall;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox select1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox CharZ_front;
        private System.Windows.Forms.PictureBox select4;
        private System.Windows.Forms.PictureBox select3;
        private System.Windows.Forms.PictureBox select2;
    }
}

