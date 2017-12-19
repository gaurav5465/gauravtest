<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" CodeFile="Group.aspx.cs" Inherits="Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<%--<script src="jquery-ui-1.12.1.custom/jquery/jquery.js"></script>
<script src="jquery-ui-1.12.1.custom/jquery-ui.js"></script>--%>
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
	<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
	<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		function preventBack() {
			window.history.forward();
		}
		setTimeout("preventBack()", 0);
		window.onunload = function () {
			null
		};

		function validateCheckBoxList() {
			var isAnyCheckBoxChecked = false;
			// ::: Step-1 & 2 ::: Let's get all the CheckBoxes inside the CheckBoxList.
			var checkBoxes = document.getElementById("<%= personsinvolved.ClientID %>").getElementsByTagName("input");
					// ::: Step-3 ::: Now let's Loop through the Children.
					for (var i = 0; i < checkBoxes.length; i++) {
						if (checkBoxes[i].type == "checkbox") {
							if (checkBoxes[i].checked) {
								// ::: Step-4 ::: If current CheckBox is checked, then show alert.
								// Break the Loop.
								isAnyCheckBoxChecked = true;
								// alert("Atleast one CheckBox is checked");
								break;
							}
						}
					}
					// ::: Step-5 ::: Check if any CheckBox is checked or not.
					// Show alert and return accordingly.
					if (!isAnyCheckBoxChecked) {
						//alert("No CheckBox is Checked.");
						alertMX("Please select a Person!");
						disableall();
					}
					return isAnyCheckBoxChecked;
				}

				$(function () {

					$("#dateofexpense").datepicker(({
						minDate: '-5d', // your min date
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
						} else {
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
						} else {
							var key = e.keyCode;
							if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
								$("#nameofitem_error").html("Invalid input!");
								$("#nameofitem_error").show();
								var error_nameofitem = true;
							} else {
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

					} else if (now.getHours() >= 5 && now.getHours() < 12) {
						$('#greet').html("Welcome Hardeep, Good morning!")
					} else if (now.getHours() >= 12 && now.getHours() <= 18) {
						$('#greet').html("Welcome Hardeep, Good Afternoon!")
					} else if (now.getHours() >= 18 && now.getHours() < 21) {
						$('#greet').html("Welcome Hardeep, Good Evening!")
					} else {
						$('#greet').html("Welcome Hardeep, Good Night!")
					}
				});

				function disableall() {

					$("input").prop('disabled', true);
					document.getElementById("<%= dateofexpense.ClientID %>").disabled = true;
					document.getElementById("<%= nameofitem.ClientID %>").disabled = true;
					document.getElementById("<%= priceofitem.ClientID %>").disabled = true;
					document.getElementById("<%= personsinvolved.ClientID %>").disabled = true;
					$(function () {
						$('a').on("click", function (e) {
							e.preventDefault();
						});
					});
				}

				function enableall() {

					$("input").prop('disabled', false);
					document.getElementById("<%= dateofexpense.ClientID %>").disabled = false;
					document.getElementById("<%= nameofitem.ClientID %>").disabled = false;
					document.getElementById("<%= priceofitem.ClientID %>").disabled = false;
					document.getElementById("<%= personsinvolved.ClientID %>").disabled = false;
					$(function () {
						$('a').on("click", function (e) {
							window.location = this.href;
						});
					});
				}

				$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");

				function alertMX(t) {
					$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center'>CLOSE</p></div>"));
					$('.msgMX').text(t);
					var popMargTop = ($('#boxMX').height() + 24) / 2,
						popMargLeft = ($('#boxMX').width() + 24) / 2;
					$('#boxMX').css({
						'margin-top': -popMargTop,
						'margin-left': -popMargLeft
					}).fadeIn(600);
					$("#boxMX").click(function () {
						$(this).remove();
						enableall();
					});
				};

				$('form2').submit(function (e) {
					if ($("input:checked").length == 0) {
						alert('please checked atleast one');
					}
				});
	</script>

	<style type="text/css">
		#grouptable {
			/*width: 850px;*/
			border: 1px solid black;
			width: 1000px;
			line-height: inherit;
			margin-left: 10px;
			border-radius: 5px;
			background-color: #DAA520;
		}

			#grouptable td {
				border: 1px solid #ddd;
				padding: 8px;
			}

			#grouptable tr:nth-child(even) {
				background-color: #f2f2f2;
			}

			#grouptable tr:hover {
				background-color: #3CB371;
			}

			#grouptable tr:last-child:hover {
				background-color: #DAA520;
			}

			#grouptable tr#sumbt {
				height: 10px;
				width: 50px;
			}

		.button {
			/*background-color: #4CAF50;*/
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

		.auto-style16 {
			height: 344px;
		}

		.auto-style24 {
			width: 238px;
			height: 189px;
		}

		.auto-style27 {
			margin-top: 0px;
		}

		.auto-style28 {
			width: 190px;
			height: 69px;
		}

		.auto-style29 {
			height: 69px;
		}

		.auto-style32 {
			width: 190px;
			height: 68px;
		}

		.auto-style33 {
			height: 68px;
		}
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Headercontent1" runat="Server">

	<h1 id="heading">
		<img id="image1" src="images/logo.png" alt="logo" />
		<i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite!
	  <img id="image2" src="images/logo.png" alt="logo" />
	</h1>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="navbarhold" runat="Server">

	<ul>
		<li><a href="Login.aspx" title="Home">Home</a></li>
		<li><a href="Self.aspx" title="Self">Self</a></li>
		<li><a href="#this" title="Group">Group</a></li>
		<li><a href="#this" class="logutpage" title="Logout" onserverclick="logoutevent" runat="server" causesvalidation="false">Logout</a></li>
		<li>
			<p id="greet"><span id="greetmsg"></span></p>
		</li>
	</ul>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="sectionmsge" runat="Server">

	<h1 style="font-size: 2em; color: black; margin-left: 13px; text-shadow: 1px 1px #ff0000; text-align: center">Group Expense</h1>
	<br />
	<p id="grupmsg" class="auto-style24"><b>Here you can keep a track of all money spent by you in a group!</b></p>

</asp:Content>

<%-- code for inserting image into self.aspx --%>
<asp:Content ID="Content6" ContentPlaceHolderID="sectionimage" runat="Server">

	<img src="UsersImages/Group.jpg" width="240" height="220" style="border-style: double; border-radius: 5px; border-width: medium" alt="Image" />

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="detail" runat="Server">
	<form id="form2" runat="server" autocomplete="off">
		<table id="grouptable" class="auto-style16">
			<tr>
				<td class="auto-style32">
					<asp:Label ID="datelabel" runat="server" Text="Date of Expenditure"></asp:Label>
				</td>
				<td class="auto-style33">
					<asp:TextBox ID="dateofexpense" runat="server" ClientIDMode="Static" ViewStateMode="Enabled" ToolTip="Select Date"></asp:TextBox>
					<asp:RequiredFieldValidator ID="daterequired" runat="server" ControlToValidate="dateofexpense" ErrorMessage="* Field is required!" SetFocusOnError="True" />
				</td>
			</tr>
			<tr>
				<td class="auto-style28">
					<asp:Label ID="nameofitemlabel" runat="server" Text="Name of the Item"></asp:Label>
				</td>
				<td class="auto-style29">
					<asp:TextBox ID="nameofitem" runat="server" ToolTip="Name of the Item"></asp:TextBox>
					<asp:RequiredFieldValidator ID="namerequired" runat="server" ControlToValidate="nameofitem" ErrorMessage="* Field is required!" SetFocusOnError="True" />
					<asp:RegularExpressionValidator ID="RegularExpressionValidatorname" runat="server" ControlToValidate="nameofitem" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Valid characters: Alphabets and space." />
				</td>
			</tr>
			<tr>
				<td class="auto-style28">
					<asp:Label ID="priceofitemlabel" runat="server" Text="Price of the Item(Rs.)"></asp:Label>
				</td>
				<td class="auto-style29">
					<asp:TextBox ID="priceofitem" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="PriceReq" runat="server" ControlToValidate="priceofitem" ErrorMessage="* Field is required!" SetFocusOnError="True" />
					<asp:RegularExpressionValidator ID="digitsReq" ControlToValidate="priceofitem" runat="server" ErrorMessage="Only Digits allowed!" ValidationExpression="\d+"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style29">
					<asp:Label ID="prsninvlded" runat="server" Text="Persons involved"></asp:Label>
				</td>
				<td class="auto-style29">

					<asp:CheckBoxList ID="personsinvolved" runat="server" RepeatDirection="Horizontal" CssClass="auto-style27" Height="19px">
						<asp:ListItem Text="Divya" Value="1" />
						<asp:ListItem Text="Jyotsna" Value="2" />
						<asp:ListItem Text="Keshvam" Value="3" />
						<asp:ListItem Text="Mitali" Value="4" />
						<asp:ListItem Text="Monika" Value="5" />
						<asp:ListItem Text="Navdeep" Value="6" />
						<asp:ListItem Text="Neeraj" Value="7" />
						<asp:ListItem Text="Seema" Value="8" />
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr>
				<td class="auto-style29" colspan="2">
					<div class="buttonrow">
						<asp:Button ID="sumbt" runat="server" Text="Submit" CssClass="button" Width="150px" OnClientClick="return validateCheckBoxList();" OnClick="sumbt_Click" />

						<%-- code for inserting image into self.aspx --%>
					</div>
				</td>
			</tr>
		</table>
	</form>

</asp:Content>
