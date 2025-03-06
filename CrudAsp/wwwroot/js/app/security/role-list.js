$(document).ready(function(){
    function getRoles()
    {

        let arrd = [];
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
    $(document).on('click', '.btnRoleClaims', function(){
        alert($(this).data('id'));
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

    $('#moduleTree').jstree({
        "plugins":["wholerow","checkbox"]
    });


})