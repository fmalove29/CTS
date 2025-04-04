
$(document).ready(async function(){
    let hallId = '';
    let movieId = '';
    let cinemaId = '';
    let userId = '';
    let showId = '';
    let ticketPrice = 0;
    async function renderContent(searchTerm = '') {
        let shows = await getCinemaShow(searchTerm);
        console.log(shows);
        let container = $('#cinemaShowsContainer');
        container.html(''); // Clear existing content
    
        if (shows.length === 0) {
            container.html('<div class="d-flex justify-content-center"><p>No Cinema or Location Available.</p></div>'); // Handle empty response
            return;
        }
    
        let content = `
            <div class="accordion" id="cinemaAccordion">
                ${shows.map((cinema, index) => `
                    <div class="accordion-item mb-3">
                        <h2 class="accordion-header" id="heading${index}">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" 
                                data-bs-target="#collapse${index}" aria-expanded="false" 
                                aria-controls="collapse${index}">
                                ${cinema.cinemaName} - ${cinema.location}
                            </button>
                        </h2>
                        <div id="collapse${index}" class="accordion-collapse collapse" aria-labelledby="heading${index}" data-bs-parent="#cinemaAccordion">
                            <div class="accordion-body">
                                ${cinema.halls.map(hall => `
                                    <div class="mb-3">
                                        <ul class="list-group">
                                            ${hall.shows.map(show => `
                                                <li class="list-group-item">
                                                    <strong>Movie:</strong> ${show.movie.title} <br>
                                                    <strong>Show Date:</strong> ${new Date(show.showDate).toLocaleDateString()} <br>
                                                    <strong>Price:</strong> ₱${show.ticketPrice}
                                                    <br>
                                                    <div class="d-flex justify-content-start">
                                                        <button class="btn btn-netflix-secondary btnBookMovie" data-id="${show.id}"><strong>${new Date(show.showDate).toLocaleTimeString()} </strong><br></button>
                                                    </div>
                                                </li>
                                            `).join('')}
                                        </ul>
                                    </div>
                                `).join('')}
                            </div>
                        </div>
                    </div>
                `).join('')}
            </div>
        `;
    
        container.html(content);
    
        // Add event listener to change background color on open
        $('.accordion-button').on('click', function () {
            $('.accordion-button').removeClass('bg-danger text-white'); // Reset all headers
            if (!$(this).hasClass('collapsed')) {
                $(this).addClass('bg-danger text-white'); // Apply danger background when expanded
            }
        });
    }
    
    
    
    
    async function getCinemaShow(searchTerm) {
        try {
            let movieId = $('#showingMovie').val(); // Get the movie ID from the dropdown/select
            console.log(movieId);
            let response = await axios.get('/book/cinema', {
                params: { 
                    searchTerm: searchTerm,
                    MovieId: movieId
                }
            });
            return response.data;
        } catch (err) {
            console.log(err);
        }
    }
    
    
    
    renderContent();
    
    $('#searchCinemaId').on('input', async function () {
        let searchTerm = $(this).val();
        renderContent(searchTerm);
    });
    
})

$(document).on('click', '.btnBookMovie',async function(e){
    let dataShowId = $(e.currentTarget).data('id');

    let found = await getShowById(dataShowId);
    
    console.log(found);

    hallId = found.data.hallId;
    movieId = found.data.movieId;
    showId = dataShowId;
    cinemaId = found.data.cinemaId;
    ticketPrice = found.data.ticketPrice;
    userId = '';

    $('#cinemaName').text(`${found.data.hall.cinema.cinemaName}`);
    $('#cinemaLocation').text(`${found.data.hall.cinema.location}`);
    $('#costId').html(`Cost: <strong>${found.data.ticketPrice}</strong>`);
    $('#formatType').html(`Cinema Type : <strong>${ found.data.hall.cinemaFormat.screenTypeName}</strong>`);
    $('#bookModal').modal('show');
})

$('#bookModal').on('shown.bs.modal', function () {
    let qtyInput = $("#qtyVal");

    // Ensure the input has a default value
    if (!qtyInput.val()) {
        qtyInput.val(0);
    }

    function updateSubtotal() {
        let value = parseInt(qtyInput.val()) || 0;
        if (value < 0) value = 0; 
        if (value > 8) value = 8; 
        qtyInput.val(value); 

        let totalTicket = value * ticketPrice;
        $('#subTotal').text(totalTicket.toFixed(2));
    }

    $("#increase").off("click").on("click", function () {
        let value = parseInt(qtyInput.val()) || 0;
        if (value < 8) {
            qtyInput.val(value + 1);
            updateSubtotal();
        }
    });

    $("#decrease").off("click").on("click", function () {
        let value = parseInt(qtyInput.val()) || 0;
        if (value > 0) {
            qtyInput.val(value - 1);
            updateSubtotal();
        }
    });

    qtyInput.off("input").on("input", function () {
        updateSubtotal();
    });
});


async function getShowById(Id)
{
    try
    {
        let findShow = axios.get(`/book/${Id}`);
        return findShow;
    }
    catch(err)
    {
        console.log(err);
    }
}

