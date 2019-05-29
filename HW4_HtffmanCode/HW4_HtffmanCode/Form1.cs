using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4_HtffmanCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //讀出來的文字串 & 讀寫的檔案路徑
        private string HW_Message = "" , file_path = "";

        //宣告第一組為計算用 第二組為Huffman code count用
        private int char_num = 0 , Huffman_code_count = 0;

        //宣告讀取出來的字元
        private CharTime[] Charset = new CharTime[100];

        //第一組拿來當作計算用 第二組拿來當 Huffman code用
        private CharTime[] Huffman_Code = new CharTime[100];

        private void button1_Click(object sender, EventArgs e)
        {
            //先把字串讀讀進來，每一個都創建一個chartime的物件存放
            Message_Preset();

            for(int i = 0; i < Huffman_code_count; i++)
            {
                //Huffman 演算法
                Huffman();
            }

            //寫入原本讀取的檔案
            string write_file = "";
            for(int i = 0; i < Huffman_code_count; i++ )
            {
                write_file += " " + Huffman_Code[i].char_name + ":" + Huffman_Code[i].Huffmancode;
                if(i != (Huffman_code_count-1))
                {
                    write_file += ",";
                }
            }
            //MessageBox.Show("" + write_file);
            StreamWriter sw = new StreamWriter(file_path);
            sw.WriteLine(HW_Message + "\r\n" + write_file);            // 寫入文字
            sw.Close();                     // 關閉串流

        }

        private void Message_Preset() {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", Multiselect = false, ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding(950), true))
                        {
                            //存取檔案路徑
                            file_path = ofd.FileName;

                            HW_Message = sr.ReadToEnd();

                            //確認有沒有讀取到
                            //MessageBox.Show("" + HW_Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀檔失敗!!!!");
            }
           // MessageBox.Show("" + HW_Message);


            string[] a_whole_new_world = HW_Message.Split('\n');
            int Charset_index = 0;
            //檢查是不是存 字元的name
            bool is_name_store = true;
            foreach(string reg in a_whole_new_world)
            {
                string[] spli = { "  ", " " , "," };
                string[] name1 = reg.Split(spli, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string reg2 in name1)
                {
                    if(is_name_store)
                    {
                        //MessageBox.Show("name " + reg2);
                        //存入char裡面
                        Charset[Charset_index] = new CharTime(""+reg2);
                        Huffman_Code[Charset_index] = new CharTime("" + reg2);
                        Charset_index++;
                    }
                    if (!is_name_store)
                    {
                        // MessageBox.Show("time " + reg2);
                        //把次數存入裡面
                        Charset[Charset_index].store_time(Int32.Parse(reg2));
                        Charset_index++;
                    }

                }
                is_name_store = false;
                char_num = Charset_index;
                Huffman_code_count = Charset_index;
                Charset_index = 0;
            }

           /* MessageBox.Show("總共" + char_num + "個char");
            for(int i = 0; i < char_num; i++)
            {
                MessageBox.Show("" + Charset[i].char_name + " " + Charset[i].char_time);
            }*/
        }

        private void Huffman()
        {
            String reg = "";
            /*  //排序前
              for(int i = 0; i < char_num; i++)
              {
                  reg += Charset[i].char_name + " " + Charset[i].char_time + '\n';
              }
              MessageBox.Show("" + reg);*/

            //排序 -> 最小的兩個相加
            for (int i = 0; i < char_num; i++)
            {
                for (int j = i; j < char_num; j++)
                {
                    if (Charset[i].char_time > Charset[j].char_time)
                    {
                        CharTime reg3 = new CharTime(Charset[i].char_name, Charset[i].char_time);
                        Charset[i] = Charset[j];
                        Charset[j] = reg3;
                    }
                }
            }
            reg = "";

            //解讀最小跟第二小的集合有哪些
            if (!(Charset[0] == null || Charset[1] == null))
            {
                Analysis(Charset[0].char_name, Charset[1].char_name);


                //最小的兩個相加
                Charset[1].char_name = '\n' + Charset[0].char_name + " " + Charset[1].char_name;
                Charset[1].char_time += Charset[0].char_time;

                //把變成null那個推到最後去
                CharTime reg2 = new CharTime(Charset[char_num - 1].char_name, Charset[char_num - 1].char_time);
                Charset[char_num - 1] = Charset[0];
                Charset[0] = reg2;
                Charset[char_num - 1] = null;
                char_num--;

         /*       //排序後
                for (int i = 0; i < Huffman_code_count; i++)
                {
                    reg += Huffman_Code[i].char_name + " " + Huffman_Code[i].Huffmancode + '\n';
                }
                MessageBox.Show("" + reg);*/
            }

        }
        private void Analysis(string small1 , string small2) {
            for(int i = 0; i < Huffman_code_count; i++)
            {
                if (small1.Contains(Huffman_Code[i].char_name))
                {
                    Huffman_Code[i].Huffmancode = "0" + Huffman_Code[i].Huffmancode;
                    //MessageBox.Show("最小：" + Huffman_Code[i].char_name);
                }

                if (small2.Contains(Huffman_Code[i].char_name))
                {
                    Huffman_Code[i].Huffmancode = "1" + Huffman_Code[i].Huffmancode;
                    //MessageBox.Show("最大：" + Huffman_Code[i].char_name);
                }
            }
        }
    }
}
