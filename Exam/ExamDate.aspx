<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ExamDate.aspx.cs" Inherits="Exam_ExamDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Examination Schedule
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Marks Entry" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label>

<div class="fromRegisterlbl"><h1>Examination Marks Entry Via Examination Center :-</h1></div><br />
<center>Session:&nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnGoTo" runat="server" OnClick="lbtnGOTO_Onclick" Font-Bold="true" ForeColor="Maroon"></asp:LinkButton>
<br /><asp:Label ID="lblExceptionExamCenterse" runat="server" Visible="false"></asp:Label></center>
<hr /><asp:UpdatePanel ID="updatePanelRechecking" runat="server"><ContentTemplate>
<asp:Panel ID="panelREchecking" runat="server" >
<table class="tbl" width="99%"><tr><td><asp:RadioButton ID="rbtnRollNo" 
        runat="server" Text="Roll No" GroupName="path" 
        oncheckedchanged="rbtnRollNo_CheckedChanged" AutoPostBack="true" /></td>
        <td><asp:RadioButton ID="rbtnMembershipNo" 
        runat="server" Text="Membership No" GroupName="path" 
        oncheckedchanged="rbtnMembershipNo_CheckedChanged" AutoPostBack="true" /></td><td>
        <asp:RadioButton ID="rbtnCenterCode" runat="server" Text="Center Code" 
            GroupName="path" oncheckedchanged="rbtnCenterCode_CheckedChanged" AutoPostBack="true" /></td><td>
        <asp:RadioButton ID="rbtnIMID" runat="server" Text="IM ID" GroupName="path" 
            oncheckedchanged="rbtnIMID_CheckedChanged" AutoPostBack="true" /></td><td>
        <asp:RadioButton ID="rbtnAll" runat="server" Text="All" GroupName="path" 
            oncheckedchanged="rbtnAll_CheckedChanged" AutoPostBack="true" /></td></tr></table>
<center><asp:Label ID="lblSearchLabel" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtSearchBox" runat="server" Width="250px" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" Text=" View " OnClick="btnOK_Click" /></center>

<br /><asp:Panel ID="panelInnerRechecking" runat="server" CssClass="confirmationBox">
<br />
<table class="tbl" width="80%"><tr><td>Roll No:&nbsp;&nbsp;<asp:Label ID="lblRollNoRecheckingj" runat="server" Font-Bold="true"></asp:Label></td><td>Membership No:&nbsp;<asp:Label ID="lblMembershipNoRechecking" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Subject Code:&nbsp;<asp:Label ID="lblSubjectNoRechecking" runat="server" Font-Bold="true"></asp:Label></td><td>Subject Name:&nbsp;<asp:Label ID="lblSubNameRechecking" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Old Marks:&nbsp;&nbsp;<asp:Label ID="lblOldMarksRechecking" runat="server" Font-Bold="true"></asp:Label></td><td>New Marks:&nbsp;<asp:TextBox ID="txtNewMarks" runat="server" CssClass="txtbox" Width="100px" BorderColor="Maroon" Font-Bold="true"></asp:TextBox></td></tr>
</table>
<center><asp:Label ID="lblExceptionRechecking" runat="server" ></asp:Label><br /><asp:Button ID="btnSaveRechecking" runat="server" Text="Save" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencelRechecking" runat="server" Text="Cancel" OnClick="btnCencel_ONClickRechecking" /></center><br />
</asp:Panel><br />
<script>
    function toggleA1x(showHideDiv, switchImgTag) {
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
<div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Rechecking Subject List</div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 98%; overflow:scroll; height:200px">
<asp:GridView ID="GridRechecking" runat="server" AutoGenerateColumns="true" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" OnRowDataBound="GridView1_OnRowDataBound" 
        GridLines="Vertical" onselectedindexchanged="GridView1_SelectedIndexChanged"  PageSize="50" 
        Width="100%" onpageindexchanging="GridUFM_PageIndexChanging">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <EmptyDataTemplate><center>No Rechecking  Case Found !!!</center></EmptyDataTemplate>
     <PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" Position="Bottom" FirstPageText="First" NextPageText="Next" LastPageText="Last"  /><PagerStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Bottom" /> 
        <Columns> <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    
   </div>
   </div></div>
</asp:Panel></ContentTemplate>
</asp:UpdatePanel>
<asp:Panel ID="panelMarks" runat="server" >



<script>
    function toggleA1y(showHideDiv, switchImgTag) {
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
<div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1y" href="javascript:toggleA1y('Div1y', 'A1y');"><img src="../images/plus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select Examination Center:</div>
<div id="Div1y" style="display:none;"><br />
  <input id="scrollPos2" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid2" style="width: 100%; overflow:scroll; height:200px" 
            onscroll='javascript:setScroll(this, <% =scrollPos2.ClientID %> );'>
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" AllowPaging="true"
        CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black"  PageSize="50" OnPageIndexChanging="GridView2_OnPageIndexChanging"
        GridLines="Vertical" onselectedindexchanged="GridExamCenter_SelectedIndexChanged" 
        Width="99%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" /> 
        <EmptyDataTemplate><center>No UFM Case Found !!!</center></EmptyDataTemplate>
     <PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" Position="Bottom" FirstPageText="First" NextPageText="Next" LastPageText="Last"  /><PagerStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Bottom" /> 
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="ToSeat" HeaderText="ToSeat" 
                SortExpression="ToSeat" />
            <asp:BoundField DataField="RollNo" HeaderText="RollNo" 
                SortExpression="RollNo" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
        SelectCommand="SELECT [ID], [Name], [City], [State], [Email], [ToSeat], [RollNo] FROM [ExamCenter] WHERE ([Season] = @Season) ORDER BY [ID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
   </div>
   </div></div>
<table class="tbl" style="float:right; margin-right:10px; width:50%;"><tr><td>Exam Center Code:&nbsp;&nbsp;<asp:Label ID="lblCenterCode" runat="server" ></asp:Label></td></tr><tr><td>Exam Center Name:&nbsp;&nbsp;<asp:Label ID="lblCenteNaem" runat="server" ></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblCenter" runat="server" ></asp:Label></td></tr>
<tr><td>Total Capacity:&nbsp;&nbsp;<asp:Label ID="lblCapacity" runat="server" ></asp:Label></td></tr>
</table>
<table class="tbl" width="35%"><tr><td>
Enter Exam Center Code:&nbsp;<br /><asp:TextBox ID="txtExamCode" runat="server" CssClass="txtbox" Width="100px" ></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtExamCode" FilterType="Numbers"></dev:FilteredTextBoxExtender></td></tr>
<tr><td><asp:Button ID="btnOKCenterCode" runat="server" OnClick="btnCenterCode_OnClick" Text="OK" /></td></tr>
</table><center><asp:Label ID="lblExceptionCode" runat="server" ></asp:Label></center><hr />
<table class="tbl" width="95%">
<tr><td></td><td></td><td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblStreamCode" runat="server" Visible="false"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SeelctedIndexchanged" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList></td><td>Part/Section:&nbsp;</td><td><asp:DropDownList ID="ddlPart" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td></tr>

<tr><td>Subject Code:</td><td><asp:DropDownList ID="ddlsubcode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCode_SelectedIndexChanged"></asp:DropDownList></td><td>Subject Name:&nbsp;&nbsp;&nbsp;</td><td><asp:Label ID="lblSubNamess" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Total Marks:</td><td><asp:Label ID="lblToMarks" runat="server" CssClass="txtbox" Font-Bold="true"></asp:Label></td><td>Min. Passing Marks:</td><td><asp:Label ID="lblMinMarsk" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Date :</td><td><asp:TextBox ID="txtDOB" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="txtDOB" Display="Dynamic" ValidationGroup="A" errormessage="Insert Date " >*</asp:RequiredFieldValidator><dev:CalendarExtender Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight" runat="server" TargetControlID="txtDOB"></dev:CalendarExtender> <img src="../images/cal.png" id="cal" runat="server"  alt="Cal" /></td><td>First Div. Marks:&nbsp;</td><td><asp:Label ID="lblFirstMarks" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table>
<center><asp:Button ID="btnShowEnrolment" runat="server" Text="Show Enrolment" OnClick="btnShowEnrolment_Onclick" /></center>
<hr /><asp:UpdatePanel ID="updatepanel1" runat="server" ><ContentTemplate>
<asp:Panel ID="panelView" runat="server" CssClass="confirmationBox">
   <br />
   <center><h3 class="hl3">Unfair Means Case</h3></center>
   <table class="tbl" width="90%">
<tr><td>ExamDate:&nbsp;&nbsp;<asp:Label ID="lblExamDate" runat="server" Font-Bold="true"></asp:Label></td><td>Shift:&nbsp;&nbsp;<asp:Label ID="lblShift" runat="server" Font-Bold="true"></asp:Label></td></tr>



</table>
<br /><center><asp:ImageButton ID="btnunfair" ImageUrl="~/images/unfair.png" runat="server" /><asp:ImageButton ID="btnfair" ImageUrl="~/images/fair.png" runat="server"/></center>

<br />
</asp:Panel>
<table class="tbl" width="80%">
<tr><td>IM ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtIMID" runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtIMID" Display="Dynamic" ValidationGroup="A" errormessage="Insert IMID " >*</asp:RequiredFieldValidator></td><td>Status:&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Font-Bold="true" ></asp:Label></td></tr>
<tr><td>Enrolment No.:&nbsp;&nbsp;<asp:TextBox ID="txtEnrolment" runat="server" CssClass="txtbox" Width="150px"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtEnrolment" Display="Dynamic" ValidationGroup="A" errormessage="Insert Membership No(Enrolment No) " >*</asp:RequiredFieldValidator></td><td>Exam Roll No.:&nbsp;&nbsp;<asp:Label ID="lblRollNoSelected" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label></td></tr>
<tr><td>Obtained Marks:&nbsp;<asp:TextBox ID="txtObtainMarks" runat="server" CssClass="txtbox" Width="50px" BorderColor="Green" BorderWidth="2px" AutoPostBack="true" OnTextChanged="txtOMarks_TextChanged"></asp:TextBox></td><td>Subject Status:&nbsp;&nbsp;<asp:Label ID="lblExStatus" runat="server" Font-Bold="true" ></asp:Label></td></tr>
<tr><td></td></tr>
</table>
<br /><center><asp:ValidationSummary ValidationGroup="A" DisplayMode="BulletList" runat="server" ID="ValidationSummary" CssClass="expbox" ShowSummary="true" /><asp:Label ID="lblException" runat="server" ></asp:Label><br /><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSAVE_Click" ValidationGroup="A" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" /></center>

<script>
    function toggledev(showHideDiv, switchImgTag) {
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
 <a id="Adev" href="javascript:toggledev('Divdev', 'Adev');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Select Roll No:</div>
<div id="Divdev" style="display:block;"><br />
  <input id="scrollPos3" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid3" style="width: 98%; overflow:scroll; height:200px" 
         >
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  ForeColor="Black" 
        GridLines="Vertical" onselectedindexchanged="GridExamForm_SelectedIndexChanged" 
        Width="100%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <EmptyDataTemplate><center>Subject Record Not Found !</center></EmptyDataTemplate>
        <Columns>
        <asp:CommandField ShowSelectButton="True" SelectText="Select" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    
   </div>
   </div></div>

</ContentTemplate></asp:UpdatePanel>
</asp:Panel>
<br /><br />

</div>
</asp:Content>

