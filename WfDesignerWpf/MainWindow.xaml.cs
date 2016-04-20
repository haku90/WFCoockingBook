using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using CreateFileWriterActivity;
using Microsoft.Win32;
using MyOwnActivityDesigner;


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
            ImportReferences();
            AddDesigner();
            AddTollBox();
            AddPropertyInspector();
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void MenuItem_Click_SaveAs(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog(this).Value)
            {
                _workflowFilePathName = saveDialog.FileName;
                _workflowDesigner.Save(_workflowFilePathName);
                MessageBox.Show("Save Ok");
                Title = $"Workflow Designer - {_workflowFilePathName}";
            }
        }

        private void MenuItem_Click_Test(object sender, RoutedEventArgs e)
        {
            var items = _workflowDesigner.Context.Items;
            var selectionView =
                items.FirstOrDefault(i => i.ItemType.FullName.Equals("System.Activities.Presentation.View.Selection"));
            var selectedObjects = (selectionView as Selection).SelectedObjects;
            var selectedItem = selectedObjects.First();
            var prop = selectedItem.Properties.ToList();
            var selectedDocumentTypeProp = prop.FirstOrDefault(p => p.Name.Equals("DocumentType"));
            if (selectedDocumentTypeProp == null)
            {
                return;
            }
            var documentType = selectedDocumentTypeProp.ComputedValue as Type;
            var setDocumentPropertiesWindow = new SetDocumentPropertiesWindow();
            setDocumentPropertiesWindow.DocumentType = documentType;
            setDocumentPropertiesWindow.ShowDialog();
            if (setDocumentPropertiesWindow.DocumentPropertiesInfo != null)
            {
                var documentProperitesInfo = setDocumentPropertiesWindow.DocumentPropertiesInfo;
                documentProperitesInfo.ActivityName = _workflowFilePathName;
                documentProperitesInfo.StageName = (string)prop[3].ComputedValue;
                documentProperitesInfo.StageId = (string) prop[4].ComputedValue;
            }
        }

        private void MenuItem_Click_RunWorkflow(object sender, RoutedEventArgs e)
        {
            Save();
            var activity = GetActivity();
            var wfApp = new WorkflowApplication(activity);
            var visualTracking = new VisualTracking(_workflowDesigner);
            wfApp.Extensions.Add(visualTracking);
            wfApp.Run();
        }

        private void TabItem_GotFocus_RefreshXamlBox(object sender, RoutedEventArgs e)
        {
            if (_workflowDesigner.Text != null)
            {
                _workflowDesigner.Flush();
                xamlTextBox.Text = _workflowDesigner.Text;
            }
        }

        private void ImportReferences()
        {
            var path = Properties.Settings.Default.DllsPath;
            var assemblies = new List<Assembly>();
            foreach (var dllFile in Directory.GetFiles(path, "*.dll"))
            {
                var assembly = Assembly.LoadFile(dllFile);
                AppDomain.CurrentDomain.Load(assembly.GetName());
                assemblies.Add(assembly);
            }
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

            var activitiesToolboxCategory = new ToolboxCategory("Activities");
            var sequence = new ToolboxItemWrapper(typeof(Sequence));
            var writeLine = new ToolboxItemWrapper(typeof(WriteLine));
            var fileWriter = new ToolboxItemWrapper(typeof(FileWriterActivity));
            var ownActivity = new ToolboxItemWrapper(typeof(MyOwnActivityDesigner.OneStepActivity));

            activitiesToolboxCategory.Add(sequence);
            activitiesToolboxCategory.Add(writeLine);
            activitiesToolboxCategory.Add(fileWriter);
            activitiesToolboxCategory.Add(ownActivity);

            var documentActivitiesToolboxCategory = new ToolboxCategory("Documents");
            var document = new ToolboxItemWrapper(typeof(Document));
            documentActivitiesToolboxCategory.Add(document);

            toolboxControl.Categories.Add(activitiesToolboxCategory);
            toolboxControl.Categories.Add(documentActivitiesToolboxCategory);
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

        private void Save()
        {
            if (_workflowFilePathName.Equals("temp.xml"))
            {
                var saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog(this).Value)
                {
                    _workflowFilePathName = saveDialog.FileName;
                    _workflowDesigner.Save(_workflowFilePathName);
                    MessageBox.Show("Save Ok");
                    Title = $"Workflow Designer - {_workflowFilePathName}";
                }
                else
                {
                    return;
                }
            }
            else
            {
                _workflowDesigner.Save(_workflowFilePathName);
                MessageBox.Show("Save ok");
            }
            LoadWorkflowFromFile(_workflowFilePathName);
        }

        private Activity GetActivity()
        {
            _workflowDesigner.Flush();
            var stringReader = new StringReader(_workflowDesigner.Text);
            var root = ActivityXamlServices.Load(stringReader);
            return root;
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
                LoadWorkflowFromFile(_workflowFilePathName);
            }
            
        }

        private void MenuItem_Click_NewType(object sender, RoutedEventArgs e)
        {
            var newTypeWindow = new NewTypeWindow();
            newTypeWindow.ShowDialog();
            ImportReferences();
        }


    }
}
