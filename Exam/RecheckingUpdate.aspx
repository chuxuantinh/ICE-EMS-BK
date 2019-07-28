<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="RecheckingUpdate.aspx.cs" Inherits="Exam_RecheckingUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Rechecking Form Update
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    

    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="scriptmangaer11" runat="server" ></asp:ScriptManager>
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" 
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td></tr></table></div>

<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Update Rechecking Form</h1></div>
<table  class="tbl">
 <tr><td>Examination Session:</td><td>&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlExamSeason" 
         runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" 
         AutoPostBack="true" 
          ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
 <%--<tr><td>Examination Roll No:</td><td colspan="1"><asp:TextBox ID="txtRollNo" runat="server" CssClass="txtbox" Width="150px" Font-Bold="true"></asp:TextBox></td><td>
     <asp:Button ID="btnOK" runat="server" Text=" OK " OnClick="btnOK_OnClcick" 
         CssClass="btnsmall" /></td></tr>--%>
 </table><table class="tbl" width="95%">
<tr><td>Syllabus Level:</td><td><asp:DropDownList ID="ddlSyllabus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSyllabus_SelectedIndexChanged"></asp:DropDownList></td><td><asp:Label ID="lblStreamName" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblStreamCode" runat="server" Visible="false"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SeelctedIndexchanged" CssClass="txtbox"><asp:ListItem Value="Civil" Text="Civil Engineering" /><asp:ListItem Value="Architecture" Text="Architectural Engineering" /></asp:DropDownList></td><td>Part/Section:&nbsp;</td><td><asp:DropDownList ID="ddlPart" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged" runat="server" CssClass="txtbox"><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td></tr>

<tr><td>Subject Code:</td><td><asp:DropDownList ID="ddlsubcode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCode_SelectedIndexChanged"></asp:DropDownList></td><td>Subject Name:&nbsp;&nbsp;&nbsp;</td><td><asp:Label ID="lblSubNamess" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>Total Marks:</td><td><asp:Label ID="lblToMarks" runat="server" 
        Font-Bold="True"></asp:Label></td><td>Min. Passing Marks:</td><td><asp:Label ID="lblMinMarsk" runat="server" Font-Bold="true"></asp:Label></td></tr>
<tr><td>First Div. Marks:&nbsp;</td><td><asp:Label ID="lblFirstMarks" runat="server" Font-Bold="true"></asp:Label></td></tr>
</table><asp:Label ID="lblHiddenSeason" runat="server" Visible="false"></asp:Label><center><asp:Label ID="lblExceptionOK" runat="server" ></asp:Label></center>
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
</div><div style="padding:5px; color:White; font-size:18px; font-family:Times New Roman;">Feed Marks</div>
<div id="Divdev" style="display:block;">
  <input id="scrollPos3" runat="server" type="hidden" value="0" />
                 <div id="divdatagrid3" style="width: 100%; overflow:scroll; height:250px">
<asp:GridView ID="GridMarks" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        CellPadding="3"  ForeColor="Black" GridLines="Vertical" 
        Width="100%" AllowPaging="True" onpageindexchanging="GridMarks_PageIndexChanging" 
                         PageSize="50" onselectedindexchanged="GridMarks_SelectedIndexChanged">
        <RowStyle HorizontalAlign="Center" />
        <EmptyDataTemplate><center>Subject Record Not Found !</center></EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="IMID" HeaderText="IMID" SortExpression="IMID" />        
            <asp:BoundField DataField="SID" HeaderText="Std.ID" SortExpression="SID" />
            <asp:BoundField DataField="RollNo" HeaderText="Roll No." 
                SortExpression="RollNo" /><asp:BoundField DataField="SubID" HeaderText="SubID" 
                SortExpression="SubId" />
                <asp:BoundField DataField="SubName" HeaderText="SubName" 
                SortExpression="SubName" /><asp:BoundField DataField="OldMarks" HeaderText="OldMarks" 
                SortExpression="OldMarks" />
                 <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:Label ID="lblObtainMarks" runat="server" Text="NewMarks" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:TextBox ID="txtObtainMarks" runat="server" Width="100px" CssClass="txtbox"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField> 
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
   </div>
   </div></div>
   <center>
       <asp:Button ID="btnSave" runat="server" Text=" Save " 
         CssClass="btnsmall" onclick="btnSave_Click" /></center>
</div></asp:Content>