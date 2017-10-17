var markDown = function(){
    var handleConvert = function(){
        //创建实例
        var converter = new showdown.Converter();
        var txt = $(".dvmark").html();
        txt = txt.replace(/<pre><code>/g,"");
        txt = txt.replace(/<\/code><\/pre>/g,"");
        //进行转换
        var html = converter.makeHtml(txt);
        //展示到对应的地方  result便是id名称
        $(".dvmark").html(html);

        //获取要转换的文字
        txt = $(".dvsequence").html();
        //进行转换
        var html = converter.makeHtml(txt);
        html = html.replace(/-&amp;gt;/g,"->");
        //展示到对应的地方  result便是id名称
        $(".dvsequence").html(html);
        $(".dvsequence").sequenceDiagram({theme:"simple"});
    }
    return {
        init:function(){
            handleConvert();
        }
    }
}();