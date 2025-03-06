
$(document).ready(function(){
    let showArr = [];
    $(document).on('click', '.btnHallShow', (e)=>{
        // $('#btnHallShow').modal('show');

        // let row = $(this).closest('tr');

        // // Extract data from each column in the row
        // let hallName = row.find('td').eq(0).text().trim(); // Hall Name
        // let seatCapacity = row.find('td').eq(1).text().trim(); // Seat Capacity
        // let hallId = $(e.currentTarget).data('id'); // Data attribute from the button

        axios.get('/show/'+ $(e.currentTarget).data('id'))
        .then((e)=>{
            return console.log(e);
        })
        .catch((err)=>{
            return console.log(err);
        })

        // return console.log();

    })

    // function getHall()
    // {
    //     $('#hTable').DataTable();
    // }

    getHall();
    $('#selMovie').select2({
        placeholder: 'Select a movie',
        allowClear : true,
        ajax: {
            url: '/show/movies/getAll', // Adjust to your endpoint
            dataType: 'json',
            data : function(params){
                return{
                    search : params.term
                }
            },
            processResults: function (data) {
                return {
                    results: data.map(movie => ({
                        id: movie.id,
                        text : movie.title,
                        
                    }))
                };
            }
        }
    }).on('select2:select', function(e){
        var data = e.params.data;

        showArr.push(data);
        
        sTable();
    });

    sTable();
    
    function sTable()
    {
        $('#sTable').DataTable().destroy();
        $('#sTable').DataTable({
    
        });
        console.log(showArr);
    }
})