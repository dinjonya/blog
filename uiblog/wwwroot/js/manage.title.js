var ManageTitle=function(){
    var handleChangeTitle=function(){
        $("#btnSubmit").click(function(){
            jqAjax.ajax({
                url:App.ajaxUri.Manager.ChangeTitle.Uri,
                method:App.ajaxUri.Manager.ChangeTitle.Method,
                data:JSON.stringify({
                    "title":$("#txtTitle").val()
                }),
                success:function(data){
                    console.log("ManageTitle handleChangeTitle");
                    if(data.Message=="ok"){
                        alert("修改成功");
                        $("#txtTitle").val("");
                    }
                },
                buserror:function(data){
                    console.log(data)
                    $.removeCookie("ct")
                    $.removeCookie("cv")
                    window.location.href = "/index.html";
                }
            });
        });
    }

    return{
        init:function(){
            handleChangeTitle();
        }
    }
}();