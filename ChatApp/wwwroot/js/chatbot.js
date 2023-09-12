

function loadContent(url, container) {
    $.ajax({
        url: url,
        success: function (data) {
            $(container).html(data);
        }
    });
}

$(document).on('click', '#openUserList', function () {
    console.log("Users tab clicked");
    loadContent("/ChatApp/userList", '.UserListContainer');
});

$(document).on('click', '#openInductionUserList', function () {
    console.log("Induction Users tab clicked");
    loadContent("/ChatApp/InductionUser", '.InductionUserListContainer');
});


$(document).on('click', 'li a.ClickRow', function () {
    var userId = $(this).attr("data-UserGUID");
    console.log(userId);
    console.log("clicked");
    $('li a.ClickRow').removeClass('highlighted');
    $(this).addClass('highlighted');
    $.ajax({
        url: "/ChatApp/chatView?userId=" + userId,
        success: function (data) {
            $('.UserChatDetails').html(data);
        }
    });
});


$(document).on("click", 'li a.ClickRowForInductionUser', function () {
    var inductionUSerId = $(this).attr("data-InductionUserGUID");
    console.log(inductionUSerId);
    $('li a.ClickRowForInductionUser').removeClass('highlighted');
    $(this).addClass('highlighted');
    $.ajax({
        url: "/ChatApp/chatViewForInductionUser?userId=" + inductionUSerId,
        success: function (data) {
            $('.InductionUserChatDetails').html(data);
        }
    })
})

document.getElementById('openUserList').addEventListener('click', function () {
    Swal.fire({
        title: "click on perticular user to communicate ",
        icon: 'info',
        confirmButtonText: 'OK'
    });
});

document.getElementById('openInductionUserList').addEventListener('click', function () {
    Swal.fire({
        title: "click on particular induction user to communicate",
        icon: 'info',
        confirmButtonText: 'OK'
    });
});


$(document).on('click', '#post-message', function () {
    var toUserId = $(this).attr("data-toUserId");
    console.log(toUserId);
    var message = $('#message-text').val();
    console.log(message);

    if (message.trim() === "") {
        Swal.fire({
            title: "Please enter a message",
            icon: 'error',
            confirmButtonText: 'OK'
        });
        return; 
    }
        $.ajax({
            url: '/ChatApp/SaveComments',
            type: 'POST',
            data: {
                sendToUser: toUserId,
                messageTxt: message
            },
            success: function (data) {
                if (data === true) {
                    $('#message-text').val('');
                    Swal.fire({
                        title: "message sent successfully",
                        icon: 'success',
                        confirmButtonText: 'OK'
                    });
                    $.ajax({
                        url: "/ChatApp/chatView?userId=" + toUserId,
                        success: function (data) {
                            $('.UserChatDetails').html(data);
                        }
                    })
                }
            }
        });
});

$(document).on('click', '#post-message-for-inductionUser', function () {
    var toUserId = $(this).attr("data-toInductionUserId");
    console.log(toUserId);
    var message = $('#message-text-for-inductionUser').val();
    console.log(message);

    if (message.trim() === "") {
        Swal.fire({
            title: "Please enter a message",
            icon: 'error',
            confirmButtonText: 'OK'
        });
        return;
    }

    $.ajax({
        url: '/ChatApp/SaveCommnetsForInductionUser',
        type: 'POST',
        data: {
            sendToUserId: toUserId,
            messageText: message
        },
        success: function (data) {
            if (data === true) {
                $('#message-text-for-inductionUser').val('');
                Swal.fire({
                    title: "message sent successfully",
                    icon: 'success',
                    confirmButtonText: 'OK'
                });
                $.ajax({
                    url: "/ChatApp/chatViewForInductionUser?userId=" + toUserId,
                    success: function (data) {
                        $('.InductionUserChatDetails').html(data);
                    }
                })
            }
        }
    });
});

//-----
$(document).on("click", ".myButtons", function () {
    $('.myButtons').removeClass("activated");
    $('.myButtons').removeClass("fw-bold");
    $('.myButtons').removeClass("pb-2");
    $(this).addClass("activated");
    $(this).addClass("fw-bold");
    $(this).addClass("pb-2");
})

