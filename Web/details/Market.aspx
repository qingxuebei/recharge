<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="Web.details.Market" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/Market.js"></script>

</head>
<body>
    <form id="form1" class="easyui-form">
        <div id="div_id" style="display: none">
            <div>
                <table style="width: 100%; border: 0">
                    <tr>
                        <td>
                            <div id="tb_lzd" style="padding: 5px; height: auto">
                                <input class="easyui-textbox" id="txt_Name_Sel" data-options="prompt: 'Product Name'" style="width: 200px; height: 25px" />
                                <span></span>
                                <select id="txt_State_Sel" class="easyui-combobox" data-options="editable:false"  name="txt_State_Sel" style="width: 100px;">
                                    <option value="">All</option>
                                    <option value="1">Enable</option>
                                    <option value="0">Disable</option>
                                </select>
                                <span></span>
                                <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">Search</a>
                                <span></span>
                                <a href="#" class="easyui-linkbutton" id="lbtn_add" data-options="iconCls:'icon-add'">Add</a>
                                <span></span>
                            </div>
                            <div class="easyui-datagrid" id="dg"></div>
                        </td>
                    </tr>

                </table>
            </div>
            <div id="dlg_market" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 500px; height: 350px;">
                <table>
                    <tr>
                        <td style="height: 3px"></td>
                    </tr>
                     <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">IconUrl：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_IconUrl" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">Name：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_Name" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">SmsUrl：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_SmsUrl" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">MaxMoney：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_MaxMoney" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">Tenure：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_Tenure" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">Rate：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_Rate" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">ApprovlTime：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_ApprovlTime" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">Disbursement：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_Disbursement" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">SortId：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-numberbox" id="txt_SortId" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="height: 3px">
                            <input type="hidden" id="txt_ID" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">State：</td>
                        <td colspan="4">
                            <select class="easyui-combobox" id="txt_State" data-options="width:150,panelHeight:60,editable:false">
                                <option value="1">Enable</option>
                                <option value="0">Disable</option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
