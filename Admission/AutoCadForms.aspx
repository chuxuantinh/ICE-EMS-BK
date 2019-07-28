<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/MasterAdmission.master" AutoEventWireup="true" CodeFile="AutoCadForms.aspx.cs" Inherits="Admission_AutoCadForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Auto CAD Form Submission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Auto Cad Form Submission" CssClass="redirecttabhome" /></td></tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="UpdPnlMain" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:10px;">&nbsp;</h1><h1>Auto CAD Forms</h1></div>
<center>Serial No:&nbsp;&nbsp;<asp:TextBox ID="txtAppNo" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnView_Click" /></center>
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
</div><div style="padding:5px; color:White; font-size:15px;"><b>M-CAD Forms:</b><br />
<br />
<div id="Div1" style="display: block;">
<div  id="divAutoCad" style="width: 100%; overflow:scroll; height:200px;">
<asp:GridView ID="GridAutoCad" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
        CellSpacing="2" Width="100%" 
        onselectedindexchanged="GridAutoCad_SelectedIndexChanged" 
        onrowdatabound="GridAutoCad_RowDataBound">
        <Columns>
            <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView></div></div></div></div>
<asp:Panel ID="pnlAuto" runat="server" Visible="false">
<br />
<table class="tbl" width="90%">
<tr><td>M-CAD Batch:</td><td><asp:Label ID="lblStBatch" runat="server" Font-Bold="true"></asp:Label></td>
<td align="right">Duration(Months):</td><td align="left"><asp:Label ID="lblMonthDif" runat="server" Font-Bold="true" /></td>
</tr>
<tr><td>Registration No:</td><td><asp:Label ID="lblRegNo" runat="server" /></td>
<td align="right">Registration Date:</td><td align="left"><asp:Label ID="lblRegistrationDAte" runat="server" ></asp:Label></td></tr>
<tr><td>Status:</td><td align="left"><asp:Label ID="lblStatus" runat="server" Font-Bold="true" ForeColor="Brown" /></td></tr>
<tr><td>Fees Type:</td><td><asp:Label ID="lblFeeType" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td>
<td align="right">Current Date:</td><td align="left"><asp:TextBox ID="txtDAte" runat="server" CssClass="txtbox" /><dev:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender3" PopupButtonID="Img3" PopupPosition="BottomRight" runat="server" TargetControlID="txtDAte" />&nbsp;<img src="../images/cal.png" id="Img3" runat="server"  alt="Cal" /></td>
</tr>
</table>
<center>&nbsp;&nbsp;
<asp:Label ID="lblException" ForeColor="Brown" runat="server" Font-Bold="true" />
<table class="tbl">
<tr><td>Name:</td><td><asp:TextBox ID="txtName" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true" /></td>
<td>DOB:</td><td><asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox"></asp:TextBox></td></tr>
<tr><td>E-mail:</td><td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" Width="200px" /></td>
<td>Mobile:</td><td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtbox" /></td>
</tr>
</table>
<br />
<table class="tbl" width="90%"><tr><td>
                  <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">New Registration </font></legend>
               <center>   Batch No:&nbsp;&nbsp;<asp:DropDownList ID="ddlBatchNo" runat="server" CssClass="txtbox" Width="50px"></asp:DropDownList>
                  <br /><br />
                  <asp:Button ID="btnReg" runat="server" OnClientClick="return confirm('Are you sure submit New Registration form ?');" CssClass="btnsmall" Text="New Registration" onclick="btnReg_Click" />
                <br />  </center>
                 <br /> </fieldset>
</td><td>
                  <fieldset><legend><font style="color:#B21235; font-size:18px; font-family:Verdana;">Form Submission</font></legend>
              <center><p>Submit Re-Registration and LateFee M-CAD Forms</p>
                 <asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" Text="Submit" OnClientClick="return confirm('Are you sure submit form ?');" onclick="btnSubmit_Click" />
                 <br /></center>   
                  </fieldset>
</td></tr></table>
&nbsp;&nbsp;&nbsp;&nbsp;
</center>
</asp:Panel>
<asp:Panel ID="pnlspc" runat="server" Height="310px"/>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>