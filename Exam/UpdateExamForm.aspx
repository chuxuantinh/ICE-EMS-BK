<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="UpdateExamForm.aspx.cs" Inherits="Exam_UpdateExamForm" %>
<%@ PreviousPageType VirtualPath="~/Exam/ExamForm.aspx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Update Exam Form
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">input:focus,textarea:focus,select:focus { outline:1px solid red;}</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <script type="text/javascript">
      var isCtrl = false;
      document.onkeyup = function (e) {
          if (e.which == 17) isCtrl = false;
      }
      document.onkeydown = function (e) {
          if (e.which == 17) isCtrl = true;
          if (e.which == 83 && isCtrl == true) {
              document.getElementById('<%= btnSave.ClientID %>').click();
              return false;
          }
          if (e.which == 65 && isCtrl == true) {
              document.getElementById('<%= btnselectfour.ClientID %>').click();
              return false;
          }
      }
  </script>

  <script type="text/javascript" src="../jquery.min.js">
</script>

    <asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td>
    <asp:LinkButton ID="Examform" runat="server" Text="Return Exam Form" 
        CssClass="redirecttab" onclick="Examform_Click"
         ></asp:LinkButton></td>&nbsp;</tr></table>
            </div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updatePanel1" runat="server" >
    <Triggers><asp:PostBackTrigger ControlID="GridCivil" />
    <asp:PostBackTrigger ControlID="GridArchi" />
<asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
    </Triggers>
<ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Examination Form&nbsp;&nbsp;&nbsp;Exam Schedule:&nbsp;&nbsp;<asp:DropDownList ID="ddlExamSchedule" runat="server" CssClass="txtbox"><asp:ListItem Value="Home" Text="Home" /><asp:ListItem Value="Overseas" Text="Overseas" /></asp:DropDownList></h1></div>
<center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox"></asp:TextBox></td></tr></table></center>
<br /><center>Application  No or Membership:&nbsp;&nbsp;<asp:TextBox ID="txticesn" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtSerialNo_TextChanged" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<b>
<br /> <asp:Label ID="lblExceptionOK" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
<asp:Label ID="lblCourseID" runat="server" Visible="false" /><asp:Label ID="txtSerialNo" runat="server" 
/>&nbsp;<asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label><asp:Label ID="lblCourseStatus" runat="server" Visible="false"></asp:Label><asp:Label ID="lblexamstatus" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblException" runat="server" ></asp:Label><br /><asp:Label ID="lblException2" runat="server" ></asp:Label></center>
<asp:Panel ID="panelVisible" runat="server" >
<table class="tbl"><tr><td>Membership No.:</td><td><asp:Label ID="lblEnrolNo" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Name:</td><td>
    <asp:Label ID="lblName" runat="server" Font-Bold="True" 
        ForeColor="Maroon" Font-Size="Small"></asp:Label></td></tr>
<tr><td>s/o,d/o,w/o:</td><td><asp:Label ID="lblFName" runat="server" ></asp:Label></td></tr>

<tr><td>Course:</td><td><asp:Label ID="lblStream" runat="server" ></asp:Label></td><td>
    <b>&nbsp;<asp:Label ID="lblCourse" runat="server"></asp:Label>
    &nbsp;&nbsp;<asp:Label ID="lblPartName" runat="server"></asp:Label>
    </b></td></tr>
<tr><td> Date of Birth:&nbsp;&nbsp;</td><td><asp:Label ID="txtDOB" runat="server"></asp:Label> </td><td>Admission Type:&nbsp;<asp:Label ID="lblAdmissionType" runat="server" ></asp:Label>&nbsp;&nbsp;[<asp:Label ID="lblAdmissionSession" runat="server" ></asp:Label>]</td></tr>
<tr><td>Institutional Member:</td><td colspan="2">[<asp:Label ID="lblIMID" runat="server" Font-Bold="true" ></asp:Label>]&nbsp;<asp:Label ID="lblIMName" runat="server" ></asp:Label>,&nbsp;<asp:Label runat="server" ID="lblIMCity"></asp:Label></td></tr>
<tr><td>ExamCenter City-1:</td><td>
<asp:DropDownList ID="ddlExamCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedInexChanged"  CssClass="txtbox"></asp:DropDownList>
&nbsp;&nbsp;<asp:Label runat="server" ID="txtExamID" ></asp:Label></td><td>ExamCenter City-2:&nbsp;&nbsp;
<asp:DropDownList ID="ddlExamCity2" runat="server" CssClass="txtbox" ></asp:DropDownList>
</td></tr>
</table>
<asp:Label ID="lblpart" runat="server" Visible="false"></asp:Label><asp:Label ID="maxsn" runat="server" Visible="false"></asp:Label><asp:Label ID="lblHiddendStream" runat="server" Visible="false" ></asp:Label>
<asp:Panel ID="panelCivilGrid" runat="server" >
<asp:GridView ID="GridCivil" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" HorizontalAlign="Center" 
        GridLines="Vertical" Width="100%">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
            <asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
            <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:Label ID="lblAppSub" runat="server" Text="Select Subjects" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                     <asp:CheckBox ID="chkAppSubject" runat="server" AutoPostBack="true" OnCheckedChanged="chkAppSubjectC_CheckChanged"  />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />
                <asp:BoundField DataField="SubjectType" SortExpression="SubjectType" HeaderText="Type" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <EmptyDataTemplate>
          <center>  No Subject found !!!</center>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView><br />
</asp:Panel>
<asp:Panel ID="panelArchiGrid" runat="server" >
<asp:GridView ID="GridArchi" runat="server" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  ForeColor="Black" 
        GridLines="Vertical" Width="100%">
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
            <asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
            <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:Label ID="lblAppSubA" runat="server" Text="Appearing Subjects" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox ID="chkAppSubjectA"  runat="server" AutoPostBack="true" OnCheckedChanged="chkChkAppSubjectA_CheckChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="SubName" HeaderText="Subject Name"  SortExpression="SubName" />
                <asp:BoundField DataField="SubjectType" SortExpression="SubjectType" HeaderText="Type" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <EmptyDataTemplate>
         <center>   No Subject found !!!</center>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
</asp:GridView><br /><center></center>
</asp:Panel><center>
 <table class="tbl">
<tr>
<td><asp:Button ID="btnselectfour" runat="server" OnClick="btnselectFour_Click" 
        Height="0px" Width="0px" /></td><td>
        <asp:Button ID="btnSave" runat="server" CssClass="btnsmall" 
            OnClick="btnSaveExamForm_Click" Text="Update ExamForm" />&nbsp;&nbsp;</td>
<td><asp:Label ID="lblSpecialSiD" runat="server"></asp:Label>
<blink><asp:Label ID="lblFianlPass" runat="server" Font-Bold="true"></asp:Label></blink>&nbsp;<asp:Label ID="lblReg" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblEx" runat="server"></asp:Label>
</td>
</tr>
</table></center>
<asp:Panel ID="pnlSubList" runat="server" >
    </asp:Panel>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3"  ForeColor="Black" 
        GridLines="Vertical" Width="100%">
      <Columns>
            <asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
           <%-- <asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />--%>
            <asp:BoundField DataField="GetMarks" HeaderText="Marks" SortExpression="GetMarks" />
                 <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:Label ID="lblAppSubA" runat="server" Text="Select" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox ID="chkImprove"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="ExamSeason" SortExpression="ExamSeason" HeaderText="Session" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="true" Visible="false" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3"  ForeColor="Black" 
        GridLines="Vertical" Width="100%">
      <Columns>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <asp:Label ID="lblCourseHidden" runat="server" Visible="false" ></asp:Label>
<asp:Label ID="lblPartHidden" runat="server" Visible="false"></asp:Label>
</asp:Panel><asp:Panel ID="panelInVisible" runat="server" Height="500px"></asp:Panel>
    </b>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>

</div></asp:Content>