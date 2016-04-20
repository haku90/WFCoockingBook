using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WfDesignerWpf.Models;

namespace WfDesignerWpf
{
    /// <summary>
    /// Interaction logic for SetDocumentPropertiesWindow.xaml
    /// </summary>
    public partial class SetDocumentPropertiesWindow : Window
    {
        public Type DocumentType { get; set; }
        public DocumentPropertiesInfo DocumentPropertiesInfo { get; set; }
        public SetDocumentPropertiesWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if(DocumentType == null)
                return;
            var properties = DocumentType.GetProperties();
            foreach (var property in properties)
            {
                StackPanel.Children.Add(new Label {Content = $"Name: {property.Name} Type: {property.PropertyType.Name}"});
                StackPanel.Children.Add(new CheckBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = $"IsReadOnly{property.Name}", 
                    Content = "IsReadOnly"
                });
                StackPanel.Children.Add(new CheckBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = $"IsVisible{property.Name}",
                    Content = "IsVisible"
                });
                StackPanel.Children.Add(new CheckBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = $"IsFilled{property.Name}",
                    Content = "IsFilled"
                });
                StackPanel.Children.Add(new Separator());
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var documentPropertiesInfo = new DocumentPropertiesInfo();
            documentPropertiesInfo.DocumentType = DocumentType;
            documentPropertiesInfo.PropertiesInfo = GetProperitesInfos();
            DocumentPropertiesInfo = documentPropertiesInfo;
            Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private List<PropertyInfo> GetProperitesInfos()
        {
            var propertiesInfos = new List<PropertyInfo>();
            var propertyInfo = new PropertyInfo();
            foreach (UIElement uiElement in StackPanel.Children)
            {

                var label = uiElement as Label;
                if (label != null)
                {

                   propertyInfo = new PropertyInfo();
                    var nameAndType = label.Content.ToString().Split(':');
                    var name = nameAndType[1].Replace("Type", "");
                    var type = nameAndType[2];
                    propertyInfo.PropName = name;
                    propertyInfo.Type = type;
                    propertiesInfos.Add(propertyInfo);
                }

                var checkBox = uiElement as CheckBox;
                
                if (checkBox != null)
                {
                    if (checkBox.Name.Contains("IsReadOnly"))
                    {
                        propertyInfo.IsReadOnly = (bool)checkBox.IsChecked;
                    }
                    else if (checkBox.Name.Contains("IsVisible"))
                    {
                        propertyInfo.IsVisible = (bool) checkBox.IsChecked;
                    }
                    else if(checkBox.Name.Contains("IsFilled"))
                    {
                        propertyInfo.IsFilled = (bool) checkBox.IsChecked;
                    }
                }
                
            }

            return propertiesInfos;
        }


    }
}
