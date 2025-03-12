$(document).ready(function(){
    let hallarr = [];
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

    async function populateHall()
    {
        let data = await getHall();
        console.log(data);
        data.forEach((e)=>{
            hallarr.push({
                Id : e.id,
                HallName : e.hallName,
                SeatCapacity : e.seatCapacity,
                created_at : e.created_at,
                CinemaFormatId : e.cinemaFormatId,
                ScreenTypeName : e.screenTypeName,
                action: ''
            })
        })

        $('#hTable').DataTable().destroy();
        $('#hTable').DataTable({
            paging: false,  
            searching: false,  
            info: false,          
            lengthChange: false,
            data : hallarr,
            columns : [
                { data : 'HallName', title : 'Name' },
                { data : 'SeatCapacity', title : 'SeatCapacity' },
                { data : 'action'}
            ],
            columnDefs :[
                {
                    targets : 2,
                    render: function(data,type,row)
                    {
                        return `<button class="btn btn-netflix-secondary editbtn" data-id="${row.Id}">Edit</button>`
                    }
                }
            ]
        })
    }

    $(document).on('click', '.editbtn', function(e){
        e.preventDefault();
        let Id = $(e.currentTarget).data('id');

        let findData = hallarr.find(e => e.Id == Id);
        console.log(findData);
        
        if ($('#cinemaFormat').find(`option[value="${findData.Id}"]`).length === 0) {
            $('#cinemaFormat').append(new Option(findData.ScreenTypeName, findData.Id, true, true));
        }
    
        // Set the value and trigger Select2 update
        $('#cinemaFormat').val(findData.Id).trigger('change');


        $('#hallName').val(findData.HallName);
        $('#seatCapacity').val(findData.SeatCapacity);
        $('#hallModal').modal('show');
    })

    $('#btnAddHall').on('click', function(){
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
                            price: format.price ? `₱${format.price.toFixed(2)}` : "N/A", // Ensure price is formatted
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
                        <div style="font-size: 12px; color: green;">Price: ${data.price}</div>
                    </div>
                </div>
            `);
            return $element;
        },
        templateSelection: function (data) {
            return data.text || "Select Cinema Format";
        }
    });

    $('#btnHallSave').on('click', function(){
        let hallResponse = {
            CinemaId : $(this).data('id'),
            HallName : $('#hallName').val(),
            CinemaFormatId : $('#cinemaFormat option:selected').val(),
            SeatCapacity : parseInt($('#seatCapacity').val())
        }

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
    })
})