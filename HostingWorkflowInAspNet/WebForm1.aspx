<%@ Page Language="C#" %>
<%@ Import Namespace="System.Activities" %>
<%@ Import Namespace="System.Activities.Statements" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        
        var wf = new Sequence
            {
                Activities =
                {
                    new WriteLine {Text = "Start..."},
                    new FileWriterActivity
                    {
                        FileName = "Test.txt",
                        FileData = "Text content"
                    },
                    new WriteLine {Text = "End..."}
                }
            };
         WorkflowInvoker.Invoke(wf);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Title</title>
</head>
<body>
<form id="HtmlForm" runat="server">
    <div>
        
    </div>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <asp:ListBox ID="ListBox1" runat="server">
        <asp:ListItem></asp:ListItem>
    </asp:ListBox>
</form>
</body>
</html>
