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
                // var extract_preselected_ids = function(element){
                //     console.log(element.val());
                    
                //     var preselected_ids = [];
                //         if(element.val())
                //         $(element.val().toString().split(",")).each(function () {
                //             preselected_ids.push({id: this});
                //         });
                //     console.log(preselected_ids);
                //     return preselected_ids;
                // };
                
                // var preselect = function(preselected_ids){
                //     var pre_selections = [];
                //     for(index in selections)
                //         for(id_index in preselected_ids)
                //             if (selections[index].id == preselected_ids[id_index].id)
                //                 pre_selections.push(selections[index]);
                    
                //     return pre_selections;
                // };
                
                $('#selTag').select2({
                    minimumInputLength: 0,
                    width:300,
                    multiple: true,
                    allowClear: true,
                    data: selections,  
                    // initSelection: function(element, callback){
                    //     //var preselected_ids = extract_preselected_ids(element);
                    //     var preselected_ids = [{"id":1},{"id":4}];
                    //     var preselections = preselect(preselected_ids);
                    //     console.log(preselections);
                    //     callback(preselections);
                    // }
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
            plugins: 'image,textcolor,codesample,colorpicker,fullscreen,link',
            toolbar: 'undo redo | forecolor backcolor | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image imagetools | codesample | fullscreen',
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
            plugins: 'image,textcolor,codesample,colorpicker,fullscreen,link',
            toolbar: 'undo redo | forecolor backcolor | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image imagetools | codesample | fullscreen',
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
            jqAjax.ajax({
                url:App.ajaxUri.Manager.AddPost.Uri,
                method:App.ajaxUri.Manager.AddPost.Method,
                data:JSON.stringify({
                    "title":title,
                    "postDesc":postDesc,
                    "postContent":postContent,
                    "categoryId":categoryId,    //int
                    "tags":tags
                }),
                success:function(data){

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