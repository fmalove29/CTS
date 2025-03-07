// $(document).ready(function () {
//     // const table = $('#menuTable').DataTable({
//     //     data : [
//     //         "",              // The control column (e.g., details-control icon)
//     //         "Tiger Nixon",   // Name
//     //         "System Architect", // Position
//     //         "Edinburgh",     // Office
//     //         "$320,800"       // Salary
//     //     ]
//     // });

//     // // Event listener for opening and closing details
//     // $('#menuTable tbody').on('click', 'td.details-control', function () {
//     //     const tr = $(this).closest('tr');
//     //     const row = table.row(tr);

//     //     if (row.child.isShown()) {
//     //         // Hide row details
//     //         row.child.hide();
//     //         tr.removeClass('shown');
//     //     } else {
//     //         // Show row details
//     //         row.child(drawerFormat(row.data())).show();
//     //         tr.addClass('shown');
//     //     }
//     // });

//     // function drawerFormat(data) {
//     //     // Example of building a drawer
//     //     return `
//     //         <div style="padding: 10px; background: #f9f9f9; border: 1px solid #ddd;">
//     //             <strong>Details for:</strong> ${data[1]}<br>
//     //             <strong>Position:</strong> ${data[2]}<br>
//     //             <strong>Office:</strong> ${data[3]}<br>
//     //             <strong>Salary:</strong> ${data[4]}<br>
//     //         </div>
//     //     `;
//     // }
    
// });

$(document).ready(function () {
    const table = $('#menuTable').DataTable({
        columns: [
            { className: 'details-control', orderable: false, data: null, defaultContent: '' },
            { data: 'name', title: 'Name' },
            { data: 'position', title: 'Position' },
            { data: 'office', title: 'Office' },
            { data: 'salary', title: 'Salary' }
        ],
        columnDefs : [
            { targets : 0, render : function(data, type, row){ return `>`}}
        ],
        data: [
            {
                name: "Tiger Nixon",
                position: "System Architect",
                office: "Edinburgh",
                salary: "$320,800"
            },
            {
                name: "Garrett Winters",
                position: "Accountant",
                office: "Tokyo",
                salary: "$170,750"
            }
        ]
    });

    $('#menuTable tbody').on('click', 'td.details-control', function () {
        const tr = $(this).closest('tr');
        const row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        } else {
            row.child(`<div>Details for ${row.data().name}</div>`).show();
            tr.addClass('shown');
        }
    });

});
