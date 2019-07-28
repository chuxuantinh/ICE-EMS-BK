<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="BookletPRange.aspx.cs" Inherits="Exam_Exam2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Booklet Paper Range
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="scriptmangaer11" runat="server" />
<div id="redirect" runat="server">	
<table>
<tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab" onclick="lbtnNext1Redirect_Click" /></td><td><asp:Label ID="lblPageName" runat="server" Text="Booklet Paper Range" CssClass="redirecttabhome" /></td></tr>
</table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Booklet Paper Range(Exam Center):- &nbsp;&nbsp;Type:&nbsp;<asp:DropDownList ID="ddlType" runat="server" CssClass="txtbox"><asp:ListItem Value="Home"/><asp:ListItem Value="Overseas"/></asp:DropDownList>&nbsp;&nbsp;&nbsp;Level:<asp:DropDownList ID="ddlSyllabus" runat="server" CssClass="txtbox"><asp:ListItem Value="081" Text="081" /></asp:DropDownList></h1></div>
<center><table>
<tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px" /></td><td></td></tr></table></center><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false" /><br />
<center><table><tr><td>Center Code:</td><td><asp:TextBox ID="txtCenter" runat="server" CssClass="txtbox" Font-Bold="true" ontextchanged="txtCenter_TextChanged" AutoPostBack="true" Width="90px" /><dev:FilteredTextBoxExtender ID="FCenter" runat="server" TargetControlID="txtCenter" FilterType="Numbers" />&nbsp;&nbsp;</td><td><asp:Label ID="lblCenterName" runat="server" Font-Bold="true" ForeColor="Brown" /></td>
<td><asp:Button ID="btnUpdateqty" runat="server" Text="Update Data" OnClick="btnUpdate_clic" CssClass="btnsmall" /></td>
</tr>
<tr><td>Examination Date:</td><td><asp:DropDownList ID="ddlExaminationdate" runat="server" CssClass="txtbox" AutoPostBack="True" Width="120px"   dataTextFormatString="{0:dd/MM/yyyy}" DataSourceID="SqlDataSource1" DataTextField="Date" DataValueField="Date" ></asp:DropDownList>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT DISTINCT [Date] FROM [ExamDate] WHERE ([Season] = @Season) AND ([Type] = @Type) ORDER BY [Date]">
<SelectParameters>
<asp:ControlParameter ControlID="lblExamSeasonHidden" Name="Season" PropertyName="Text" Type="String" />
<asp:ControlParameter ControlID="ddlType" Name="Type" PropertyName="SelectedValue" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
 </td><td>Shift:&nbsp;&nbsp;<asp:DropDownList ID="ddlShift" runat="server" CssClass="txtbox" ><asp:ListItem Value="FN" Text="FN" /><asp:ListItem Value="AN" Text="AN" /></asp:DropDownList></td><td><asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btnsmall" /></td></tr></table>
<br />
<asp:Label ID="lblException" runat="server" ForeColor="Brown" /><br />
<asp:GridView ID="GridBookletPRange" runat="server" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        Width="100%" AutoGenerateColumns="true"
        onselectedindexchanged="GridBookletPRange_SelectedIndexChanged" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Select"/>
            </Columns>
             <RowStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" />
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle ForeColor="#003399" HorizontalAlign="Left" BackColor="#99CCCC" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
        </asp:GridView>
<table><tr><td>
<table class="tbl">
<tr><td>Paper code:</td><td><asp:Label ID="lblPaperCode" runat="server"></asp:Label></td></tr>
<tr><td>Required Qty.:</td><td><asp:Label ID="lblReqQty" runat="server"></asp:Label></td><td>Supplied Qty.:</td><td><asp:Label ID="lblSupplyQty" runat="server"></asp:Label></td></tr>
<tr><td>Start Range:</td><td><asp:TextBox ID="txtStart" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtStartRange_TextChanged"  /><dev:FilteredTextBoxExtender ID="FStart" runat="server" TargetControlID="txtStart" FilterType="Numbers" /></td><td>End Range:</td><td><asp:TextBox ID="txtEnd" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtEndRange_TextChanged" /><dev:FilteredTextBoxExtender ID="FEnd" runat="server" TargetControlID="txtEnd" FilterType="Numbers" /></td></tr>
<tr><td>Total Booklet:</td><td><asp:TextBox ID="txtBooklet" runat="server" CssClass="txtbox" Width="50px"></asp:TextBox> <dev:FilteredTextBoxExtender ID="TxtBoxFilter" runat="server" FilterType="Numbers" TargetControlID="txtBooklet" ViewStateMode="Enabled"></dev:FilteredTextBoxExtender></td></tr>
</table>
</td><td>
<asp:GridView ID="gridRange" runat="server"  BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="true" OnRowDataBound="gridRangeRowBound_Click">
</asp:GridView><asp:Label ID="lblSt1" runat="server" Visible="false" /><asp:Label ID="lblSt2" Visible="false" runat="server" /><asp:Label ID="lblRef" Visible="false" runat="server" /><asp:Label ID="lblSN" Visible="false" runat="server" /><asp:Label ID="lblPQty" Visible="false" runat="server" />
</td></tr></table><br />
<asp:Label ID="Label1" runat="server" ForeColor="Brown" />
<asp:Label ID="lblNo" runat="server" Visible="false" /><br />
<asp:Button ID="btnAdd" runat="server" CssClass="btnsmall" Text="Add Booklet Paper Range" onclick="btnAdd_Click" />&nbsp;
<asp:Button ID="btnDelete" runat="server" CssClass="btnsmall" Text="Delete Range" OnClick="btnDelte_click" />&nbsp;&nbsp;&nbsp;
&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsmall" Text="Cancel" onclick="btnCancel_Click" Visible="false" />
<br /><br />
</center>
<script>
    function toggleA1x(showHideDiv, switchImgTag) {
        var ele = document.getElementById(showHideDiv);
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
<div class="togalfees" style="width:100%"><div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1');"><img src="../images/minus.png" alt="Show"/></a></div>
<div style="padding:5px; color:White; font-size:15px;"><b>Booklet Paper Range</b></div><br />
<div id="Div1" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div  id="divdatagrid1" style="width: 100%; overflow:scroll; height:300px">
 <asp:GridView ID="GridPRange" runat="server" BackColor="White"
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        Width="100%" AutoGenerateColumns="false" DataKeyNames="No" 
        onselectedindexchanged="GridPRange_SelectedIndexChanged" >
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Edit" InsertImageUrl="~/images/delete.png" />
             <asp:TemplateField HeaderText="SN">
            <ItemTemplate ><asp:Label ID="lblSN" runat="server" Text='<%# Eval("No") %>' /></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Paper Code">
            <ItemTemplate><asp:Label ID="lblSubID" runat="server" Text='<%# Eval("SubID") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Center Code">
            <ItemTemplate><asp:Label ID="lblCenterCode" runat="server" Text='<%# Eval("CenterCode") %>' /></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Start Range">
            <ItemTemplate><asp:Label ID="lblStartRange" runat="server" Text='<%# Eval("StartRange") %>' /></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="End Range">
            <ItemTemplate><asp:Label ID="lblEndRange" runat="server" Text='<%# Eval("EndRange") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
            <ItemTemplate><asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref" Visible="false">
            <ItemTemplate><asp:Label ID="lblPRef" runat="server" Text='<%# Eval("Ref") %>' /></ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" />
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle ForeColor="#003399" HorizontalAlign="Left" BackColor="#99CCCC" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
</asp:GridView>
</div></div></div>
</div>
</asp:Content>