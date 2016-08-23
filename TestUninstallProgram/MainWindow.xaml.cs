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

using System.Management;

namespace TestUninstallProgram
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public ManagementObjectSearcher mSearcher_O;

      public MainWindow()
      {
         InitializeComponent();

         string Query_st = string.Format("select * from Win32_Product where Name='Template Editor'");
         mSearcher_O = new ManagementObjectSearcher(Query_st);
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         ProductCode.Text = GetProductCode(ProductName.Text);

         System.Diagnostics.Process process = new System.Diagnostics.Process();
         System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
         startInfo.FileName = "msiexec.exe";
         startInfo.Arguments = "/x " + ProductCode.Text;
         process.StartInfo = startInfo;
         process.Start();

         // use Process.Start(exe path) to install someting
      }

      public string GetProductCode(string productName)
      {
         foreach (ManagementObject product in mSearcher_O.Get())
            return product["IdentifyingNumber"].ToString();

         return null;
      }
   }
}
