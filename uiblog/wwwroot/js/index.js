var Index = function() {
    var handleInit = function(){
        loadPosts(1);
    }
    var loadPosts = function(pi) {
        pi = pi==undefined ? 0 : pi;
        jqAjax.ajax({
            url:App.ajaxUri.Index.GetPosts.Uri+pi+"/"+Math.random(),
            method:App.ajaxUri.Index.GetPosts.Method,
            success:function(data){
                var pc = data;
                var postLst = createPostLst(data.Posts);
                $(".posts").html("");
                $(".posts").html(postLst);

                var pageRecord = createPageNav(data.PageCount,pi);
                $(".pagination").html("");
                $(".pagination").html(pageRecord);

                $(".pagination a").click(function(){
                    var selectedPage = $(this).attr("data-page");
                    console.log(selectedPage)
                    loadPosts(selectedPage);
                });
            }
        });
    }
    var createPostLst = function(posts){
        var html = "";
        for (var i in posts) {
            var post = posts[i];
            var dt = DinPlug.ConvertUnixTimeToJsTime(post.PostTime);
            var y = dt.split(" ")[0].split('-')[0];
            var M = dt.split(" ")[0].split('-')[1];
            var d = dt.split(" ")[0].split('-')[2];
            var h = dt.split(" ")[1].split(':')[0];
            var m = dt.split(" ")[1].split(':')[1];
            var s = dt.split(" ")[1].split(':')[2];
            var ymd = dt.split(" ")[0];
            var hms = dt.split(" ")[1];
            html += "<article class=\"post\">";
            html += "<header class=\"post-header\">";
            html += "<h2 class=\"post-title\">";
            html += "<a class=\"post-title-link\" href=\"/blog/post/"+y+"/"+M+"/"+post.Id+"/post.html\">";
            html += post.PostTitle;
            html += "</a>";
            html += "</h2>";
            html += "<div class=\"post-meta\">";
            html += "<span class=\"post-time\">";
            html += "<span class=\"post-meta-item-icon\">";
            html += "<i class=\"fa fa-calendar-o\"></i>";
            html += "</span> ";
            html += "<span class=\"post-meta-item-textdate\">发表于</span>";
            html += "<time title=\"Post created\" datetime=\""+ dt +"\"> ";
            html += "<a href=\"/blog/post/"+y+"/"+M+"/"+d+"/date.html\" rel=\"index\">"+ymd+"</a>";
            html += " </time>";
            html += "</span>";
            html += "<span class=\"post-category\">";
            html += "<span class=\"post-meta-divider\"> | </span>";
            html += "<span class=\"post-meta-item-icon\">";
            html += "<i class=\"fa fa-folder-o\"></i>";
            html += "</span> ";
            html += "<span class=\"post-meta-item-text\">分类于</span> ";
            html += "<span>";
            html += "<a href=\"/blog/category/"+ post.PostCategoryId +"/category.html\" rel=\"index\">";
            html += "<span itemprop=\"name\">"+ post.PostCategory +"</span>";
            html += "</a>";
            html += "</span>";
            html += "</span>";
            html += "</div>";
            html += "</header>";
            html += "<div class=\"post-body\">";
            html += post.PostDesc;
            html += "</div>";
            html += "<div class=\"post-button text-center\">";
            html += "<a class=\"btn\" href=\"/blog/post/"+y+"/"+M+"/"+post.Id+"/post.html\">";
            html += "阅读全文 »";
            html += "</a>";
            html += "</div>";
            html += "</article>";
       }
       return html;
    }
    var createPageNav = function(pageCount,currentPage){
        var component = "";
        //总页数>0
        if(pageCount>0)
            if(currentPage>1)
                component += "<a href=\"javascript:void(0)\" data-page=\""+(parseInt(currentPage)-1)+"\" class=\"pnleft\">←</a>";

            component +="<ul class=\"pnnums\">";

            if(currentPage>5){                
                for(var i =(pageCount-currentPage>=2?(currentPage-2):((currentPage-(4-(pageCount-currentPage)))>0?currentPage-(4-(pageCount-currentPage)):1));
                        i<=(pageCount-currentPage>2?currentPage+2:pageCount);i++){
                    component += "<li class=\""+(i==currentPage?"current":"")+"\"><a data-page=\""+i+"\" href=\"javascript:void(0)\">"+i+"</a></li>";
                    //component += "<li data-lp=\""+i+"\" class=\""+(i==currentPage?"disabled":"")+"\"><a href=\"javascript:void(0);\">"+i+"</a></li>";
                }
            }
            else{
                for(var i = 1;i<=(pageCount>5?5:pageCount);i++){
                    component += "<li class=\""+(i==currentPage?"current":"")+"\"><a data-page=\""+i+"\" href=\"javascript:void(0)\">"+i+"</a></li>";
                    //component += "<li data-lp=\""+i+"\" class=\""+(i==currentPage?"disabled":"")+"\"><a href=\"javascript:void(0);\">"+i+"</a></li>";
                }
            }
            component +="</ul>";
            if(currentPage<pageCount)
                component += "<a  data-page=\""+(parseInt(currentPage)+1)+"\" href=\"javascript:void(0)\" class=\"pnright\">→</a>";
                //component += "<li data-lp=\""+(currentPage+1)+"\" class=\"next\"><a href=\"javascript:void(0);\">»</a></li>";
            
            return component;
        }
        // if(pageCount>1)
        // <a href="javascript:void(0)" class="pnleft">←</a>
        // <ul class="pnnums">
        //     <li><a href="javascript:void(0)">1</a></li>
        //     <li><a href="javascript:void(0)">2</a></li>
        //     <li><a href="javascript:void(0)">3</a></li>
        //     <li><a href="javascript:void(0)">4</a></li>
        //     <li><a href="javascript:void(0)">5</a></li>
        //     <li><a href="javascript:void(0)">6</a></li>
        //     <li><a href="javascript:void(0)">7</a></li>
        //     <li><a href="javascript:void(0)">8</a></li>
        //     <li><a href="javascript:void(0)">9</a></li>
        // </ul>
        // <a href="javascript:void(0)" class="pnright">→</a>
    return {
        init:function(){
            handleInit();
        }
    }
}();