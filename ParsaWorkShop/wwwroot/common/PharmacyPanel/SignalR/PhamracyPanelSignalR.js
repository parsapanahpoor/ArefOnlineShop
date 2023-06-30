
//#region Create Connection

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/Notification")
    .build();

//#endregion

//#region Write Incoming Methods From Hub 

connection.on("SendSupporterNotification", GetSupporterNotification);

//#endregion

//#region Start Connection And Check User Onile State

async function Start() {
    try {

        //If User In Connected 
        await connection.start();
        $("#disConnect").hide();
        $("#Online").show();

    }
    catch (e) {

        //If User Is Disconnected
        $("#disConnect").show();
        $("#Online").hide();
        ShowMessage("اعلان", "اتصال شما به اینترنت قطع شده است.", "error");
        setTimeout(Start, 6000);

    }
}

//When Connection Is Lost Try To Connect Again
connection.onclose(Start);
Start();

//#endregion

//#region Get Supporter Notification

function GetSupporterNotification(notificationModel) {

    //Play Sound
    var audio = {};
    audio["walk"] = new Audio();
    audio["walk"].src = "/common/sound/RingtoneGram.IR_1643188156_17148.mp3"
    audio["walk"].play();

    $(".contacts-list").prepend(`
        <li class="contact">
            <div class="contact-avatar">
                    <img src="/content/images/user/thumb/${notificationModel.userImage ? notificationModel.userImage : 'DefaultAvatar.png'}" />
             </div>
             <div class="contact-info">
                    <div class="contact-name">${notificationModel.username}</div>
                    <div class="contact-status" style="margin-bottom: 25px;">
                        <div class="status">${notificationModel.createNotificationDate}</div>
                    </div>
                    <div class="last-chat-time">
                        ${notificationModel.notificationText}
                    </div>
             </div>
        </li>
    `);

    //Send Browser Notification
    if (Notification.permission !== 'granted') {
        Notification.requestPermission();
    }

    if (Notification.permission === "granted") {
        var notification = new Notification("اعلان جدید",
            {
                icone: 'Site/Manifest/doctorfamLogo512.png',
                body: notificationModel.notificationText,
                image: 'Site/Manifest/doctorfamLogo512.png'
            });
        notification.onclick = function () {
            window.open('http://doctorfam.com');
        };
    }
}

//#endregion





