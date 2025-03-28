$(document).ready(function(){
    let hallarr = [];
    let gHallId;
    let showHallId;
    let showIdx;
    let hallPrice;
    let ticketPrice;
    let currentItemOpen = null; 
    populateHall();
    async function getHall()
    {
        try
        {
            let response = await axios.get('/hall/'+ $('#cinemaId').val());
            return response.data;
        }
        catch(err)
        {
            console.error(err);
        }
    }

    async function populateHall() {
        let data = await getHall();
        console.log(data);
    
        hallarr = [];
        data.forEach((e)=>{
            hallarr.push({
                Id : e.id,
                HallName : e.hallName,
                SeatCapacity : e.seatCapacity,
                created_at : e.created_at,
                CinemaFormatId : e.cinemaFormatId,
                ScreenTypeName : e.screenTypeName,
                TicketPrice : e.ticketPrice,
                action: ''
            })
        })
    
        $('#hTable').DataTable().destroy();
        let table = $('#hTable').DataTable({
            paging: false,
            searching: false,
            info: false,
            lengthChange: false,
            data: hallarr,
            columns: [
                {
                    className: 'dt-control',
                    orderable: false,
                    data: null,
                    defaultContent: ''
                },
                { data: 'HallName', title: 'Name' },
                { data: 'SeatCapacity', title: 'Seat Capacity' },
                { data: 'ScreenTypeName', title: 'Screen Type' },
                { data: 'action', title: 'Action' }
            ],
            columnDefs: [
                {
                    targets: 4, // Correct index for the action column
                    render: function (data, type, row) {
                        return `<button class="btn btn-netflix-secondary editbtn" data-id="${row.Id}">Edit</button>`;
                    }
                }
            ],
            order: [[1, 'asc']]
        });
    
        
    
        $('#hTable tbody').on('click', 'td.dt-control', async function (e) {
            let tr = e.target.closest('tr');
            let row = table.row(tr);

            if (currentItemOpen && currentItemOpen !== tr) {
                let prevRow = table.row(currentItemOpen);
                if (prevRow.child.isShown()) {
                    prevRow.child.hide();
                    currentItemOpen.classList.remove('details');
                }
            }
    
            if (row.child.isShown()) {
                row.child.hide();
                tr.classList.remove('details');
                currentItemOpen = null;  
            } else {
                tr.classList.add('details');
                currentItemOpen = tr;
                
                showHallId = row.data().Id;
                hallPrice = row.data().TicketPrice;
                let show = await getShowByHallId(row.data().Id);
                row.child(format(show)).show();
            }
        });
    }
    
    // Function to format row details
    function format(showData) {
        if (!showData || showData.length === 0) {
            return `<div class="card p-3 mb-3"><h5 class="text-center">No shows available.</h5></div>
            <div class="d-flex justify-content-center"><button class="btn btn-netflix-primary btnAddShow">Add</button></div>`;
        }
    
        let content = `
                        <div id="showContainer">
                        </div>
                        <div class="card shadow-sm">
                        <div class="card-header bg-danger text-white">
                            <h5 class="mb-0">Showing.....</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">`;
    
        showData.forEach(s => {
            const imagePath = s.movie.movieImages.length > 0 ? s.movie.movieImages[0].path : "default-image.png";
            const fullImagePath = `http://localhost:5103/${imagePath}`;
    
            content += `
                <div class="row mb-3">
                    <div class="col-12">
                        <div class="card border-secondary">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-md-4 text-center">
                                        <img src="${fullImagePath}" class="img-fluid rounded border" alt="${s.movie.title}" style="max-width: 120px; height: auto;">
                                    </div>
                                    <div class="col-md-4">
                                        <h6 class="card-title">Movie: ${s.movie.title}</h6>
                                        <p class="card-text">
                                            <strong>Date:</strong> ${formatDateTime(s.showDate)} <br>
                                            <strong>Price:</strong> ₱${s.ticketPrice}
                                        </p>
                                    </div>
                                    <div class="col-md-4">
                                        <button class="btn btn-netflix-secondary btnShowDetails" data-id="${s.id}">Reschedule</button>
                                        <button class="btn btn-netflix-secondary btnDeleteShow" data-id="${s.id}">Remove</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
    
        content += `</div></div></div><br>`;
        content += `<div class="d-flex justify-content-center"><button class="btn btn-netflix-primary btnAddShow">Add</button></div>`;
    
        return content;
    }
    $(document).on('click', '.btnShowDetails', async function(e){
        let showId  = $(e.currentTarget).data('id');
        let show = await getShowById(showId);
        showIdx = showId;
        $('#btnShowSave').text('Update');
    
        // If the movie option is not in the select, add it
        if($('#movieSelect').find(`option[value="${show.data.movie.id}"]`).length === 0) {
            let newOption = new Option(show.data.movie.title, show.data.movie.id, false, false);
            $('#movieSelect').append(newOption).trigger('change');
        }
        
        $('#movieSelect').val(show.data.movie.id).trigger('change');
        // Use the correct disabled property name
        $('#movieSelect').prop('disabled', true);
        // No need to reinitialize select2 if it was already initialized.
        $('#showDate').val(show.data.showDate);
        $('#addShowModal').modal('show');
    });
    async function getShowById(showId)
    { 
        try{
            let show = axios.get(`/show/${showId}`);
            return show;
        }
        catch(err)
        {
            console.log(err);
        }
    }

    function formatDateTime(dateString) {
        const date = new Date(dateString);
        
        const optionsDate = { year: 'numeric', month: 'long', day: 'numeric' };
        const formattedDate = date.toLocaleDateString('en-US', optionsDate);
        
        const optionsTime = { hour: '2-digit', minute: '2-digit', hour12: true };
        const formattedTime = date.toLocaleTimeString('en-US', optionsTime);
    
        return `${formattedDate} ${formattedTime}`;
    }
    
    $(document).on('click', '.btnAddShow', function(){
        // Clear the movie select input and enable it
        $('#btnShowSave').text('Save');
        $('#movieSelect').val(null).trigger('change');
        $('#movieSelect').prop('disabled', false);
        // If needed, you can call .select2('destroy') and reinitialize it,
        // but if it's already initialized, updating the underlying property should suffice.
        
        // Clear other fields
        $('#showDate').val('');
        $('#addShowModal').modal('show');
    });
    
    
    
    $('#btnShowSave').on('click',async function(){

        if($('#btnShowSave').text() == 'Update')
        {
            let show = {
                Id : showIdx,
                MovieId : $('#movieSelect option:selected').val(),
                HallId : showHallId,
                ShowDate : $('#showDate').val(),
                TicketPrice : hallPrice
            }

            let updateShow = await putShow(show);
            
            if(updateShow.data.success)
            {
                Swal.fire({
                    title : "Success",
                    text : updateShow.data.message,
                    icon : "success"
                })
                .then(async ()=>{
                    let dataShow = await getShowByHallId(updateShow.data.hallId);
                    if (currentItemOpen) {
                        let table = $('#hTable').DataTable();
                        let row = table.row(currentItemOpen);
                        
                        if (row.child.isShown()) {
                            row.child(format(dataShow)).show();
                        }
                    }
                
                    $('#addShowModal').modal('hide');
                })
            }
            return updateShow;
        }
        else{
            let show = {
                MovieId : $('#movieSelect option:selected').val(),
                HallId : showHallId,
                ShowDate : $('#showDate').val(),
                TicketPrice : hallPrice
            }
            

            let s = await postShow(show);
            if(s.data.success)
            {
                Swal.fire({
                    title : "Success",
                    text : s.data.message,
                    icon : "success"
                })
                .then(async ()=>{
                    let dataShow = await getShowByHallId(s.data.hallId);
                    console.log(dataShow);
                    if (currentItemOpen) {
                        let table = $('#hTable').DataTable();
                        let row = table.row(currentItemOpen);
                        
                        if (row.child.isShown()) {
                            row.child(format(dataShow)).show();
                        }
                    }
                
                    $('#addShowModal').modal('hide');
                })
            }
            return s;
        }
    })
    async function putShow(showData)
    {
        try
        {
            let show = await axios.put('/show/Put', showData);
            return show;
        }
        catch(err)
        {
            console.log(err);
        }
    }

    async function postShow(showData)
    {
        try
        {
            let addshow = axios.post('/show/Post',showData);
            return addshow;
        }
        catch(err)
        {
            console.log(err);
        }
    }



    $('#movieSelect').select2({
        width: '100%', // Optional: Ensures Select2 fits properly
        placeholder: "Select Movie",
        tags: false,
        ajax: {
            transport: function (params, success, failure) {
                axios.get("/movie-select/", {
                    params: { searchTerm: params.data.term}
                })
                .then(response => {
                    console.log(response.data);
                    success({
                        results: response.data.map(movie => ({
                            id: movie.id,  // Make sure this matches backend
                            text: movie.title
                        }))
                    });
                })
                .catch(error => {
                    failure(error);
                });
            },
            processResults: function (data) {
                return { results: data.results }; // Ensure proper result structure
            },
            delay: 250
        }
    }).on('select2:select', function (e) {
        let selectedFormat = e.params.data; // Get selected cinema object
        console.log("🎯 Selected Cinema:", selectedFormat);

        screenTypeName = selectedFormat.text;
    });
    
    
    async function getShowByHallId(hallId)
    {
        try
        {
            let hall = await axios.get(`/hall/${hallId}/show`);
            return hall.data;
        }
        catch(err)
        {
            console.log(err)
        }
    }

    $(document).on('click', '.editbtn', function(e){
        e.preventDefault();
        $('#btnHallSave').text('Update');
        let Id = $(e.currentTarget).data('id');
        
        
        let findData = hallarr.find(e => e.Id == Id);
        gHallId = findData.Id;
        
            $('#cinemaFormat').append(new Option(findData.ScreenTypeName, findData.CinemaFormatId, true, true));
        
    
        // Set the value and trigger Select2 update
        $('#cinemaFormat').val(findData.CinemaFormatId).trigger('change');


        $('#hallName').val(findData.HallName);
        $('#seatCapacity').val(findData.SeatCapacity);
        $('#hallModal').modal('show');
    })

    $('#btnAddHall').on('click', function(){
        $('#btnHallSave').text('Save');
        $('#cinemaFormat').val(null).trigger('change'); 
        $('#hallName').val('');
        $('#seatCapacity').val('');
    })

    $('#cinemaFormat').select2({
        width: '100%',
        placeholder: "Select Cinema Format",
        allowClear: true,
        ajax: {
            transport: function (params, success, failure) {
                axios.get("/cinema-format/select", {
                    params: { searchTerm: params.data.term }
                })
                .then(response => {
                    console.log(response);
                    success({
                        results: response.data.map(format => ({
                            id: format.id,  
                            text: format.screenTypeName,
                            description: format.description || "No description available",
                            priceIcon: format.price ? `₱${format.price.toFixed(2)}` : "N/A", // Ensure price is formatted
                            price : format.price,
                            icon: format.icon || '🎥'
                        }))
                    });
                })
                .catch(error => {
                    failure(error);
                });
            },
            processResults: function (data) {
                return { results: data.results };
            },
            delay: 250
        },
        templateResult: function (data) {
            if (!data.id) return data.text; // Default text for placeholders
            
            let $element = $(`
                <div style="display: flex; align-items: center; gap: 10px;">
                    <span>${data.icon}</span>
                    <div>
                        <strong>${data.text}</strong>
                        <div style="font-size: 12px; color: green;">Price: ${data.priceIcon}</div>
                    </div>
                </div>
            `);
            return $element;
        },
        templateSelection: function (data) {
            return data.text || "Select Cinema Format";
        }
    }).on('select2:select', function(e){
        let selectedData = e.params.data;
        console.log("Selected Format:", selectedData);
    
        ticketPrice = selectedData.price;
    });

    $('#btnHallSave').on('click', function(){
        if($('#btnHallSave').text() == 'Update')
        {
            let hallResponse = {
                Id : gHallId,
                CinemaId : $(this).data('id'),
                HallName : $('#hallName').val(),
                CinemaFormatId : $('#cinemaFormat option:selected').val(),
                SeatCapacity : parseInt($('#seatCapacity').val()),
                TicketPrice : ticketPrice
            }
            axios.put('/hall',hallResponse)
            .then((success)=>{
                Swal.fire({
                    title : "Success",
                    text : success.data.message,
                    icon : "success"
                })
                .then(()=>{
                    $('#hallModal').modal('hide');
                    populateHall();
                })
            })
            .catch((err)=>{
                console.log(err);
            })
        }
        else
        {
            let hallResponse = {
                CinemaId : $(this).data('id'),
                HallName : $('#hallName').val(),
                CinemaFormatId : $('#cinemaFormat option:selected').val(),
                SeatCapacity : parseInt($('#seatCapacity').val())
            }
            // return console.log(hallResponse);
            axios.post('/hall', hallResponse)
            .then((success)=>{
                Swal.fire({
                    title : "Success",
                    text : success.data.message,
                    icon : "success"
                })
                .then(()=>{
                    populateHall();
                    $('#hallModal').modal('hide');
                })
            })
            .catch((err)=>{
                console.log(err);
            })
        }
        
    })

    $(document).on('click', '.btnDeleteShow', async function(){
        let showId = $(this).data('id');
    
        // Confirm before deleting
        let confirmDelete = await Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!"
        });
    
        if (confirmDelete.isConfirmed) {
            let result = await removeShow(showId);
            
            if (result.success) {
                Swal.fire({
                    title: "Success",
                    text: result.message,
                    icon: "success"
                }).then(async () => {
                    // Ensure hallId exists before fetching data
                        let dataShow = await getShowByHallId(result.hallId);
    
                        if (currentItemOpen) {
                            let table = $('#hTable').DataTable();
                            let row = table.row(currentItemOpen);
                            
                            if (row.child.isShown()) {
                                row.child(format(dataShow)).show();
                            }
                        }
                });
            } else {
                Swal.fire("Error", result.message || "Failed to delete the show.", "error");
            }
        }
    });
    
    async function removeShow(Id) {
        try {
            let response = await axios.delete(`/show/${Id}`);
            return response.data; // Return the response data only
        } catch (err) {
            console.error("Error deleting show:", err);
            Swal.fire("Error", "Something went wrong. Please try again.", "error");
            return null;
        }
    }
    
})