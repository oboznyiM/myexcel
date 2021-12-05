using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Data
    {
        public Table a;
        public DataGridView main;
        public Data(DataGridView _main)
        {
            a = new Table(5, 5);
            main = _main;
            add_data();
        }
        public void add_data()
        {

            int height = a.height;
            int width = a.width;

            main.ColumnCount = width;
            main.Rows.Clear();
            Debug.WriteLine(height);
            for (int r = 0; r < height; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(main);
                row.HeaderCell.Value = r.ToString();
                for (int c = 0; c < width; c++)
                {
                    row.Cells[c].Value = a.content[r, c].value;
                }

                this.main.Rows.Add(row);
            }
            for (int c = 0; c < width; c++)
                main.Columns[c].HeaderText = Cell.intToLetter(c).ToString();
        }
        /*
         
         */
       
        public bool applyExpression(string txt, int r, int c)
        {
            int res = a.applyExpression(txt, r, c);
            if (res == 0)
                return false;
            if (res == 2)
            {
                MessageBox.Show("Рекурсія в виразі");
                return true;
            }
            for (int i = 0; i < a.height; i++)
                for (int j = 0; j < a.width; j++)
                {
                    Debug.WriteLine("HEH" + i.ToString() + j.ToString() + a.content[i, j].value);
                    main[j, i].Value = a.content[i, j].value;
                }
            return true;
        }

        public void setValue(TextBox expr, int i, int j)
        {
            if (i >= a.height || j >= a.width || j < 0 || i < 0)
                return;
            expr.Text = a.content[i, j].expression;   
        }

        public void addColumn()
        {
            a.addColumn();
            add_data();
        }

        public void addRow()
        {
            a.addRow();
            add_data();
        }

        public bool removeColumn()
        {
            if (a.removeColumn())
            {
                add_data();
                return true;
            }
            return false;
        }
        public bool removeRow()
        {
            if (a.removeRow())
            {
                add_data();
                return true;
            }
            return false;
        }
    }
}
