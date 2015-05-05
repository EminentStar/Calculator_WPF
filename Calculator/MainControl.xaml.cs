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
        private MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
        //sc.Language = "VBscript";
        private string expression;
        private object result;
        private string inputStr;
        private string lastOperator;
        private string lastOperand;
        private bool isFinished;
        public MainControl()
        {
            InitializeComponent();
            sc.Language = "VBscript";
            currentTxt.Text = "0";

            btn_1.Click += num_Click; btn_2.Click += num_Click; btn_3.Click += num_Click; btn_4.Click += num_Click; btn_5.Click += num_Click;
            btn_6.Click += num_Click; btn_7.Click += num_Click; btn_8.Click += num_Click; btn_9.Click += num_Click; btn_0.Click += num_Click;
            dot.Click += dot_Click; clear.Click += clear_Click; equal.Click += equal_Click;

            add.Click += OperatorCheck; subtract.Click += OperatorCheck; multiple.Click += OperatorCheck; divide.Click += OperatorCheck;

            backspace.Click += backspace_Click;
        }

        //Exception Handling
        public void InitializedCheckAndInsert(string paramNum)
        {
            string lastChar = currentTxt.Text.Substring(currentTxt.Text.Length - 1, 1);

            if (currentTxt.Text.Equals("0") || (lastChar.Equals("/") || lastChar.Equals("*") || lastChar.Equals("+") || lastChar.Equals("-")))
                currentTxt.Text = paramNum;
            else
                currentTxt.Text += paramNum;
        }

        public void dot_check()
        {
            if (!currentTxt.Text.Contains("."))
                currentTxt.Text += ".";
        }

        void num_Click(object sender, RoutedEventArgs e)
        {
            inputStr = GetExressionElement(e);
            InitializedCheckAndInsert(inputStr);

        }

        public void OperatorCheck(object sender, RoutedEventArgs e)
        {
            lastOperand = currentTxt.Text;
            inputStr = GetExressionElement(e);
            string lastChar = currentTxt.Text.Substring(currentTxt.Text.Length - 1, 1);

            if (lastChar.Equals("/") || lastChar.Equals("*") || lastChar.Equals("+") || lastChar.Equals("-"))
            {
                resultTxt.Text = resultTxt.Text.Remove(resultTxt.Text.Length - 1);
                resultTxt.Text += inputStr;

            }
            else
            {
                //currentTxt.Text += inputStr;
                result = sc.Eval(resultTxt.Text + currentTxt);
                resultTxt.Text += currentTxt.Text + inputStr;
                currentTxt.Text = result.ToString();
                isFinished = true;
            }
            lastOperator = inputStr;
        }

        //Get the string of what I've clicked just now
        public string GetExressionElement(RoutedEventArgs e)
        {
            return e.Source.ToString().Substring(e.Source.ToString().Length - 1, 1); //Get the string of what I've clicked just now
        }

        void backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!currentTxt.Text.Equals("0"))
                currentTxt.Text = (currentTxt.Text.Length == 1) ? ("0") : (currentTxt.Text.Remove(currentTxt.Text.Length - 1));
        }
        void equal_Click(object sender, RoutedEventArgs e)
        {
            expression = resultTxt.Text + currentTxt.Text;
            if (expression[expression.Length - 1] == '+' || expression[expression.Length - 1] == '-' || expression[expression.Length - 1] == '*' || expression[expression.Length - 1] == '/')
                expression += expression.Remove(expression.Length - 1);
            result = sc.Eval(expression);
            currentTxt.Text = result.ToString();
            resultTxt.Text = null;
        }

        void clear_Click(object sender, RoutedEventArgs e)
        {
            resultTxt.Text = null; currentTxt.Text = "0";
        }
        void dot_Click(object sender, RoutedEventArgs e)
        {
            dot_check();
        }
    }
}
