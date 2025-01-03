$(document).ready(function(){

    $('#btnSave').on('click', async () => {
        // let genresArr = [];
        // let movieGen = [];
        // $('.genre:checked').each(function() {
        //     let genName = $(this).data('id');
        //     genresArr.push({
        //         genreName : genName,
        //         Id : $(this).val()
        //     });

        //     movieGen.push({
        //         genresId : $(this).val()
        //     });
        // });
        
        // // start of file
        // var fileInput = $('#movieImg')[0];
        // var file = fileInput.files[0];

        // if (!file) {
        //     alert("Please select a file.");
        //     return;
        // }


        //     let imageArr = [{
        //         name: file.name,
        //         type: file.type,
        //         size: file.size,
        //         path: ''
        //     }];
        
        // // end of file

        //     let movieRequest = {
        //         Id : $('#movieId').val(),
        //         title: $('#movieTitle').val(),
        //         description: $('#movieDescription').val(),
        //         releaseDate: $('#releaseDate').val(),
        //         endDate: $('#endDate').val(),
        //         genres: genresArr,
        //         MovieImages: imageArr,
        //         MovieGenre: movieGen
        //     };

        //     // You can uncomment the following axios code for the POST request
        //     /*
        //     axios.post('/Movie/PostMovie', movieRequest, {
        //         headers: {
        //             'Content-Type': 'application/json'
        //         }
        //     })
        //     .then((res) => {
        //         console.log(res);
        //         Swal.fire({
        //             icon: 'success',
        //             title: 'Success!',
        //             text: 'Movie created successfully!'
        //         });
        //     })
        //     .catch((err) => {
        //         console.log(err);
        //     });
        //     */

    

        //         let decider = 'PostMovie';
        //         let update = await request(decider, movieRequest);
        //         console.log(update);

        let formData = new FormData();
        formData.append("Title", $('#movieTitle').val());
        formData.append("Description", $('#movieDescription').val());
        formData.append("ReleaseDate", $('#releaseDate').val());
        formData.append("EndDate", $('#endDate').val());

        // Add file(s)
        let fileInput = $('#movieImg')[0];
        if (fileInput.files.length > 0) {
            for (let i = 0; i < fileInput.files.length; i++) {
                formData.append("MovieImages", fileInput.files[i]);
            }
        }

        // return console.log(formData);
        // Send data to server
        axios.post('/Movie/PostMovie', formData, {
            headers: { 'Content-Type': 'multipart/form-data' }
        })
        .then((response) => {
            console.log(response.data);
            Swal.fire({ icon: 'success', title: 'Success!', text: response.data.message });
        })
        .catch((error) => {
            console.error(error);
            Swal.fire({ icon: 'error', title: 'Error!', text: 'Something went wrong.' });
        });

    });

    // async function request(decider, movie) {

    //     // return console.log(movie);
    //     try {
    //         const data = await axios.post('/Movie/' + decider, movie);
    //         console.log(data);
    //         return data.response;
    //     } catch(error) {
    //         console.log(error);
    //     }   
    // }
});



