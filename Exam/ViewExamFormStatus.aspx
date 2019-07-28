<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true" CodeFile="ViewExamFormStatus.aspx.cs" Inherits="Exam_ViewRollNotGenerated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Exam Form Status</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:Label ID="lblPageName" runat="server" CssClass="redirecttabhome" Text="View Status" ></asp:Label> </td></tr></table></div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList 
            ID="ddlExamSeason" runat="server" 
            OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" 
           ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
        ID="txtYearSeason" runat="server" CssClass="txtbox" AutoPostBack="true" 
        Width="100px" OnTextChanged="txtYearSeason_TextChanged" Height="16px"></asp:TextBox></h1><h1>
        Examination Form Status</h1></div><br />
<br /><center>View Status:<asp:DropDownList
            ID="ddlType" runat="server" AutoPostBack="True" onselectedindexchanged="ddlType_SelectedIndexChanged" 
            >
            <asp:ListItem>RollNo</asp:ListItem>
            <asp:ListItem>AdmitCard</asp:ListItem>
        </asp:DropDownList></center>
<asp:Label ID="lblSeasonHidden" runat="server" Visible="false"></asp:Label><br />
<center>
        <asp:Label ID="lblSelect" runat="server" Text="Select Criteria"></asp:Label><asp:DropDownList
            ID="ddlSelect" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlSelect_SelectedIndexChanged">
            <asp:ListItem>IM</asp:ListItem>
            <asp:ListItem>ExamCenter</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" 
            onselectedindexchanged="ddlList_SelectedIndexChanged" Height="22px" 
            Width="200px">
        </asp:DropDownList><asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" 
            AutoPostBack="true" Width="100px" ontextchanged="txtIMID_TextChanged" 
            Height="16px" ></asp:TextBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [Name] FROM [ExamCenter] WHERE ([Season] = @Season)">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblSeasonHidden" Name="Season" 
                    PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
       </center>
    <br />
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
    </script><center><asp:Label ID="lblExceptionAppTable" runat="server"></asp:Label></center>
<div class="togalfees" style="width:100%">
    <div class="headerDivImgfees">
    
   <asp:ImageButton ID="ImageButton1" runat="server"  Height="30px" Width="30px" AlternateText="Doc" ImageUrl="~/images/doc_icon.png" OnClick="ibtnExportDocAppTableDoc_click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2"  Height="30px" Width="30px"  runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif" OnClick="ibtnExportExcelAppTableDoc_Click" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton3"  Height="30px" Width="30px" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif" OnClick="ibtnExportPDFAppTableDoc_Click" />
   
    </div>
    <div style="padding:5px; color:White; font-size:15px;">
<asp:RadioButton ID="rbtnGenerated" runat="server" AutoPostBack="True" 
            GroupName="a" oncheckedchanged="rbtnGenerated_CheckedChanged" 
            Text="Generated" /><asp:RadioButton ID="rbtnNotGenerated"
            runat="server" AutoPostBack="True" GroupName="a" 
            oncheckedchanged="rbtnNotGenerated_CheckedChanged" Text="Not Generated" /> 
</div>
<div id="Div1" style="display:block;">
 <input id="scrollPos" runat="server" type="hidden" value="0" />

                 <div id="divdatagrid1" style="width: 98%; overflow:scroll; height:400px">
 
   
 <input id="scrollPos2" runat="server" type="hidden" value="0" />
<div id="divdatagrid2" style="width: 100%; overflow:scroll; height:400px" 
          >
   
    <asp:GridView ID="GridView2" runat="server" AllowPaging="true"
        AutoGenerateColumns="true" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
         ForeColor="Black" GridLines="Vertical" 
        Width="100%" onselectedindexchanged="GridView2_SelectedIndexChanged" 
        onrowdatabound="GridView2_RowDataBound">
        <RowStyle BackColor="#F7F7DE"  HorizontalAlign="Center"/>
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
</div>
</div></div>
</asp:Content>

