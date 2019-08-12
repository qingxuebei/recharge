<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/login.css" rel="stylesheet" />
    <script src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/login.js"></script>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
        <div class="pagewrap">
            <div class="main">
                <div class="header" style="height:100px;">
                </div>
                <div class="content" style="padding-left:25%; text-align:center">
                    <div class="con_right">
                        <div class="con_r_top">
                            <a href="javascript:;" class="right" style="color: rgb(51, 51, 51); border-bottom-width: 2px; border-bottom-style: solid; border-bottom-color: rgb(46, 85, 142);">Login</a>
                        </div>
                        <ul>
                            <li class="con_r_right" style="display: block; ">
                                <div class="user">
                                    <div>
                                        <input type="text" id="username" name="username" placeholder="　account" value="" runat="server" />
                                    </div>

                                    <div>
                                        <input type="password" id="password" name="password" placeholder="　password" value="" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <asp:Button Id="btn_Login" type="submit" runat="server" Text="Access" CssClass="loginbtn" OnClick="btn_cs_Click"></asp:Button>
                            </li>
                        </ul>

                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
