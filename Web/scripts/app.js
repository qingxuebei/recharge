

//获取数据库时间（年月日）
function ShortDatetime(time) {
    if (time == "" || time == "null" || time == null)
        return "";
    else if (time.length < 10)
        return "";
    else if (time.substr(0, 10) == "1900-01-01")
        return "";
    else
        return time.substr(0, 10);
}
function dateConvert(value) {
    var reg = new RegExp('/', 'g');
    var d = eval('new ' + value.replace(reg, ''));
    return new Date(d).format('yyyy-MM-dd')
}
//判断数据是否为空
function GetIsNull(Paramet) {
    if (Paramet == null || Paramet == "null" || Paramet == "") {
        return "";
    } else {
        return Paramet;
    }

}
//判断数字是否为空
function GetIsNum(Paramet) {
    if (Paramet == null || Paramet == "null" || Paramet == "") {
        return 0;
    } else {
        return Paramet;
    }

}


function GetTimeNum() {
    return (new Date()).valueOf();
}

function numIsNull(num) {
    if (num == null || num == "") {
        return 0;
    } else
        return num;
}

function IsThisYearMonth(yearMonth) {
    if (yearMonth == null || yearMonth == "") {
        return false;
    }
    var date = new Date;
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    month = (month < 10 ? "0" + month : month);
    var mydate = (year.toString() + month.toString());
    if (mydate == yearMonth) { return true; }
    return false;
}
function getCurrentYear() {
    return new Date().getFullYear().toString();
}
function getCurrentMonth() {
    var date = new Date;
    var month = date.getMonth() + 1;
    month = (month < 10 ? "0" + month : month);
    return month;
}
function StringIsNull(StrValue, ToStr) {
    if (StrValue == null || StrValue == "") {
        return ToStr;
    } else
        return StrValue;
}

