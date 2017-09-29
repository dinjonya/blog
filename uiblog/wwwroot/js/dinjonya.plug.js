var DinPlug = function(){
    //ConvertUnixTimeToJsTime() 
    var formatTime = function (unixTime) {
        let unixtime = unixTime;
        let unixTimestamp = new Date(unixtime * 1000)
        let Y = unixTimestamp.getFullYear()
        let M = ((unixTimestamp.getMonth() + 1) > 10 ? (unixTimestamp.getMonth() + 1) : '0' + (unixTimestamp.getMonth() + 1))
        let D = (unixTimestamp.getDate() > 10 ? unixTimestamp.getDate() : '0' + unixTimestamp.getDate())
        let h = unixTimestamp.getHours();
        let m = unixTimestamp.getMinutes();
        let s = unixTimestamp.getSeconds();
        let toDay = Y + '-' + M + '-' + D +" "+h+":"+m+":"+s;
        return toDay
    }
    return {
        ConvertUnixTimeToJsTime:function(unixTime){
            return formatTime(unixTime);
        }
    }
}();