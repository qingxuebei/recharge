$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/accessRecord.ashx?i=cx" + Math.random(),
        toolbar: "#tb_lzd",
        striped: true,
        rownumbers: true,
        resizable: true,
        remoteSort: false,
        pagination: true,
        fitColumns: true,
        showFooter: true,
        singleSelect: true,
        pageSize: 20,
        pageList: [20, 30, 40, 50],
        queryParams: {
            type: "get",
            wherestr: $("#txt_Name_Sel").val() + "," + $("#txt_State_Sel").combobox('getValue')
        },
        columns: [[
                    { field: "ID", checkbox: false, hidden: true },
                    { field: 'UserIP', title: 'UserIP', width: 100, align: 'left' },
                    { field: 'VisitUrl', title: 'VisitUrl', width: 100, align: 'left' },
                    { field: 'MarketID', title: 'MarketID', width: 100, align: 'left' },
                    { field: 'MarketName', title: 'ProductName', width: 100, align: 'left' },
                    { field: 'MarketSmsUrl', title: 'MarketSmsUrl', width: 100, align: 'left' },
                    { field: 'CreateTime', title: 'CreateTime', width: 100, align: 'left' },
                    { field: 'Initialize', title: 'Initialize', width: 100, align: 'left' },

        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $('#lbtn_get').bind('click', function () {
        $('#dg').datagrid('load', {
            type: "get",
            wherestr: $("#txt_Name_Sel").val() + "," + $("#txt_State_Sel").combobox('getValue')
        });
    });


});