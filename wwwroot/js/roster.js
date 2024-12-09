document.getElementById('searchInput').addEventListener('keyup', function () {
    const searchValue = this.value.toLowerCase().trim();;
    const tableRows = document.querySelectorAll('#rosterTable tbody tr');

    tableRows.forEach(row => {
        const nameCell = row.querySelector('td:nth-child(1)').textContent.toLowerCase().trim();
        const numberCell = row.querySelector('td:nth-child(3)').textContent.toLowerCase().trim();
        const SkiCell = row.querySelector('td:nth-child(5)').textContent.toLowerCase().trim();

        if (nameCell.includes(searchValue) || numberCell.includes(searchValue) || SkiCell.includes(searchValue)) {
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
});