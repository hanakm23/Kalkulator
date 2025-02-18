using System;
using System.Windows;
using System.Windows.Controls;

namespace kalkulator
{
    public partial class MainWindow : Window
    {
        private string input = "";
        private double num1 = 0;
        private double num2 = 0;
        private char operation;
        private bool isOperationPressed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = (sender as Button).Content.ToString();

            if (double.TryParse(value, out _)) // Čísla 0-9
            {
                if (isOperationPressed)
                {
                    input = "";
                    isOperationPressed = false;
                }

                input += value;
                Display.Text = input;
            }
            else if (value == "C") // Vymazání
            {
                input = "";
                num1 = num2 = 0;
                operation = '\0';
                Display.Text = "0";
            }
            else if (value == "CE") // Vymazání poslední číslice
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1); // Odstraní poslední číslici
                    Display.Text = input.Length > 0 ? input : "0";  // Pokud je prázdno, nastavíme na "0"
                }
            }
            else if (value == "⌫") // Mazání posledního čísla
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Display.Text = input.Length > 0 ? input : "0";
                }
            }
            else if (value == "+/−") // Změna znaménka
            {
                if (input != "" && double.TryParse(input, out double num))
                {
                    input = (-num).ToString();
                    Display.Text = input;
                }
            }
            else if (value == "1/x") // 1 ÷ číslo
            {
                if (double.TryParse(input, out double num))
                {
                    if (num != 0) // Zabránění dělení nulou
                    {
                        input = (1 / num).ToString();
                        Display.Text = input;
                    }
                    else
                    {
                        Display.Text = "Error"; // Chyba při dělení nulou
                    }
                }
            }
            else if (value == "x²") // Druhá mocnina
            {
                if (double.TryParse(input, out double num))
                {
                    input = (num * num).ToString();
                    Display.Text = input;
                }
            }
            else if (value == "²√x") // Druhá odmocnina
            {
                if (double.TryParse(input, out double num) && num >= 0)
                {
                    input = Math.Sqrt(num).ToString();
                    Display.Text = input;
                }
                else
                {
                    Display.Text = "Error"; // Handling negative numbers for square root
                }
            }
            else if (value == "%") // Procenta
            {
                if (double.TryParse(input, out double num))
                {
                    input = (num / 100).ToString();
                    Display.Text = input;
                }
            }
            else if (value == "=") // Výpočet
            {
                if (operation != '\0' && double.TryParse(input, out num2))
                {
                    double result = 0;
                    switch (operation)
                    {
                        case '+': result = num1 + num2; break;
                        case '−': result = num1 - num2; break;
                        case '×': result = num1 * num2; break;
                        case '÷': result = num2 != 0 ? num1 / num2 : double.NaN; break;
                    }
                    Display.Text = result.ToString();
                    input = result.ToString();
                    operation = '\0';
                }
            }
            else if ("+−×÷".Contains(value)) // Operátory
            {
                if (double.TryParse(input, out num1))
                {
                    operation = value[0];
                    isOperationPressed = true;
                }
            }
        }
    }
}
