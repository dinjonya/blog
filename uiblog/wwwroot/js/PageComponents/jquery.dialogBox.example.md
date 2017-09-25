# dialog example

1.simple dialogBox

```json
    $('#btn-simple').click(function(){
        $('#simple-dialogBox').dialogBox({
            content: 'dialog content text,image,html file'
        });
    })
```

2.stantard dialogBox

```json
    $('#btn-stantard').click(function(){
        $('#stantard-dialogBox').dialogBox({
            title: 'title text',
            hasClose: true,
            content: 'dialog content text,image,html file'
        });
    })
```

3.custom size dialogBox

```json
    $('#btn-custom-size').click(function(){
        $('#custom-size-dialogBox').dialogBox({
            width: 500,
            height: 300,
            title: 'title text',
            hasClose: true,
            content: '<ul><li>Support custom dialog box style.</li><li>on the high version of brwosers support a series of animation effect</li><li>Support adaptive popup content size</li><li>Support the standard and the modal dialog box</li><li>Support IE7+,Firefox3+,Chrome and Oprea</li></ul>'
        });
    })
```

4.auto close dialogBox

```json
    $('#btn-auto-close').click(function(){
        $('#auto-close-dialogBox').dialogBox({
            autoHide: true,
            time: 3000,
            title: 'title text',
            content: 'dialog content text,image,html file'
        });
    })
```

5.mask dialogBox

```json
    $('#btn-mask').click(function(){
        $('#mask-dialogBox').dialogBox({
            hasClose: true,
            hasMask: true,
            title: 'title text',
            content: 'dialog content text,image,html file'
        });
    })
```

6.btn dialogBox

```json
    $('#btn-btn').click(function(){
        $('#btn-dialogBox').dialogBox({
            hasClose: true,
            hasBtn: true,
            confirmValue: 'I am sure',
            confirm: function(){
                alert('this is callback function');
            },
            cancelValue: 'I will cancel',
            title: 'title text',
            content: 'dialog content text,image,html file'
        });
    })
```

7.effect dialogBox

```json
    $('#btn-btn').click(function(){
        $('#btn-dialogBox').dialogBox({
            width: 500,
            height: 260,
            autoHide: true,
            time: 3000,
            effect: 'flip-horizontal',
            title: 'title text',
            content: 'dialog content text,image,html file'
        });
    })
```

8.type dialogBox

```json
    $('#btn-type').click(function(){
        $('#type-dialogBox').dialogBox({
            type: 'correct'  //three type:'normal'(default),'correct','error',
            width: 300,
            height: 200,
            hasMask: true,
            autoHide: true,
            time: 3000,
            effect: 'fall',
            title: 'title text',
            content: 'dialog content text,image,html file'
        });
    })
```