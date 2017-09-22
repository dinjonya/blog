var ManageAbout = function(){
    var handleSubmitAboutContent=function(){
        $("#btnSubmit").click(function(){
            console.log(tinyMCE.activeEditor.getContent());
            tinyMCE.activeEditor.setContent("<p>一个<b>文</b>字</p>")
        });
    }

    return{
        init:function(){
            handleSubmitAboutContent();
        }
    }
    
}();