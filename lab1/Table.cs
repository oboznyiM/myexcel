using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Table
    {
        public int width;
        public int height;
        public Cell[,] content;
        public Dictionary<String, int> values = new Dictionary<string, int>();
        public int[,] used;

        public Table()
        {
            height = 5;
            width = 5;
            content = new Cell[height, width];
            used = new int[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    content[i, j] = new Cell();
                    values.Add(Cell.intToLetter(j) + i.ToString(), content[i, j].value.Equals(String.Empty) ? 0 : Int32.Parse(content[i, j].value)); 
                }
        }
        public void addColumn()
        {
            Cell[,] new_content = new Cell[height, width + 1];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    new_content[i, j] = content[i, j];
                }
            for (int i = 0; i < height; i++)
            {
                new_content[i, width] = new Cell();
                values.Add(Cell.intToLetter(width) + i.ToString(), new_content[i, width].value.Equals(String.Empty) ? 0 : Int32.Parse(new_content[i, width].value));
            }
            used = new int[height, width + 1];
            content = new_content;
            width++;
        }

        public void addRow()
        {
            Cell[,] new_content = new Cell[height + 1, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    new_content[i, j] = content[i, j];
                }
            for (int j = 0; j < width; j++)
            {
                new_content[height, j] = new Cell();
                values.Add(Cell.intToLetter(j) + height.ToString(), new_content[height, j].value.Equals(String.Empty) ? 0 : Int32.Parse(new_content[height, j].value));
            }
            used = new int[height + 1, width];
            content = new_content;
            height++;
        }

        public bool check(int i, int j)
        {
            used[i, j] = 1;
            Debug.WriteLine(i + j.ToString()+content[i, j].val.Count);
            foreach (var x in content[i, j].val)
                if (used[x.Item1, x.Item2] == 1)
                    return false;
                else if (used[x.Item1, x.Item2] == 0 && !check(x.Item1, x.Item2))
                    return false;
            used[i, j] = 2;
            return true;
        }
        public void calc(int i, int j)
        {
            string txt = content[i, j].expression;
            if (txt.Equals(String.Empty))
                content[i, j].value = String.Empty;
            else
            {
                Debug.WriteLine(i.ToString() + j);
                foreach (var x in content[i, j].val)
                {
                    Debug.WriteLine(x.Item1 + x.Item2.ToString()+content[i, j].val.Count);
                    if (!values.ContainsKey(Cell.intToLetter(x.Item2) + x.Item1.ToString()))
                        calc(x.Item1, x.Item2);
                    Debug.WriteLine(x.Item1 + x.Item2.ToString() + content[i, j].val.Count+values[Cell.intToLetter(x.Item2) + x.Item1.ToString()]);

                }
                content[i, j].value = Parser.eval(txt, values).ToString();
            }
            values[Cell.intToLetter(j) + i.ToString()] = content[i, j].value.Equals(String.Empty) ? 0 : Int32.Parse(content[i, j].value);
        }
        public bool eval()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    used[i, j] = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (used[i, j] == 0)
                        if (!check(i, j))
                            return false;

            values.Clear();
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (!values.ContainsKey(Cell.intToLetter(j)+i.ToString()))
                    {
                        calc(i, j);
                    }
            /* {  
                 string txt = content[i, j].expression;
                 if (txt.Equals(String.Empty))
                     content[i, j].value = String.Empty;
                 else
                     content[i, j].value = Parser.eval(txt, values).ToString();
                 values[Cell.intToLetter(j) + i.ToString()] = content[i, j].value.Equals(String.Empty) ? 0 : Int32.Parse(content[i, j].value);
             }*/
            return true;
        }
        public int applyExpression(string txt, int r, int c)
        {
            if (r >= height || c >= width || r < 0 || c < 0)
                return 1;
            if (!validateExp(txt))
                return 0;
            content[r, c].setExpression(txt);

            if (!eval())
            {
                content[r, c].retractExpression();
                return 2;
            }
            return 1;
        }
        public bool validateExp(string exp)
        {
            try
            {
                Parser.eval(exp, values);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
