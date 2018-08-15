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
    public partial class Window2 : Window
    {
        FileStream fs = new FileStream("hist.txt", FileMode.Append, FileAccess.Write);
        StreamWriter sw,sw2;
        System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch(); //计时器1初始化


        string[] line,line2;
        string path = "RFID.txt";
        string path2 = "fanyi.txt";
        int bj = 0;

        string qustion = "";
        string eng = "";
        string result = "";
        string result2 = "";
        int ys = 0;
        int time2 = 0;

        int nnn = 0;
        int cs = 0;
        int zt = 0;
        int err = 0;
        double rem = 0;
        int cont = 0;

        Random rd = new Random();

        Random r = new Random();
        DateTime dt = new System.DateTime();

        Dictionary<string, string> fanyi = new Dictionary<string, string>();

        public Window2()
        {
            InitializeComponent();

            for (int l = 0; l< 3; l++)
            {
                textBlock.Text += "1234\n";
            }

            t1.Focus();
            stopwatch2.Start();
            if (File.Exists(path2))
            {
                line2 = File.ReadAllLines(path2, Encoding.Default);
                for (int i = 0; i < line2.Length; i++)
                {
                    fanyi.Add(line2[i].Split(' ')[0], line2[i].Split(' ')[1]);
                    textBlock.Text += line2[i].Split(' ')[0] + ":" + fanyi[line2[i].Split(' ')[0]];
                }
            }
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
                    s = line[a].Split('|');

                    tb.Text = s[0];
                    eng = s[0];
                    t1.Text = "";
                    result = s[1];

                    cs= Convert.ToInt32(s[4]);
                    zt = Convert.ToInt32(s[5]);
                    err = Convert.ToInt32(s[6]);
                    rem = Convert.ToDouble(s[7]);
                    cont = Convert.ToInt32(s[8]);

                    MessageBox.Show(rem.ToString("0.0"));
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
                t1.Text = "";
            }

        }





        private void t1_KeyDown_1(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(e.Key.ToString());
            if (e.Key == Key.Return)
            {
                dt = DateTime.Now;
                int 年 = dt.Year;
                int 日 = dt.DayOfYear;
                int 时 = dt.Hour;
                int 分 = dt.Minute;
                int 秒 = dt.Second;
                int time =秒+ (分 + 时 * 60 + 日 * 60 * 24 + (年 - 2016) * 60 * 24 * 366)*60;


                if (t1.Text == result)
                {
                    stopwatch2.Stop();
                    sw.WriteLine(bj + "|" + stopwatch2.ElapsedMilliseconds + "|" + time+"|"+ys+"|"+(time-time2));
                    sw.Flush();
                    if (ys == 0)
                    {
                        ys = 1;
                        cs = 1;
                        err = 1;
                    }
                    if (ys < 60 * 60 * 24 * 5)
                    {
                        ys = ys * 4 + (cs) + 10;
                    }
                   
                    if (cont < 0)
                    {
                        cont = 1;
                    }
                    else
                    {
                        cont++;
                    }
                    if ((err/cs) < 0.2)
                    {
                      //  rem = rem+0.2;
                    }

                    line[bj] = eng + "|" + result + "|" + ys + "|" + time + "|" + (cs + 1) + "|" + (zt + stopwatch2.ElapsedMilliseconds) + '|'+err + "|" + (rem ).ToString() + "|" + cont;
                  
                    stopwatch2.Restart();
                    for (int i = 0; i < line.Length; i++)
                    {
                       // MessageBox.Show(":" + line.Length);
                        string[] s = line[i].Split('|');
                        if (s.Length < 4)
                        {
                      //      MessageBox.Show("" + i);
                           // tb.Text = "已复习完" + i;

                            t1.Text = "";
                            this.Close();
                            
                            return;
                        }

                        if (Convert.ToInt32(s[3]) + Convert.ToInt32(s[2]) < time && Convert.ToInt32(s[6])>3)
                        {
                            bj = i;

                            tb.Text = s[0].Replace(" ", "");
                            eng = s[0];
                            result = s[1];
                            time2 = Convert.ToInt32(s[3]);
                            ys = Convert.ToInt32(s[2]);
                            cs = Convert.ToInt32(s[4]);
                            zt = Convert.ToInt32(s[5]);
                            err = Convert.ToInt32(s[6]);
                            rem = Convert.ToDouble(s[7]);
                            cont = Convert.ToInt32(s[8]);

                            result2 = "";
                            if (cont > -2)
                            {
                                t1.Text = "";
                                textBlock.Text = "";
                                for (int l = 0; l < s[0].Split(' ').Length; l++)
                                {

                                    if (fanyi.ContainsKey(s[0].Split(' ')[l])) // True 
                                    {
                                        textBlock.Text += s[0].Split(' ')[l] + ":" + fanyi[s[0].Split(' ')[l]] + '\n';
                                    }
                                    else
                                    {
                                        textBlock.Text += s[0].Split(' ')[l] + '\n';
                                    }

                                }
                            }
                            else
                            {
                                nnn = 0;
                                tb.Text = "";
                                int qr = rd.Next(0, 2);

                                for (int q = 0; q < s[0].Replace(" ", "").Length; q++)
                                {
                                    if (q % 2 == qr)
                                    {
                                        tb.Text += s[0].Replace(" ", "")[q];
                                        result2 += "_";
                                    }
                                    else
                                    {
                                        tb.Text += "_";
                                        result2 += s[0].Replace(" ", "")[q];
                                    }
                                    //  MessageBox.Show(""+q);
                                }
                                //   MessageBox.Show("2222");
                                tb.Text += ":";
                                result2 += ":";
                                string ss = result;

                                tb.Text += ss;
                                result2 += ss;
                                
                                tb.Focus();
                                tb.Select(tb.Text.IndexOf("_"), 1);
                            }
                            nnn = 1;


                            return;
                        }
                    }
                   

                    MessageBox.Show("复习完");
                    //tb.Text = "已复习完"+time;
    

                }
                else if (t1.Text.Contains(result) )
                {
                    MessageBox.Show(result);
                    stopwatch2.Stop();
                    sw.WriteLine(bj + "|" + stopwatch2.ElapsedMilliseconds + "|" + time + "|" + ys + "|" + (time - time2));
                    sw.Flush();
                    if (ys == 0)
                    {
                        ys = 1;
                        cs = 1;
                        err = 1;
                    }
                    if (ys < 60 * 60 * 24 * 5)
                    {
                        ys = ys * 4 + (cs) + 10;
                    }
                    if (cont < 0)
                    {
                        cont = 1;
                    }
                    else
                    {
                        cont++;
                    }
                    if ((err / cs) < 0.2)
                    {
                        rem = rem + 0.2;
                    }
                    line[bj] = eng + "|" + result + "|" + ys + "|" + time + "|" + (cs + 1) + "|" + (zt + stopwatch2.ElapsedMilliseconds) + '|' + err + "|" + (rem).ToString() + "|" + cont;
                    // MessageBox.Show(":"+ys);
                    stopwatch2.Restart();
                    for (int i = 0; i < line.Length; i++)
                    {
                        // MessageBox.Show(":" + line.Length);
                        string[] s = line[i].Split('|');
                        if (s.Length < 4)
                        {
                            //      MessageBox.Show("" + i);
                            
                            tb.Text = "已复习完" + i;
                            t1.Text = "";
                            this.Close();
                            return;
                        }

                        if (Convert.ToInt32(s[3]) + Convert.ToInt32(s[2]) < time && Convert.ToInt32(s[6]) > 3)
                        {
                            bj = i;

                            tb.Text = s[0].Replace(" ", "");
                            eng = s[0];
                            result = s[1];
                            time2 = Convert.ToInt32(s[3]);
                            ys = Convert.ToInt32(s[2]);
                            cs = Convert.ToInt32(s[4]);
                            zt = Convert.ToInt32(s[5]);
                            err = Convert.ToInt32(s[6]);
                            rem = Convert.ToDouble(s[7]);
                            cont = Convert.ToInt32(s[8]);
                            t1.Text = "";
                            textBox.Text = "" + i;

                            textBlock.Text = "";
                            result2 = "";
                            if (cont > -2)
                            {
                                t1.Text = "";
                                textBlock.Text = "";
                                for (int l = 0; l < s[0].Split(' ').Length; l++)
                                {

                                    if (fanyi.ContainsKey(s[0].Split(' ')[l])) // True 
                                    {
                                        textBlock.Text += s[0].Split(' ')[l] + ":" + fanyi[s[0].Split(' ')[l]] + '\n';
                                    }
                                    else
                                    {
                                        textBlock.Text += s[0].Split(' ')[l] + '\n';
                                    }

                                }
                            }
                            else
                            {
                                nnn = 0;
                                tb.Text = "";
                                int qr = rd.Next(0, 2);

                                for (int q = 0; q < s[0].Replace(" ", "").Length; q++)
                                {
                                    if (q % 2 == qr)
                                    {
                                        tb.Text += s[0].Replace(" ", "")[q];
                                        result2 += "_";
                                    }
                                    else
                                    {
                                        tb.Text += "_";
                                        result2 += s[0].Replace(" ", "")[q];
                                    }
                                    //  MessageBox.Show(""+q);
                                }
                                //   MessageBox.Show("2222");
                                tb.Text += ":";
                                result2 += ":";
                                
                                string ss = result;

                                tb.Text += ss;
                                result2 += ss;
                                tb.Focus();
                                tb.Select(tb.Text.IndexOf("_"), 1);
                            }
                            nnn = 1;


                            return;
                        }
                    }
                    //tb.Text = "已复习完" + time;
                    MessageBox.Show("复习完");

                   
                    

                }
                else
                {
                    WM wm = new WM();
                    wm.textBlock.Text = result;
                    wm.ShowDialog();

                    if (wm.DialogResult == true)
                    {
 
                        sw.WriteLine(bj + "|" + "-1" + "|" + time + "|" + ys + "|" + (time - time2));
                        sw.Flush();

                        if(ys==0)
                        {
                            ys = 1;
                            cs = 1;
                            err = 1;
                        }

                        ys = 1 ;
                        if (cont > 0)
                        {
                            cont = -1;
                        }
                        else
                        {
                            cont--;
                        }
                   
                        line[bj] = eng + "|" + result + "|" + ys + "|" + time + "|" + (cs + 1) + "|" + (zt + stopwatch2.ElapsedMilliseconds) + '|' + (err+1) + "|" + (rem -0.1).ToString() + "|" + cont;
                        stopwatch2.Restart();
                        for (int i = 0; i < line.Length; i++)
                        {
                            // MessageBox.Show(":" + line.Length);
                            string[] s = line[i].Split('|');
                            if (s.Length < 4)
                            {
                                //      MessageBox.Show("" + i);

                                tb.Text = "格式错误" + i;
                                t1.Text = "";
                                this.Close();
                                return;
                            }
                            try
                            {
                                if (Convert.ToInt32(s[3]) + Convert.ToInt32(s[2]) < time && Convert.ToInt32(s[6]) > 3)
                                {
                                    bj = i;

                                    tb.Text = s[0].Replace(" ", "");
                                    eng = s[0];
                                    result = s[1];
                                    time2 = Convert.ToInt32(s[3]);
                                    ys = Convert.ToInt32(s[2]);
                                    cs = Convert.ToInt32(s[4]);
                                    zt = Convert.ToInt32(s[5]);
                                    err = Convert.ToInt32(s[6]);
                                    rem = Convert.ToDouble(s[7]);
                                    cont = Convert.ToInt32(s[8]);
                                    t1.Text = "";
                                    textBox.Text = ""+i;

                                    textBlock.Text = "";
                                    result2 = "";
                                    if (cont > -2)
                                    {
                                        t1.Text = "";
                                        textBlock.Text = "";                                      
                                        for (int l = 0; l < s[0].Split(' ').Length; l++)
                                        {
                                          
                                            if (fanyi.ContainsKey(s[0].Split(' ')[l])) // True 
                                            {
                                                textBlock.Text += s[0].Split(' ')[l] + ":" + fanyi[s[0].Split(' ')[l]] + '\n';
                                            }
                                            else
                                            {
                                                textBlock.Text += s[0].Split(' ')[l] + '\n';
                                            }

                                        }
                                    }
                                    else
                                    {
                                        nnn = 0;
                                        tb.Text = "";
                                        int qr = rd.Next(0, 2);

                                        for (int q = 0; q < s[0].Replace(" ", "").Length; q++)
                                        {
                                            if (q % 2 ==qr)
                                            {
                                                tb.Text += s[0].Replace(" ", "")[q];
                                                result2 += "_";
                                            }
                                            else
                                            {
                                                tb.Text += "_";
                                                result2 += s[0].Replace(" ", "")[q];
                                            }
                                          //  MessageBox.Show(""+q);
                                        }
                                     //   MessageBox.Show("2222");
                                        tb.Text += ":";
                                        result2 += ":";
                                        string ss = result;

                                        tb.Text += ss;
                                        result2 += ss;
                                        tb.Focus();
                                        tb.Select(tb.Text.IndexOf("_"), 1);
                                    }
                                    nnn = 1;

                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show((s[0]) + "," + (s[1]));
                            }

                        }

                        // ys = 0;
                        // MessageBox.Show("正确答案:" + result);
                        //t1.Text = "";
                    }
                    else
                    {
                        stopwatch2.Stop();
                        sw.WriteLine(bj + "|" + stopwatch2.ElapsedMilliseconds + "|" + time + "|" + ys + "|" + (time - time2));
                        sw.Flush();
                        if (ys == 0)
                        {
                            ys = 1;
                            cs = 1;
                            err = 1;
                        }
                        if (ys < 60 * 60 * 24 * 5)
                        {
                            ys = ys * 4 + (cs) + 10;
                        }
                        if (cont < 0)
                        {
                            cont = 1;
                        }
                        else
                        {
                            cont++;
                        }
                        if ((err / cs) < 0.2)
                        {
                            rem = rem + 0.2;
                        }
                        line[bj] = eng  + "|" + result + "|" + ys + "|" + time + "|" + (cs + 1) + "|" + (zt + stopwatch2.ElapsedMilliseconds) + '|' + err + "|" + (rem ).ToString() + "|" + cont;
                        stopwatch2.Restart();
                        for (int i = 0; i < line.Length; i++)
                        {
                            // MessageBox.Show(":" + line.Length);
                            string[] s = line[i].Split('|');
                            if (s.Length < 4)
                            {
                                //      MessageBox.Show("" + i);

                                tb.Text = "格式错误" + i;
                                t1.Text = "";
                                this.Close();
                                return;
                            }
                            try
                            {
                                if (Convert.ToInt32(s[3]) + Convert.ToInt32(s[2]) < time && Convert.ToInt32(s[6]) > 3)
                                {
                                    bj = i;

                                    tb.Text = s[0].Replace(" ", "");
                                    eng = s[0];
                                    result = s[1];
                                    time2 = Convert.ToInt32(s[3]);
                                    ys = Convert.ToInt32(s[2]);
                                    cs = Convert.ToInt32(s[4]);
                                    zt = Convert.ToInt32(s[5]);
                                    err = Convert.ToInt32(s[6]);
                                    rem = Convert.ToDouble(s[7]);
                                    cont = Convert.ToInt32(s[8]);
                                    t1.Text = "";
                                    textBox.Text = "" + i;

                                    result2 = "";
                                    if (cont > -2)
                                    {
                                        t1.Text = "";
                                        textBlock.Text = "";
                                        for (int l = 0; l < s[0].Split(' ').Length; l++)
                                        {

                                            if (fanyi.ContainsKey(s[0].Split(' ')[l])) // True 
                                            {
                                                textBlock.Text += s[0].Split(' ')[l] + ":" + fanyi[s[0].Split(' ')[l]] + '\n';
                                            }
                                            else
                                            {
                                                textBlock.Text += s[0].Split(' ')[l] + '\n';
                                            }

                                        }
                                    }
                                    else
                                    {
                                        nnn = 0;
                                        tb.Text = "";
                                        int qr = rd.Next(0, 2);

                                        for (int q = 0; q < s[0].Replace(" ", "").Length; q++)
                                        {
                                            if (q % 2 == qr)
                                            {
                                                tb.Text += s[0].Replace(" ", "")[q];
                                                result2 += "_";
                                            }
                                            else
                                            {
                                                tb.Text += "_";
                                                result2 += s[0].Replace(" ", "")[q];
                                            }
                                            //  MessageBox.Show(""+q);
                                        }
                                        //   MessageBox.Show("2222");
                                        tb.Text += ":";
                                        result2 += ":";
                                        string ss = result;

                                        tb.Text += ss;
                                        result2 += ss;
                                        tb.Focus();
                                        tb.Select(tb.Text.IndexOf("_"), 1);
                                    }
                                    nnn = 1;


                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show((s[3])+","+ (s[2]));
                            }
                           
                        }

                        MessageBox.Show("复习完");
                        //  tb.Text = "已复习完" + time;


                    }


                    


                  
                }



            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllLines(path, line, Encoding.Default);
            sw.Close();
            fs.Close();
        }

        private void t1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("nnn" + 666);
      
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (nnn == 1)
                {
                    //ssageBox.Show("nnn" );
                    int ii = tb.SelectionStart;
                    if (ii > 0)
                    {
                        ii = ii - 1;
                    }
                    else
                    {
                        ii = 0;
                    }

                    if (tb.Text[ii] == result2[ii])
                    {
                        if(tb.Text.IndexOf("_")==-1)
                        {
                            t1.Focus();
                            
                        }
                        else
                        {
                            tb.Select(tb.Text.IndexOf("_"), 1);
                        }
                        
                    }
                    else
                    {
                                         
                        tb.Select(ii, 1);
                        t1.Text = ""+result2[ii];
                    }
                }
            }
            catch
            {
                tb.Select(0, 1);
               
            }
            
            
            
        }

        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
