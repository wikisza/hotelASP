const darkPalette = [
    "#4B0082", // Indigo
    "#2F4F4F", // Dark Slate Gray
    "#8B0000", // Dark Red
    "#556B2F", // Dark Olive Green
    "#483D8B", // Dark Slate Blue
    "#191970", // Midnight Blue
    "#2C2C54", // Deep Dark Blue
    "#3C3C3C"  // Dark Gray
];


document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        firstDay: '1',
        events: function (fetchInfo, successCallback, failureCallback) {
            fetch('/get_reservations')
                .then(response => response.json())
                .then(data => {
                    data.forEach((event, index) => {
                        const colorIndex = index % darkPalette.length;
                        event.backgroundColor = darkPalette[colorIndex];
                        event.borderColor = darkPalette[colorIndex];
                    });

                    console.log('Events with colors:', data);
                    successCallback(data);
                })
                .catch(error => {
                    console.error('Error fetching events:', error);
                    failureCallback(error);
                });
        },
        locale: 'pl',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        buttonText: {
            today: 'Dziś',
            month: 'Miesiąc',
            week: 'Tydzień',
            day: 'Dzień'
        }
    });

    calendar.render();
});