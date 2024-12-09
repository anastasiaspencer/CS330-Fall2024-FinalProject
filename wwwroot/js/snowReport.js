
const resorts = [
    { name: "Mt.Rose Ski Tahoe", id: "1" },
    { name: "Alta Ski Area", id: "2" },
    { name: "Breckenridge, CO", id: "3" }
];

// Function to fetch snow report data for a given resort
async function fetchSnowReport(resortName, num) {
    if (!resortName) {
        alert("Invalid Resort Name");
        return;
    }

    try {
        // Calls API to get the snow report, the fetch calls the controlelr through convention based routing
        const response = await fetch(`/SnowReport/Report?locationName=${resortName}`);
        if (response.ok) {
            const report = await response.json();
            // Update the page with the report
            const snowReportDiv = document.getElementById("snow-report" + num);
            snowReportDiv.innerHTML = `
                <div class="card-body text-center">
                    <h4 id="snowReport-locationName">${report.locationName}</h4>
                    <hr class="border-primary">
                    <p class="fs-7"><strong>Temperature:</strong> ${report.temperature} °C</p>
                    <p class="fs-7"><strong>Description:</strong> ${report.weatherDescription}</p>
                    <p class="fs-7"><strong>Pressure:</strong> ${report.pressure} hPa</p>
                    <p class="fs-7"><strong>Humidity:</strong> ${report.humidity} %</p>
                </div>
            `;

        } else {
            alert("Failed to get report");
        }
    } catch (error) {
        console.error("Error whiile Displaying the reports:", error);
        alert("Error while Displaying the reports.");
    }
}

document.addEventListener("DOMContentLoaded", function () {

    fetchAllReports();
    // Dynamically updates every 30 seconds
    setInterval(fetchAllReports, 30000);
});

function fetchAllReports() {
    resorts.forEach(resort => {
        fetchSnowReport(resort.name, resort.id);
    });
}
