//set content
//tinyMCE.activeEditor.setContent("<p>一个<b>文</b>字</p>")
//get content 
//tinyMCE.activeEditor.setContent(data.Message);
var ManageAutograph = function(){

    var loadAutographContent = function(){
        jqAjax.ajax({
            url:App.ajaxUri.Manager.SelectAutograph.Uri+Math.random(),
            method:App.ajaxUri.Manager.SelectAutograph.Method,
            success:function(data){
                tinyMCE.activeEditor.setContent(data.Message);
            }
        });
    }

    var handleSelectAutographContent = function() {
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        loadAutographContent();
    }

    var handleSubmitAutographContent=function(){
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        $("#btnSubmit").click(function(){
            var aboutContent = tinyMCE.activeEditor.getContent();
            aboutContent = aboutContent.replace(/..\/images\/upload/g,"/images/upload");
            console.log(aboutContent)
            jqAjax.ajax({
                url:App.ajaxUri.Manager.UpdateAutograph.Uri,
                method:App.ajaxUri.Manager.UpdateAutograph.Method,
                data:JSON.stringify({
                    "content":aboutContent
                }),
                success:function(data){
                    if(data.Message=="ok"){
                        alert("修改成功");
                        loadAutographContent();
                    }
                }
            });

        });
    }

    var handleTinymceInit = function() {
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        tinymce.init({
            selector: '#editor1',
            theme: 'modern',
            plugins: 'image,textcolor,codesample,colorpicker,fullscreen,link,hr,emoticons,lists',
            toolbar: 'undo redo | forecolor backcolor fontsizeselect | styleselect | bold italic | numlist bullist | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image imagetools hr codesample emoticons | markdown sequence | fullscreen ',
            menubar: false,
            Height: 400,
            file_browser_callback_types: 'file image',
            images_upload_url: App.ajaxUri.Manager.UploadImg.Uri,
            images_upload_base_path: '/images/upload',
            images_upload_credentials: true,
            automatic_uploads: true,
            images_upload_handler: function (blobInfo, success, failure) {
                var xhr, formData;
                xhr = new XMLHttpRequest();
                xhr.withCredentials = false;
                xhr.open(App.ajaxUri.Manager.UploadImg.Method, App.ajaxUri.Manager.UploadImg.Uri);
                xhr.onload = function() {
                  var json;
                  if (xhr.status != 200) {
                    failure('HTTP Error: ' + xhr.status);
                    return;
                  }
                  json = JSON.parse(xhr.responseText);
                  
                  if (!json || typeof json.location != 'string') {
                    failure('Invalid JSON: ' + xhr.responseText);
                    return;
                  }
                  success(json.location);
                };
                formData = new FormData();
                formData.append('file', blobInfo.blob(), blobInfo.name());
                xhr.send(formData);
            },
            setup: function (editor) {
                editor.addButton('markdown', {
                    text: 'markdown',
                    icon: false,
                    onclick: function () {
                      var content = editor.selection.getContent({ 'format' : 'text' });
                      editor.selection.setContent("<div class='dvmark'><pre><code>"+content+"</code></pre></div>");
                    }
                });
                editor.addButton('sequence', {
                    text: 'sequence',
                    icon: false,
                    onclick: function () {
                      var content = editor.selection.getContent({ 'format' : 'text' });
                      editor.selection.setContent("<div class='dvsequence'><pre><code>"+content+"</code></pre></div>");
                    }
                });
            }
        });
    }

    return{
        init:function(){
            handleSelectAutographContent();
            handleTinymceInit();
            handleSubmitAutographContent();
        }
    }
    
}();