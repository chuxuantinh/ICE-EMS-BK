<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true" CodeFile="ApproveRechecking.aspx.cs" Inherits="Acc_ApproveRechecking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Rechecking Application Forms
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
 <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">Current Batch No.:&nbsp;<asp:Label ID="lblCurrentBatch" runat="server"></asp:Label>&nbsp;&nbsp;Fee Master:&nbsp;<asp:DropDownList 
        ID="ddlFeeMaster" runat="server" CssClass="txtbox" Width="85px" 
        AutoPostBack="True" Height="16px"><asp:ListItem Value="Home" Text="Home" /><asp:ListItem Value="Overseas" Text="Overseas" /></asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Application Forms Entry:</h1></div>
        <center>Session:&nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" 
         AutoPostBack="true" CssClass="txtbox" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; 
            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" 
                Width="80px"  ></asp:TextBox></center>
<table class="tbl"><tr><td>Diary No:&nbsp;&nbsp;</td>
<td>
    <asp:TextBox ID="txtDiaryNo" Width="100px" runat="server" CssClass="txtbox" 
        AutoPostBack="true" ontextchanged="txtDiaryNo_TextChanged" ></asp:TextBox></td></tr>
<tr><td>IMID:&nbsp;&nbsp;</td><td><asp:Label ID="lblIMID"  runat="server"></asp:Label></td>
<td><asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>&nbsp;&nbsp;</td></tr>
<tr><td>Diary Date:</td><td><asp:Label ID="txtDiaryRcvDate" runat="server" Font-Bold="true"></asp:Label>
        &nbsp;&nbsp;&nbsp;Session:</td><td>
        <asp:Label ID="lblSessionHiddend" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
    </td></tr>
    <tr><td>ReChecking Submitted:&nbsp;<asp:Label ID="lblRecheckingSub" runat="server" ForeColor="black" Font-Bold="true"  ></asp:Label></td><td>&nbsp;&nbsp;&nbsp;&nbsp;ReChecking Received:&nbsp;<asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="black" Font-Bold="true" ></asp:Label></td></tr>
</table>
<center><asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label></center>
<asp:Panel ID="pansession" runat="server" CssClass="panelCenter" >
<br />
<table ID="tblAdd2" runat="server" width="90%">
<tr><td><b>Select Exam Session and Enter Membership ID:</b></td></tr>
<tr><td align="center">
Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlsession" runat="server" 
         CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum">
         </asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win">
         </asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; 
         <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="80px" ></asp:TextBox></td></tr>
     <tr>  <td align="center">
        Membership No.<asp:TextBox ID="txtMem" runat="server" CssClass="txtbox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnView" runat="server" CssClass="btnsmall" 
            onclick="btnView_Click" Text="View" />
        <asp:Label ID="lblFormType" runat="server" Font-Bold="True" Visible="false"></asp:Label>
   
  </td></tr></table>
  </asp:Panel>
 <center>
  <asp:Panel ID="PnlMembership" runat="server" CssClass="panelCenter" >
<table class="tbl" runat="server" id="tblDetails" width="100%"><tr>
    <td ><b>Student Name:</b><asp:Label ID="lblName" runat="server" 
            ForeColor="Maroon"></asp:Label>
    </td>
    <td><b>Father's Name:</b><asp:Label ID="lblFName" runat="server"></asp:Label>
    </td></tr>
<tr><td><b>Date Of Birth:</b><asp:Label ID="lblDOB" runat="server"></asp:Label>
    <asp:Label ID="lblLvl" runat="server" Visible="false"></asp:Label>    </td>
    <td ><b>Course:</b><asp:Label ID="lblCourse" runat="server" 
            ForeColor="Maroon"></asp:Label>
        <asp:Label ID="lblPart" runat="server" ForeColor="Maroon"></asp:Label>
        &nbsp;
        <asp:Label ID="lblStream" runat="server" ForeColor="Maroon"></asp:Label>
    </td></tr>
        <tr><td colspan="2" align="center"> <asp:Label ID="lblRechecking" runat="server" 
           Font-Bold="True"  ForeColor="Green" Text="Serial No:-"></asp:Label>
     <asp:Label ID="lblCADSerialNo" runat="server" Font-Bold="True"  ForeColor="Green"></asp:Label>  <asp:Label ID="lblAmount" runat="server" ForeColor="Green" Visible="false"></asp:Label></b><asp:Label ID="lblappno" runat="server" Font-Bold="True" ForeColor="Green" Visible="false"></asp:Label><asp:Label ID="lblStatus" runat="server" Forecolor="Brown"></asp:Label></td></tr></table>

</asp:Panel>

</center><br /><asp:Panel ID="pnlMain" runat="server">
<br />
    </asp:Panel> 
    
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
        &nbsp;&nbsp;&nbsp;&nbsp;<a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div>
<br /><br />
<div id="Div12" style="display:block;">

 <input id="scrollPos2" runat="server" type="hidden" value="0" />
 <div id="divdatagrid2" style="width: 100%; overflow:scroll; height:165px">

 <asp:GridView ID="GridToBeApprove" runat="server" BackColor="#DEBA84" 
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%"  >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns><asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
        <asp:TemplateField ><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
</asp:GridView></div>
<center><asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" 
        onclick="btnSubmit_Click" Text="Submit" /></center>

</div>

<div id="Div1" style="display:block;">
 <input id="Hidden1" runat="server" type="hidden" value="0" /><div id="div2" style="width: 100%; overflow:scroll; height:200px"><asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true" OnRowDataBound="GridAppTable_RowDataBound"
        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="5" 
        CellSpacing="5" Width="100%">
        <Columns>
        </Columns>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    </asp:GridView></div></div>
    <asp:Panel ID="pnlSpace" runat="server" Height="300px"></asp:Panel>
</div>
</div>
</asp:Content>

