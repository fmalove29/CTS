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
        width: '100%',
        placeholder: "Select Cinema Format",
        allowClear: true,
        ajax: {
            url: "/CinemaFormat/GetScreenTypes",
            dataType: "json",
            delay: 250,
            data: function (params) {
                console.log("Search term:", params.term);
                return { searchTerm: params.term || "" };
            },
            processResults: function (data) {
                // Map the API response to the format expected by Select2
                return {
                    results: data.map(function(screentype) {
                        return {
                            id: screentype.id,   // must match your backend
                            text: screentype.name // display text
                        };
                    })
                };
            }
        }
    }).on('select2:select', function (e) {
        let selectedFormat = e.params.data;
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
        
        // Save format values globally if needed
        cinemaFormatId = findFormat.Id;
        screenTypeName = findFormat.ScreenTypeName;
        
        var $cinemaFormat = $('#cinemaFormat'); // Cache the select element
        
        // Check if the option already exists; if not, add it
        if ($cinemaFormat.find(`option[value="${findFormat.ScreenType}"]`).length === 0) {
            let newOption = new Option(findFormat.ScreenTypeName, findFormat.ScreenType, false, false);
            $cinemaFormat.append(newOption).trigger('change');
        }
        
        // Set the selected value and update Select2
        $cinemaFormat.val(findFormat.ScreenType).trigger('change');
        
        // Disable the select input so the user cannot change it during edit
        $cinemaFormat.prop('disabled', true);
        // Reinitialize Select2 to reflect the disabled state (if necessary)
        $cinemaFormat.select2({
            width: '100%',
            placeholder: "Select Cinema Format",
            allowClear: true
        });
        
        // Populate other form fields with the format's details
        $('#formatDescription').val(findFormat.Description);
        $('#formatPrice').val(findFormat.Price);
        
        // Show the Edit modal (adjust modal id as needed)
        $('#btnHallShow').modal('show');
    });
    

    $(document).on('click', '#btnAddFormat', function () {
        var $cinemaFormat = $('#cinemaFormat');
        $('#cinemaFormatSave').text('Save');
        // Enable the select input
        $cinemaFormat.prop('disabled', false);
        
        // If the select is already initialized with Select2, destroy it before reinitializing.
        if ($cinemaFormat.data('select2')) {
            $cinemaFormat.select2('destroy');
        }
        
        // Reinitialize Select2 (if necessary) to ensure it reflects the enabled state
        $cinemaFormat.select2({
            width: '100%',
            placeholder: "Select Cinema Format",
            allowClear: true,
            ajax: {
                url: "/CinemaFormat/GetScreenTypes",
                dataType: "json",
                delay: 250,
                data: function (params) {
                    return { searchTerm: params.term || "" };
                },
                processResults: function (data) {
                    return {
                        results: data.map(function(screentype) {
                            return {
                                id: screentype.id,
                                text: screentype.name
                            };
                        })
                    };
                }
            }
        });
        
        // Clear any previous selection and other fields
        $cinemaFormat.val(null).trigger('change');
        $('#formatDescription').val('');
        $('#formatPrice').val('');
        
        // Show the Add modal (adjust modal id as needed)
        $('#addFormatModal').modal('show');
    });
    
})