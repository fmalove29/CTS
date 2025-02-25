$(document).ready(function () {
    $('#btnSave').on('click', async () => {
        let formData = new FormData();

        let movieId = $('#movieId').val();
        formData.append("Id", movieId);
        formData.append("Title", $('#movieTitle').val());
        formData.append("Description", $('#movieDescription').val());
        formData.append("ReleaseDate", $('#releaseDate').val());
        formData.append("EndDate", $('#endDate').val());

        
        let movieGenres = [];
        $('.genre:checked').each(function () {
            movieGenres.push({ MoviesId: movieId, GenresId: $(this).val() });
        });
        formData.append("MovieGenresJson", JSON.stringify(movieGenres));


        formData.append("MovieGenres", JSON.stringify(movieGenres)); // Backend should expect JSON and deserialize it

        
        let fileInput = $('#movieImg')[0].files[0];
        if (!fileInput) {
            alert("Please select an image file.");
            return;
        }
        formData.append("MovieImages", fileInput);

        try {
            const response = await axios.post('/Movie/PostMovie', formData, {
                headers: { 'Content-Type': 'multipart/form-data' }
            });

            console.log(response.data);
            Swal.fire({
                icon: 'success',
                title: 'Success!',
                text: 'Movie created successfully!'
            });
        } catch (error) {
            console.error(error);
            Swal.fire({
                icon: 'error',
                title: 'Error!',
                text: 'Something went wrong.'
            });
        }
    });
});
