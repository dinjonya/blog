var ManageAddPost = function(){
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
    var handleGetTags = function(){
        jqAjax.ajax({
            url:App.ajaxUri.Manager.GetAllTags.Uri+Math.random(),
            method:App.ajaxUri.Manager.GetAllTags.Method,
            success:function(data){
                var selections = [];
                for (var key in data) {
                    var text = data[key]["TagName"];
                    var val = data[key]["Id"];
                    var obj = { "id":val,"text":text };
                    selections.push(obj);
                }
                $('#selTag').select2({
                    minimumInputLength: 0,
                    width:300,
                    multiple: true,
                    allowClear: true,
                    data: selections,  
                }); 
            }
        });
    }

    var handleTinymceInit = function() {
        if(Cookies.get("cv")==undefined || Cookies.get("ct")==undefined)
            window.location.href = "/index.html";
        tinymce.init({
            selector: '#textAreaPostdesc',
            theme: 'modern',
            plugins: 'image,textcolor,codesample,colorpicker,fullscreen,link,hr,emoticons,lists',
            toolbar: 'undo redo | forecolor backcolor fontsizeselect | styleselect | bold italic | numlist bullist | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image imagetools hr codesample emoticons | fullscreen',
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
                console.log(blobInfo.name());
                formData.append('file', blobInfo.blob(), blobInfo.name());
                xhr.send(formData);
            }
        });
        tinymce.init({
            selector: '#textAreaPostContent',
            theme: 'modern',
            plugins: 'image,textcolor,codesample,colorpicker,fullscreen,link,hr,emoticons,lists',
            toolbar: 'undo redo | forecolor backcolor fontsizeselect | styleselect | bold italic | numlist bullist | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image imagetools hr codesample emoticons | fullscreen',
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
                console.log(blobInfo.name());
                formData.append('file', blobInfo.blob(), blobInfo.name());
                xhr.send(formData);
            }
        });
    }

    var handleSubmit = function() {
        $("#btnSubmit").click(function(){
            var title = $("#txtPostTitle").val();
            var categoryId = $("#selCategory").val();
            var postDesc = tinyMCE.editors[0].getContent();
            var postContent = tinyMCE.editors[1].getContent();
            var tags = $("#selTag").select2("val").toString();
            var desc = $("#txtPostPageDescription").val();
            var kw = $("#txtPostPageKeywords").val();
            jqAjax.ajax({
                url:App.ajaxUri.Manager.AddPost.Uri,
                method:App.ajaxUri.Manager.AddPost.Method,
                data:JSON.stringify({
                    "title":title,
                    "postDesc":postDesc,
                    "postContent":postContent,
                    "categoryId":categoryId,    //int
                    "tags":tags,
                    "desc":desc,
                    "kw":kw
                }),
                success:function(data){
                    alert("添加成功");
                    window.location.href = window.location.href;
                }
            });
        });
    }
    return {
        init:function(){
            handleTinymceInit();
            handleGetAllCategory();
            handleGetTags();
            handleSubmit();
            
        }
    }
}();