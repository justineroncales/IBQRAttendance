$(document).ready(function () {
    SetDataTable();
    SetDataTables();
})
function SetDataTable() {
    var ttt = $("#demoGrid").DataTable({
        "dom": '<<"d-flex flex-row justify-content-between"<"button"><"searchBy"f>><t><"row"<"col-6"<"text-left"i>><"col-6"<"float-right"<"q-paginate"p>>>>>',
        "order": [[1, "desc"]],
        "lengthMenu": [4],
        "lengthChange": false,
        columns: [
            { data: 'DAYS' },
            { data: 'STAMPS' },
            { data: 'FULLNAME' },
            { data: 'ACTIVITY' },
            { data: 'LEVEL' },
            { data: 'DEPARTMENT' }
        ]
    });

    ttt.each(function (value, index) {
    });
 
}
function getAttendanceList(day) {
    var filter = day;
    console.log(day);
    if (filter != "" && filter != null) {
        $.ajax({
            url: '/QRCode/Attendance/',
            data: { deptid: filter },
            type: "GET",
            success: function (data) {
                location.reload();
            }
        });
    }
}
function SetDataTables() {
    var ttt = $("#RTable").DataTable({
        "dom": '<<"d-flex flex-row justify-content-between"<"button"><"searchBy"f>><t><"row"<"col-6"<"text-left"i>><"col-6"<"float-right"<"q-paginate"p>>>>>',
        "order": [[1, "desc"]],
        "lengthMenu": [9],
        "lengthChange": false,
        columns: [
            { data: 'FIRSTNAME' },
            { data: 'LASTNAME' },
            { data: 'MIDDLENAME' },
            { data: 'EMAIL' },
            { data: 'ADDRESS' },
            { data: 'EMAIL' },
            { data: 'PHONENUMBER' },
            { data: 'LEVEL' },
            { data: 'DEPARTMENT' },
            { data: 'ImageID' }
        ]
    });

    ttt.each(function (value, index) {
    });
}