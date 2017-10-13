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

        /* 表格数据 */
        var result = tags;
        var html = "";
        $("#tbody").html(html);
        for (var index in result) {
            html += "<tr style=\"height:50px;\">";
            html += "<td>"+result[index]["Id"]+"</td>";
            html += "<td>"+result[index]["TagName"]+"</td>";
            html += "<td><a href='#' class=\"deletePost\" data-val=\""+result[index]["Id"]+"\">删除</a></td>";
            html += "</tr>"
        }
        $("#tbody").html(html);
        $(".deletePost").click(function(){
            var tId = $(this).attr("data-val");
            $('#btn-dialogBox').dialogBox({
                hasClose: true,
                hasMask: true,
                hasBtn: true,
                confirmValue: 'Yes',
                confirm: function(){
                    jqAjax.ajax({
                        url:App.ajaxUri.Manager.RemoveTag.Uri,
                        method:App.ajaxUri.Manager.RemoveTag.Method,
                        data:JSON.stringify({
                            "tid":tId
                        }),
                        success:function(data){
                            if(data.Message=="ok"){
                                alert("删除成功");
                                handleGetAllTags();
                            }
                        }
                    });
                },
                cancelValue: 'No',
                title: '删除标签【 '+ tId +' 】',
                content: '确定要删除这个标签吗?'
            });
        });
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