$(document).ready(function(){
    let uArr = [];

        axios.get('/user-role')
        .then((e)=>{
            e.data.forEach((g)=>{
                uArr.push({
                    id          : g.id,
                    firstName   : g.firstName,
                    middleName  : g.middleName,
                    lastName    : g.lastName,
                    email       : g.email,
                    action      : ''
                });
            })
            init_uTable();
        })
        .catch((err)=>{
            console.log(err);
        })
        console.log(uArr);

    function init_uTable()
    {
        $('#uTable').DataTable().destroy();
        $('#uTable').DataTable({
            data : uArr,
            columns : [
                // {data : 'id', title : ''},
                {data : 'firstName', title : 'First Name' },
                {data : 'lastName', title : 'Last Name' },
                {data : 'middleName', title : 'Middle Name' },
                {data : 'email', title : 'Email' },
                {data : 'action', title : '' }
            ],
            columnDefs : [
                {
                    targets : 4,
                    render : function(data, type, row)
                    {
                        return `<div class="d-flex">
                                    <a class="btn .btn-netflix-info mr-2"  href="/User/Details/${row.id}">View</a>
                                    <button class="btn btn-sm btn-netflix-secondary  btnUC" data-toggle="modal" data-target="#userClaimsModal" data-id="${row.id}">Claims</button>
                                </div>`; 
                    }
                }
            ]
        })
    }

    $(document).on('click','.btnUC' ,function(){
        let findIdInArray = uArr.find(e => e.id == $(this).data('id'));
        
        populateUserClaim(findIdInArray);
    })

    function populateUserClaim(objUser)
    {
        let fullName = objUser.firstName + ' '+ objUser.middleName + ' '+ objUser.lastName;
        let hderTxt = `Add Claims to ${fullName}`;

        $('#uscHeaderText').html(`<strong>${hderTxt}</strong>`);
    }
    
})