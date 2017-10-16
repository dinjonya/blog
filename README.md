# OdinSam's Blog

## 更新日志

### v1.01(2017-10-16)

+ ?显示文章信息 创建页面 调试接口
+ 解决markdown在html页面的解析问题，但是table依然不能解析
+ 解决markdown解析时序图的内容
+ tinymce添加了markdown sequence两个自定义工具栏，并实现对应功能
+ 添加文章  加入了description和keywords 字段

### v1.01(2017-10-13)

+ ?解决文本编辑器支持markdown的方案
+ 后台文章、标签、分类添加了删除操作
+ 所有页面添加了description和keywords
+ post表添加了description和keywords 字段
+ 添加文章  加入了description和keywords 字段
+ 找到了 页面 解析markdown 并 同时通过svg绘制 时序图的解决方案
+ 加入了百度统计

### v1.00(2017-10-12)

+ ?后台删除文章需要减去Tag表中文章数量
+ 完成Rss订阅页面
+ 修改归档的样式 强制换行 以及 行高
+ 添加文本编辑器字体选项
+ 添加文章后添加Tag表文章数量
+ 发布线上
+ 添加www.f2e8.com的跨域访问

### v0.15(2017-10-10)

+ ? 需要读取数据创建blog的rss页面
+ 完成mvc返回xml的扩展
+ 完成linq to xml 的atom rss的生成器

### v0.14(2017-10-09)

+ 完成分类菜单
+ 完成归档菜单
+ 完成标签菜单

### v0.13(2017-09-29)

+ 修改前台为cshtml
+ 完成页面url设计
+ 完成index页面post数据的读取和显示
+ 完成index页面 页面导航数字按钮的分页和显示

### v0.12(2017-09-27)

+ app.js 完成首页title读取，使用mongo缓存
+ 修改后台 修改about me 修改blogtitle 接口为 mongo缓存数据
+ app.js 完成Index页面 pv 和 uv的读取

### v0.12(2017-09-26)

+ 修改Post表PostDescription长度
+ 完成Post表添加、删除操作

### v0.12(2017-09-25)

+ 修正Tag表  添加PostNum字段 用来记录当前Tag对应的文章数量
+ 完成about me 页面 查询、修改Ajax操作
+ 完成Tag表 添加、查询ajax操作

### v0.12(2017-09-22)

+ 完成blog类别 添加
+ 完成blog about me 富文本编辑器的安装、调试、图片上传、内容的设置和获取
+ 完成后台图片上传的代码

### v0.11(2017-09-21)

+ 完成项目基础搭建
+ 框架基本完成 token认证、接口调用记录、cookie认证 （ 后续补充 消息队列 分布式消息同步、通信等机制 ）
+ 完成后台 登录、修改标题页面
+ 基本搭建  简单的js框架
+ 修复Cookie.js 的一些bug