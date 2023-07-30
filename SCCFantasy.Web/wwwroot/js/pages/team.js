var toastService = new ToastService();
var myTeamGrid; var playersGrid;

$(document).ready(function () {
    playersGrid = document.getElementById("players-grid").ej2_instances[0];
    myTeamGrid = document.getElementById("my-team-grid").ej2_instances[0];

    myTeamIds.forEach(function (playerId) {
        var player = playersGrid.dataSource.find(x => x.Id == playerId);
        myTeamGrid.dataSource.push(player);
    });

    playersGrid.refresh();
    myTeamGrid.refresh();
});

$("#players-grid").on('click', '.btn[data-action=add-player]', function (e) {
    var playerId = parseInt(e.currentTarget.getAttribute('data-id'));
    var player = playersGrid.dataSource.find(x => x.Id == playerId);

    if (maxPlayersCountValidation(player) == false) {
        return;
    }

    myTeamGrid.dataSource.push(player);
    myTeamGrid.dataSource = myTeamGrid.dataSource.sort(function (a, b) { return a.PositionId - b.PositionId });
    myTeamGrid.refresh();
    playersGrid.refresh();
});

$("#my-team-grid").on('click', '.btn[data-action=remove-player]', function (e) {
    var playerId = parseInt(e.currentTarget.getAttribute('data-id'));
    var player = myTeamGrid.dataSource.find(x => x.Id == playerId);
    var index = myTeamGrid.dataSource.indexOf(player);

    myTeamGrid.dataSource.splice(index, 1);
    myTeamGrid.refresh();
    playersGrid.refresh();
});

$("#save-my-team").on('click', function () {
    var dataSource = myTeamGrid.dataSource;
    toastService.success(__messages.ModifySuccessfully);
    var submitUrl = window.serverUrl + "/Team/UpdateTeam";
    var teamIds = myTeamGrid.dataSource.map(x => x.Id).join(';');

    $.ajax({
        url: submitUrl,
        data: { team: teamIds },
        method: "POST",
        success: function (res) {
            if (res.success) {
                toastService.success(__messages.ModifySuccessfully);
            } else {
                toastService.error(__messages.DefaultError);
            }
        },
        error: function (err) {
            console.log(err);
            toastService.error(__messages.DefaultError);
        }
    });
});

function maxPlayersCountValidation(player) {
    var isValid = true;

    var playerPositionId = player.PositionId;
    var maxCount = __maxPlayerSetting.find(x => x.positionId == playerPositionId).maxCount;
    var currentCount = myTeamGrid.dataSource.filter(x => x.PositionId == playerPositionId).length;

    if (currentCount >= maxCount) {
        Swal.fire({
            title: 'Warning',
            text: 'Cannot have more than ' + maxCount + ' ' + player.PositionName,
            icon: 'warning',
            confirmButtonText: 'Ok'
        });

        return false;
    }

    var playerClubId = player.ClubId;
    var currentNumberPlayersOfClub = myTeamGrid.dataSource.filter(x => x.ClubId == playerClubId).length;

    if (currentNumberPlayersOfClub >= 3) {
        Swal.fire({
            title: 'Warning',
            text: 'Cannot have more than 3 players of ' + player.ClubName,
            icon: 'warning',
            confirmButtonText: 'Ok'
        });

        return false;
    }

    return isValid;
}