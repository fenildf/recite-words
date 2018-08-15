using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFbj
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        FileStream fs = new FileStream("hist.txt", FileMode.Append, FileAccess.Write);
        StreamWriter sw;
        System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch(); //计时器1初始化


        string[] line;
        string path = "RFID.txt";
        int bj = 0;

        string qustion = "";
        string result = "";
        int ys = 0;
        int time2 = 0;
        int k = 0;

        Random r = new Random();
        DateTime dt = new System.DateTime();

        public Window4()
        {
            InitializeComponent();
            stopwatch2.Start();
            sw = new StreamWriter(fs, Encoding.Default);
            if (File.Exists(path))
            {
                line = File.ReadAllLines(path, Encoding.Default);
                try
                {
                    int a = r.Next(0, line.Length - 1);
                    bj = a;
                    //t1.Tag = line[0];
                    string[] s;
                    s = line[k].Split('|');

                    tb.Text = s[0];
                    tb2.Text = s[1];
                    t1.Text = " ";
                    t1.Focus();
                    t1.SelectAll();
                    result = s[1];
                    time2 = Convert.ToInt32(s[3]);
                    ys = Convert.ToInt32(s[2]);

                }
                catch
                {
                    MessageBox.Show("错误");
                }


            }
            else
            {
                tb.Text = "不存在！";
                t1.Text = " ";
            }

        }





        private void t1_KeyDown_1(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(e.Key.ToString());
           

        }

      

        private void t1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb.Text.Length >= t1.Text.Length)
            {
                
                try
                 {
                    if (t1.Text[t1.Text.Length - 1] != tb.Text[t1.Text.Length - 1])
                    {
                       // int i=t1.Text.IndexOf(" ");
                        t1.Select(t1.Text.Length - 1, 1);
                    }
                 }
                 catch
                 {
                        t1.SelectAll();
                 }
                    
            }
            
            else
            {
                  try
                    {
                    Random r = new Random();

                    int k0 = 0;
                    int j0 = 0;
                    int k1 = 0;
                    int j1 = 0;

                    k0 = r.Next(0, line.Length - 1);
                    j0 = Convert.ToInt32(line[k0].Split('|')[2]);

                    for (int i = 0; i < 6; i++)
                    {
                      //  MessageBox.Show(k1+" "+j1);
                        k1 = r.Next(0, line.Length - 1);
                        j1 = Convert.ToInt32(line[k1].Split('|')[2]) ;
                       // MessageBox.Show(k1 + " " + j1);
                        if (j1 < j0)
                        {
                            k0 = k1;
                            j0 = j1;
                       //     MessageBox.Show(k1 + " " + j1);
                        }
                    }

                    
    
                        string[] s = line[k0].Split('|');
                        tb.Text = s[0];
                        tb2.Text = s[1];
                    }
                    catch
                    {
                        tb.Text = "错误1";
                    }
                t1.Text = " ";                     
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //File.WriteAllLines(path, line, Encoding.Default);
            sw.Close();
            fs.Close();
        }
    }
}
