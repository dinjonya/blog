var ajaxUrl = {
    "pageSize":10,
    "local":{
        "Statistics": {
            "PUV":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/blogauthen/puv/","Method":"Get"}
        },
        "Index":{
            "GetBlogTitle":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/Blog/GetBlogTitle/","Method":"Get"},
            "GetPvUv":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/Blog/GetPvUv/","Method":"Get"}
        },
        "Manager": {
            "Login":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/Login/","Method":"Put"},
            "Authen":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/Authen/","Method":"Put"},
            "ChangeTitle":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/ChangeTitle/","Method":"Put"},
            "GetAllCategory":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/GetAllCategory/","Method":"Get"},
            "AddCategory":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/AddCategory/","Method":"Post"},
            "UploadImg":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/UploadImage/","Method":"Put"},
            "SelectAbout":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/SelectAbout/","Method":"Get"},
            "UpdateAbout":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/UpdataAbout/","Method":"Put"},
            "GetAllTags":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/GetAllTags/","Method":"Get"},
            "AddTag":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/AddTag/","Method":"Post"},
            "AddPost":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/AddPost/","Method":"Post"},
            "SelectPost":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/SelectPost/","Method":"Get"},
            "RemovePost":{"Uri":"http://127.0.0.1:18080/apiblog/api1.0/BlogManager/RemovePost/","Method":"Delete"}
        }
    },
    "online":{
        "Statistics": {
            "PUV":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/blogauthen/puv/","Method":"Get"}
        },
        "Index":{
            "GetBlogTitle":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/Blog/GetBlogTitle/","Method":"Get"},
            "GetPvUv":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/Blog/GetPvUv/","Method":"Get"}
        },
        "Manager": {
            "Login":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/Login/","Method":"Put"},
            "Authen":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/Authen/","Method":"Put"},
            "ChangeTitle":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/ChangeTitle/","Method":"Put"},
            "GetAllCategory":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/GetAllCategory/","Method":"Get"},
            "AddCategory":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/AddCategory/","Method":"Post"},
            "UploadImg":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/UploadImage/","Method":"Put"},
            "SelectAbout":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/SelectAbout/","Method":"Get"},
            "UpdateAbout":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/UpdataAbout/","Method":"Put"},
            "GetAllTags":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/GetAllTags/","Method":"Get"},
            "AddTag":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/AddTag/","Method":"Post"},
            "AddPost":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/AddPost/","Method":"Post"},
            "SelectPost":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/SelectPost/","Method":"Get"},
            "RemovePost":{"Uri":"http://101.201.232.99:18080/apiblog/api1.0/BlogManager/RemovePost/","Method":"Delete"}
        }
    }
};