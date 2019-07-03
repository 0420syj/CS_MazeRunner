using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UI_Design
{
    public partial class Forms : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        int _x = 10;     // 말 x 좌표
        int _y = 10;     // 말 y 좌표 
        
        Random rand;
        int temp_wall = 0;
        int map_select;
        bool Trap_on = false;
        string nickname;
        int wall_type;          // 벽 스타일
        int level;              // 미로 난이도 
        int char_type;
        int char_width = 50;
        int char_height = 50;
        int count = 0;
        int speed_count = 0; // 스피드 다운시 방향키 횟수
        bool speeds = false; //스피드 다운 작동
        bool angels = false; // 엔젤 작동
        bool time_down = false; // 타임 다운 작동
        bool ice = false; // 얼음 발동
        bool demon = false; // 악마 발동
        bool black = false; // 블랙아웃 발동
        bool items = false;
        bool[,] arr = new bool[1500, 1500];

        int map_count = 0;


        private int time = 0; // 시간 변수
        private int score = 15000; // 점수 변수
        private const int REDUCE = 25; // 감소폭 상수

        //아이템 관련 전역 변수
        int[] arr1 = new int[10];       //아이템 배치 배열 x좌표
        int[] arr2 = new int[10];       //아이템 배치 배열 y좌표
        int[] Item_array = new int[10]; //아이템 배열
        bool set_Item = false;          //아이템 배치는 한번만
        int x, y; // 폭탄 위치
        bool boom = false; // 아이템 폭탄 발동 조건
        bool jump = false;  // 아이템 점프 발동 조건
        int[] bomb_array_x = new int[10]; //폭탄 발생 지점 저장
        int[] bomb_array_y = new int[10]; //폭탄 발생 지점 저장
        int count_bomb = 0; // 쓸수 잇는 폭탄갯수
        bool bulldozer = false; // 불도저
        int speed = 5; // 초기화 스피드
        bool eyes_open = false; // 시야확장
        bool start = false;
        int[] Show_Item_Image_x = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Show_Item_Image_y = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Show_golden_x = new int[] { 0, 0, 0 };
        int[] Show_golden_y = new int[] { 0, 0, 0 };
       
        int count_ice = 0; //얼음 해동 조건
        int[] arr_Key = { 0 }; //점프 방향
        int[] Image_array = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        bool eat = false;
        int width = 400, height = 400;
        int count_Item = 0;             //난이도에 따라 아이템 개수가 변함
        bool ffirst = false;
        int brick_x, brick_y;           //벽의 가로, 세로 길이
        int[,] new_brick;               //벽의 크기를 정해줄 배열
        int char_x, char_y;             //캐릭터 인덱스 배열
        int index_x, index_y;           //초급, 중급, 고급별로 배열의 가로 세로 길이
        Class1 cl;
        int map_size_x = 0, map_size_y = 0;      //난이도별 맵 사이즈 결정하기 위해
        int Item_size;          //난이도별 아이템 박스 사이즈 결정
        //파일스트림 관련 전역변수
        public string file_path = @"C:\Temp\score.txt";
        public FileStream fs_write;
        public StreamWriter sw;
        // 좀비 관련 전역변수
        int _jx;
        int _jy;
        int zombie_speed = 2;

        int demon_cnt = 100;
        int black_cnt = 100;
        int speed_cnt = 100;
        // Map 에서 true false 로 벽 생성 함수
        public void set(int x_start, int y_start, int width, int height)
        {
            for (int i = x_start + 1; i < x_start + width; i++)
                for (int j = y_start + 1; j < y_start + height; j++)
                    arr[i, j] = true;
        }
        public void set_False(int x_start, int y_start, int width, int height) // 벽 파괴 함수
        {
            for (int i = x_start; i < x_start + width; i++)
                for (int j = y_start; j < y_start + height; j++)
                    arr[i, j] = false;
        }


        // 벽 그려주고 Map 에서 true 설정
        private void Destroy_Maze(object sender, PaintEventArgs e) // 벽파괴 실행
        {
            //            x = start_x;        //캐릭터 위치
            //            y = start_y;
            if (boom == true)
            {
                bomb_array_x[count_bomb] = char_x;           //모든 폭탄 위치 저장을 위해 배열로 생성
                bomb_array_y[count_bomb] = char_y;
                count_bomb++;
                for (int i = 0; i < count_bomb; i++)
                {
                    if (bomb_array_x[i] - 1 > 1 && bomb_array_y[i] - 1 > 1 && bomb_array_x[i] + 1 < index_x - 1 && bomb_array_y[i] + 1 < index_y - 1)
                    {
                        new_brick[bomb_array_x[i] - 1, bomb_array_y[i]] = 0;
                        new_brick[bomb_array_x[i] + 1, bomb_array_y[i]] = 0;
                        new_brick[bomb_array_x[i], bomb_array_y[i] + 1] = 0;
                        new_brick[bomb_array_x[i], bomb_array_y[i] - 1] = 0;
                    }
                    else
                    {
                        if (bomb_array_x[i] - 1 <= 1 && bomb_array_y[i] - 1 > 1 && bomb_array_x[i] + 1 < index_x - 1 && bomb_array_y[i] + 1 < index_y - 1)
                        {
                            new_brick[bomb_array_x[i] + 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] + 1] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] - 1] = 0;

                        }
                        else if (bomb_array_y[i] - 1 <= 1 && bomb_array_x[i] - 1 > 1 && bomb_array_x[i] + 1 < index_x - 1 && bomb_array_y[i] + 1 < index_y - 1)
                        {
                            new_brick[bomb_array_x[i] - 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i] + 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] + 1] = 0;
                        }
                        else if (bomb_array_x[i] + 1 >= index_x - 1 && bomb_array_x[i] - 1 > 1 && bomb_array_y[i] - 1 > 1 && bomb_array_y[i] + 1 < index_y - 1)
                        {
                            new_brick[bomb_array_x[i] - 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] + 1] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] - 1] = 0;
                        }
                        else if (bomb_array_y[i] + 1 >= index_y - 1 && bomb_array_x[i] - 1 > 1 && bomb_array_y[i] - 1 > 1 && bomb_array_x[i] + 1 < index_x - 1)
                        {
                            new_brick[bomb_array_x[i] - 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i] + 1, bomb_array_y[i]] = 0;
                            new_brick[bomb_array_x[i], bomb_array_y[i] - 1] = 0;
                        }
                    }
                }
                boom = false;

            }
            else if (bulldozer == true)
            {
                //MessageBox.Show("a");
                if (char_x != 1 || char_y != 1 || char_x != index_x - 1 || char_y != index_y - 1)
                {
                    //MessageBox.Show(index_x + "aa " +index_y);

                    if (arr_Key[0] == 1)
                    {
                        for (int i = char_x + 1; i < index_x - 1; i++)
                            new_brick[i, char_y] = 0;
                    }
                    else if (arr_Key[0] == 2)
                    {
                        for (int i = char_x - 1; i > 0; i--)
                            new_brick[i, char_y] = 0;
                    }
                    else if (arr_Key[0] == 3)
                    {
                        for (int i = char_y - 1; i > 0; i--)
                            new_brick[char_x, i] = 0;
                    }
                    else if (arr_Key[0] == 4)
                        for (int i = char_y + 1; i < index_y - 1; i++)
                            new_brick[char_x, i] = 0;
                }
                bulldozer = false;
            }
        }

        private void Show_Bomb(PaintEventArgs e) // 벽파괴 범위 그려주고 파괴시키기
        {
            Image imageWall = Image.FromFile("wall.png");
            if (wall_type == 1)
            {
                imageWall = Image.FromFile("wall.png");
            }
            else if (wall_type == 2)
            {
                imageWall = Image.FromFile("wall2.png");
            }
            else if (wall_type == 3)
            {
                imageWall = Image.FromFile("wall3.png");
            }
            else if (wall_type == 4)
            {
                imageWall = Image.FromFile("wall4.png");
            }
            for (int j = 0; j < index_x; j++)
            {
                for (int k = 0; k < index_y; k++)
                {
                    if (new_brick[j, k] == 1)
                    {
                        e.Graphics.DrawImage(imageWall, brick_x * j, brick_y * k, brick_x, brick_y);

                    }
                }
            }
        }

        public Forms(string nickname, int wall_type, int level, int sound_on, int char_type)
        {
            InitializeComponent();
            if (sound_on == 1)
            {
                player.SoundLocation = "miro_bgm.wav";
            }

            this.char_type = char_type;
            this.level = level;
            this.wall_type = wall_type;
            this.nickname = nickname;
            rand = new Random();
            if (count == 0)
            {
                map_select = rand.Next();
                count++;
            }
        }

        private void level1_Miro(object sender, PaintEventArgs e) //초급 벽
        {
            cl = new Class1();
            
            Image imageWall = Image.FromFile("wall.png");
            if(wall_type == 1)
            {
                imageWall = Image.FromFile("wall.png");
            }else if(wall_type == 2)
            {
                imageWall = Image.FromFile("wall2.png");
            }
            else if (wall_type == 3)
            {
                imageWall = Image.FromFile("wall3.png");
            }
            else if (wall_type == 4)
            {
                imageWall = Image.FromFile("wall4.png");
            }
            if(map_count == 0)
            {
                new_brick = cl.level1();
            }
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (new_brick[i, j] == 1)
                    {
                        e.Graphics.DrawImage(imageWall, 69 * i, 52 * j, 69, 52);
                        
                    }
                }
            }
            map_count++;
        }       
        private void level2_Miro(object sender, PaintEventArgs e) //중급 벽 
        {
            cl = new Class1();
            Image imageWall = Image.FromFile("wall.png");
            if (wall_type == 1)
            {
                imageWall = Image.FromFile("wall.png");
            }
            else if (wall_type == 2)
            {
                imageWall = Image.FromFile("wall2.png");
            }
            else if (wall_type == 3)
            {
                imageWall = Image.FromFile("wall3.png");
            }
            else if (wall_type == 4)
            {
                imageWall = Image.FromFile("wall4.png");
            }
            new_brick = cl.level2();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (new_brick[i, j] == 1)
                    {
                        e.Graphics.DrawImage(imageWall, 36 * i, 27 * j, 36, 27);
                    }
                }
            }

        }
        private void level3_Miro(object sender, PaintEventArgs e) //고급 벽 
        {
            cl = new Class1();
            Image imageWall = Image.FromFile("wall.png");
            if (wall_type == 1)
            {
                imageWall = Image.FromFile("wall.png");
            }
            else if (wall_type == 2)
            {
                imageWall = Image.FromFile("wall2.png");
            }
            else if (wall_type == 3)
            {
                imageWall = Image.FromFile("wall3.png");
            }
            else if (wall_type == 4)
            {
                imageWall = Image.FromFile("wall4.png");
            }
            new_brick = new int[41, 41]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1},
{1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1},
{1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,1},
{1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1},
{1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1},
{1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,1},
{1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1},
{1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1},
{1,1,1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1},
{1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1},
{1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1},
{1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1},
{1, 0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,1,1,0,1},
{1, 0,  1,  0,  1,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1},
{1, 0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1},
{1, 0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1},
{1, 1,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  0,  0,  0,  0,  0,  1,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1},
{1, 0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1},
{1, 0,  1,  1,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  0,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  1,  1,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  1,  0,  0,  0,  0,  0,  1},
{1, 0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  0,  1,  0,  1,  1,  1,  0,  1,  0,  1,  1,  1,  0,  1,  1,  1,  0,  1},
{1, 0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  1,  0,  0,  0,  1},
{1, 1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1}

            };
            new_brick = cl.level3();
            for(int i=0; i<41; i++)
            {
                for(int j=0; j<41; j++)
                {
                    if(new_brick[i, j] == 1)
                    {
                        e.Graphics.DrawImage(imageWall, 21 * i, 16 * j, 21, 16);
                    }
                }
            }
            
        }
        // 말 생성
        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            map_size_x = 900;
            map_size_y = 680;

            if (start == false)
            {
                if (level == 1)
                {
                    if (char_type == 1)
                    {
                        CharZ_front.Size = new System.Drawing.Size(69, 52);
                        CharZ_left.Size = new System.Drawing.Size(69, 52);
                        CharZ_right.Size = new System.Drawing.Size(69, 52);
                        CharZ_up.Size = new System.Drawing.Size(69, 52);
                    }
                    else if (char_type == 2)
                    {
                        any_up.Size = new System.Drawing.Size(69, 52);
                        any_down.Size = new System.Drawing.Size(69, 52);
                        any_right.Size = new System.Drawing.Size(69, 52);
                        any_left.Size = new System.Drawing.Size(69, 52);
                    }
                    if (char_type == 3)
                    {
                        down_foot.Size = new System.Drawing.Size(69, 52);
                        top_foot.Size = new System.Drawing.Size(69, 52);
                        right_foot.Size = new System.Drawing.Size(69, 52);
                        left_foot.Size = new System.Drawing.Size(69, 52);
                    }
                    level1_Miro(sender, e);
                   
                    if (ffirst == false)
                    {
                        _x = 207;   _y = 48;
                        Point p = new Point(_x, _y);
                        if(char_type == 1)
                        {
                            CharZ_front.Visible = true;
                            CharZ_front.BringToFront();
                            CharZ_front.Location = p;
                        }else if(char_type == 2)
                        {
                            any_down.Visible = true;
                            any_down.BringToFront();
                            any_down.Location = p;
                        }else if(char_type == 3)
                        {
                            down_foot.Visible = true;
                            down_foot.BringToFront();
                            down_foot.Location = p;
                        }
                        
                        ffirst = true;
                        brick_x = 69;
                        brick_y = 52;
                        char_x = 1; char_y = 1;
                        index_x = 13;   index_y = 13;
                        count_Item = 3;
                        trap_ItemBox.Visible = true;
                        finish.Visible = true;
                        finish.Size = new System.Drawing.Size(brick_x, brick_y);
                        finish.Location = new Point(207 + (index_x - 3) * brick_x, 48 + (index_y - 3) * brick_y);
                        finish.BringToFront();
                    }
                }
                else if (level == 2)
                {
                    if (char_type == 1)
                    {
                        CharZ_front.Size = new System.Drawing.Size(36, 27);
                        CharZ_left.Size = new System.Drawing.Size(36, 27);
                        CharZ_right.Size = new System.Drawing.Size(36, 27);
                        CharZ_up.Size = new System.Drawing.Size(36, 27);
                    }
                    else if (char_type == 2)
                    {
                        any_up.Size = new System.Drawing.Size(36, 27);
                        any_down.Size = new System.Drawing.Size(36, 27);
                        any_right.Size = new System.Drawing.Size(36, 27);
                        any_left.Size = new System.Drawing.Size(36, 27);
                    }
                    if (char_type == 3)
                    {
                        down_foot.Size = new System.Drawing.Size(36, 27);
                        top_foot.Size = new System.Drawing.Size(36, 27);
                        right_foot.Size = new System.Drawing.Size(36, 27);
                        left_foot.Size = new System.Drawing.Size(36, 27);
                    }
                    level2_Miro(sender, e);
                    if (ffirst == false)
                    {
                        _x = 174; _y = 23;
                        Point p = new Point(_x, _y);
                        if (char_type == 1)
                        {
                            CharZ_front.Visible = true;
                            CharZ_front.BringToFront();
                            CharZ_front.Location = p;
                        }
                        else if (char_type == 2)
                        {
                            any_down.Visible = true;
                            any_down.BringToFront();
                            any_down.Location = p;
                        }
                        else if (char_type == 3)
                        {
                            down_foot.Visible = true;
                            down_foot.BringToFront();
                            down_foot.Location = p;
                        }
                        ffirst = true;
                        brick_x = 36; brick_y = 27;
                        index_x = 25; index_y = 25;
                        char_x = 1; char_y = 1;
                        count_Item = 6;
                        trap_ItemBox.Visible = true;
                        finish.Visible = true;
                        finish.Size = new System.Drawing.Size(brick_x, brick_y);
                        finish.Location = new Point(173 + (index_x - 3) * brick_x, 23 + (index_y - 3) * brick_y);
                        finish.BringToFront();
                    }
                }
                else if (level == 3)
                {
                    if (char_type == 1)
                    {
                        CharZ_front.Size = new System.Drawing.Size(21, 16);
                        CharZ_left.Size = new System.Drawing.Size(21, 16);
                        CharZ_right.Size = new System.Drawing.Size(21, 16);
                        CharZ_up.Size = new System.Drawing.Size(21, 16);
                    }
                    else if (char_type == 2)
                    {
                        any_up.Size = new System.Drawing.Size(21, 16);
                        any_down.Size = new System.Drawing.Size(21, 16);
                        any_right.Size = new System.Drawing.Size(21, 16);
                        any_left.Size = new System.Drawing.Size(21, 16);
                    }
                    if (char_type == 3)
                    {
                        down_foot.Size = new System.Drawing.Size(21, 16);
                        top_foot.Size = new System.Drawing.Size(21, 16);
                        right_foot.Size = new System.Drawing.Size(21, 16);
                        left_foot.Size = new System.Drawing.Size(21, 16);
                    }
                    level3_Miro(sender, e);
                    if (ffirst == false)
                    {
                        _x = 159; _y = 12;
                        Point p = new Point(_x, _y);
                        if (char_type == 1)
                        {
                            CharZ_front.Visible = true;
                            CharZ_front.BringToFront();
                            CharZ_front.Location = p;
                        }
                        else if (char_type == 2)
                        {
                            any_down.Visible = true;
                            any_down.BringToFront();
                            any_down.Location = p;
                        }
                        else if (char_type == 3)
                        {
                            down_foot.Visible = true;
                            down_foot.BringToFront();
                            down_foot.Location = p;
                        }
                        ffirst = true;
                        brick_x = 21;   brick_y = 16;
                        index_x = 41; index_y = 41;
                        char_x = 1; char_y = 1;
                        count_Item = 10;
                        trap_ItemBox.Visible = true;
                        finish.Visible = true;
                        finish.Size = new System.Drawing.Size(brick_x, brick_y);
                        finish.Location = new Point(159 + (index_x - 3) * brick_x, 12 + (index_y - 3) * brick_y);
                        finish.BringToFront();
                    }
                }


                               System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                               activate_Timer();       
                start = true;
            }
            if (bulldozer == true || boom == true)
            {
                Destroy_Maze(sender, e);
            }

            Draw_Item(e);
            Inventory(e);
            Eat_Item(e);
            Show_Bomb(e);

            /*            if (!eyes_open)
                        {
                                    Image DarkSight = Image.FromFile("1500.png"); // 사용할 이미지 로드(이미지는 exe 파일과 같은 디렉토리에 둘 것!)
                                    Graphics g; // 시야제한 이미지를 담을 새로운 변수
                                    g = e.Graphics;
                                    g.DrawImage(DarkSight, _x - (DarkSight.Width / 2) + (char_width / 2), _y - (DarkSight.Height / 2) + (char_height / 2));

                        }*/
            if (!eyes_open)
            {
                if (level == 1)
                {
                    DarkSight1.Visible = true;
                    DarkSight1.Size = new Size(2000, 2000);
                    int DarkX = _x - (DarkSight1.Width / 2) - FormView.Location.X + (CharZ_front.Width / 2);
                    int DarkY = _y - (DarkSight1.Height / 2) - FormView.Location.Y + (CharZ_front.Height / 2);
                    DarkSight1.Location = new Point(DarkX, DarkY);
                }
                
            }
        }
        public class DoubleBufferedPictureBox : System.Windows.Forms.PictureBox
        {
            public DoubleBufferedPictureBox()
            {
                this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer |
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint,
                true);

                this.UpdateStyles();
            }
        }
        private void Random_Item(int width, int height, int level, PaintEventArgs e)          //랜덤으로 아이템 배치 좌표 구하기
        {
            if (level == 1)              //초급이라면
            {
                Item_size = 30;
                Item1.Size = new System.Drawing.Size(30, 30);
                Item2.Size = new System.Drawing.Size(30, 30);
                Item3.Size = new System.Drawing.Size(30, 30);
            }
            else if (level == 2)        //중급
            {
                Item_size = 20;
                Item1.Size = new System.Drawing.Size(20, 20);
                Item2.Size = new System.Drawing.Size(20, 20);
                Item3.Size = new System.Drawing.Size(20, 20);
                Item4.Size = new System.Drawing.Size(20, 20);
                Item5.Size = new System.Drawing.Size(20, 20);
                Item6.Size = new System.Drawing.Size(20, 20);
                
            }
            else if (level == 3)        //고급
            {
                Item_size = 10;
                Item1.Size = new System.Drawing.Size(10, 10);
                Item2.Size = new System.Drawing.Size(10, 10);
                Item3.Size = new System.Drawing.Size(10, 10);
                Item4.Size = new System.Drawing.Size(10, 10);
                Item5.Size = new System.Drawing.Size(10, 10);
                Item6.Size = new System.Drawing.Size(10, 10);
                Item7.Size = new System.Drawing.Size(10, 10);
                Item8.Size = new System.Drawing.Size(10, 10);
                Item9.Size = new System.Drawing.Size(10, 10);
                Item10.Size = new System.Drawing.Size(10, 10);
                
            }
            for (int i = 0; i < count_Item; i++)
            {
                while (true)
                {
                    arr1[i] = rand.Next((i * index_x) / count_Item, ((i + 1) * index_x / count_Item));
                    arr2[i] = rand.Next(1, index_y);
                    if (new_brick[arr1[i], arr2[i]] == 0)
                    {
                        break;
                    }
                }
            }
        }

        private void Draw_Item(PaintEventArgs e)        //Random_Item에서 구한 좌표에다가 아이템 배치
        {
            if (set_Item == false)
            {
                Random_Item(width, height, level, e);
                set_Item = true;
            }
            Item1.Visible = true;           //아이템 상자
            Item2.Visible = true;
            Item3.Visible = true;
            if(level > 1)
            {
                Item4.Visible = true;
                Item5.Visible = true;
                Item6.Visible = true;
                if(level > 2)
                {
                    Item7.Visible = true;
                    Item8.Visible = true;
                    Item9.Visible = true;
                    Item10.Visible = true;
                }
            }
            Item1.Location = new Point(144 + arr1[0] * brick_x, arr2[0] * brick_y);
            Item2.Location = new Point(144 + arr1[1] * brick_x, arr2[1] * brick_y);
            Item3.Location = new Point(144 + arr1[2] * brick_x, arr2[2] * brick_y);
            Item1.BringToFront();
            Item2.BringToFront();
            Item3.BringToFront();
            if (level > 1)
            {
                Item4.Location = new Point(144 + arr1[3] * brick_x, arr2[3] * brick_y);
                Item5.Location = new Point(144 + arr1[4] * brick_x, arr2[4] * brick_y);
                Item6.Location = new Point(144 + arr1[5] * brick_x, arr2[5] * brick_y);
                Item4.BringToFront();
                Item5.BringToFront();
                Item6.BringToFront();
                if (level > 2)
                {
                    Item7.Location = new Point(144 + arr1[6] * brick_x, arr2[6] * brick_y);
                    Item8.Location = new Point(144 + arr1[7] * brick_x, arr2[7] * brick_y);
                    Item9.Location = new Point(144 + arr1[8] * brick_x, arr2[8] * brick_y);
                    Item10.Location = new Point(144 + arr1[9] * brick_x, arr2[9] * brick_y);
                    Item7.BringToFront();
                    Item8.BringToFront();
                    Item9.BringToFront();
                    Item10.BringToFront();
                }
            }
            trap_ItemBox.Location = new Point(1100, 600);                       //함정
            trap_ItemBox.BringToFront();

        }

       
        private void Use_Item(object sender, KeyEventArgs e, int key, int num)        //아이템 사용   key: 키보드 전달 숫자
        {
            if (num == 1)
                ItemBox1.Visible = true;
            if (num == 2)
                ItemBox2.Visible = true;
            if (num == 3)
                ItemBox3.Visible = true;
            if (num == 4)
                ItemBox4.Visible = true;
            if (num == 5)
                ItemBox5.Visible = true;
            if (num == 6)
                ItemBox6.Visible = true;
            if (num == 7)
                ItemBox7.Visible = true;
            if (num == 8)
                ItemBox8.Visible = true;
            if (num == 9)
                ItemBox9.Visible = true;

            Show_Item_Image_x[num - 1] = 0;
            Show_Item_Image_y[num - 1] = 0;
            if (Image_array[num - 1] == 1)      //점프
            {
                if (char_x - 2 > 0 && char_x + 2 < index_x && char_y - 2 > 0 && char_y + 2 < index_y)
                {

                    jump = true;
                    if (arr_Key[0] == 1)        //오른쪽
                    {
                        if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                        {
                            speed_count = speed;
                            char_x += 2;
                            jump = false;
                            _x += brick_x * 2;
                            CharZ_right.Location = new Point(_x, _y);
                            CharZ_right.BringToFront();
                        }
                    }
                    else if (arr_Key[0] == 2)
                    {
                        if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                        {
                            speed_count = speed;
                            char_x -= 2;
                            jump = false;
                            _x -= brick_x * 2;
                            CharZ_left.Location = new Point(_x, _y);
                            CharZ_left.BringToFront();
                        }
                    }
                    else if (arr_Key[0] == 3)               //위
                    {
                        if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                        {
                            speed_count = speed;
                            char_y -= 2;
                            jump = false;
                            _y -= brick_y * 2;
                            CharZ_up.Location = new Point(_x, _y);
                            CharZ_up.BringToFront();
                        }

                    }
                    else if (arr_Key[0] == 4)
                    {
                        if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                        {
                            speed_count = speed;
                            char_y += 2;
                            jump = false;
                            _y += brick_y * 2;
                            CharZ_front.Location = new Point(_x, _y);
                            CharZ_front.BringToFront();
                        }

                    }
                    speed_count = 0;
                    Image_array[num - 1] = 0;
                    jumping.Visible = false;
                    items = false;
                }
                else
                {
                    if (char_x - 2 <= 0)
                    {
                        if (char_y + 2 < index_y && char_y - 2 > 0)
                        {
                            jump = true;
                            if (arr_Key[0] == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                }
                            }

                            else if (arr_Key[0] == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                }

                            }
                            else if (arr_Key[0] == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                }

                            }
                        }
                        else if (char_y + 2 >= index_y && char_y - 2 > 0)
                        {
                            jump = true;
                            if (arr_Key[0] == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                }
                            }

                            else if (arr_Key[0] == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                }

                            }
                        }
                        else if (char_y + 2 < index_y && char_y - 2 <= 0)
                        {
                            jump = true;
                            if (arr_Key[0] == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                }

                            }
                        }


                    }
                    //왼
                    else if (char_x + 2 >= index_x)
                    {
                        if (char_y + 2 >= index_y && char_y - 2 > 0)
                        {
                            jump = true;

                            if (arr_Key[0] == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                }

                            }
                            
                               

                        }
                        else if (char_y + 2 < index_y && char_y - 2 <= 0)
                        {
                            jump = true;

                            if (arr_Key[0] == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                }

                            }
                        }
                        /*               ------------------
                         *               ----------------
                         *               -------------
                         *               --------------
                         *               ----------------
                         *               --------------
                         *               ---------------
                         *               -------------
                         *               수정코드    (밑에)     
                         *               -------------------
                         ------------------------------
                         -------------------------------
                         ---------------------------
                         -------------------------*/
                        else
                        {
                            jump = true;
                            if (arr_Key[0] == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                }

                            }
                            else if (arr_Key[0] == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                }

                            }
                        }
                        /* 여기 까지 입니다.*/
                    }
                    //오
                    else if (char_y + 2 >= index_y)
                    {
                        if (char_y - 2 <= 0)
                        {
                            jump = true;
                            if (arr_Key[0] == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                }
                            }

                        }
                        else
                        {
                            jump = true;
                            if (arr_Key[0] == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                }
                            }
                            else if (arr_Key[0] == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                }

                            }
                        }



                    }
                    //아
                    else if (char_y - 2 <= 0)
                    {
                        jump = true;
                        if (arr_Key[0] == 1)        //오른쪽
                        {
                            if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                            {
                                speed_count = speed;
                                char_x += 2;
                                jump = false;
                                _x += brick_x * 2;
                                CharZ_right.Location = new Point(_x, _y);
                                CharZ_right.BringToFront();
                            }
                        }
                        else if (arr_Key[0] == 2)
                        {
                            if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                            {
                                speed_count = speed;
                                char_x -= 2;
                                jump = false;
                                _x -= brick_x * 2;
                                CharZ_left.Location = new Point(_x, _y);
                                CharZ_left.BringToFront();
                            }
                        }

                        else if (arr_Key[0] == 4)
                        {
                            if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                            {
                                speed_count = speed;
                                char_y += 2;
                                jump = false;
                                _y += brick_y * 2;
                                CharZ_front.Location = new Point(_x, _y);
                                CharZ_front.BringToFront();
                            }

                        }
                    }
                    //위
                    speed_count = 0;
                    Image_array[num - 1] = 0;
                    jumping.Visible = false;
                }
            }
            else if (Image_array[num - 1] == 2)     //폭탄
            {
                //MessageBox.Show("bomb");
                boom = true;
                //bulldozer = true;
                Image_array[num - 1] = 0;
                bomb.Visible = false;

                FormView.Invalidate(true);
                FormView.Update();
            }
            /* 수정 코드 엔젤 : 12->3번으로 옮기고 12번 지움 */
            else if (Image_array[num - 1] == 3) // 천사
            {
                angels = true;
                if (demon)
                {
                    demon = false;
                    devil.Visible = false;
                    demon_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                    trap_ItemBox.Visible = true;
                }
                if (black)
                {
                    BlackOut_Off();
                    devil.Visible = false;
                    trap_ItemBox.Visible = true;
                    black_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                }
                if (speeds)
                {
                    speed = 1;
                    time_decrease.Visible = false;
                    trap_ItemBox.Visible = true;
                    speed_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                }
                angel.Visible = false;
                Image_array[num - 1] = 0;
            }
            /*여기까지 입니다.*/
            else if (Image_array[num - 1] == 4)     //시간 추가
            {
                //               MessageBox.Show("eye open");
                Hourglass();
                time_add.Visible = false;
                Image_array[num - 1] = 0;
            }
            else if (Image_array[num - 1] == 5)     //벽 부수는 좀비
            {
                //               MessageBox.Show("break block");
                bulldozer = true;
                Image_array[num - 1] = 0;
                bull.Visible = false;

                FormView.Invalidate(true);
                FormView.Update();
            }
            
        }

        private void Hourglass()            //모래 시계(시간 추가 아이템)
        {
            int save_score = score;
            if (!time_down)
                score += rand.Next(2500, 7500);                      //1000=4초    10초 ~ 30초 안으로 랜덤 추가
            else
            {
                score -= rand.Next(1500, 3000);                      //1000=4초    6초 ~ 12초 안으로 랜덤 감소
                time_down = false;
            }              //8초 ~ 16초 안으로 시간 감소
        }

        private void Eat_Item(PaintEventArgs e)         //아이템 먹기
        {
            if (Image_array[0] != 0 && Image_array[1] != 0 && Image_array[2] != 0 && Image_array[3] != 0 && Image_array[4] != 0)
                return ;
            for (int i = 0; i < count_Item; i++)
            {
                if (arr1[i] == char_x && arr2[i]== char_y)              //아이템이랑 말의 좌표가 같다면
                {
//                    MessageBox.Show(arr1[i].ToString() + " " + char_x.ToString() + " " + arr2[i].ToString() + " " + char_y.ToString());
                    arr1[i] = -50;              //먹으면 아이템 위치 이상한 곳으로
                    arr2[i] = -50;
                    eat = true;
                    for (int j = 0; j < count_Item; j++)
                    {
                        if (Show_Item_Image_y[j] == 0)
                        {
                            if (ItemTrap() == 0)        //함정이 아니라면
                            {
                                Show_Item_Image_x[j] = 1100;                    //함정이라면 패스
                                Show_Item_Image_y[j] = 100 + j * 60;
                            }
                            
                            Item_list(e);


                            if (j == 0)
                            {
                                Item1.Visible = false;          //아이템 상자 사라지게 만들기
                            }
                            else if (j == 1)
                            {
                                Item2.Visible = false;
                            }
                            else if (j == 2)
                            {
                                Item3.Visible = false;
                            }
                            else if (j == 3)
                            {
                                Item4.Visible = false;
                            }
                            else if (j == 4)
                            {
                                Item5.Visible = false;
                            }
                            else if (j == 5)
                            {
                                Item6.Visible = false;
                            }
                            else if (j == 6)
                            {
                                Item7.Visible = false;
                            }
                            else if (j == 7)
                            {
                                Item8.Visible = false;
                            }
                            else if (j == 8)
                            {
                                Item9.Visible = false;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private int count_dup(int n)
        {
            if (n >= 1 && n <= 17)      //점프
            {
                return 1;
            }
            else if (n > 17 && n <= 32)   //폭탄
            {
                return 2;
            }
            else if (n > 32 && n <= 42)   //천사
            {
                return 3;
            }
            else if (n > 42 && n <= 47)   //시간 추가
            {
                return 4;
            }
            else if (n > 47 && n <= 50)   //벽 부수는 좀비
            {
                return 5;
            }
            else if (n > 50 && n <= 60)   //시간 감소
            {
                return 6;
            }
            else if (n > 60 && n <= 67)   //맵 다 가리기
            {
                return 7;
            }
            else if (n > 67 && n <= 77)   //악마
            {
                return 8;
            }
            else if (n > 77 && n <= 87)   //속도 느리게 하기
            {
                return 9;
            }
            else if (n > 87 && n <= 97)   //얼음
            {
                return 10;
            }
            else if (n > 97 && n <= 100)  //초기화
            {
                return 11;
            }
            /*수정코드 엔젤 : return 12; 지웠습니다. 엔젤 12->3번입니다.*/ 
            return 0;
        }

        private void BlackOut()
        {
            black = true;
            FormView.BackColor = Color.Black;
            temp_wall = wall_type;
            wall_type = 1;
        }

        private void BlackOut_Off()
        {
            black = false;
            FormView.BackColor = Color.Transparent;
            wall_type = temp_wall;
        }

        private void Trap(int n)
        {
            trap_ItemBox.Visible = false;
            if (n == 6)    //시간 감소
            {
                devil.Visible = false;
                go_slow.Visible = false;
                ice1.Visible = false;
                reset.Visible = false;
                hide.Visible = false;
                time_decrease.Visible = true;
                time_decrease.Location = new Point(1100, 600);
                time_down = true;
                Hourglass();
            }
            else if (n == 7)    //맵 다 가리기
            {
                devil.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                time_decrease.Visible = false;
                hide.Visible = true;
                hide.Location = new Point(1100, 600);
                BlackOut();

            }

            else if (n == 8)   //악마
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                devil.Visible = true;
                devil.Location = new Point(1100, 600);

                demon = true;
            }
            else if (n == 9)   //속도 느리게 하기
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                devil.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                go_slow.Visible = true;
                go_slow.Location = new Point(1100, 600);
                speeds = true;
            }
            else if (n == 10)  //얼음
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                devil.Visible = false;
                go_slow.Visible = false;
                reset.Visible = false;
                ices.Visible = true;
                ices.Location = new Point(1100, 600);
                ice = true;
            }
            else if (n == 11)       //초기화
            {
                time_decrease.Visible = false;
                devil.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                hide.Visible = false;
                reset.Visible = true;
                reset.Location = new Point(1100, 600);
                char_x = 1; char_y = 1;
                if (level == 1)
                {
                    _x = 207; _y = 48;
                }
                else if (level == 2)
                {
                    _x = 174; _y = 23;
                }
                else if (level == 3)
                {
                    _x = 159; _y = 12;
                }
                Point pp = new Point(_x, _y);
                CharZ_front.Size = new System.Drawing.Size(brick_x, brick_y);
                CharZ_right.Visible = false;
                CharZ_left.Visible = false;
                CharZ_up.Visible = false;
                CharZ_front.Visible = true;
                CharZ_front.Location = pp;
                CharZ_front.BringToFront();
                reset.Visible = false;
                trap_ItemBox.Visible = true;
            }

        }

        private int ItemTrap()
        {
            int ret = 0;
            if (eat == true)
            {
                while (true)
                {
                    int n = rand.Next(1, 116);        //아이템을 랜덤으로 정하기
                    n = count_dup(n);
                    if (n >= 6 && Trap_on == true)
                        continue;
                    else if (n >= 6)      //함정이 걸린다면
                    {
                        Trap(n);
                        ret = 1;
                        eat = false;
                        return ret;
                    }
                    else
                    {
                        if (Image_array[0] == n || n == Image_array[1] || Image_array[2] == n || Image_array[3] == n || Image_array[4] == n || Image_array[5] == n || Image_array[6] == n || Image_array[7] == n || Image_array[8] == n || Image_array[9] == n)
                        {
                            continue;
                        }
                        if (Image_array[0] == 0)
                        {
                            Image_array[0] = n;
                            break;
                        }
                        else if (Image_array[1] == 0)
                        {
                            Image_array[1] = n;
                            break;
                        }
                        else if (Image_array[2] == 0)
                        {
                            Image_array[2] = n;
                            break;
                        }
                        else if (Image_array[3] == 0)
                        {
                            Image_array[3] = n;
                            break;
                        }
                        else if (Image_array[4] == 0)
                        {
                            Image_array[4] = n;
                            break;
                        }
                        break;
                    }
                }
                eat = false;
            }
            return ret;
        }

        private int Item_list(PaintEventArgs e)     //아이템이 중복되면 문제가 생김(중복이 생기지 않도록 저장할 수 있는 아이템에 같은 아이템이 들어올 수 있도록 해놨음)
        {
            //i: 몇번째 아이템 박스를 먹었는지 저장
            int ret = 0;
            for (int j = 0; j < count_Item; j++)
            {
//                MessageBox.Show(Show_Item_Image_y[j].ToString());
                if (Show_Item_Image_y[j] == 100)
                    ItemBox1.Visible = false;
                if (Show_Item_Image_y[j] == 160)
                    ItemBox2.Visible = false;
                if (Show_Item_Image_y[j] == 220)
                    ItemBox3.Visible = false;
                if (Show_Item_Image_y[j] == 280)
                    ItemBox4.Visible = false;
                if (Show_Item_Image_y[j] == 340)
                    ItemBox5.Visible = false;

                if (Image_array[j] == 1 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                     //점프 아이템
                {
                    jumping.Visible = true;
                    jumping.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
                else if (Image_array[j] == 2 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //폭탄 아이템
                {
                    bomb.Visible = true;
//                    MessageBox.Show(Show_Item_Image_y[j].ToString());
                    bomb.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);

                }
                else if (Image_array[j] == 3 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //시야 전체 확장
                {
                    angel.Visible = true;
                    angel.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
                else if (Image_array[j] == 4 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //시간 추가
                {
                    time_add.Visible = true;
                    time_add.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
                else if (Image_array[j] == 5 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //벽 부수는 좀비
                {
                    bull.Visible = true;
                    bull.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
            }
            return ret;
        }

        
        // Deley 함수 
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            player.PlayLooping();
        }

        private void FormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
        }

        private void Inventory(PaintEventArgs e)
        {
            Point inven = new Point(1080, 0);
            Inven.Visible = true;
            Inven.Location = inven;
            if (Show_Item_Image_y[0] == 0)
            {
                ItemBox1.Visible = true;
                ItemBox1.Location = new Point(1100, 100);       //인벤토리 창에 뜨는 아이템 박스
            }
            if (Show_Item_Image_y[1] == 0)
            {
                ItemBox2.Visible = true;
                ItemBox2.Location = new Point(1100, 160);
            }
            if (Show_Item_Image_y[2] == 0)
            {
                ItemBox3.Visible = true;
                ItemBox3.Location = new Point(1100, 220);
            }
            if (level > 1)
            {
                if (Show_Item_Image_y[3] == 0)
                {
                    ItemBox4.Visible = true;
                    ItemBox4.Location = new Point(1100, 280);       
                }
                if (Show_Item_Image_y[4] == 0)
                {
                    ItemBox5.Visible = true;
                    ItemBox5.Location = new Point(1100, 340);
                }
            }
        }

        private void tmr1_Tick(object sender, EventArgs e)
        {
            // 경과시간 출력
            time++;
            lbl_PastTime.Text = Convert.ToString(time / 10 + "." + time % 10 + "초");

            // 점수 출력
            score -= REDUCE; // 점수 감소폭
            lbl_Score.Text = Convert.ToString(score);
            // 좀비 움직임
            //            move_zombie(_jx, _jy);
            // 남은시간 출력
            lbl_RemainTime.Text = Convert.ToString(score / REDUCE / 10 + "." + score / REDUCE % 10 + "초");

            if (score / REDUCE / 10 <= 14) // 시간이 얼마 안 남으면 라벨 배경,텍스트 색상 변경
            {
                lbl_RemainTime.BackColor = Color.Black;
                lbl_RemainTime.ForeColor = Color.Red;
            }

           else
            {
                lbl_RemainTime.BackColor = this.BackColor;
                lbl_RemainTime.ForeColor = Color.Black;
            }

            // 사망 판정
            if (score <= 0)
            {
                tmr1.Enabled = false; // 타이멍 작동 정지
                MessageBox.Show("엌ㅋㅋㅋㅋ! 죽었습니닼ㅋㅋㅋㅋ!!");
                this.Close(); // 폼 종료
            }

            if (demon)
            {
                lbl_TrapTime.Text = Convert.ToString(demon_cnt / 10 + "." + demon_cnt % 10 + "초");
                demon_cnt--;
                Trap_on = true;
                if (demon_cnt == 0 || angels == true) // 10초가 다 지나면 효과 해제, 각 함정마다 타이머 int 변수 따로 지정
                {
                    demon = false;
                    devil.Visible = false;
                    demon_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                    trap_ItemBox.Visible = true;
                }
            }

            if (black)
            {
                //                MessageBox.Show("어둡게");
                lbl_TrapTime.Text = Convert.ToString(black_cnt / 10 + "." + black_cnt % 10 + "초");
                black_cnt--;
                Trap_on = true;
                if (black_cnt == 0 || angels == true) // 10초가 다 지나면 효과 해제, 각 함정마다 타이머 int 변수 따로 지정
                {
                    BlackOut_Off();
                    devil.Visible = false;
                    trap_ItemBox.Visible = true;
                    black_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                }
            }
            if (speeds)
            {
                lbl_TrapTime.Text = Convert.ToString(speed_cnt / 10 + "." + speed_cnt % 10 + "초");

                speed = 5;
                speed_cnt--;
                Trap_on = true;
                if (speed_cnt == 0 || angels == true)
                {
                    speed = 1;
                    go_slow.Visible = false;
                    trap_ItemBox.Visible = true;
                    speed_cnt = 100; // 원래대로 복구
                    lbl_TrapTime.Text = "OFF";
                    Trap_on = false;
                    speeds = false;

                }
            }
        }

        private void activate_Timer()
        {
            if (!tmr1.Enabled)
                tmr1.Enabled = true; // 작동시작
        }

        private void Forms_KeyDown(object sender, KeyEventArgs e)
        {
            Keys ArrowKey = 0;
            ArrowKey = e.KeyCode;

            if (pause1.Visible)
            {
                if(e.KeyCode == Keys.Escape)
                {
                    pause1.Visible = false;
                    FormView.BringToFront();
                    //FormView.Focus();
                    start = false;
                    FormView.Invalidate(true);
                    FormView.Update();
                    if (char_type == 1)
                    {
                        CharZ_front.BringToFront();
                        CharZ_left.BringToFront();
                        CharZ_right.BringToFront();
                        CharZ_up.BringToFront();
                    }
                    else if (char_type == 2)
                    {
                        any_up.BringToFront();
                        any_down.BringToFront();
                        any_right.BringToFront();
                        any_left.BringToFront();
                    }
                    if (char_type == 3)
                    {
                        down_foot.BringToFront();
                        top_foot.BringToFront();
                        right_foot.BringToFront();
                        left_foot.BringToFront();
                    }
                    

                    finish.BringToFront(); // 도착점 맨앞으로
                    
                    tmr1.Enabled = true;


                    return;
                }

                else
                    return;
            }
                

            if (e.KeyCode == Keys.D1)
            {
                Use_Item(sender, e, Image_array[0], 1);
            }
            else if (e.KeyCode == Keys.D2)
            {
                Use_Item(sender, e, Image_array[1], 2);
            }
            else if (e.KeyCode == Keys.D3)
            {
                Use_Item(sender, e, Image_array[2], 3);
            }
            else if (e.KeyCode == Keys.D4)
            {
                Use_Item(sender, e, Image_array[3], 4);
            }
            else if (e.KeyCode == Keys.D5)
            {
                Use_Item(sender, e, Image_array[4], 5);
            }
            else if (e.KeyCode == Keys.D6)
            {
                Use_Item(sender, e, Image_array[5], 6);
            }
            else if (e.KeyCode == Keys.D7)
            {
                Use_Item(sender, e, Image_array[6], 7);
            }
            else if (e.KeyCode == Keys.D8)
            {
                Use_Item(sender, e, Image_array[7], 8);
                // demon = true; // TEST ONLY
            }
            else if (e.KeyCode == Keys.D9)
            {
                Use_Item(sender, e, Image_array[8], 9);
                // BlackOut(); // TEST ONLY
            }

            if (demon)
            {
                if (e.KeyCode == Keys.Right)
                    ArrowKey = Keys.Left;

                else if (e.KeyCode == Keys.Left)
                    ArrowKey = Keys.Right;

                else if (e.KeyCode == Keys.Up)
                    ArrowKey = Keys.Down;

                else if (e.KeyCode == Keys.Down)
                    ArrowKey = Keys.Up;
            }

            // 오른쪽 방향키 입력
            if (ArrowKey == Keys.Right)
            {
                arr_Key[0] = 1;
                if (ice == true)
                {
                    count_ice++;
                    if (count_ice == 20)
                    {
                        ice = false;
                        count_ice = 0;
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                    }
                    Point pp = new Point(_x, _y);
                    ice_right.Location = pp;
                    ice_right.Size = new System.Drawing.Size(brick_x, brick_y);
                    CharZ_front.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    ice1.Visible = false;
                    ice_left.Visible = false;
                    ice_right.Visible = true;
                    CharZ_right.Visible = false;
                    any_up.Visible = false;
                    any_down.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    down_foot.Visible = false;
                    top_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;

                    ice_right.BringToFront();
                }
                else
                {
                    if (new_brick[char_x + 1, char_y] == 0 || new_brick[char_x + 1, char_y] == 2)   //0은 벽이 없는 부분 2는 아이템 박스 위치
                    {
                        if (speeds)
                        {
                            speed_count++;
                            if (speed_count == speed)
                            {
                                char_x += 1;
                                _x += brick_x;
                                speed_count = 0;
                            }
                        }
                        else
                        {
                            char_x += 1;
                            _x += brick_x;
                        }

                    }
                    Point pp = new Point(_x, _y);
                    ice1.Visible = false;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    if(char_type == 1)
                    {
                        CharZ_right.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_right.Visible = true;
                        CharZ_right.Location = pp;
                        CharZ_right.BringToFront();
                    }
                    else if(char_type == 2)
                    {
                        any_right.Size = new System.Drawing.Size(brick_x, brick_y);
                        any_right.Visible = true;
                        any_right.Location = pp;
                        any_right.BringToFront();
                    }
                    else if(char_type == 3)
                    {
                        right_foot.Size = new System.Drawing.Size(brick_x, brick_y);
                        right_foot.Visible = true;
                        right_foot.Location = pp;
                        right_foot.BringToFront();
                    }
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    CharZ_front.Visible = false;
                    any_up.Visible = false;
                    any_left.Visible = false;
                    any_down.Visible = false;
                    down_foot.Visible = false;
                    left_foot.Visible = false;
                    top_foot.Visible = false;

                }
            }

            // 왼쪽 방향키 입력
            else if (ArrowKey == Keys.Left)
            {
                arr_Key[0] = 2;
                if (ice == true)
                {
                    count_ice++;
                    if (count_ice == 20)
                    {
                        ice = false;
                        count_ice = 0;
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                    }
                    Point pp = new Point(_x, _y);

                    ice_left.Location = pp;
                    ice_left.Size = new System.Drawing.Size(brick_x, brick_y);
                    CharZ_right.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    CharZ_front.Visible = false;
                    any_up.Visible = false;
                    any_down.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    down_foot.Visible = false;
                    top_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;
                    ice1.Visible = false;
                    ice_left.Visible = true;
                    ice_right.Visible = false;
                    ice_left.BringToFront();

                }
                else
                {
                    if (new_brick[char_x - 1, char_y] == 0 || new_brick[char_x - 1, char_y] == 2)
                    {
                        if (speeds)
                        {
                            speed_count++;
                            if (speed_count == speed)
                            {
                                char_x -= 1;
                                _x -= brick_x;
                                speed_count = 0;
                            }
                        }
                        else
                        {
                            char_x -= 1;
                            _x -= brick_x;
                        }

                    }
                    Point pp = new Point(_x, _y);
                    ice1.Visible = false;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    if (char_type == 1)
                    {
                        CharZ_left.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_left.Visible = true;
                        CharZ_left.Location = pp;
                        CharZ_left.BringToFront();
                    }
                    else if (char_type == 2)
                    {
                        any_left.Size = new System.Drawing.Size(brick_x, brick_y);
                        any_left.Visible = true;
                        any_left.Location = pp;
                        any_left.BringToFront();
                    }
                    else if (char_type == 3)
                    {
                        left_foot.Size = new System.Drawing.Size(brick_x, brick_y);
                        left_foot.Visible = true;
                        left_foot.Location = pp;
                        left_foot.BringToFront();
                    }
                    
                    CharZ_right.Visible = false;
                    CharZ_up.Visible = false;
                    CharZ_front.Visible = false;
                    any_right.Visible = false;
                    any_up.Visible = false;
                    any_down.Visible = false;
                    down_foot.Visible = false;
                    top_foot.Visible = false;
                    right_foot.Visible = false;

                }

            }

            // 위쪽 방향키 입력
            else if (ArrowKey == Keys.Up)
            {
                arr_Key[0] = 3;
                if (ice == true)
                {

                    if (count_ice == 20)
                    {
                        ice = false;
                        count_ice = 0;
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                    }
                    Point pp = new Point(_x, _y);
                    ice1.Location = pp;
                    ice1.Size = new System.Drawing.Size(brick_x, brick_y);
                    CharZ_front.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    CharZ_right.Visible = false;
                    any_up.Visible = false;
                    any_down.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    down_foot.Visible = false;
                    top_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;
                    ice1.Visible = true;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    ice1.BringToFront();

                }
                else
                {
                    if (char_y > 0 && new_brick[char_x, char_y - 1] == 0 || new_brick[char_x, char_y - 1] == 2)
                    {
                        if (speeds)
                        {
                            speed_count++;
                            if (speed_count == speed)
                            {
                                char_y -= 1;
                                _y -= brick_y;
                                speed_count = 0;
                            }
                        }
                        else
                        {
                            char_y -= 1;
                            _y -= brick_y;
                        }


                    }

                    Point pp = new Point(_x, _y);
                    if (char_type == 1)
                    {
                        CharZ_up.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_up.Visible = true;
                        CharZ_up.Location = pp;
                        CharZ_up.BringToFront();
                    }
                    else if (char_type == 2)
                    {
                        any_up.Size = new System.Drawing.Size(brick_x, brick_y);
                        any_up.Visible = true;
                        any_up.Location = pp;
                        any_up.BringToFront();
                    }
                    else if (char_type == 3)
                    {
                        top_foot.Size = new System.Drawing.Size(brick_x, brick_y);
                        top_foot.Visible = true;
                        top_foot.Location = pp;
                        top_foot.BringToFront();
                    }
                   
                    ice1.Visible = false;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    CharZ_right.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_front.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    any_down.Visible = false;
                    down_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;
                }

            }
            // 아래쪽 방향키 입력
            else if (ArrowKey == Keys.Down)
            {
                arr_Key[0] = 4;

                if (ice == true)
                {
                    if (count_ice == 20)
                    {
                        ice = false;
                        count_ice = 0;
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                    }

                    Point pp = new Point(_x, _y);
                    ice1.Location = pp;
                    ice1.Size = new System.Drawing.Size(brick_x, brick_y);
                    CharZ_front.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    CharZ_right.Visible = false;
                    any_up.Visible = false;
                    any_down.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    down_foot.Visible = false;
                    top_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;
                    ice1.Visible = true;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    ice1.BringToFront();

                }
                else
                {
                    if (new_brick[char_x, char_y + 1] == 0 || new_brick[char_x, char_y + 1] == 2)
                    {
                        if (speeds)
                        {
                            speed_count++;
                            if (speed_count == speed)
                            {
                                char_y += 1;
                                _y += brick_y;
                                speed_count = 0;
                            }
                        }
                        else
                        {
                            char_y += 1;
                            _y += brick_y;
                        }

                    }

                    Point pp = new Point(_x, _y);
                    if (char_type == 1)
                    {
                        CharZ_front.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_front.Visible = true;
                        CharZ_front.Location = pp;
                        CharZ_front.BringToFront();
                    }
                    else if (char_type == 2)
                    {
                        any_down.Size = new System.Drawing.Size(brick_x, brick_y);
                        any_down.Visible = true;
                        any_down.Location = pp;
                        any_down.BringToFront();
                    }
                    else if (char_type == 3)
                    {
                        down_foot.Size = new System.Drawing.Size(brick_x, brick_y);
                        down_foot.Visible = true;
                        down_foot.Location = pp;
                        down_foot.BringToFront();
                    }
                    ice1.Visible = false;
                    ice_left.Visible = false;
                    ice_right.Visible = false;
                    CharZ_right.Visible = false;
                    CharZ_left.Visible = false;
                    CharZ_up.Visible = false;
                    any_right.Visible = false;
                    any_left.Visible = false;
                    any_up.Visible = false;
                    top_foot.Visible = false;
                    left_foot.Visible = false;
                    right_foot.Visible = false;
                }

            }
            // 일시정지
            else if (e.KeyCode == Keys.Escape)
            {
                //MessageBox.Show("od");
                if (!pause1.Visible)
                {
                    //pause1.BackColor = Color.FromArgb(255, 224, 224, 224);
                    pause1.BackColor = this.BackColor;

                    pause1.BringToFront();
                    pause1.Visible = true;
                    tmr1.Enabled = false;

                    Item_GoBack(); // 아이템상자 맨뒤로
                    finish.SendToBack(); // 도착점 맨뒤로
                    Char_GoBack(); // 캐릭터 맨뒤로
                    

                }
            }
            // activate_Timer();
            if (char_x == (index_x - 2) && char_y == (index_y - 2))
            {
                tmr1.Enabled = false;
                score_Save();
                
                MessageBox.Show("축하합니다");
                this.Close();
            }
        }

        private void Item_GoBack()
        {
            Item1.SendToBack();
            Item2.SendToBack();
            Item3.SendToBack();
            Item4.SendToBack();
            Item5.SendToBack();
            Item6.SendToBack();
            Item7.SendToBack();
            Item8.SendToBack();
            Item9.SendToBack();
            Item10.SendToBack();
        }

        private void Char_GoBack()
        {
            if (char_type == 1)
            {
                CharZ_front.SendToBack();
                CharZ_left.SendToBack();
                CharZ_right.SendToBack();
                CharZ_up.SendToBack();
            }
            else if(char_type == 2)
            {
                any_up.SendToBack();
                any_left.SendToBack();
                any_right.SendToBack();
                any_down.SendToBack();
            }else if(char_type == 3)
            {
                down_foot.SendToBack();
                top_foot.SendToBack();
                left_foot.SendToBack();
                right_foot.SendToBack();
            }
           
        }

        private void score_Save()
        {
            if(level == 1)
            {
                file_path = @"C:\Temp\score_easy.txt";
            } else if(level == 2)
            {
                file_path = @"C:\Temp\score_mid.txt";
            } else
            {
                file_path = @"C:\Temp\score_advanced.txt";
            }
            fs_write = new FileStream(file_path, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs_write, Encoding.UTF8);

            // MessageBox.Show("<" + nickname + " : " + score + "> 저장!");

            sw.WriteLine(nickname + " " + score);
            sw.Close();
            sw.Dispose();
            fs_write.Close();
            fs_write.Dispose();
        }
    }
}
