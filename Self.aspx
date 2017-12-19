<%@ page title="" language="C#" debug="true" masterpagefile="~/MasterPageLogin.master" autoeventwireup="true" codefile="Self.aspx.cs" inherits="Self" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css"/>
<%-- code for stoping user to visit this page after back button --%>
<script type="text/javascript">
		function preventBack() { window.history.forward(); }
		setTimeout("preventBack()", 0);
		window.onunload = function () { null };
		$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");
		function alertMX(t) {
			$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center' >CLOSE</p></div>"));
			$('.msgMX').text(t); var popMargTop = ($('#boxMX').height() + 24) / 2, popMargLeft = ($('#boxMX').width() + 24) / 2;
			$('#boxMX').css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }).fadeIn(600);
			$("#boxMX").click(function () {
				$(this).remove();
				document.getElementById("<%=itemTextBox.ClientID%>").focus();
				document.getElementById("<%=itemTextBox.ClientID%>").style.borderColor = "red";
				document.getElementById("<%=itemTextBox.ClientID%>").style.borderWidth = "3px";
			});
		};
		$(function () {
			$("#dateofexpense").datepicker(({
			  minDate: '-60d', // your min date this is commented for testing.
			 maxDate: '+0d', // one week will always be 5 business day - not sure if you are including current day
				dateFormat: 'yy-mm-dd'
			}));
		});
		function checknewitembox(){
			var val = document.getElementById('<%=itemTextBox.ClientID%>').value;
			if (val == "")
			{
				//document.getElementById("<%=Itemlist.ClientID%>").disabled = true;
				alertMX('Item Field can not be blank! ');

				return false;
			}
			if (!isNaN(val)) {
				alert("Only Alphabets are allowed !");
				reset();
				document.getElementById("<%=itemTextBox.ClientID%>").focus();
				return false;
			}
			return true;
		}
	function reset() { document.getElementById("<%=itemTextBox.ClientID%>").value = ""; }
	$(function () {
		var error_priceofitem = false;
		$("#price_error").hide();
		$("#priceofitem").keydown(function (er) {
			var Key = er.keyCode;
			if (!((Key == 8) || (Key == 46) || (Key >= 35 && Key <= 40) || (Key >= 48 && Key <= 57) || (Key >= 96 && Key <= 105))) {
				$("#price_error").html("*Only Numerics are allowed!");
				$("#price_error").show();
				var error_priceofitem = true;
			}
			else {
				$("#price_error").hide();
			}
		});
	});
	$(function () {
		var error_nameofitem = false;
		$("#nameofitem_error").hide();
		$("#nameofitem").keydown(function (e) {
			if (e.shiftKey || e.ctrlKey || e.altKey) {
				$("#nameofitem_error").html("Invalid input!");
				var error_nameofitem = true;
			}
			else {
				var key = e.keyCode;
				if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
					$("#nameofitem_error").html("*Invalid input!");
					$("#nameofitem_error").show();
					var error_nameofitem = true;
				}
				else {
					$("#nameofitem_error").hide();
				}
			}
		});
	});
	$(function () {
		function pulse() {
			$('#greet').fadeIn();
			$('#greet').fadeOut();
		}
		setInterval(pulse, 1000);
	});
   $(function () {
		now = new Date
		if (now.getHours() < 5) {
			$('#greet').html("Welcome Hardeep, What are you doing up so late?");
		}
		else if (now.getHours() >= 5 && now.getHours() < 12) {
			$('#greet').html("Welcome Hardeep, Good morning!")
		}
		else if (now.getHours() >= 12 && now.getHours() <= 18) {
			$('#greet').html("Welcome Hardeep, Good Afternoon!")
		}
		else if (now.getHours() >= 18 && now.getHours() < 21) {
			$('#greet').html("Welcome Hardeep, Good Evening!")
		}
		else {
			$('#greet').html("Welcome Hardeep, Good Night!")
		}
   });
	
	$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");
	function alertMX(t) {
		$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center'>CLOSE</p></div>"));
		$('.msgMX').text(t); var popMargTop = ($('#boxMX').height() + 24) / 2, popMargLeft = ($('#boxMX').width() + 24) / 2;
		$('#boxMX').css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }).fadeIn(600);
		$("#boxMX").click(function () {
			$(this).remove();
			document.getElementById("<%=itemTextBox.ClientID%>").focus();
			
		});
	};
	$("<style type='text/css'>#itemboxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");
	function itemalertMX(t) {
		$("body").append($("<div id='itemboxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center'>CLOSE</p></div>"));
		$('.msgMX').text(t); var popMargTop = ($('#itemboxMX').height() + 24) / 2, popMargLeft = ($('#itemboxMX').width() + 24) / 2;
		$('#itemboxMX').css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }).fadeIn(600);
		$("#itemboxMX").click(function () {
			
		    <%-- document.getElementById("<%=dateofexpense.ClientID%>").disabled = false;
			document.getElementById("<%=priceofitem.ClientID%>").disabled = false;
			document.getElementById("<%=Itemlist.ClientID%>").SelectedIndex = -1;
			 document.getElementById("<%=additem.ClientID%>").disabled = true;--%>
		    
			$(this).remove();
			
		});
	};





	</script>
<style type="text/css">
#selftable {
			width: 1000px;
			border: 1px solid black;
			margin-left: 10px;
			border-radius: 5px;
			background-color: #DAA520;
			float: left;
		}
#selftable td {
				border: 1px solid #ddd;
				padding: 8px;
			}
			#selftable tr:nth-child(even) {
				background-color: #f2f2f2;
			}
			#selftable tr:hover {
				background-color: #3CB371;
			}
			#selftable tr:last-child:hover {
				background-color:#DAA520;
				 /*background-color: #f2f2f2;*/
			}
			#selftable tr#sumbt {
				height: 10px;
				width: 50px;
			}
			.button {
			/*background-color: #4CAF50;*/
			background-color: black;
			color: #fff;
			font-size: 16px;
			font-family: Calibri,Arial;
			padding: 6px 30px;
			border: none;
			/*border-bottom: 3px solid #925b08;*/
			border-radius: 5px;
			box-shadow: 0 4px #999;
			cursor: pointer;
		}
			.button:hover {
				background-color: #3e8e41;
			}
			.button:active {
				background-color: #3e8e41;
				box-shadow: 0 3px #666;
				transform: translateY(2px);
			}
			.button:focus {
				outline: none;
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
			.ui-datepicker {
			margin-left: 100px;
			margin-top: 10px;
		}
				div.ui-datepicker {
			font-size: 12px;
		}
					.reqvalidateclass {
			display: inline;
		}
		.logoutbutton:hover {
			cursor: pointer;
		}
		.auto-style17 {
			height: 432px;
		}
		.auto-style20 {
			width: 190px;
			height: 62px;
		}
		.auto-style21 {
			height: 62px;
		}
		.auto-style23 {
			width: 190px;
			height: 61px;
		}
		.auto-style24 {
			height: 61px;
		}
		.auto-style25 {
			width: 190px;
		}
		.auto-style26 {
			height: 20px;
		}
		.auto-style27 {
			height: 49px;
		}
</style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="Headercontent1" runat="Server">
<h1 id="heading">
<img id="image1" src="images/logo.png"/>
<i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite! <img id="image2" src="images/logo.png"/>
</h1>
</asp:content>
<asp:content id="Content3" contentplaceholderid="navbarhold" runat="Server">
<ul>
	<li><a href="Login.aspx" class="homepage" title="Home">Home</a></li>
	<li><a href="#this" class="selfpage" title="Self">Self</a></li>
	<li><a href="Group.aspx" class="grouppage" title="Group">Group</a></li>
	<li><a href="#this" class="logutpage" title="Logout" onserverclick="logoutevent" runat="server" causesvalidation="false">Logout</a></li>
	<li>
	<p id="greet">
		<span id="greetmsg"></span>
	</p>
	</li>
</ul>
</asp:content>
<%-- code for inserting image into self.aspx --%>
<asp:content id="Content6" contentplaceholderid="sectionimage" runat="Server">
<img src="UsersImages/Hardeep.jpg" width="240" height="220" style="border-style: double; border-radius: 5px; border-width: medium" alt="Image"/>
</asp:content>
<asp:content id="Content5" contentplaceholderid="sectionmsge" runat="Server">
<h1>Self Expense</h1>
<br/>
<p>
	<b>Here you can keep a track of all money spent by you individually!</b>
</p>
</asp:content>
<asp:content id="Content4" contentplaceholderid="detail" runat="Server">
<form id="form1" runat="server" autocomplete="off">
	
	<table id="selftable" class="auto-style17">
	<tr>
		<td class="auto-style23">
			<asp:label id="datelabel" runat="server" text="Date of Expenditure"></asp:label>
		</td>
		<td class="auto-style24">
			<asp:textbox id="dateofexpense" runat="server" clientidmode="Static" viewstatemode="Enabled" tooltip="Select Date"></asp:textbox>
			<asp:requiredfieldvalidator id="daterequired" runat="server" controltovalidate="dateofexpense" errormessage="* Field is required!" setfocusonerror="True"/>
		</td>
	</tr>
	<tr>
		<td class="auto-style25" rowspan="2">
			<asp:label id="nameofitemlabel" runat="server" text="Name of the Item"></asp:label>
		</td>
		<td class="auto-style26">
			<asp:dropdownlist id="Itemlist" runat="server" appenddatabounditems="true" onselectedindexchanged="Itemlist_SelectedIndexChanged" autopostback="true" enableviewstate="true">
			</asp:dropdownlist>
			&nbsp;<asp:comparevalidator controltovalidate="Itemlist" id="CompareValidatorlist" errormessage="Please select name of the item!" runat="server" display="Dynamic" operator="NotEqual" valuetocompare="0" type="Double" setfocusonerror="True"/>
		</td>
	</tr>
	<tr>
		<td class="auto-style27">
			 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<br/>
			<asp:textbox id="itemTextBox" runat="server" enabled="false"></asp:textbox>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="additem" enabled="false" runat="server" text="Add New Item" width="150px" onclick="itemsubmit" tooltip="Add New Item" onclientclick=" return checknewitembox()" causesvalidation="False"/>
			&nbsp;&nbsp; &nbsp;
		</td>
	</tr>
	<tr>
		<td class="auto-style20">
			<asp:label id="priceofitemlabel" runat="server" text="Price of the Item(Rs.)"></asp:label>
		</td>
		<td class="auto-style21">
			<asp:textbox id="priceofitem" runat="server"></asp:textbox>
			<asp:requiredfieldvalidator id="PriceReq" runat="server" controltovalidate="priceofitem" errormessage="* Field is required!" setfocusonerror="True"/>
			<asp:regularexpressionvalidator id="digitsReq" controltovalidate="priceofitem" runat="server" errormessage="*Only Digits allowed!" validationexpression="\d+"></asp:regularexpressionvalidator>
		</td>
	</tr>
	<tr>
		<td class="auto-style20">
			<asp:label id="Requirement" runat="server" text="Requirement"></asp:label>
		</td>
		<td class="auto-style21">
			<asp:radiobuttonlist id="requirementlist" runat="server" repeatdirection="Horizontal" borderwidth="2">
			<asp:listitem>Health</asp:listitem>
			<asp:listitem>Need</asp:listitem>
			<asp:listitem>Luxury</asp:listitem>
			<asp:listitem>Can't Say</asp:listitem>
			</asp:radiobuttonlist>
			<asp:requiredfieldvalidator id="requirementlistValidator" runat="server" controltovalidate="requirementlist" errormessage="*Field is required!"/>
		</td>
	</tr>
	<tr>
		<td class="auto-style20">
			<asp:label id="Timeofpurchase" runat="server" text="Time of Purchase"></asp:label>
		</td>
		<td class="auto-style21">
			<asp:radiobuttonlist id="purchaseList" runat="server" repeatdirection="Horizontal" borderwidth="2">
			<asp:listitem>Morning</asp:listitem>
			<asp:listitem>Afternoon</asp:listitem>
			<asp:listitem>Evening</asp:listitem>
			<asp:listitem>Night</asp:listitem>
			</asp:radiobuttonlist>
			<asp:requiredfieldvalidator id="timelistValidator" runat="server" controltovalidate="purchaseList" errormessage="*Field is required!"/>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="auto-style21">
			<div class="buttonrow">
				<asp:button id="sumbt" runat="server" text="Submit" cssclass="button" width="150px" onclick="sumbt_Click" tooltip="Submit"/>
				<div class="divider"/>
				</div>
			</td>
		</tr>
		</table>
	</form>
	</asp:content>