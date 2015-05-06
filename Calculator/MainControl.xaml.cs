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
        private char inputStr; //Get a character from the button you just clicked
        private string eStr; // used for extracting a digit from the button clicked in the GetExpressionElement method
        private char lastChar; //used for getting the last character of currentTxt
        private string lastOperator; //get the last operator you've clicked
        private string lastOperand;  // get the last operand you've inserted
        private string expression; // refer to the entire expression in the equal_Click method
        private object result; // used for evaulating and getting the result of the expression
        private int numLen; // used so that we could check how many digits we put; totaly you can put 16 digits
        private bool isDivideByZero; //used for checking whether you're facing divide by zero trap or not. (true value is when you're facing that)
        private bool isFinished; //used for checking whther you've just clicked equal sign or operator, so then, you can make operand
        private bool ZeroOperator; //used for check whether there is no operator or not.

        public MainControl()
        {
            InitializeComponent();
            sc.Language = "VBscript"; currentTxt.Text = "0"; ZeroOperator = true; numLen = 1;

            btn_1.Click += num_Click; btn_2.Click += num_Click; btn_3.Click += num_Click; btn_4.Click += num_Click; btn_5.Click += num_Click;
            btn_6.Click += num_Click; btn_7.Click += num_Click; btn_8.Click += num_Click; btn_9.Click += num_Click; btn_0.Click += num_Click;
            dot.Click += dot_Click; clear.Click += clear_Click; equal.Click += equal_Click;
            add.Click += OperatorCheck; subtract.Click += OperatorCheck; multiple.Click += OperatorCheck; divide.Click += OperatorCheck;
            ce.Click += ce_Click;
            backspace.Click += backspace_Click;
        }

        public char GetExressionElement(RoutedEventArgs e) //extract a digit from button
        {
            eStr = e.Source.ToString();
            return eStr[eStr.Length - 1];
        }
        public void num_Click(object sender, RoutedEventArgs e)// number button click handler
        {
            if (isDivideByZero) { return; } //cannot click number buttons after divide by 0 trap happen

            if (numLen < 16) //Exception Handling EH001
            {
                inputStr = GetExressionElement(e);
                InitializedCheckAndInsert(inputStr);
            }
        }
        public void InitializedCheckAndInsert(char paramNum) //
        {
            if (isFinished) // The variable isFinished's value is true; mind you, you gotta clear the original value and then you need to newly start inserting values
                currentTxt.Text = "0";
            isFinished = false; //It means that you've just started to insert digits
            lastChar = currentTxt.Text[currentTxt.Text.Length - 1];

            if (currentTxt.Text.Equals("0") || (lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/')) //related to EH002
            {
                currentTxt.Text = paramNum.ToString();
                numLen = 1; //reset numLet
            }
            else
            {
                currentTxt.Text += paramNum; //relate to EH006
                numLen++; 
            }
        }
        public void OperatorCheck(object sender, RoutedEventArgs e)
        {
            if (isDivideByZero) { return; } //cannot click number buttons after divide by 0 trap happen
            numLen = 1; //related to EH001
            lastOperand = Convert.ToDouble(currentTxt.Text).ToString(); // The first line related to EH015
            inputStr = GetExressionElement(e);

            if (isFinished) // The meaning of the 'isFinished' variable having true is that you've just put an operator most recently. So you need to change the operator when putting another operator in this case.
            {//related to EH008
                if (!resultTxt.Text.Equals("")) //If there is something in resultTxt
                {
                    resultTxt.Text = resultTxt.Text.Remove(resultTxt.Text.Length - 1);
                    resultTxt.Text += inputStr;
                }
                else
                    resultTxt.Text = "0" + inputStr;  //related to EH015
            }
            else // isFinished's vaule is false
            {
                numLen = 1;
                if (!ZeroOperator)
                {
                    string tempStr = resultTxt.Text + lastOperand;  //related to EH015
                    
                    if (tempStr.Contains("/0"))
                    {
                        result = (tempStr.Substring(0, 3).Equals("0/0")) ? "정의되지 않은 결과입니다." : "0으로 나눌 수 없습니다.";
                        isDivideByZero = true;
                    }
                    else { result = sc.Eval(tempStr); }

                    resultTxt.Text += lastOperand + inputStr; // related to EH007 and EH015
                    currentTxt.Text = result.ToString(); //related to EH005
                }
                else //related to EH004
                {
                    resultTxt.Text += lastOperand + inputStr;  //related to EH015
                    currentTxt.Text = lastOperand; //related to EH015
                    ZeroOperator = false;
                }
            }
            isFinished = true; //set true and prepare for insert new operand
            lastOperator = inputStr.ToString(); //store the latest operater
        }
        
        //Get the string of what I've clicked just now
        public void equal_Click(object sender, RoutedEventArgs e)
        {
            // (0/0)               OverflowException
            // (number/0)          DivideByZeroException
            if (isDivideByZero) { return; }
            if (!isFinished)
            {
                lastOperand = currentTxt.Text;
                expression = resultTxt.Text + currentTxt.Text;
                if (expression[expression.Length - 1] == '+' || expression[expression.Length - 1] == '-' || expression[expression.Length - 1] == '*' || expression[expression.Length - 1] == '/')
                    expression += expression.Remove(expression.Length - 1);
            }
            else { expression = currentTxt.Text + lastOperator + lastOperand; }
            
            if(expression.Contains("/0")) //divide by zero error
            {
                result = (expression.Substring(0, 3).Equals("0/0")) ? "정의되지 않은 결과입니다." : "0으로 나눌 수 없습니다.";
                currentTxt.Text = result.ToString();
                isFinished = true;
                numLen = 17;
                isDivideByZero = true;
            }
            else // !expression.Contains("/0")
            {
                result = sc.Eval(expression);
                currentTxt.Text = result.ToString();
                resultTxt.Text = null;
                isFinished = true;
                numLen = 1;
            }
        }
        public void clearAll()
        {
            expression = null;
            lastOperator = null;
            lastOperand = null;
            currentTxt.Text = "0";
            resultTxt.Text = null;
            isDivideByZero = false;
            ZeroOperator = true;
            numLen = 1;
        }
        public void ce_Click(object sender, RoutedEventArgs e)
        {
            currentTxt.Text = "0";
            numLen = 1;
            if (isDivideByZero)
                clearAll();
        }
        public void clear_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
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
