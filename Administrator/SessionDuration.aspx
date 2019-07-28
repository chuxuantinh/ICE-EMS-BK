<%@ Page Language="C#" MasterPageFile="~/Administrator/Administrator.master" AutoEventWireup="true" CodeFile="SessionDuration.aspx.cs" Inherits="Administrator_SessionDuration" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Session Duration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="updatepanel" runat="server" ><ContentTemplate><br />
<center>
                            <br />
                          <table class="tbl"><tr><td>Stream:</td><td colspan="2"><asp:DropDownList Width="200px" 
        ID="ddlStream" runat="server" CssClass="txtbox" ><asp:ListItem Value="Asso" Text="Associate Examination" /><asp:ListItem Value="Tech" Text="Technical Examination" /></asp:DropDownList></td>
<td>Part/Section:&nbsp;</td><td><asp:DropDownList  ID="ddlPart"  runat="server" CssClass="txtbox"><asp:ListItem Text="--Select--" Value=""></asp:ListItem><asp:ListItem Value="PartI" Text="Part I" /><asp:ListItem Value="PartII" Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" /><asp:ListItem Value="SectionB" Text="Section B" /></asp:DropDownList></td></tr>

<tr><td>Duration:</td><td><asp:DropDownList ID="ddlDuration" runat="server" Width="150px" >
<asp:ListItem Text="1.5" Value="1.5"></asp:ListItem>
<asp:ListItem Text="2" Value="2"></asp:ListItem>
<asp:ListItem Text="2.5" Value="2.5"></asp:ListItem>
<asp:ListItem Text="3" Value="3"></asp:ListItem>
<asp:ListItem Text="3.5" Value="3.5"></asp:ListItem>
<asp:ListItem Text="4" Value="4"></asp:ListItem>
<asp:ListItem Text="4.5" Value="4.5"></asp:ListItem>
<asp:ListItem Text="5" Value="5"></asp:ListItem>
<asp:ListItem Text="5.5" Value="5.5"></asp:ListItem>
<asp:ListItem Text="6" Value="6"></asp:ListItem>
</asp:DropDownList></td><td><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Onclic" /></td></tr>
</table>
                            
<script type="text/javascript">
if (window.XMLHttpRequest)
  {// code for IE7+, Firefox, Chrome, Opera, Safari
  xmlhttp=new XMLHttpRequest();
  }
else
  {// code for IE6, IE5
  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
xmlhttp.open("GET","../Exam/SessionDuration.xml",false);
xmlhttp.send();
xmlDoc=xmlhttp.responseXML; 

document.write("<table border='1' width='250px'><CAPTION>Civil Engineering</CAPTION>");
var x=xmlDoc.getElementsByTagName("Session");
//for (i=1;i<=8;i++)
//  { 
// var elm="part"+i+"";
  document.write("<tr><td>Part I");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part1")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  document.write("<tr><td>Part II");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part2")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  
  document.write("<tr><td>Section A");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part3")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  document.write("<tr><td>Section B");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part4")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr></table><br><table  border='1' width='250px'><CAPTION>Architectural Engineering</CAPTION>");
  
  document.write("<tr><td>Part I");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part5")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  document.write("<tr><td>Part II");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part6")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  document.write("<tr><td>Section A");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part7")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  document.write("<tr><td>Section B");
  document.write("</td><td>");
  document.write(x[0].getElementsByTagName("part8")[0].childNodes[0].nodeValue);
  document.write("&nbsp;Year</td></tr>");
  
  
//  }
document.write("</table>");
</script>
                            
                         </center>
</ContentTemplate></asp:UpdatePanel>
</asp:Content>

