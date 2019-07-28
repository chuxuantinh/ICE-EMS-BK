<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="ProCopyDispatch.aspx.cs" Inherits="project_ProCopyDispatch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
Project Copy Dispatch
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Project copy Dispatch" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Project Copy  Dispatch</h1></div><br />
<br />
<center>View By:&nbsp;&nbsp;<asp:DropDownList ID="ddlViewBy" CssClass="txtbox" runat="server"><asp:ListItem Value="All" Text="All" /><asp:ListItem Value="SID" Text="Membership No" /><asp:ListItem Value="ProjectNo" Text="ProjectNo" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtView" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" Text="View" OnClick="btnView_Click" runat="server" CssClass="btnsmall"/></center>
<script>
    function toggleA1w(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
        var imageEle = document.getElementById(switchImgTag);
        var imageEle = document.getElementById(switchImgTag);
        if (ele.style.display == "block") {
            ele.style.display = "none";
            imageEle.innerHTML = '<img src="../images/plus.png">';
        }
        else {
            ele.style.display = "block";
            imageEle.innerHTML = '<img src="../images/minus.png">';
        }
    }
</script>
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><h1>Project Record</h1>
<div id="Div1x" style="display: block;">
<div  id="divGridPrCpySub" style="width: 100%; overflow:scroll; height:250px;">
   <script type="text/javascript">
       function selectAll(invoker) {
           var inputElements = document.getElementsByTagName('input');
           for (var i = 0; i < inputElements.length; i++)
           {
               var myElement = inputElements[i];
               if (myElement.type === "checkbox") {
                   myElement.checked = invoker.checked;
               }
           }
       } 
</script>
<asp:GridView ID="GridDispatch" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2"  Width="100%">
<Columns>
    <asp:TemplateField ><HeaderTemplate><asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
</Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center"/>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView>
</div>
</div>
</div>
</div>
<br />
<center>
<p>Send Project copies for Project Approval</p>
Disptach Date:&nbsp;&nbsp;<asp:TextBox ID="txtcDate" runat="server" CssClass="txtbox" 
        Font-Bold="true" Width="100px" />&nbsp;&nbsp;&nbsp;<dev:CalendarExtender ID="txtcDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="cal" PopupPosition="BottomRight" TargetControlID="txtcDate" />&nbsp;<img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /><br />
<asp:Label ID="lblException" runat="server" ></asp:Label><br />
<asp:Button ID="btnSave" CssClass="btnsmall" runat="server" Text="Save" OnClick="btnSave_Click" /><br /><br /><br />
</center>
<br />
<br />
<br />
<br />
<br />
</asp:Content>

