using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using ClassLibrary1;

namespace Server
{
    public partial class Form1 : Form
    {
        private NetworkStream[] m_networkstream = new NetworkStream[3];
        private TcpListener server;
        private TcpClient client;
        public Arrive arrive;
        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] readBuffer = new byte[1024 * 4];

        private bool m_bClientOn = false;
        private Thread m_thread;
        private Thread m_serverThread;

        public string name1;
        public string name2;

        private int[] arr1 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] arr2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private int playerCount = 0;
        private int thisPlayer = 0;
        private int playerNum;
        Random rand = new Random();

        public Location location;
        public connect con;
        public Item bombs;

        private void random_Item()
        {
            for (int i = 0; i < 25; i++)
            {
                while (true)
                {
                    bool dup = false;
                    int a, b;
                    a = rand.Next(1, 47);
                    b = rand.Next(1, 47);
                    arr1[i] = a;
                    arr2[i] = b;
                    for (int j = 0; j < i; j++)
                    {
                        if (arr1[j] == a && arr2[j] == b)
                        {
                            dup = true;
                            break;
                        }
                    }
                    if (dup == true)
                    {
                        dup = false;
                        continue;
                    }
                    if (new_brick[arr1[i], arr2[i]] == 0)
                    {
                        break;
                    }
                }
            }
        }
        private int[,] new_brick = new int[,] {
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
        private void ServerStart()
        {
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ClientIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ClientIP = host.AddressList[i].ToString();
                }
            }

            txtIp.Text += ClientIP;
            IPAddress localAddr = IPAddress.Parse(ClientIP);
            server = new TcpListener(localAddr, 9999);
            server.Start();
            while (true)
            {
                client = server.AcceptTcpClient();
                if (client.Connected)
                {
                    thisPlayer++;

                    //MessageBox.Show(thisPlayer + "");
                    m_bClientOn = true;
                    //this.Invoke(new MethodInvoker(delegate ()
                    //{
                    //this.txtLog.Text += thisPlayer + "p 접속\r\n";
                    //}));

                    m_networkstream[thisPlayer] = client.GetStream();

                    if(thisPlayer == 1)
                    {
                        m_thread = new Thread(new ThreadStart(RUN1));
                        m_thread.Start();
                    }
                    if(thisPlayer == 2)
                    {
                        m_thread = new Thread(new ThreadStart(RUN2));
                        m_thread.Start();
                    }
                    
                }
            }
        }
        private void RUN1()
        {
            int nRead;

            while (this.m_bClientOn)
            {
               
                nRead = 0;
                nRead = m_networkstream[1].Read(readBuffer, 0, 1024 * 4);
                
                Packet packet = (Packet)Packet.Desserialize(this.readBuffer);

                switch ((int)packet.Type)
                {
                    case (int)PacketType.접속:
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                con = (connect)Packet.Desserialize(readBuffer);
                                name1 = con.nickname;
                                txtLog.Text += "1p " + name1 + " 접속\r\n";

                                playerCount++;
                                //MessageBox.Show(playerCount + "?");

                                setPlayer setplayer = new setPlayer();
                                setplayer.Type = (int)PacketType.할당;
                                

                                setplayer.player = playerCount;
                                Packet.Serialize(setplayer).CopyTo(this.sendBuffer, 0);
                                this.Send();
                            }));
                            break;
                        }
                    case (int)PacketType.수신:
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                //MessageBox.Show(playerCount + "?");
                                if (playerCount == 2)
                                {
                                    start gameStart = new start();
                                    gameStart.isStart = true;
                                    gameStart.Type = (int)PacketType.시작;

                                    Packet.Serialize(gameStart).CopyTo(this.sendBuffer, 0);
                                    this.SendAll();
                                }
                            }));
                            break;
                        }
                    case (int)PacketType.위치:
                        {
                            
                            location = (Location)Packet.Desserialize(readBuffer);
                            
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                playerNum = location.player;
                                txtLog.Text += location.player + " (" + location._x + "," + location._y + ")\r\n";
                                //MessageBox.Show(playerNum + "");
                                Location otherLocation = new Location();
                                if (location.boom_x != -1 && location.boom_y != -1)
                                {
                                    otherLocation.boom_x = location.boom_x;
                                    otherLocation.boom_y = location.boom_y;
                                }
                                otherLocation._x = location._x;
                                otherLocation._y = location._y;
                                otherLocation.foot_type = location.foot_type;
                                otherLocation.box_num = location.box_num;
                                otherLocation.Type = (int)PacketType.위치;

                                Packet.Serialize(otherLocation).CopyTo(this.sendBuffer, 0);
                                this.Send_other();
                            }));   
                            break;
                        }
                    case (int)PacketType.도착:
                        {
                            Packet.Serialize(packet).CopyTo(this.sendBuffer, 0);
                            this.SendAll();
                            break;
                        }


                }
            }
        }
        private void RUN2()
        {
            int nRead;

            while (this.m_bClientOn)
            {

                nRead = 0;
                nRead = m_networkstream[2].Read(readBuffer, 0, 1024 * 4);

                Packet packet = (Packet)Packet.Desserialize(this.readBuffer);

                switch ((int)packet.Type)
                {
                    case (int)PacketType.접속:
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                con = (connect)Packet.Desserialize(readBuffer);
                                name2 = con.nickname;
                                txtLog.Text += "2p " + name2 + " 접속\r\n";

                                playerCount++;
                                //MessageBox.Show(playerCount + "?");

                                setPlayer setplayer = new setPlayer();
                               
                                setplayer.Type = (int)PacketType.할당;

                                setplayer.player = playerCount;
                                Packet.Serialize(setplayer).CopyTo(this.sendBuffer, 0);
                                this.Send();
                            }));
                            break;
                        }
                    case (int)PacketType.수신:
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                //MessageBox.Show(playerCount + "?");
                                if (playerCount == 2)
                                {
                                    start gameStart = new start();
                                    gameStart.isStart = true;
                                    gameStart.p1name = name1;
                                    gameStart.p2name = name2;
                                    gameStart.arr1 = arr1;
                                    gameStart.arr2 = arr2;
                                    gameStart.Type = (int)PacketType.시작;

                                    Packet.Serialize(gameStart).CopyTo(this.sendBuffer, 0);
                                    this.SendAll();
                                }
                            }));
                            break;
                        }
                    case (int)PacketType.위치:
                        {

                            location = (Location)Packet.Desserialize(readBuffer);
                            
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                playerNum = location.player;
                                txtLog.Text += location.player + " (" + location._x +","+location._y+")\r\n";
                                //MessageBox.Show(playerNum + "");
                                Location otherLocation = new Location();
                                if (location.boom_x != -1 && location.boom_y != -1)
                                {
                                    otherLocation.boom_x = location.boom_x;
                                    otherLocation.boom_y = location.boom_y;
                                }
                                otherLocation._x = location._x;
                                otherLocation._y = location._y;
                                otherLocation.box_num = location.box_num;
                                otherLocation.foot_type = location.foot_type;
                                otherLocation.Type = (int)PacketType.위치;



                                Packet.Serialize(otherLocation).CopyTo(this.sendBuffer, 0);
                                this.Send_other();
                            }));

                            break;
                        }
                    case (int)PacketType.도착:
                        {
                            Packet.Serialize(packet).CopyTo(this.sendBuffer, 0);
                            this.SendAll();
                            break;
                        }
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.Stop();
            m_networkstream[1].Close();
            m_networkstream[2].Close();

            m_thread.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            random_Item();
            m_serverThread = new Thread(new ThreadStart(ServerStart));
            m_serverThread.Start();
        }

        // 패킷 보내는 함수 
        public void Send()
        {
            m_networkstream[thisPlayer].Write(sendBuffer, 0, sendBuffer.Length);
            m_networkstream[thisPlayer].Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                sendBuffer[i] = 0;
            }
        }
        public void Send_other()
        {
            m_networkstream[3 - playerNum].Write(sendBuffer, 0, sendBuffer.Length);
            m_networkstream[3 - playerNum].Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                sendBuffer[i] = 0;
            }
        }

        public void SendAll()
        {
            for (int i = 1; i < playerCount + 1; i++)
            {
                m_networkstream[i].Write(sendBuffer, 0, sendBuffer.Length);
                m_networkstream[i].Flush();
            }

            for (int i = 0; i < 1024 * 4; i++)
            {
                sendBuffer[i] = 0;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}
