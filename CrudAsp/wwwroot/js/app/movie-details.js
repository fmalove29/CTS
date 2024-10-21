$(document).ready(function(){

    $('#btnSave').on('click', () => {
        let genresArr = [];
        let movieGen = [];
        $('.genre:checked').each(function() {
            let genName = $(this).data('id');
            genresArr.push({
                genreName : genName,
                Id : $(this).val()
            });

            movieGen.push({
                genresId : $(this).val()
            });
        });
        
        var fileInput = $('#movieImg')[0];
        var file = fileInput.files[0];

        if (!file) {
            alert("Please select a file.");
            return;
        }

        var reader = new FileReader();
        reader.onloadend = async function () {
            var base64String = reader.result.split(',')[1];

            let imageArr = [{
                name: file.name,
                type: file.type,
                size: file.size,
                path : '',
                Base64File: base64String
            }];

            let movieRequest = {
                Id : $('#movieId').val(),
                title: $('#movieTitle').val(),
                description: $('#movieDescription').val(),
                releaseDate: $('#releaseDate').val(),
                endDate: $('#endDate').val(),
                genres: genresArr,
                MovieImages: imageArr,
                MovieGenre: movieGen
            };

            // You can uncomment the following axios code for the POST request
            /*
            axios.post('/Movie/PostMovie', movieRequest, {
                headers: {
                    'Content-Type': 'application/json'
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
            */

            if($('#btnSave').text() == ' Update') {
                let decider = 'PutMovie';
                let update = await request(decider, movieRequest);
                console.log(update);
            }
        };
        reader.readAsDataURL(file);

    });

    async function request(decider, movie) {
        try {
            const data = await axios.put('/Movie/' + decider, movie);
            console.log(data);
            return data.response;
        } catch(error) {
            console.log(error);
        }   
    }
});
