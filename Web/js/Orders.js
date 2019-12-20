$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/Orders.ashx?i=cx" + Math.random(),
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
            wherestr: $("#txt_Name_Sel").val() + ","
                + $("#txt_Name_Sel").val() + ","
                + $("#txt_operator_Sel").combobox('getValue') + ","
                + $("#txt_types_Sel").combobox('getValue') + ","
                + $("#txt_WechatpayState_Sel").combobox('getValue') + ","
                + $("#txt_PulsaState_Sel").combobox('getValue')
        },
        columns: [[
                    {
                        field: 'WechatHeadimgurl', title: '头像', width: 50, align: 'center',
                        formatter: function (value, row, index) {
                            return '<img width="50px" height="50px" border="0" src="' + value + '"/>';
                        },
                    },
                    { field: "WechatOpenid", title: 'OpenId', width: 100, align: 'right' },
                    { field: "OrderId", title: '订单编号', width: 100, align: 'right' },
                    { field: "PulsaCode", title: '产品编号', width: 100, align: 'right' },
                    { field: 'OperatorId', title: '供应商名称', width: 100, align: 'right' },
                    { field: 'CnPulsatype', title: '充值类型', width: 100, align: 'right' },
                    { field: 'CnQuatity', title: '显示标签', width: 50, align: 'right' },
                    { field: 'CnOp', title: '显示介绍', width: 200, align: 'right' },
                    { field: 'UsePoints', title: '使用积分', width: 200, align: 'right' },
                    { field: 'OperatorId', title: '供应商名称', width: 100, align: 'right' },
                    { field: 'cn_price', title: '显示价格', width: 100, align: 'right' },
                    { field: 'masaaktif', title: '有效天数', width: 100, align: 'right' },
                    {
                        field: 'cn_status', title: '状态', width: 100, align: 'right',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "已禁用";
                            } else if (value == 1) { return "已启用"; }
                        }
                    },
                    {
                        field: 'operatorb', title: 'Edit', align: 'center', width: 70,
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0);" onclick="update(' + index + ')" style="text-decoration: none;color: #800080;">Edit</a>';
                        }
                    }
        ]],
        loadFilter: function (data) {
            if (data)
                return data;
        }
    });

    $('#lbtn_get').bind('click', function () {
        $('#dg').datagrid('load', {
            type: "get",
            wherestr: $("#txt_Name_Sel").val() + ","
               + $("#txt_Name_Sel").val() + ","
               + $("#txt_operator_Sel").combobox('getValue') + ","
               + $("#txt_types_Sel").combobox('getValue') + ","
               + $("#txt_WechatpayState_Sel").combobox('getValue') + ","
               + $("#txt_PulsaState_Sel").combobox('getValue')
        });
    });

    $('#dlg_pulsa_product').dialog({
        buttons: [{
            id: "btn_mbtj",
            text: 'Save',
            iconCls: "icon-ok",//{ mbmc: $("#txt_mb").val()}
            handler: function () {

                if ($("#txt_cn_quatity").val() == "") {
                    alert('显示标签不能为空！');
                    return;
                } if ($("#txt_cn_op").val() == "") {
                    alert('显示介绍不能为空！');
                    return;
                } if ($("#txt_cn_price").val() == "") {
                    alert('显示价格不能为空！');
                    return;
                }
                loadMbData();
                $.ajax({
                    datatype: "text",
                    url: "../ashx/PulsaProduct.ashx?i=" + Math.random(),
                    data: csdata,
                    success: function (mess) {
                        if (mess == "0") {
                            $.messager.alert("Tips", "保存成功！", "info");
                            $('#dlg_pulsa_product').dialog('close');
                            $("#lbtn_get").click();
                        } else {
                            alert(mess);
                        }
                    }
                });

            }
        }, {
            text: 'Cancel',
            iconCls: "icon-cancel",
            handler: function () {
                $('#dlg_pulsa_product').dialog('close');
            }
        }]
    });
});

function loadMbData() {
    var type = "add";
    if ($("#txt_ID").val() != "") {
        type = "update";
    }
    csdata = {
        type: type,
        pulsa_code: $("#txt_ID").val(),
        cn_quatity: $("#txt_cn_quatity").val(),
        cn_op: $("#txt_cn_op").val(),
        cn_price: $('#txt_cn_price').val(),
        cn_status: $("#txt_State").combobox('getValue')
    }
}


function update(index) {
    $('#dlg_pulsa_product').dialog('open').dialog('setTitle', 'VIEW');
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#txt_ID").val(row.pulsa_code),
        $("#lbl_pulsa_nominal").html(row.pulsa_nominal),
        $("#txt_cn_quatity").val(row.cn_quatity),
        $('#txt_cn_op').val(row.cn_op),

        $("#lbl_pulsa_price").html(row.pulsa_price),
        $("#txt_cn_price").val(row.cn_price),
        $('#txt_State').combobox('setValue', row.cn_status);
    }
}