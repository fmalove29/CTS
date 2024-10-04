// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.btnDelete').on('click', function(e){
    let id = $(e.currentTarget).data('id');
    

    axios.delete('/User/Delete', {
        params: {
            id: id // Replace with the actual user ID you want to delete
        }
    })
    .then(function (response) {
        console.log('User deleted successfully:', response);
        // Optionally, redirect or update the UI after deletion
    })
    .catch(function (error) {
        console.error('There was an error deleting the user:', error);
    });
    
})