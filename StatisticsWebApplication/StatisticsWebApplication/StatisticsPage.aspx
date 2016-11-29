<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsPage.aspx.cs" Inherits="StatisticsWebApplication.StatisticsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<table border="1">

    <tr>
        <td><asp:FileUpload id="FileUploadControl" runat="server" /></td>
        <td><asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" /></td>
    </tr>
    
    <tr>
        <td> <asp:Label runat="server" id="lblStatus" text="" ForeColor="Red"/></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" id="lblSelect" text="" ForeColor="Red"/></td>
        <td>
        </td>
    </tr>
    <tr>
        <td>            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="dllPerson" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblType" runat="server">
                            <asp:ListItem Selected="True">JSON</asp:ListItem>
                            <asp:ListItem>XML</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                </tr>
            </table></td>
        <td><asp:Button runat="server" id="btnShow" text="Show" OnClick="btnShow_Click"/></td>
    </tr>

</table>
    
    
    <br />
    </div>
    </form>
</body>
</html>
