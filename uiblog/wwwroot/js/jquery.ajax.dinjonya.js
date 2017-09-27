var jqAjax=function(){
    return {
        ajax:function(ajaxArgs){
            var data=ajaxArgs.data==undefined?null:ajaxArgs.data;
            var headers=null;
            if(ajaxArgs.headers==undefined && (Cookies.get("cv")!=undefined && Cookies.get("ct")!=undefined))
            {
                headers={"cv":Cookies.get("cv"),"ct":Cookies.get("ct")};
            }
            else
            {
                for(var key in ajaxArgs.headers) {
                    headers[key]=ajaxArgs.headers[key];
                }
            }
            var async = ajaxArgs.async==undefined?true:ajaxArgs.async;
            var method = ajaxArgs.method==undefined?"post":ajaxArgs.method;

            $.ajax({
                headers:headers,
                type:method,
                url:ajaxArgs.url,
                data:data,
                async:async,
                cache: false,
                crossDomain:true,
                success:function(data){
                    var status = data.Value.Status;
                    var result = data.Value.Data;
                    if(status) {
                        if(result.cookie!=undefined)
                        {
                            Cookies.set("cv",result.cookie.CookieValue);
                            Cookies.set("ct",result.cookie.CreateTime);
                        }
                        if(ajaxArgs.success!=undefined)
                            ajaxArgs.success(result);
                    }
                    else{
                        console.log(data)
                        if(ajaxArgs.buserror!=undefined)
                            ajaxArgs.buserror(result);
                    }
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    console.log(XMLHttpRequest.status);
                    console.log(XMLHttpRequest.readyState);
                    console.log(textStatus);
                },
                beforeSend: function(xhr) {
                    xhr.withCredentials = true;
                }
            });
        }
    }
}();