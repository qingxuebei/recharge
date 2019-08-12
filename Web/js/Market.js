$(document).ready(function () {
    document.getElementById("div_id").style.display = "";

    $("#dg").datagrid({
        url: "../ashx/market.ashx?i=cx" + Math.random(),
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
                    {
                        field: 'IconUrl', title: 'IconUrl', width: 50, align: 'center',
                        formatter: function(value,row,index){
                            return '<img width="50px" height="50px" border="0" src="'+value+'"/>';  
                        }
                    },
                    { field: 'Name', title: 'Name', width: 100, align: 'left' },
                    { field: 'MaxMoney', title: 'MaxMoney', width: 100, align: 'left' },
                    { field: 'Tenure', title: 'Tenure', width: 100, align: 'left' },
                    { field: 'CreatePerson', title: 'CreatePerson', width: 100, align: 'left' },
                    {
                        field: 'CreateTime', title: 'CreateTime', width: 100, align: 'left',
                        formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                    },
                    { field: 'UpdatePerson', title: 'UpdatePerson', width: 100, align: 'left' },
                    {
                        field: 'UpdateTime', title: 'UpdateTime', width: 100, align: 'left',
                        formatter: function (value, row, index) { if (value) { return ShortDatetime(value) } }
                    },
                    { field: 'SortId', title: 'SortId', width: 100, align: 'left' },
                    {
                        field: 'State', title: 'State', width: 100, align: 'left',
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "Disable";
                            } else if (value == 1) { return "Enable"; }
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
            wherestr: $("#txt_Name_Sel").val() + "," + $("#txt_State_Sel").combobox('getValue')
        });
    });

    $("#lbtn_add").bind('click', function () {
        clear();
        $('#dlg_market').dialog('open').dialog('setTitle', 'ADD');
    });

    $('#dlg_market').dialog({
        buttons: [{
            id: "btn_mbtj",
            text: 'Save',
            iconCls: "icon-ok",//{ mbmc: $("#txt_mb").val()}
            handler: function () {

                if ($("#txt_IconUrl").val() == "") {
                    alert('Please enter the IconUrl！');
                    return;
                } if ($("#txt_Name").val() == "") {
                    alert('Please enter the name！');
                    return;
                } if ($("#txt_SmsUrl").val() == "") {
                    alert('Please enter the SmsUrl！');
                    return;
                } if ($("#txt_MaxMoney").val() == "") {
                    alert('Please enter the MaxMoney！');
                    return;
                } if ($("#txt_Tenure").val() == "") {
                    alert('Please enter the Tenure！');
                    return;
                }

                if ($("#txt_Rate").val() == "") {
                    alert('Please enter the Rate！');
                    return;
                } if ($("#txt_ApprovlTime").val() == "") {
                    alert('Please enter the ApprovlTime！');
                    return;
                } if ($("#txt_Disbursement").val() == "") {
                    alert('Please enter the Disbursement！');
                    return;
                } if ($("#txt_SortId").val() == "") {
                    alert('Please enter the SortId！');
                    return;
                } if ($("#txt_State").combobox('getValue') == "") {
                    alert('Please enter the Tenure！');
                    return;
                }
                loadMbData();
                $.ajax({
                    datatype: "text",
                    url: "../ashx/market.ashx?i=" + Math.random(),
                    data: csdata,
                    success: function (mess) {
                        if (mess == "0") {
                            $.messager.alert("Tips", "Save Success！", "info");
                            $('#dlg_market').dialog('close');
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
                $('#dlg_market').dialog('close');
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
        ID: $("#txt_ID").val(),
        IconUrl: $("#txt_IconUrl").val(),
        Name: $("#txt_Name").val(),
        SmsUrl: $('#txt_SmsUrl').val(),

        MaxMoney: $("#txt_MaxMoney").val(),
        Tenure: $("#txt_Tenure").val(),
        Rate: $("#txt_Rate").val(),
        ApprovlTime: $('#txt_ApprovlTime').val(),

        Disbursement: $("#txt_Disbursement").val(),
        SortId: $("#txt_SortId").val(),

        State: $('#txt_State').combobox('getValue')
    }
}

function clear() {
    $("#txt_ID").val(""),
    $("#txt_IconUrl").val(""),
    $("#txt_Name").val(""),
    $('#txt_SmsUrl').val(""),

    $("#txt_MaxMoney").val(""),
    $("#txt_Tenure").val(""),
    $("#txt_Rate").val(""),
    $('#txt_ApprovlTime').val(""),

    $("#txt_Disbursement").val(""),
    $("#txt_SortId").numberbox('setValue', 0),

    $('#txt_State').combobox('setValue', "");
}

function update(index) {
    $('#dlg_market').dialog('open').dialog('setTitle', 'VIEW');
    $("#dg").datagrid("selectRow", index);
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#txt_ID").val(row.ID),
        $("#txt_IconUrl").val(row.IconUrl),
        $("#txt_Name").val(row.Name),
        $('#txt_SmsUrl').val(row.SmsUrl),
      
        $("#txt_MaxMoney").val(row.MaxMoney),
        $("#txt_Tenure").val(row.Tenure),
        $("#txt_Rate").val(row.Rate),
        $('#txt_ApprovlTime').val(row.ApprovlTime),
      
        $("#txt_Disbursement").val(row.Disbursement),
        $("#txt_SortId").numberbox('setValue', row.SortId),
      
        $('#txt_State').combobox('setValue', row.State);
    }
}