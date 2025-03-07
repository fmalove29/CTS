$(document).ready(function(){
    $('#cinemaFormat').select2({
        placeholder: "Select a cinema",
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

        // Access selected cinema details
        console.log("Cinema ID:", selectedFormat.id);
        console.log("Cinema Name:", selectedFormat.name);
    });
})