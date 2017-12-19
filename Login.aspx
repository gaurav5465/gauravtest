<%@ page title="" language="C#" masterpagefile="~/MasterPageLogin.master" autoeventwireup="true" codefile="Login.aspx.cs" inherits="Login" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">

	 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css"
	rel="stylesheet" type="text/css" />
<script type="text/javascript">
	$(function () {
		$("[id*=receiveclearbutton]").removeAttr("onclick");
		$("#dialog").dialog({
			modal: true,
			autoOpen: false,
			draggable: false,
			resizable: false,
			title: "Confirmation",
			width: 350,
			height: 160,
			buttons: [
			{
				id: "No",
				text: "Cancel",
				click: function () {
					$(this).dialog('close');
				}
			},
			{
				id: "Yes",
				text: "Proceed",
				click: function () {
					$("[id*=receiveclearbutton]").attr("rel", "delete");
					var person = $("[id*=personlist]");
					if (person.val() == "") {
						$(this).dialog('close');
						alertMX("Please select a Person!");
						return false;
					}
					else {
						$("[id*=receiveclearbutton]").click();
					}
				}	
			},		     
			]		 
		});
		$("[id*=receiveclearbutton]").click(function () {
			if ($(this).attr("rel") != "delete") {
				$('#dialog').dialog('open');
				return false;
			}
			else {
				__doPostBack(this.name, '');
			}
		})
	});
	function personconfirmview() {
		var e = document.getElementById("<%= personlist.ClientID %>");
		var strUser1 = e.options[e.selectedIndex].text;
		if (strUser1 == "-Select Person-") 
		{
			alertMX("Please select a Person!");
		}
	}
	//disable back button
	function DisableBackButton() {
		window.history.forward()
	}
	DisableBackButton();
	window.onload = DisableBackButton;
	window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
	window.onunload = function () { void (0) }
	function disableall() {
		$("input").prop('disabled', true);
		document.getElementById("<%= expenseview.ClientID %>").disabled = true;
		document.getElementById("<%= monthlist.ClientID %>").disabled = true;
		document.getElementById("<%= yearlist.ClientID %>").disabled = true;
	    //document.getElementById("<%= personlist.ClientID %>").val("");
		document.getElementById("<%=personlist.ClientID %>").disabled = true;
		$(function () {
			$('a').on("click", function (e) {
				e.preventDefault();
			});
		});
	}
	 function enableall() {
		$("input").prop('disabled', false);
		document.getElementById("<%= expenseview.ClientID %>").disabled = false;
		document.getElementById("<%= monthlist.ClientID %>").disabled = false;
		 document.getElementById("<%= yearlist.ClientID %>").disabled = false;
		 document.getElementById("<%=personlist.ClientID %>").disabled = false;
		$(function () {
			$('a').on("click", function (e) {
				window.location = this.href;
			});
		});
	}
	function reset() { document.getElementById("<%=dateofexpense.ClientID%>").value = ""; }		 
	$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");
	function alertMX(t) {
		$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center'>CLOSE</p></div>"));
		$('.msgMX').text(t); var popMargTop = ($('#boxMX').height() + 24) / 2, popMargLeft = ($('#boxMX').width() + 24) / 2;
		$('#boxMX').css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }).fadeIn(600);
		$("#boxMX").click(function () {
			$(this).remove();
			enableall();
		});
	};
	$(function () {
		$("#dateofexpense").datepicker(({
						//minDate: '-5d', // your min date
						maxDate: '+0d', // one week will always be 5 business day - not sure if you are including current day
						dateFormat: 'yy-mm-dd'
					}));
	});
	 $(function () {
		var error_priceofitem = false;
		$("#price_error").hide();
		$("#priceofitem").keydown(function (er) {
			var Key = er.keyCode;
			if (!((Key == 8) || (Key == 46) || (Key >= 35 && Key <= 40) || (Key >= 48 && Key <= 57) || (Key >= 96 && Key <= 105))) {
				$("#price_error").html("Only Numerics are allowed!");
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
					$("#nameofitem_error").html("Invalid input!");
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
</script>
<style type="text/css">
	#producttable {
		font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
		border-collapse: collapse;
		width: 50%;
		display: inline-block;
		float: left;
	}
	#producttable td, #producttable th {
		border: 1px solid #ddd;
		padding: 8px;
	}
#producttable tr:nth-child(even){background-color: #f2f2f2;}
#producttable tr:hover {background-color: #ddd;}
	#producttable th {
		padding-top: 12px;
		padding-bottom: 12px;
		text-align: left;
		background-color: #4CAF50;
		color: white;
	}
/*style of database table*/
 #selftable {
			width: 1000px;
			border: 1px solid black;
			margin-left: 10px;
			border-radius: 5px;
			background-color: #DAA520;
			float:left;
}
	#selftable td {
		border: 1px solid #ddd;
		padding: 8px;
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
			margin-bottom:5px;
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
	.auto-style15 {
		width: 190px;
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
	.auto-style16 {
			width: 1000px;
		}
</style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="Headercontent1" runat="Server">
<h1 id="heading">
<img id="image1" src="images/logo.png" alt="logo"/>
<i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite! <img id="image2" src="images/logo.png" alt="logo"/>
</h1>
</asp:content>
<asp:content id="Content3" contentplaceholderid="navbarhold" runat="Server">
<ul>
	<li><a href="#this" class="homepage" title="Home">Home</a></li>
	<li><a href="Self.aspx" class="selfpage" title="Self">Self</a></li>
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
<img src="UsersImages/Login.jpg" width="240" height="220" style="border-style:double;border-radius:5px;border-width:medium" alt="Image"/>
</asp:content>
<asp:content id="Content5" contentplaceholderid="sectionmsge" runat="Server">
<h1>Details about Expense!</h1>
<br/>
<p>
	<b>Here you can view of all the expenses made by you!</b>
</p>
</asp:content>
<asp:content id="Content4" contentplaceholderid="detail" runat="Server">
<%-- div for displaying confirmation --%>
<div id="dialog" style="display:none;position:center">
	 Are you sure payment has been received?<br/> All records will be deleted.
</div>
<form id="form1" runat="server" autocomplete="off">
	<table id="selftable" class="auto-style16">
	<tr id="firstrow" runat="server">
		<td class="auto-style15">
			<label>Select Expense Type</label>
		</td>
		<td>
			<asp:dropdownlist id="expenseview" runat="server" autopostback="true" onselectedindexchanged="OnSelectedIndexChanged">
			<asp:listitem text="-Select-" value=""/>
			<asp:listitem text="Daily" value="1"></asp:listitem>
			<asp:listitem text="Monthly" value="2"></asp:listitem>
			<asp:listitem text="Group" value="3"></asp:listitem>
			</asp:dropdownlist>
		</td>
	</tr>
	<tr id="secondrow" runat="server" visible="false">
		<td class="auto-style15">
			<asp:label id="datelabel" runat="server" text="Date of Expenditure"></asp:label>
		</td>
		<td>
			<asp:textbox id="dateofexpense" runat="server" clientidmode="Static" viewstatemode="Enabled" tooltip="Select Date" onmouseover='test()'></asp:textbox>
			<asp:requiredfieldvalidator id="daterequired" runat="server" controltovalidate="dateofexpense" errormessage="* Field is required!" setfocusonerror="True"/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="sumbt" runat="server" text="View" cssclass="button" width="150px" onclick="sumbt_Click"/>
		</td>
	</tr>
	<tr id="thirdrow" runat="server" visible="false">
		<td class="auto-style15">
			 Monthly Expenditure
		</td>
		<td>
			<asp:dropdownlist id="yearlist" runat="server">
			<asp:listitem text="2017" value=""></asp:listitem>
			</asp:dropdownlist>
			&nbsp;&nbsp; <asp:dropdownlist id="monthlist" runat="server" autopostback="True">
			<asp:listitem text="-Select Month-" value=""/>
			<asp:listitem text="January" value="1"></asp:listitem>
			<asp:listitem text="February" value="2"></asp:listitem>
			<asp:listitem text="March" value="3"></asp:listitem>
			<asp:listitem text="April" value="4"></asp:listitem>
			<asp:listitem text="May" value="5"></asp:listitem>
			<asp:listitem text="June" value="6"></asp:listitem>
			<asp:listitem text="July" value="7"></asp:listitem>
			<asp:listitem text="August" value="8"></asp:listitem>
			<asp:listitem text="September" value="9"></asp:listitem>
			<asp:listitem text="October" value="10"></asp:listitem>
			<asp:listitem text="November" value="11"></asp:listitem>
			<asp:listitem text="December" value="12"></asp:listitem>
			</asp:dropdownlist>
			<asp:requiredfieldvalidator id="Requiredmonth" runat="server" controltovalidate="monthlist" errormessage="* Field is required!" setfocusonerror="True"/> &nbsp;<asp:button id="monthbutton" runat="server" text="View" cssclass="button" width="150px" onclick="sumbt_month"/>
		</td>
	</tr>
	<tr id="fourthrow" runat="server" visible="false">
		<td class="auto-style15" rowspan="2">
			<asp:label id="Label1" runat="server" text="Select Person"></asp:label>
		</td>
		<td>
			<asp:dropdownlist id="personlist" runat="server" autopostback="true">
			<asp:listitem text="-Select Person-" value=""/>
			<asp:listitem text="Divya" value="1"></asp:listitem>
			<asp:listitem text="Jyotsna" value="2"></asp:listitem>
			<asp:listitem text="Keshvam" value="3"></asp:listitem>
			<asp:listitem text="Mitali" value="4"></asp:listitem>
			<asp:listitem text="Monika" value="5"></asp:listitem>
			<asp:listitem text="Navdeep" value="6"></asp:listitem>
			<asp:listitem text="Neeraj" value="7"></asp:listitem>
			<asp:listitem text="Seema" value="8"></asp:listitem>
			</asp:dropdownlist>
			<asp:requiredfieldvalidator id="Requiredperson" runat="server" controltovalidate="personlist" errormessage="* Field is required!" setfocusonerror="True"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		</td>
	</tr>
	<tr id="fifthrow" runat="server" visible="false">
		<td>
			<asp:button id="personbutton" runat="server" text="Amount to Receive" cssclass="button" width="182px" onclick="sumbt_person" causesvalidation="true" onclientclick="personconfirmview()"/>
			&nbsp;<asp:button id="givebutton" runat="server" text="Amount to Give" cssclass="button" width="182px" onclick="sumbt_person"/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="receiveclearbutton" runat="server" text="Payment Received" cssclass="button" width="182px" onclick="paymentreceived" causesvalidation="True"/>
			&nbsp;<asp:button id="giveclearbutton" runat="server" text="Payment Given" cssclass="button" width="182px" onclick="sumbt_person"/>
		</td>
	</tr>
	</table>
</form>
<asp:placeholder id="producttable" runat="server">
</asp:placeholder>
<asp:placeholder id="productotal" runat="server">
</asp:placeholder>
<asp:placeholder id="morPlaceHolder" runat="server">
</asp:placeholder>
<br/>
<asp:placeholder id="afterPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="evePlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="nightPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="mortotalPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="afttotalPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="evetotalPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="nighttotalPlaceHolder" runat="server">
</asp:placeholder>
<asp:placeholder id="monthlyplaceholder" runat="server">
</asp:placeholder>
<asp:placeholder id="persondataholder" runat="server">
</asp:placeholder>
<asp:placeholder id="persontotalholder" runat="server">
</asp:placeholder>
<asp:placeholder id="chartplaceholder" runat="server">
</asp:placeholder>
<asp:placeholder id="totalmoritemsmonth" runat="server">
</asp:placeholder>
<asp:placeholder id="afttotalitemsmonth" runat="server">
</asp:placeholder>
<asp:placeholder id="evetotalitemsmonth" runat="server">
</asp:placeholder>
<asp:placeholder id="nighttotalitemsmonth" runat="server">
</asp:placeholder>
<asp:placeholder id="totalitemsmonth" runat="server">
</asp:placeholder>
<asp:placeholder id="totatmorningmonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="totalaftmonexpense" runat="server">
<br/>
</asp:placeholder>
<asp:placeholder id="totaleveningmonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="totalnightmonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="healthmonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="needmonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="luxurymonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="notsuremonexpense" runat="server">
</asp:placeholder>
<asp:placeholder id="mostexpensiveday" runat="server">
</asp:placeholder>
<asp:placeholder id="leastexpensiveday" runat="server">
</asp:placeholder>
</asp:content>