<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="UpdateStudList.aspx.cs" Inherits="project_UpdateStudList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Update Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
<asp:Label ID="lblNext" runat="server" Text="Update Student List" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Update Student List</h1></div><br />
<center>
<table class="tbl"><tr><th align="left">Session</th>
<td>:</td><td align="left">
<asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true" CssClass="txtbox"  Width="170px">
<asp:ListItem Text="Winter Examination" Value="Win"/><asp:ListItem Text="Summer Examination" Value="Sum"/></asp:DropDownList>&nbsp;
<asp:TextBox ID="txtyr" runat="server" Width="80px" CssClass="txtbox" AutoPostBack="true"/></td></tr><tr>
<th align="left">Membership</th><td>:</td><td align="left"><asp:TextBox ID="txtID" runat="server" CssClass="txtbox" ontextchanged="txtID_TextChanged" AutoPostBack="true" Width="80px" /></td></tr>
<tr><th align="left">Name</th>
<td>:</td>
<td align="left"><asp:Label ID="lblName" runat="server" ForeColor="Brown" Font-Bold="true"/></td></tr><tr>
<th align="left">Course</th>
<td>:</td>
<td align="left"><asp:Label ID="lblCourse" runat="server" ForeColor="Brown" Font-Bold="true"/><asp:Label ID="lblpart" runat="server" ForeColor="Brown" Font-Bold="true"/></td>
</tr><tr><th align="left">IMID</th>
<td>:</td>
<td align="left"><asp:Label ID="lblIMID" runat="server" ForeColor="Brown" Font-Bold="true"/></td>
</tr><tr><th style="color: #800000" align="left">Select Course*</th><td style="color: #800000">:</td><td>
<asp:DropDownList ID="ddlCrse" runat="server" AutoPostBack="True" CssClass="txtbox" Width="170px" ForeColor="Maroon">
<asp:ListItem Value="Civil" Text="Civil Engineering"/>
<asp:ListItem Value="Architecture" Text="Architecture Engineering"/>
</asp:DropDownList>&nbsp; <asp:DropDownList ID="ddlPart" runat="server" AutoPostBack="True" CssClass="txtbox" Width="80px" ForeColor="Maroon">
<asp:ListItem Text="PartII"/>
<asp:ListItem Text="SectionB"/>
</asp:DropDownList></td></tr><tr><th align="left">Course Status</th>
<td>:</td>
<td align="left"><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="txtbox" Width="170px">
<asp:ListItem Value="FinalPass" Text="FinalPass"/>
<asp:ListItem Value="AfterReChecking" Text="AfterReChecking"/>
<asp:ListItem Value="Promotted" Text="Promotted"/>
<asp:ListItem Value="ReSubmit" Text="ReSubmit" />
</asp:DropDownList></td>
</tr>
<tr><td colspan="3" align="center"><asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btnsmall" OnClick="btnAdd_Click" /></td>
</tr>
</table>
<hr />
</center>
<center><asp:Label ID="lblApprvExcep" runat="server" ForeColor="Red" Font-Bold="true"/></center>
<asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label>
<center>
<asp:Panel ID="pnlView" runat="server" Visible="false">
<table class="tbl"><tr><td>Session:&nbsp;</td><td align="left">
<asp:DropDownList ID="ddlsession" runat="server" AutoPostBack="true" CssClass="txtbox" onselectedindexchanged="ddlsession_SelectedIndexChanged" Width="170px"><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;Year:&nbsp; <asp:TextBox ID="txtSession" runat="server" Width="72px" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"/>
</td></tr><tr><td>
Course:&nbsp;</td><td align="left"><asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" CssClass="txtbox" Width="170px" ForeColor="Maroon">
<asp:ListItem Value="Civil">Civil Engineering</asp:ListItem>
<asp:ListItem Value="Architecture">Architecture Engineering</asp:ListItem>
</asp:DropDownList>
<asp:RadioButton ID="rbtnPartII" runat="server" Text="PartII" GroupName="a" />
<asp:RadioButton ID="rbtnSectionB"  runat="server" Text="SectionB" GroupName="a"/></td></tr><tr><td colspan="2">
<asp:Button ID="btnView" runat="server" Text="View Final Passed" CssClass="btnsmall" onclick="btnView_Click" /> &nbsp;&nbsp;<asp:Button ID="btnPromotted" runat="server" Text="Promotted" CssClass="btnsmall" onclick="btnPromotted_Click" /></td></tr></table>
</asp:Panel>
<asp:Label ID="lblExceptionOK" runat="server" Font-Bold="True" ForeColor="Red" /><br />
</center>
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
<asp:Panel ID="Panel1" runat="server" Visible="false">
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
&nbsp;&nbsp;&nbsp;&nbsp;<a id="A1x" href="javascript:toggleA1w('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"/></a>
</div>
<div style="padding:5px; color:White; font-size:15px;"><b><asp:Label ID="lblList" runat="server"></asp:Label>&nbsp;&nbsp; </b><asp:Label ID="lblCount" runat="server"></asp:Label><br />
<br />
<div id="Div1x" style="display: block;">
<div  id="divApprPerfA" style="width: 100%; overflow:scroll; height:300px;">
<script type="text/javascript">
    function selectAlll(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
    } 
</script>
<asp:GridView ID="GridShow" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" CellSpacing="2" Width="100%" onrowdatabound="GridShow_RowDataBound" >
        <Columns>
             <asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><center>Record(s) Not Found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" Height="16px"/>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
</asp:GridView>
</div>
</div>
</div>
<br /><center>Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlsesion" runat="server" AutoPostBack="true" CssClass="txtbox"><asp:ListItem Text="Winter Examination" Value="Win" /><asp:ListItem Text="Summer Examination" Value="Sum" /></asp:DropDownList>&nbsp; 
<asp:TextBox ID="txtyer" runat="server" Width="72px" CssClass="txtbox" AutoPostBack="true"/><asp:Button ID="btnUpdateProject" Text="Update Project" runat="server" CssClass="btnsmall" onclick="btnUpdateProject_Click" /></center></div>
</asp:Panel>
<br />
<asp:Label ID="Label2" runat="server" Visible="false" />
<asp:Label ID="lblStat" runat="server" Visible="False"/>
<asp:Label ID="lblParts" runat="server" Visible="False" />
<asp:Label ID="lblCouse" runat="server" Visible="False"/>
<asp:Label ID="lblsName" runat="server" Visible="false" />
<asp:Label ID="lblIM" runat="server" Visible="False" /><br />
<hr />
<h2>Upload Project Data</h2>
<center>
<asp:FileUpload ID="fileupload" runat="server" CssClass="txtbox" />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="lbtnUpload_OnClick" />
</center>
<br /><b>
Note: Please take consideration while uploading data.</b><br />
1. file format should be excel file<br />
2. file sheet name should be <b>Sheet1$</b><br />
3. file column name should be as follows given below image<br />
4. Column Format should be String/Text
<br />
<img src="../images/ProjectUploadFormat.png"/>
</div><br />
</asp:Content>