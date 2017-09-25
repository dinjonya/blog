var ManageTag=function(){
    var handleGetTags = function(){
        jqAjax.ajax({
            url:App.ajaxUri.Manager.GetAllTags.Uri+Math.random(),
            method:App.ajaxUri.Manager.GetAllTags.Method,
            success:function(data){
                console.log("ManageTag handleGetTags");
                console.log(data);
                showTags(data);
            },
            buserror:function(data){
                console.log(data)
                Cookies.remove("ct")
                Cookies.remove("cv")
                window.location.href = "/index.html";
            }
        });
    }
    var showTags = function(tags) {
        var html = "";
        for (var index in tags) {
            html += "<span style=\"padding:4px 6px;\"> "+ tags[index].TagName +" ("+ tags[index].PostNum +") </span>"
        }
        $("#dvTags").html(html);
    }
    //获取所有tag
    var handleGetAllTags=function(){
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        handleGetTags();
    }
    //添加新tag
    var handleAddTag = function() {
        $("#btnSubmit").click(function(){
            if($("#txtTag").val()=="")
                alert("请填写Tag 名称");   
            jqAjax.ajax({
                url:App.ajaxUri.Manager.AddTag.Uri,
                method:App.ajaxUri.Manager.AddTag.Method,
                data:JSON.stringify({
                    "tag":$("#txtTag").val()
                }),
                success:function(data){
                    console.log("ManageCategory handleAddCategory");
                    $("#txtTag").val("")
                    handleGetTags();
                },
                buserror:function(data){
                    console.log(data)
                    Cookies.remove("ct")
                    Cookies.remove("cv")
                    window.location.href = "/index.html";
                }
            });
        });
    }
    return{
        init:function(){
            handleGetAllTags();

            handleAddTag();
        }
    }
}();