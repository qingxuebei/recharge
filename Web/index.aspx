<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mangement</title>
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="scripts/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="scripts/themes/icon.css" />
    <script type="text/javascript" src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.easyui.min.js"></script>
    <script type="text/javascript" src='scripts/index.js'> </script>
    <script type="text/javascript" src="scripts/locale/easyui-lang-en.js"></script>
    <script type="text/javascript">

        var _menus = {
            "menus": [
                {
                    "menuid": "1",
                    "icon": "icon-sys",
                    "menuname": "H5 Market",
                    "menus": [
                        {
                            "menuid": "3",
                            "menuname": "Product",
                            "icon": "icon-nav",
                            "url": "/details/Market.aspx"
                        }, {
                            "menuid": "4",
                            "menuname": "User Access Record",
                            "icon": "icon-nav",
                            "url": "/details/AccessRecord.aspx"
                        }
                    ]
                }
        //{
        //    "menuid": "2",
        //    "icon": "icon-sys",
        //    "menuname": "数据查询",
        //    "menus": [{
        //        "menuid": "21",
        //        "menuname": "小组VIP概要",
        //        "icon": "icon-nav",
        //        "url": "/details/agentsSelect.aspx"
        //    }, {
        //        "menuid": "21",
        //        "menuname": "市场会员概要",
        //        "icon": "icon-nav",
        //        "url": "/details/agencysSelect.aspx"
        //    }, {
        //        "menuid": "21",
        //        "menuname": "区域会员概要",
        //        "icon": "icon-nav",
        //        "url": "/details/qyagencysSelect.aspx"
        //    }]
        //},
        //{
        //    "menuid": "3",
        //    "icon": "icon-sys",
        //    "menuname": "数据汇总",
        //    "menus": [
        //        {
        //            "menuid": "21",
        //            "menuname": "月度订单汇总",
        //            "icon": "icon-nav",
        //            "url": "/details/hzorders.aspx"
        //        }, {
        //            "menuid": "21",
        //            "menuname": "月度会员收入",
        //            "icon": "icon-nav",
        //            "url": "/details/income.aspx"
        //        }, {
        //            "menuid": "21",
        //            "menuname": "月度会员概要",
        //            "icon": "icon-nav",
        //            "url": "/details/huiyuanGaiyao.aspx"
        //        }
        //    ]
        //},
        //{
        //    "menuid": "4",
        //    "icon": "icon-sys",
        //    "menuname": "系统管理",
        //    "menus": [
        //        {
        //            "menuid": "3",
        //            "menuname": "产品管理",
        //            "icon": "icon-nav",
        //            "url": "/details/products.aspx"
        //        },
        //        {
        //            "menuid": "3",
        //            "menuname": "月度任务",
        //            "icon": "icon-nav",
        //            "url": "/details/logMonthCreate.aspx"
        //        }
        //    ]
        //},
            ]
        };

        //设置登录窗口
        function openPwd() {
            $('#w').window({
                title: '修改密码',
                width: 300,
                modal: true,
                shadow: true,
                closed: true,
                height: 200,
                resizable: false
            });
        }
        //关闭登录窗口
        function closePwd() {
            $('#w').window('close');
        }



        //修改密码
        function serverLogin() {
            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');

            if ($newpass.val() == '') {
                msgShow('System warning', '请输入密码！', 'warning');
                return false;
            }
            if ($rePass.val() == '') {
                msgShow('System warning', '请再输入一次密码！', 'warning');
                return false;
            }

            if ($newpass.val() != $rePass.val()) {
                msgShow('System warning', '两次输入的密码不一致，请重试', 'warning');
                return false;
            }
            $.ajax({
                datatype: "text",
                url: "/ashx/Base.ashx?i=" + Math.random(),
                data: {
                    type: "editpassword",
                    oldpwd: $("#Password1").val(),
                    newpwd: $('#txtNewPass').val()
                },
                success: function (mess) {
                    if (mess == "1") {
                        msgShow('System warning', "修改成功！", 'info');
                        closePwd()
                    } else {
                        msgShow('System warning', mess, 'info');
                    }
                    $newpass.val('');
                    $rePass.val('');
                    close();
                }
            });
        }

        $(function () {

            openPwd();

            $('#editpass').click(function () {
                $('#w').window('open');
            });

            $('#btnEp').click(function () {
                serverLogin();
            })

            $('#btnCancel').click(function () { closePwd(); })

            $('#loginOut').click(function () {
                $.messager.confirm('System warning', '确定退出登录?', function (r) {
                    $.ajax({
                        datatype: "text",
                        url: "/ashx/Base.ashx?i=" + Math.random(),
                        data: {
                            type: "loginout",
                        },
                        success: function (mess) {
                            if (mess) {
                                location.href = 'login.aspx';
                            }
                        }
                    });
                });
            })
        });



    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden" fit="true" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px; width: 100%; background: white; text-align: center;">
            <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background: #D2E0F2; z-index: 20000">
        <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px; text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px; padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
            <img src="images/loading.gif" align="absmiddle" />
            加载中，请稍后...
        </div>
    </div>
    <div region="north" split="true" border="false" style="overflow: hidden; height: 30px; background: url(images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%; line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float: right; padding-right: 20px;" class="head">欢迎
            <asp:Label ID="lbl_NAME" runat="server" Text=""></asp:Label>
            <a href="#" id="editpass">修改密码</a> <a href="#" id="loginOut">Sign Out</a></span>
        <span style="padding-left: 10px; font-size: 16px;">
            <img src="images/blocks.gif" width="20" height="20" align="absmiddle" />
            Mangement</span>
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2;">
        <div class="footer">
            Verson：V1.0     
        </div>
    </div>
    <div region="west" split="true" title="Menu" style="width: 180px;" id="west">
        <div id="nav">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="Welcome" style="padding: 20px; overflow: hidden; color: red;">
                <%--<h1 style="font-size: 24px;">君和软件</h1>--%>
                <iframe id="sysMain" frameborder="0" name="sysMain" width="100%" height="600px" src="details/backHome.aspx"></iframe>
            </div>
        </div>
    </div>
    <!--修改密码窗口-->
    <div id="w" class="easyui-window" title="Update Password" collapsible="false" minimizable="false"
        maximizable="false" icon="icon-save" style="width: 300px; height: 150px; padding: 5px; background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table cellpadding="3">
                    <tr>
                        <td>旧密码：
                        </td>
                        <td>
                            <input id="Password1" type="password" class="easyui-validatebox txt01" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td>新密码：
                        </td>
                        <td>
                            <input id="txtNewPass" type="password" class="easyui-validatebox txt01" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td>确认密码：
                        </td>
                        <td>
                            <input id="txtRePass" type="password" class="easyui-validatebox txt01" data-options="required:true" />
                        </td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)">Save</a>
                <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">Cancel</a>
            </div>
        </div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="tabupdate">tabupdate</div>

        <div class="menu-sep"></div>
        <div id="close">关闭</div>
        <div id="closeall">关闭所有</div>
        <div id="closeother">关闭其他</div>

        <div class="menu-sep"></div>
        <div id="closeright">关闭右侧</div>
        <div id="closeleft">关闭左侧</div>

        <div class="menu-sep"></div>
        <div id="exit">修改</div>
    </div>
</body>
</html>
&nbsp;
