<%@ Page Title="" Language="C#" MasterPageFile="~/Exam/ExamMaster.master" AutoEventWireup="true"
    CodeFile="DuplicateAdmitCard.aspx.cs" Inherits="Exam_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenttitle" runat="Server">Duplicate AdmitCard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        tr.try1 td, tr.try2 td
        {
            padding: 4px;
        }
        tr.try1 td
        {
            padding: 8px 5px;
            background-color: #F1F1F1;
        }
        tr.try2 td
        {
            padding: 8px 5px;
        }
    </style>
    <asp:Panel ID="Panel1" runat="server" Height="550px"><br /><br />
    <center><table><tr><td>Exam Session:</td><td><asp:DropDownList ID="ddlExamSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamSeason_SelectedIndexChanged" CssClass="txtbox" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtYearSeason" AutoPostBack="true" OnTextChanged="txtYearSeason_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox> 
   &nbsp;&nbsp; </td></tr></table></center><asp:Label ID="lblExamSeasonHidden" runat="server" Visible="false"></asp:Label>
    <table style="padding: 5px; font-size: 11px;"  align="center">
        <tbody>
            <tr>
            <td colspan="2"> <asp:Label ID="Label1" runat="server" Text="Excel File Upload" 
                            Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label><br /><br /><br />
            </tr>
            <tr>
                <td>
                    <div>

                        <strong>Please Select Excel file </strong><asp:FileUpload ID="txtFilePath" runat="server"></asp:FileUpload>&nbsp;&nbsp;
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                            onclick="btnUpload_Click"  /><br />
                            Select Lot No To Print Admit Card:&nbsp;&nbsp;&nbsp;<asp:DropDownList 
            ID="ddlRSN" runat="server" DataSourceID="SqlDataSource2" 
            DataTextField="RSN" DataValueField="RSN" Width="100px">
        </asp:DropDownList> 
        &nbsp;&nbsp;<asp:Button ID="Padmit" runat="server" Text="Print Admit Card" 
            onclick="Padmit_Click" />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ICEDataConnectionString %>" 
            SelectCommand="SELECT DISTINCT [RSN] FROM [ExamForms] WHERE (([ExamSeason] = @ExamSeason))">
            <SelectParameters>
            <asp:ControlParameter ControlID="lblExamSeasonHidden" Name="ExamSeason" 
                PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
                        <br /><br /><br />
                        <asp:Label ID="lblMessage" runat="server" align="center" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label><br /><br /><br />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grvExcelData" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                        onpageindexchanging="grvExcelData_PageIndexChanging" PageSize="10"  >
                        <RowStyle CssClass="try2" />
                        <AlternatingRowStyle CssClass="try1" />
                    </asp:GridView>
                </td>
            </tr>
        </tbody>
    </table>
    </asp:Panel>
</asp:Content>
