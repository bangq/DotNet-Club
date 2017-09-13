var THISPAGE = {};
$(function(){
	THISPAGE = {
		init:function(){
			this.initPage();
			this.addEvent();
			this.errorLoginCount = parseInt($("#loginErrorCount").val() || 0,10)
		},
		initPage:function(){
			var _self = this;
			/*init remember_me*/
			if($.cookie('username') != ""){
				$("input[name=username]").val($.cookie('username'));
			}
			_self.rememberCheck = $(".loginextra-box").cssCheckbox();
			$(".ui-input").defaultValue();
		},
		addEvent:function(){
			var _self = this;
			/*refresh verifycode*/
			$(".auth-image").click(function(){
				$(this).attr("src","verifyCode"+ '?v=' + new Date().getTime());
			});
			/*二维码*/
			$('.mobileDown').hover(function(){
				$(".tips").show();
			},function(){
				$(".tips").hide();
			});
			/*加入收藏*/
			$(".addtofav").click(function(){
				_self.addToFav();
			});
			$("#btn-submit").click(function(e){
				e.preventDefault();
				var postData = {
					service:$("input[name=service]").val(),
					relayState:$("input[name=relayState]").val(),
				    username:$.trim($("input[name=username]").val()),
				    password:$.trim($("input[name=password]").val()),
				    verfCode:$.trim($("input[name=verifyCode]").val())
				};
				if(postData.username == ""){
					$(".error",$(".warning-box")).text("用户名不能为空");
					return false;
				}
				if(postData.password == ""){
					$(".error",$(".warning-box")).text("密码不能为空");
					return false;
				}
				
				$.ajax({
					url:"authentication",
					data:postData,
					type:"POST",
					dataType:"text",
					beforeSend:function(){
						$(".error",$(".warning-box")).text("");
						$("#btn-submit").attr("disabled",true).text("正在登录...");
					},
					success:function(responseText){
						if(responseText.indexOf("OK_") != -1){
							$(".error",$("#warning-box")).text("&nbsp;");
							postData.lt = responseText.split("OK_")[1];
							$("input[name=lt]").val(postData.lt);
							if($.inArray("on",_self.rememberCheck.chkVal()) != -1){
								$.cookie('username', postData.username, { expires: 7 });
							}else{
								$.cookie('username', "");
							}
							$("#myForm").submit();
							//_self.doLogin(postData);
						}else if(responseText == "login_error"){
							_self.errorLoginCount++;
							$(".error",$(".warning-box")).text("用户名密码错误");
							$("#btn-submit").attr("disabled",null).html("登&nbsp;&nbsp;录");
							if(_self.errorLoginCount >= 3){
								$(".verifyCode-box").show();
							}
						}else if(responseText == "need_verfCode"){
							$(".error",$(".warning-box")).text("用户名密码错误输入错误累计三次，请输入验证码");
							$(".verifyCode-box").show();
							$("#btn-submit").attr("disabled",null).html("登&nbsp;&nbsp;录");
						}else if(responseText == "wrong_verfCode"){
							$(".error",$(".warning-box")).text("验证码错误，请重新输入");
							$(".auth-image").attr("src","/verifyCode"+ '?v=' + new Date().getTime());
							$("#btn-submit").attr("disabled",null).html("登&nbsp;&nbsp;录");
						}else{
							$(".error",$(".warning-box")).text("网络异常，请联系管理员");
							$("#btn-submit").attr("disabled",null).html("登&nbsp;&nbsp;录");
						}
					},
					error:function(){
						$(".error",$("#warning-box")).text("网络异常，请联系管理员");
						$("#btn-submit").attr("disabled",null);
					}
				});
			});
			/*回车*/
			$("input[name=username]").keyup(function(e){
				if(e.keyCode == 13){
					$("input[name=password]").focus();
				}
			});
			$("input[name=username]").keyup(function(e){
				if(e.keyCode == 13){
					$("#btn-submit").trigger("click");
				}
			});
			
		},
		addToFav:function(){
			try{
				if (document.all){
				   window.external.addFavorite('www.yongjiasoft.com','永佳软件');
				}else if (window.sidebar){
				   window.sidebar.addPanel('永佳软件', 'www.yongjiasoft.com', "");
				}else{
					alert("您的浏览器需要您同时按下 CTRL + D 加入收藏");
				}
			}
			catch(e){
				alert("您的浏览器需要您同时按下 CTRL + D 加入收藏");
			}
			
		}
		/*doLogin:function(data){
			
			$.ajax({
				url:"/login",
				data:data,
				type:"POST",
				dataType:"JSON",
				success:function(responseText){
					debugger;
				},
				error:function(){
					$(".error",$("#warning-box")).text("网络异常，请联系管理员");
				}
			});
			
		}*/
	};
	THISPAGE.init();
});
