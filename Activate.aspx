<%@ Page Title="" Language="C#" MasterPageFile="~/Activate.master" AutoEventWireup="true" CodeFile="Activate.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

	<script src="jquery-ui-1.12.1.custom/jquery/jquery.js"></script>
	<script src="jquery-3.1.0.js"></script>
	<script src="jqueryui/jquery-ui.js"></script>
	<script type="text/javascript">

		$("<style type='text/css'>#boxMX{display:none;background: #333;padding: 10px;border: 2px solid #ddd;float: left;font-size: 1.2em;position: fixed;top: 50%; left: 50%;z-index: 99999;box-shadow: 0px 0px 20px #999; -moz-box-shadow: 0px 0px 20px #999; -webkit-box-shadow: 0px 0px 20px #999; border-radius:6px 6px 6px 6px; -moz-border-radius: 6px; -webkit-border-radius: 6px; font:13px Arial, Helvetica, sans-serif; padding:6px 6px 4px;width:300px; color: white;}</style>").appendTo("head");

		function alertMX(t) {
			$("body").append($("<div id='boxMX'><p class='msgMX' style='text-align:center'></p><p style='text-align:center'>CLOSE</p></div>"));
			$('.msgMX').text(t); var popMargTop = ($('#boxMX').height() + 24) / 2, popMargLeft = ($('#boxMX').width() + 24) / 2;
			$('#boxMX').css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }).fadeIn(600);
			$("#boxMX").click(function () { $(this).remove(); enableall(); });
		};

		function disableall() {
			$("input").prop('disabled', true);
			$(function () {
				$('a').on("click", function (e) {
					e.preventDefault();
				});
			});
		}

		function enableall() {
			$("input").prop('disabled', false);
			$(function () {
				$('a').on("click", function (e) {
					window.location = this.href;
				});
			});
		}

		$(function () {
			function pulse() {
				$('#noteanimate').fadeIn();
				$('#noteanimate').fadeOut();
			}
			setInterval(pulse, 1000);
		});

		function emailcheck() {
			var error_code = false;
			$("#codeerror").hide();
			var pass = $('#activecode').val().length;

			if ((pass != 8)) {

				$("#codeerror").html("Invalid Code!");
				$("#codeerror").show();
				var error_code = true;
			}
			else {
				$("#codeerror").hide();
			}
		};
	  </script>

	<style type="text/css">

		  #regtable {
			/*width: 850px;*/
			border: 1px solid black;
			height: 300px;
			width: 600px;
			margin-left: auto;
			margin-right: auto;
			border-radius: 5px;
			background-color: #DAA520;
		}
		   .noteregister #note {
			color: black;
			font-size: small;
			font-family: Algerian,Arial,sans-serif;
			text-align: center;
		}
		   
		.noteregister {
			width: 500px;
			height: 60px;
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
			font-family: Calibri,Arial;
			padding: 6px 30px;
			border: none;
			/*border-bottom: 3px solid #925b08;*/
			border-radius: 5px;
			box-shadow: 0 4px #999;
			cursor: pointer;
		}

			.button:hover {
				background-color: #228B22;
			}
			

			.button:active {
				background-color: #3e8e41;
				box-shadow: 0 3px #666;
				transform: translateY(2px);
			}
			
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

		.auto-style51 {
			height: 49px;
		}

		.auto-style52 {
			height: 123px;
			width: 351px;
		}

		.auto-style55 {
			width: 180px;
			text-align: left;
			height: 123px;
		}

		.auto-style56 {
			width: 351px;
			text-align: left;
			height: 123px;
		}

		  </style>
	</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Headercontent1" runat="Server">

	<h1 id="heading">
		<img id="image1" src="images/logo.png" />
		<i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite!
	  <img id="image2" src="images/logo.png" />
	</h1>

	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="navigationbar" runat="Server">

	<ul>
		<li><a href="Default.aspx" title="Home">Home</a></li>

	</ul>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="register" runat="Server">

	<form id="form1" runat="server" autocomplete="off">
		<table id="regtable" class="auto-style21">
			<tr>
				<td class="auto-style55">&nbsp; 
					<br />
					&nbsp;&nbsp;
				   
					 <asp:Label ID="Label2" runat="server" Text="Enter Email"></asp:Label>
				</td>
				<td class="auto-style52">&nbsp;<asp:TextBox ID="email" runat="server" required=""></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="auto-style55">&nbsp;
					<asp:Label ID="Label1" runat="server" Text="Activation Code"></asp:Label>
				</td>
				<td class="auto-style56">&nbsp;<asp:TextBox ID="code" runat="server" TextMode="Password" required=""></asp:TextBox>

							&nbsp;</td>
			</tr>

			<tr>
				<td colspan="2" class="auto-style51">

					<div class="buttonrow">
						<asp:Button ID="active_code" runat="server" Text="Activate" CssClass="button" Width="150px" OnClick="active_code_Click" />
					</div>
				</td>
			</tr>
		</table>
	</form>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="registernote" runat="Server">

	<p id="note">
		<u id="noteanimate">Note:</u><br />
		Only a few specified members can
						register on this site! Please provide details to proceed for registration!
	</p>

</asp:Content>



		



		 

   

		

		   





	

		

		

	  


		


	   

		

			

			


		
  


	
	  

	   





		

		 

 


		   

	   
		




	   


	 


	  
  






	


		
