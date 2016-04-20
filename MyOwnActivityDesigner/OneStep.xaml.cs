using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.View;
using System.Collections.Generic;
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

namespace MyOwnActivityDesigner
{
    // Interaction logic for OneStep.xaml
    public partial class OneStep
    {
        public OneStep()
        {
            InitializeComponent();
            TypePresenter.Context = new EditingContext();
        }

        //private void TypePresenter_TypeChanged(object sender, RoutedEventArgs e)
        //{
        //    var typePresenter = sender as TypePresenter;
        //    var type = typePresenter?.Type;
        //    if(type == null)
        //        return;
        //    var properties = type.GetProperties();
        //    foreach (var property in properties)
        //    {
        //        StackPanel.Children.Add(new Label {Content = "Field"});
        //        StackPanel.Children.Add(new Label {Content = property.Name});
        //        StackPanel.Children.Add(new CheckBox {Content = "ReadOnly", Name = $"ReadOnly{property.Name}"});
        //        StackPanel.Children.Add(new Separator());

        //    }
        //}
    }
}
