<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowResult.aspx.cs" Inherits="StatisticsWebApplication.ShowResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function goBack() {
            window.history.back();
        }

        //function OnSuccess(response) {
        //    var table = $("#dvStatistics table").eq(0).clone(true);
        //    var statistics = response.d;
        //    $("#dvStatistics table").eq(0).remove();
        //    $(statistics).each(function () {
        //        $(".no", table).html(this.No);
        //        $(".name", table).html(this.Name);
        //        $(".surname", table).html(this.Surname);
        //        $(".gamewon", table).html(this.GameWon);
        //        $(".gamelose", table).html(this.GameLose);
        //        $(".draw", table).html(this.Draw);
        //        $("#dvStatistics").append(table).append("<br />");
        //        table = $("#dvStatistics table").eq(0).clone(true);
        //    });
        //}
</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtXml" runat="server" ForeColor="Red" TextMode="MultiLine" Height="100px" Width="100%" />
        <button onclick="goBack()"><- Back</button>
<%--    <div id = "dvStatistics">
    <table class="tblStatistics" cellpadding="2" cellspacing="0" border="1">
    <tr>
        <th>
            <b><u><span class="name"></span></u></b>
        </th>
    </tr>
    <tr>
        <td>
            <b>No: </b><span class="no"></span><br />
            <b>Name: </b><span class="name"></span><br />
            <b>Surname: </b><span class="surname"></span><br />
            <b>Game Won: </b><span class="gamewon"></span><br />
            <b>Game Lose: </b><span class="gamelose"></span><br />
            <b>Draw: </b><span class="draw"></span><br />
        </td>
    </tr>
</table>
</div>--%>
    </form>
</body>
</html>
