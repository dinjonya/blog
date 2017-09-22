var ManageCategory=function(){
    var handleGetAllCategory=function(){
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
            
        jqAjax.ajax({
            url:App.ajaxUri.Manager.GetAllCategory.Uri+Math.random(),
            method:App.ajaxUri.Manager.GetAllCategory.Method,
            success:function(data){
                console.log("ManageCategory handleGetAllCategory");
                handleSetSelect(data);
            },
            buserror:function(data){
                console.log(data)
                Cookies.remove("ct")
                Cookies.remove("cv")
                window.location.href = "/index.html";
            }
        });
    }
    var handleSetSelect = function(data) {
        $("#txtCategory").val("");
        var categories = data.Categories;
        $("#selCategory")[0].length=1;
        for (var item in categories) {
            if(categories[item].Pid==0){
                $("#selCategory").append("<option value='"+categories[item].Id+"'>"+categories[item].CategoryName+"</option>");
                handleAddChildOptions(categories,categories[item].Id);
            }
        }
    }
    var handleAddChildOptions=function(categories,Pid){
        for (var item in categories) {
            if(categories[item].Pid==Pid){
                $("#selCategory").append("<option value='"+categories[item].Id+"'>"+categories[item].CategoryName+"</option>");
                handleAddChildOptions(categories,categories[item].Id);
            }
        }
    }

    var handleAddCategory = function() {
        $("#btnSubmit").click(function(){
            jqAjax.ajax({
                url:App.ajaxUri.Manager.AddCategory.Uri,
                method:App.ajaxUri.Manager.AddCategory.Method,
                data:JSON.stringify({
                    "category":$("#txtCategory").val(),
                    "value": $("#selCategory").val()
                }),
                success:function(data){
                    console.log("ManageCategory handleAddCategory");
                    handleSetSelect(data);
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
            handleGetAllCategory();

            handleAddCategory();
        }
    }
}();