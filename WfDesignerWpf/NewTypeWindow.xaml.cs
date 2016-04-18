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
using System.Windows.Shapes;
using WfDesignerWpf.Services;

namespace WfDesignerWpf
{
    /// <summary>
    /// Interaction logic for NewTypeWindow.xaml
    /// </summary>
    public partial class NewTypeWindow : Window
    {
        private GenerateDllService _generateDllService = new GenerateDllService();
        private static int _filedCount = 1;
        
        public NewTypeWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_NewType(object sender, RoutedEventArgs e)
        {
            StackPanel.Children.Clear();
            StackPanel.Children.Add(new Label {Content = "Name Type", Name = "NameTypeLabel"});
            StackPanel.Children.Add(new TextBox {Name = "NameType"});
            _filedCount = 1;
        }

        private void MenuItem_Click_SaveType(object sender, RoutedEventArgs e)
        {
            var props = CreateProps();
            var className = GetClassName();
            var dllsPath = Properties.Settings.Default.DllsPath;
            _generateDllService.Generate(className, props, dllsPath);
        }

        private void MenuItem_Click_AddField(object sender, RoutedEventArgs e)
        {
            if (StackPanel.Children.Count <= 0)
            {
                MessageBox.Show("Należy utworzyć nowy typ!");
                return;
            }
            AddControls();
            _filedCount++;
        }

        private void AddControls()
        {
            StackPanel.Children.Add(new Label { Content = $"Filed Name{_filedCount}" });
            StackPanel.Children.Add(new TextBox { Name = $"FiledName{_filedCount}" });
            StackPanel.Children.Add(new Label { Content = "Filed Type" });
            StackPanel.Children.Add(new TextBox { Name = $"FiledType{_filedCount}" });
            StackPanel.Children.Add(new Separator());

        }

        private Dictionary<string, Type> CreateProps()
        {
            var props = new Dictionary<string, Type>();
            var controls = StackPanel.Children;
            var filedName = String.Empty;
            Type filedType = null;

            foreach (UIElement control in controls)
            {
                var label = control as Label;
                if (label != null)
                {
                    continue;
                }

                var textBox = control as TextBox;
                if (textBox != null)
                {
                    if (textBox.Name.Contains("FiledName"))
                    {
                        filedName = textBox.Text;
                    }
                    if(textBox.Name.Contains("FiledType"))
                    {
                        filedType = Type.GetType(textBox.Text);
                    }

                    if (!String.IsNullOrEmpty(filedName) && filedType != null)
                    {
                        props.Add(filedName, filedType);
                        filedName = String.Empty;
                        filedType = null;
                    }
                }
            }
            return props;
        }

        private string GetClassName()
        {
            var controls = StackPanel.Children;
            foreach (UIElement control in controls)
            {
                var label = control as Label;
                if (label != null)
                {
                    continue;
                }

                var textBox = control as TextBox;
                if (textBox != null)
                {
                    if (textBox.Name.Contains("NameType"))
                    {
                        return textBox.Text;
                    }
                    
                }
            }
            return String.Empty;
        }


    }
}
