<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true"
    CodeFile="AppApprove.aspx.cs" Inherits="Acc_AppApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Application Form ICE(I)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="redirect">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lblHomeRedirect" runat="server" OnClick="ibtnHome_Click" Text="Home"
                        CssClass="redirecttab"></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="lblAppApprove" runat="server" Text="Application Forms Approval" CssClass="redirecttabhome"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
        <asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddToApprovalTAble" />
                <asp:PostBackTrigger ControlID="ibtnExportPDF"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="ibtnExportDoc"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="ibtnExportExcel"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnSendDebitNote" />
                <asp:PostBackTrigger ControlID="btnPay" />
            </Triggers>
            <ContentTemplate>
                <div class="fromRegisterlbl">
                    <h1 style="float: right; margin-right: 50px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblEnrolment" runat="server"></asp:Label></h1>
                    <h1>
                        Application Forms Approval [Accounts Section]
                    </h1>
                </div>
                <asp:Label ID="lblIMCity" runat="server" Visible="false" Font-Bold="true"></asp:Label><asp:Label
                    ID="txtDOB" runat="server" Visible="false"></asp:Label><asp:Label ID="lblSessionHiddend"
                        runat="server" Visible="false" Font-Bold="true"></asp:Label>
                <table width="98%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="1">
                                        Select Session:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsession" runat="server" CssClass="txtbox" OnTextChanged="ddldevExamSeason_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                                            <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true"
                                            Width="100px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        IM ID:&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIMID" runat="server" CssClass="txtbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button
                                            ID="btnOK" runat="server" Text=" OK " CssClass="btnsmall" OnClick="btnOK_Click" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Diary No.:&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiaryNo" runat="server" CssClass="txtbox" AutoPostBack="true"
                                            OnTextChanged="txtDiaryNo_TectChantd"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Image
                                                ID="ibtnViewDairy" ImageUrl="~/images/dairycount.gif" runat="server" AlternateText="Dairy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>(<asp:Label
                                            ID="lblGroupID" runat="server"></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Application Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAppstype" runat="server" CssClass="txtbox" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlApstype_OnSelectedIndexChanged">
                                            <asp:ListItem Value="Academic" Text="Academic" />
                                            <asp:ListItem Value="Project" Text="Project" />
                                            <asp:ListItem Value="AutoCAD" Text="AutoCAD" />
                                            <asp:ListItem Value="Others" Text="Others" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <input id="scrollPos4" runat="server" type="hidden" value="0" />
                            <div id="div1" style="width: 100%; overflow: scroll; height: 150px">
                                <asp:GridView ID="GridDiaryNo" runat="server" HorizontalAlign="Center" OnSelectedIndexChanged="GridDiaryNo_SelectedIndexChanged"
                                    Width="200px" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                            </div>
                        </td>
                    </tr>
                </table>
                <dev:PopupControlExtender ID="popupex" runat="server" Position="Center" OffsetX="-550"
                    OffsetY="0" PopupControlID="pnlDairyCount" TargetControlID="ibtnViewDairy">
                </dev:PopupControlExtender>
                <asp:Panel ID="pnlDairyCount" runat="server" Width="350px" CssClass="pnlpopup">
                    <div class="redsubtitle">
                        <center>
                            Application Form Count</center>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                Applications
                            </td>
                            <td>
                                Total Received
                            </td>
                            <td>
                                Total Submitted
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Academic DD
                            </td>
                            <td>
                                <asp:Label ID="lblADDRcv" ForeColor="White" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ForeColor="White" ID="lblADDSub" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Others DD
                            </td>
                            <td>
                                <asp:Label ID="lblODDRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblODDSub" ForeColor="White" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Admission Form
                            </td>
                            <td>
                                <asp:Label ID="lblAdmissionRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAdmissionSub" ForeColor="White" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Exam Form
                            </td>
                            <td>
                                <asp:Label ID="lblExamRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblExamSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ITI Form
                            </td>
                            <td>
                                <asp:Label ID="lblITIRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblITISub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Others Form
                            </td>
                            <td>
                                <asp:Label ID="lblOthersFormRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOthersFormSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Provisional
                            </td>
                            <td>
                                <asp:Label ID="lblProvisionalRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProvisionalSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Final Pass
                            </td>
                            <td>
                                <asp:Label ID="lblFinalPassRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFinalPassSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Re-Checking
                            </td>
                            <td>
                                <asp:Label ID="lblReCheckingRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblReCheckingSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Duplicate Docs
                            </td>
                            <td>
                                <asp:Label ID="lblDuplicateRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDuplicateSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project DD
                            </td>
                            <td>
                                <asp:Label ID="lblProjectRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProjectSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project ProformaC
                            </td>
                            <td>
                                <asp:Label ID="lblProformaCRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProformaCSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project ProformaB
                            </td>
                            <td>
                                <asp:Label ID="lblProformaBRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProformaBSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Membership DD
                            </td>
                            <td>
                                <asp:Label ID="lblMembershipRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMembershipSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Books DD
                            </td>
                            <td>
                                <asp:Label ID="lblBooksRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBooksSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Prospectus DD
                            </td>
                            <td>
                                <asp:Label ID="lblProsRcv" runat="server" ForeColor="White"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProsSub" runat="server" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <center>
                    <asp:Label ID="lblExceptionOK" runat="server" Font-Bold="true"></asp:Label></center>
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
                <center>
                    <asp:Label ID="lblExceptionAppTable" runat="server"></asp:Label></center>
                <div class="togalfees" style="width: 100%">
                    <div class="headerDivImgfees" style="color: White;">
                        Date:&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="txtbox" Width="80px"></asp:TextBox>
                        <a id="A12" href="javascript:toggleA1w('Div12', 'A12');">
                            <img src="../images/minus.png" alt="Show"></a>
                    </div>
                    <table style="color: White; font-weight: bold;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnApproveNow" runat="server" CssClass="btnsmall" OnClick="btnSaveRecordTable_click"
                                    Text="View Record(s)" />&nbsp;&nbsp;<asp:Button ID="btnSelectAll" CssClass="btnsmall"
                                        runat="server" Text="Select All" OnClick="btnSelectAll_Onclick" />&nbsp;&nbsp;<asp:Button
                                            ID="btnViewHold" runat="server" Text="View Hold" OnClick="btnViewHold_Click"
                                            CssClass="btnsmall" />
                            </td>
                        </tr>
                    </table>
                    <div id="Div12" style="display: block;">
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
                            <asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true"
                                OnRowDataBound="GridAppTable_RowDataBound" BorderColor="#DEBA84" BorderStyle="None"
                                BorderWidth="1px" CellPadding="5" CellSpacing="5" Width="100%">
                                <EmptyDataTemplate>
                                    <center>
                                        Record(s) Not Found !</center>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:ButtonField ButtonType="Link" CommandName="Select" Text="Approve" Visible="false" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSelectAlll" runat="server" OnClick="selectAlll(this)" /></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkapp" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="togalfees" style="width: 100%">
                    <div class="headerDivImgfees">
                        <a id="A14" href="javascript:toggleA1w('Div12', 'A12');"></a>
                    </div>
                    <table style="color: White; padding: 5px; width: 80%; font-weight: bold;">
                        <tr>
                            <td>
                                <asp:Label Visible="false" ID="lblExamSnFo" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblAdmiSnFO" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount:&nbsp;<asp:Label ID="lblToAmtFo"
                                    runat="server" ForeColor="White" Font-Bold="true"></asp:Label>&nbsp;Rs.
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;Total Late Fees:&nbsp;<asp:Label ID="lblToLateFo" runat="server"
                                    ForeColor="White" Font-Bold="true"></asp:Label>&nbsp;Rs.
                            </td>
                        </tr>
                    </table>
                    <div id="Div4" style="display: none;">
                    </div>
                </div>
                <script>
                    function toggleBG(showHideDiv, switchImgTag) {
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
                <center>
                    <asp:Label ID="lblExceptinApprovedTable" runat="server"></asp:Label></center>
                <asp:Label ID="lblAddTotal" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblExamTotal" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblotherTotal" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblAddForms" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblExamForms" runat="server" Visible="False"></asp:Label>
                <div class="togalfees" style="width: 100%">
                    <div class="headerDivImgfees">
                        <asp:ImageButton ID="ibtnExportDoc" runat="server" AlternateText="Doc" ImageUrl="~/images/doc_icon.png"
                            OnClick="ibtnExportDocAppTable_click" Height="25px" Width="25px" />&nbsp;&nbsp;<asp:ImageButton
                                ID="ibtnExportExcel" runat="server" AlternateText="Excel" ImageUrl="~/images/excel_icon.gif"
                                OnClick="ibtnExportExcelAppTable_Click" Height="25px" Width="25px" />&nbsp;&nbsp;<asp:ImageButton
                                    ID="ibtnExportPDF" runat="server" AlternateText="PDF" ImageUrl="~/images/pdf-icon3.gif"
                                    OnClick="ibtnExportPDFAppTable_Click" Height="25px" Width="25px" />
                        <a id="A1G" href="javascript:toggleBG('Div1G', 'A1G');">
                            <img src="../images/plus.png" alt="Show"></a>
                    </div>
                    <table style="color: White; font-weight: bold;">
                        <tr>
                            <td>
                                <asp:Button ID="btnAddToApprovalTAble" runat="server" CssClass="btnsmall" Text="Select for Approval"
                                    OnClick="btnAddToApprovalTableFinal_Click" />&nbsp;&nbsp;<asp:Button ID="btnHold"
                                        runat="server" CssClass="btnsmall" Text="Hold" OnClick="btnHold_Click" />
                            </td>
                        </tr>
                    </table>
                    <div id="Div1G" style="display: none;">
                        <input id="scrollPos3" runat="server" type="hidden" value="0" />
                        <div id="divdatagrid3" style="width: 100%; overflow: scroll; height: 200px">
                            <asp:GridView ID="GridApprove" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true"
                                OnRowDataBound="GridApprove_RowDataBound" AllowPaging="false" BorderColor="#DEBA84"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="5" Width="100%">
                                <EmptyDataTemplate>
                                    <center>
                                        Record(s) Not Added !</center>
                                </EmptyDataTemplate>
                                <Columns>
                                </Columns>
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="togalfees" style="width: 100%">
                    <div class="headerDivImgfees">
                        <a id="A2" href="javascript:toggleBG('Div1G', 'A1G');"></a>
                    </div>
                    <table style="color: White; padding: 5px; width: 80%; font-weight: bold;">
                        <tr>
                            <td>
                                <asp:Label Visible="false" ID="lblExamSnFo2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label Visible="false" ID="lblAdmiSnFO2" runat="server"></asp:Label>
                            </td>
                            <td>
                                To.No of Forms:&nbsp;[<asp:Label ID="lbltoFNo" runat="server" ForeColor="White" Font-Bold="true"></asp:Label>
                                ]
                            </td>
                            <td>
                                To. Amount:&nbsp;<asp:Label ID="lblToAmtFo2" runat="server" ForeColor="White" Font-Bold="true"></asp:Label>&nbsp;Rs.
                            </td>
                            <td>
                                To. Late Fees:&nbsp;<asp:Label ID="lblToLateFo2" runat="server" ForeColor="White"
                                    Font-Bold="true"></asp:Label>&nbsp;Rs.
                            </td>
                        </tr>
                    </table>
                    <div id="Div3" style="display: none;">
                    </div>
                </div>
                <div class="pnltwice" id="acclass" runat="server">
                    <table class="tbl" width="100%">
                        <tr>
                            <td>
                                <table>
                                    <caption>
                                        ICE (I)</caption>
                                    <tr>
                                        <td colspan="2">
                                            IMID:<asp:DropDownList ID="ddlGid" runat="server" CssClass="txtbox" OnTextChanged="ddlGid_SelectedIndexChanged"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlGid_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                  
                                </table>
                            </td>
                            <td>
                                <table>
                                    <caption>
                                        IM Account</caption>
                                    <tr>
                                        <td>
                                            Total Amount:(Rs.)
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTAmt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Payable Amount:(Rs.)
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPAmt" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Diary Amount:(Rs.)
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiaryAmount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblMessage1" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="UpdatePanelAccount">
            <ContentTemplate>
                <asp:Panel ID="panelApproveNow" runat="server">
                    <table width="100%" style="background-color: Maroon; padding: 3px; color: White;">
                        <tr>
                            <td>
                               Approval Limit:&nbsp;&nbsp;<asp:Label ID="lblApprovallimit" runat="server" ForeColor="White"></asp:Label>
                               &nbsp;&nbsp;&nbsp;Used:&nbsp;<asp:Label ID="lblApprovalRange" runat="server" ForeColor="White"></asp:Label>&nbsp;
                               &nbsp;&nbsp;Available:&nbsp;<asp:Label ID="lblAvailable" runat="server" ForeColor="White"></asp:Label>&nbsp;&nbsp;
                                <asp:CheckBox ID="chkUseLimit" runat="server" Text="Add Amount" AutoPostBack="true" OnCheckedChanged="chkAddAmount_OnChackedChanged" />
                            </td>
                        </tr>
                    </table>
                    <center>
                        <asp:Label ID="lblExceptionAC" runat="server"></asp:Label><br />
                     &nbsp;</center>
                    <asp:Panel ID="panelAcount" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;Total Payable&nbsp;<asp:Label ID="lblIMAmount" runat="server" ForeColor="Maroon"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>   Total Required Amt:&nbsp;&nbsp;Rs.&nbsp;&nbsp;<asp:Label ID="lblReqAmt" runat="server"
                            ForeColor="Maroon" Font-Bold="true"></asp:Label><br />
                                </td>
                                <td>
                                    &nbsp;&nbsp;Dues Amount:&nbsp;<asp:Label ID="lblDuesAmt" runat="server" ForeColor="Maroon" Text="0"
                                        Font-Bold="true"></asp:Label>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <center>
                            <b>
                                <asp:Label ID="lblPayMessage" runat="server"></asp:Label></b><br />
                            <script type="text/javascript" language="javascript">
                                function ConfirmApp() {
                                    if (confirm("Are you sure you want to Approve this Form?") == true)
                                        return true;
                                    else
                                        return false;
                                }
                            </script>
                            <asp:Label ID="lblDebitNote" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="btnApprove" runat="server" Text="Calculate" OnClick="btnApprove_Click"
                                CssClass="btnsmall" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btnsmall" ID="btnPay"
                                    runat="server" Text="Approve" OnClick="btnPay_click" OnClientClick="return ConfirmApp();" />&nbsp;&nbsp;&nbsp;<asp:Button
                                        ID="btnSendDebitNote" runat="server" CssClass="btnsmall" OnClick="btnSendDebitNote_Click"
                                        Text="Debit Note Request >>>" /></center>
                        <br />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
</asp:Content>
