<%@ Page Language="C#" MasterPageFile="~/Acc/Account.master" AutoEventWireup="true"
    CodeFile="EditMainAC.aspx.cs" Inherits="Acc_EditMainAC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Edit ICE Main Account
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
                    <asp:Label ID="lblEditMainAC" runat="server" Text="Edit Account" CssClass="redirecttabhome"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="rightpanel2">
        <asp:UpdatePanel ID="UpdatePanelIMInfo" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
            <ContentTemplate>
                <div class="fromRegisterlbl">
                    <h1 style="float: right; margin-right: 50px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Edit Count:&nbsp;<asp:Label ID="lblEditCount"
                            runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                ID="lblEnrolment" runat="server"></asp:Label></h1>
                    <h1>
                        Edit Account</h1>
                </div>
                <br />
                <asp:Panel ID="pnlEdit" runat="server">
                    <table width="98%">
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
                                            IM ID:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIDIM" runat="server" BorderColor="Gray" Width="150px" CssClass="txtbox"
                                                AutoPostBack="true" BorderStyle="Solid" BorderWidth="1px" ForeColor="Blue" OnTextChanged="txtIDIM_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAccountNo" Text="Diary No." runat="server"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDiaryNo" Width="150px" CssClass="txtbox" runat="server" AutoPostBack="true"
                                                OnTextChanged="txtDiaryNo_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Diary No."
                                                    ControlToValidate="txtDiaryNo" ValidationGroup="amt">*</asp:RequiredFieldValidator>&nbsp;&nbsp;<asp:Button
                                                        ID="btnViewDiray" runat="server" Text="View" CssClass="btnsmall" OnClick="btnViewDiary_OnClick" />
                                            <br />
                                            <asp:Label ID="lblexceptionDirary" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DD No.:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDDSearch" runat="server" AutoPostBack="true" CssClass="txtbox"
                                                Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td>
                                <input id="scrollPos4" runat="server" type="hidden" value="0" />
                                <div id="div1" style="width: 100%; overflow: scroll; height: 150px">
                                    <asp:GridView ID="GridDiaryNo" runat="server" HorizontalAlign="Center" OnSelectedIndexChanged="GridDiaryNo_SelectedIndexChanged"
                                        Width="200px" PageSize="100" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                    <hr />
                    <asp:Panel ID="pnlIMInfo" runat="server">
                        <br />
                        <div class="rightbox">
                            <center>
                                <br />
                                Group ID:&nbsp;<asp:Label ID="lblGroupID" runat="server"></asp:Label>
                            </center>
                            <center>
                                <table visible="false">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Diary Amount:&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiaryAmount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRenewalDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="ibtnviewGroup" runat="server" Text="View Group" Visible="false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <asp:Panel ID="panelIM" runat="server" CssClass="imbox">
                            <asp:Label ID="lblIMName" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="15px"></asp:Label><br />
                            <asp:Label ID="lblIMAddress" runat="server"></asp:Label><br />
                            <asp:Label ID="lblIMCity" runat="server"></asp:Label><br />
                            <br />
                        </asp:Panel>
                    </asp:Panel>
                    <div id="Div12" style="display: block;">
                        <input id="scrollPos2" runat="server" type="hidden" value="0" />
                        <div id="divdatagrid2" style="width: 100%; overflow: scroll; height: 200px">
                            <asp:GridView ID="GridAppTable" runat="server" BackColor="#DEBA84" AutoGenerateColumns="true"
                                OnRowDataBound="GridAppTable_RowDataBound" BorderColor="#DEBA84" BorderStyle="None"
                                BorderWidth="1px" CellPadding="5" OnSelectedIndexChanged="GridAppTable_SelectedIndexChanged"
                                CellSpacing="5" Width="100%">
                                <EmptyDataTemplate>
                                    <center>
                                        Record(s) Not Found !</center>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:CommandField SelectImageUrl="~/images/Edit.png" HeaderText="Edit" ShowSelectButton="true"
                                        ButtonType="Image" />
                                </Columns>
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="panelCourier" runat="server" CssClass="expbox">
                        <center>
                            Name of Bank:&nbsp;<asp:TextBox ID="txtNewCourier" runat="server" CssClass="txtbox"
                                Width="200px" Font-Bold="true"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNewCity" Visible="false"
                                runat="server" Width="150px" CssClass="txtbox"></asp:TextBox><br />
                            <asp:Label ID="lblExceptionNewCourier" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
                            <br />
                            <asp:Button ID="btnSaveNewCourier" runat="server" Text="Save" OnClick="btnSAveNew_Onclick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCencel" runat="server" Text="Cencel" OnClick="btnCencelnew_Onclick" />
                        </center>
                    </asp:Panel>
                    <asp:Panel ID="panelSubmitAmt" runat="server">
                        <center>
                            <asp:Label ID="lblTitleInfo" runat="server" Font-Bold="true" ForeColor="Gray"></asp:Label></center>
                        <br />
                        <asp:Label ID="lblhiddenSession" runat="server" Visible="false"></asp:Label>
                        <center id="ddgrid">
                            <asp:Label ID="lblEdit" Text="Please select Edit from above table" runat="server"></asp:Label></center>
                        <table width="98%">
                            <tr>
                                <td>
                                    Amount Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAmtType" runat="server" CssClass="txtbox" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAmtType_SelectedIndexChanged">
                                        <asp:ListItem Value="DD" Text="Demand Draft"></asp:ListItem>
                                        <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem>
                                        <asp:ListItem Text="Chaque" Value="CC"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDDNNO" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDDNO" CssClass="txtbox" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="reqddno" runat="server" Display="Dynamic" ErrorMessage="Please Insert DD No."
                                        ControlToValidate="txtDDNO" ValidationGroup="amt">*</asp:RequiredFieldValidator>
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
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sub. Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" Enabled="false" runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator
                                            runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtDate" Display="Dynamic"
                                            ValidationGroup="amt" ErrorMessage="Insert Date of Submission, Othrwise default present date.">*</asp:RequiredFieldValidator><dev:CalendarExtender
                                                Format="dd/MM/yyyy" ID="devdage" PopupButtonID="cal" PopupPosition="BottomRight"
                                                runat="server" TargetControlID="txtDate">
                                            </dev:CalendarExtender>
                                        <img src="../images/cal.png" id="cal" runat="server" alt="Cal" /><br />
                                        <asp:Label ID="lblExceptiondAte" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td>
                                    Amount For:
                                </td>
                                <td>
                                    <br />
                                    <%--<asp:DropDownList ID="ddlAmtForMs" CssClass="txtbox" runat="server" DataSourceID="SqlDataSource2"
                                        DataTextField="Name" DataValueField="Name" Width="200px">
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlAmtForMs" runat="server" CssClass="txtbox" >
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Narration:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNarration" CssClass="txtbox" runat="server" TextMode="MultiLine"
                                        Width="300px" Height="50px"></asp:TextBox>
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
                                <td>
                                    DD Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDOB" AutoPostBack="true" OnTextChanged="txtDate_TechChanged"
                                        runat="server" CssClass="txtbox"></asp:TextBox><asp:RequiredFieldValidator runat="server"
                                            ID="RequiredFieldValidator3" ControlToValidate="txtDOB" Display="Dynamic" ValidationGroup="amt"
                                            ErrorMessage="Insert Date of DD.">*</asp:RequiredFieldValidator><dev:CalendarExtender
                                                Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="cal2" PopupPosition="BottomRight"
                                                runat="server" TargetControlID="txtDOB">
                                            </dev:CalendarExtender>
                                    <img src="../images/cal.png" id="cal2" runat="server" alt="Cal" /><br />
                                    <asp:Label ID="lblExcedate" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div id="totalamt" runat="server">
                            Your Total Amount:&nbsp;&nbsp;<asp:Label Font-Bold="true" ForeColor="Maroon" ID="lblTAmt"
                                runat="server"></asp:Label>&nbsp;Rs.</div>
                        <asp:ValidationSummary ID="validasum" runat="server" CssClass="expbox" DisplayMode="BulletList"
                            ForeColor="Red" ValidationGroup="Architecture" />
                        <center>
                            <asp:Label ID="lblException" runat="server"></asp:Label></center>
                        <br />
                        <center>
                            <asp:Button ID="btnSubmitAmt" runat="server" Text="Edit" ValidationGroup="amt" CssClass="btnsmall"
                                OnClick="btnSubmitAmt_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btnsmall"
                                    ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" /></center>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    <br />
</asp:Content>
