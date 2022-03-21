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
using Microsoft;
using System.Windows.Threading;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int ColVoprt;
      
        Label lq;
        RadioButton[,] rb;
        int k;
        public static int pv;
        String[,] Info;
        DateTime now;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            now = DateTime.Now;
            timer.Start();

        }
        private void Result()
        {
            MessageBox.Show("Тест окончен.\nНачало: "+now.ToString()+"\nКонец: "+DateTime.Now.ToString()+"\nПравильных ответов: " + k);
            btnCheck.IsEnabled = false;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.Subtract(now).ToString(@"mm\:ss");
            if (lblTime.Content.ToString() == "10:00")
                Result();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
            for (int j=0;j<ColVoprt-1;j++)
            {
                int var = Convert.ToInt32(Info[j,6]);
                if (rb[j,var-1].IsChecked==true)
                {
                    k++;
                    rb[j, var - 1].Background = new SolidColorBrush(Colors.Green);
                    
                }
                else
                {
                    for (int i=0;i<5;i++)
                    {
                        if (rb[j, i].IsChecked == true)
                        {
                            rb[j, i].Background = new SolidColorBrush(Colors.Red);
                            
                        }
                        break;
                    }
                }
            }
            Result();
            k = 0;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            ReadExcelMethod();
            rb = new RadioButton[ColVoprt - 1, 5];
            for (int j = 0; j < ColVoprt-1; j++)
            {
                StackPanel dynamicStackPanel = new StackPanel();
                dynamicStackPanel.Background = new SolidColorBrush(Colors.LightBlue);
                dynamicStackPanel.Orientation = Orientation.Vertical;

                lq = new Label();
                lq.Content = Info[j,0];
                lq.FontSize = 23;
                dynamicStackPanel.Children.Add(lq);
                for (int i = 0; i < 5; i++)
                {
                    rb[j,i] = new RadioButton();
                    rb[j,i].FontSize = 15;
                    rb[j,i].Content = Info[j, i + 1];
                    dynamicStackPanel.Children.Add(rb[j,i]);
                }
                dynamicStackPanel.Margin = new Thickness(3, 5, 0, 0);
                StackMain.Children.Add(dynamicStackPanel);
            }
            
            k = 0;
        }
        private void ReadExcelMethod()
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(@"C:\Users\rmatv\source\repos\WpfTest\WpfTest\test.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            ColVoprt = excelRange.Rows.Count;
            Info = new String[ColVoprt,7];
            pv = 0;
            for (int i = 1; i < ColVoprt+1; i++)
            {
                Info[i - 1, 0] = Convert.ToString((excelRange.Cells[i + 1, 1] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1, 1] = Convert.ToString((excelRange.Cells[i + 1, 2] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1,2] = Convert.ToString((excelRange.Cells[i + 1, 3] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1,3] = Convert.ToString((excelRange.Cells[i + 1, 4] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1,4]= Convert.ToString((excelRange.Cells[i + 1, 5] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1, 5] = Convert.ToString((excelRange.Cells[i + 1, 6] as Microsoft.Office.Interop.Excel.Range).Value2);
                Info[i - 1, 6] = Convert.ToString((excelRange.Cells[i + 1, 7] as Microsoft.Office.Interop.Excel.Range).Value2);
            }
            excelBook.Close(true, null, null);
            excelApp.Quit();
        }
    }

    }

