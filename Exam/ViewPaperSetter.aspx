<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewPaperSetter.aspx.cs" Inherits="Exam_ViewPaperSetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Paper Setter Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />

<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Paper Setter Profile</h1></div>
 <center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server"><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;</td></tr></table></center>
 <center><table class="tbl">
 <tr>
 <td align="center"><asp:RadioButtonList ID="rbnPapersetter" runat="server" RepeatDirection="Horizontal" Width="372px" onselectedindexchanged="rbnPapersetter_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="Code" Selected="False" Text="Paper Setter Code" ></asp:ListItem>
        <asp:ListItem Value="Course" Text="Course"></asp:ListItem>
        <asp:ListItem Value="Name" Text="Name"></asp:ListItem>
 </asp:RadioButtonList></td>
 </tr>
 <tr>
 <td colspan="5">
     <asp:Panel ID="pnlCourse" align="center" runat="server" Visible="false" >
        Course:
        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="txtbox" AutoPostBack="true"
             onselectedindexchanged="ddlCourse_SelectedIndexChanged">
            <asp:ListItem Value="Civil" Text="Civil Engineering" />
        <asp:ListItem Value="Architecture" Text="Architectural Engineering" />
        </asp:DropDownList>
     </asp:Panel>
 </td>
 </tr>
 <tr>
 <td colspan="5">
    <asp:Panel ID="pnlView" runat="server" align="Center" Visible="false" >
        <asp:Label ID="lblPaperCode" runat="server" Text="PaperSetterCode:" Font-Bold="True"></asp:Label>
        <asp:Label ID="lblNameShow" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
        <asp:Label ID="lblCourseShow" runat="server" Text="Course:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" CssClass="btnsmall"  />
    </asp:Panel></td>
 </tr>
 </table>
 <br />
 </center>
    <asp:Panel ID="pnlGrid" align="center" runat="server">
     <script>
         function toggleA1x(showHideDiv, switchImgTag) {
             var ele = document.getElementById(showHideDiv);
             var imageEle = document.getElementById(switchImgTag);
             var imageEle = document.getElementById(switchImgTag);
             if (ele.style.display == "block")
             {
                 ele.style.display = "none";
                 imageEle.innerHTML = '<img src="../images/plus.png">';
             }
             else
             {
                 ele.style.display = "block";
                 imageEle.innerHTML = '<img src="../images/minus.png">';
             }
         }
    </script>
    <div class="togalfees" style="width:99%">
    <div class="headerDivImgfees">
 <a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/plus.png" alt="Show"></a>
</div><div align="left" style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Paper Setter:</div>
<div id="Div1x" style="display:block;"><br />
  <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="div1" style="width: 98%; overflow:scroll; height:200px" 
            >
<asp:GridView ID="GridpaperView" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" 
        GridLines="Vertical" onselectedindexchanged="GridpaperView_SelectedIndexChanged" 
        Width="100%">
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Code" HeaderText="Code" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Session" HeaderText="Session" />
            <asp:BoundField DataField="Course" HeaderText="Course" />
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
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlselect" align="center" runat="server" Visible="false" >
    <table class="tbl">
    <tr>
    <th align="left" >Paper Setter Code</th>
    <td>
        <asp:Label ID="lblPapersetterCode" runat="server" Text="PapersetterCode"></asp:Label>
    </td>
    <th align="left">Designation</th>
    <td>
        <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label></td>
    </tr>
    <tr>
    <th align="left">Name</th>
    <td>
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
    </td>
        <th align="left">DOB</th>
    <td><asp:Label ID="lblDOB" runat="server" Text="DOB"></asp:Label></td>
    </tr>
    <tr>
    <th align="left">Age</th>
    <td>
        <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label></td>
        <th align="left">Education</th>
    <td>
        <asp:Label ID="lblEducation" runat="server" Text="Education"></asp:Label></td>
    </tr>
    <tr>
    <th align="left">Experience</th>
        <td> <asp:Label ID="lblExperience" runat="server" Text="Experience"></asp:Label></td>
    </tr>
    <tr>
    <th align="left" >Permanent Address</th>
    <td colspan="2">
        <asp:Label ID="lblPAddress" runat="server" Text="PAddress"></asp:Label>&nbsp;
        <asp:Label ID="lblPCity" runat="server" Text="PCity"></asp:Label>&nbsp;
        <asp:Label ID="lblPState" runat="server" Text="PState"></asp:Label>&nbsp;
        <asp:Label ID="lblPPin" runat="server" Text="PPin"></asp:Label>
    </td>
    </tr>
    <tr>
    <th align="left">Correspondence Address</th>
    <td colspan="2">
        <asp:Label ID="lblCAddress" runat="server" Text="CAddress"></asp:Label>&nbsp;
        <asp:Label ID="lblCCity" runat="server" Text="CCity"></asp:Label>&nbsp;
        <asp:Label ID="lblCState" runat="server" Text="CState"></asp:Label>&nbsp;
        <asp:Label ID="lblCPin" runat="server" Text="CPin"></asp:Label>
    </td>
    </tr>
    <tr>
    <th align="left" class="style1">Mobile</th>
    <td class="style1">
        <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
    <th align="left" class="style1">Phone</th>
    <td class="style1">
        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></td>
    </tr>
    <tr>
    <th align="left">Fax</th>
    <td>
        <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label></td>
        <th align="left">Email</th>
        <td>
            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
    </tr>
    <tr>
        <th align="left">Session</th>
        <td>
            <asp:Label ID="lblSession" runat="server" Text="Session"></asp:Label></td>
        <th align="left">Course</th>
        <td>
            <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label></td>
        </tr>
    <tr>
    <th align="left">SubjectCode</th>
    <td>
        <asp:Label ID="lblSubjectCode" runat="server" Text="SubjectCode"></asp:Label></td>
        <th align="left">SubjectName</th>
        <td>
            <asp:Label ID="lblSubjectName" runat="server" Text="SubjectName"></asp:Label></td>
        </tr>
    </table>
    </asp:Panel>
    <br />
    <br />
    <br />
    <br />
    </div>
    <br />
    <br />
</asp:Content>