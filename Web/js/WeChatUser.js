$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/WeChatUser.ashx?i=cx" + Math.random(),
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
            wherestr: $("#txt_OpenId_Sel").val() + "," + $("#txt_RecommendUserOpenid_Sel").val() + "," + $("#txt_MyRecommendCode_Sel").val()
        },
        columns: [[
                    {
                        field: 'Headimgurl', title: '头像', width: 50, align: 'center',
                        formatter: function (value, row, index) {
                            return '<img width="50px" height="50px" border="0" src="' + value + '"/>';
                        },
                    },
                    { field: 'Nickname', title: '昵称', width: 100, align: 'right' },
                    { field: "Openid", title: 'OpenId', width: 200, align: 'right' },
                    {
                        field: 'Sex', title: '性别', width: 50, align: 'right',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "男";
                            } else if (value == 1) { return "女"; }
                        }
                    },
                    { field: 'Country', title: '国家', width: 100, align: 'right' },
                    { field: 'Province', title: '省份', width: 100, align: 'right' },
                    { field: 'City', title: '城市', width: 100, align: 'right' },
                    { field: 'RecommendUserOpenid', title: '推荐人OpenId', width: 200, align: 'right' },
                    { field: 'MyRecommendCode', title: '我的推荐码', width: 100, align: 'right' },

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



