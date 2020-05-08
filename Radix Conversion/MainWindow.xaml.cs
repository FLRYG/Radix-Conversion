using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Diagnostics;

namespace Radix_Conversion
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid[] grid;
        private TextBlock[] textBlock;
        private TextBox[] textBox;

        private Radix radix;
        private int tableSize;

        public MainWindow()
        {
            InitializeComponent();
            Debug.Print("aaaaa");

            radix = new Radix();
            tableSize = radix.GetTableSize();
            SetRadixComboBox();
            Init();
        }

        private void Init()
        {
            grid = new Grid[tableSize-1]; ;
            for (int i = 0; i < tableSize - 1; i++) grid[i] = new Grid();
            textBlock = new TextBlock[tableSize - 1];
            for (int i = 0; i < tableSize - 1; i++) textBlock[i] = new TextBlock();
            textBox = new TextBox[tableSize - 1];
            for (int i = 0; i < tableSize - 1; i++) textBox[i] = new TextBox();

            for (int i = 0; i < tableSize - 1; i++)
            {
                var rowdef1 = new RowDefinition();
                var rowdef2 = new RowDefinition();
                rowdef1.Height = GridLength.Auto;
                rowdef2.Height = new GridLength(4.0);
                var coldef1 = new ColumnDefinition();
                var coldef2 = new ColumnDefinition();
                var coldef3 = new ColumnDefinition();
                coldef1.Width = new GridLength(16.0);
                coldef2.Width = new GridLength(4.0);
                coldef3.Width = new GridLength(1.0, GridUnitType.Star);

                grid[i].RowDefinitions.Add(rowdef1);
                grid[i].RowDefinitions.Add(rowdef2);
                grid[i].ColumnDefinitions.Add(coldef1);
                grid[i].ColumnDefinitions.Add(coldef2);
                grid[i].ColumnDefinitions.Add(coldef3);

                Grid.SetRow(textBlock[i], 0);
                Grid.SetColumn(textBlock[i], 0);
                textBlock[i].Text = (i+2).ToString();
                textBlock[i].HorizontalAlignment = HorizontalAlignment.Left;
                textBlock[i].VerticalAlignment = VerticalAlignment.Center;
                textBlock[i].FontSize = 12;

                Grid.SetRow(textBox[i], 0);
                Grid.SetColumn(textBox[i], 2);
                textBox[i].IsReadOnly = true;

                grid[i].Children.Add(textBlock[i]);
                grid[i].Children.Add(textBox[i]);

                if (i <= (tableSize - 1) / 2)
                {
                    stackPanel_left.Children.Add(grid[i]);
                }
                else
                {
                    stackPanel_right.Children.Add(grid[i]);
                }
            }
        }

        private void SetRadixComboBox()
        {
            int[] tmp = new int[tableSize-1];
            for (int i = 0; i < tableSize - 1; i++) tmp[i] = i + 2;
            radixComboBox.ItemsSource = tmp;
        }

        private void inputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /*string s = e.Text;
            char c = s[s.Length - 1];
            if (radixComboBox.SelectedItem != null)
            {
                int n = (int)radixComboBox.SelectedItem;
                if (!radix.Contain(c, n))
                {
                    ainputTextBox.Text.Remove(s.Length - 1);
                }
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (inputTextBox.Text != null && radixComboBox.SelectedItem != null)
            {
                string input = inputTextBox.Text;
                int inputRadix = (int)radixComboBox.SelectedItem;
                for (int i = 0; i < tableSize - 1; i++)
                {
                    string res = radix.Conversion(input, inputRadix, i + 2);
                    textBox[i].Text = res;
                }
            }
        }
    }
}