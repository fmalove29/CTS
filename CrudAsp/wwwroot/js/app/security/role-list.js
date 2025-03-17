$(document).ready(function(){
    let arrd = [];
    let roleIdRef;
    let roleClaimTypeRef;
    function getRoles()
    {
        axios.get('/role/list')
        .then(async(e)=>{
            console.log(e)
            e.data.forEach((r)=>{
                arrd.push({
                    Id: r.id,
                    Name : r.name,
                    Description : r.normalizedName,
                    Action : ''
                });
            })
            await init_rTable(arrd);
        })
        .catch((err)=>{
            console.log(err);
        })
    }

    getRoles();
    init_rTable();

    async function init_rTable(arr)
    {

        $('#r_table').DataTable().destroy();
        $('#r_table').DataTable({
            data : arr,
            columns : [
                { data : 'Name' , title : 'Name' },
                { data : 'Description' , title : 'Description' },
                { data : 'Action', title: ''}
            ],
            columnDefs :[
                {
                    targets : 2,
                    render : function(data,type, row)
                    {
                        return `<button class="btn btn-netflix-secondary btnRoleClaims" data-id="${row.Id}">Claims</button>`;
                    }
                },
                {
                    targets: 0,
                    render : function(data, type, row)
                    {
                        return `<p><span class="badge bg-danger">${row.Name}</span></p><p><span class="badge bg-secondary">${row.Id}</span></p> `
                    }
                }
            ],
            // pagingType: "full_numbers", // Enables full pagination controls
            // pageLength: 10, // Adjust number of rows per page
            // language: {
            //     paginate: {
            //         first: '«', // Custom icons
            //         last: '»',
            //         next: '›',
            //         previous: '‹'
            //     },
            //     drawCallback: function () {
            //         // Remove old class to prevent duplication
            //         // $('.dataTables_paginate .paginate_button').removeClass('netflix-page-number');
        
            //         // Add class only to numeric page indicators
            //         $('.dataTables_paginate .paginate_button:not(.previous):not(.next):not(.first):not(.last)').addClass('bg-danger');
            //     }
            // }
        })
    }
    $(document).on('click', '.btnRoleClaims', async function(){
        let Id = $(this).data('id');

        let findRole = arrd.find(e => e.Id == Id);
        roleIdRef = findRole.Id;
        roleClaimTypeRef = findRole.Name;

        let claimsById = await GetRoleClaims(roleIdRef);
        renderRoleClaims(claimsById);
        $('#exampleModalLabel').text(findRole.Name);
        $('#role_claimsModal').modal('show');
    })

    $('#btnSaveRole').on('click', function(){
            let CreateRoleDTO = {
                name : $('#roleName').val(),
                description : $('#roleDescription').val()
            }

            axios.post('/create-role', CreateRoleDTO)
            .then((e)=>{
                console.log(e);
                    Swal.fire({
                        title : "Success",
                        text : e.data,
                        icon : "success"
                    })
                    .then(()=>{
                        getRoles();
                        
                        $('#addRoleModal').hide(); // Close the modal
                        $('body').removeClass('modal-open'); // Ensure no leftover classes
                        $('.modal-backdrop').remove();
                        
                    })
                
            })
            .catch((err)=>{
                console.log(err);
                Swal.fire({
                    title : "Error",
                    text : err.response.data,
                    icon : "error"
                })
            })

        
    })

    // $('#moduleTree').jstree({
    //     "plugins":["wholerow","checkbox"]
    // });

    $('#management_level').select2({
        width: '100%',
        placeholder: "Select Management Level",
        allowClear: true ,
        dropdownParent: $('#role_claimsModal'),
        ajax: {
            transport: function (params, success, failure) {
                axios.get("/management-level", {
                    params: { searchTerm: params.data.term }
                })
                .then(response => {
                    

                    success({
                        results: response.data.map(screentype => ({
                            id: screentype.name,  // Make sure this matches backend
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
    });


    $('#saveRoleClaims').on('click', function(){
        let roleClaim = {
            RoleId : roleIdRef,
            ClaimType : roleClaimTypeRef,
            ClaimValue : $('#management_level option:selected').val()
        }

        axios.post('/role-claims', roleClaim)
        .then((success)=>{
            if(success.data.success == true)
            {
                GetRoleClaims(roleClaim.RoleId)
                toastr.success(success.data.message ,"Success");
            }
        })
        .catch((err)=>{
            toastr.error(err.response.data.message, "Error");
        })
    });

    async function GetRoleClaims(Id)
    {
        try
        {
            let roleClaims = await axios.get('/role-claims/'+ Id);
            renderRoleClaims(roleClaims.data);
            return roleClaims.data;
        }
        catch(error)
        {
            console.error(error);
        }
    }

    function renderRoleClaims(claimsData)
    {
        let claim = [];
        claimsData.forEach((e)=>{
            claim.push({
                Type : e.type,
                Value : e.value,
                Action : ''
            })
        })
        $('#claims_table').DataTable().destroy();
        $('#claims_table').DataTable({
            data : claim,
            columns : [
                { data : 'Type', title : 'Type'},
                { data : 'Value', title : 'Value'},
                { data : 'Action', title : ''}
            ],
            columnDefs : [
                {   
                    className : 'w-10 text-center',
                    targets : 2,
                    render : function(data, type, row)
                    {
                        return `<button class="btn btn-netflix-secondary btnDeletRoleClaim"  data-ctype="${row.Type}" data-cvalue="${row.Value}">Delete</button>`;
                    }
                    
                }
            ]
        });
    }

    $(document).on('click', '.btnDeletRoleClaim', function(e){
        let rcId = roleIdRef;
    
        let roleClaim = {
            RoleId: rcId,
            ClaimType: $(e.currentTarget).data('ctype'),
            ClaimValue: $(e.currentTarget).data('cvalue')
        };
    
        axios.delete(`/delete-roleClaim/${rcId}`, {
            data: roleClaim, 
            headers: { "Content-Type": "application/json" }
        })
        .then(async (success) => {
            if (success.data.success) {
                await GetRoleClaims(rcId);
                toastr.success(success.data.message ,"Success");
            }
        })
        .catch((err) => {
            toastr.error(err.response.data.message, "Error");
        });
    });
    

})