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

        data.forEach((e)=>{
            hallarr.push({
                Id : e.id,
                HallName : e.hallName,
                SeatCapacity : e.seatCapacity,
                created_at : e.created_at,
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
        
        $('#hallName').val(findData.HallName);
        $('#seatCapacity').val(findData.SeatCapacity);
        $('#btnHallShow').modal('show');
    })

    $('#btnAddHall').on('click', function(){
        $('#hallName').val('');
        $('#seatCapacity').val('');
    })
})