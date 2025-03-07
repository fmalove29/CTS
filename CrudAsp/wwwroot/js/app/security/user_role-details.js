$(document).ready( function(){
    let uD = [];
    let aR = [];
    

    function GetRolesByUserId()
    {
        let roleArr = [];
        axios.get('/user-role/list/' + $('#userId').val())
        .then(async (e)=>{
            e.data.forEach((e)=>{
                roleArr.push({
                    Id : e.id,
                    Name : e.name,
                    NormalizedName : e.normalizeName,
                    Action : ''
                })
            })
            const h = await init_rTable(roleArr);
            return h;
        })
        .catch((err)=>{
            console.log(err);
        })
    }
    
    GetRolesByUserId();


    async function init_rTable(arr)
    {

        $('#rTb').DataTable().destroy();
        $('#rTb').DataTable({
            data : arr,
            columns : [
                { data : 'Id' , title : 'Role Id' },
                { data : 'Name' , title : 'Name' },
                { data : 'NormalizedName' , title : 'Description' },
                { data : 'action', title: ''}
            ],
            columnDefs : [
                {
                    targets : 3,
                    render : function(data, type, row)
                    {
                        return `<button class="btn btn-danger btnDeletUserRole" data-id=${row.Id}>Delete</button>`;
                    }
                }
            ]
        })
    }

    $(document).on('click', '.btnDeletUserRole', function(){
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
            }).then((result) => {
            if (result.isConfirmed) {
                let RoleDTO = {
                    roleId : $(this).data('id'),
                    userId : $('#userId').val()
                }
                
                axios.post('/delete/user-role', RoleDTO)
                .then((e)=>{
                    Swal.fire({
                        title: "Deleted!",
                        text: e.data.message,
                        icon: "success"
                    }).then(()=>{
                        GetRolesByUserId();
                    });
                })
                .catch((err)=>{
                    console.log(err);
                })
            }
        });
        
        
    })

    async function init_addRoleTable()
    {
        const data = await getRole();
        data.forEach((e)=>{
            aR.push({
                Id : e.id,
                Name : e.name,
                NormalizedName : e.normalizedName,
                ConcurrencyStamp : e.concurrencyStamp,
                Action : ''
            })
        })

        $('#addRoleTb').DataTable().destroy();
        $('#addRoleTb').DataTable({
            data : aR,
            columns : [
                { data : 'Name', title: 'Name'},
                { data : 'NormalizedName', title : 'Description' },
                { data : 'Action', title : ''}
            ],
            columnDefs : [
                { 
                    targets : 2,
                    render : function(data, type, row)
                    { return `<button class="btn btn-netflix-secondary btnAddRole" data-id="${row.Id}">Add</button>`}
                    
                }
            ]

        })
    }



    init_addRoleTable()

    init_rTable();

    async function getRole()
    {
        try
        {
            const q = await axios.get('/role/list');
            return q.data;
        }
        catch(e)
        {
            console.log(e);
            return [];
        }
    }


    $(document).on('click', '.btnAddRole',(e)=>{
        e.preventDefault();
        let roleId = $(e.currentTarget).data('id');
        let userId = $('#userId').val();

        // return console.log(userId);


        

        let RoleDTO = {
            roleId : roleId,
            userId : userId
        }
        // return console.log('test');
        // return console.log(userRoleDTO);
        axios.post('/assign-role', RoleDTO)
        .then(async (e)=>{
            console.log(e);
            if(e.data.succeeded == true)
            {
                GetRolesByUserId();
                toastr.success("Your changes have been saved!" ,"Success");

                
            }
            else{
                e.data.errors.forEach((tError)=>{
                    toastr.error(tError.description, "Error");
                })
            }
        })
        .catch((err)=>{
            console.log(err);
        })


        
    })

})