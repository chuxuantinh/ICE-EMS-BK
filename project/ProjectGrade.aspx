<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProjectGrade.aspx.cs" Inherits="project_ProjectGrade" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Grading Submission</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td><asp:Label ID="lblNext" runat="server" Text="Project Grading" CssClass="redirecttabhome"/></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdPnlComplete" runat="server">
<Triggers>
<asp:PostBackTrigger ControlID="btnView" />
<asp:PostBackTrigger ControlID="btnUpdate" />
<asp:PostBackTrigger ControlID="btnRefresh" />
</Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1>Project Grading</h1></div><br />
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false" />
<center> Select Type:&nbsp;<asp:DropDownList ID="ddlSelect" runat="server" CssClass="txtbox" Width="130px" onselectedindexchanged="ddlSelect_SelectedIndexChanged" AutoPostBack="true"><asp:ListItem Value="Enroll" Text="Membership No"/><asp:ListItem Value="IMID" Text="IMID"/><asp:ListItem Value="InsID" Text="Institution ID"/></asp:DropDownList>
&nbsp;<asp:TextBox ID="txtView" runat="server" CssClass="txtbox" Width="80px"/>&nbsp;<asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" onclick="btnView_Click" /><br />
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Red" /></center><br />
<asp:Panel ID="pnlComplete" runat="server" Visible="false"><div class="togalfees" style="width:100%"><div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>Project Grading</b><br /><br />
<div id="Div1x" style="display: block;"><div  id="divProjGrade" style="width: 100%; overflow:scroll; height:180px;">
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 
</script>
<asp:GridView ID="GridProjGrade" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="5" OnRowDataBound="GridProjGrade_RowDataBound" Width="100%">
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<Columns><asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="Grading"><ItemTemplate><asp:DropDownList ID="ddlGrade" runat="server" CssClass="txtbox" Width="50px"><asp:ListItem Text="N/A" Value="N/A" /><asp:ListItem Text="A" Value="A" /><asp:ListItem Text="A+" Value="A+" /><asp:ListItem Text="B" Value="B" /><asp:ListItem Text="B+" Value="B+" /><asp:ListItem Text="Absent" Value="Absent" /></asp:DropDownList></ItemTemplate></asp:TemplateField></Columns>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" /><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView></div></div></div></div>
<center>Project Grade Date:&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="txtbox"/><dev:CalendarExtender TargetControlID="txtDate" runat="server" PopupPosition="BottomRight" Format="dd/MM/yyyy" PopupButtonID="cal1"></dev:CalendarExtender>
&nbsp;<img id="cal1" runat="server" alt="Cal" src="../images/cal.png" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpdate" Text="Update" CssClass="btnsmall" runat="server" onclick="btnUpdate_Click" />&nbsp;<asp:Button ID="btnrefresh" runat="server" Text="Refresh" CssClass="btnsmall" Height="25px" onclick="btnrefresh_Click" /></center>
</asp:Panel><br />
<asp:Panel runat="server" ID="pnlspc" Height="460px"/>
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>