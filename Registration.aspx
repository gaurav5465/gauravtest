<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRegister.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %> <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> <script type="text/javascript" > function preventBack() {
	window.history.forward();
}

setTimeout("preventBack()", 0);
window.onunload=function() {
	null
}

;
</script> <%-- code for displaying message use has registered --%> <script type="text/javascript"> $("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head").dialog( {
	modal: true
}

);
function alertMX(t) {
	$("body").append($("<div id='boxMX'><p class='msgMX'></p><p>OK</p></div>"));
	$('.msgMX').text(t);
	var popMargTop=($('#boxMX').height() + 24) / 2,
	popMargLeft=($('#boxMX').width() + 24) / 2;
	$('#boxMX').css( {
		'margin-top': -popMargTop, 'margin-left': -popMargLeft
	}
	).fadeIn(600);
	$("#boxMX").click(function () {
		window.location.replace('Default.aspx');
	}
	);
}

;
</script> <script src="jquery-ui-1.12.1.custom/jquery/jquery.js"></script> <script src="jquery-3.1.0.js"></script> <script src="jquery-ui-1.12.1.custom/jquery-ui.js"></script> <script type="text/javascript"> $(function () {
	$("#dob").datepicker(( {
		changeMonth: true, changeYear: true, yearRange: '-29:+0', dateFormat: 'yy-mm-dd'
	}
	));
}

);
$(function () {
	var error_mob=false;
	$("#moberror").hide();
	$("#mob").keydown(function (er) {
		var Key=er.keyCode;
		if (!((Key==8) || (Key==46) || (Key >=35 && Key <=40) || (Key >=48 && Key <=57) || (Key >=96 && Key <=105))) {
			$("#moberror").html("Only Numerics are allowed!");
			$("#moberror").show();
			var error_mob=true;
		}
		else {
			$("#moberror").hide();
		}
	}
	);
}

);
function validation() {
	//var error_mob = false;
	//$("#moberror").hide();
	var pass=$("<%=mob.ClientID %>").val().length;
	if((pass!=10)) {
		//  $("#moberror").html("10 digits are required!");
		// $("#moberror").show();
		alert("check mobile no");
	}
}

;
function passcheck() {
	var error_pass=false;
	$("#passerror").hide();
	var pass=/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*] {
		7,
		15
	}
	$/;
	if (!passwd.value.match(pass)) {
		$("#passerror").html("Min length 7 char including number and special character!");
		$("#passerror").show();
		var error_pass=true;
	}
	else {
		$("#passerror").hide();
	}
}

;
function cnfpasscheck() {
	var error_cnfpass=false;
	$("#cnfpasserror").hide();
	var pass=$('#passwd').val();
	var cnfpass=$('#cnfpasswd').val();
	if (!(pass=cnfpass)) {
		$("#cnfpasserror").html("Password doesn't match!");
		$("#cnfpasserror").show();
		var error_cnfpass=true;
	}
	else {
		$("#cnfpasserror").hide();
	}
}

;
$(function () {
	function pulse() {
		$('#noteanimate').fadeIn();
		$('#noteanimate').fadeOut();
	}
	setInterval(pulse, 1000);
}

);
</script> <style type="text/css"> #regtable {
	/*width: 850px;*/
	border: 1px solid black;
	margin-left: auto;
	margin-right: auto;
	border-radius: 5px;
	background-color: #DAA520;
}

.noteregister #note {
	color: black;
	font-size: small;
	font-family: Algerian, Arial, sans-serif;
	text-align: center;
}

.noteregister {
	width: 500px;
	height: 50px;
	background-color: papayawhip;
	box-shadow: 5px 5px 5px #DC143C;
	border-radius: 5px;
}

#regtable td {
	border: 1px solid #ddd;
	/*padding: 8px;*/
}

#regtable tr:nth-child(even) {
	background-color: #DCDCDC;
}

#regtable tr:last-child {
	background-color: #A9A9A9;
}

#regtable tr:hover {
	background-color: #3CB371;
}

#regtable tr:last-child:hover {
	background-color: #A9A9A9;
}

#regtable tr#sumbt {
	height: 10px;
	width: 50px;
}

.button {
	background-color: black;
	color: #fff;
	font-size: 16px;
	font-family: Calibri, Arial;
	padding: 6px 30px;
	border: none;
	/*border-bottom: 3px solid #925b08;*/
	border-radius: 5px;
	box-shadow: 0 4px #999;
	cursor: pointer;
}

.button:hover {
	background-color: #228B22
}

.button:active {
	background-color: #3e8e41;
	box-shadow: 0 3px #666;
	transform: translateY(2px);
}


/*.button:hover{
	background:#32CD32;                /*925b08;*/


/* border-bottom: 3px solid #f90;}*/

.button:focus {
	outline: none;
}

div.ui-datepicker {
	font-size: 10px;
}

.divider {
	width: 180px;
	height: auto;
	display: inline-block;
}

.buttonrow {
	text-align: center;
	margin-left: auto;
	margin-right: auto;
}

.auto-style21 {
	height: 374px;
	width: 838px;
}

.auto-style23 {
	height: 101px;
}

.auto-style30 {
	width: 165px;
	text-align: left;
}

.auto-style50 {
	height: 54px;
	text-align: left;
}

.auto-style51 {
	height: 53px;
}

.auto-style52 {
	width: 165px;
	height: 54px;
	text-align: left;
}

.auto-style54 {
	height: 54px;
}

.auto-style55 {
	margin-left: 6px;
}

</style> </asp:Content> <asp:Content ID="Content2" ContentPlaceHolderID="Headercontent1" Runat="Server"> <h1 id="heading"> <img id="image1" src="images/logo.png"/> <i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite! <img id="image2" src="images/logo.png"/> </h1> </asp:Content> <asp:Content ID="Content3" ContentPlaceHolderID="navigationbar" Runat="Server"> <ul> <li><a href="Default.aspx" title="Home">Home</a></li> </ul> </asp:Content> <asp:Content ID="Content4" ContentPlaceHolderID="register" Runat="Server"> <form id="form1" runat="server" autocomplete="off"> <table id="regtable" class="auto-style21"> <tr> <td class="auto-style30"> &nbsp;
Username <br /> </td> <td class="auto-style51"> &nbsp;
<asp:TextBox ID="Username" runat="server" placeholder="Enter Username" ToolTip="Enter Username" AutoCompleteType="Disabled"></asp:TextBox> <asp:RequiredFieldValidator id="usernameReq" runat="server" ControlToValidate="Username" ErrorMessage="*Field is required!" SetFocusOnError="True" /> <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="Username" ID="RegularExpressionValidatorname" ValidationExpression="^[\s\S]{8,}$" runat="server" ErrorMessage="Minimum 8 characters required!"></asp:RegularExpressionValidator> </td> </tr> <tr> <td class="auto-style52"> &nbsp;
Date of Birth</td> <td class="auto-style50"> &nbsp;
<asp:TextBox ID="dob" runat="server" ToolTip="Enter DOB" ClientIDMode="Static" ViewStateMode="Enabled"></asp:TextBox> <asp:RequiredFieldValidator id="dobReq" runat="server" ControlToValidate="dob" ErrorMessage="*Field is required!" SetFocusOnError="True" /> <%-- <asp:RegularExpressionValidator ID="dateformat" runat="server" ErrorMessage="Invalid date!" ControlToValidate="dob" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" SetFocusOnError="True" />--%> </td> </tr> <tr> <td class="auto-style52"> &nbsp;
Mobile(+91)</td> <td class="auto-style50"> &nbsp;
<asp:TextBox ID="mob" runat="server" placeholder="Mobile No" ToolTip="Enter Mobile" AutoCompleteType="Disabled"></asp:TextBox> <asp:RequiredFieldValidator id="mobReq" runat="server" ControlToValidate="mob" ErrorMessage="*Field is required!" SetFocusOnError="True" /> <asp:RegularExpressionValidator ID="digitsReq" ControlToValidate="mob" runat="server" ErrorMessage="*Only Numerics allowed!" ValidationExpression="\d+"></asp:RegularExpressionValidator> &nbsp;
</td> </tr> <tr> <td class="auto-style52"> &nbsp;
Password</td> <td class="auto-style54"> &nbsp;
<br /><asp:TextBox ID="passwd" runat="server" TextMode="Password" placeholder="Enter Password" ToolTip="Enter Password" AutoCompleteType="Disabled" CssClass="auto-style55"></asp:TextBox> <asp:RequiredFieldValidator id="passwdReq" runat="server" ControlToValidate="passwd" ErrorMessage="*Field is required!" SetFocusOnError="True" /> <asp:RegularExpressionValidator ID="passwdlength" runat="server" ErrorMessage="*Password length must be between 7 to 10 characters!" ControlToValidate="passwd" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{7,10}$" SetFocusOnError="True" /> &nbsp;
</td> </tr> <tr> <td class="auto-style52"> &nbsp;
Confirm Password</td> <td class="auto-style54"> &nbsp;
<asp:TextBox ID="cnfpasswd" runat="server" placeholder="Confirm Password" TextMode="Password" ToolTip="Confirm Password" AutoCompleteType="Disabled"></asp:TextBox> <asp:RequiredFieldValidator id="cnfpasswdReq" runat="server" ControlToValidate="cnfpasswd" ErrorMessage="*Field is required!" SetFocusOnError="True" /> <asp:CompareValidator id="comparePasswords" runat="server" ControlToCompare="passwd" ControlToValidate="cnfpasswd" ErrorMessage="*Your passwords do not match up!" Display="Dynamic" /> </td> </tr> <tr> <td colspan="2" class="auto-style23"> <div class="buttonrow"> <asp:Button ID="sumbt" runat="server" Text="Register" CssClass="button" Width="150px" OnClick="sumbt_Click" OnClientClick="validatation();" /> </div> </td> </tr> </table> </form> </asp:Content> <asp:Content ID="Content5" ContentPlaceHolderID="registernote" Runat="Server"> <p id="note"><u id="noteanimate">Note:</u><br />Only a few specified members can register on this site! </p> </asp:Content>