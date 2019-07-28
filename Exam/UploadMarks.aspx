<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadMarks.aspx.cs" Inherits="Exam_UploadMarks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Result</title>
    <link href="CSS/ThemeBlue.css" rel="Stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
     <link rel="stylesheet" href="../style.css" type="text/css" charset="utf-8" />
    <link href="../Admin/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //Enumeration for messages status
        MessageStatus = {
            Success: 1,
            Information: 2,
            Warning: 3,
            Error: 4
        }
        //Enumeration for messages status class
        MessageCSS = {
            Success: "Success",
            Information: "Information",
            Warning: "Warning",
            Error: "Error"
        }
        //Global variables
        var intervalID = 0;
        var subintervalID = 0;
        var fileUpload;
        var form;
        var previousClass = '';

        //Attach to the upload click event and grab a reference to the progress bar
        function pageLoad() {
            $addHandler($get('upload'), 'click', onUploadClick);
        }

        //Register the form
        function register(form, fileUpload) {
            this.form = form;
            this.fileUpload = fileUpload;
        }
        //Start upload process
        function onUploadClick() {
            if (fileUpload.value.length > 0) {
                var filename = fileExists();
                if (filename == '') {
                    //Update the message
                    updateMessage(MessageStatus.Information, 'Initializing upload ...', '', '0 of 0 Bytes');
                    //Submit the form containing the fileupload control
                    form.submit();
                    //Set transparancy 20% to the frame and upload button
                    Sys.UI.DomElement.addCssClass($get('dvUploader'), 'StartUpload');
                    //Initialize progressbar
                    setProgress(0);
                    //Start polling to check on the progress ...
                    startProgress();
                    intervalID = window.setInterval(function () {
                        PageMethods.GetUploadStatus(function (result) {
                            if (result) {
                                setProgress(result.percentComplete);
                                // Upadte the message every 500 milisecond
                                updateMessage(MessageStatus.Information, result.message, result.fileName, result.downloadBytes);
                                if (result == 100) {
                                    //clear the interval
                                    window.clearInterval(intervalID);
                                    clearTimeout(subintervalID);
                                }
                            }
                        });
                    }, 500);
                }
                else
                    onComplete(MessageStatus.Error, "File name '<b>" + filename + "'</b> already exists in the list.", '', '0 of 0 Bytes');
            }
            else
                onComplete(MessageStatus.Warning, 'You need to select a file.', '', '0 of 0 Bytes');
        }

        //Stop progrss when file was successfully uploaded
        function onComplete(type, msg, filename, downloadBytes) 
        {
            window.clearInterval(intervalID);
            clearTimeout(subintervalID);
            updateMessage(type, msg, filename, downloadBytes);
            if (type == MessageStatus.Success) setProgress(100);
            //Set transparancy 100% to the frame and upload button
            Sys.UI.DomElement.removeCssClass($get('dvUploader'), 'StartUpload');
            //Refresh uploaded files list.
            refreshFileList('<%=hdRefereshGrid.ClientID %>');
        }

        //Update message based on status
        function updateMessage(type, message, filename, downloadBytes) {
            var _className = MessageCSS.Error;
            var _messageTemplate = $get('tblMessage');
            var _icon = $get('dvIcon');
            _icon.innerHTML = message;
            $get('dvDownload').innerHTML = downloadBytes;
            $get('dvFileName').innerHTML = filename;
            switch (type) {
                case MessageStatus.Success:
                    _className = MessageCSS.Success;
                    break;
                case MessageStatus.Information:
                    _className = MessageCSS.Information;
                    break;
                case MessageStatus.Warning:
                    _className = MessageCSS.Warning;
                    break;
                default:
                    _className = MessageCSS.Error;
                    break;
            }
            _icon.className = '';
            _messageTemplate.className = '';
            Sys.UI.DomElement.addCssClass(_icon, _className);
            Sys.UI.DomElement.addCssClass(_messageTemplate, _className);
        }

        //Refresh uploaded file list when new file was uploaded successfully
        function refreshFileList(hiddenFieldID) {
            var hiddenField = $get(hiddenFieldID);
            if (hiddenField) {
                hiddenField.value = (new Date()).getTime();
                __doPostBack(hiddenFieldID, '');
            }
        }

        //Set progressbar based on completion value
        function setProgress(completed) {
            $get('dvProgressPrcent').innerHTML = completed + '%';
            $get('dvProgress').style.width = completed + '%';
        }

        //Display mouse over and out effect of file upload list
        function eventMouseOver(_this) {
            previousClass = _this.className;
            _this.className = 'GridHoverRow';
        }
        function eventMouseOut(_this) {
            _this.className = previousClass;
        }

        //This will call every 200 milisecnd and update the progress based on value
        function startProgress() {
            var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
            increase = Number(increase) + 1;
            if (increase <= 100) {
                setProgress(increase);
                subintervalID = setTimeout("startProgress()", 200);
            }
            else {
                window.clearInterval(subintervalID);
                clearTimeout(subintervalID);
            }
        }

        //This will check whether will was already exist on the server, 
        //if file was already exists it will return file name else empty string.
        function fileExists() {
            var selectedFile = fileUpload.value.split('\\');
            var file = $get('gvNewFiles').getElementsByTagName('a');
            for (var f = 0; f < file.length; f++) {
                if (file[f].innerHTML == selectedFile[selectedFile.length - 1]) {
                    return file[f].innerHTML;
                }
            }
            return '';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
      <div id="page">
    <div id="content">
    <div id="welcome"><asp:ImageButton ID="btnNoredird" runat="server" ImageUrl="~/images/invisible.gif"  AlternateText="." TabIndex="1" /><asp:ImageButton ID="ImageButton1" TabIndex="20" runat="server" ImageUrl="~/images/home.png" ToolTip="Home" AlternateText="Home" OnClick="ibtnHome_Click" Height="20px" Width="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWelcome" runat="server" ForeColor="GrayText"></asp:Label>&nbsp;&nbsp;<asp:LinkButton ID="lbtnUserName" runat="server" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton 
            ID="lbtnLogout" runat="server" Text="Sign Out" onclick="lbtnLogout_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSettings" runat="server" Text="Settings"></asp:LinkButton><br /><div style="float:right; margin-right:30px; margin-top:30px;">
         <asp:Label ID="lbltest" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="refreshimage" runat="server" 
                ImageUrl="~/images/refresh.jpg" onclick="refreshimage_Click" /></div></div>
    <a href="#" title="ICE(I)"><img src="../images/logo.gif" alt="ICE(I)" title="ICE (I)" width="50%" /></a><br />
    <div id="redline"></div>
    <div id="usermanage" runat="server" >
    <table width="40%" style="text-align:center;"><tr><td><asp:ImageButton ID="imgbtnCreate" runat="server"  ImageUrl="~/images/createcolor.png"
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnCreate_Click"/><br />New User</td><td><asp:ImageButton ID="imgbtnRecover" runat="server" ImageUrl="~/images/user_update.png"
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnRecover_Click" /><br />Recover Password</td><td><asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/user_delete.png"
            CssClass="imgbtncreate"  AlternateText="Create New" 
            onclick="imgbtnDelete_Click" /><br />Disactive</td>
            <td><asp:ImageButton ID="imgbtnManage" runat="server" 
            CssClass="imgbtncreate"  AlternateText="Reports" 
            onclick="imgbtnManage_Click" ImageUrl="~/images/Report.png"/><br />Reports</td>
            </tr></table>
    </div>
   <hr />
   <div id="leftpanel2">
<div id="leftteg" >
<div id="panelExamCenter" runat="server" >
          <script>
              function toggle99(showHideDiv, switchImgTag) {
                  var ele = document.getElementById(showHideDiv);
                  var imageEle = document.getElementById(switchImgTag);
                  if (ele.style.display == "block") {
                      ele.style.display = "none";
                      imageEle.innerHTML = '<img src="../images/plus.png">';
                  }
                  else {
                      ele.style.display = "block";
                      imageEle.innerHTML = '<img src="../images/minus.png">';
                      imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';
                      ele = document.getElementById('Div1'); ele.style.display = "none";
                      ele = document.getElementById('Div2'); ele.style.display = "none";
                      ele = document.getElementById('Div3'); ele.style.display = "none";
                      ele = document.getElementById('Div4'); ele.style.display = "none";
                      ele = document.getElementById('Div5'); ele.style.display = "none";
                      ele = document.getElementById('Div6'); ele.style.display = "none";
                      ele = document.getElementById('Div7'); ele.style.display = "none";
                      ele = document.getElementById('Div8'); ele.style.display = "none";
                      //ele = document.getElementById('Div9'); //ele.style.display = "none";
                      ele = document.getElementById('Div10'); ele.style.display = "none";
                      ele = document.getElementById('Div11'); ele.style.display = "none";
                      ele = document.getElementById('Div12'); ele.style.display = "none";
                      ele = document.getElementById('Div13'); ele.style.display = "none";
                      ele = document.getElementById('Div14'); ele.style.display = "none";
                  }
              }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
    <a id="A0" href="javascript:toggle99('Div0', 'A0');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Exam Center</h1>
<div id="Div0" style="display: none;"> 
     <br />
   <div class="leftlist">
  <ul>
  <li><asp:LinkButton ID="lbtnCenterRegi" runat="server" Text="Register Exam Center" 
          CssClass="leftlink" onclick="lbtnCenterRegi_Click"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnViewExamCenter" runat="server" Text="View Exam Center"
           CssClass="leftlink" OnClick="lbtnViewExamCenter_Onclick" ></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnAddRooms" runat="server" Text="Add Rooms"
           CssClass="leftlink" OnClick="lbtnAddRooms_Onclick" ></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnCenterAdmin" runat="server" Text="Exam Center Admin" 
          CssClass="leftlink" onclick="lbtnCenterAdmin_Click"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnViewCenterAdminProfile" runat="server" Text="View Center Admin Profile"
           CssClass="leftlink" OnClick="lbtnViewCenterAdminProfile_OnClick" ></asp:LinkButton></li>
           <li><a href="../Reports/AttendenceCrt.aspx" target="_blank" class="leftlink">Print AttedenceSheet Report</a></li>
  </ul>
    </div>
    </div>
   </div>
  </div>
  <asp:Panel ID="panelExamSchedule" runat="server" >
   <script>
       function toggle981(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';
               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';
               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A2" href="javascript:toggle981('Div2', 'A2');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Examination Schedule</h1>
<div id="Div2" style="display: none;"> 
  <br />
   <div class="leftlist">
  <ul>
   <li><asp:LinkButton ID="lbtnExaminationDate" runat="server" Text="Create Examination Schedule" 
           CssClass="leftlink" onclick="lbtnExaminationDate_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnViewExamSchedule" runat="server" Text="View Examination Schedule" 
           CssClass="leftlink" onclick="lbtnViewExamSchedule_Click"></asp:LinkButton></li>
   </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelPaperSetter" runat="server" >
   <script>
       function toggle986(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';
               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';
               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A10" href="javascript:toggle986('Div10', 'A10');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Paper Setter Profile</h1>
<div id="Div10" style="display: none;"> 
  <br />
   <div class="leftlist">
  <ul>
   <li><asp:LinkButton ID="lbtnpaperSetter" runat="server" Text="Create Paper Setter Profile" 
           CssClass="leftlink" onclick="lbtnpaperSetter_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnViewPaperSetter" runat="server" Text="View Paper Setter Profile" 
           CssClass="leftlink" onclick="lbtnViewpaperSetter_Click"></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbtnPaperUpload" runat="server" Text="Paper Setter [Upload paper]" 
           CssClass="leftlink" onclick="lbtnpaperUpload_Click"></asp:LinkButton></li></ul>
    </div>
    </div>
    </div>
    </asp:Panel>
<asp:Panel ID="panelExamForm" runat="server" >
    <script>
        function toggle100(showHideDiv, switchImgTag) {
            var ele = document.getElementById(showHideDiv);
            var imageEle = document.getElementById(switchImgTag);
            if (ele.style.display == "block") {
                ele.style.display = "none";
                imageEle.innerHTML = '<img src="../images/plus.png">';
            }
            else {
                ele.style.display = "block";
                imageEle.innerHTML = '<img src="../images/minus.png">';

                imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
                //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
                imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

                ele = document.getElementById('Div1'); ele.style.display = "none";
                ele = document.getElementById('Div2'); ele.style.display = "none";
                ele = document.getElementById('Div0'); ele.style.display = "none";
                ele = document.getElementById('Div4'); ele.style.display = "none";
                ele = document.getElementById('Div5'); ele.style.display = "none";
                ele = document.getElementById('Div6'); ele.style.display = "none";
                ele = document.getElementById('Div7'); ele.style.display = "none";
                ele = document.getElementById('Div8'); ele.style.display = "none";
                //ele = document.getElementById('Div9'); //ele.style.display = "none";
                ele = document.getElementById('Div10'); ele.style.display = "none";
                ele = document.getElementById('Div11'); ele.style.display = "none";
                ele = document.getElementById('Div12'); ele.style.display = "none";
                ele = document.getElementById('Div13'); ele.style.display = "none";
                ele = document.getElementById('Div14'); ele.style.display = "none";
            }
        }
    </script>
  <div class="togelleft">
    <div class="headerDivImg">
    <a id="A3" href="javascript:toggle100('Div3', 'A3');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Examination Forms</h1>
<div id="Div3" style="display: none;"> 
   <br />
   <div id="leftLink">
   <ul><li><asp:LinkButton ID="lbtnExamFrom" runat="server" Text="Examination Form" 
           CssClass="leftlink" onclick="lbtnExamFrom_Click"></asp:LinkButton></li>
        <li>  <asp:LinkButton ID="lbtnEditExamForm" runat="server" Text="Re-Submit Exam Form" CssClass="leftlink" OnClick="lbtnEditExamFrom_OnClick" ></asp:LinkButton></li> 
           <li><asp:LinkButton ID="lbtnviewExamForm" runat="server" Text="View Exam Form" CssClass="leftlink" OnClick="lbtnViewExamFrom_OnClick" ></asp:LinkButton></li>
           <li><a href="../Reports/ExamFormRpt.aspx" target="_blank" class="leftlink">Print Report Via CenterCode</a></li>
           <li><a href="../Reports/CandidateListCrt.aspx" target="_blank" class="leftlink">Print Report Via City</a></li>
   </ul>
    </div>
    </div>
   </div>
   </asp:Panel>
    <asp:Panel ID="PanelRollNo" runat="server" >
   <script>
       function toggle982(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A4" href="javascript:toggle982('Div4', 'A4');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Roll No Generation</h1>
<div id="Div4" style="display: none;"> 
  <br />
   <div class="leftlist">
   <ul>
  <li><asp:LinkButton ID="lbtnRollNo" runat="server" Text="Roll No. Generation" 
           CssClass="leftlink" onclick="lbtnRollNo_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnChangeCenter" runat="server" Text="Change Exam Center" 
           CssClass="leftlink" onclick="lbtnChangeCenter_Onclick"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnViewRollNo" runat="server" Text="View RollNo List" 
           CssClass="leftlink" onclick="lbtnViewRollNo_Click"></asp:LinkButton></li>
           </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelSeatingArrangement" runat="server" >
   <script>
       function toggle988(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A1" href="javascript:toggle988('Div1', 'A1');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Seating Arrangement</h1>
<div id="Div1" style="display: none;"> 
  <br />
   <div class="leftlist">
  <ul>
  <li><asp:LinkButton ID="lbtnSeating" runat="server" Text="Seating Arrangement" 
          CssClass="leftlink" onclick="lbtnSeating_Click"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnViewSeating" runat="server" Text="View Seating Arrangement" 
          CssClass="leftlink" onclick="lbtnviewSeating_Click"></asp:LinkButton></li>
          </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelAdmitCard" runat="server" >
   <script>
       function toggle983(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A5" href="javascript:toggle983('Div5', 'A5');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Exam Admit Card</h1>
<div id="Div5" style="display: none;"> 
  <br />
   <div class="leftlist"><ul>
    <li><asp:LinkButton ID="lbtnAdmitAppli" runat="server" Text="Admit Card Application" 
           CssClass="leftlink" onclick="lbtnAdmitCardAppli_Click"></asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnAdmitCard" runat="server" Text="Admit Card Via IMID" CssClass="leftlink" onclick="lbtnAdmitCard_Click"></asp:LinkButton></li>
   <li><asp:LinkButton ID="lbtnAdmitCardGen" runat="server" Text="Admit Card Approval" CssClass="leftlink" onclick="lbtnAdmitCardGen_Click"></asp:LinkButton></li>
   <li><a href="../Reports/Exam/AdmitCardCrt.aspx" target="_blank" class="leftlink">Print Admit Cards</a></li>
  </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelMarksFeed" runat="server" >
   <script>
       function toggle984(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A6" href="javascript:toggle984('Div6', 'A6');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Marks Feed</h1>
<div id="Div6" style="display: none;"> 
  <br />
   <div class="leftlist">
   <ul>
   <li><asp:LinkButton ID="lbtnMarksFeed" runat="server" Text="Marks Feed" 
           CssClass="leftlink" onclick="lbtnMarksFeed_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnMarksUpload" runat="server" Text="Upload Result" 
           CssClass="leftlink" onclick="lbtnMarksUPload_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnRecheckingMarks" runat="server" Text="Rechecking Marks Entry" 
          CssClass="leftlink" onclick="lbtnRecheckingMarks_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnApproveMarksEntry" runat="server" Text="Approve Marks Entry" 
          CssClass="leftlink" onclick="lbtnApproveMarksEntry_Click"></asp:LinkButton></li>
           <li><asp:LinkButton ID="lbtnviewMarksFeed" runat="server" Text="View Marks Details" 
           CssClass="leftlink" onclick="lbtnViewMarksFeed_Click"></asp:LinkButton></li>
   </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
<asp:Panel ID="panelCertificate" runat="server" >
   <script>
       function toggle98(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A13" href="javascript:toggle98('Div13', 'A13');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Marksheets</h1>
<div id="Div13" style="display: none;"> 
  <br />
   <div class="leftlist">
  <ul><li><asp:LinkButton ID="lbtnMarksAppli" runat="server" Text="Final Marksheet Application" 
          CssClass="leftlink" onclick="lbtnMarksAppli_Click"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnViewMarksDetails" runat="server" Text="Marks Statement" 
          CssClass="leftlink" onclick="lbtnViewMarkDetails_Click"></asp:LinkButton></li>
          <li><a href="../Reports/Exam/MarksStatementsCrt.aspx " target="_blank" class="leftlink">Examination Marks Statements</a></li>
  </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelUFM" runat="server" >
   <script>
       function toggle9811(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';
               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';
               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A7" href="javascript:toggle9811('Div7', 'A7');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>UFM</h1>
<div id="Div7" style="display:none;"> 
  <br />
   <div class="leftlist">
  <ul>
  <li><asp:LinkButton ID="lbtnUFMSubmit" runat="server" Text="Submit UFM" 
          CssClass="leftlink" onclick="lbtnUFMSubmit_Onclick"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnUFMView" runat="server" Text="View UFM Details" 
          CssClass="leftlink" onclick="lbtnUFMdeetails_Onclick"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnUFMManage" runat="server" Text="Manage UFM" 
          CssClass="leftlink" onclick="lbtnUFMManage_OnClick"></asp:LinkButton></li>
          </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelRechecking" runat="server" >
   <script>
       function toggle9812(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A8" href="javascript:toggle9812('Div8', 'A8');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Rechecking Forms</h1>
<div id="Div8" style="display:none;"> 
  <br />
   <div class="leftlist">
  <ul>
  <li><asp:LinkButton ID="lbtnRecheckingForm" runat="server" Text="Submit Rechecking Form" 
          CssClass="leftlink" onclick="lbtnSubmitRechecking_Onclick"></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnViewRechecking" runat="server" Text="View Rechecking Form" 
          CssClass="leftlink" onclick="lbtnViewRechecking_Onclick"></asp:LinkButton></li>
          </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelExamPaper" runat="server" Visible="false" >
   <script>
       function toggle985(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
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
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A9" href="javascript:toggle985('Div9', 'A9');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Examination Paper</h1>
<div id="Div9" style="display: none;"> 
  <br />
   <div class="leftlist">    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelDegree" runat="server" >
   <script>
       function toggle987(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';

               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A11" href="javascript:toggle987('Div11', 'A11');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Certificates & Degree</h1>
<div id="Div11" style="display: none;"> 
    
  <br />
   <div class="leftlist">
  <ul><li><asp:LinkButton ID="lbtnprovisional" runat="server" Text="Provisional Certificate Application" CssClass="leftlink" onclick="lbtnProvisional_Click" ></asp:LinkButton></li>
         <li><asp:LinkButton ID="lbtnViewProvisional" runat="server" Text="View Provisional Certificate Status" CssClass="leftlink" onclick="lbtnViewProvisional_Click"></asp:LinkButton></li>
  </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelUpdate" runat="server" >
   <script>
       function toggle989(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';
               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A12'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div12'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A14" href="javascript:toggle989('Div14', 'A14');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Old Question Set</h1>
<div id="Div14" style="display: none;"> 
    
  <br />
   <div class="leftlist">
  <ul><li><asp:LinkButton ID="lbtnOldQuestionSet" runat="server" Text="Old Question Set Application" 
          CssClass="leftlink" onclick="lbtnOldPapers_Click"></asp:LinkButton></li>
  </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="panelMarking" runat="server">
   <script>
       function toggle9810(showHideDiv, switchImgTag) {
           var ele = document.getElementById(showHideDiv);
           var imageEle = document.getElementById(switchImgTag);
           if (ele.style.display == "block") {
               ele.style.display = "none";
               imageEle.innerHTML = '<img src="../images/plus.png">';
           }
           else {
               ele.style.display = "block";
               imageEle.innerHTML = '<img src="../images/minus.png">';
               imageEle = document.getElementById('A1'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A2'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A3'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A4'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A5'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A6'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A7'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A8'); imageEle.innerHTML = '<img src="../images/plus.png">';
               //                      imageEle = document.getElementById('A9');imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A10'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A11'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A0'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A13'); imageEle.innerHTML = '<img src="../images/plus.png">';
               imageEle = document.getElementById('A14'); imageEle.innerHTML = '<img src="../images/plus.png">';

               ele = document.getElementById('Div1'); ele.style.display = "none";
               ele = document.getElementById('Div2'); ele.style.display = "none";
               ele = document.getElementById('Div3'); ele.style.display = "none";
               ele = document.getElementById('Div4'); ele.style.display = "none";
               ele = document.getElementById('Div5'); ele.style.display = "none";
               ele = document.getElementById('Div6'); ele.style.display = "none";
               ele = document.getElementById('Div7'); ele.style.display = "none";
               ele = document.getElementById('Div8'); ele.style.display = "none";
               //ele = document.getElementById('Div9'); //ele.style.display = "none";
               ele = document.getElementById('Div10'); ele.style.display = "none";
               ele = document.getElementById('Div11'); ele.style.display = "none";
               ele = document.getElementById('Div0'); ele.style.display = "none";
               ele = document.getElementById('Div13'); ele.style.display = "none";
               ele = document.getElementById('Div14'); ele.style.display = "none";
           }
       }
    </script>
   <div class="togelleft">
    <div class="headerDivImg">
        <a id="A12" href="javascript:toggle9810('Div12', 'A12');"><img src="../images/plus.png" alt="Show"></a>
</div><h1>Student Promotion</h1>
<div id="Div12" style="display: none;"> 
    
  <br />
   <div class="leftlist">
  <ul>
  <li><asp:LinkButton ID="lbtnApproveFinalMarkshit" runat="server" Text="Approve Final Marksheet" CssClass="leftlink" OnClick="lbtnApproveFinalMarksheet_Onclick"></asp:LinkButton></li>
  <li><asp:LinkButton ID="lbtnApproveMarksheets" runat="server" Text="Student Promotion. "  Visible="false"  CssClass="leftlink" onclick="lbtnApproveMarksheets_Click"></asp:LinkButton></li>
  </ul>
    </div>
    </div>
    </div>
    </asp:Panel>
</div>
</div>
    <div id="redirect" runat="server">	
<table><tr><td><asp:LinkButton ID="lblHomeRedirect" runat="server" onclick="lblHomeRedirect_Click" Text="Home" CssClass="redirecttab"></asp:LinkButton></td><td>
        <asp:LinkButton ID="lbtnNext1Redirect" runat="server" Text="Examination" CssClass="redirecttab"
            onclick="lbtnNext1Redirect_Click" ></asp:LinkButton> </td><td><asp:Label ID="lblPageName" runat="server" Text="Upload Result" CssClass="redirecttabhome"></asp:Label></td></tr></table>
            </div>
<div id="rightpanel2">
<div class="fromRegisterlbl"><h1 style="float:right; margin-right:50px;"><asp:Label ID="lblEnrolment" runat="server" ></asp:Label></h1><h1>Upload Result</h1></div>
        <table width="100%" cellpadding="5" cellspacing="5" border="0">
        <tr>
            <td>
                <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr class="ContainerHeader">
                        <td>
                            Upload Result
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerMargin">
                            <table class="Container" cellpadding="0" cellspacing="4" width="100%" border="0">
                                <tr>
                                    <td>
                                        <div id="dvUploader">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr><td>select Result Data:</td>
                                                    <td>
                                                        <iframe id="uploadFrame" frameborder="0" height="25" width="200" scrolling="no" src="UploadEngine.aspx">
                                                        </iframe>
                                                    </td>
                                                    <td>
                                                        <input id="upload" type="button" value="Save File" class="btnsmall" /> &nbsp;&nbsp;&nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblMessage" cellpadding="4" cellspacing="4" class="Information" border="0">
                                            <tr>
                                                <td style="text-align: left" colspan="2">
                                                    <div id="dvIcon" class="Information">
                                                        Please select a file to upload
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="2" width="100%" border="0">
                                            <tr>
                                                <td style="width: 100px; text-align: left">
                                                    Progress
                                                </td>
                                                <td style="width: auto">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <div id="dvProgressContainer">
                                                                    <div id="dvProgress">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div id="dvProgressPrcent">
                                                                    0%
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    Download Bytes
                                                </td>
                                                <td align="right">
                                                    <div id="dvDownload">
                                                        Bytes
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    File Name
                                                </td>
                                                <td align="right">
                                                    <div id="dvFileName">
                                                        FileName
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr class="ContainerHeader">
                        <td>
                            List of uploaded files
                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerMargin">
                            <asp:UpdatePanel runat="server" ID="upFiles" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdRefereshGrid" runat="server" OnValueChanged="hdRefereshGrid_ValueChanged" />
                                    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    <table class="Container" cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr class="GridHeader">
                                            <td class="Separator" style="width: 5%;" align="right">
                                            </td>
                                            <td class="Separator" style="width: 60%">
                                                File
                                            </td>
                                            <td class="Separator" style="width: 15%" align="Center">
                                                Size
                                            </td>
                                            <td class="Separator" style="width: 5%">
                                                Delete</td>
                                            <td class="Separator" style="width: 5%">
                                                Upload</td>

                                                <td  style="width: 5%">
                                                Update</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <div style="height: 80px; overflow: auto;">
                                                    <asp:GridView DataKeyNames="Name" ID="gvNewFiles" AllowPaging="false" runat="server"
                                                        PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" Width="101%"
                                                        CellPadding="0" BorderWidth="0" GridLines="None" ShowHeader="false" OnRowCommand="gvNewFiles_RowCommand"
                                                        OnRowDataBound="gvNewFiles_RowDataBound" 
                                                        onselectedindexchanged="gvNewFiles_SelectedIndexChanged">
                                                        <AlternatingRowStyle CssClass="GridAlternate" />
                                                        <PagerStyle HorizontalAlign="Center" />
                                                        <RowStyle CssClass="GridNormalRow" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td class="GridNumberRow" style="width: 5%;" align="right">
                                                                                <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                                            </td>
                                                                            <td style="width: 60%; padding-left: 2px;" align="left">
                                                                                <asp:LinkButton ToolTip='<%# String.Format("Download {0}",Eval("Name")) %>' runat="server"
                                                                                    ID="lbtnFiles" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("Name") %>'
                                                                                    CommandName="downloadFile"></asp:LinkButton>
                                                                            </td>
                                                                            <td style="width: 15%" align="right">
                                                                                <%#Eval("ConvertedSize")%>
                                                                            </td>
                                                                            <td style="width: 5%" align="center">
                                                                                <asp:ImageButton Width="10" runat="server" ImageUrl="~/Images/Grid_ActionDelete.gif"
                                                                                    ID="imgBtnDel" CommandName="deleteFile" CommandArgument='<%#Eval("Name") %>'
                                                                                    AlternateText="Delete" ToolTip="Delete File" />
                                                                            </td>
                                                                             <td style="width: 5%" align="center">
                                                                                <asp:ImageButton Width="10" runat="server" ImageUrl="~/Images/upload.jpg"
                                                                                    ID="imgBtnup" CommandName="uploadFile" CommandArgument='<%#Eval("Name") %>'
                                                                                    AlternateText="upload" ToolTip="upload File" />
                                                                            </td>
                                                                        
                                                                        <td style="width: 5%" align="center">
                                                                                <asp:ImageButton Width="10" runat="server" ImageUrl="~/Images/GreenUpArrow.png"
                                                                                    ID="imgBtnupdate" CommandName="Update" CommandArgument='<%#Eval("Name") %>'
                                                                                    AlternateText="update" ToolTip="update File" />
                                                                            </td>
                                                                        
                                                                        
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="GridEmptyRow" />
                                                        <EmptyDataTemplate>
                                                            <span>No file uploaded</span>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="GridFooter">
                                            <td colspan="6">
                                                <div style="float: left">
                                                    Total Files:
                                                    <%= gvNewFiles.Rows.Count  %>
                                                </div>
                                                <div style="float: right">
                                                    Total Size:
                                                    <asp:Label runat="server" ID="lblTotalSize" Text="0 K"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvNewFiles" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td>
        <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr class="ContainerHeader">
                        <td>Delete Result                        </td>
                    </tr>
                    <tr>
                        <td class="ContainerMargin">
                               <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr><td>Exam Session:&nbsp;</td><td><asp:DropDownList ID="ddlSession" 
                                                    runat="server" CssClass="txtbox" 
                                                    onselectedindexchanged="ddlSession_SelectedIndexChanged" ><asp:ListItem Text="Summer Examination" Value="Sum"></asp:ListItem><asp:ListItem Text="Winter Examination" Value="Win"></asp:ListItem></asp:DropDownList></td><td>Year:</td><td> <asp:TextBox ID="txtYear" AutoPostBack="true"  OnTextChanged="txtYear_TextChanged" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox></td><td>
                                               SeasionId: 
                                                <asp:Label ID="SeasonId" runat="server" ForeColor="#FF3300"></asp:Label></td></tr>
<tr><td>Course:</td><td><asp:DropDownList ID="ddlCourseDelete" runat="server" 
        Width="150px" CssClass="txtbox" ><asp:ListItem Value="Architecture" Text="Architectural Engineering"></asp:ListItem><asp:ListItem Value="Civil" Text="Civil Engineering" /></asp:DropDownList></td>
        <td>Section/Part:</td><td><asp:DropDownList ID="ddlPartDelete" runat="server" CssClass="txtbox"  Width="80px" >
    <asp:ListItem Text="Part I" Value="PartI" /><asp:ListItem Value="PartII" 
        Text="Part II" /><asp:ListItem Value="SectionA" Text="Section A" />
    <asp:ListItem Value="SectionB" Text="Section B" ></asp:ListItem></asp:DropDownList></td></tr> <br /><br />
                                                <tr>
                                                    <td colspan="6" align="center">
                                                      <asp:Button ID="btnPromote" runat="server" Text="Update result" OnClientClick="return confirm('Confirm Update Result ? ')" OnClick="btnPromoteStudent_click" CssClass="btnsmall" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                                               
                                                      <asp:Button ID="btnDelete" runat="server" Text="Delete Result" OnClientClick="return confirm('Confirm Delete Result ? ')" OnClick="btnDelete_click" CssClass="btnsmall" /></td>
                                                </tr>
                                            </table>     
                        </td>
                    </tr>
                </table>
        </td></tr>
    </table>
    <asp:GridView ID="GridExamForms" runat="server" 
        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"  AutoGenerateColumns="true"
        CellPadding="1" CellSpacing="1" PageSize="50"
        GridLines="Horizontal" HorizontalAlign="Center" Width="100%" 
                         >
        <Columns>
        </Columns>
        <EmptyDataTemplate><center> Duplicate Record Not found !</center></EmptyDataTemplate>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
    <div id="reporttitle"><b>Instructions:</b></div>
    <ul><li>Uploaded File  should be<b> Microsoft Office Excel 2007/2003</b>. with extension of file (.xls or .xlsx) </li>
    <li>First row of data file is considering as header of datatable.</li>
    <li>Sheet Name of Excel file sholud be <b>Sheet1$. </b></li>
    <li>Once deleted result can not be recovered.</li>
    <li>Set Marks Status <b>Pass/Fail</b> according to subject's passing criteria.</li>
    <li>Order of Data field should be like this:<b></b></li>
    <li></li>
    </ul>
    <div style="width:100%; overflow:scroll;"><img src="../images/resultuploadformat.png" alt="Data Formt" /></div>
    </div>
           <br /><br />
    </div><br />
    </div>
    <!-- footer -->
    <div class="footer">
     <br /><br /><center><table><tr><td><a href="#" title="About ICE (I)">About ICE(I)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Home</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Term & Condition</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" title="About ICE (I)">Help & Support</a></td></tr></table></center>
	<center>© Copyright The Institution of Civil Engineers (India). All Rights Reserved</center>
	</div>
    </form>
</body>
</html>