var App=function(){
    var AjaxUri = null;
    if(window.location.hostname=="127.0.0.1") {
        AjaxUri = ajaxUrl.local;
    }
    else {
        AjaxUri = ajaxUrl.online;
    }
    /* 计算footer的位置 */
    var handleFooterPosition=function(){
        var windowHeight = $(window).height();
        var headerHeight = $(".nav").height();
        var footerHeight = $(".footer").height();
        var mainHeight = $(".main").height();
        if(windowHeight-headerHeight-footerHeight-10-20 > mainHeight)
            $(".main").css("min-height",windowHeight-headerHeight-footerHeight-30+"px");
    }

    var handleNavMenuToggle = function(){
        /*移动端导航菜单 显示*/
        $(".menu-icon").click(function(){
            $(".nav .container ul").slideToggle(500);
        });
    }

    var handleSetWebPUV = function(pagePath){
        jqAjax.ajax({
            url:AjaxUri.Statistics.PUV.Uri+pagePath+"/"+Math.random(),
            method:AjaxUri.Statistics.PUV.Method,
            success:function(data){
            }
        });
    }

    return{
        ajaxUri:AjaxUri,
        init:function(){
            handleFooterPosition();
            handleNavMenuToggle();
        },
        setWebPUV:function(pagePath){
            handleSetWebPUV(pagePath);
        }
    }
}();