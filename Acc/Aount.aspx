<%@ Page Title="" Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true"
    CodeFile="Aount.aspx.cs" Inherits="Acc_Aount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Submit Amount ICE(India)
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
                    <asp:Label ID="lblAount" runat="server" Text="Institutional Member[IM] Account" CssClass="redirecttabhome"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
        <asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server">
            <ContentTemplate>
                <div class="fromRegisterlbl">
                    <h1 style="float: right; margin-right: 50px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblEnrolment" runat="server"></asp:Label></h1>
                    <h1>
                        Institutional Member[IM] Account
                    </h1>
                </div>
                <br />
                <table width="98%" class="tbl">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Session:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="txtbox">
                                            <asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem>
                                            <asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;Year:&nbsp;<asp:TextBox ID="txtSession" Width="70px" runat="server"
                                            CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Membership:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIDIM" runat="server" BorderColor="Gray" AutoPostBack="true" CssClass="txtbox"
                                            OnTextChanged="txtIDIM_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAccountNo" Text="Diary No." runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtACNO" CssClass="txtbox" runat="server" AutoPostBack="true" OnTextChanged="txtDiaryNo_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Diary No."
                                            ControlToValidate="txtACNO" ValidationGroup="amt">*</asp:RequiredFieldValidator><br />
                                        <asp:Label ID="lblexceptionDirary" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            Academic DD<br />
                                            <asp:Label ID="lblADDSub" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblADDRcv" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
                                            Others DD<br />
                                            <asp:Label ID="lblODDSub" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblODDRcv" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
                                            Project DD<br />
                                            <asp:Label ID="lblProSub" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblProRcv" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
                                            Books DD<br />
                                            <asp:Label ID="lblBookSub" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblBookRcv" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
                                            Prospectus DD<br />
                                            <asp:Label ID="lblProspectusSub" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblProspectusRcv" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
                                            Total<br />
                                            <asp:Label ID="lblTDDSub" Font-Bold="true" ForeColor="Maroon" runat="server"></asp:Label>/<asp:Label
                                                ID="lblTDDRcv" Font-Bold="true" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <asp:Label ID="lblExceptionCount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></center>
                        </td>
                        <td>
                        </td>
                        <td>
                            <input id="scrollPos4" runat="server" type="hidden" value="0" />
                            <div id="div1" style="width: 100%; overflow: scroll; height: 150px">
                                <asp:GridView ID="GridDiaryNo" runat="server" HorizontalAlign="Center" OnSelectedIndexChanged="GridDiaryNo_SelectedIndexChanged"
                                    Width="200px" PageSize="100" OnRowDataBound="GridDiaryNo_OnRowDataBound" CellPadding="4"
                                    ForeColor="#333333" GridLines="None">
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
                </center><hr />
                <asp:Panel ID="pnlIMInfo" runat="server">
                    <br />
                    <div class="rightbox">
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        Total Amount:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Group Amount:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroupAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Books Amount:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBooksAmount" runat="server"></asp:Label>&nbsp;&nbsp;Prospectus:&nbsp;<asp:Label
                                            ID="lblProspectus" runat="server"></asp:Label>
                                </tr>
                                <tr>
                                    <td>
                                        Diary Amount:&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiaryAmount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                    <asp:Panel ID="panelIM" runat="server" CssClass="imbox">
                        <asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px"></asp:Label><br />
                        <asp:Label ID="lblIMAddress" runat="server"></asp:Label><br />
                        <asp:Label ID="lblIMCity" runat="server"></asp:Label><br />
                        <center>
                            Group ID:&nbsp;<asp:Label ID="lblGroupID" runat="server"></asp:Label>
                        </center>
                        <br />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="panelCourier" runat="server" CssClass="expbox">
                    <center>
                        Name of Bank:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox"
                            Width="200px" Font-Bold="true"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNewCity" Visible="false"
                            runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><br />
                        <asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
                        <br />
                        <asp:Button ID="btnSaveNewCourier" runat="server" Text="Save" OnClick="btnSAveNew_Onclick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel" runat="server" Text="Close" OnClick="btnCencelnew_Onclick" />
                    </center>
                </asp:Panel>
                <asp:Panel ID="panelSubmitAmt" runat="server">
                    <center>
                        <asp:Label ID="lblTitleInfo" runat="server" Font-Bold="true" ForeColor="Gray"></asp:Label></center>
                    <br />
                    <asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
                    <table width="95%" class="tbl">
                        <tr>
                            <td>
                                Amount Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAmtType" runat="server" CssClass="txtbox" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlAmtType_SelectedIndexChanged">
                                    <asp:ListItem Value="DD" Text="Demand Draft"></asp:ListItem>
                                    <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem>
                                    <asp:ListItem Text="Cheque" Value="CC"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <asp:Panel ID="PanBank" runat="server">
                            <tr>
                                <td>
                                    Bank:
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnNewCourierService" runat="server" Font-Bold="true" ForeColor="Blue"
                                        Text=" Add New Bank Name" OnClick="ibtnNewCourier_Onclick"></asp:LinkButton>
                                    <br />
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="txtbox" DataSourceID="SqlDataSource1"
                                        DataTextField="Name" DataValueField="Name" Width="200px">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>"
                                        SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Bank') ORDER BY Name">
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <asp:Label ID="lblDDNNO" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDDNO" CssClass="txtbox" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="reqddno" runat="server" Display="Dynamic" ErrorMessage="Please Insert DD No."
                                        ControlToValidate="txtDDNO" ValidationGroup="amt">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                Submission Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" AutoPostBack="true" OnTextChanged="txtDateSub_TextChanged"
                                    CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3"
                                        ControlToValidate="txtDate" Display="Dynamic" ValidationGroup="amt" ErrorMessage="Insert Date of Submission, Othrwise default present date.">*</asp:RequiredFieldValidator><dev:CalendarExtender
                                            Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="Img1" PopupPosition="BottomRight"
                                            runat="server" TargetControlID="txtDate">
                                        </dev:CalendarExtender>
                                <img src="../images/cal.png" id="Img1" runat="server" alt="Cal" />
                            </td>
                            <td>
                                DD Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDOB" AutoPostBack="true" OnTextChanged="txtDate_TechChanged"
                                    runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server"
                                        ID="RequiredFieldValidator9" ControlToValidate="txtDOB" Display="Dynamic" ValidationGroup="amt"
                                        ErrorMessage="Insert Date of DD.">*</asp:RequiredFieldValidator><dev:CalendarExtender
                                            Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight"
                                            runat="server" TargetControlID="txtDOB">
                                        </dev:CalendarExtender>
                                <img src="../images/cal.png" id="cal" runat="server" alt="Cal" /><br />
                                <asp:Label ID="lblExceptiondAte" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount For:
                            </td>
                            <td>
                                <%--<asp:LinkButton ID="lbtnNewAmtFor" runat="server" Font-Bold="true" ForeColor="Blue"
                                    Text=" Add New Amount Type" OnClick="ibtnNewAmtType_Onclick" Visible="false"></asp:LinkButton>--%>
                                <br />
                                <asp:DropDownList ID="ddlAmountHeader" runat="server" CssClass="txtbox" Width="150px">
                                </asp:DropDownList>
                                <%--<asp:DropDownList ID="ddlAmtForMs" runat="server" CssClass="txtbox" DataSourceID="SqlDataSource2"
                                    DataTextField="Name" DataValueField="Name" Width="150px" OnSelectedIndexChanged="ddlAmtForMs_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>--%>
                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:icedbConnectionString %>"
                                    SelectCommand="SELECT DISTINCT Name FROM ServiceNameMaster WHERE (Type = 'Amount') ORDER BY Name">
                                </asp:SqlDataSource>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Narration Box:&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtNarration" CssClass="txtbox" runat="server" TextMode="MultiLine"
                                    Width="200px" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmt" runat="server" CssClass="txtbox" AutoPostBack="true" OnTextChanged="txtAmt_textChanged"
                                    ValidationGroup="amt"></asp:TextBox><dev:FilteredTextBoxExtender ID="filtercurrentcy"
                                        runat="server" FilterType="Numbers" TargetControlID="txtAmt">
                                    </dev:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="Please Insert DD No." ControlToValidate="txtAmt" ValidationGroup="amt">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency:&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCurrancy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrancy_Changed"
                                    CssClass="txtbox">
                                    <asp:ListItem Value="RS" Selected="True" Text="Rupees"></asp:ListItem>
                                    <asp:ListItem Value="DL" Text="Dolar"></asp:ListItem>
                                    <asp:ListItem Value="OT" Text="Other"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCurrancyText" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblCurrancyName" runat="server"></asp:Label>&nbsp;<asp:TextBox ID="txtCurrancyValue"
                                    runat="server" AutoPostBack="true" OnTextChanged="txtCurrancyValue_TextChanged"
                                    CssClass="txtbox" Width="50px"></asp:TextBox><dev:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                        runat="server" FilterType="Numbers" TargetControlID="txtCurrancyValue">
                                    </dev:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelAmtFor" runat="server" CssClass="expbox">
                        <br />
                        <center>
                            <h4>
                                Insert New Amount Type</h4>
                            <br />
                            <br />
                            Amount Type:&nbsp;&nbsp;<asp:TextBox ID="txtNewAmtType" runat="server" CssClass="txtbox"
                                Width="200px" /><br />
                            <br />
                            <asp:Label ID="lbleXceptionAmtType" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                            <asp:Button ID="btnSaveAmtType" runat="server" Text="Save" OnClick="btnSAveAmtType_Onclick" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                ID="btnCencelamtType" runat="server" Text="Close" OnClick="btnCloseAmtFor_Onclick" /><br />
                        </center>
                    </asp:Panel>
                    <div id="totalamt" runat="server">
                        &nbsp;&nbsp;&nbsp; Total Amount:&nbsp;&nbsp;<asp:Label Font-Bold="true" ForeColor="Maroon"
                            ID="lblTAmt" runat="server"></asp:Label>&nbsp;Rs.</div>
                    <asp:ValidationSummary ID="validasum" runat="server" CssClass="expbox" DisplayMode="BulletList"
                        ForeColor="Red" ValidationGroup="Architecture" />
                    <center>
                        <asp:Label ID="lblException" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></center>
                    <br />
                    <center>
                        <asp:Button ID="btnSubmitAmt" runat="server" Text="Submit" ValidationGroup="amt"
                            CssClass="btnsmall" OnClick="btnSubmitAmt_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                CssClass="btnsmall" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" /></center>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <br />
</asp:Content>
