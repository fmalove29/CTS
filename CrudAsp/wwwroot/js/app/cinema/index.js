$(document).ready(function(){
    $('#cinemaForm').on('submit', async function(e){
        e.preventDefault();

        let cinemaRequest = {
            CinemaName: $('#cinemaName').val(),  // Properly capitalized
            Location: $('#location').val()       // Correct value for location
        };
        let decider = 'PostCinema';
        let response = await postCinema(decider, cinemaRequest);

        console.log(response);
        if(response.success)
        {
            window.location.href = '/Cinema';
        }

        

    });

    async function postCinema(decider, data) {
        try {
            const d = await axios.post('/Cinema/' + decider, data);
            return d.data;
        } catch (error) {
            console.log(error);
        }
    }



    
    $('#searchInput').on('input', function() {
        const searchQuery = $(this).val(); // Get the input value
        
        axios.get('/Cinema/Cinemasearch', {
            params: { searchQuery: searchQuery } // Pass the query string as a parameter
        })
        .then((response) => {
            const cinemas = response.data; // Get the filtered cinemas from the response
            let cinemaRows = '';
            
            $('.cTbody').empty();
            // Create table rows for the cinema data

            if(cinemas.length != 0)
            {
                cinemas.forEach(cinema => {
                    cinemaRows += `
                        <tr>
                            <td>${cinema.id}</td>
                            <td>${cinema.cinemaName}</td>
                            <td>${cinema.location}</td>
                            <td>${cinema.created_at}</td>
                            <td>
                                <a class="btn btn-outline-info" href="/Cinema/Details/${cinema.id}">View</a>
                            </td>
                        </tr>
                    `;
                });
            }
            else
            {
                cinemaRows += `
                <tr>
                    <td colspan="5" class="text-center">No Data Found</td>
                </tr>`;
            }
            
    
            // Update the table body with the new cinema data
            $('.cTbody').html(cinemaRows);
        })
        .catch((err) => {
            console.error(err); // Handle any errors
        });
    });
    
    
    
    
});
