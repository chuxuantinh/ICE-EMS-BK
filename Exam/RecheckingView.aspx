<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="RecheckingView.aspx.cs" Inherits="Exam_RecheckingView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Rechecking From View
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 49px;
        }
        .style2
        {
            width: 49px;
            height: 26px;
        }
        .style3
        {
            height: 26px;
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
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Rechecking Form View</h1></div>
<asp:Panel ID="panlSession" runat="server" ><table width="90%" class="tbl"><tr>
    <td >Select Exam Session:</td>
    <td><asp:DropDownList ID="ddlSession" runat="server" CssClass="txtbox" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_ONSelectediNdexCanged" Width="150px" ><asp:ListItem Value="Sum" Text="Summer Examination" /><asp:ListItem Value="Win" Text="Winter Examination" /></asp:DropDownList>&nbsp;&nbsp;Year:<asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYear_OnTextChanged"></asp:TextBox><asp:Label ID="lblSessionHidden" runat="server" Visible="false"></asp:Label></td></tr><tr>
    <td >Select Criteria:</td>
    <td>
    <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="True" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem>Membership</asp:ListItem>
        <asp:ListItem>SerialNo</asp:ListItem>
        <asp:ListItem>RollNo</asp:ListItem>
        <asp:ListItem>SubjectCode</asp:ListItem>
        <asp:ListItem>IMID</asp:ListItem>
        <asp:ListItem>Name</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtEnter" runat="server" CssClass="txtbox"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; 
    </td></tr></table><table class="tbl" width="90%"<tr><td class="style1"></td><td><asp:Panel ID="SubjectCode" runat="server">
    <table><tr><td>CourseID
        <asp:DropDownList ID="ddlCourseId" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource1" DataTextField="CourseID" 
            DataValueField="CourseID" CssClass="txtbox" 
            onselectedindexchanged="ddlCourseId_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [CourseID] FROM [CivilSubMaster]">
        </asp:SqlDataSource>
    </td><td>Course
        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                CssClass="txtbox" onselectedindexchanged="ddlCourse_SelectedIndexChanged">
            <asp:ListItem Value="Civil">Civil Engineering</asp:ListItem>
            <asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
        </asp:DropDownList>
    </td>
        <td>
           Part
        <asp:DropDownList ID="ddlPart" runat="server" AutoPostBack="True" CssClass="txtbox" 
                onselectedindexchanged="ddlPart_SelectedIndexChanged">
            <asp:ListItem>PartI</asp:ListItem>
            <asp:ListItem>PartII</asp:ListItem>
            <asp:ListItem>SectionA</asp:ListItem>
            <asp:ListItem>SectionB</asp:ListItem>
        </asp:DropDownList></td>
        <td>
           SubjectCode
        <asp:DropDownList ID="ddlSubjectCode" runat="server" AutoPostBack="True" 
                CssClass="txtbox">
        </asp:DropDownList>
            
        </td>
       </tr> 
    </table></asp:Panel></td></tr></table><center><asp:Button ID="btnView" runat="server" CssClass="btnsmall" Text="View" OnClick="btnVeiw_OnClick"    /></center></asp:Panel>
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
    
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
    <a id="A12" href="javascript:toggleA1w('Div12', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1><asp:Label ID="lblGridTitle" runat="server" ></asp:Label>Total Forms:<asp:Label 
            ID="lblTotalForms" runat="server"></asp:Label>
    </h1>
<div id="Div12" style="display:block;">
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:450px">
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4"  PageSize="25" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onrowdatabound="GridView_RowDataBound" >
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
          
           
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
   
   
   </div>
</div>
</div></div></asp:Content>