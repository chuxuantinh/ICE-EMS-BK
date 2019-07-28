<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="CivilSyllabus.aspx.cs" Inherits="Administrator_Course_CivilSyllabus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Civil Engineering Subject
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../../style.css" rel="stylesheet" type="text/css" />
<link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="leftpanel2">
<div id="leftteg" >
<script>
        function toggle100(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../../images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../../images/minus.png">';
            }
        }
</script>
<div class="togelleft">
<div class="headerDivImg">
<a id="imageDivLink100" href="javascript:toggle('contentDivImg100', 'imageDivLink100');"><img src="../../images/minus.png" alt="Show"/></a>
</div><h1>&nbsp;ICE(India) Courses&nbsp;</h1>
<div id="contentDivImg100" style="display: block;"> <br />
<div id="leftLink">
<ul><li><asp:LinkButton ID="lbtnCivilSubjects" runat="server" Text="Civil Engineering Subjects" CssClass="leftlink" onclick="lbtnCivill_Click"></asp:LinkButton></li>
    <li><asp:LinkButton ID="lbtnArchiSubjects" runat="server" Text="Architectural Engineering Subjects" CssClass="leftlink" onclick="lbtnArchitectural_Click"></asp:LinkButton></li>
    <li><asp:LinkButton ID="btnCreateNowSyllabus" runat="server" Text="Create New Syllabus" CssClass="leftlink" onclick="lbtnCteateNewSyllabus_Click"></asp:LinkButton></li>
    <li><asp:LinkButton ID="btnManageSyllabus" runat="server" Text="Manage[Active/Disactive] Syllabus" CssClass="leftlink" onclick="lbtnManageSyllabus_Click"></asp:LinkButton></li>
</ul>
</div>
</div>
<br /></div></div>
<script>
              function toggle99(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../../images/minus.png">';
                  }
              }
</script>
</div>
<div id="rightpanel2" style="border:none;">
<asp:UpdatePanel ID="updatePanelMaian" runat="server" >
<Triggers><asp:PostBackTrigger ControlID="lbntSelectAging" /></Triggers>
<ContentTemplate>
<div id="header" style="border:1px solid gray;"><div class="fromRegisterlbl"><h1 style="padding-top:0px;"><img src="../../images/forward.png" alt="forward"/> &nbsp;&nbsp;
&nbsp;&nbsp;Civil Engineering:- &nbsp;&nbsp;
&nbsp;&nbsp;[ Subjects ]</h1></div>
</div><br />
<asp:Panel ID="panelSelect" runat="server" CssClass="SubjectSelect">
<asp:Panel ID="panelselectLevel" runat="server" ><br />
<center><asp:Label ID="lblLevelInfoTitle" runat="server" ></asp:Label><br />
<asp:RadioButton ID="rbtnDiploma" runat="server" GroupName="level" Text="Diploma Level Syllabus"  AutoPostBack="true" oncheckedchanged="rbtnDiploma_CheckedChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnDegree" runat="server" GroupName="level" Text="Degree Level Syllabus" AutoPostBack="true" oncheckedchanged="rbtnDegree_CheckedChanged" /></center> 
</asp:Panel>
<asp:Panel ID="panelDiplomaSelect" runat="server" ><br />
<center><asp:Label ID="lblSelectedLevel" runat="server" ></asp:Label><br />
<asp:RadioButton ID="rbtnPart1" runat="server" Text="Part-I" AutoPostBack="true" GroupName="part" oncheckedchanged="rbtnPart1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnPart2" runat="server" Text="Part-II" AutoPostBack="true" GroupName="part" oncheckedchanged="rbtnPart2_CheckedChanged" /></center>
<div style="float:right; margin-right:30px; margin-top:10px; color:Gray; font-size:10px; text-decoration:none;">
<asp:LinkButton runat="server" Text="BACK" ID="LinkButton1" OnClick="lbtnBackDiploma_Click"></asp:LinkButton>
</div>
</asp:Panel>
<asp:Panel ID="panelDegreeSelect" runat="server" ><br />
<center><asp:Label ID="lblSelectedlevelDeg" runat="server" ></asp:Label><br />
<asp:RadioButton ID="rbtnSecA" runat="server" Text="Section A" AutoPostBack="true" GroupName="part" oncheckedchanged="rbtnSecA_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnSecB" runat="server" Text="Section B" AutoPostBack="true" GroupName="part" oncheckedchanged="rbtnSecB_CheckedChanged"/></center>
<div style="float:right; margin-right:30px; margin-top:10px; color:Gray; font-size:10px; text-decoration:none;">
<asp:LinkButton runat="server" Text="BACK" ID="lbtnBackDegree" OnClick="lbtnBackDegree_Click"></asp:LinkButton>
</div>
</asp:Panel>
</asp:Panel><br />
<asp:Panel ID="PanelCheckBox" runat="server" CssClass="PanelcheckBoxSelect"><img src="../../images/boxleft.png" alt="" style="float:left;" /><img src="../../images/boxright.png" alt="" style="float:right;" /><div id="boxmiddle">
<center><asp:LinkButton ID="lbntSelectAging" runat="server" Text="Select Again" onclick="lbntSelectAging_Click"></asp:LinkButton> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkPart1" runat="server" AutoPostBack="true" Text="Part I" oncheckedchanged="chkPart1_CheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkPartII" runat="server" Text="Part II" AutoPostBack="true" oncheckedchanged="chkPartII_CheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSecA" runat="server" AutoPostBack="true" Text="Section A" oncheckedchanged="chkSecA_CheckedChanged" />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSecB" runat="server" AutoPostBack="true" Text="Section B" oncheckedchanged="chkSecB_CheckedChanged" /><br /></center></div></asp:Panel><br />
<asp:Panel ID="PnlSubjectEdit" CssClass="SubEdit" runat="server" >
<img src="../../images/bxleft.png" alt="" style="float:left;" /><img src="../../images/bxright.png" alt="" style="float:right;" /><div id="bxcenter"><div style="float:right;">
<asp:ImageButton ID="ibtnClose" runat="server" ImageUrl="../../images/cls.png" onclick="ibtnClose_Click" /></div>
<br />
<center><h3>Total No. of Subject:-<asp:Label ID="lblSubNoEdit" runat="server" ></asp:Label></h3>
<br />
<h4>Technician Engineering Subjects:-<asp:Label ID="lblSubType" runat="server" ></asp:Label></h4></center>
<br /><asp:Label ID="lblSecType" runat="server" Visible="false"></asp:Label>
<center><asp:Panel ID="panelShowExpEdit" runat="server" Width="70%" >
<table><tr><td>Subject ID:</td><td><asp:Label ID="lblSubID" runat="server" CssClass="boldlbl"></asp:Label></td><td>Subject Name:</td><td><asp:Label ID="lblSubName" runat="server" CssClass="boldlbl"></asp:Label></td></tr>
<tr><td>Total Marks:</td><td><asp:Label ID="lblMaxMark" runat="server" CssClass="boldlbl"></asp:Label></td><td>Passing Marks:</td><td><asp:Label ID="lblMinMark" runat="server" CssClass="boldlbl"></asp:Label></td></tr>
<tr><td>First Div Marks:</td><td><asp:Label ID="lblFirst" runat="server" CssClass="boldlbl"></asp:Label></td></tr>
</table>
<br /><asp:Button ID="btnAddMeorExp" runat="server" OnClick="btnAddMoreExp_Click" Text="Add More" />
<asp:LinkButton ID="lbtnExpStatus" runat="server"></asp:LinkButton>
</asp:Panel></center>
<center><asp:Panel runat="server" ID="PanelInsertExperience" Width="90%">
<fieldset><legend>&nbsp;<img src="../../images/leftlink.jpg" alt="" />&nbsp;Insert Subject Detials:&nbsp;</legend>
<center><asp:Label ID="lblInsertExpInfo" runat="server" ></asp:Label></center>
<table width="95%">
<tr align="right"><td>Select Subject:</td>
<td align="left">
<asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="SubName"  Width="263px" DataValueField="SubName" onselectedindexchanged="ddlSubject_SelectedIndexChanged" ></asp:DropDownList>
</td>
</tr>
</table>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [SubName], [SubID], [Section] FROM [CivilSubMaster] WHERE (([Section] = @Section) AND ([CourseID]=@CourseID)) ORDER BY [SubID]">
<SelectParameters>
<asp:ControlParameter ControlID="lblSecType" Name="Section" PropertyName="Text" DefaultValue="Select Subject Name" Type="String" />
<asp:QueryStringParameter Type="String" Name="CourseID" QueryStringField="id" />
</SelectParameters>
</asp:SqlDataSource>
<table width="95%"><tr align="right"><td>&nbsp;</td><td align="left">
<asp:Label ID="lblActiveId" runat="server"></asp:Label>
</td></tr>
<tr align="right"><td>Subject ID:</td>
<td align="left"><asp:TextBox ID="txtSubID" runat="server" AutoPostBack="True" ontextchanged="txtSubID_TextChanged" Width="255px"></asp:TextBox>
<asp:RequiredFieldValidator ID="rexID" runat="server" ControlToValidate="txtSubID" Display="Dynamic" ErrorMessage="Please Insert Subject ID." ForeColor="Red" ValidationGroup="Architecture">*</asp:RequiredFieldValidator>
</td></tr>
<tr  align="right"><td>Subject Name:</td><td align="left">
    <asp:TextBox ID="txtSubName" Width="255px" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubName" Display="Dynamic" ValidationGroup="Architecture" ForeColor="Red" ErrorMessage="Please Insert Subject Name." >*</asp:RequiredFieldValidator></td></tr>
<tr><td></td><td align="left"><asp:CheckBox ID="chkSubjectType" runat="server" AutoPostBack="true" OnCheckedChanged="chkSubjecttype_CheckChaged" /></td></tr>
<tr align="right"><td>Total Marks:</td><td align="left">
    <asp:TextBox ID="txtMaxMark" Width="255px" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMaxMark" Display="Dynamic" ValidationGroup="Architecture" ForeColor="Red" ErrorMessage="Please Insert Subject Total Marks." >*</asp:RequiredFieldValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMaxMark" FilterType="Numbers"></dev:FilteredTextBoxExtender><asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Subject Total Marks  greater than 999" ValueToCompare="999" ControlToValidate="txtMaxMark" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td></tr>
<tr align="right"><td>Passing Marks:</td><td align="left">
    <asp:TextBox ID="txtMinMark" Width="255px" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMinMark" Display="Dynamic" ValidationGroup="Architecture" ForeColor="Red" ErrorMessage="Please Insert Subject Passing Marks" >*</asp:RequiredFieldValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMinMark" FilterType="Numbers"></dev:FilteredTextBoxExtender><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passing Marks can not be greater then 999" ValueToCompare="999" ControlToValidate="txtMinMark" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td></tr>
<tr align="right"><td>I<sup>st</sup>&nbsp;Div. Marks</td><td align="left">
    <asp:TextBox ID="txtFirst" Width="255px" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirst" Display="Dynamic" ValidationGroup="Architecture" ForeColor="Red" ErrorMessage="Please Insert Subject First Score Marks." >*</asp:RequiredFieldValidator><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtFirst" FilterType="Numbers"></dev:FilteredTextBoxExtender><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="First Div. marks can not be greater then 999" ValueToCompare="999" ControlToValidate="txtFirst" Operator="LessThanEqual" Type="Double" ValidationGroup="Architecture">*</asp:CompareValidator></td></tr>
</table><asp:ValidationSummary ID="validatoinSummary2" runat="server" DisplayMode="BulletList" ValidationGroup="Architecture" CssClass="expbox" />
<br /><asp:Label ID="lblSaveException" runat="server" ></asp:Label>
<center><asp:Button ID="btnSave" runat="server" Text="Save"  ValidationGroup="Architecture" onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click" /></center>
</fieldset></asp:Panel></center>
</div>
</asp:Panel><br /><asp:Label ID="lbltemp" runat="server" ></asp:Label>
<asp:Panel ID="PnlPartI" runat="server" >
<div class="sylabuseditbox" style="padding-right:10px;"><b><asp:LinkButton ID="lbtnPartIEdit" runat="server" ForeColor="#F5E0CE" Text="Edit" onclick="lbtnPartIEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><h1>PART-I</h1>
<br />
<h3>Technician Engineering Subjects:-&nbsp;&nbsp;&nbsp;Part I</h3>
<br />
<asp:GridView ID="GridView1" runat="server" AllowPaging="True"  PageSize="20" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Vertical" Width="100%">
<Columns>
<asp:BoundField DataField="SubID" HeaderText="Subject Code" SortExpression="SubID" />
<asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />
<asp:BoundField DataField="MaxMark" HeaderText="Total" SortExpression="MaxMark" />
<asp:BoundField DataField="MinMark" HeaderText="Min. Passing Marks" SortExpression="MinMark" />
<asp:BoundField DataField="First" HeaderText="First Div. Marks" SortExpression="First" />
</Columns>
<FooterStyle BackColor="#CCCCCC" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<EmptyDataTemplate>
<center>Subject List not Found !!!</center>
</EmptyDataTemplate>
<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="#CCCCCC" />
</asp:GridView><br />
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [SubID], [SubName], [MaxMark], [MinMark], [First], [Date] FROM [CivilSubMaster] WHERE (([Section] = @Section) AND ([CourseID] = @CourseID)) ORDER BY [SubID]">
<SelectParameters>
<asp:Parameter DefaultValue="PartI" Name="Section" Type="String" />
<asp:QueryStringParameter Type="String" Name="CourseID" QueryStringField="id" />                
</SelectParameters>
</asp:SqlDataSource></div>
</asp:Panel><br />
<asp:Panel ID="PnlPartII" runat="server" >
<div class="sylabuseditbox" style="padding-right:10px; padding-bottom:10px;"><b><asp:LinkButton ID="lbtnPartIIEdit" runat="server" ForeColor="#F5E0CE" Text="Edit" onclick="lbtnPartIIEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><h1>PART-II</h1>
<br />
<h3>Technician Engineering Subjects:-&nbsp;&nbsp;&nbsp;Part II</h3>
<br />
<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" PageSize="20" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Vertical" Width="100%">
<Columns>
<asp:BoundField DataField="SubID" HeaderText="SubID" SortExpression="Subject Code" />
<asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />
<asp:BoundField DataField="MaxMark" HeaderText="Total" SortExpression="MaxMark" />
<asp:BoundField DataField="MinMark" HeaderText="Min. Passing Marks" SortExpression="MinMark" />
<asp:BoundField DataField="First" HeaderText="First Div. Marks" SortExpression="First" />
<asp:BoundField DataField="SubjectType" HeaderText="Type" SortExpression="SubjectType" />
</Columns>
<FooterStyle BackColor="#CCCCCC" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="#CCCCCC" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [SubID], [SubName], [MaxMark], [MinMark], [Date], [First], [SubjectType] FROM [CivilSubMaster] WHERE (([Section] = @Section) AND ([CourseID]=@CourseID)) ORDER BY [SubID]">
<SelectParameters>
<asp:Parameter DefaultValue="PartII" Name="Section" Type="String" />
<asp:QueryStringParameter Type="String" Name="CourseID" QueryStringField="id" />                
</SelectParameters>
</asp:SqlDataSource>
</div>
</asp:Panel><br />
<asp:Panel ID="PnlSecA" runat="server" >
<div class="sylabuseditbox" style="padding-right:10px;"><b><asp:LinkButton ID="lbtnSecAEdit" runat="server" ForeColor="#F5E0CE" Text="Edit" onclick="lbtnSecAEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><h1>SECTION A</h1>
<br />
<h3>Associate Engineering Subjects:-&nbsp;&nbsp;&nbsp;Section A</h3>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" PageSize="20"  AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource5" ForeColor="Black" GridLines="Vertical" Width="100%">
<Columns>
<asp:BoundField DataField="SubID" HeaderText="SubID" SortExpression="Subject Code" />
<asp:BoundField DataField="SubName" HeaderText="Subject Name" SortExpression="SubName" />
<asp:BoundField DataField="MaxMark" HeaderText="Total" SortExpression="MaxMark" />
<asp:BoundField DataField="MinMark" HeaderText="Min. Passing Marks" SortExpression="MinMark" />
<asp:BoundField DataField="First" HeaderText="First Div. Marks" SortExpression="First" />
</Columns>
<FooterStyle BackColor="#CCCCCC" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="#CCCCCC" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [SubID], [SubName], [MaxMark], [MinMark], [First], [Date] FROM [CivilSubMaster] WHERE (([Section] = @Section) AND ([CourseID]=@CourseID)) ORDER BY [SubID]">
<SelectParameters>
<asp:Parameter DefaultValue="SectionA" Name="Section" Type="String" />
<asp:QueryStringParameter Type="String" Name="CourseID" QueryStringField="id" />                  
</SelectParameters>
</asp:SqlDataSource>
<br />
</div>
</asp:Panel><br />
<asp:Panel ID="PnlSecB" runat="server">
<div class="sylabuseditbox" style="padding-right:10px;"><b><asp:LinkButton ID="lbtnSecBEdit" runat="server" ForeColor="#F5E0CE" Text="Edit" onclick="lbtnSecBEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><h1>SECTION B</h1>
<br />
<h3>Associate Engineering Subjects:-&nbsp;&nbsp;&nbsp;Section B</h3>
<asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"  PageSize="30"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource4" ForeColor="Black" GridLines="Vertical" Width="100%" onpageindexchanging="GridView3_PageIndexChanging">
<Columns>
<asp:BoundField DataField="SubID" HeaderText="SubID" SortExpression="SubID" />
<asp:BoundField DataField="SubName" HeaderText="SubName" SortExpression="SubName" />
<asp:BoundField DataField="MaxMark" HeaderText="MaxMark" SortExpression="MaxMark" />
<asp:BoundField DataField="MinMark" HeaderText="MinMark" SortExpression="MinMark" />
<asp:BoundField DataField="SubjectType" HeaderText="SubjectType" SortExpression="SubjectType" />
<asp:BoundField DataField="First" HeaderText="First" SortExpression="First" />
</Columns>
<FooterStyle BackColor="#CCCCCC" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="#CCCCCC" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" SelectCommand="SELECT [SubID], [SubName], [MaxMark], [MinMark], [SubjectType], [First] FROM [CivilSubMaster] WHERE (([Section] = @Section) AND ([CourseID]=@CourseID)) ORDER BY [SubID], [SubjectType]">
<SelectParameters>
<asp:Parameter  DefaultValue="SectionB" Name="Section" Type="String" />
<asp:QueryStringParameter Type="String" Name="CourseID" QueryStringField="id" />                     
</SelectParameters>
</asp:SqlDataSource>
<br />
</div>
</asp:Panel>
</ContentTemplate></asp:UpdatePanel>
</div>
</asp:Content>