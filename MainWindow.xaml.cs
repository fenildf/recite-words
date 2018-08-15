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
    public partial class MainWindow : Window
    {

        string[] line;
        string path = "RFID.txt";
        string path2 = "KY.txt";

        public MainWindow()
        {
            InitializeComponent();


        }

         private void btn1c(object sender, RoutedEventArgs e)
        {
            Window2 wd2 = new Window2();
            wd2.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btn2c(object sender, RoutedEventArgs e)
        {
            Window1 wd1 = new Window1();
            wd1.ShowDialog();
        }

        private void btn3c(object sender, RoutedEventArgs e)
        {
            Window3 wd3 = new Window3();
            wd3.ShowDialog();
        }

        private void btn4c(object sender, RoutedEventArgs e)
        {
            Window4 wd4 = new Window4();
            wd4.ShowDialog();
        }

        private void btn5c(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                line = File.ReadAllLines(path, Encoding.Default);
                try
                {
                    DateTime dt = new System.DateTime();
                    dt = DateTime.Now;
                    int 年 = dt.Year;
                    int 日 = dt.DayOfYear;
                    int 时 = dt.Hour;
                    int 分 = dt.Minute;
                    int 秒 = dt.Second;
                    int time = 秒 + (分 + 时 * 60 + 日 * 60 * 24 + (年 - 2016) * 60 * 24 * 366) * 60;
                    List<string> newline = new List<string>();
                    int j = 0;
                    for (int i = 0; i < line.Length-1; i++)
                    {

                        if (true)
                        {
                            newline.Add(line[i].Split('|')[0]); 
                            j++;
                            //MessageBox.Show(" "+i);
                        }
                  //      newline.Add(line[i] + '|' + Convert.ToInt32(line[i].Split('|')[4]) / 5 + '|' + "1.0" + '|' + 0);
                    }
                    MessageBox.Show("qqqqq " + j);
                    File.WriteAllLines(path2, newline, Encoding.Default);
                    MessageBox.Show("成功!!!");

                }
                catch
                {
                    MessageBox.Show("错误");
                }
            }
            else
            {
                MessageBox.Show("不存在");
            }

          
        }
    }
}
