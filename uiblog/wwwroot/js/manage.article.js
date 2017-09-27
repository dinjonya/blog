var ManageArticle=function(){
    var cTime = function (time) {
        let unixtime = time
        let unixTimestamp = new Date(unixtime * 1000)
        let Y = unixTimestamp.getFullYear()
        let M = ((unixTimestamp.getMonth() + 1) > 10 ? (unixTimestamp.getMonth() + 1) : '0' + (unixTimestamp.getMonth() + 1))
        let D = (unixTimestamp.getDate() > 10 ? unixTimestamp.getDate() : '0' + unixTimestamp.getDate())
        let toDay = Y + '-' + M + '-' + D
        return toDay;
     }
    var handleShowArticleList=function(pi){
        jqAjax.ajax({
            url:App.ajaxUri.Manager.SelectPost.Uri+pi+"/"+Math.random(),
            method:App.ajaxUri.Manager.SelectPost.Method,
            success:function(data){
                var result = data.Result;
                console.log(data)
                var pc = data.PageCount;
                var html = "";
                $("#tbody").html(html);
                for (var index in result) {
                    html += "<tr style=\"height:50px;\">";
                    html += "<td>"+result[index]["Id"]+"</td>";
                    html += "<td>"+result[index]["Title"]+"</td>";
                    html += "<td>"+result[index]["Category"]+"</td>";
                    html += "<td>"+cTime(result[index]["DateTime"])+"</td>";
                    html += "<td>编辑</td>";
                    html += "<td><a href='#' class=\"deletePost\" data-val=\""+result[index]["Id"]+"\">删除</a></td>";
                    html += "</tr>"
                }
                $("#tbody").html(html);
                $(".deletePost").click(function(){
                    var postId = $(this).attr("data-val");
                    jqAjax.ajax({
                        url:App.ajaxUri.Manager.RemovePost.Uri,
                        method:App.ajaxUri.Manager.RemovePost.Method,
                        data:JSON.stringify({
                            Id:postId
                        }),
                        success:function(data){
                            if(data.Message=="ok"){
                                alert("删除成功");
                                handleShowArticleList(pi);
                            }
                        }
                    });
                });
            }
        });
    }

    return{
        init:function(){
            handleShowArticleList(1);
        }
    }
}();