<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" CodeFile="Guest.aspx.cs" Inherits="Guest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	
  <script src="jquery-ui-1.12.1.custom/jquery/jquery.js"></script>
	
	 <%-- code for stoping user to visit this page after back button --%>
	

<script type = "text/javascript" >

   function preventBack(){window.history.forward();}

	setTimeout("preventBack()", 0);

	window.onunload=function(){null};

</script>
  <script src="jquery-ui-1.12.1.custom/jquery-ui.js"></script>

	<script type="text/javascript">

		
	//code for disabling the back button after login
	(function (global) {

		if (typeof (global) === "undefined") {
			throw new Error("window is undefined");
		}

		var _hash = "!";
		var noBackPlease = function () {
			global.location.href += "#";

			// making sure we have the fruit available for juice (^__^)
			global.setTimeout(function () {
				global.location.href += "!";
			}, 50);
		};

		global.onhashchange = function () {
			if (global.location.hash !== _hash) {
				global.location.hash = _hash;
			}
		};

		global.onload = function () {
			noBackPlease();

			// disables backspace on page except on input fields and textarea..
			document.body.onkeydown = function (e) {
				var elm = e.target.nodeName.toLowerCase();
				if (e.which === 8 && (elm !== 'input' && elm !== 'textarea')) {
					e.preventDefault();
				}
				// stopping event bubbling up the DOM tree..
				e.stopPropagation();
			};
		}

	})(window);



 $(function () {

		  $("#datetextbox").datepicker(({
			 minDate: '-7d', // your min date
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
				  $("#nameofitem_error").html("Only Alphabets are allowed!");
				 // $("#nameofitem_error").show();
			  
				  var error_nameofitem = true;
				  
			  } else {
				  var key = e.keyCode;
				  if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
					  $("#nameofitem_error").html("Only Alphabets are allowed!");
					  $("#nameofitem_error").show();
			  
					  var error_nameofitem = true;
				  }
				  else  {
					  $("#nameofitem_error").hide();
				  }

			  }

		  });

	  });

	  $(function () {

		  var error_location = false;
		  $("#location_error").hide();

		  $("#Location").keydown(function (e) {
			  if (e.shiftKey || e.ctrlKey || e.altKey) {
				  $("#location_error").html("Only Alphabets are allowed!");
				  // $("#nameofitem_error").show();

				  var error_location = true;

			  } else {
				  var key = e.keyCode;
				  if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
					  $("#location_error").html("Only Alphabets are allowed!");
					  $("#location_error").show();

					  var error_location = true;
				  }
				  else {
					  $("#location_error").hide();
				  }

			  }

		  });

	  });




	  //$(function () {
	  //function pulse() {
	  //    $('#greet').fadeIn();
	  //    $('#greet').fadeOut();
	  //}
	  //setInterval(pulse, 1000);
	  //});
   
	  $(function () { 

		  now = new Date

		  if (now.getHours() < 5) {
			  $('#greet').html("Welcome, What are you doing up so late?");
			  
		  }
		  else if (now.getHours() >= 5 && now.getHours() < 12) {
			  $('#greet').html("Welcome , Good morning!")
			 
		  }
		  else if (now.getHours() >= 12 && now.getHours() <= 18) {
			  $('#greet').html("Welcome,Good Afternoon!")
		  }

		  else if (now.getHours() >= 18 && now.getHours() < 21) {
			  $('#greet').html("Welcome, Good Evening!")
		  }


		  else {
			  $('#greet').html("Welcome, Good Night!")
		  }


	  });

   /* $(function () {

	  $("#sumbt").click(function(){

		  $("#addbut").attr("disabled", false); 

	  });​

	  })*/

		</script>
  



	<style type="text/css">
		#selftable {
			width: 1030px;
			border: 1px solid black;
		  
		 
		 margin-left:5px;
		  border-radius:5px;
		  background-color: #DAA520;
		 


		}
	   

#selftable td {
	border: 1px solid #ddd;
	padding: 8px;
	
}

#selftable tr:nth-child(even){background-color: #f2f2f2;}

#selftable tr:hover {background-color: #3CB371;}
#selftable tr:last-child:hover {background-color: #DAA520;}


#selftable tr#sumbt{
	height:10px;
	width:50px;
}



.button  {

	/*background-color: #4CAF50;*/
	background-color:black;
	color: #fff;
	font-size: 16px;
	font-family: Calibri,Arial;
	padding: 6px 30px;
	border:none;
   /*border-bottom: 3px solid #925b08;*/ 
   border-radius: 5px;
  box-shadow: 0 4px #999;
  cursor:pointer;
   
}
.button:hover {background-color: #3e8e41}

.button:active {
  background-color: #3e8e41;
  box-shadow: 0 3px #666;
  transform: translateY(2px);
}

/*.button:hover{
	background:#32CD32;                /*925b08;*/
   /* border-bottom: 3px solid #f90;}*/

		
.button:focus{
	outline:none;
}





.divider{
	width:180px;
	height:auto;
	display:inline-block;
   
	
}
.buttonrow{
	text-align:center;
	margin-left:auto;
	margin-right:auto;
}

		
		.auto-style9 {
			width: 190px;
			height: 26px;
		}
		.auto-style15 {
			width: 190px;
		}
		
		
		
		.ui-datepicker { 
  margin-left: 100px;
  
  margin-top:10px;
}
		
		
		div.ui-datepicker{
 font-size:12px;
}

	  
		.auto-style16 {
			height: 26px;
		}

	   

	   

	   

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Headercontent1" Runat="Server">

	<h1 id="heading">
	 <img id="image1" src="images/logo.png"/>
	 <i>W</i>elcome <i>t</i>o <i>y</i>our <i>S</i>aving <i>S</i>ite!
	  <img id="image2" src="images/logo.png"/>
	  </h1>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navbarhold" Runat="Server">


	<ul>
		 <%--  <li><a href="Login.aspx" class="homepage" title="Home">Home</a></li>

	 
			<li><a href="#this" class="selfpage" title="Self">Self</a></li>
  <li><a href="#this" class="logutpage"  title="Logout" onserverclick="logoutevent" runat="server" causesvalidation="false">Logout</a></li>
	
			<li><a href="Group.aspx" class="grouppage"    title="Group">Group</a></li> --%>
	  <%--  <li><form id="logoutform"><asp:Button ID="logoutbutton" Text="Logout" runat="server" OnClick="logouteventmethod" /></form></li>--%>
		   <%-- <li><a href="Logout.aspx" class="logutpage"  title="Logout">Logout</a></li>--%>
	   <%-- <li><input type="button" ID="logoutbutton" value="Logout" runat="server" onclick="logouteventmethod" /></li>--%>
	 
		  <li><a href="#this" class="logutpage"  title="Logout" onserverclick="logoutevent" runat="server" causesvalidation="false">Logout</a></li>
	 <li><asp:Label ID="usernamelabel" runat="server" style="color:white;font-size:medium;margin-left:25px;display:inline;text-align:center"  Text="No user" ClientIDMode="Static" ></asp:Label> </li>
	
		<li>   <p id="greet"><span id="greetmsg"></span></p></li>
		<%-- <li>    <p id="greet"><span id="greetmsg"></span></p></li>--%>
		
	</ul>

	
		


</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="sectionmsge" Runat="Server">
   <h1 id="expense" style="position:center;margin-left:auto;margin-right:auto;">Expense Details</h1><br /> <p style="font-size:medium"><b>Here you can provide the expense details so that Hardeep will return it on time!</b></p>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="detail" Runat="Server">

	
	<form id="detailsform" runat="server">
		<table id="selftable">
			<tr>
				<td class="auto-style15">
					<asp:Label ID="datelabel" runat="server" Text="Date of Expenditure"></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="datetextbox" runat="server" ToolTip="Select Date" ClientIDMode="Static" ViewStateMode="Enabled" ></asp:TextBox>
					<asp:RequiredFieldValidator id="RequiredFieldValidator1"
			  runat="server"
			  ControlToValidate="datetextbox"
			  ErrorMessage="Field is required!"
			  SetFocusOnError="True" />
				</td>
			</tr>
			<tr>
				<td class="auto-style15">
					<asp:Label ID="nameofitemlabel" runat="server" Text="Name of the Item"></asp:Label>
				</td>
				<td>
					<span id="nameofitem_error" style="color:black"></span>
					<asp:TextBox ID="itemtextbox" runat="server" placeholder="Enter Item Name"  ToolTip="Enter Item Name" AutoCompleteType="Disabled"></asp:TextBox>
					  <asp:RequiredFieldValidator id="itemnameReq"
			  runat="server"
			  ControlToValidate="itemtextbox"
			  ErrorMessage="Field is required!" 
			  SetFocusOnError="True" />
					 <asp:RegularExpressionValidator ID="itemnamereg" ControlToValidate="itemtextbox" runat="server" ErrorMessage="Only Characters are allowed!" ValidationExpression="([A-Za-z])+( [A-Za-z]+)*"></asp:RegularExpressionValidator>  
&nbsp;</td>
			</tr>
			<tr>
				<td class="auto-style15">
					<asp:Label ID="priceofitemlabel" runat="server"  Text="Price of the Item(Rs.)"></asp:Label>
				</td>
				<td>
					<span id="price_error" style="color:black"></span>
					<asp:TextBox ID="pricetextbox" runat="server" placeholder="Enter Price of the Item"  ToolTip="Enter Price " AutoCompleteType="Disabled"></asp:TextBox>
					 <asp:RequiredFieldValidator id="PriceReq"
			  runat="server"
			  ControlToValidate="pricetextbox"
			  ErrorMessage="Field is required!"
			  SetFocusOnError="True" />
				  <asp:RegularExpressionValidator ID="digitsReq" ControlToValidate="pricetextbox" runat="server" ErrorMessage="Only Digits allowed!" ValidationExpression="\d+"></asp:RegularExpressionValidator>  
&nbsp;</td>
			</tr>
			<tr>
				<td class="auto-style9">
					<asp:Label ID="Location" runat="server" Text="Location of Expense"></asp:Label>
				</td>
				<td class="auto-style16">
					<span id="location_error" style="color:black"></span>
					<asp:TextBox ID="locationtextbox" runat="server" placeholder="Enter Location Name"  ToolTip="Enter Location Name" AutoCompleteType="Disabled"></asp:TextBox>
					<asp:RequiredFieldValidator id="locationReq"
			  runat="server"
			  ControlToValidate="locationtextbox"
			  ErrorMessage="Field is required!" 
			  SetFocusOnError="True" />
					  <asp:RegularExpressionValidator ID="locationnamereg" ControlToValidate="locationtextbox" runat="server" ErrorMessage="Only Characters are allowed!" ValidationExpression="([A-Za-z])+( [A-Za-z]+)*"></asp:RegularExpressionValidator>  
&nbsp;</td>
			</tr>
	  
			<tr>
				<td colspan="2">
					<div class="buttonrow">
					<asp:Button ID="sumbt" runat="server" Text="Submit" CssClass="button" Width="150px"   />
					
				  <%-- <div class="divider" />
					 <asp:Button ID="addbut" runat="server" Text="Add More " Enabled="true" CssClass="button1" Width="150px" />
			   </div>--%>
						</div>
				 </td>
			</tr>
		</table>
	</form>

	
	</asp:Content>


<%-- code for inserting image into self.aspx --%>
	<asp:Content ID="Content6" ContentPlaceHolderID="sectionimage" runat="Server">
	 
		<%--<img id="userimage" src="" width="240" height="220" style="border-style:double;border-radius:5px;border-width:medium" alt="Image" />--%>
	</asp:Content>