<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="UpdProStatus.aspx.cs" Inherits="project_UpdProStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update Project Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Update Project Status" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlcomp" runat="server">
<ContentTemplate>
<div class="fromRegisterlbl"><h1>Update Project Status</h1></div>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center> Session:&nbsp;<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; 
<asp:TextBox ID="txtSession" runat="server" Width="72px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"/> View By:&nbsp;<asp:DropDownList ID="ddlViewBy" runat="server" OnSelectedIndexChanged="ddlViewBy_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Value="Status" Text="Status" /><asp:ListItem Value="Membership" Text="Membership No." /></asp:DropDownList>
<br /><br />
<asp:Panel ID="pnlStatus" runat="server" >
Project Status:&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" ForeColor="Brown" Font-Bold="true" Width="170px" AutoPostBack="True" CssClass="txtbox" onselectedindexchanged="ddlStatus_SelectedIndexChanged" >
<asp:ListItem Text="Selected" Value="Selected"/>
<asp:ListItem Text="ProformaASubmitted" Value="ProformaASubmitted"/>
<asp:ListItem Text="ProformaAApproved" Value="ProformaAApproved"/>
<asp:ListItem Text="ProformaBSubmitted" Value="ProformaBSubmitted"/>
<asp:ListItem Text="ProformaBApproved" Value="ProformaBApproved"/>
<asp:ListItem Text="CopySubmitted" Value="CopySubmitted"/>
<asp:ListItem Text="CopyPending" Value="CopyPending" />
<asp:ListItem Text="CopyDispatched" Value="CopyDispatched" />
<asp:ListItem Text="Approved" Value="Approved"/>
<asp:ListItem Text="Rejected" Value="Rejected"/>
</asp:DropDownList>&nbsp;&nbsp;Proforma A Status:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" Text="View" CssClass="btnsmall" OnClick="btnView_Click" />
</asp:Panel>
<asp:Panel ID="pnlSID" runat="server">
Membership No: &nbsp;&nbsp;<asp:TextBox ID="txtSIDStatus" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSIDView" Text="View" runat="server" OnClick="btnSIDView_Click" />
</asp:Panel>
</center><br />
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
</div><h1></h1>
<br />
<div id="Div1x" style="display: block;">
<div  id="divApprPerfA" style="width: 100%; overflow:scroll; height:250px;">
<script type="text/javascript">
    function selectAll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    }
</script>
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Continue ?") == true)
            return true;
        else
            return false;
    }
</script>
<asp:GridView ID="GridupdProStatus" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%">
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
<Columns><asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAll" runat="server" OnClick="selectAll(this)" /></HeaderTemplate>
<ItemTemplate><asp:CheckBox ID="chkStatus" runat="server" /></ItemTemplate>
</asp:TemplateField>
</Columns>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div>
<br />
<center><b>Edit Status:</b>&nbsp;<asp:DropDownList ID="ddlEditStatus" runat="server" Font-Bold="true" ForeColor="Brown" CssClass="txtbox" Width="170px"/>&nbsp;&nbsp;Pro-A Status:&nbsp;<asp:DropDownList ID="ddlSynosisStatus" runat="server" ForeColor="Brown" Font-Bold="true" Width="150px" CssClass="txtbox" /><asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return ConfirmApp();" CssClass="btnsmall" Text="Update Status"/></center>
</div><hr />
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
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div><div style="padding:5px; color:White; font-size:15px;"><b>&nbsp;Student Membership No:&nbsp;<asp:TextBox ID="txtSid" runat="server" AutoPostBack="true" OnTextChanged="txtSID_OnTextChanted"  CssClass="txtbox" ForeColor="Brown" Font-Bold="true" /></b><br />
<br />
<div id="Div1" style="display: block;">
<div  id="divGridPrCpySub" style="width: 100%; overflow:scroll; height:120px;">
<asp:GridView ID="GridEval" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%" onselectedindexchanged="GridEval_SelectedIndexChanged">
<Columns>
<asp:CommandField HeaderText="Select" ShowSelectButton="True" />
</Columns>
<EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center"/>
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div>
</div>
<center>
<p>Select Project Record form above grid panel and set status as <b>Running</b> and <b>OldProject</b> of selected proejct record </p>
<asp:Button ID="btnRunning" runat="server" Text="Running Project" OnClick="btnRunning_Click" CssClass="bigbutton" Width="200px" />&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnOldProject" runat="server" Text="Set As Old Project" OnClick="btnOldProject_click" CssClass="bigbutton" Width="200px"/>
</center>
<br />
</ContentTemplate></asp:UpdatePanel>
</div><br />
</asp:Content>