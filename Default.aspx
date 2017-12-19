<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

	<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">

		<script src="jquery-ui-1.12.1.custom/jquery/jquery.js"></script>
		<script src="jquery-3.1.0.js"></script>
		<script src="jqueryui/jquery-ui.js"></script>
		<script type="text/javascript">
			//disable back button
			function DisableBackButton() {
				window.history.forward()
			}
			DisableBackButton();
			window.onload = DisableBackButton;
			window.onpageshow = function(evt) {
				if (evt.persisted) DisableBackButton()
			}
			window.onunload = function() {
				void(0)
			}

			$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");
			$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");

			function alertMX(t) {
				$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p>CLOSE</p></div>"));
				$('.msgMX').text(t);
				var popMargTop = ($('#boxMX').height() + 24) / 2,
					popMargLeft = ($('#boxMX').width() + 24) / 2;
				$('#boxMX').css({
					'margin-top': -popMargTop,
					'margin-left': -popMargLeft
				}).fadeIn(600);
				$("#boxMX").click(function() {
					$(this).remove();
					enableall();
				});
			};

			//disable everything on page

			function disableall() {

				$("input").prop('disabled', true);
				$(function() {
					$('a').on("click", function(e) {
						e.preventDefault();
					});
				});
			}

			function enableall() {

				$("input").prop('disabled', false);
				$(function() {
					$('a').on("click", function(e) {
						window.location = this.href;
					});
				});
			}

			//code for disabling the back button after login
			(function(global) {

				if (typeof(global) === "undefined") {
					throw new Error("window is undefined");
				}

				var _hash = "!";
				var noBackPlease = function() {
					global.location.href += "#";

					global.setTimeout(function() {
						global.location.href += "!";
					}, 50);
				};

				global.onhashchange = function() {
					if (global.location.hash !== _hash) {
						global.location.hash = _hash;
					}
				};

				global.onload = function() {
					noBackPlease();

					// disables backspace on page except on input fields and textarea..
					document.body.onkeydown = function(e) {
						var elm = e.target.nodeName.toLowerCase();
						if (e.which === 8 && (elm !== 'input' && elm !== 'textarea')) {
							e.preventDefault();
						}
						// stopping event bubbling up the DOM tree..
						e.stopPropagation();
					};
				}

			})(window);
		</script>

	</asp:Content>

	<asp:Content ID="Content1" ContentPlaceHolderID="Headercontent1" Runat="Server">

		<h1 id="heading">
	 <img id="image1" src="images/logo.png" alt="Logo" />
	 <i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite!
	 <img id="image2" src="images/logo.png" alt="Logo" />
	 </h1>

	</asp:Content>

	<asp:Content ID="Content2" ContentPlaceHolderID="Formcontent1" Runat="Server">

		<form id="theForm" runat="server" autocomplete="off">
			<asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />

			<div class="Container">
				<label for="Username" title="Username">Username:&nbsp;&nbsp; </label>
				<asp:TextBox ID="username" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator id="usernameReq" runat="server" ControlToValidate="username" ErrorMessage="Field is Required!" SetFocusOnError="True" />
				<br>
				<br>
			</div>

			<div class="container">
				<label for="Password" title="Password">Password:&nbsp;&nbsp; </label>
				<asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
				<asp:RequiredFieldValidator id="passReq" runat="server" ControlToValidate="password" ErrorMessage="Field is Required!" SetFocusOnError="True" />
				<br>
				<br />
				<br>
			</div>
			<div class="container">
				<asp:CheckBox ID="chkBoxRememberMe" Font-Size="Small" Text="Remember Me" runat="server" />
				<!--  <a href="Activate.aspx" id="forgotpassword">Forgot your Password?</a>-->
				<a href="Activate.aspx">Register Here?</a>
			</div>
			<div class="container">
				<asp:Button ID="submitbutton" runat="server" onclick="submitEventMethod" Text="Login" CssClass="button" ToolTip="Log In" />
			</div>

		</form>
	</asp:Content>

	<asp:Content ID="Content3" ContentPlaceHolderID="Quotescontent1" Runat="Server">

		<h3>Few Words:</h3>
		<p class="textquotes">A good reputation is more valuable than money.
			<br>
			<br><b>-Publilius Syrus</b></p>
		<p class="textquotes">Sometimes by losing a battle you find a new way to win the war.
			<br>
			<br><b>-Donald Trump</b></p>
		<p class="textquotes">Don't let making a living prevent you from making a life.
			<br>
			<br><b>-John Wooden</b></p>
		<p class="textquotes">You can't blame gravity for falling in love.
			<br>
			<br><b>-Albert Einstein</b></p>
		<p class="textquotes">A successful man is one who makes more money than his wife can spend. A successful woman is one who can find such a man.
			<br>
			<br><b>-Lara Turner</b></p>
		<p class="textquotes">The difference between stupidity and genius is that genius has its limits.
			<br>
			<br><b>-Albert Einstein</b></p>
		<p class="textquotes">Success is not final, failure is not fatal: it is the courage to continue that counts.
			<br>
			<br><b>-Winston Churchill</b></p>
		<p class="textquotes">Whatever you decide to do, make sure it makes you happy.
			<br>
			<br><b>-Lazar Angelov</b></p>
	</asp:Content>

	<asp:Content ID="Content4" ContentPlaceHolderID="slideshowcontainer1" Runat="Server">

		<div id="sliderFrame">
			<div id="slider" title="Move out mouse for slideshow!">

				<img src="images/div1.jpg" style="width:100%" alt="" />
				<img src="images/div2.jpg" style="width:100%" alt="" />
				<img src="images/div3.jpg" style="width:100%" alt="" />
				<img src="images/div4.jpg" style="width:100%" alt="" />
				<img src="images/div5.jpg" style="width:100%" alt="" />
				<img src="images/div6.jpg" style="width:100%" alt="" />
				<img src="images/div7.jpg" style="width:100%" alt="" />
				<img src="images/div8.jpg" style="width:100%" alt="" />

			</div>

		</div>

	</asp:Content>

	<asp:Content ID="Content5" ContentPlaceHolderID="Footer1" Runat="Server">

		<ul>
			<li><a href="About.html" title="About">About</a></li>
			<li><a href="Contact.html" title="Contact" id="contact">Contact</a></li>
			<li>
				<div id="clockDisplay" class="container" title="Date"></div>
			</li>
			<li>
				<asp:Label runat="server" ID="visitlabel" Text="Visitor No:"></asp:Label>
				<asp:Label runat="server" ID="visitorlabel" Text=""></asp:Label>
				<%--</div>--%>
			</li>
		</ul>

		&copy:2017 All Rights Reserved.
		<%--&copy:2017 www.hardeepsinghnegi.com. All Rights Reserved.--%>

	</asp:Content>

	<asp:Content ID="Content7" ContentPlaceHolderID="About1" Runat="Server">

	</asp:Content>