using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laba_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtOutput.ReadOnly = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            txtBox1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == ' ')
            {
 
                if (e.KeyChar == (char)Keys.Enter)
                {
                    btnExecute.Focus(); 
                }
                return;
            }

            if (e.KeyChar == '-')
            {
                if (txtBox1.SelectionStart == 0 || txtBox1.Text[txtBox1.SelectionStart - 1] == ' ')
                    return;
            }

            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.KeyChar = ',';
                int lastSpace = txtBox1.Text.LastIndexOf(' ', Math.Max(0, txtBox1.SelectionStart - 1));
                string currentPart = txtBox1.Text.Substring(lastSpace + 1);

                if (!currentPart.Contains(","))
                    return;
            }

            e.Handled = true;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string input = txtBox1.Text;
            string[] items = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int n = items.Length;

            if (n == 0)
            {
                MessageBox.Show("напиши числа через пробіл");
                return;
            }

            double[] array = new double[n];

            // перетворюємо кожен текст у число
            for (int i = 0; i < n; i++)
            {
                string normalizedItem = items[i].Replace(',', '.');

                if (!double.TryParse(normalizedItem, NumberStyles.Any, CultureInfo.InvariantCulture, out array[i]))
                {
                    array[i] = 0;
                }
            }

            txtOutput.Text += "Введений масив: ";
            for (int i = 0; i < n; i++)
            {
                txtOutput.Text += array[i] + "  ";
            }
            txtOutput.Text += "\r\n";

            // а) шукаємо кількість від'ємних чисел
            int countNegative = 0;
            for (int i = 0; i < n; i++)
            {
                if (array[i] < 0)
                {
                    countNegative++;
                }
            }
            txtOutput.Text += "Від'ємних елементів: " + countNegative + "\r\n";

            // б) сума модулів після мінімального за модулем
            double minVal = Math.Abs(array[0]);
            int minIdx = 0;

            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(array[i]) < minVal)
                {
                    minVal = Math.Abs(array[i]);
                    minIdx = i;
                }
            }

            double sum = 0;
            for (int i = minIdx + 1; i < n; i++)
            {
                sum += Math.Abs(array[i]);
            }
            txtOutput.Text += "Сума модулів після мінімального: " + sum + "\r\n";

            for (int i = 0; i < n; i++)
            {
                if (array[i] < 0)
                {
                    array[i] = array[i] * array[i]; // квадрат
                }
            }

            double temp;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            txtOutput.Text += "Відсортований масив: ";
            for (int i = 0; i < n; i++)
            {
                txtOutput.Text += Math.Round(array[i], 2) + "  ";
            }
            txtOutput.Text += "\r\n\r\n";


            // 2) двовимірний масив (матриця)
            Random rnd = new Random();

            int rows = 3;
            int cols = 3;
            int[,] matrix = new int[rows, cols]; 

            txtOutput.Text += "Матриця:\r\n";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(1, 10);
                    txtOutput.Text += matrix[i, j] + "\t";
                }
                txtOutput.Text += "\r\n";
            }

            // а)поміняти місцями верхній правий та нижній лівий
            int t1 = matrix[0, 2];
            matrix[0, 2] = matrix[2, 0];
            matrix[2, 0] = t1;

            // б)поміняти місцями нижній правий та верхній лівий
            int t2 = matrix[2, 2];
            matrix[2, 2] = matrix[0, 0];
            matrix[0, 0] = t2;



            txtOutput.Text += "Матриця після заміни кутів:\r\n";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    txtOutput.Text += matrix[i, j] + "\t";
                }
                txtOutput.Text += "\r\n";
            }
            txtOutput.Text += "-----------------------------------------\r\n";
        }

        
    }
}