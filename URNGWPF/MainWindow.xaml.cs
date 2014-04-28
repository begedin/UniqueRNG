using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using URNG;

namespace URNGWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPermutation_Click(object sender, RoutedEventArgs e)
        {
            var amount = Int32.Parse(txtAmount.Text);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            RandomUsingPrimeCongruence randomPermutation = new RandomUsingPrimeCongruence();
            var numbers = randomPermutation.Sequence(amount);

            sw.Stop();

            listMetrics.Items.Add("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");

            listResult.Items.Clear();

            if (numbers.Count != numbers.Distinct().Count())
            {
                listMetrics.Items.Add("List contains DUPLICATES!");
            }
            else
            {
                listMetrics.Items.Add("List is valid");
            }

            //if (chkToFile.IsChecked.Value)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.AppendLine("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");
            //    sb.AppendLine();

            //    foreach (var number in numbers)
            //    {
            //        sb.AppendLine(number.ToString());
            //    }

            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.FileName = "result.txt";
            //    if (dialog.ShowDialog() == true)
            //    {
            //        File.WriteAllText(dialog.FileName, sb.ToString());
            //    }
            //}
            //else 
            //{
            //    foreach (var number in numbers)
            //    {
            //        listResult.Items.Add(number);
            //    }
            //}

        }

        private void btnLSFR_Click(object sender, RoutedEventArgs e)
        {

            var amount = Int32.Parse(txtAmount.Text);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            RandomUsingLFSR randomLSFR = new RandomUsingLFSR();
            List<uint> numbers = randomLSFR.Sequence(amount);

            sw.Stop();

            listMetrics.Items.Add("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");

            listResult.Items.Clear();

            if (numbers.Count != numbers.Distinct().Count())
            {
                listMetrics.Items.Add("List contains DUPLICATES!");
            }
            else
            {
                listMetrics.Items.Add("List is valid");
            }

            //if (chkToFile.IsChecked.Value)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.AppendLine("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");
            //    sb.AppendLine();

            //    foreach (var number in numbers)
            //    {
            //        sb.AppendLine(number.ToString());
            //    }

            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.FileName = "result.txt";
            //    if (dialog.ShowDialog() == true)
            //    {
            //        File.WriteAllText(dialog.FileName, sb.ToString());
            //    }
            //}
            //else
            //{
            //    foreach (var number in numbers)
            //    {
            //        listResult.Items.Add(number);
            //    }
            //}
        }

        private void btnPRBS31_Click(object sender, RoutedEventArgs e)
        {
            var amount = Int32.Parse(txtAmount.Text);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            RandomUsingPRBS31 randomPRBS31 = new RandomUsingPRBS31();
            List<uint> numbers = randomPRBS31.Sequence(amount);

            sw.Stop();

            listMetrics.Items.Add("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");

            listResult.Items.Clear();

            if (numbers.Count != numbers.Distinct().Count())
            {
                listMetrics.Items.Add("List contains DUPLICATES!");
            }
            else
            {
                listMetrics.Items.Add("List is valid");
            }

            //if (chkToFile.IsChecked.Value)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.AppendLine("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");
            //    sb.AppendLine();

            //    foreach (var number in numbers)
            //    {
            //        sb.AppendLine(number.ToString());
            //    }

            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.FileName = "result.txt";
            //    if (dialog.ShowDialog() == true)
            //    {
            //        File.WriteAllText(dialog.FileName, sb.ToString());
            //    }
            //}
            //else
            //{
            //    foreach (var number in numbers)
            //    {
            //        listResult.Items.Add(number);
            //    }
            //}

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listMetrics.Items.Clear();
        }

        private void btnMT_Click(object sender, RoutedEventArgs e)
        {
            var amount = Int32.Parse(txtAmount.Text);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            RandomUsingMersenneTwister randomMT = new RandomUsingMersenneTwister();
            List<uint> numbers = randomMT.Sequence(amount);

            sw.Stop();

            listMetrics.Items.Add("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");

            listResult.Items.Clear();

            if (numbers.Count != numbers.Distinct().Count())
            {
                listMetrics.Items.Add("List contains DUPLICATES!");
            }
            else
            {
                listMetrics.Items.Add("List is valid");
            }

            //if (chkToFile.IsChecked.Value)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    sb.AppendLine("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");
            //    sb.AppendLine();

            //    foreach (var number in numbers)
            //    {
            //        sb.AppendLine(number.ToString());
            //    }

            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.FileName = "result.txt";
            //    if (dialog.ShowDialog() == true)
            //    {
            //        File.WriteAllText(dialog.FileName, sb.ToString());
            //    }
            //}
            //else
            //{
            //    foreach (var number in numbers)
            //    {
            //        listResult.Items.Add(number);
            //    }
            //}
        }
    }
}
