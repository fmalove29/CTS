

$(document).ready(function(){

    $('#btnSave').on('click', () => {
        let genresArr = [];
        let movieGen = [];
        $('.genre:checked').each(function(e) {
            let genName = $(this).data('id');

            genresArr.push({
                genreName : genName,
                Id : $(this).val()
            });

            movieGen.push({
                genresId : $(this).val()
            })
        });
        
        var fileInput = $('#movieImg')[0];  // Get the input DOM element
        var file = fileInput.files[0];      // Get the first file from the FileList
        let imageArr = [];


        // return console.log(file);
        var reader = new FileReader();
        reader.onloadend = function () {
            // The Base64 encoded content of the image
            var base64String = reader.result.split(',')[1];

            let imageArr = [];
            imageArr.push({
                name: file.name,
                type: file.type,
                size: file.size,
                path : '',
                Base64File: base64String  // Base64 string sent in request
            });

            let movieRequest = {
                title: $('#movieTitle').val(),
                description: $('#movieDescription').val(),
                releaseDate: $('#releaseDate').val(),
                endDate: $('#endDate').val(),
                genres: genresArr,
                MovieImages: imageArr,
                MovieGenre: movieGen
            };

            // Send the request
            axios.post('/Movie/PostMovie', movieRequest, {
                headers: {
                    'Content-Type': 'application/json'  // Ensure that the content type is set to JSON
                }
            })
            .then((res) => {
                console.log(res);

                Swal.fire({
                    icon: 'success',
                    title: 'Success!',
                    text: 'Movie created successfully!'
                });
            })
            .catch((err) => {
                console.log(err);
            });
        };
        reader.readAsDataURL(file);  // Read the file as DataURL, which includes Base64 encoding

        
    });

})