document.addEventListener('DOMContentLoaded', function () {
    const randevuForm = document.getElementById('randevuForm');
    randevuForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        const calisan = document.getElementById('calisan').value;
        const tarih = document.getElementById('tarih').value;

        const randevuData = {
            calisanId: calisan,
            tarih: tarih
        };

        // Randevu almayı backend'e gönderme
        const response = await fetch('https://localhost:5433/api/randevu/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(randevuData)
        });

        if (response.ok) {
            const result = await response.json();
            alert(result.message);
        } else {
            alert('Bir hata oluştu, lütfen tekrar deneyin.');
        }

        randevuForm.reset();
    });
});
