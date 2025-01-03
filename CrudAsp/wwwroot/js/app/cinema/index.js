class Hall{
    constructor(index, hallName, seatCapacity, shows)
    {
        this.index           = index,
        this.hallName        = hallName,
        this.seatCapacity    = seatCapacity,
        this.shows           = shows
    }
}

$(document).ready(function(){
        let hallIndexopen = 0;
        let hallIndex = 1;
        let halls = [];
        let shows = [];

    function generateHalls(halls) {
        // Clear any existing content in the target element
        $('#num_halls').empty();
    
        // Variable to store the generated HTML
        let hallsHTML = '';
    
        // Generate each hall item based on the halls parameter
        for (let i = 1; i <= halls; i++) {
            hallsHTML += `
                <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${i}">
                        <button class="accordion-button collapsed collapsedBtn" type="button" data-bs-toggle="collapse" data-id="${i}" data-name="hall# ${i}" data-bs-target="#collapse${i}" aria-expanded="false" aria-controls="collapse${i}">
                            Hall #${i}
                        </button>
                    </h2>
                    <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#num_halls">
                        <div class="accordion-body">
                            <strong>This is Hall #${i}'s accordion body.</strong> Customize this content to display relevant information about Hall #${i}.
                            <div class="d-flex justify-content-end mb-3">
                                <label for="#cnum" class="col-form-label mb-3">Number of Seats</label>
                                <div id="cnum">
                                    <button class="btn btn-secondary m-1 dSeat" id="dSeat">-</button>
                                    <input type="number" class="text-center w-25 num_seat" data-id="${i}" value="0">
                                    <button class="btn btn-secondary m-1 iSeat" id="iSeat">+</button>
                                </div>
                            </div>
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-primary btn-sm saveHall" data-id="${i}">Save</button>
                            </div> 
                        </div>
                    </div>
                </div>
            `;
        }
    
        // Append the generated HTML to the target container
        $('#num_halls').html(hallsHTML);
        $('.accordion-button').on('click', async function () {
            const hallId = $(this).data('id'); // Retrieve the data-id attribute for the button
            hallIndexopen = hallId;
            const hallInput = $(this).closest('.accordion-item').find('.num_seat').data('id');
            const capacity = $(this).closest('.accordion-item').find('.num_seat').val();
            // await getHallId(hallId, hallInput);
        });
    }
    


    $('#btnSave').on('click', async function(e){
        e.preventDefault();

        
        // halls.push();
        
        let cinemaRequest = {
            CinemaName: $('#cinemaName').val(), 
            Location: $('#location').val(),
            Halls :  halls

        };
        let decider = 'PostCinema';
        let response = await postCinema(decider, cinemaRequest);

        // if(response.success)
        // {
        //     window.location.href = '/Cinema';
        // }
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
    
    //Adding halls
    $('#iClub').on('click', function() {
        const input = $('#cInput'); 
        let currentValue = parseInt(input.val()) || 0; 
        let newValue = currentValue + 1;  
        input.val(newValue);  
    
        
        generateHalls(newValue);  
    });

    $('#dClub').on('click', function() {
        const input = $('#cInput'); 
        let currentValue = parseInt(input.val()) || 0; 
        let newValue; 
        if (currentValue > 0) {
            newValue = currentValue - 1;  
            generateHalls(newValue);  
            input.val(currentValue - 1);
        }
    });

    $('#cInput').on('input', function(e){
        let val = $(e.currentTarget).val();
        generateHalls(val);
    })
    


    //Adding Seats
    $(document).on('click', '.iSeat', async function () {
        const input = $(this).closest('.accordion-item').find('.num_seat'); 
        let currentValue = parseInt(input.val()) || 0; 
        let newValue = currentValue + 1;  
        console.log("Incremented value:", newValue);
        input.val(newValue);
        
        
    });
    
    $(document).on('click', '.dSeat', async function () {
        const input = $(this).closest('.accordion-item').find('.num_seat'); 
        let currentValue = parseInt(input.val()) || 0; 
        if (currentValue > 0) {
            let newValue = currentValue - 1;  
            console.log("Decremented value:", newValue);
            input.val(newValue);
        }
    });
    

    async function getHallId(Id, hallinput)
    {
        // console.log(Id);
        // console.log(hallinput);
        if(Id == hallinput)
        {
            const loopCount = await parseInt($('#cInput').val()) || 0; // Get input value and default to 0 if empty or invalid
            generateHalls(loopCount); // Initialize an empty array
            

            for (let i = 0; i < loopCount; i++) {
                let hallName = `cinema${i+1}`;
                let cntxt = {
                    [`cinema${i + 1}`]: `${i + 1}` // Creates dynamic property like cinema1, cinema2, etc.
                };


                // let hall = new Hall();
                // hall.hallName = hallName;
                // hall.seatCapacity = seatcapacity;

            }
        }
    }

    // $(document).on('click', '.saveHall', (e)=>{
    //     if($(e.currentTarget).data('id') == hallIndex)
    //     {
    //         alert('yes');
    //     }
    // })
    $(document).on('click', '.saveHall', (e) => {

        
        const hallId = $(e.currentTarget).data('id'); // Get the data-id of the clicked button
        let findHall = halls.find(x => x.index == hallId);


        if (hallId == hallIndexopen) {
            const numSeatValue = $(e.currentTarget)
            .closest('.accordion-body') // Find the closest accordion body
            .find('.num_seat') // Locate the input with class num_seat
            .val(); // Get the value of the input

            const hallName = $(e.currentTarget).closest('.accordion-item').find('.collapsedBtn').data('name');

            if(findHall)
            {
                findHall.seatCapacity = parseInt(numSeatValue);
            }
            else{
                let hall = new Hall();
                hall.index              = hallIndex; 
                hall.hallName           = hallName;
                hall.seatCapacity       = parseInt(numSeatValue);
                hall.shows              = shows;
    
                halls.push(hall);
                hallIndex++;
    
                
            }

            console.log(halls);
        }
    });
    
});
