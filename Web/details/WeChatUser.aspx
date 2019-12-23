<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatUser.aspx.cs" Inherits="Web.details.WeChatUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../scripts/alljs.js"></script>
    <script src="../js/WeChatUser.js"></script>

</head>
<body>
    <form id="form1" class="easyui-form">
        <div id="div_id" style="display: none">
            <div>
                <table style="width: 100%; border: 0">
                    <tr>
                        <td>
                            <div id="tb_lzd" style="padding: 5px; height: auto">
                                OpenId：
                                <input class="easyui-textbox" id="txt_OpenId_Sel" style="width: 200px; height: 25px" />
                                <span></span>
                                推荐人OpenId：
                                <input class="easyui-textbox" id="txt_RecommendUserOpenid_Sel" style="width: 200px; height: 25px" />
                                <span></span>
                                我的推荐码：
                                 <input class="easyui-textbox" id="txt_MyRecommendCode_Sel" style="width: 200px; height: 25px" />
                                <span></span>
                                <a href="#" class="easyui-linkbutton" id="lbtn_get" data-options="iconCls:'icon-search'">Search</a>
                                <span></span>
                            </div>
                            <div class="easyui-datagrid" id="dg"></div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </form>
</body>
</html>
