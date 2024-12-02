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
            Promise.all([
                fetch('/get_current_reservations').then(response => response.json()),
                fetch('/get_old_reservations').then(response => response.json())
            ])
                .then(dataArray => {
                    const [currentReservations, oldReservations] = dataArray;

                    currentReservations.forEach((event, index) => {
                        const colorIndex = index % darkPalette.length;
                        event.backgroundColor = darkPalette[colorIndex];
                        event.borderColor = darkPalette[colorIndex];
                    });

                    oldReservations.forEach(event => {
                        event.backgroundColor = 'gray';
                        event.borderColor = event.backgroundColor;
                    });

                    const allEvents = [...currentReservations, ...oldReservations];
                    successCallback(allEvents);
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
        },
        eventClick: function (info) {
            const modal = document.createElement('div');
            modal.className = 'modal';
            modal.style.position = 'fixed';
            modal.style.left = '50%';
            modal.style.top = '50%';
            modal.style.transform = 'translate(-50%, -50%)';
            modal.style.backgroundColor = '#fff';
            modal.style.padding = '20px';
            modal.style.boxShadow = '0 4px 8px rgba(0, 0, 0, 0.2)';
            modal.style.borderRadius = '8px';
            modal.style.zIndex = '1000';

            modal.innerHTML = `
                <h3>Informacje o rezerwacji</h3>
                <p><strong>Tytuł:</strong> ${info.event.title}</p>
                <p><strong>Data rozpoczęcia:</strong> ${new Date(info.event.start).toLocaleString()}</p>
                <p><strong>Data zakończenia:</strong> ${new Date(info.event.end).toLocaleString()}</p>
                <p><strong>Opis:</strong> ${info.event.extendedProps.description || 'Brak opisu'}</p>
                <button id="closeModal" style="margin-top: 10px;">Zamknij</button>
            `;

            document.body.appendChild(modal);

            document.getElementById('closeModal').addEventListener('click', function () {
                document.body.removeChild(modal);
            });
        }
    });

    calendar.render();
});