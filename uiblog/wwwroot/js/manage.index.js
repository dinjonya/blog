var ManageIndex=function(){
    var handleAuthen=function(){
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        jqAjax.ajax({
            url:App.ajaxUri.Manager.Authen.Uri,
            Method:App.ajaxUri.Manager.Authen.Method,
            success:function(data){
                console.log("ManageIndex handleAuthen");
            },
            buserror:function(data){
                console.log(data)
                Cookies.remove("ct")
                Cookies.remove("cv")
                window.location.href = "/index.html";
            }
        });
    }

    return{
        init:function(){
            handleAuthen();
        }
    }
}();