$(document).ready(function(){
    let screenTypeName;
    let cinemaFormatId;
    let arrayFormats = [];

    $('#cinemaFormatSave').on('click', function(){
        // let cinema = new CinemaFormat();
        // cinema.screenType = parseInt($('#cinemaFormat option:selected').val());
        // cinema.screenTypeName = screenTypeName;
        // cinema.description = $('#formatDescription').val();
        // cinema.price = $('#formatPrice').val();


        if($('#cinemaFormatSave').text() == 'Update')
        {
            let cinemaFormat = {
                Id  :  cinemaFormatId,
                // ScreenType : parseInt($('#cinemaFormat option:selected').val()),
                // screenTypeName : screenTypeName,
                Description : $('#formatDescription').val(),
                Price : $('#formatPrice').val()
            }
            // return console.log(cinemaFormat);
            axios.put('/cinema-format', cinemaFormat)
            .then((success)=>{
                Swal.fire({
                    title : "Success",
                    text : success.data.message,
                    icon : "success"
                })
                .then(()=>{
                    renderFormats();
                    
                    $('#btnHallShow').modal('hide');
                    
                })
            })
            .catch((err)=>{
                console.log(err);
                Swal.fire({
                    title : "Error",
                    text : err.response.data.message,
                    icon : "error"
                });
            })
        }
        else
        {

            let cinemaFormat = {
                ScreenType : parseInt($('#cinemaFormat option:selected').val()),
                screenTypeName : screenTypeName,
                Description : $('#formatDescription').val(),
                Price : $('#formatPrice').val()
            }
            axios.post('/cinema-format', cinemaFormat)
            .then((response)=>{
                console.log(response);
                Swal.fire({
                    title : "Success",
                    text : response.data.message,
                    icon : "success"
                })
                .then(()=>{
                    renderFormats();
                    
                    $('#btnHallShow').modal('hide');
                    
                })
            })
            .catch((err)=>{
                console.log(err);
                Swal.fire({
                    title : "Error",
                    text : err.response.message,
                    icon : "error"
                });
            });
        }
    })

    $('#cinemaFormat').select2({
        width: '100%', // Optional: Ensures Select2 fits properly
        placeholder: "Select Cinema Format",
        allowClear: true ,
        ajax: {
            transport: function (params, success, failure) {
                axios.get("/CinemaFormat/GetScreenTypes", {
                    params: { searchTerm: params.data.term }
                })
                .then(response => {
                    

                    success({
                        results: response.data.map(screentype => ({
                            id: screentype.id,  // Make sure this matches backend
                            text: screentype.name
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

    async function getformats()
    {
        try
        {
            let list = await axios.get('/cinema-format/api/list');

            return list.data;
        }
        catch(error)
        {
            console.log(error);
        }
    }

    async function renderFormats()
    {
        let response = await getformats();

        arrayFormats = [];
        response.data.forEach((e)=>{
            arrayFormats.push({
                Id : e.id,
                ScreenType : e.screenType,
                ScreenTypeName : e.screenTypeName,
                Description : e.description,
                Price : e.price,
                Action : ''
            })
        })

        $('#formatTable').DataTable().destroy();
        $('#formatTable').DataTable({
            data : arrayFormats,
            columns : [
                { data : 'ScreenTypeName', title : 'ScreenType'},
                { data : 'Description', title : 'Description'},
                { data : 'Price', title : 'Price'},
                { data : 'Action', title : ''}
            ],
            columnDefs : [
                { 
                    targets : 3,
                    render : function(data, type , row)
                    {
                        return `<button class="btn btn-netflix-secondary edtnBtn" data-id="${row.Id}">Edit</button>`;
                    }
                }
            ]
        })

    }
    renderFormats();

    $(document).on('click', '.edtnBtn', function () {
        let Id = $(this).data('id');
        let findFormat = arrayFormats.find(e => e.Id == Id);
    
        if (!findFormat) {
            console.error("Format not found for ID:", Id);
            return;
        }
    
        console.log(findFormat);
    
        $('#cinemaFormatSave').text('Update');
        cinemaFormatId = findFormat.Id;
        screenTypeName = findFormat.ScreenTypeName;
    
        let $cinemaFormat = $('#cinemaFormat'); // Cache Select2 element
    
        // Check if option already exists in Select2, if not, add it
        if ($cinemaFormat.find(`option[value="${findFormat.ScreenType}"]`).length === 0) {
            let newOption = new Option(findFormat.ScreenTypeName, findFormat.ScreenType, false, false);
            $cinemaFormat.append(newOption).trigger('change');
        }
    
        // Set the selected value and trigger Select2 update
        $cinemaFormat.val(findFormat.ScreenType).trigger('change');
        
        $cinemaFormat.prop('disabled', true);
        $cinemaFormat.select2({ disabled: true, width : '100%' });
        // Populate other form fields
        $('#formatDescription').val(findFormat.Description);
        $('#formatPrice').val(findFormat.Price);
    
        // Show modal
        $('#btnHallShow').modal('show');
    });
    

    $('#btnAddFormat').on('click', function () {
        $cinemaFormat.prop('disabled', false);
        $cinemaFormat.select2({ disabled: false, width : '100%' });
        $('#cinemaFormat').val('').trigger('change.select2');
        $('#formatDescription').val('');
        $('#formatPrice').val('');
    });
    
})