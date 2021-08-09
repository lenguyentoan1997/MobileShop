
function loadImgAvatar(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgAvatar').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadImgThumbnail1(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (event) {
            $('#imgThumbnail1').attr('src', event.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadImgThumbnail2(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (event) {
            $('#imgThumbnail2').attr('src', event.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadImgThumbnail3(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (event) {
            $('#imgThumbnail3').attr('src', event.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadImgThumbnail4(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (event) {
            $('#imgThumbnail4').attr('src', event.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}












