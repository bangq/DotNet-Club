$.fn.cssCheckbox = function(opts) {
	    var $_chk = $(".chk", this);
	    var defaultOpts = {
	        callback:function(){}
	    };
	    var opts = $.extend({},true,defaultOpts, opts);
	    $_chk.each(function() {
	        if ($(this).find("input")[0].checked) {
	            $(this).addClass("checked");
	        };
	        if ($(this).find("input")[0].disabled) {
	            $(this).addClass("dis_check");
	        };
	    }).hover(function() {
	            $(this).addClass("over")
	        }, function() {
	            $(this).removeClass("over")
	        }).off("click").on("click",function(event) {
	            if ($(this).find("input")[0].disabled) {
	                return;
	            };
	            $(this).toggleClass("checked");
	            $(this).find("input")[0].checked = !$(this).find("input")[0].checked;
	            opts.callback($(this).find("input")[0]);
	            event.preventDefault();
	        });

	    return {
	        chkAll:function(){
	            $_chk.addClass("checked");
	            $_chk.find("input").attr("checked","checked");
	        },
	        chkNot:function(){
	            $_chk.removeClass("checked");
	            $_chk.find("input").removeAttr("checked");
	        },
	        chkVal:function(){
	            var val = [];
	            $_chk.find("input").each(function(i,v) {
	                if(v.checked){
	                    val.push($(this).val());
	                }else{
	                    val.push("");
	                }
	            });
	            return val;
	        }
	    }
	};