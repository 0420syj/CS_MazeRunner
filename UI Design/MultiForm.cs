using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ClassLibrary1;
using System.IO;

namespace UI_Design
{
    public partial class MultiForm : Form
    {
        private NetworkStream m_networkstream;
        private TcpClient m_client;
        public Arrive arrive;
        public FileStream fs_write;

        // 패킷 보낼 때 저장하는 버퍼
        private byte[] sendBuffer = new byte[1024 * 4];     
        private byte[] readBuffer = new byte[1024 * 4];

        // 연결 bool
        private bool m_bConnect = false;

        Random rand = new Random();

        string ip;
        int port = 9999;

        private Thread m_thread;

        // 1p인지 2p인지 
        int playerNum;
        Point pp1;
        Point pp2;

        int brick_x;
        int brick_y;
        int char_x;
        int char_y;
        int index_x = 47;
        int index_y = 47;
        bool[,] arr = new bool[1000, 1000];

        // 처음 시작할 때만 paint 위해 사용
        bool ffirst = false;
        bool boom_packet = false;

        int[,] new_brick;

        // 말 위치 좌표
        int _x = 10;
        int _y = 10;
        
        int your_x;
        int your_y;

        int speed = 5; // 초기화 스피드

        int char_width = 50;
        int char_height = 50;
        string nickname;
        int foot_type;
        int map_size_x = 0, map_size_y = 0;

        public setPlayer setplayer;
        public Location location = new Location();
        
        public start _start;
        // 아이템 관련 변수들
        
        int map_select;
        bool Trap_on = false;
        int wall_type;          // 벽 스타일
        int level;              // 미로 난이도 
        int count = 0;
        int speed_count = 0; // 스피드 다운시 방향키 횟수
        bool speeds = false; //스피드 다운 작동
        bool angels = false; // 엔젤 작동
        bool time_down = false; // 타임 다운 작동
        bool ice = false; // 얼음 발동
        bool demon = false; // 악마 발동
        bool black = false; // 블랙아웃 발동
        bool items = false;
        bool jump = false;
        private int time = 0; // 시간 변수
        private int score = 15000; // 점수 변수
        private const int REDUCE = 25; // 감소폭 상수

        //아이템 관련 전역 변수
        int[] arr1 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };       //아이템 배치 배열 x좌표
        int[] arr2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Item_array = new int[26]; //아이템 배열
        bool set_Item = false;          //아이템 배치는 한번만
        int x, y; // 폭탄 위치
        bool boom = false; // 아이템 폭탄 발동 조건
        int[] bomb_array_x = new int[26]; //폭탄 발생 지점 저장
        int[] bomb_array_y = new int[26]; //폭탄 발생 지점 저장
        int count_bomb = 0; // 쓸수 잇는 폭탄갯수
        bool bulldozer = false; // 불도저
        bool eyes_open = false; // 시야확장
        bool start = false;
        int[] Show_Item_Image_x = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Show_Item_Image_y = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        

        int count_ice = 0; //얼음 해동 조건
        int[] arr_Key = { 0 }; //점프 방향
        int[] Image_array = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Item_dup = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };        //아이템 저장소에 아이템 중복을 확인하는 변수
        bool eat = false;
        int width = 400, height = 400;
        int count_Item = 25;             //난이도에 따라 아이템 개수가 변함
       
        int Item_size;          //난이도별 아이템 박스 사이즈 결정
        //파일스트림 관련 전역변수
        public string file_path = @"C:\Temp\score.txt";
        //public FileStream fs_write;
        public StreamWriter sw;
        // 좀비 관련 전역변수
        int _jx;
        int _jy;
        int zombie_speed = 2;

        int demon_cnt = 100;
        int black_cnt = 100;
        int speed_cnt = 100;

        int receive_cnt = 0;

        public MultiForm(string nickname, string ip)
        {
            InitializeComponent();
            this.nickname = nickname;
            this.ip = ip;
        }

        private void MultiForm_Load(object sender, EventArgs e)
        {
            m_client = new TcpClient();

            try
            {
                m_client.Connect(ip, Convert.ToInt32(port));
            }
            catch
            {
                MessageBox.Show("접속 에러");
                return;
            }
            m_bConnect = true;
            m_networkstream = m_client.GetStream();

            connect con = new connect();
            con.Type = (int)PacketType.접속;
            con.nickname = nickname;
            Packet.Serialize(con).CopyTo(this.sendBuffer, 0);
            this.Send();

            m_thread = new Thread(new ThreadStart(RUN));
            m_thread.Start();

            
        }
        private void RUN()
        {
            int nRead;

            while (this.m_bConnect)
            {
                try
                {
                    nRead = 0;
                    nRead = m_networkstream.Read(readBuffer, 0, 1024 * 4);
                }
                catch
                {
                    this.m_bConnect = false;
                    this.m_networkstream = null;
                }

                Packet packet = (Packet)Packet.Desserialize(this.readBuffer);

                switch ((int)packet.Type)
                {
                    case (int)PacketType.할당:
                        {
                            setplayer = (setPlayer)Packet.Desserialize(readBuffer);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                int num;
                                num = setplayer.player;
                                playerNum = num;
                                //MessageBox.Show("" + playerNum);
                                if (playerNum == 1)
                                {
                                    char_x = 1; char_y = 1;
                                    _x = 25; _y = 21;
                                    your_x = 1125; your_y = 945;
                                    pp1 = new Point(_x, _y);
                                    pp2 = new Point(your_x, your_y);

                                }
                                else
                                {
                                    char_x = 45; char_y = 45;
                                    _x = 1125; _y = 945;
                                    your_x = 25; your_y = 21;
                                    pp1 = new Point(_x, _y);
                                    pp2 = new Point(your_x, your_y);

                                }
                                Receive receive = new Receive();
                                receive.Type = (int)PacketType.수신;
                                Packet.Serialize(receive).CopyTo(this.sendBuffer, 0);
                                this.Send();
                            }));
                            //loading1.Enabled = true;
                            loading1.Focus();
                            loading1.BringToFront();
                            break;
                        }
                    case (int)PacketType.시작:
                        {
                            start st = (start)Packet.Desserialize(readBuffer);
                            start start = new start();


                            lbl_1pName.Text = st.p1name;
                            lbl_2pName.Text = st.p2name;
                            if(playerNum == 1)
                            {
                                lbl_1pName.BackColor = Color.Black;
                                lbl_1pName.ForeColor = Color.White;
                            }else if(playerNum == 2)
                            {
                                lbl_2pName.BackColor = Color.Black;
                                lbl_2pName.ForeColor = Color.White;
                            }
                            loading1.Dispose();
                            FormView.Focus();
                            FormView.BringToFront();

                            CharZ_front.Size = new Size(brick_x, brick_y);
                            CharZ_right.Size = new Size(brick_x, brick_y);
                            CharZ_left.Size = new Size(brick_x, brick_y);
                            CharZ_up.Size = new Size(brick_x, brick_y);
                            arr1 = st.arr1;
                            arr2 = st.arr2;
                            CharZ_front.Location = pp1;
                            CharA_front.Location = pp2;
                            CharZ_front.BringToFront();
                            CharA_front.BringToFront();
                            FormView.Invalidate(true);
                            FormView.Update();
                            break;
                        }
                    case (int)PacketType.위치:
                        {
                            location = (Location)Packet.Desserialize(readBuffer);
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                your_x = location._x;
                                your_y = location._y;
                            
                            //MessageBox.Show(playerNum + " " + your_x +" " +  your_y + "");
                            Point p2 = new Point(your_x, your_y);
                            int type = location.foot_type;

                            if (location.boom_x != -1 && location.boom_y != -1)
                            {
                                if (location.boom_x > 0)
                                {
                                    if (location.boom_x - 1 != 0 )
                                        new_brick[location.boom_x - 1, location.boom_y] = 0;
                                }
                                if (location.boom_x < 46)            //여기는 보류
                                {
                                    if(location.boom_x + 1!= 46)
                                        new_brick[location.boom_x + 1, location.boom_y] = 0;
                                }
                                if (location.boom_y > 0)
                                {
                                        if(location.boom_y - 1 != 0)
                                        new_brick[location.boom_x, location.boom_y - 1] = 0;
                                }
                                if (location.boom_y < 46)
                                {
                                        if(location.boom_y + 1!= 46)
                                           new_brick[location.boom_x, location.boom_y + 1] = 0;
                                }
                                FormView.Invalidate(true);
                                FormView.Update();

                            }
                            if (location.box_num != -1)
                            {
                                if (location.box_num == 0)
                                {
                                    Item1.Visible = false;
                                }
                                if (location.box_num == 1)
                                {
                                    Item2.Visible = false;
                                }
                                if (location.box_num == 2)
                                {
                                    Item3.Visible = false;
                                }
                                if (location.box_num == 3)
                                {
                                    Item4.Visible = false;
                                }
                                if (location.box_num == 4)
                                {
                                    Item5.Visible = false;
                                }
                                if (location.box_num == 5)
                                {
                                    Item6.Visible = false;
                                }
                                if (location.box_num == 6)
                                {
                                    Item7.Visible = false;
                                }
                                if (location.box_num == 7)
                                {
                                    Item8.Visible = false;
                                }
                                if (location.box_num == 8)
                                {
                                    Item9.Visible = false;
                                }
                                if (location.box_num == 9)
                                {
                                    Item10.Visible = false;
                                }
                                if (location.box_num == 10)
                                {
                                    Item11.Visible = false;
                                }
                                if (location.box_num == 11)
                                {
                                    Item12.Visible = false;
                                }
                                if (location.box_num == 12)
                                {
                                    Item13.Visible = false;
                                }
                                if (location.box_num == 13)
                                {
                                    Item14.Visible = false;
                                }
                                if (location.box_num == 14)
                                {
                                    Item15.Visible = false;
                                }
                                if (location.box_num == 15)
                                {
                                    Item16.Visible = false;
                                }
                                if (location.box_num == 16)
                                {
                                    Item17.Visible = false;
                                }
                                if (location.box_num == 17)
                                {
                                    Item18.Visible = false;
                                }
                                if (location.box_num == 18)
                                {
                                    Item19.Visible = false;
                                }
                                if (location.box_num == 19)
                                {
                                    Item20.Visible = false;
                                }
                                if (location.box_num == 20)
                                {
                                    Item21.Visible = false;
                                }
                                if (location.box_num == 21)
                                {
                                    Item22.Visible = false;
                                }
                                if (location.box_num == 22)
                                {
                                    Item23.Visible = false;
                                }
                                if (location.box_num == 23)
                                {
                                    Item24.Visible = false;
                                }
                                if (location.box_num == 24)
                                {
                                    Item25.Visible = false;
                                }

                            }


                                switch (type)
                                {
                                    case 1:
                                        CharA_left.Visible = false;
                                        CharA_up.Visible = false;
                                        CharA_front.Visible = false;
                                        CharA_right.Visible = true;
                                        CharA_right.Location = p2;
                                        CharA_right.BringToFront();
                                        break;
                                    case 2:
                                        CharA_left.Visible = true;
                                        CharA_up.Visible = false;
                                        CharA_front.Visible = false;
                                        CharA_right.Visible = false;
                                        CharA_left.Location = p2;
                                        CharA_left.BringToFront();
                                        break;
                                    case 3:
                                        CharA_left.Visible = false;
                                        CharA_up.Visible = true;
                                        CharA_front.Visible = false;
                                        CharA_right.Visible = false;
                                        CharA_up.Location = p2;
                                        CharA_up.BringToFront();
                                        break;
                                    case 4:
                                        CharA_left.Visible = false;
                                        CharA_up.Visible = false;
                                        CharA_front.Visible = true;
                                        CharA_right.Visible = false;
                                        CharA_front.Location = p2;
                                        CharA_front.BringToFront();
                                        break;
                                }
                            }));
                            break;
                        }
                    case (int)PacketType.도착:
                        {
                            arrive = (Arrive)Packet.Desserialize(readBuffer);
                            if(receive_cnt==0)
                            {
                                if (arrive.player == playerNum)
                                {
                                    MessageBox.Show("You win");
                                    score_Save(playerNum);
                                    this.Close();

                                }
                                else
                                {
                                    MessageBox.Show("You lose");
                                    score_Save(playerNum % 2 + 1);
                                    this.Close();
                                }
                            }
                            receive_cnt++;
                            break;
                        }
                    
                }

            }
        }
        // 패킷 보내는 함수 
        public void Send()
        {
            m_networkstream.Write(this.sendBuffer, 0, this.sendBuffer.Length);
            m_networkstream.Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                sendBuffer[i] = 0;
            }
        }
        //----------------------아이템 함수들-------------------//
        //------------------------------------------------------//
        //-                                                     //
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
                location.Type = (int)PacketType.위치;
                if (boom_packet == true)
                {
                    boom_packet = false;
                    location.boom_x = char_x;
                    location.boom_y = char_y;
                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);
                    this.Send();
                }
                boom = false;

            }
        }

        private void Show_Bomb(PaintEventArgs e) // 벽파괴 범위 그려주고 파괴시키기
        {
            Image imageWall = Image.FromFile("wall.png");
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

        // 키 입력
        private void MultiForm_KeyDown(object sender, KeyEventArgs e)
        {
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
            
            Keys ArrowKey = 0;
            ArrowKey = e.KeyCode;
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
                this.Invoke(new MethodInvoker(delegate ()
                {
                    foot_type = 1;
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
                            trap_ItemBox.Visible = true;
                            trap_ItemBox.BringToFront();
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
                        CharZ_right.Visible = true;
                        CharZ_right.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = false;
                        CharZ_front.Visible = false;
                        CharZ_right.Location = pp;
                        CharZ_right.BringToFront();
                        location = new Location();
                        location.Type = (int)PacketType.위치;

                        location.player = playerNum;
                        location._x = _x;
                        location._y = _y;
                        location.foot_type = 1;
                        Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                        this.Send();
                    }
                }));
            }


            // 왼쪽 방향키 입력
            else if (ArrowKey == Keys.Left)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    foot_type = 2;
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
                            trap_ItemBox.Visible = true;
                            trap_ItemBox.BringToFront();
                        }
                        Point pp = new Point(_x, _y);

                        ice_left.Location = pp;
                        ice_left.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_right.Visible = false;
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = false;
                        CharZ_front.Visible = false;
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
                        CharZ_left.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_right.Visible = false;
                        CharZ_left.Visible = true;
                        CharZ_up.Visible = false;
                        CharZ_front.Visible = false;
                        CharZ_left.Location = pp;
                        CharZ_left.BringToFront();
                    }

                    location = new Location();
                    location.Type = (int)PacketType.위치;
                    location.player = playerNum;
                    location._x = _x;
                    location._y = _y;
                    location.foot_type = 2;
                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                    this.Send();

                }));
            }

            // 위쪽 방향키 입력
            else if (ArrowKey == Keys.Up)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    foot_type = 3;
                    if (ice == true)
                    {

                        if (count_ice == 20)
                        {
                            ice = false;
                            count_ice = 0;
                            ice1.Visible = false;
                            ice_left.Visible = false;
                            ice_right.Visible = false;
                            trap_ItemBox.Visible = true;
                            trap_ItemBox.BringToFront();
                        }
                        Point pp = new Point(_x, _y);
                        ice1.Location = pp;
                        ice1.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_front.Visible = false;
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = false;
                        CharZ_right.Visible = false;
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
                        CharZ_up.Size = new System.Drawing.Size(brick_x, brick_y);
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                        CharZ_right.Visible = false;
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = true;
                        CharZ_front.Visible = false;
                        CharZ_up.Location = pp;
                        CharZ_up.BringToFront();
                    }
                    location = new Location();
                    location.Type = (int)PacketType.위치;
                    location.player = playerNum;
                    location._x = _x;
                    location._y = _y;
                    location.foot_type = 3;
                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                    this.Send();
                }));

            }
            // 아래쪽 방향키 입력
            else if (ArrowKey == Keys.Down)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    foot_type = 4;

                    if (ice == true)
                    {
                        if (count_ice == 20)
                        {
                            ice = false;
                            count_ice = 0;
                            ice1.Visible = false;
                            ice_left.Visible = false;
                            ice_right.Visible = false;
                            trap_ItemBox.Visible = true;
                            trap_ItemBox.BringToFront();
                        }

                        Point pp = new Point(_x, _y);
                        ice1.Location = pp;
                        ice1.Size = new System.Drawing.Size(brick_x, brick_y);
                        CharZ_front.Visible = false;
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = false;
                        CharZ_right.Visible = false;
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
                        CharZ_front.Size = new System.Drawing.Size(brick_x, brick_y);
                        ice1.Visible = false;
                        ice_left.Visible = false;
                        ice_right.Visible = false;
                        CharZ_right.Visible = false;
                        CharZ_left.Visible = false;
                        CharZ_up.Visible = false;
                        CharZ_front.Visible = true;
                        CharZ_front.Location = pp;
                        CharZ_front.BringToFront();
                    }
                    location = new Location();
                    location.Type = (int)PacketType.위치;
                    location.player = playerNum;
                    location._x = _x;
                    location._y = _y;
                    location.foot_type = 4;
                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                    this.Send();
                }));
            }
            activate_Timer();
            if (char_x == (index_x / 2) && char_y == (index_y / 2))
            {
                arrive = new Arrive();
                arrive.player = playerNum;
                arrive.Type = (int)PacketType.도착;
                Packet.Serialize(arrive).CopyTo(this.sendBuffer, 0);
                this.Send();
                
            }
        }
         private void score_Save(int win_player)
        {
           
            file_path = @"C:\Temp\score_multi.txt";
            
            fs_write = new FileStream(file_path, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs_write, Encoding.UTF8);

            if (playerNum == 1 && win_player == 1)
            {
                sw.WriteLine(lbl_1pName.Text + " " + lbl_2pName.Text + " " + lbl_1pName.Text+ "승");
            }
            else if (playerNum == 1 && win_player == 2)
            {
                sw.WriteLine(lbl_1pName.Text + " " + lbl_2pName.Text + " " + lbl_1pName.Text + "패");
            }
            else if (playerNum == 2 && win_player == 2)
            {
                sw.WriteLine(lbl_2pName.Text + " " + lbl_1pName.Text + " " + lbl_2pName.Text + "승");
            }
            else if (playerNum == 2 && win_player == 1)
            {
                sw.WriteLine(lbl_2pName.Text + " " + lbl_1pName.Text + " " + lbl_2pName.Text + "패");
            }
            sw.Close();
            sw.Dispose();
            fs_write.Close();
            fs_write.Dispose();
        }
        private void MultiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_client.Close();
            m_networkstream.Close();
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            map_size_x = 1182;
            map_size_y = 1004;

            CharZ_front.Size = new System.Drawing.Size(brick_x, brick_y);
            CharZ_left.Size = new System.Drawing.Size(brick_x, brick_y);
            CharZ_right.Size = new System.Drawing.Size(brick_x, brick_y);
            CharZ_up.Size = new System.Drawing.Size(brick_x, brick_y);
            CharA_front.Size = new System.Drawing.Size(brick_x, brick_y);
            CharA_right.Size = new System.Drawing.Size(brick_x, brick_y);
            CharA_left.Size = new System.Drawing.Size(brick_x, brick_y);
            CharA_up.Size = new System.Drawing.Size(brick_x, brick_y);
            

            if (ffirst == false)
            {
                level1_Miro(sender, e);
                ffirst = true;
                CharZ_front.Visible = true;
                CharA_front.Visible = false;
                CharA_right.Visible = false;
                CharA_up.Visible = false;
                CharA_left.Visible = false;
                //down_foot.BringToFront();
                brick_x = 25;
                brick_y = 21;
                index_x = 47; index_y = 47;
            }
            if (boom == true)
            {
                Destroy_Maze(sender, e);

            }
            if (black != true)
                Show_Bomb(e);
            Draw_Item(e);
            Inventory(e);
            Eat_Item(e);

            finish.Visible = true;
            finish.Size = new System.Drawing.Size(brick_x, brick_y);
            finish.Location = new Point((index_x / 2) * brick_x,(index_y / 2) * brick_y);
            finish.BringToFront();
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
        private void Random_Item(PaintEventArgs e)          //랜덤으로 아이템 배치 좌표 구하기
        {
            Item_size = 15;
            Item1.Size = new System.Drawing.Size(15, 15);
            Item2.Size = new System.Drawing.Size(15, 15);
            Item3.Size = new System.Drawing.Size(15, 15);
            Item4.Size = new System.Drawing.Size(15, 15);
            Item5.Size = new System.Drawing.Size(15, 15);
            Item6.Size = new System.Drawing.Size(15, 15);
            Item7.Size = new System.Drawing.Size(15, 15);
            Item8.Size = new System.Drawing.Size(15, 15);
            Item9.Size = new System.Drawing.Size(15, 15);
            Item10.Size = new System.Drawing.Size(15, 15);
            Item11.Size = new System.Drawing.Size(15, 15);
            Item12.Size = new System.Drawing.Size(15, 15);
            Item13.Size = new System.Drawing.Size(15, 15);
            Item14.Size = new System.Drawing.Size(15, 15);
            Item15.Size = new System.Drawing.Size(15, 15);
            Item16.Size = new System.Drawing.Size(15, 15);
            Item17.Size = new System.Drawing.Size(15, 15);
            Item18.Size = new System.Drawing.Size(15, 15);
            Item19.Size = new System.Drawing.Size(15, 15);
            Item20.Size = new System.Drawing.Size(15, 15);
            Item21.Size = new System.Drawing.Size(15, 15);
            Item22.Size = new System.Drawing.Size(15, 15);
            Item23.Size = new System.Drawing.Size(15, 15);
            Item24.Size = new System.Drawing.Size(15, 15);
            Item25.Size = new System.Drawing.Size(15, 15);
            Item1.Visible = true;
            Item2.Visible = true;
            Item3.Visible = true;
            Item4.Visible = true;
            Item5.Visible = true;
            Item6.Visible = true;
            Item7.Visible = true;
            Item8.Visible = true;
            Item8.Visible = true;
            Item10.Visible = true;
            Item11.Visible = true;
            Item12.Visible = true;
            Item13.Visible = true;
            Item14.Visible = true;
            Item15.Visible = true;
            Item16.Visible = true;
            Item17.Visible = true;
            Item18.Visible = true;
            Item19.Visible = true;
            Item20.Visible = true;
            Item21.Visible = true;
            Item22.Visible = true;
            Item23.Visible = true;
            Item24.Visible = true;
            Item25.Visible = true;
            trap_ItemBox.Location = new Point(1240, 600);                       //함정
            trap_ItemBox.Visible = true;
            trap_ItemBox.BringToFront();

        }

        private void Draw_Item(PaintEventArgs e)        //Random_Item에서 구한 좌표에다가 아이템 배치
        {
            if (set_Item == false)
            {
                Random_Item(e);
                set_Item = true;
            }
            Item1.Location = new Point(arr1[0] * brick_x, arr2[0] * brick_y);
            Item2.Location = new Point(arr1[1] * brick_x, arr2[1] * brick_y);
            Item3.Location = new Point(arr1[2] * brick_x, arr2[2] * brick_y);
            Item4.Location = new Point(arr1[3] * brick_x, arr2[3] * brick_y);
            Item5.Location = new Point(arr1[4] * brick_x, arr2[4] * brick_y);
            Item6.Location = new Point(arr1[5] * brick_x, arr2[5] * brick_y);
            Item7.Location = new Point(arr1[6] * brick_x, arr2[6] * brick_y);
            Item8.Location = new Point(arr1[7] * brick_x, arr2[7] * brick_y);
            Item9.Location = new Point(arr1[8] * brick_x, arr2[8] * brick_y);
            Item10.Location = new Point(arr1[9] * brick_x, arr2[9] * brick_y);
            Item11.Location = new Point(arr1[10] * brick_x, arr2[10] * brick_y);
            Item12.Location = new Point(arr1[11] * brick_x, arr2[11] * brick_y);
            Item13.Location = new Point(arr1[12] * brick_x, arr2[12] * brick_y);
            Item14.Location = new Point(arr1[13] * brick_x, arr2[13] * brick_y);
            Item15.Location = new Point(arr1[14] * brick_x, arr2[14] * brick_y);
            Item16.Location = new Point(arr1[15] * brick_x, arr2[15] * brick_y);
            Item17.Location = new Point(arr1[16] * brick_x, arr2[16] * brick_y);
            Item18.Location = new Point(arr1[17] * brick_x, arr2[17] * brick_y);
            Item19.Location = new Point(arr1[18] * brick_x, arr2[18] * brick_y);
            Item20.Location = new Point(arr1[19] * brick_x, arr2[19] * brick_y);
            Item21.Location = new Point(arr1[20] * brick_x, arr2[20] * brick_y);
            Item22.Location = new Point(arr1[21] * brick_x, arr2[21] * brick_y);
            Item23.Location = new Point(arr1[22] * brick_x, arr2[22] * brick_y);
            Item24.Location = new Point(arr1[23] * brick_x, arr2[23] * brick_y);
            Item25.Location = new Point(arr1[24] * brick_x, arr2[24] * brick_y);
            Item1.BringToFront();
            Item2.BringToFront();
            Item3.BringToFront();
            Item4.BringToFront();
            Item5.BringToFront();
            Item6.BringToFront();
            Item7.BringToFront();
            Item8.BringToFront();
            Item9.BringToFront();
            Item10.BringToFront();
            Item11.BringToFront();
            Item12.BringToFront();
            Item13.BringToFront();
            Item14.BringToFront();
            Item15.BringToFront();
            Item16.BringToFront();
            Item17.BringToFront();
            Item18.BringToFront();
            Item19.BringToFront();
            Item20.BringToFront();
            Item21.BringToFront();
            Item22.BringToFront();
            Item23.BringToFront();
            Item24.BringToFront();
            Item25.BringToFront();
        }

        private void Use_Item(object sender, KeyEventArgs e, int key, int num)        //아이템 사용   key: 키보드 전달 숫자
        {
            if (num == 1)
                ItemBox1.Visible = true;
            if (num == 2)
                ItemBox2.Visible = true;

            Show_Item_Image_x[num - 1] = 0;
            Show_Item_Image_y[num - 1] = 0;
            if (Image_array[num - 1] == 1)      //점프
            {
                if (char_x - 2 > 0 && char_x + 2 < index_x && char_y - 2 > 0 && char_y + 2 < index_y)
                {
                    jump = true;
                    if (foot_type == 1)        //오른쪽
                    {
                        if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                        {
                            speed_count = speed;
                            char_x += 2;
                            jump = false;
                            _x += brick_x * 2;
                            CharZ_right.Location = new Point(_x, _y);
                            CharZ_right.BringToFront();
                            location = new Location();
                            location.Type = (int)PacketType.위치;

                            location.player = playerNum;
                            location._x = _x;
                            location._y = _y;
                            location.foot_type = 1;
                            Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                            this.Send();
                        }
                    }
                    else if (foot_type == 2)
                    {
                        if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                        {
                            speed_count = speed;
                            char_x -= 2;
                            jump = false;
                            _x -= brick_x * 2;
                            CharZ_left.Location = new Point(_x, _y);
                            CharZ_left.BringToFront();
                            location = new Location();
                            location.Type = (int)PacketType.위치;

                            location.player = playerNum;
                            location._x = _x;
                            location._y = _y;
                            location.foot_type = 2;
                            Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                            this.Send();
                        }
                    }
                    else if (foot_type == 3)               //위
                    {
                        if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                        {
                            speed_count = speed;
                            char_y -= 2;
                            jump = false;
                            _y -= brick_y * 2;
                            CharZ_up.Location = new Point(_x, _y);
                            CharZ_up.BringToFront();
                            location = new Location();
                            location.Type = (int)PacketType.위치;

                            location.player = playerNum;
                            location._x = _x;
                            location._y = _y;
                            location.foot_type = 3;
                            Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                            this.Send();
                        }

                    }
                    else if (foot_type == 4)
                    {
                        if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                        {
                            speed_count = speed;
                            char_y += 2;
                            jump = false;
                            _y += brick_y * 2;
                            CharZ_front.Location = new Point(_x, _y);
                            CharZ_front.BringToFront();
                            location = new Location();
                            location.Type = (int)PacketType.위치;

                            location.player = playerNum;
                            location._x = _x;
                            location._y = _y;
                            location.foot_type = 4;
                            Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                            this.Send();
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
                            if (foot_type == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 1;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }

                            else if (foot_type == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 3;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                            else if (foot_type == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 4;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                        }
                        else if (char_y + 2 >= index_y && char_y - 2 > 0)
                        {
                            jump = true;
                            if (foot_type == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 1;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }

                            else if (foot_type == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 3;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                        }
                        else if (char_y + 2 < index_y && char_y - 2 <= 0)
                        {
                            jump = true;
                            if (foot_type == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 1;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 4;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
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

                            if (foot_type == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 2;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 3;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }

                        }
                        else if (char_y + 2 < index_y && char_y - 2 <= 0)
                        {
                            jump = true;

                            if (foot_type == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 2;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 4;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                            
                        }
                        /* 수정코드 여기서 부터*/
                        else
                        {
                            jump = true;
                            if (foot_type == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 2;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 3;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                            else if (foot_type == 4)
                            {
                                if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y += 2;
                                    jump = false;
                                    _y += brick_y * 2;
                                    CharZ_front.Location = new Point(_x, _y);
                                    CharZ_front.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 4;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                        }
                        /* 여기까지 입니다.*/
                    }
                    //오
                    else if (char_y + 2 >= index_y)
                    {
                        if (char_y - 2 <= 0)
                        {
                            jump = true;
                            if (foot_type == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 1;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 2;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }

                        }
                        else
                        {
                            jump = true;
                            if (foot_type == 1)        //오른쪽
                            {
                                if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                                {
                                    speed_count = speed;
                                    char_x += 2;
                                    jump = false;
                                    _x += brick_x * 2;
                                    CharZ_right.Location = new Point(_x, _y);
                                    CharZ_right.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 1;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 2)
                            {
                                if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                                {
                                    speed_count = speed;
                                    char_x -= 2;
                                    jump = false;
                                    _x -= brick_x * 2;
                                    CharZ_left.Location = new Point(_x, _y);
                                    CharZ_left.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 2;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }
                            }
                            else if (foot_type == 3)               //위
                            {
                                if (jump == true && new_brick[char_x, char_y - 1] == 1 && new_brick[char_x, char_y - 2] == 0)
                                {
                                    speed_count = speed;
                                    char_y -= 2;
                                    jump = false;
                                    _y -= brick_y * 2;
                                    CharZ_up.Location = new Point(_x, _y);
                                    CharZ_up.BringToFront();
                                    location = new Location();
                                    location.Type = (int)PacketType.위치;

                                    location.player = playerNum;
                                    location._x = _x;
                                    location._y = _y;
                                    location.foot_type = 3;
                                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                    this.Send();
                                }

                            }
                        }



                    }
                    //아
                    else if (char_y - 2 <= 0)
                    {
                        jump = true;
                        if (foot_type == 1)        //오른쪽
                        {
                            if (jump == true && new_brick[char_x + 1, char_y] == 1 && new_brick[char_x + 2, char_y] == 0)
                            {
                                speed_count = speed;
                                char_x += 2;
                                jump = false;
                                _x += brick_x * 2;
                                CharZ_right.Location = new Point(_x, _y);
                                CharZ_right.BringToFront();
                                location = new Location();
                                location.Type = (int)PacketType.위치;

                                location.player = playerNum;
                                location._x = _x;
                                location._y = _y;
                                location.foot_type = 1;
                                Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                this.Send();
                            }
                        }
                        else if (foot_type == 2)
                        {
                            if (jump == true && new_brick[char_x - 1, char_y] == 1 && new_brick[char_x - 2, char_y] == 0)   //왼쪽
                            {
                                speed_count = speed;
                                char_x -= 2;
                                jump = false;
                                _x -= brick_x * 2;
                                CharZ_left.Location = new Point(_x, _y);
                                CharZ_left.BringToFront();
                                location = new Location();
                                location.Type = (int)PacketType.위치;

                                location.player = playerNum;
                                location._x = _x;
                                location._y = _y;
                                location.foot_type = 2;
                                Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                this.Send();
                            }
                        }

                        else if (foot_type == 4)
                        {
                            if (jump == true && new_brick[char_x, char_y + 1] == 1 && new_brick[char_x, char_y + 2] == 0)
                            {
                                speed_count = speed;
                                char_y += 2;
                                jump = false;
                                _y += brick_y * 2;
                                CharZ_front.Location = new Point(_x, _y);
                                CharZ_front.BringToFront();
                                location = new Location();
                                location.Type = (int)PacketType.위치;

                                location.player = playerNum;
                                location._x = _x;
                                location._y = _y;
                                location.foot_type = 4;
                                Packet.Serialize(location).CopyTo(this.sendBuffer, 0);

                                this.Send();
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
                boom = true;
                Image_array[num - 1] = 0;
                bomb.Visible = false;
                boom_packet = true;
                FormView.Invalidate(true);
                FormView.Update();
            }
            else if (Image_array[num - 1] == 3)
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
                    speeds = false;
                }
                angel.Visible = false;
                angels = false;
                Image_array[num - 1] = 0;
            }   //천사
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
            if (Image_array[0] != 0 && Image_array[1] != 0)
                return;
            for (int i = 0; i < count_Item; i++)
            {
                if (arr1[i] == char_x && arr2[i] == char_y)              //아이템이랑 말의 좌표가 같다면
                {
                    //                    MessageBox.Show(arr1[i].ToString() + " " + char_x.ToString() + " " + arr2[i].ToString() + " " + char_y.ToString());
                    arr1[i] = -50;              //먹으면 아이템 위치 이상한 곳으로
                    arr2[i] = -50;

                    location.box_num = i;
                    location.Type = (int)PacketType.위치;
                    Packet.Serialize(location).CopyTo(this.sendBuffer, 0);
                    this.Send();
                    eat = true;
                    for (int j = 0; j < count_Item; j++)
                    {
                        if (Show_Item_Image_y[j] == 0)
                        {
                            if (ItemTrap() == 0)        //함정이 아니라면
                            {
                                Show_Item_Image_x[j] = 1240;                    //함정이라면 패스
                                Show_Item_Image_y[j] = 100 + j * 60;
                            }
                            Item_list(e);
                            break;
                        }
                    }
                }
            }
        }

        private int count_dup(int n)
        {
            if (n >= 1 && n <= 10)      //점프
            {
                return 1;
            }
            else if (n > 10 && n <= 20)   //폭탄
            {
                return 2;
            }
            else if (n > 20 && n <= 35)   //천사
            {
                return 3;
            }
            else if (n > 35 && n <= 65)   //맵다 가리기
            {
                return 4;
            }
            else if (n > 65 && n <= 79)   //악마
            {
                return 5;
            }
            else if (n > 79 && n <= 89)   //속도 느리게하기
            {
                return 6;
            }
            else if (n > 89 && n <= 99)   //얼음
            {
                return 7;
            }
            else if (n > 99 && n <= 100)   //초기화
            {
                return 8;
            }
            return 0;
        }

        private void BlackOut()
        {
            black = true;
            FormView.BackColor = Color.Black;
        }

        private void BlackOut_Off()
        {
            black = false;
            FormView.BackColor = Color.Transparent;
        }

        private void Trap(int n)
        {
            trap_ItemBox.Visible = false;
            if (n == 4)    //맵 다 가리기
            {
                devil.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                time_decrease.Visible = false;
                hide.Visible = true;
                hide.Location = new Point(1240, 600);
                BlackOut();
            }

            else if (n == 5)   //악마
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                devil.Visible = true;
                devil.Location = new Point(1240, 600);
                demon = true;
            }
            else if (n == 6)   //속도 느리게 하기
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                devil.Visible = false;
                ices.Visible = false;
                reset.Visible = false;
                go_slow.Visible = true;
                go_slow.Location = new Point(1240, 600);
                speeds = true;
            }
            else if (n == 7)  //얼음
            {
                hide.Visible = false;
                time_decrease.Visible = false;
                devil.Visible = false;
                go_slow.Visible = false;
                reset.Visible = false;
                ices.Visible = true;
                ices.Location = new Point(1240, 600);
                ice = true;
            }
            else if (n == 8)       //초기화
            {
                time_decrease.Visible = false;
                devil.Visible = false;
                go_slow.Visible = false;
                ices.Visible = false;
                hide.Visible = false;
                reset.Visible = true;
                reset.Location = new Point(1240, 600);
                if (playerNum == 1)
                {
                    char_x = 1; char_y = 1;
                    _x = 25; _y = 21;
                    your_x = 1125; your_y = 945;
                    pp1 = new Point(_x, _y);
                    pp2 = new Point(your_x, your_y);
                }
                else
                {
                    char_x = 45; char_y = 45;
                    _x = 1125; _y = 945;
                    your_x = 25; your_y = 21;
                    pp1 = new Point(_x, _y);
                    pp2 = new Point(your_x, your_y);

                }
                Point pp = new Point(_x, _y);
                CharZ_front.Size = new System.Drawing.Size(brick_x, brick_y);
                CharZ_right.Visible = false;
                CharZ_left.Visible = false;
                CharZ_up.Visible = false;
                CharZ_front.Visible = true;
                CharZ_front.Location = pp;
                CharZ_front.BringToFront();
                //                reset.Visible = false;
                //                trap_ItemBox.Visible = true;
            }


        }
        private int ItemTrap()
        {
            int ret = 0;
            if (eat == true)
            {
                while (true)
                {
                    int n = rand.Next(1, 101);        //아이템을 랜덤으로 정하기
                    n = count_dup(n);
                    if (n >= 4 && Trap_on == true)
                        continue;
                    else if (n >= 4)      //함정이 걸린다면
                    {
                        Trap(n);
                        ret = 1;
                        eat = false;
                        return ret;
                    }
                    else
                    {
                        if (Image_array[0] == n || n == Image_array[1] || Image_array[2] == n || Image_array[3] == n || Image_array[4] == n)
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
                        break;
                    }
                }
                eat = false;
            }
            return ret;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (demon)
            {
                lbl_TrapTime.Text = Convert.ToString(demon_cnt / 10 + "." + demon_cnt % 10 + "초");
                demon_cnt--;
                Trap_on = true;
                if (demon_cnt == 0) // 10초가 다 지나면 효과 해제, 각 함정마다 타이머 int 변수 따로 지정
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
                if (black_cnt == 0) // 10초가 다 지나면 효과 해제, 각 함정마다 타이머 int 변수 따로 지정
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
                if (speed_cnt == 0)
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
            if (!timer1.Enabled)
                timer1.Enabled = true; // 작동시작
        }
        private int Item_list(PaintEventArgs e)     //아이템이 중복되면 문제가 생김(중복이 생기지 않도록 저장할 수 있는 아이템에 같은 아이템이 들어올 수 있도록 해놨음)
        {
            //i: 몇번째 아이템 박스를 먹었는지 저장
            int ret = 0;
            for (int j = 0; j < count_Item; j++)
            {
                if (Show_Item_Image_y[j] == 100)
                    ItemBox1.Visible = false;
                if (Show_Item_Image_y[j] == 160)
                    ItemBox2.Visible = false;
                //                MessageBox.Show(Show_Item_Image_x[0].ToString() + " " + Show_Item_Image_y[0].ToString() + " " + Show_Item_Image_x[1].ToString() + " " + Show_Item_Image_y[1].ToString() + " " + Show_Item_Image_x[2].ToString() + " " + Show_Item_Image_y[2].ToString() + " " );

                if (Image_array[j] == 1 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                     //점프 아이템
                {
                    jumping.Visible = true;
                    jumping.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
                else if (Image_array[j] == 2 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //폭탄 아이템
                {
                    bomb.Visible = true;
                    bomb.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
                else if (Image_array[j] == 3 && Show_Item_Image_x[j] != 0 && Show_Item_Image_y[j] != 0)                //함정 해제
                {
                    angel.Visible = true;
                    angel.Location = new Point(Show_Item_Image_x[j], Show_Item_Image_y[j]);
                }
            }
            return ret;
        }
        private void Inventory(PaintEventArgs e)
        {
            Point inven = new Point(1240, 0);
            Inven.Visible = true;
            Inven.Location = inven;
            if (Show_Item_Image_y[0] == 0)
            {
                ItemBox1.Visible = true;
                ItemBox1.Location = new Point(1240, 100);       //인벤토리 창에 뜨는 아이템 박스
            }
            if (Show_Item_Image_y[1] == 0)
            {
                ItemBox2.Visible = true;
                ItemBox2.Location = new Point(1240, 160);
            }
        }

        //-----------------------------------//
        //                                   //
        //               맵                  //
        //-----------------------------------//

        private void level1_Miro(object sender, PaintEventArgs e)       //초급 벽
        {
            Image imageWall = Image.FromFile("wall.png");
            new_brick = new int[47, 47] {
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1},
{1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1},
{1,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1},
{1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1},
{1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1},
{1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1},
{1,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1},
{1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1},
{1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1},
{1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1},
{1,0,1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1},
{1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1},
{1,0,1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1},
{1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1},
{1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1},
{1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1},
{1,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1},
{1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1},
{1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1},
{1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1},
{1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1},
{1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1},
{1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
{1,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,1},
{1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1},
{1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };
            for (int i = 0; i < 47; i++)
            {
                for (int j = 0; j < 47; j++)
                {
                    if (new_brick[i, j] == 1)
                    {
                        e.Graphics.DrawImage(imageWall, 25 * i, 21 * j, 25, 21);

                    }
                }
            }
        }
    }
}
