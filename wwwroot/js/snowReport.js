
        const resorts = [
        {name: "Mt.Rose Ski Tahoe", id: "1" },
        {name: "Alta Ski Area", id: "2"},
        {name: "Breckenridge, Colorado", id: "3"}
        ];

        // Function to fetch snow report data for a given resort
        async function fetchSnowReport(resortName, num) {
            if (!resortName) {
                alert("Invalid Resort Name");
                return;
            }

            try {
                // Calls API to get the snow report
                const response = await fetch(`/SnowReport/Report?locationName=${resortName}`);
                if (response.ok) {
                    const report = await response.json();
                    // Update the page with the report
                    const snowReportDiv = document.getElementById("snow-report" + num);
                    snowReportDiv.innerHTML = `
                        <h3>Snow Report for ${report.locationName}</h3>
                        <p>Temperature: ${report.temperature} °C</p>
                        <p>Description: ${report.weatherDescription}</p>
                        <p>Snowfall: ${report.snowfall} cm</p>
                    `;
                } else {
                    alert("Failed to get report");
                }
            } catch (error) {
                console.error("Error fetching snow report:", error);
                alert("An error occurred while fetching the snow report.");
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            
            resorts.forEach(resort => {
                fetchSnowReport(resort.name, resort.id);
            });

            // Dynamically updates every 30 seconds
            setInterval(fetchAllReports, 30000);
        });
