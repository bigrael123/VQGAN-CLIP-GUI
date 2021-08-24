using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VQGanGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public String ProjectPath;
        public MainWindow()
        {
            InitializeComponent();
            if (System.IO.File.Exists("file.txt"))
            {
                string xaml = System.IO.File.ReadAllText("file.txt");
                String content = (String)System.Windows.Markup.XamlReader.Parse(xaml);
                this.ProjectPath = content;
                textBox1.Text = ProjectPath;
            }
            if (System.IO.File.Exists("file2.txt"))
            {
                string xamls = System.IO.File.ReadAllText("file2.txt");
                String contents = (String)System.Windows.Markup.XamlReader.Parse(xamls);
                textBox.Text = contents;
            }
        }
        private void GenerateImage(object sender, RoutedEventArgs e)
        {
            run_cmd();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                ProjectPath = dialog.SelectedPath;
                textBox1.Text = ProjectPath;
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            string xaml = System.Windows.Markup.XamlWriter.Save(this.ProjectPath);
            System.IO.File.WriteAllText("file.txt", xaml);
            string xamltwo = System.Windows.Markup.XamlWriter.Save(this.textBox.Text);
            System.IO.File.WriteAllText("file2.txt", xamltwo);
        }
        private void run_cmd()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.WorkingDirectory = ProjectPath;
            process.StartInfo.Arguments = "/c Python generate.py -p " + "\"textBox.Text\"";
            process.Start();

        }
    }

}
