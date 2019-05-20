using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {
        long freespace_c;
        long freespace_d;
        string volume;
                public static long GetHardDiskFreeSpace(string str_HardDiskName)  
  
        {  
  
            long freeSpace = new long();  
  
            str_HardDiskName = str_HardDiskName + ":\\";  
  
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();  
  
            foreach (System.IO.DriveInfo drive in drives)  
  
            {  
  
                if (drive.Name == str_HardDiskName)  
  
                {  
  
                    freeSpace = drive.TotalFreeSpace/(1024*1024*1024);  
  
                }  
  
            }  
  
            return freeSpace;  
  
        }

                public static long GetHardDiskSpace(string str_HardDiskName)
                {
                    long totalSize = new long();
                    str_HardDiskName = str_HardDiskName + ":\\";
                    System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                    foreach (System.IO.DriveInfo drive in drives)
                    {
                        if (drive.Name == str_HardDiskName)
                        {
                            totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                        }
                    }
                    return totalSize;
                }

                private string GetIpAddress()
                {
                    string hostName = Dns.GetHostName();   //获取本机名
                    IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
                    //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
                    IPAddress localaddr = localhost.AddressList[0];

                    return localaddr.ToString();
                }

                private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
                {

                    // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
                    int intHour = e.SignalTime.Hour;
                    int intMinute = e.SignalTime.Minute;
                    int intSecond = e.SignalTime.Second;
                    // 定制时间； 比如 在10：30 ：00 的时候执行某个函数  
                    int iHour = 8;
                    int iMinute = 58;
                    int iSecond = 00;
                    // 设置　 每秒钟的开始执行一次  
                    //if (intSecond == iSecond)
                    //{
                    //    //int myint = 1;
                    //    //textBox1.Text = myint.ToString();
                    //    //myint++;
                    //    Console.WriteLine("每秒钟的开始执行一次！");
                    //}
                    //// 设置　每个小时的３０分钟开始执行  
                    //if (intMinute == iMinute && intSecond == iSecond)
                    //{
                    //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
                    //}

                    //// 设置　每天的１０：３０：００开始执行程序  
                    //if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
                    //{
                    //    Console.WriteLine("在每天１０点３０分开始执行！");
                    //}
                    if (intHour == iHour && intMinute == iMinute)
                    {
                        volume = "C";
                        freespace_c = GetHardDiskFreeSpace(volume);
                        volume = "D";
                        freespace_d = GetHardDiskFreeSpace(volume);
                        SqlConnection con = new SqlConnection();

                        //con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
                        con.ConnectionString = "server=10.138.42.49;database=test1;uid=sa;pwd=Haier,4249";
                        con.Open();

                        /*
                        SqlDataAdapter 对象。 用于填充DataSet （数据集）。
                        SqlDataReader 对象。 从数据库中读取流..
                        后面要做增删改查还需要用到 DataSet 对象。
                        */

                        SqlCommand com = new SqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        com.CommandText = "INSERT INTO table_test VALUES ('" + GetIpAddress() + "'," + freespace_c + "," + freespace_d + ")";
                        SqlDataReader dr = com.ExecuteReader();//执行SQL语句
                        dr.Close();//关闭执行   
                        con.Close();//关闭数据库
                    }

                }   

        public Form1()
        {
            InitializeComponent();

            //string AppPath = Application.StartupPath.ToString();
            //string volume = "C";
            //freespace_c = GetHardDiskFreeSpace(volume);
            //volume = "D";
            //freespace_d = GetHardDiskFreeSpace(volume);
            //textBox1.Text = DateTime.Now.DayOfWeek.ToString();
            //String volume = "c";
            //textBox1.Text = GetHardDiskFreeSpace(volume)+"";
            //textBox1.Text = GetIpAddress();
            //textBox1.Text = drive.Name;
            textBox1.Text = "日常巡检自动化软件 请勿关闭 Powered by wyh";



            //***************************数据库操作
            //SqlConnection con = new SqlConnection();

            ////con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
            //con.ConnectionString = "server=.;database=test1;uid=sa;pwd=123";
            //con.Open();

            ///*
            //SqlDataAdapter 对象。 用于填充DataSet （数据集）。
            //SqlDataReader 对象。 从数据库中读取流..
            //后面要做增删改查还需要用到 DataSet 对象。
            //*/

            //SqlCommand com = new SqlCommand();
            //com.Connection = con;
            //com.CommandType = CommandType.Text;
            //com.CommandText = "INSERT INTO table_test VALUES ('"+GetIpAddress()+"',"+freespace_c+","+freespace_d+")";
            //SqlDataReader dr = com.ExecuteReader();//执行SQL语句
            //dr.Close();//关闭执行   
            //con.Close();//关闭数据库
            //********************数据库操作

            //********************定时器操作
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 59000;//执行间隔时间,单位为毫秒  秒*1000
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);  
            //********************定时器操作
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
