using System;
using System.Collections.Generic;
using System.Data;
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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {

        //FileStream fs = new FileStream("a.txt", FileMode.Append, FileAccess.Write);
        // StreamWriter sw;
        string[] line;
        string path = "RFID.txt";
        int bj = 0;

        string qustion = "";
        string result = "";
        int ys = 0;
        int time2 = 0;

        Random r = new Random();
        DateTime dt = new System.DateTime();

        List<DATA> list = new List<DATA>();

        public Window1()
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                line = File.ReadAllLines(path, Encoding.Default);
                int a = r.Next(0, line.Length - 1);
                bj = a;
                //t1.Tag = line[0];
                string[] s;
                s = line[a].Split('|');

                tb.Text = "总数:"+line.Length;
             //   t1.Text = "";
             //   result = s[1];
              //  time2 = Convert.ToInt32(s[3]);
             //   ys = Convert.ToInt32(s[2]);

            }
            else
            {
                tb.Text = "不存在！";
                //t1.Text = "";
            }

          

            for (int i = 0; i < line.Length; i++)
            {
                string []ss = line[i].Split('|');
                if (ss.Length < 9)
                {
                    if (ss.Length >= 5)
                    {
                        DATA data = new DATA();
                        data.name = ss[0];
                        data.result = ss[1];
                        data.ys = ss[2];
                        data.time = ss[3];
                        data.cs = "0";
                        data.zt = "0";
                        data.err = "" + Convert.ToInt32(ss[4]) / 5 ;
                        data.rem = "1.0";
                        data.cont = "0";
                        list.Add(data);

                    }
                }
                else
                {
                    DATA data = new DATA();
                    data.name = ss[0];
                    data.result = ss[1];
                    data.ys = ss[2];
                    data.time = ss[3];

                   // data.ys = "1";
                   // data.time = "1";

                    data.cs = ss[4];
                    data.zt = ss[5];
                    data.err = ss[6];

                    data.rem = ss[7];
                    data.cont = ss[8];
                    list.Add(data);

                    if (data.cs == "0")
                    {
                        data.rem = "0.5";
                    }
                }
               

            }

            datagride1.ItemsSource = list;
           // dataGrid1.ItemsSource=
            //dataGrid1.da

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] line2 = new string[list.Count+1 ];
            for (int i = 0; i < list.Count; i++)
            {
                string ss = list[i].name + "|" + list[i].result + "|" + list[i].ys + "|" + list[i].time + "|" + list[i].cs + "|" + list[i].zt + "|" + list[i].err + "|" + list[i].rem + "|" + list[i].cont;
                line2[i] = ss;

            }
            File.WriteAllLines(path, line2, Encoding.Default);
        }

        private void datagride1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void datagride1_KeyUp(object sender, KeyEventArgs e)
        {
           
            if (e.Key == Key.Return)
            {
                //MessageBox.Show("!!!");

             //   DataRowView selectItem = datagride1.Items[1] as DataRowView;
            //    datagride1.Columns[1].GetCellContent(datagride1.Items[1]);
            }
        }
    }

    public class DATA {
        public string name { get; set; }
        public string result { get; set; }
        public string ys { get; set; }
        public string time { get; set; }
        public string cs { get; set; }
        public string zt { get; set; }
        public string err { get; set; }
        public string rem { get; set; }
        public string cont { get; set; }


    }

}
