﻿using System;
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
        private string expression;
        private object result;
        private string inputStr;
        private string lastOperator;
        private string lastOperand;
        private int numLen;
        private bool isFinished;
        private bool ZeroOperator;
        private bool isDivideByZero;
        private char lastChar;

        public MainControl()
        {
            InitializeComponent();
            sc.Language = "VBscript";
            currentTxt.Text = "0";
            ZeroOperator = true;
            numLen = 1;

            btn_1.Click += num_Click; btn_2.Click += num_Click; btn_3.Click += num_Click; btn_4.Click += num_Click; btn_5.Click += num_Click;
            btn_6.Click += num_Click; btn_7.Click += num_Click; btn_8.Click += num_Click; btn_9.Click += num_Click; btn_0.Click += num_Click;
            dot.Click += dot_Click; clear.Click += clear_Click; equal.Click += equal_Click;
            add.Click += OperatorCheck; subtract.Click += OperatorCheck; multiple.Click += OperatorCheck; divide.Click += OperatorCheck;

            backspace.Click += backspace_Click;
        }
        //Exception Handling
        public void InitializedCheckAndInsert(string paramNum)
        {
            if (isFinished) // The variable isFinished's value is true; mind you, you gotta clear the original value and then you need to newly start inserting values
                currentTxt.Text = "0";
            isFinished = false;
            lastChar = currentTxt.Text[currentTxt.Text.Length - 1];

            if (currentTxt.Text.Equals("0") || (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')) //related to EH002
            {
                currentTxt.Text = paramNum;
                numLen = 1;
            }
            else
            {
                currentTxt.Text += paramNum; //relate to EH006
                numLen++;
            }
        }
        public void OperatorCheck(object sender, RoutedEventArgs e)
        {
            if (isDivideByZero)
                return;
            numLen = 1; //related to EH001
            lastOperand = currentTxt.Text;
            inputStr = GetExressionElement(e);

            if (isFinished) // The meaning of the 'isFinished' variable having true is that you've just put an operator most recently. So you need to change the operator when putting another operator in this case.
            {//related to EH008
                if (!resultTxt.Text.Equals(""))
                {
                    resultTxt.Text = resultTxt.Text.Remove(resultTxt.Text.Length - 1);
                    resultTxt.Text += inputStr;
                }
                else
                    resultTxt.Text = currentTxt.Text + inputStr;
            }
            else
            {
                numLen = 1;
                if (!ZeroOperator)
                {
                    string tempStr = resultTxt.Text + currentTxt.Text;
                    
                    if (tempStr.Substring(0, 3).Equals("0/0"))
                    {
                        result = "정의되지 않은 결과입니다.";
                        isDivideByZero = true;
                    }
                    else if(tempStr.Contains("0/0"))
                    {
                        result = "0으로 나눌 수 없습니다.";
                        isDivideByZero = true;
                    }
                    else
                    {
                        result = sc.Eval(tempStr);
                    }
                    resultTxt.Text += currentTxt.Text + inputStr; // related to EH007
                    currentTxt.Text = result.ToString(); //related to EH005

                }
                else //related to EH004
                {
                    resultTxt.Text += currentTxt.Text + inputStr;
                    ZeroOperator = false;
                }
            }
            isFinished = true;
            lastOperator = inputStr;
        }
        public void num_Click(object sender, RoutedEventArgs e)
        {
            if (isDivideByZero)
                return;

            if (numLen < 16) //Exception Handling EH001
            {
                inputStr = GetExressionElement(e);
                InitializedCheckAndInsert(inputStr);
            }
        }
        //Get the string of what I've clicked just now
        public string GetExressionElement(RoutedEventArgs e)
        {
            return e.Source.ToString().Substring(e.Source.ToString().Length - 1, 1); //Get the string of what I've clicked just now
        }

        public void equal_Click(object sender, RoutedEventArgs e)
        {
            if (isDivideByZero)
                return;
            if (!isFinished)
            {
                lastOperand = currentTxt.Text;
                expression = resultTxt.Text + currentTxt.Text;
                if (expression[expression.Length - 1] == '+' || expression[expression.Length - 1] == '-' || expression[expression.Length - 1] == '*' || expression[expression.Length - 1] == '/')
                    expression += expression.Remove(expression.Length - 1);
            }
            else { expression = currentTxt.Text + lastOperator + lastOperand; }



            if (!expression.Contains("0/0"))
            {
                result = sc.Eval(expression);
                currentTxt.Text = result.ToString();
                resultTxt.Text = null;
                isFinished = true;
                numLen = 1;
            }
            else //divide by zero error
            {
                result = "정의되지 않은 결과입니다.";
                currentTxt.Text = result.ToString();
                isFinished = true;
                numLen = 17;

            }

        }
        public void clear_Click(object sender, RoutedEventArgs e)
        {
            currentTxt.Text = "0";
            resultTxt.Text = null;
            isDivideByZero = false;
            ZeroOperator = true;
            numLen = 1;
        }
        public void dot_Click(object sender, RoutedEventArgs e)
        {
            dot_check();
        }
        public void dot_check() //related to EH009
        {
            if (!currentTxt.Text.Contains("."))
            {
                if (isFinished)//related to EH010
                    currentTxt.Text = "0";
                currentTxt.Text += ".";
                numLen = 0; //related to EH003
            }
        }
        public void backspace_Click(object sender, RoutedEventArgs e)
        {//related to EH012
            if (!currentTxt.Text.Equals("0") && !isFinished) { currentTxt.Text = (currentTxt.Text.Length == 1) ? ("0") : (currentTxt.Text.Remove(currentTxt.Text.Length - 1)); }
        }
    }
}
