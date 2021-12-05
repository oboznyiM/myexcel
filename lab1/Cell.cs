using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Cell
    {
        public string expression;
        public string oldExpression = String.Empty;
        public string value;
        public List<(int, int)> val = new List<(int, int)> ();
        public bool evaluated;
        public Cell ()
        {
            value = "";
            expression = String.Empty;
        }
        public List<(int, int)> getDependency(string exp)
        {
            var res = new List<(int, int)>();
            int cur_x = -1;
            string cur_y = String.Empty;
            foreach(char c in exp)
            {
                if (Char.IsLetter(c))
                    cur_y += c;
                else if (Char.IsDigit(c) && !cur_y.Equals(String.Empty))
                {
                    if (cur_x == -1)
                        cur_x = 0;
                    cur_x *= 10;
                    cur_x += (c - '0');
                } else
                {
                    if (!cur_y.Equals(String.Empty) && cur_x != -1)
                        res.Add((cur_x, letterToInt(cur_y)));
                    cur_y = String.Empty;
                    cur_x = -1;
                }
            }

            if (!cur_y.Equals(String.Empty) && cur_x != -1)
                res.Add((cur_x, letterToInt(cur_y)));
            return res;
        }
        public void setExpression(string exp)
        {
            val = getDependency(exp);
            oldExpression = expression;
            expression = exp;
        }
        public void retractExpression()
        {
            expression = oldExpression;
        }
        public static string intToLetter(int x)
        {
            int len = 1, cnt = 26;
            while (cnt <= x)
            {
                x -= cnt;
                len++;
                cnt *= 26;
            }
            string res = "";
            while (x > 0)
            {
                res += (char)((x % 26) + 'A');
                x /= 26;
            }
            while (res.Length < len)
                res += "A";
            char[] charArray = res.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static int letterToInt(string a)
        {
            int cnt = 1, res = 0;
            for (int i = 0; i < a.Length; i++)
            {
                res += cnt;
                cnt *= 26;
            }
            res -= 1;
            int val = 0;
            foreach(char c in a)
            {
                val *= 26;
                val += (c - 'A');
            }
            return res + val;
        }
    }
}
