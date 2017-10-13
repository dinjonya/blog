# [IKYC索引列表](http://10.10.141.51:4999/index.php?s=/3&page_id=3 "IKYC索引列表")

## 短信发送的规范规定

### 短信发送接口，使用发送者提供的模板以及模板中对应的值，向接受者发送短信信息。短信息信息其实是发送到了邵伟的短信发送接口，再发送给短信发送商。通过当前短信发送接口可以查询短信到达的成功率等短信发送指标。

### 当前短信发送接口默认重复短信在5分钟内部会重复提交发送，请注意！

### [短信发送接口文档](http://10.10.141.51:4999/index.php?s=/3&page_id=44 "短信发送接口文档")

### 说明：

### Key：Guid 当前发送信息的唯一标识.由当前接口生成，一遍入口查询短信发送到达率使用

### SendTime：接受发送请求的时间 UnixTime时间戳

### GetTime：发送到axon短信接口的时间。

### OverTime：成功调用运营商接口的时间

------------

### 短信发送流程

```sequence
user->Send\nApi: Request send
Send\nApi->Aop\nServer:validate
Aop\nServer->RabbitMQ\nServer: add in MQ
RabbitMQ\nServer->Send\nServer:get msg
Note right of Send\nServer: Send sms
Send\nServer-->Send\nApi:return Guid
user-->Send\nApi:select Result by guid
```

------------

### 短信发送所有接口返回统一格式对象：

```json
{
    "Value": {},
    "Formatters": [],
    "ContentTypes": [],
    "DeclaredType": "NCDF.Models.ConfigModels.ConfigModel, NCDF.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    "StatusCode": null,
    "ResultTime": "2017-03-02 13:25:36,2536"
}
```

### 其中 value中为返回给调用者的返回信息