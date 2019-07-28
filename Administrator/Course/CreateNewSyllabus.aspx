<%@ Page Language="C#" MasterPageFile="~/Administrator/Fees/FeeMaster.master" AutoEventWireup="true" CodeFile="CreateNewSyllabus.aspx.cs" Inherits="Administrator_Course_CreateNewSyllabus" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="dev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">ICE(I) Syllabus Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../style.css" rel="stylesheet" type="text/css" />
    <link href="../../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><asp:ScriptManager ID="Scriptmanager1" runat="server" ></asp:ScriptManager>
<div id="leftpanel2">
<div id="leftteg" >
    <script>
        function toggle100(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../../images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../../images/minus.png">';
            }
        }
    </script>
  
   
  <div class="togelleft">
    <div class="headerDivImg">
    
    <a id="imageDivLink100" href="javascript:toggle('contentDivImg100', 'imageDivLink100');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1>&nbsp;ICE(India) Courses&nbsp;</h1>
<div id="contentDivImg100" style="display: block;"> 
<br />
  <div id="leftLink">
   <ul><li><asp:LinkButton ID="lbtnCivilSubjects" runat="server" Text="Civil Engineering Subjects" 
           CssClass="leftlink" onclick="lbtnCivill_Click"></asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnArchiSubjects" runat="server" Text="Architectural Engineering Subjects" 
           CssClass="leftlink" onclick="lbtnArchitectural_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="btnCreateNowSyllabus" runat="server" Text="Create New Syllabus" 
           CssClass="leftlink" onclick="lbtnCteateNewSyllabus_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="btnManageSyllabus" runat="server" Text="Manage[Active/Disactive] Syllabus" 
           CssClass="leftlink" onclick="lbtnManageSyllabus_Click"></asp:LinkButton></li>
   
   </ul>
    </div>
           
        </div>
   <br />
   
    </div>
   </div>
          <script>
              function toggle99(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../../images/minus.png">';
                  }
              }
    </script>
   
   <%--<div class="togelleft">
    <div class="headerDivImg">
    
    <a id="imageDivLink99" href="javascript:toggle99('contentDivImg99', 'imageDivLink99');"><img src="../../images/minus.png" alt="Show"></a>
</div><h1></h1>
<div id="contentDivImg99" style="display: block;"> 
    
   <br />
    <br /><br />
    
    </div></div>--%>

     </div>
<div id="rightpanel2" style="border:none;">
<asp:UpdatePanel ID="updatePanelMaian" runat="server" ><ContentTemplate>
<div id="header" style="border:1px solid gray;"><div class="fromRegisterlbl"><h1 style="padding-top:0px;"><img src="../../images/forward.png" /> &nbsp;&nbsp;
    &nbsp;&nbsp;ICE(India) Syllabus Management:-</h1></div>
    </div><br />
    
    <asp:Panel ID="panelSelect" runat="server" CssClass="SubjectSelect">
  <br />
     
       <center><br />Select Stream<br />
           <asp:RadioButton ID="rbtnArchi" runat="server" GroupName="level" 
               Text="Architectural Engineering Syllabus"  AutoPostBack="true" 
               oncheckedchanged="rbtnArchi_CheckedChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton 
               ID="rbtnCivil" runat="server" GroupName="level" Text="Civil Engineering Syllabus" 
               AutoPostBack="true" oncheckedchanged="rbtnCivil_CheckedChanged" /></center> 
              
   
     </asp:Panel>
     <asp:Panel ID="PanelCheckBox" runat="server" CssClass="PanelcheckBoxSelect"><img src="../../images/boxleft.png" alt="" style="float:left;" /><img src="../../images/boxright.png" alt="" style="float:right;" /><div id="boxmiddle">
   
    <center><asp:LinkButton ID="lbntSelectAging" runat="server" Text="<<<" onclick="lbntSelectAging_Click"></asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblLevelInfoTitle" Font-Bold="true" ForeColor="white" runat="server" ></asp:Label>
    </center>

    </div>
    </asp:Panel>
    <asp:Panel ID="panelCreate" runat="server" >
  
    
  <br />
  <asp:Panel ID="PnlSubjectEdit" CssClass="SubEdit" runat="server" >
    <img src="../../images/bxleft.png" alt="" style="float:left;" /><img src="../../images/bxright.png" alt="" style="float:right;" /><div id="bxcenter"><div style="float:right;">
            <asp:ImageButton ID="ibtnClose" runat="server" ImageUrl="../../images/cls.png" 
                onclick="ibtnClose_Click" /></div>
      <br />
      <center>
      Select Session:&nbsp;&nbsp;<asp:DropDownList ID="ddlsession" runat="server" OnTextChanged="ddldevExamSeason_SelectedIndexChanged" AutoPostBack="true"  ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;Year:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSession" runat="server" CssClass="txtbox" AutoPostBack="true" Width="100px" OnTextChanged="txtdevYearSeason_TextChanged"></asp:TextBox>
      <asp:Label ID="lblSessionHiddend" runat="server" Visible="false"></asp:Label><br /><br /><br />
  <div style="color:White; font-weight:bolder;" runat="server"  id="dvlvl">New Course Syllabus ID:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNewid" runat="server" ></asp:Label></div>
  <asp:Label ID="lblExceptionID" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br /><br />
  <div><asp:Button ID="btnCreateNew" CssClass="bigbutton" OnClick="btnCreateNew_Onclick" runat="server" Text="Generate New Syllabus " /></div><br />
  </center>
      </center>
      </div></asp:Panel>
    </asp:Panel>
    </ContentTemplate></asp:UpdatePanel>
    <asp:Panel ID="panelManage" runat="server" ><br />
    <asp:UpdatePanel ID="updatepanelMange" runat="server" ><ContentTemplate>
    
    <asp:Panel ID="Panel1" CssClass="SubEdit" runat="server" >
    <img src="../../images/bxleft.png" alt="" style="float:left;" /><img src="../../images/bxright.png" alt="" style="float:right;" /><div id="bxcenter"><div style="float:right;">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../images/cls.png" 
                onclick="ibtnClose_Click" /></div>
      <br />
      <center>
      Select Syllabus Level:&nbsp;&nbsp;<asp:DropDownList ID="ddlsylbsmamage" runat="server" OnTextChanged="ddlsylabmanage_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
      
  <br /><br /><div style="color:White; font-weight:bolder;" runat="server"  id="Div1"><asp:Label ID="lblActivetext" runat="server" ></asp:Label></div><br /><br />
  <div><asp:Button ID="btnActive" CssClass="bigbutton" OnClick="btnActive_Onclick" Text="Active" runat="server"/></div><br />
  </center>
      </center>
      </div></asp:Panel>
    
    
    </ContentTemplate></asp:UpdatePanel>
    
    </asp:Panel>
    </div>
</asp:Content>

