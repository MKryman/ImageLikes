$(() => {

    const id = $("#image-id").val();

    setInterval(() => {
        $.get("/home/getimage", { id }, image => {
            $("#likes-count").text(image.likes);
        })
    }, 500)

    $('.btn').on('click', function () {
        $.post("/home/updateLikes", { id }, function () {
            $('.btn').prop('disabled', true);
        })
    })


})