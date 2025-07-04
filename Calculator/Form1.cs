using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enTurn
        {
            Number1,
            Number2
        }
        enum enOperation
        {
             Plus = 1, Minus = 2, Divide = 3, Multiply = 4
        }

        enum enOperationNumber1
        {
            Square = 1, Pourcentage = 2
        }

        enTurn Turn = enTurn.Number1;
        enOperation Operation;

        private void DisableOperationsButtons()
        {
            btPlus.Enabled = false;
            btDivise.Enabled = false;
            btMiness.Enabled = false;
            btx.Enabled = false;
            btSquare.Enabled = false;
            btPourcentage.Enabled = false;
     
        }

        private void EnableAllButtons()
        {
            btPlus.Enabled = true;
            btDivise.Enabled = true;
            btMiness.Enabled = true;
            btx.Enabled = true;
            btSquare.Enabled = true;
            btPourcentage.Enabled = true;
            btEqual.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "0";
            label2.Text = "";
            Turn = enTurn.Number1;
            DisableOperationsButtons();
            btEqual.Enabled = false;
        }
        
        string SNumber1 = "";
        string SNumber2 = "";
        double Number1 = 0;
        double Number2 = 0;

        private void btNumber_Click(object sender, EventArgs e)
        {
            switch (Turn)
            {
                case enTurn.Number1:

                    EnableAllButtons();
                        SNumber1 += ((Button)sender).Tag;
                        label2.Text += ((Button)sender).Tag;
                    
                    break;
                case enTurn.Number2:

                    EnableAllButtons();
                    SNumber2 += ((Button)sender).Tag;
                    label2.Text += ((Button)sender).Tag;

                    btPlus.Enabled = false;
                    btDivise.Enabled = false;
                    btMiness.Enabled = false;
                    btx.Enabled = false;

                    break;
            }
        }

        private void btPoint_Click(object sender, EventArgs e)
        {
            
                switch (Turn)
                {
                    case enTurn.Number1:
                        if (SNumber1.Contains("."))
                        {
                            MessageBox.Show("You can't Add another '.'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SNumber1 += btPoint.Tag;
                            label2.Text = SNumber1;
                        }

                    break;


                case enTurn.Number2:
                    
                        if (SNumber2.Contains("."))
                        {
                            MessageBox.Show("You can't Add another '.'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SNumber2 += btPoint.Tag;
                            int opIndex = label2.Text.LastIndexOf(" ");
                            if (opIndex != -1)
                            {
                                label2.Text = label2.Text.Substring(0, opIndex + 1) + SNumber2;
                            }
                            else
                            {
                                label2.Text = SNumber2; 
                            }
                    }
                    break;
            }
        }

        private void btHandleOperation(object sender, EventArgs e)
        {
            
            btEqual.Enabled = false;
            btPourcentage.Enabled = false;
            btSquare.Enabled = false;

            Button clickedButton = (Button)sender;

            if (clickedButton == btPlus) Operation = enOperation.Plus;
            else if (clickedButton == btMiness) Operation = enOperation.Minus;
            else if (clickedButton == btDivise) Operation = enOperation.Divide;
            else if (clickedButton == btx) Operation = enOperation.Multiply;

            Turn = enTurn.Number2;
            label2.Text = SNumber1 + " " + clickedButton.Tag + " ";
            label1.Text = SNumber1;
        }

     
        private void btEqual_Click(object sender, EventArgs e)
        {

            Number1 = Convert.ToDouble(SNumber1);

            double Result = 0;
            if (Turn == enTurn.Number1)
            {
                Result = Number1;
               
            }

            else
            {

                Number2 = Convert.ToDouble(SNumber2);

                if (Operation == enOperation.Divide && Number2 == 0)
                {
                    MessageBox.Show("Cannot divide by zero\nEnter Number 2 again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    int index = label2.Text.IndexOf(SNumber2);
                    if (index != -1)
                    {
                        label2.Text = label2.Text.Remove(index, SNumber2.Length);
                        label2.Text = label2.Text.Replace("%", "");
                        label2.Text = label2.Text.Replace("²", "");
                    }
                    SNumber2 = "";
                    btEqual.Enabled = false; 
                    return;
                }

                switch (Operation)
                {

                    case enOperation.Plus:
                        Result = Number1 + Number2;
                        break;
                    case enOperation.Minus:
                        Result = Number1 - Number2;

                        break;
                    case enOperation.Divide:
                        Result = Number1 / Number2;

                        break;
                    case enOperation.Multiply:
                        Result = Number1 * Number2;
                        break;
                }
            }

            label1.Text = Result.ToString();
            label2.Text = Result.ToString();
            Turn = enTurn.Number1;
            SNumber1 = Result.ToString();
            SNumber2 = "";
            Number1 = Result;
            Number2 = 0;
            EnableAllButtons();

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            DisableOperationsButtons();
            btEqual.Enabled = false;
            label1.Text = "0";
            label2.Text = "";
            SNumber1 = "";
            SNumber2 = "";
            Number1 = 0;
            Number2 = 0;

            Turn = enTurn.Number1;

        }

        private void btReverseSign_Click(object sender, EventArgs e)
        {
            switch (Turn)
            {
                case enTurn.Number1:
                    if (SNumber1 != "")
                    {
                        if (SNumber1.StartsWith("-"))
                            SNumber1 = SNumber1.Substring(1); // Remove the negative sign
                        else
                            SNumber1 = "-" + SNumber1; // Add the negative sign

                        // Update label2 only for the first number without breaking the expression
                        label2.Text = SNumber1;
                    }
                    break;

                case enTurn.Number2:
                    if (SNumber2 != "")
                    {
                        if (SNumber2.StartsWith("-"))
                            SNumber2 = SNumber2.Substring(1);
                        else
                            SNumber2 = "-" + SNumber2;

                        // Update only the last number after the operator
                        int opIndex = label2.Text.LastIndexOf(" ");
                        if (opIndex != -1)
                        {
                            label2.Text = label2.Text.Substring(0, opIndex + 1) + SNumber2;
                        }
                    }
                    break;
            }
        }


        private void btSquarePourcentage(object sender, EventArgs e)
        {

            Button tempBut = (Button)sender;
            double Result = 0;

            string selectedNumber = (Turn == enTurn.Number1) ? SNumber1 : SNumber2;

            label2.Text += tempBut.Tag;

            if (tempBut.Tag.ToString() == "²")
            {
                Result = Convert.ToDouble(selectedNumber) * Convert.ToDouble(selectedNumber);
            }
            else if (tempBut.Tag.ToString() == "%")
            {
                Result = Convert.ToDouble(selectedNumber) / 100;
            }

            if (Turn == enTurn.Number1)
                SNumber1 = Result.ToString();
            else
                SNumber2 = Result.ToString();

        }

       
    }
}
