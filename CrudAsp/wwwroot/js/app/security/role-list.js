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
                { data : 'Id' , title : 'Role Id' },
                { data : 'Name' , title : 'Name' },
                { data : 'Description' , title : 'Description' },
                { data : 'Action', title: ''}
            ],
            columnDefs :[
                {
                    targets : 3,
                    render : function(data,type, row)
                    {
                        return `<button class="btn btn-sm btn-success btnRoleClaims" data-id="${row.Id}">Add Role Claims</button>`;
                    }
                }
            ]
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