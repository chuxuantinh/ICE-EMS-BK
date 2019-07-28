<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/ProfileMaster.master" AutoEventWireup="true" CodeFile="DebitNoteReq.aspx.cs" Inherits="Profile_DebitNoteReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" Runat="Server">Debit Note Requests
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
<link href="../style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" />
<div id="redirect">	
<table><tr>
<td><asp:LinkButton ID="lblHomeRedirect" runat="server" Text="Home" CssClass="redirecttab" onclick="lblHomeRedirect_Click"/></td>
<td><asp:Label ID="lblText" Text="Debit Note" runat="server" CssClass="redirecttabhome"/></td>
</tr></table></div>
<div id="rightpanel2">
<asp:UpdatePanel ID="updpnlDebit" runat="server"><ContentTemplate>
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="lnlRequest" 
                    runat="server" Text="Requested" onclick="lnlRequest_Click" Height="20px" 
                    ImageUrl="~/images/request.png" Width="20px" ToolTip="Requested" /> 
                      &nbsp;<asp:ImageButton ID="lnlApproved" runat="server" Text="Approved"  onclick="lnlApproved_Click" Height="20px" Width="20px" ImageUrl="~/images/approve.png" ToolTip="Approved" /> 
    &nbsp;<asp:ImageButton ID="lnkProcess" runat="server" Text="Processed" onclick="lnkProcess_Click"  Height="20px"  Width="20px" ImageUrl="~/images/processq.png" ToolTip="Processed" />
    &nbsp;<asp:ImageButton ID="imgHold" runat="server" Text="Hold"  Height="20px" Width="20px" ImageUrl="~/images/hold1.png" ToolTip="Hold"  onclick="imgHold_Click" Visible="false" /></h1>
<h1>Debit Note</h1>
    <p><table class="tbl"><tr><td>&nbsp;&nbsp;&nbsp;&nbsp; Session:</td><td><asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true" CssClass="txtbox"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox><asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td><td align="right">
                &nbsp;</td></tr></table>
        
    </p>
    </div>

<div id="div2" style="width: 100%; overflow:scroll; height:250px;"  >
<asp:GridView ID="gridDebitNote" runat="server" CellPadding="4" Width="100%"   onrowdatabound="gridDebitNote_RowDataBound"   onselectedindexchanged="gridDebitNote_SelectedIndexChanged"   ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>
<asp:CommandField ShowSelectButton="True" />
</Columns>
<EmptyDataTemplate>No Records Found</EmptyDataTemplate>
    <EditRowStyle BackColor="#999999" />
<EmptyDataRowStyle  HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
<FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#E9E7E2" />
<SortedAscendingHeaderStyle BackColor="#506C8C" />
<SortedDescendingCellStyle BackColor="#FFFDF8" />
<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView></div>
<asp:Panel ID="pnlAppr" runat="server" Visible="false">
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
    <a id="A12" href="javascript:toggleA1w('DivApp', 'A12');"><img src="../images/minus.png" alt="Show"></a>
</div><h1>Diary No:<asp:Label id="lblDno" runat="server"></asp:Label></h1>
                    <div id="DivApp" style="display: block;">
                        <input id="scrollPos2" runat="server" type="hidden" value="0" />
                        <div id="divdatagrid2" style="width: 100%; overflow: scroll; height: 200px">
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
<asp:GridView ID="grdAppRecord" runat="server" CellPadding="4" Width="100%" OnRowDataBound="gridAppRecord_RowDataBound"  ForeColor="#333333" HorizontalAlign="Center" >
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>
 <asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
<asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
</Columns>
<EmptyDataTemplate>No Records Found</EmptyDataTemplate>
    <EditRowStyle BackColor="#999999" />
<EmptyDataRowStyle  HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
<FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
<PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#E9E7E2" />
<SortedAscendingHeaderStyle BackColor="#506C8C" />
<SortedDescendingCellStyle BackColor="#FFFDF8" />
<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView></div></div></div>
<center><b>Remarks :</b><br /> &nbsp;<asp:TextBox ID="txtRemarks" runat="server"  Height="52px" CssClass="txtbox" TextMode="MultiLine" Width="295px"></asp:TextBox>
<br />
<br />
<asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btnsmall" onclick="btnApprove_Click" />&nbsp;&nbsp;
<asp:Button ID="btnHold" runat="server" Text="Send To Account" CssClass="btnsmall" onclick="btnHold_Click" />
</center>
</asp:Panel>
<br />
</ContentTemplate></asp:UpdatePanel>
</div>
<br />
</asp:Content>

