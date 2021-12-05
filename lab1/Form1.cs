using System.Diagnostics;
using Z.Expressions;

namespace lab1
{
    public partial class Form1 : Form
    {
        Table a;
        int ci = 0, cj = 0;
        Data frame;
        
        public Form1()
        {
            a = new Table();
            //frame = new Data(main);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frame = new Data(main);
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            var cell = main.CurrentCell;
            if (!frame.applyExpression(expr.Text, cell.RowIndex, cell.ColumnIndex))
                MessageBox.Show("Помилкова інструкція");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frame.addColumn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frame.addRow();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ласкаво прошу в програму для збереження та обробки електронних таблиць. Працює вона аналогічно відомим аналогам. Якщо ви їочете змінити значення в клітинці, оберіть її, напишіть вираз в поле зверху та натисніть кнопку Застосувати.");
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frame.setValue(expr, e.RowIndex, e.ColumnIndex);
        }
    }
}