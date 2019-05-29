using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4_HtffmanCode
{
    class CharTime
    {
        public String char_name = "";
        public int char_time = 0;
        public String Huffmancode = "";

        public CharTime(String name)
        {
            this.char_name = name;
        }
        public CharTime(String name , int time)
        {
            this.char_name = name;
            this.char_time = time;
        }

        public void store_time(int time) {
            this.char_time = time;
        }
    }
}
