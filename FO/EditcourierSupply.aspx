<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAccount.master" AutoEventWireup="true" CodeFile="EditcourierSupply.aspx.cs" Inherits="FO_EditcourierSupply" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Edit Courier Supply
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="Scriptmanger1" runat="server" ></asp:ScriptManager>
<div id="redirect"><table><tr><td><asp:LinkButton ID="lblHomeRedirect" 
        runat="server" onclick="ibtnHome_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
         </td><td><asp:Label ID="lblCourierHome" runat="server" Text="Update Diary Supply" CssClass="redirecttabhome"></asp:Label></td></tr></table>
             </div>
              <div id="rightpanel2"><div id="header">
                           <asp:UpdatePanel ID="updatediray" runat="server" ><ContentTemplate>
                           <div id="Div1" class="fromRegisterlbl" runat="server" ><h1 style="float:right; margin-right:30px;">Edit Count:&nbsp;<asp:Label ID="lblCount" runat="server" ></asp:Label></h1><h1>Edit Diary Count</h1></div>
                           <br /><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label>
                          <asp:Panel ID="pnlMain" runat="server" >
                          <table width="98%">
<tr><td>
<table class="tbl"><tr><td>Select Session:</td><td>
    <asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" 
        CssClass="txtbox" OnTextChanged="ddlExamSeason_SelectedIndexChanged">
        <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
        <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
    </asp:DropDownList>
    </td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
                           <tr><td>Diary No.:</td><td><asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChanged"></asp:TextBox></td></tr>
                           <tr><td><asp:Label ID="lblRcv" Text="Receiving Date:" runat="server" Visible="false" /></td><td><asp:Label ID="lblSubDate" runat="server" ></asp:Label></td></tr>
</table>
                         <asp:Panel ID="panelIM" runat="server" Width="100%" CssClass="imbox" Visible="false" >
<asp:Label ID="lblIMID" runat="server" ForeColor="Blue" Font-Size="15px"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px" ></asp:Label><br />
<asp:Label ID="lblIMAddress" runat="server" ></asp:Label><br />
<asp:Label ID="lblIMCity" runat="server" ></asp:Label><br />
<br />
</asp:Panel>
    <asp:Label ID="lblRemarks" Text="Remark:" runat="server" Font-Bold="true" Visible="false"/>
    &nbsp;<asp:Label ID="lblRemark" runat="server" ForeColor="GrayText"></asp:Label>
    <br /> <asp:Label ID="lblForm" Text="FormType" runat="server" Font-Bold="true" Visible="true"/>
    &nbsp;<asp:Label ID="lblFormType" runat="server" ForeColor="GrayText"></asp:Label>
    <br />
    
 </td>
 <td>
<input id="scrollPos4" runat="server" type="hidden" value="0" />
                 <div id="div2" style="width: 100%; overflow:scroll; height:150px">
<asp:GridView ID="GridDiaryNo" runat="server"
            HorizontalAlign="Center" 
            onselectedindexchanged="GridDiaryNo_SelectedIndexChanged" Width="200px" PageSize="100"
            CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="Select" HeaderText="Select" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div>
</td></tr>
</table>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txt" runat="server" BackColor="Cornsilk" 
        CssClass="txtbox" Width="25px" ReadOnly="True"></asp:TextBox>
    &nbsp;Received Forms/DD&nbsp;&nbsp;
    <asp:TextBox ID="lbl" runat="server" BackColor="BurlyWood" 
        CssClass="txtbox" Width="25px" ReadOnly="True"></asp:TextBox>
    &nbsp; Submitted Forms/DD
    
<asp:Panel ID="pnlAcademic" runat="server" >
<div class="redsubtitle"><p>Date:&nbsp;&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDate" Display="Dynamic" ValidationGroup="dev" errormessage="Insert Current Date" >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDate"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></p>Academic Form</div>
<table class="tbl" width="100%" ><tr align="center"><td>No of DD<br />
    <asp:TextBox ID="txtADDNo" runat="server" Width="25px" CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="filtercurrentcy" runat="server" FilterType="Numbers" TargetControlID="txtADDNo" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblADDNo" runat="server" Width="25px" CssClass="txtbox" BackColor="BurlyWood"></asp:Textbox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers" TargetControlID="lblADDNo" ></dev:FilteredTextBoxExtender></td>
<td>Enroll Form<br /> 
    <asp:TextBox ID="txtEnroll" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtEnroll" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblEnroll" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers" TargetControlID="lblEnroll" ></dev:FilteredTextBoxExtender>
    </td>
<td>Exam Form<br /> 
    <asp:TextBox ID="txtExam" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtExam" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblExam" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers" TargetControlID="lblExam" ></dev:FilteredTextBoxExtender>
    </td>
<td>ITI<br /> <asp:TextBox ID="txtITI" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>
    &nbsp;<asp:Textbox ID="lblITI" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtITI" ></dev:FilteredTextBoxExtender>
     <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Numbers" TargetControlID="lblITI" ></dev:FilteredTextBoxExtender></td>
<td>Auto-CAD<br /> 
    <asp:TextBox ID="txtCAD" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers" TargetControlID="txtCAD" ></dev:FilteredTextBoxExtender>
        <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Numbers" TargetControlID="lblCAD" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblCAD" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
<td>Others Form<br /> 
    <asp:TextBox ID="txtOtherForm" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox ID="lblOtherForm" 
        runat="server" Width="25px" CssClass="txtbox" BackColor="BurlyWood"></asp:Textbox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtOtherForm" ></dev:FilteredTextBoxExtender>
     <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers" TargetControlID="lblOtherForm" ></dev:FilteredTextBoxExtender></td>
</tr></table>
</asp:Panel>
<asp:Panel ID="pnlOther" runat="server" >
<div class="redsubtitle">Other Form</div>
<table class="tbl" width="100%"><tr align="center"><td>No of DD<br />
    <asp:TextBox ID="txtODDNo" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="Cornsilk"></asp:TextBox>
    &nbsp;<asp:Textbox ID="lblODDNo" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
&nbsp;<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtODDNo" ></dev:FilteredTextBoxExtender>
<dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Numbers" TargetControlID="lblODDNo" ></dev:FilteredTextBoxExtender></td>
<td>Provisional<br /> 
    <asp:TextBox ID="txtProvisional" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtProvisional" ></dev:FilteredTextBoxExtender>
        <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Numbers" TargetControlID="lblProvisional" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblProvisional" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
<td>Final Pass<br /> 
    <asp:TextBox ID="txtFinalPass" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox 
        ID="lblFinalPass" Width="25px" CssClass="txtbox"
        runat="server" BackColor="BurlyWood"></asp:Textbox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="txtFinalPass" ></dev:FilteredTextBoxExtender>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterType="Numbers" TargetControlID="lblFinalPass" ></dev:FilteredTextBoxExtender></td>
<td>Re-Checking<br /> 
    <asp:TextBox ID="txtReChecking" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox ID="lblRechecking" 
        runat="server" Width="25px" CssClass="txtbox" BackColor="BurlyWood"></asp:Textbox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="txtReChecking" ></dev:FilteredTextBoxExtender>
      <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers" TargetControlID="lblReChecking" ></dev:FilteredTextBoxExtender></td>
<td>Duplicate Docs<br /> 
    <asp:TextBox ID="txtDuplicate" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox ID="lblDuplicate" 
        runat="server" Width="25px" CssClass="txtbox" BackColor="BurlyWood"></asp:Textbox>
    <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers" TargetControlID="txtDuplicate" ></dev:FilteredTextBoxExtender>
        <dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers" TargetControlID="lblDuplicate" ></dev:FilteredTextBoxExtender></td>
</tr></table>
</asp:Panel>
<asp:Panel ID="pnlAdvance" runat="server">
<div class="redsubtitle">Advance DD</div>
<table class="tbl" width="100%"><tr align="center"><td>Membership DD<br />
    <asp:TextBox ID="txtMembershipDD" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="Cornsilk"></asp:TextBox>
    &nbsp;<asp:Textbox ID="lblMembership" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
<td>Books DD<br /> 
    <asp:TextBox ID="txtBooksDD" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox ID="lblBooks" 
        runat="server" Width="25px" CssClass="txtbox" BackColor="BurlyWood"></asp:Textbox>
    </td>
<td>Prospectus DD <br />
    <asp:TextBox ID="txtPrespectusDD" runat="server" 
        CssClass="txtbox" Width="25px" BackColor="Cornsilk"></asp:TextBox>&nbsp;<asp:Textbox 
        ID="lblProspectus" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
</tr></table>
</asp:Panel>
<center>
<asp:Panel ID="pnlToFrom" runat="server" >
    <table width="100%" class="tbl"><tr><td>Letter/Diary For:&nbsp;(Name and Address)<br /><asp:TextBox ID="txtLtrTo" TextMode="MultiLine" Height="30px" runat="server" Width="250px" CssClass="txtbox" /></td>
    <td>Letter/Diary From:&nbsp;(Name and Address)<br /><asp:TextBox ID="txtFrom" runat="server" Width="250px" TextMode="MultiLine" Height="30px" CssClass="txtbox" /></td></tr></table>
    </asp:Panel>
   <asp:Panel ID="pnlDepartment" runat="server" Visible="false" >
    <table id="tblEmpNameAcademic"  runat="server" visible="false"><tr><td>
<asp:Label ID="lblDepartmneName" runat="server" Text="Department Name"></asp:Label>&nbsp;&nbsp;
<asp:DropDownList ID="ddlDeparmentNeme" runat="server" CssClass="txtbox"
            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
            Width="200px"  AutoPostBack="true"
            onselectedindexchanged="ddlDeparmentNeme_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Department') ORDER BY Name">
        </asp:SqlDataSource></td><td>
        <asp:LinkButton ID="btnNewDepartment" runat="server" Font-Bold="true" ForeColor="Blue" Text=" Add New >>>" OnClick="ibtnNewDepartment_Onclick"></asp:LinkButton></td></tr>
        <tr><td>
       <asp:Label ID="lblEmplabel" runat="server" Text="Employee Name"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpName" runat="server" Width="200px" CssClass="txtbox" ></asp:TextBox></td><td>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpCode" Visible="false"  Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>
        </td></tr></table>
      <br /> </asp:Panel>
</center>
<asp:Panel ID="pnlProject" runat="server" >
<div class="redsubtitle">Project Form</div>
<table class="tbl" width="100%"><tr align="center"><td>No of DD<br />
    <asp:TextBox ID="txtPDD" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txtPDD" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblProj" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
    <td>Proforma A<br /> 
    <asp:TextBox ID="txtProformaC" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers" TargetControlID="txtProformaC" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblProA" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
<td>Proforma B<br /> 
    <asp:TextBox ID="txtProformaB" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers" TargetControlID="txtProformaB" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblProB" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
    <td>Proforma C<br /> 
    <asp:TextBox ID="txtProformaA" runat="server" Width="25px" 
        CssClass="txtbox" BackColor="Cornsilk"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="txtProformaA" ></dev:FilteredTextBoxExtender>
    &nbsp;<asp:Textbox ID="lblProC" runat="server" Width="25px" CssClass="txtbox" 
        BackColor="BurlyWood"></asp:Textbox>
    </td>
</tr></table>
<center runat="server" id="pnlPsubmit">
<asp:Label ID="lblExceptionProject" runat="server" Font-Bold="true"></asp:Label><br />
<asp:DropDownList ID="ddlProjectDepartment" runat="server"  Visible="false"
            DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Name" 
            Width="200px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Department') ORDER BY Name">
        </asp:SqlDataSource>&nbsp;&nbsp;&nbsp;
        <center><h3 class="hl3"><asp:Label ID="lblExceptionDiary" runat="server"></asp:Label></h3><br /></center>
   &nbsp;<asp:TextBox ID="txtPEmpName" runat="server" Visible="false" Width="200px" CssClass="txtbox" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPEmpCode" Width="100px" runat="server" Visible="false" CssClass="txtbox"></asp:TextBox>
       <br /> <br /></center>
</asp:Panel>
    <center><asp:Panel ID="panelCourier" runat="server" CssClass="expbox">
    <center>
    Name of Departmnet:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox" Width="200px" Font-Bold="true"></asp:TextBox>
<br /><asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br /><br /><asp:Button ID="btnSaveNewCourier" runat="server" Text="Save" OnClick="btnSAveNew_Onclick" />    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel"  runat="server" Text="Close" OnClick="btnCencelnew_Onclick" />
    </center>
    </asp:Panel></center>    
    <br /><center>
    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnsmall" onclick="btnUpdate_Click" /></center>
    </asp:Panel>
    <asp:Panel ID="pnlSpace" runat="server" Height="250px"></asp:Panel>
</ContentTemplate></asp:UpdatePanel><center>
                           </center><br />
</div></div>
</asp:Content>

