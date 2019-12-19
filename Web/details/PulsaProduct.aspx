<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PulsaProduct.aspx.cs" Inherits="Web.details.PulsaProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/PulsaProduct.js"></script>

</head>
<body>
    <form id="form1" class="easyui-form">
        <div id="div_id" style="display: none">
            <div>
                <table style="width: 100%; border: 0">
                    <tr>
                        <td>
                            <div id="tb_lzd" style="padding: 5px; height: auto">
                                运营商：
                                <select id="txt_operator" class="easyui-combobox" data-options="editable:false" name="txt_operator" style="width: 100px;">
                                    <option value="">全部</option>
                                    <option value="4">TELKOMSEL</option>
                                    <option value="1">INDOSAT</option>
                                    <option value="2">XL</option>
                                    <option value="3">AXIS</option>
                                    <option value="5">SMARTFREN</option>
                                    <option value="6">THREE</option>
                                </select>
                                <span></span>
                                充值类型：
                                <select id="txt_types_Sel" class="easyui-combobox" data-options="editable:false" name="txt_types_Sel" style="width: 100px;">
                                    <option value="">全部</option>
                                    <option value="data">流量</option>
                                    <option value="pulsa">话费</option>
                                </select>
                                状态：
                                <select id="txt_status_Sel" class="easyui-combobox" data-options="editable:false" name="txt_status_Sel" style="width: 100px;">
                                    <option value="">全部</option>
                                    <option value="0">已禁用</option>
                                    <option value="1">已启用</option>
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
            <div id="dlg_pulsa_product" class="easyui-dialog" data-options="closed:true,modal:true" style="width: 500px; height: 350px;">
                <table>
                    <tr>
                        <td style="height: 3px">
                            <input type="hidden" id="txt_ID" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">供应商介绍：</td>
                        <td colspan="8">
                            <label id="lbl_pulsa_nominal"></label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">显示标签：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_cn_quatity" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">显示介绍：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_cn_op" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">供应商价格：</td>
                        <td colspan="8">
                            <label id="lbl_pulsa_price"></label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">显示价格：</td>
                        <td colspan="8">
                            <input type="text" class="easyui-validatebox" id="txt_cn_price" style="width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="text-align: right">启用状态：</td>
                        <td colspan="4">
                            <select class="easyui-combobox" id="txt_State" data-options="width:150,panelHeight:60,editable:false">
                                <option value="1">启用</option>
                                <option value="0">禁用</option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

