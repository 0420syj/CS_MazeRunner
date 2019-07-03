using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace ClassLibrary1
{
    public enum PacketType
    {
        접속 = 0,
        할당 = 1,
        수신 = 2,
        시작 = 3,
        위치 = 4,
        아이템 = 5,
        도착=6

    }
    [Serializable]
    public class Packet
    {
        // public int Length;
        public int Type;

        public Packet()
        {
            this.Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }


        public static Object Desserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            foreach (byte b in bt)
            {
                ms.WriteByte(b);
            }

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }


    [Serializable]
    public class connect : Packet
    {
        public bool isConnect;
        public string nickname;
    }

    [Serializable]
    public class setPlayer : Packet
    {
        public int player;
    }
    [Serializable]
    public class Receive : Packet
    {
        public bool isReceive;
    }
    [Serializable]
    public class start : Packet
    {
        public bool isStart;
        public string p1name;
        public string p2name;
        public int[] arr1 = new int[26] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] arr2 = new int[26] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    [Serializable]
    public class Location : Packet
    {
        public int player;
        public int _x;
        public int _y;
        public int foot_type;
        public int box_num = -1;
        public int boom_x = -1;
        public int boom_y = -1;
    }
    [Serializable]
    public class Item : Packet
    {
        public bool bomb;
        public int[] bomb_array_x = new int[26]; //폭탄 발생 지점 저장
        public int[] bomb_array_y = new int[26]; //폭탄 발생 지점 저장
    }
    [Serializable]
    public class Arrive : Packet
    {
        public int player;
    }
}
