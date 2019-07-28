<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" CodeFile="Membership.aspx.cs" Inherits="Administrator_Membership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="usermanage" runat="server"  >
  <asp:Panel ID="panelAccount" runat="server" >
    <center><table width="100%"><tr><td  id="tdRegisternews" runat="server" align="center">
            <asp:ImageButton ID="ibtnmembership" runat="server" 
                ImageUrl="~/images/newmem1.png" AlternateText="View Accounts Form" onclick="ibtnmembership_Click" 
                /><br />
        <asp:LinkButton ID="lbtnnewmember" runat="server"  CssClass="txt2"  
               >New Membership</asp:LinkButton></td>
    <td id="tdInspections" runat="server" align="center">
        <asp:ImageButton ID="ibtnInspections" runat="server"  
            ImageUrl="~/images/inspection1.jpg" AlternateText="Im Inspection" onclick="ibtnInspection_Click" 
            /><br />
        <asp:LinkButton ID="lbtnInspection" runat="server"  CssClass="txt2"  
           >Inspection</asp:LinkButton></td>
           <td id="tdSubscriptions" runat="server" align="center">
                <asp:ImageButton ID="ibtnSubscriptions"  runat="server" 
                    AlternateText="IM Subscription" ImageUrl="~/images/membershipacc.jpg" onclick="ibtnSubscription_Click" 
                   /><br />
        <asp:LinkButton ID="lbtnSubcription" runat="server"  CssClass="txt2"   
                >Subscription</asp:LinkButton></td>
           </tr><tr><td><br /></td></tr>
            <tr><td id="tdViewProfiles" runat="server" align="center"><asp:ImageButton ID="ibtnViewProfile" runat="server" 
             ImageUrl="~/images/UserIcon3.png" 
             AlternateText="View Profile" onclick="ibtnViewProfile_Click" 
            /><br />
        <asp:LinkButton ID="lbtnviewprofile" runat="server"  CssClass="txt2"  
            >View Profile</asp:LinkButton></td><td id="tdCertificates" runat="server" align="center">
                    <asp:ImageButton ID="ibtnCertificates" runat="server" 
                        AlternateText="IM Certificates" 
                        ImageUrl="~/images/certificate1.png" onclick="ibtnCertificate_Click" 
                        /><br />
        <asp:LinkButton ID="lbtnCertificate" runat="server"  CssClass="txt2" 
                >Certificate</asp:LinkButton></td>
                <td id="tdreeports" runat="server" align="center">
        <asp:ImageButton ID="ibtnReportss" ImageUrl="~/images/report1.png" 
            runat="server" AlternateText="Reports" onclick="ibtnReportss_Click"  
            /><br />
        <asp:LinkButton ID="lbtnreeports" runat="server" CssClass="txt2" 
            >Reports</asp:LinkButton></td>
                </tr><tr><td><br /></td></tr>
    </table></center><!-- Exam Fee==Account and membership Fee== Admission Form -->
    </asp:Panel><br />
</div>
</asp:Content>

