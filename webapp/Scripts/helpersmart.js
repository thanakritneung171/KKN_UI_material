

function MessageAlert(title, content, color, iconSmall, type) {
    if (type == true) {
        $.smallBox({
            title: title,
            content: content,
            color: color,
            iconSmall: iconSmall + "fa-2x fadeInRight animated",
            timeout: 5000
        });
    } else {
        $.smallBox({
            title: title,
            content: content,
            color: color,
            iconSmall: iconSmall + "fa-2x fadeInRight animated",
            timeout: 5000
        });
    }
}
function warningsmartconfirmmessagebox(string = '', content) {
    $.smallBox({
        title:/* "กำลัง" + */string,
        content: "<i class='fa fa-clock-o'></i> <i>" + content + "...</i>",
        color: "#C79121",
        iconSmall: "fa fa-shield fa-2x fadeInRight animated",


        //content: "<i class='fa fa-clock-o'></i> <i>2 seconds ago...</i>",
        //color: "#296191",
        //iconSmall: "fa fa-thumbs-up bounce animated",
        timeout: 5000
    });
}

function smartconfirmmessageboxbycase(stringY = '', stringN = '', callback) {
    $.SmartMessageBox({
        title: "Confirm!",
        content: "คุณต้องการ" + stringY + "หรือไม่",
        buttons: '[ไม่][ใช่]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "ใช่") {

            //$.smallBox({
            //    title: "กำลัง" + string,
            //    content: "<i class='fa fa-clock-o'></i> <i>You pressed Yes...</i>",
            //    color: "#659265",
            //    iconSmall: "fa fa-check fa-2x fadeInRight animated",
            //    timeout: 4000
            //});
            callback();
            //true;
        }
        if (ButtonPressed === "ไม่") {
            $.smallBox({
                title: stringN,
                content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 5000
            });
            //false;
        }
    });
}


function smartconfirmmessagebox(string = '', callback) {
    $.SmartMessageBox({
        title: "Confirm!",
        content: "คุณต้องการ" + string + "หรือไม่",
        buttons: '[ไม่][ใช่]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "ใช่") {

            //$.smallBox({
            //    title: "กำลัง" + string,
            //    content: "<i class='fa fa-clock-o'></i> <i>You pressed Yes...</i>",
            //    color: "#659265",
            //    iconSmall: "fa fa-check fa-2x fadeInRight animated",
            //    timeout: 4000
            //});
            callback();
            //true;
        }
        if (ButtonPressed === "ไม่") {
            $.smallBox({
                title: "ยกเลิกบันทึกข้อมูล",
                content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 5000
            });
            //false;
        }
    });
}

function completesmartconfirmmessagebox(string = '', content) {
    $.smallBox({
        title: "กำลัง" + string,
        content: "<i class='fa fa-clock-o'></i> <i>" + content + "...</i>",
        color: "#659265",
        iconSmall: "fa fa-check fa-2x fadeInRight animated",
        timeout: 5000
    });
}

function warningmartconfirmmessagebox(string = '', content) {
    $.smallBox({
        title: string,
        content: "<i class='fa fa-clock-o'></i> <i>" + content + "...</i>",
        color: "rgb(199, 145, 33)",
        iconSmall: "fa fa-check fa-2x fadeInRight animated",
        timeout: 5000
    });
}


function completesmallbox() {

    $.smallBox({
        title: "บันทึกสำเร็จ",
        content: "<i class='fa fa-clock-o'></i> <i>2 seconds ago...</i>",
        color: "#296191",
        iconSmall: "fa fa-thumbs-up bounce animated",
        timeout: 5000
    });

}

function smartconfirmmessageboxedit1(stringY = '', stringN = '', callbackY, callbackN) {
    $.SmartMessageBox({
        title: "Confirm!",
        content: "คุณต้องการ" + stringY + "หรือไม่",
        buttons: '[ไม่][ใช่]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "ใช่") {

            //$.smallBox({
            //    title: "กำลัง" + stringY,
            //    content: "<i class='fa fa-clock-o'></i> <i>You pressed Yes...</i>",
            //    color: "#659265",
            //    iconSmall: "fa fa-check fa-2x fadeInRight animated",
            //    timeout: 4000
            //});
            callbackY();
            //true;
        }
        if (ButtonPressed === "ไม่") {
            $.smallBox({
                title: stringN,
                content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 5000
            });
            callbackN();
            //false;
        }
    });
}

function completesmartconfirmmessagebox1(string = '', content) {
    $.smallBox({
        title: "กำลัง" + string,
        content: "<i class='fa fa-clock-o'></i> <i>" + content + "...</i>",
        color: "#659265",
        iconSmall: "fa fa-check fa-2x fadeInRight animated",
        timeout: 10000
    });
}