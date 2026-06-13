// URL base de tu API (cambiar según el puerto donde esté hosteada)
const API_BASE_URL = 'http://localhost:5056/api';

async function buscarBecas() {
    const carrera = document.getElementById('carrera').value;

    if (!carrera) {
        alert('Por favor selecciona una carrera');
        return;
    }

    mostrarCargando(true);

    try {
        const response = await fetch(`${API_BASE_URL}/becas/carrera/${encodeURIComponent(carrera)}`);
        
        if (!response.ok) {
            throw new Error('Error en la API');
        }

        const becas = await response.json();
        mostrarResultados(becas);
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('resultados').innerHTML = 
            '<div class="sin-resultados">Error al conectar con la API. Verifica que esté en línea.</div>';
    } finally {
        mostrarCargando(false);
    }
}

function mostrarCargando(visible) {
    document.getElementById('loading').classList.toggle('hidden', !visible);
}

function mostrarResultados(becas) {
    const container = document.getElementById('resultados');

    if (becas.length === 0) {
        container.innerHTML = '<div class="sin-resultados">No se encontraron becas para esa carrera.</div>';
        return;
    }

    container.innerHTML = becas.map(beca => `
        <div class="beca-card">
            <h3>${beca.nombre}</h3>
            <p><span class="beca-label">Carrera:</span> ${beca.carrera}</p>
            <p><span class="beca-label">Descripción:</span> ${convertirLinks(beca.descripcion)}</p>
            <p><span class="beca-label">Requisitos:</span> ${convertirLinks(beca.requisitos)}</p>
            <p class="fecha-vencimiento">
                📅 Vence: ${new Date(beca.fechaLimite).toLocaleDateString('es-ES')}
            </p>
        </div>
    `).join('');
}

function convertirLinks(texto) {
    const urlRegex = /(https?:\/\/[^\s]+)/g;
    return texto.replace(urlRegex, url =>
        `<a href="${url}" target="_blank" rel="noopener noreferrer">${url}</a>`
    );
}
