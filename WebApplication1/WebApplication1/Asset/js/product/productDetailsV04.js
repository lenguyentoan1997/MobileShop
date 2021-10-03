/*
 * Detail Product
 */
$('#btnOpenDetailImg').click(function () {
    document.getElementById("detailProductImgs").style.opacity = "1";
    document.getElementById("detailProductImgs").style.zIndex = "9999";

});
$('#closeDetailImg').click(function () {
    document.getElementById("detailProductImgs").style.opacity = "0";
    document.getElementById("detailProductImgs").style.zIndex = "-1";
});

/*
 * Detail Img Product slider
 */
$('.single-img-slider').slick({
    autoplay: true,
    autoplaySpeed: 2000,
    dots: true,
    infinite: true,
    slidesToShow: 1,
    pauseOnHover: false,
    prevArrow: $('#prevDetailImg'),
    nextArrow: $('#nextDetailImg'),

});

/*
 * Display Img when click ImgThumbnail
 */
function clickImgThumbnail(n) {
    displayImg(slideIndex = n)
}

function displayImg(n) {
    var i;
    var x = document.getElementsByClassName("img-slides");
    if (n < 1) {
        slideIndex = x.length;
    }
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    x[slideIndex - 1].style.display = "block";
}

/*hover name comment tooltip*/
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});
/*Load Comment*/
document.addEventListener("DOMContentLoaded", function (event) {
    var scrollpos = localStorage.getItem('scrollpos');
    if (scrollpos) window.scrollTo(0, scrollpos);
});
window.onbeforeunload = function (e) {
    localStorage.setItem('scrollpos', window.scrollY);
};
/*Similar Product*/
$('.center').slick({
    autoplay: true,
    autoplaySpeed: 1500,
    centerMode: true,
    centerPadding: '60px',
    slidesToShow: 3,
    prevArrow: $('.prev'),
    nextArrow: $('.next'),
    responsive: [
        {
            breakpoint: 768,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '40px',
                slidesToShow: 3
            }
        },
        {
            breakpoint: 480,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '40px',
                slidesToShow: 1
            }
        }
    ]
});

//checked star by grade point average
$('#titleAveragePoint').ready(function () {
    var titleAveragePoint = $('#titleAveragePoint');
    var averagePoint = titleAveragePoint.data("average-point");
    if (averagePoint != 0) {
        for (var i = 1; i <= averagePoint; i++) {
            $('.average-star-rate-' + i).addClass("checked");
        }
    }
});

//cursor at end of text
$('.txtarea-comment-content').focus(function () {
    var that = this;
    setTimeout(function () { that.selectionStart = that.selectionEnd = 10 }, 0);
});

$('.total-result').ready(function () {
    var resultDisplay = $('.total-result');
    //Select single star
    var star1 = $('.star-1').attr('data-star', 'Bad');
    var star2 = $('.star-2').attr('data-star', 'Worse');
    var star3 = $('.star-3').attr('data-star', 'Average');
    var star4 = $('.star-4').attr('data-star', 'Good');
    var star5 = $('.star-5').attr('data-star', 'Great');
    var allStars = [star1, star2, star3, star4, star5];
    function getValue(element, dataName) {
        element.on('click', function () {
            var currentStarData = $(this).data(dataName);
            resultDisplay.removeClass("move");
            if (currentStarData >= 'Bad' || currentStarData != 'Bad') {
                $(this).prevAll('.star').addClass('checked');
                $(this).addClass("checked");
                resultDisplay.addClass("move");
            }
            if (currentStarData <= 'Greate' || currentStarData != 'Greate') {
                $(this).nextAll('.star').removeClass('checked');
                $(this).addClass("checked");
                resultDisplay.addClass("move");
            }
            resultDisplay.val(currentStarData);
        });
    }
    for (var i = 0; i < allStars.length; i++) {
        getValue(allStars[i], "star");
    }
});

//Edit Comment
function btnEditComment_(itemId) {
    var id = itemId;
    var productId = $('#btnEditComment_' + id).data("product-id");

    var btnEditComment = document.getElementById("btnEditComment_" + id);
    if (btnEditComment.value === "Edit") {
        $("#txtareaCommentContent_" + id).prop("disabled", false).focus();

        $("#btnCancelComment_" + id).prop("hidden", false);
        $("#btnEditComment_" + id).val("Save");

        $("#btnCancelComment_" + id).click(function () {
            $("#txtareaCommentContent_" + id).prop("disabled", true);

            $("#btnCancelComment_" + id).prop("hidden", true);

            $("#btnEditComment_" + id).val("Edit");
        });
    } else {
        $("#txtareaCommentContent_" + id).prop("disabled", true);
        $("#btnCancelComment_" + id).prop("hidden", true);
        $("#btnEditComment_" + id).val("Edit");

        var listProduct = $('#txtareaCommentContent_' + id);
        var commentList = [];
        $.each(listProduct, function (i, item) {
            /*The object here must be the same as the object in Cart to be mapping*/
            commentList.push({
                CommentContent: $(item).val(),
                Id: $(item).data('id'),
            });
        });

        $.ajax({
            url: '/Comment/Edit',
            data: { commentModel: JSON.stringify(commentList) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.isStatus == true) {
                    window.location.href = "/Product/Details/" + productId;
                }
            }
        })
    }
}

//Delete Comment
function btnYesDeleteComment_(itemId) {
    var id = itemId;
    var productId = $('.btn-yes-delete-comment').data("product-id");

    $('.btn-yes-delete-comment').click(function () {
        var commentDeleteList = [];
        var listProduct = $('#txtareaCommentContent_' + id);
        $.each(listProduct, function (i, item) {
            commentDeleteList.push({
                CommentContent: $(item).val(),
                Id: $(item).data('id'),
            });
        });

        $.ajax({
            url: '/Comment/Delete',
            data: { commentModel: JSON.stringify(commentDeleteList) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.isStatus == true) {
                    window.location.href = "/Product/Details/" + productId;
                }
            }
        });
    });
}



$(".btn-like").on('click touchstart', function () {
    var productId = $('.btn-like').data('product-id');

    $.ajax({
        url: '/Product/UpdateProductLike',
        data: { id: String(productId) },
        dataType: 'json',
        type: 'POST',
    });
});

/*
 * when the animation is over, remove the class
 */
$(".btn-heart").on('animationend', function () {
    $(this).toggleClass('btn-heart-animating');
});


