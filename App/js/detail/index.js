var baseUrl = getAddress();
var pulsaPerfixProductShow = "";
$(document).ready(function () {
    $(".flexslider").flexslider();
    $(".flexslider2").flexslider();

    jQuery.ajax({
        type: "get",
        url: baseUrl + "api/PulsaApi?=" + Math.random(),
        cache: false,
        dataType: "json",
        data: {},
        success: function (result) {
            pulsaPerfixProductShow = JSON.parse(result);
            //如果返回结果有手机号，由需要直接加载套餐选项出来

        },
        error: function (e) {
            console.log("ExpandMemertyDBRealtime.GetRealtimeData:" + e);
        }
    });
});

//输入完手机号触发
function upperCase() {
    var phoneNum = $("#txt_phone").val();
    getPulsaChannel(phoneNum);
    getPulsaProduct();
};


var Code = "";
var OperatorName = "";
var DataChannel = "";
var PulsaChannel = "";
var LogoUrl = "";
//根据手机号获取运营商及套餐信息
function getPulsaChannel(phoneNum) {
    //先判断手机号是否已输入
    if (phoneNum == "") {
        return;
    }
    var pulsaPerfixList = pulsaPerfixProductShow.pulsaPerfixList;
    if (pulsaPerfixList == null || pulsaPerfixList.length == 0) {
        //号码错误
        alert("号码错误");
    }

    if (phoneNum.indexOf('8') == 0) {
        phoneNum = "0" + phoneNum;
    }
    Code = phoneNum.substring(0, 4);

    for (var i = 0; i < pulsaPerfixList.length; i++) {
        if (Code == pulsaPerfixList[i].Code) {
            OperatorName = pulsaPerfixList[i].OperatorName;
            DataChannel = pulsaPerfixList[i].DataChannel;
            PulsaChannel = pulsaPerfixList[i].PulsaChannel;
            LogoUrl = pulsaPerfixList[i].LogoUrl;
            $("#img_logoUrl").attr("src", LogoUrl);
            break;
        }
    }
}

//获取套餐信息
function getPulsaProduct() {
    var pulsaProductShowList = pulsaPerfixProductShow.pulsaProductShowList
    if (pulsaProductShowList == null || pulsaProductShowList.length == 0) {
        alert("您输入的号码套餐为空");
        return;
    }
    $("#newtab1").html("");
    $("#newtab2").html("");
    var ulHtmlNewTab1 = " <ul v-if='list'>";
    var ulHtmlNewTab2 = " <ul v-if='list'>";
    //分类读取并渲染页面
    for (var i = 0; i < pulsaProductShowList.length; i++) {
        //找出充值话费的套餐
        if (pulsaProductShowList[i].pulsa_channel == PulsaChannel) {
            ulHtmlNewTab1 += " <li><a href=\"order.html\"><span>" + pulsaProductShowList[i].cn_quatity + "</span><span>仅售" + pulsaProductShowList[i].cn_price + "元</span></a></li>";
        }
        //找出充值流量的套餐
        if (pulsaProductShowList[i].pulsa_channel == DataChannel) {
            ulHtmlNewTab2 += " <li><span>" + pulsaProductShowList[i].cn_quatity + "</span><span>仅售" + pulsaProductShowList[i].cn_price + "元</span></li>";
        }
    }
    ulHtmlNewTab1 += " </ul>";
    ulHtmlNewTab2 += " </ul>";
    $("#newtab1").html(ulHtmlNewTab1);
    $("#newtab2").html(ulHtmlNewTab2);

}





function showTab(tid, s, e, id, li_id) {
    for (var i = s; i <= e; i++) {
        $("#" + id + i).css("display", "none");
        //remove on
        if (li_id != "") {
            $("#" + li_id + i).removeClass("on");
        }
    }
    $("#" + id + tid).css("display", "");
    $("#" + id + tid).fadeIn(500);
    //on
    if (li_id != "") {
        $("#" + li_id + tid).addClass("on");
    }
}