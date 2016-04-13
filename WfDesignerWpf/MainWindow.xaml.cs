using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.View;
using System.Activities.Statements;
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
using Microsoft.Win32;

namespace WfDesignerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowDesigner _workflowDesigner;
        private string _workflowFilePathName = "temp.xaml";

        public MainWindow()
        {
            InitializeComponent();
            new DesignerMetadata().Register();
            AddDesigner();
            AddTollBox();
            AddPropertyInspector();
        }

        private void AddDesigner()
        {
            _workflowDesigner = new WorkflowDesigner();
            workflowDesignerPanel.Content = _workflowDesigner.View;
        }

        private void AddTollBox()
        {
            var tc = GetToolboxControl();
            toolboxPanel.Content = tc;
        }

        private void AddPropertyInspector()
        {
            if (_workflowDesigner == null)
            {
                return;
            }
            WorkflowPropertyPanel.Content = _workflowDesigner.PropertyInspectorView;
        }

        private ToolboxControl GetToolboxControl()
        {
            var toolboxControl = new ToolboxControl();
            var toolboxCategory = new ToolboxCategory("Activities");
            var sequence = new ToolboxItemWrapper(typeof(Sequence));
            var writeLine = new ToolboxItemWrapper(typeof(WriteLine));
            toolboxCategory.Add(sequence);
            toolboxCategory.Add(writeLine);
            toolboxControl.Categories.Add(toolboxCategory);
            return toolboxControl;
        }

        private void LoadWorkflowFromFile(string fileName)
        {
            _workflowFilePathName = fileName;
            workflowDesignerPanel.Content = null;
            WorkflowPropertyPanel.Content = null;
            _workflowDesigner = new WorkflowDesigner();
            _workflowDesigner.Load(_workflowFilePathName);
            var designerView = _workflowDesigner.Context.Services.GetService<DesignerView>();
            designerView.WorkflowShellBarItemVisibility =
            ShellBarItemVisibility.Arguments |
            ShellBarItemVisibility.Imports |
            ShellBarItemVisibility.MiniMap |
            ShellBarItemVisibility.Variables |
            ShellBarItemVisibility.Zoom;
            workflowDesignerPanel.Content = _workflowDesigner.View;
            WorkflowPropertyPanel.Content = _workflowDesigner.PropertyInspectorView;
        }
               
        private void MenuItem_Click_NewWorkflow(object sender, RoutedEventArgs e)
        {
            _workflowFilePathName = @"WFTemplate.xaml";
            LoadWorkflowFromFile(_workflowFilePathName);
            _workflowFilePathName = "temp.xaml";
        }

        private void MenuItem_Click_LoadWorkflow(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this).Value)
            {
                _workflowFilePathName = openFileDialog.FileName;
            }
            LoadWorkflowFromFile(_workflowFilePathName);
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Click_SaveAs(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Click_RunWorkflow(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabItem_GotFocus_RefreshXamlBox(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
