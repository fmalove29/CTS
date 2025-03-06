$(document).ready(function(){

    let currentStep = 0;
    const steps = document.querySelectorAll(".step");
    const progressBar = document.getElementById("progress-bar");

    $('#cinemaSelect').select2({
        placeholder: "Select a cinema",
        ajax: {
            transport: function (params, success, failure) {
                axios.get("/Booking/Cinema", {
                    params: { searchTerm: params.data.term }
                })
                .then(response => {
                    

                    success({
                        results: response.data.map(cinema => ({
                            id: cinema.id,  // Make sure this matches backend
                            text: cinema.cinemaName
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
    })
    .on('select2:select', function (e) {
        let selectedCinema = e.params.data; // Get selected cinema object
        console.log("🎯 Selected Cinema:", selectedCinema);

        // Access selected cinema details
        console.log("Cinema ID:", selectedCinema.id);
        console.log("Cinema Name:", selectedCinema.text);
    });
    


    $('#stepperNext1').on('click', function(){
        nextStep();
    })
    $('#stepperNext2').on('click', function(){
        nextStep();
    })

    $('#prev1').on('click' , function(){
        prevStep();
    })

    $('#prev2').on('click', function(){
        prevStep()
    })

    function showStep(stepIndex) {
        steps.forEach((step, index) => {
            step.classList.toggle("active", index === stepIndex);
        });

        // Update progress bar
        const progress = ((stepIndex + 1) / steps.length) * 100;
        progressBar.style.width = progress + "%";
        progressBar.textContent = `Step ${stepIndex + 1} of ${steps.length}`;
    }

    function nextStep() {
        const inputs = steps[currentStep].querySelectorAll("input");
        let isValid = true;
        console.log(inputs);
        inputs.forEach(input => {
            if (!input.value.trim()) {
                isValid = false;
                input.classList.add("is-invalid");
            } else {
                input.classList.remove("is-invalid");
            }
        });

        if (isValid) {
            currentStep++;
            if (currentStep < steps.length) {
                showStep(currentStep);
            } else {
                document.getElementById("multiStepForm").submit();
            }
        }
    }

    function prevStep() {
        if (currentStep > 0) {
            currentStep--;
            showStep(currentStep);
        }
    }
})