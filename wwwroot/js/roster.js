document.getElementById('searchInput').addEventListener('keyup', function () {
    const searchValue = this.value.toLowerCase();
    const tableRows = document.querySelectorAll('#rosterTable tbody tr');

    tableRows.forEach(row => {
        const nameCell = row.querySelector('td:nth-child(1)').textContent.toLowerCase();
        const numberCell = row.querySelector('td:nth-child(2)').textContent.toLowerCase();

        if (nameCell.includes(searchValue) || numberCell.includes(searchValue)) {
            row.style.display = ''; // Show row
        } else {
            row.style.display = 'none'; // Hide row
        }
    });
});