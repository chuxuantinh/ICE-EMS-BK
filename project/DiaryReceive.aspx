<%@ Page Title="" Language="C#" MasterPageFile="~/project/Projects.master" AutoEventWireup="true" CodeFile="DiaryReceive.aspx.cs" Inherits="project_DiaryReceive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Project Diary Receiving
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="redirect">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab" /></td><td>
<asp:Label ID="lblNext" runat="server" Text="Diary Receive" CssClass="redirecttabhome"/></td></tr></table>
</div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1>Project Diary Receiving</h1></div><br />
<center>
<table><tr><td><input id="Hidden1" runat="server" type="hidden" value="0" />
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
<script type="text/javascript" language="javascript">
    function ConfirmApp() {
        if (confirm("Are you sure you want to Receive selected diary?") == true)
            return true;
        else
            return false;
    }
</script>
<div id="div3" style="width: 100%; overflow:scroll; height:150px">
<asp:GridView ID="GrdCountDispatch" runat="server"  HorizontalAlign="Center"  Width="200px" PageSize="100"  CellPadding="4" ForeColor="#333333" GridLines="None">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
<EmptyDataTemplate><center>No Records Found!</center></EmptyDataTemplate>
<EmptyDataRowStyle BackColor="Cyan" ForeColor="#333333" />
<Columns>
<asp:TemplateField><HeaderTemplate><asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate></asp:TemplateField>
</Columns>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
</asp:GridView>
</div></td><td><asp:Button ID="btnReceive" runat="server" Text="Receive" CssClass="btnsmall" onclick="btnReceive_Click"  OnClientClick="return ConfirmApp();"/></td><td><input id="scrollPos4" runat="server" type="hidden" value="0" />
<div id="div2" style="width: 100%; overflow:scroll; height:150px">
<asp:GridView ID="GridDiaryNo" runat="server"  HorizontalAlign="Center" onselectedindexchanged="GridDiaryNo_SelectedIndexChanged" Width="200px" PageSize="100" CellPadding="4" ForeColor="#333333" GridLines="None" onrowdatabound="GridDiaryNo_RowDataBound">
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
</div></td></tr></table>
<asp:Label ID="lblHiddenSeason" runat="server" Visible="false" />
<table class="tbl">
<tr><td>Select Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" OnTextChanged="ddlExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>&nbsp;&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtYearSeason" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged"></asp:TextBox></td></tr>
<tr><td align="left">Diary No:</td><td align="left"><asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox" Width="100px" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChanged" ForeColor="Maroon"/>&nbsp;&nbsp;IMID:&nbsp;<asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox" Width="50px" ></asp:TextBox></td></tr>
<tr><td align="left"><asp:Label ID="lblRcv" Text="Receiving Date:" runat="server" Visible="false"/></td>
<td align="left"><asp:Label ID="lblSubDate" runat="server" /></td></tr>
</table>
</center>
<div class="redsubtitle">Project Form</div>
<table class="tbl" width="100%"><tr align="center"><td>No of DD<br /><asp:TextBox ID="txtPDD" runat="server" Width="50px" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txtPDD" ></dev:FilteredTextBoxExtender></td>
<td>Proforma A<br /><asp:TextBox ID="txtProformaC" runat="server" Width="50px" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtProformaC" /></td>
<td>Proforma B<br /><asp:TextBox ID="txtProformaB" runat="server" Width="50px" CssClass="txtbox"/><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers" TargetControlID="txtProformaB" /></td>
<td>Proforma C<br /><asp:TextBox ID="txtProformaA" runat="server" Width="50px" CssClass="txtbox" /><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="txtProformaA" /></td>
</tr></table>
<br />
<center><asp:Label ID="lblExcepiton" runat="server" ForeColor="Maroon"/>
<br />
<br /><asp:Button ID="btnSubmit" runat="server" CssClass="btnsmall" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;<asp:Button ID="btnSupply" runat="server" CssClass="btnsmall" Text="Supply to Account" onclick="btnSupply_Click" /></center>
<br />
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
<div class="togalfees" style="width:100%">
<div class="headerDivImgfees">
<a id="A1x" href="javascript:toggleA1x('Div1x', 'A1x');"><img src="../images/minus.png" alt="Show"></a>
</div><div style="padding:5px;"><h1>&nbsp;Total Diary:<asp:Label ID="lblDiary" runat="server" ></asp:Label></h1></div>
<div id="Div1x" style="display:block;">
<input id="scrollPos" runat="server" type="hidden" value="0" />
<div id="divdatagrid1" style="width: 99%; overflow:scroll; height:300px">
<asp:GridView ID="GridProject" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Width="99%" HorizontalAlign="Center" onrowdatabound="GridProject_RowDataBound">
<EmptyDataTemplate><center> Record(s) Not Found !!!</center></EmptyDataTemplate>
<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White"/>
</asp:GridView>
</div></div></div>
</div><br />
</asp:Content>