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
using System.Windows.Shapes;

namespace WPFbj
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        FileStream fs = new FileStream("RFID.txt", FileMode.Append, FileAccess.Write);
        StreamWriter sw;
        DateTime dt = new System.DateTime();
       
        public Window3()
        {
            InitializeComponent();
            sw = new StreamWriter(fs, Encoding.Default);

        }

        private void textBox_Copy_KeyDown(object sender, KeyEventArgs e)
        {
           
          
            if (e.Key == Key.Return)
            {
                dt = DateTime.Now;
                int 年 = dt.Year;
                int 日 = dt.DayOfYear;
                int 时 = dt.Hour;
                int 分 = dt.Minute;
                int 秒 = dt.Second;
                int time = 秒 + (分 + 时 * 60 + 日 * 60 * 24 + (年 - 2016) * 60 * 24 * 366) * 60;
                sw.WriteLine(t1.Text + "|" + t2.Text + "|" + "0"+"|"+time+"|"+0+"|"+0 + "|" + 0 + "|" + 0.5+ "|" + 0);
               // sw.Close();
               // MessageBox.Show("!!!");
                t1.Focus();
                t1.Text = "";
                t2.Text = "";
            }
            sw.Flush();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                t2.Focus();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            fs.Close();
        }
    }
}
