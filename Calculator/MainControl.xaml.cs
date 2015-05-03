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
        string _operation = "0";
        string _operator = null;
        int result = 0;
        int nextNum = 0;
        public MainControl()
        {
            InitializeComponent();
            tb2.Text = "0";
            _1.Click += _1_Click; _2.Click += _2_Click; _3.Click += _3_Click; _4.Click += _4_Click; _5.Click += _5_Click; _6.Click += _6_Click; _7.Click += _7_Click; _8.Click += _8_Click; _9.Click += _9_Click;
            zero.Click += zero_Click; dot.Click += dot_Click; clear.Click += clear_Click; equal.Click += equal_Click; add.Click += add_Click; subtract.Click += subtract_Click; multiple.Click += multiple_Click; divide.Click += divide_Click; backspace.Click += backspace_Click;
        }
        //Exception Handling
        public void InitializedCheckAndInsert(string paramNum)
        {
            if (tb2.Text.Equals("0"))
                tb2.Text = paramNum;
            else
                tb2.Text += paramNum;
        }
        public void OperatorCheck(string paramOperator)
        {
            string lastChar = tb2.Text.Substring(tb2.Text.Length - 1, 1);
            if(lastChar.Equals("/") || lastChar.Equals("*") || lastChar.Equals("+") || lastChar.Equals("-") )
                tb2.Text = tb2.Text.Remove(tb2.Text.Length - 1);
            tb2.Text += paramOperator;
        }
        public void dot_check()
        {
            if (!tb2.Text.Contains("."))
                tb2.Text += ".";
        }
        public void OperationResult()
        {
            
        }
        //Button Click
        void _1_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_1.Content.ToString());
        }
        void _2_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_2.Content.ToString());
        }
        void _3_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_3.Content.ToString());
        }
        void _4_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_4.Content.ToString());
        }
        void _5_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_5.Content.ToString());
        }
        void _6_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_6.Content.ToString());
        }
        void _7_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_7.Content.ToString());
        }
        void _8_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_8.Content.ToString());
        }
        void _9_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(_9.Content.ToString());
        }
        void zero_Click(object sender, RoutedEventArgs e)
        {
            InitializedCheckAndInsert(zero.Content.ToString());
        }
        void backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!tb2.Text.Equals("0"))
                tb2.Text = (tb2.Text.Length == 1) ? ("0") : (tb2.Text.Remove(tb2.Text.Length - 1));
        }
        void divide_Click(object sender, RoutedEventArgs e)
        {
            OperatorCheck(divide.Content.ToString());
        }
        void multiple_Click(object sender, RoutedEventArgs e)
        {
            OperatorCheck(multiple.Content.ToString());
        }
        void subtract_Click(object sender, RoutedEventArgs e)
        {
            OperatorCheck(subtract.Content.ToString());
        }
        void add_Click(object sender, RoutedEventArgs e)
        {
            OperatorCheck(add.Content.ToString());
        }
        void equal_Click(object sender, RoutedEventArgs e)
        {
            OperationResult();
        }
        void clear_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = "0"; tb2.Text = "0";
        }
        void dot_Click(object sender, RoutedEventArgs e)
        {
            dot_check();
        }

       
    }
}
