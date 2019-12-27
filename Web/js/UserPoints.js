$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/UserPoints.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
        striped: true,
        rownumbers: true,
        resizable: true,
        remoteSort: false,
        pagination: true,
        fitColumns: true,
        showFooter: true,
        singleSelect: true,
        pageSize: 10,
        pageList: [10, 20, 40, 50],
        queryParams: {
            type: "get",
            wherestr: $("#txt_OrderId_Sel").val() + "," + $("#txt_ToOpenId_Sel").val() + "," + $("#txt_FromWechatOpenid_Sel").val()
        },
        columns: [[
                    { field: 'OrderId', title: '订单编号', width: 200, align: 'right' },
                    { field: "Openid", title: 'OpenId', width: 200, align: 'right' },
                    { field: 'Point', title: '积分', width: 100, align: 'right' },
                    { field: 'FromWechatOpenid', title: 'FromOpenId', width: 200, align: 'right' },
                    { field: 'FromWechatNickname', title: 'From用户昵称', width: 100, align: 'right' },
                    {
                        field: 'CreateTime', title: 'CreateTime', width: 100, align: 'left',
                        formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                    },
                    {
                        field: 'State', title: 'State', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "Disable";
                            } else if (value == 1) { return "Enable"; }
                        }
                    },

        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $('#lbtn_get').bind('click', function () {
        $('#dg').datagrid('load', {
            type: "get",
            wherestr: $("#txt_OpenId_Sel").val() + "," + $("#txt_RecommendUserOpenid_Sel").val() + "," + $("#txt_MyRecommendCode_Sel").val()
        });
    });

});