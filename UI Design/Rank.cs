using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace UI_Design
{
    public partial class Rank : UserControl
    {
        // 파일스트림 전역변수
        //public string file_path = Application.StartupPath + @"\\score.txt";
        public string file_path = @"C:\Temp\score_easy.txt";

        public FileStream fs_read;
        public StreamReader sr;

        public int rankPage=1;

        public struct Player
        {
            public string name;
            public int score;
        }

        Player[] players = new Player[100]; // 플레이어 구조체 배열[100개] 선언

        int length = 0; // 구조체 배열 길이 전역변수

        public Rank()
        {
            
            InitializeComponent();
            levelBox.Text = "초급";
        }

        private void Rank_Load(object sender, EventArgs e)
        {
            score_Load();
        }

        private void score_Load()
        {
            if (levelBox.Text == "중급")
            {
                file_path = @"C:\Temp\score_mid.txt";
            }
            else if (levelBox.Text == "고급")
            {
                file_path = @"C:\Temp\score_advanced.txt";
            }
            else
            {
                file_path = @"C:\Temp\score_easy.txt";
            }

            fs_read = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Read);
            sr = new StreamReader(fs_read, Encoding.UTF8);

            string text = sr.ReadToEnd();
            string[] words = text.Split(' ', '\n');

            lbl_Name.Text = null;
            lbl_Score.Text = null;

            length = (words.Length - 1) / 2; // 전역변수에 현재 구조체 배열 길이 전달

            for (int i = 0; i < words.Length - 1; i += 2) // 구조체 배열에 값기 차례대로 대입되도록
            {
                players[i / 2].name = words[i];
                players[i / 2].score = Convert.ToInt32(words[i + 1]);
            }

            Array.Sort<Player>(players, (x, y) => y.score.CompareTo(x.score)); // 내림차순 정렬

            if (length >= 10)
            {
                for (int i = 0; i < 10; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    rankPage = 1;
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
            }
            else
            {
                for (int i = 0; i < length; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    rankPage = 1;
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
            }

            sr.Close();
            sr.Dispose();
            fs_read.Close();
            fs_read.Dispose();

            

            PageLabel();
        }
        public void score_multi()
        {
            file_path = @"C:\Temp\score_multi.txt";

            fs_read = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Read);
            sr = new StreamReader(fs_read, Encoding.UTF8);

            string text = sr.ReadToEnd();
            string[] words = text.Split(' ', '\n');

            lbl_Rank.Text = null;
            lbl_Name.Text = null;
            lbl_Score.Text = null;

            length = (words.Length - 1) / 2; // 전역변수에 현재 구조체 배열 길이 전달

            for (int i = 0; i < words.Length - 1 && i < 30; i += 3) // 구조체 배열에 값기 차례대로 대입되도록
            {
                lbl_Rank.Text += words[i] + '\n';
                lbl_Name.Text += words[i+1] + '\n';
                lbl_Score.Text += words[i+2] + '\n';
            }

            lbl_Page.Text = null;

            btnRankBefore.Visible = false;
            btnRankNext.Visible = false;

            /*
            if (length >= 10)
            {
                for (int i = 0; i < 10; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    rankPage = 1;
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
            }
            else
            {
                for (int i = 0; i < length; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    rankPage = 1;
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
            }
            */

            sr.Close();
            sr.Dispose();
            fs_read.Close();
            fs_read.Dispose();

            //PageLabel();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            btnRankBefore.Visible = true;
            btnRankNext.Visible = true;

            if (levelBox.Text == "멀티")
            {
                score_multi();
            }
            else
            {
                score_Load();
            }
        }
        

        private void btnRankNext_Click(object sender, EventArgs e)
        {
            if (length <= (rankPage + 1) * 10)
            {
                if (length + 10 <= (rankPage + 1) * 10)
                {

                }
                else
                {
                    lbl_Name.Text = null;
                    lbl_Score.Text = null;
                    for (int i = rankPage * 10; i < length; i++) // 구조체 배열에 값기 차례대로 대입되도록
                    {
                        lbl_Name.Text += players[i].name + '\n';
                        lbl_Score.Text += players[i].score.ToString() + '\n';
                    }
                    rankPage += 1;
                }
            }
            else if(length >= (rankPage + 1) * 10)
            {
                lbl_Name.Text = null;
                lbl_Score.Text = null;
                for (int i = rankPage * 10; i < rankPage * 10 + 10; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
                rankPage += 1;
            }
            PageLabel();
        }

        private void btnRankBefore_Click(object sender, EventArgs e)
        {
            if(rankPage != 1)
            {
                rankPage -= 1;
                lbl_Name.Text = null;
                lbl_Score.Text = null;
                for (int i = (rankPage - 1) * 10; i < (rankPage - 1) * 10 + 10; i++) // 구조체 배열에 값기 차례대로 대입되도록
                {
                    lbl_Name.Text += players[i].name + '\n';
                    lbl_Score.Text += players[i].score.ToString() + '\n';
                }
                
            }

            PageLabel();
        }

        private void PageLabel()
        {
            lbl_Page.Text = null;
            lbl_Page.Text = rankPage.ToString() + "/" + (length / 10 + 1).ToString();

            lbl_Rank.Text = null;

            for (int i = 1 + (rankPage - 1) * 10; i <= 10 + (rankPage - 1) * 10; i++)
                if (i <= length)
                    lbl_Rank.Text += i.ToString() + '\n';
        }
    }
}
