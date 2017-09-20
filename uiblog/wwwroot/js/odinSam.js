var OdinSam=function(){
    var handlerClearCookie = function(){
        //$.removeCookie("ct")
        //$.removeCookie("cv")
        Cookies.remove("ct");
        Cookies.remove("cv");
    }
    var handleManagerLogin=function(){
        $("#loginBtn").click(function(){
            jqAjax.ajax({
                url:App.ajaxUri.Manager.Login.Uri,
                method:App.ajaxUri.Manager.Login.Method,
                data:JSON.stringify({
                    "un":$("#untxt").val(),
                    "up":$("#uppwd").val()
                }),
                success:function(data){
                    window.location.href = "/manager/index.html"
                },
                buserror:function(data){
                    alert(data);
                }
            });
        });
    }

    return{
        init:function(){
            handlerClearCookie();
            handleManagerLogin();
        }
    }
}();