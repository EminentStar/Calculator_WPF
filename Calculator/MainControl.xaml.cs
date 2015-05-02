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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        //string _operation = "0";
        string _operator = null;
        int result = 0;
        int nextNum = 0;
        int test = 0;

        public MainControl()
        {
            InitializeComponent();
            tb.Text = "0";

            _1.Click += _1_Click; _2.Click += _2_Click; _3.Click += _3_Click;
            _4.Click += _4_Click; _5.Click += _5_Click; _6.Click += _6_Click;
            _7.Click += _7_Click; _8.Click += _8_Click; _9.Click += _9_Click;
            zero.Click += zero_Click; dot.Click += dot_Click; clear.Click += clear_Click;
            equal.Click += equal_Click; add.Click += add_Click; subtract.Click += subtract_Click;
            multiple.Click += multiple_Click; divide.Click += divide_Click; backspace.Click += backspace_Click;
        }

        public void InitializedCheckAndInsert(string paramNum)
        {
            if (tb.Text.Equals("0"))
                tb.Text = paramNum;
            else 
                tb.Text += paramNum;
        }

        void _1_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("1");
        }
        void _2_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("2");
        }
        void _3_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("3");
        }
        void _4_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("4");
        }
        void _5_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("5");
        }
        void _6_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("6");
        }
        void _7_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("7");
        }
        void _8_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("8");
        }
        void _9_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("9");
        }
        void zero_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert("0");
        }
        void backspace_Click(object sender, RoutedEventArgs e)
        {
            
        }
        void divide_Click(object sender, RoutedEventArgs e)
        {
        }
        void multiple_Click(object sender, RoutedEventArgs e)
        {
        }
        void subtract_Click(object sender, RoutedEventArgs e)
        {
        }
        void add_Click(object sender, RoutedEventArgs e)
        {
        }
        void equal_Click(object sender, RoutedEventArgs e)
        {
        }
        void clear_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "0";
        }
        void dot_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
