var App=function(){
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

    return{
        init:function(){
            handleFooterPosition();
            handleNavMenuToggle();
        }
    }
}();